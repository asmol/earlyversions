using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Game.DataClasses;
using Game2D.Opengl;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Game2D.Game.Helpers;
using Utils.Commands;

namespace Game2D.Game.Concrete
{
    /// <summary>
    /// что умеет: отправка команд, получение команд, коннект, дисконнект
    /// </summary>
    class NetworkController
    {
        const int BUFFER_LENGTH = 512; //по сколько байт за раз считываем максимум

        List<Command> _waitingForConfirmation = new List<Command>();
        long _confirmedCount;
        long lastTimeEmptyCommandSent = 0;

        TcpClient _activeTcpClient=null;
        NetworkStream _stream=null;


        /// <summary>
        /// Дает актуальные(подтвержденные) команды, которые нужно исполнить на текущем тике 
        /// </summary>
        public List<Command> GetCommands()
        {
            List<Command> r = new List<Command>();
            if (_stream != null)
            {
                List<byte> data = new List<byte>( GetData());
                while (data.Count > 0)
                {
                    Command c = Command.CreateConcrete(ref data);
                    if (c is ComReceivedCommandsCount)
                        SetConfirmations((ComReceivedCommandsCount)c); //это служебная команда
                        //todo гарантировать, чтобы никто кроме серва не создавал
                    else
                        r.Add(c);
                }
            }
            return r;
        }

        public void SendCommands(List<Command> commands)
        {
            if (_stream != null && commands.Count > 0)
            {
                List<byte> data = new List<byte>();
                foreach (Command c in commands)
                {
                    _waitingForConfirmation.Add(c);
                    data.AddRange(c.GetByteData());
                }
                SendDataToServer(data.ToArray());
            }
        }

        public void Connect(DStateMain state)
        {
            ConnectionInfo info = state.connectionInfo;
            if (_activeTcpClient != null) throw new Exception("тут пока реализовано только первое подключение");
            _activeTcpClient = new TcpClient();
            try
            {
                _activeTcpClient.Connect(info.ip, info.port);
                _stream = _activeTcpClient.GetStream();
                SendCommands(new List<Command>{info.firstCommand});
                info.lastResult = ConnectionInfo.EState.connected;
                state.state = DStateMain.EState.inBattle;
                state.wish = DStateMain.EWish.none;
            }
            catch (Exception exc)
            {
                _activeTcpClient = null;
                info.lastResult = ConnectionInfo.EState.serverUnavailable;
            }
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод также добавляет команду в массив ожидающих подтверждение
        /// </summary>
        void SendDataToServer(byte[] data)
        {

            _stream.Write(data, 0, data.Length);
            _stream.Flush();
            
        }

        public byte[] GetData()
        {
            byte[] result = new byte[0];
            int count = 0;
            int curPos;
            do
            {
                curPos = result.Length;
                Array.Resize(ref result, curPos + BUFFER_LENGTH);
                count = _stream.DataAvailable? _stream.Read(result, curPos, BUFFER_LENGTH) : 0;
            }
            while (count == BUFFER_LENGTH);
            Array.Resize(ref result, curPos + count);
            return result;
        }

        void SetConfirmations(ComReceivedCommandsCount c)
        {
            for (int i = 0; i < c.count; i++)
            {
                _waitingForConfirmation[i].confirmed = true;
            }
            _waitingForConfirmation.RemoveRange(0, c.count);
        }

    }
}
