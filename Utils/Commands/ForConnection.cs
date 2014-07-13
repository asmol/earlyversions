using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Game.Helpers;

namespace Utils.Commands
{
    public class ComEmpty : Command
    {
        public override List<byte> GetByteData()
        {
            return HEncoder.Encode(base.type);
        }
        protected override void SetFields(ref List<byte> data) { }
        public ComEmpty(int a){}
    }

    public class ComConnect : Command
    {
        public string nickname;
        public override List<byte> GetByteData()
        {
            return HEncoder.Encode(base.type, nickname);
        }
        protected override void SetFields(ref List<byte> data)
        {
            nickname = HEncoder.GetString(ref data);
        }
        public ComConnect(string nickname)
        {
            this.nickname = nickname;
        }
        public ComConnect() { }
    }
    
    public class ComConnectionResult : Command
    {
        public int playerID;
        public string nickname;
        public override List<byte> GetByteData()
        {
            return HEncoder.Encode(base.type, playerID, nickname);
        }
        protected override void SetFields(ref List<byte> data)
        {
            playerID = HEncoder.GetInt(ref data);
            nickname = HEncoder.GetString(ref data);
        }
        public ComConnectionResult(int playerID,
            string nickname)
        {
            this.playerID = playerID;
            this.nickname = nickname;
        }
        public ComConnectionResult() { }
    }
}
