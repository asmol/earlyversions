using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Utils.Commands;

namespace Game2D.Game.DataClasses
{
    class ConnectionInfo
    {
        public enum EState { connected, disconnected, serverUnavailable, serverFull }

        public EState lastResult = EState.disconnected;
        public bool allowDisconnect = false;
        public IPAddress ip = new IPAddress(new byte[]{127,0,0,1});
        public int port=7777;
        public int waitingTime = 0;
        public Command firstCommand = new ComConnect("The First Byte Reached Enemy Territory");
    }
}
