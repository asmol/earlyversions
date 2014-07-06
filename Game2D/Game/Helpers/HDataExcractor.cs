using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Game.DataClasses;

namespace Game2D.Game.Helpers
{
    class HDataExcractor
    {
        public static Dictionary<int,DMove> GetDMoveDictionary(List<Player> p)
        {
            Dictionary<int,DMove> r = new Dictionary<int,DMove>();
            foreach (var x in p)
            {
                r.Add(x.tank.ID, x.tank.move);
            }
            return r;
        }
    }
}
