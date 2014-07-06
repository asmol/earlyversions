using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2D.Game.DataClasses
{
    class Tank
    {
        public bool controlled; //управляет ли им наш клиент 
        public int ID;
        public DMove move;
    }
}
