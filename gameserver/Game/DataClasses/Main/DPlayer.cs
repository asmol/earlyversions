using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2D.Game.DataClasses
{
    class DPlayer
    {
        public int id;
        public string nickname;
        public DTank tank;

        public DPlayer(int id, string nickname, DTank tank)
        {
            this.id = id;
            this.nickname = nickname;
            this.tank = tank;
        }
    }
}
