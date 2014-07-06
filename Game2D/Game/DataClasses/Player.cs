using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2D.Game.DataClasses
{
    class Player
    {
        public int id;
        public string nickname;
        public Tank tank;
        public bool controlled;

        public Player(int id, string nickname, bool controlled, Tank tank)
        {
            this.id = id;
            this.nickname = nickname;
            this.controlled = controlled;
            this.tank = tank;
        }
    }
}
