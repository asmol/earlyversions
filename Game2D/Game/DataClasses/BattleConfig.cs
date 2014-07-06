using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2D.Game.DataClasses
{
    class BattleConfig
    {
        public enum ETank { first, second, third }
        public ETank tank = ETank.first;
    }
}
