using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils.Commands;

namespace gameserver.Game
{
    class GameServer
    {
        public List<Command> Process(List<Command> commands)
        {
            List<Command> r = new List<Command>();
            if (commands.Count > 0)
            {
                r.Add(new ComConnectionResult(150, "все преграды преодолены и сообщение доставлено"));
                r[r.Count - 1].clientID = 0;
            }
            return r;
        }
    }
}
