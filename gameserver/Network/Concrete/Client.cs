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
            byte[] result = new byte[0];
            int count = 0;
            int curPos;
            do
            {
                curPos = result.Length;
                Array.Resize(ref result, curPos + bufferLength);
                //todo по моему предположению эта команда выполняется мгновенно, т.е. считали все что есть и пошли дальше.
                //поэтому я пока сделал все в одном потоке, тк это проще всего кодить
                //вопрос такой - есть ли гарантия, что команды будут приходить целиком, т.е. неразрывно?
                //читал, что tcp может передавать по частям данные большой длины
                count = stream.DataAvailable ? stream.Read(result, curPos, bufferLength) : 0; //todo тут могут быть какие-то эксепшны?
            }
            while (count == bufferLength);
            Array.Resize(ref result, curPos + count);
            return result;
        }
        /* тут круче т к ловятся ексепшны, но минус, что данные ограничены длиной буфера
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
        }*/

        public void WriteData(byte[] buffer)
        {
            stream.Write(buffer,0,buffer.Length); //todo нормальное отключение(переставать слать данные, если пользователь умер)
            stream.Flush();
        }

        public void Dispose()
        {
            if (connection != null)
                connection.Close();
        }
    }
}