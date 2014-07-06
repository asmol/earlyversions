using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Game.Helpers;

namespace Game2D.Game.DataClasses.Commands
{
    //Вообще есть 3 типа команд:
    //1 - создаются только для отсылки на сервер ( нужен конструктор, прин. переменные)
    //2 - присылаются сервером (создаются по массиву байт)
    //3 - могут создаваться клиентом, и присылаться сервером(нужно оба конструктора)


    /// <summary>
    /// ядро для всех
    /// </summary>
    abstract class Command
    {
        static long CurID=0;
        public abstract List<byte> ByteData();
        /// <summary>
        /// номер команды
        /// </summary>
        public long id;
        /// <summary>
        /// Миллисекунды. Для тех команд, которые созданы в клиенте. 
        /// </summary>
        public long timeWhenSentToServer;

        public Command()
        {
            this.id = CurID++;
        }
    }

    //организационные
    class ComEmpty : Command
    {
        public override List<byte> ByteData()
        {
            return new List<byte>();
        } 
    }

    class ComRemovePlayer : Command
    {
        public byte playerID;

        public override List<byte> ByteData()
        {
            return HEncoder.Encode(playerID);
        } 
        public ComRemovePlayer(byte playerID)
        {
            this.playerID = playerID;
        }
        public ComRemovePlayer(ref List<byte> byteData)
        {
            this.playerID = HEncoder.GetByte(ref byteData);
        }
    }
    class ComAddPlayer : Command
    {
        public byte playerID;
        public string nickname;
        public override List<byte> ByteData()
        {
            return HEncoder.Encode(playerID, nickname);
        } 
        public ComAddPlayer(byte playerID,
            string nickname):base()
        {
            this.playerID = playerID;
            this.nickname = nickname;
        }
        public ComAddPlayer(ref List<byte> byteData)
        {
            this.playerID = HEncoder.GetByte(ref byteData);
            this.nickname = HEncoder.GetString(ref byteData);
        }
    }

    class ComConnect : Command
    {
        public string nickname;
        public override List<byte> ByteData()
        {
            return HEncoder.Encode(nickname);
        } 
        public ComConnect(string nickname)
            : base()
        {
            this.nickname = nickname;
        }
    }

    class ComConnectionResult : Command
    {
       public byte playerID;
        public string nickname;
        public override List<byte> ByteData()
        {
            return HEncoder.Encode(playerID, nickname);
        } 
        public ComConnectionResult(byte playerID,
            string nickname):base()
        {
            this.playerID = playerID;
            this.nickname = nickname;
        }
        public ComConnectionResult(ref List<byte> byteData)
        {
            this.playerID = HEncoder.GetByte(ref byteData);
            this.nickname = HEncoder.GetString(ref byteData);
        }
    }
    //движение
    //отправляет на сервер угол в радианах и при приеме с сервера преобразует в градусы
    class ComRefreshTankPos : Command
    {
        public byte playerID;
        public Vector2 pos;

        public override List<byte> ByteData()
        {
            return HEncoder.Encode(playerID, pos.x, pos.y, pos.angleDeg /180.0 * Math.PI ); 
        } 
        public ComRefreshTankPos(Vector2 pos)
            : base()
        {
            this.pos = pos;
        }
        public ComRefreshTankPos(ref List<byte> byteData)
        {
            playerID = HEncoder.GetByte(ref byteData);
            this.pos = new Vector2( HEncoder.GetDouble(ref byteData)
                ,HEncoder.GetDouble(ref byteData),HEncoder.GetDouble(ref byteData)/Math.PI * 180.0);
        }
        
    }
    class ComEndPointOfMoving : Command
    {
        public Point2 endPoint;
        public override List<byte> ByteData()
        {
            return HEncoder.Encode(endPoint.x, endPoint.y);
        } 
        public ComEndPointOfMoving(Point2 endPoint)
            : base()
        {
            this.endPoint = endPoint;
        }
        public ComEndPointOfMoving(ref List<byte> byteData)
        {
            this.endPoint = new Point2(HEncoder.GetDouble(ref byteData)
                ,HEncoder.GetDouble(ref byteData));
        }
    }
}
