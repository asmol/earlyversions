using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Game2D.Game.DataClasses
{
    class ConnectionInfo
    {
        public enum EState { connected, disconnected, serverUnavailable, serverFull }

        public EState lastResult = EState.disconnected;
        public bool allowConnect = false;
        public bool allowDisconnect = false;
        public IPAddress ip = new IPAddress(new byte[]{94,141,49,24});
        public int port=7777;
        public int waitingTime = 0;
        public string nickForConnection = "The First Byte Reached Enemy Territory";
    }
}
