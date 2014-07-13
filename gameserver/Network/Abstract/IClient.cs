using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Drawing;

using gameserver.Structures;

namespace gameserver.Abstract
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

        PointF Destination {get; set;}
        PointF Position {get; set;}
        AngleF Angle {get; set;}

        byte[] ReadData();
        void WriteData(byte[] buffer);
    }
}