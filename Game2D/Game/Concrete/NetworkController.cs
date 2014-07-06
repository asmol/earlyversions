using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Game.DataClasses;
using Game2D.Game.Abstract;
using Game2D.Opengl;
using Game2D.Game.DataClasses.Commands;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Game2D.Game.Helpers;

namespace Game2D.Game.Concrete
{
    class NetworkController
    {
        const int BUFFER_LENGTH = 1024; //todo криво как то с буфером
        const int SEND_AGAIN_TIME = 500;

        Stopwatch _stopwatch = new Stopwatch();
        List<Command> _waitingForConfirmation = new List<Command>();

        long lastTimeEmptyCommandSent = 0;

        TcpClient _activeTcpClient=null;
        NetworkStream _stream=null;
        IPAddress _activeIP=null; int _activePort=0;
        public NetworkController()
        {
            _stopwatch.Restart();
        }

        /// <summary>
        /// Дает актуальные(подтвержденные) команды, которые нужно исполнить на текущем тике 
        /// </summary>
        public List<Command> GetCommands(ConnectionInfo info)
        {
            List<Command> r = new List<Command>();

            if (_stream != null && _stream.DataAvailable)
            {
                List<byte> data = new List<byte>( GetData());
                while (data.Count > 0)
                {
                    byte num = HEncoder.GetByte(ref data);
                    switch(num){
                        case 0: //результат коннекта
                            byte res = HEncoder.GetByte(ref data);
                            if (res == 0)
                            {
                                byte id = HEncoder.GetByte(ref data); //todo криво 
                                ComConnect ourConnect = FindLastInWaitingList<ComConnect>();
                                if (ourConnect != null)
                                {
                                    r.Add(new ComConnectionResult(id, ourConnect.nickname));
                                    _waitingForConfirmation.Remove(ourConnect);
                                }
                            }
                            else if (res == 1)
                            {
                                info.lastResult = ConnectionInfo.EState.serverFull;
                            }
                            break;
                        case 1: //новый игрок законнектился
                            r.Add(new ComAddPlayer(ref data));
                            break;
                        case 2:
                            r.Add(new ComRefreshTankPos(ref data));
                            break;
                        case 3:
                            r.Add(new ComRemovePlayer(ref data));
                            break;
                        case 4:
                            r.Add(new ComEndPointOfMoving(ref data));
                            break;
                    }
                }
            }
            return new List<Command>();
        }

        public void SendCommands(List<Command> commands, ConnectionInfo info)
        {
            if (info.allowConnect)
            {
                //тут варианты: мы уже присоединены куда надо,
                //никуда не присоединены или присоединены куда не надо
                //или отправили запрос на подключение и ждем
                //todo сделать все варианты
                if (_activeTcpClient == null)
                {
                    _activeTcpClient = new TcpClient();
                    try
                    {
                        _activeTcpClient.Connect(info.ip, info.port);
                        info.lastResult = ConnectionInfo.EState.connected;
                        _stream = _activeTcpClient.GetStream();
                        SendCommandToServer(new ComConnect(info.nickForConnection));
                    }
                    catch
                    {
                        info.lastResult = ConnectionInfo.EState.serverUnavailable;
                    }
                }
            }
            if (_activeTcpClient != null && _activeTcpClient.Connected)
            {
                foreach (Command c in commands)
                {
                    //todo тут надо еще раз проверить
                    if (c is ComEndPointOfMoving) _waitingForConfirmation.RemoveAll((a) => a is ComEndPointOfMoving);
                    SendCommandToServer(c);
                }
            }

            for (int i = 0; i < _waitingForConfirmation.Count; i++)
            {
                Command c = _waitingForConfirmation[i];
                if (Time() - c.timeWhenSentToServer >= SEND_AGAIN_TIME)
                {
                    _waitingForConfirmation.RemoveAt(i--); //удаляем, т к все равно следующим методом туда снова добавится
                    SendCommandToServer(c);
                }
            }


            {
                ComEmpty c = FindLastInWaitingList<ComEmpty>();
                if ((c == null) || Time() - c.timeWhenSentToServer > 1000)
                {
                    _waitingForConfirmation.Remove(c);
                    SendCommandToServer(c);
                }
            }

        }

        /// <summary>
        /// Метод также добавляет команду в массив ожидающих подтверждение и ставит ей время отправки
        /// </summary>
        void SendCommandToServer(Command command)
        {
            int num=-1;
            if (command is ComEndPointOfMoving) num = 0;
            else if (command is ComConnect) num = 1;
            else if (command is ComEmpty) num = 2;

            if (num != -1)
            {
                _waitingForConfirmation.Add(command);
                List<byte> packet = new List<byte> { (byte)num};
                packet.AddRange(command.ByteData());

                _stream.Write(packet.ToArray(), 0, packet.Count);
                command.timeWhenSentToServer = Time();
                _stream.Flush();
            }
        }

        public byte[] GetData()
        {
            byte[] result = new Byte[BUFFER_LENGTH];
            int count = _stream.Read(result, 0, BUFFER_LENGTH);
            Array.Resize(ref result, count);
            return result;
        }

        long Time()
        {
            return _stopwatch.ElapsedMilliseconds;
        }

        T FindLastInWaitingList<T>() where T : Command
        {
            for (int i = _waitingForConfirmation.Count - 1; i >= 0; i--)
                if (_waitingForConfirmation[i] is T)
                    return (T)_waitingForConfirmation[i] ;
            return null;
        }

    }
}
