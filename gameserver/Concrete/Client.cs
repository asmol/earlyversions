using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Drawing;

using gameserver.Abstract;
using gameserver.Structures;

namespace gameserver.Concrete
{
    class Client : IClient
    {
        private const int bufferLength = 256,
            readTimeout = 180000;

        private readonly TcpClient connection;
        private readonly NetworkStream stream;
        private readonly int identifier;
        private bool readTimeoutReached;

        public TcpClient Connection {get {return connection;}}
        public NetworkStream Stream {get {return stream;}}
        public int Identifier {get {return identifier;}}
        public bool Initialized {get; set;}
        public bool ReadTimeoutReached {get {return readTimeoutReached;}}

        public string Name {get; set;}
        public string Identity {get {return (Initialized ? Name : String.Empty) + " (" + identifier + ")";}}
        public string Address
        {
            get
            {
                string address = connection.Client.LocalEndPoint.ToString();
                return address.Substring(0,address.IndexOf(':'));
            }
        }

        public PointF Destination {get; set;}
        public PointF Position {get; set;}
        public AngleF Angle {get; set;}

        public Client(TcpClient connection, int identifier)
        {
            this.connection = connection;
            stream = this.connection.GetStream();
            stream.ReadTimeout = readTimeout;
            this.identifier = identifier;
        }

        public byte[] ReadData()
        {
            byte[] result = new Byte[bufferLength];
            int count = 0;
            DateTime then = DateTime.Now;
            try {count = stream.Read(result,0,bufferLength);}
            catch (System.IO.IOException)
            {
                if ((DateTime.Now-then).TotalMilliseconds >= stream.ReadTimeout)
                    readTimeoutReached = true;
            }
            Array.Resize(ref result,count);
            return result;
        }

        public void WriteData(byte[] buffer)
        {
            stream.Write(buffer,0,buffer.Length); //todo нормальное отключение(переставать слать данные)
            stream.Flush();
        }

        public void Dispose()
        {
            if (connection != null)
                connection.Close();
        }
    }
}