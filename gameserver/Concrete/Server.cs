using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace gameserver
{
    class Server : IServer
    {
        private static readonly IServer instance = new Server();
        private readonly TcpListener listener = new TcpListener(Settings.Address,Settings.Port);

        private List<IClient> clients = new List<IClient>(Settings.MaxPlayers);
        private bool[] identifiers = new Boolean[Settings.MaxPlayers];

        public static IServer Instance {get {return instance;}}

        public event ServerLoggedHandler ServerLogged;

        #region Основные методы

        public void Start(IView view)
        {
            log("Server starting...");
            listener.Start();
            new Thread(new ThreadStart(listenForClients)).Start();
            log("Server started (" + Settings.Port + ").");
            if (Settings.LoadFailed)
                log("WARNING: " + Settings.SettingsFile + " wasn’t loaded properly.");
            msg("Type help for list of commands.");
            view.CommandSent += commandSent;
        }

        private void listenForClients()
        {
            try
            {
                while (true)
                {
                    TcpClient connection = listener.AcceptTcpClient();
                    if (clients.Count == Settings.MaxPlayers)
                    {
                        using (IClient temp = new Client(connection,-1))
                            sendData(temp,EConnectionResult.ServerIsFull);
                        continue;
                    }
                    acceptClient(connection);
                }
            }
            catch (SocketException) {return;}
        }

        public void Stop()
        {
            log("Server shutting down...");
            foreach (IClient client in clients)
                closeClient(client,false);
            listener.Stop();
            log("Server shut down.");
            log(String.Empty,false);
        }

        #endregion
        #region Клиентские методы

        private void acceptClient(TcpClient connection)
        {
            IClient client = new Client(connection,allocateIdentifier());
            clients.Add(client);
            new Thread(() => handleClient(client)).Start();
            sendData(client,EConnectionResult.Success);
        }

        private void handleClient(IClient client)
        {
            byte[] data;
            try
            {
                while ((data = client.ReadData()).Length != 0)
                    receiveData(client,data);
            }
            catch (IOException) {return;}
            closeClient(client);
        }

        private void closeClient(IClient client, bool logIt = true)
        {
            clients.Remove(client);
            int identifier = client.Identifier;
            string reason = !client.ReadTimeoutReached ? "quit" : "timeout";
            client.Dispose();
            if (client.Initialized && logIt)
                log(client.Identity + " disconnected (" + reason + ").");
            freeIdentifier(identifier);
        }

        #endregion
        #region Служебные методы

        public void log(string message, bool timestamp = true)
        {
            string text = (timestamp ? DateTime.Now.ToString(@"HH\:mm\:ss") + " " : String.Empty) + message;
            using (StreamWriter writer = new StreamWriter(new FileStream(Settings.LogFile,FileMode.Append,FileAccess.Write)))
                writer.WriteLine(text);
            ServerLogged(null,new ServerLoggedEventArgs(text,clients.Count));
        }

        private void msg(string message)
        {
            ServerLogged(this,new ServerLoggedEventArgs(message,clients.Count));
        }

        private int allocateIdentifier()
        {
            for (int i = 0; i < Settings.MaxPlayers; i++)
                if (!identifiers[i])
                {
                    identifiers[i] = true;
                    return i;
                }
            return -1;
        }

        private void freeIdentifier(int identifier)
        {
            identifiers[identifier] = false;
        }

        #endregion
        #region Команды сервера

        private void commandSent(object sender, CommandSentEventArgs e)
        {
            switch (e.Command)
            {
                case "help": commandHelp(); break;
                case "players": commandPlayers(); break;
                default: msg("Unknown command."); break;
            }
        }

        private void commandHelp()
        {
            msg("players: list of connected players.");
        }

        private void commandPlayers()
        {
            foreach (Client client in clients)
                msg(client.Identifier + " (" + client.Address + ").");
        }

        #endregion
        #region Передача данных

        private void sendData(IClient client, EServerMessage type, byte[] data, bool toAll = false)
        {
            byte[] buffer = new Byte[1] {(byte)type};
            Utilities.Append(ref buffer,!toAll ? 1 : 2,data);
            if (!toAll)
                client.WriteData(buffer);
            else
            {
                buffer[1] = (byte)client.Identifier;
                foreach (Client other in clients)
                    if (!client.Equals(other))
                        other.WriteData(buffer);
            }
        }

        private void sendData(IClient client, EConnectionResult result)
        {
            byte[] data = new Byte[1] {(byte)result};
            switch (result)
            {
                case EConnectionResult.Success: Utilities.Append(ref data,1,new Byte[1] {(byte)client.Identifier}); break;
            }
            sendData(client,EServerMessage.ConnectionResult,data);
        }

        #endregion
        #region Приём и обработка данных

        private void receiveData(IClient client, byte[] data)
        {
            while (data.Length != 0)
            {
                EClientMessage type = (EClientMessage)data[0];
                if (!client.Initialized && type != EClientMessage.InitialData)
                    return;
                Utilities.CutFromStart(ref data,1);
                int count = 0;
                switch (type)
                {
                    case EClientMessage.PositionChange: break; // TODO
                    case EClientMessage.InitialData: count = initializeClient(client,data); break;
                    case EClientMessage.Chat: count = chatMessage(client,data); break;
                }
                Utilities.CutFromStart(ref data,count);
            }
        }

        private int initializeClient(IClient client, byte[] data)
        {
            client.Name = Encoding.UTF8.GetString(data,1,data[0]);
            client.Initialized = true;
            log(client.Identity + " connected " + "(" + client.Address + ").");
            sendData(client,EServerMessage.PlayerConnected,data,true);
            return data[0]+1;
        }

        private int chatMessage(IClient client, byte[] data)
        {
            string message = Encoding.UTF8.GetString(data,1,data[0]);
            log(client.Identity + ": " + message);
            sendData(client,EServerMessage.PlayerWrote,data,true);
            return data[0]+1;
        }

        #endregion
    }
}