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
        public bool controlled;

        public DPlayer(int id, string nickname, bool controlled, DTank tank)
        {
            this.id = id;
            this.nickname = nickname;
            this.controlled = controlled;
            this.tank = tank;
        }
    }
}
