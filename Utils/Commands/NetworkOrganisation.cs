using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Game.Helpers;

namespace Utils.Commands
{
    //тут команды, которые нужны для организации работы всей системы. К игре не относятся.

    public class ComReceivedCommandsCount : Command
    {
        public int count;
        public override List<byte> GetByteData()
        {
            return HEncoder.Encode(base.type, count);
        }
        protected override void SetFields(ref List<byte> data)
        {
            count = HEncoder.GetInt(ref data);
        }
        public ComReceivedCommandsCount(int clientID, int count)
        {
            this.count = count;
            base.clientID = clientID;
        }
        public ComReceivedCommandsCount() { }
    }
}
