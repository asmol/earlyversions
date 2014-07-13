using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Game.Helpers;

namespace Utils.Commands
{
    

    public class ComAddPlayer : Command
    {
        public int playerID;
        public string nickname;
        public override List<byte> GetByteData()
        {
            return HEncoder.Encode(base.type, playerID, nickname);
        }
        protected override void SetFields(ref List<byte> data)
        {
            this.playerID = HEncoder.GetInt(ref data);
            this.nickname = HEncoder.GetString(ref data);
        }
        public ComAddPlayer(byte playerID,
            string nickname)
        {
            this.playerID = playerID;
            this.nickname = nickname;
        }
        public ComAddPlayer(ref List<byte> byteData) { }
    }

    public class ComRemovePlayer : Command
    {
        public int playerID;

        public override List<byte> GetByteData()
        {
            return HEncoder.Encode(base.type, playerID);
        }
        protected override void SetFields(ref List<byte> data)
        {
            playerID = HEncoder.GetInt(ref data);
        }
        public ComRemovePlayer(byte playerID)
        {
            this.playerID = playerID;
        }
        public ComRemovePlayer() { }
    }
}
