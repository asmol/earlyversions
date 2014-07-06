using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace gameserver
{
    public enum EClientMessage
    {
        PositionChange,
        InitialData,
        HasConnection,
        Chat
    }

    interface IClient : IDisposable
    {
        TcpClient Connection {get;}
        NetworkStream Stream {get;}
        int Identifier {get;}
        bool Initialized {get; set;}
        bool ReadTimeoutReached {get;}

        string Name {get; set;}
        string Identity {get;}
        string Address {get;}

        byte[] ReadData();
        void WriteData(byte[] buffer);
    }
}