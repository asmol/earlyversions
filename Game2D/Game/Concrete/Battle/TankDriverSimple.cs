using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Game.DataClasses;
using Game2D.Opengl;
using Game2D.Game.Helpers;
using Utils.Commands;

namespace Game2D.Game.Concrete
{
    class TankDriverSimple
    {
        readonly Point2 POINTER_SIZE = new Point2(4, 4);

        Point2? pointerOnMap = null;
        /*
        public List<Command> Process(ref Frame frame, IGetKeyboardState keyboard, List<Command> serverCom
            , DMove controlled, Dictionary<int,DMove> allTanks)
        {
            
            List<Command> createdCommands = new List<Command>();
            if (keyboard.MouseRightClick)
            {
                createdCommands.Add(new ComEndPointOfMoving(keyboard.MousePosMap));
            }

            foreach (Command c in serverCom)
            {
                if (c is ComRefreshTankPos)
                {
                    ComRefreshTankPos com = ((ComRefreshTankPos)c);
                    if(allTanks.ContainsKey(com.playerID))
                        allTanks[com.playerID].pos = com.pos;
                    HLog.Log("command in TankDriver"+" "+com.pos.x.ToString() + " "+ com.pos.y.ToString());
                }
                else if (c is ComEndPointOfMoving)
                {
                    ComEndPointOfMoving com = ((ComEndPointOfMoving)c);
                    pointerOnMap = controlled.aimPoint = com.endPoint;
                }
            }

            if (pointerOnMap != null)
            {
                frame.Add(new Sprite(ESprite.shell0, POINTER_SIZE.x, POINTER_SIZE.y,
                    new Vector2(((Point2)pointerOnMap).x, ((Point2)pointerOnMap).y, 0)));
            }

            foreach (DMove tank in allTanks.Values)
            {
                Vector2 speedV = new Vector2(0, 0, tank.pos.angleDeg);
                Point2 speedAdd = new Point2(speedV.vx * tank.speed, speedV.vy * tank.speed);
                tank.pos = new Vector2(tank.pos.x + speedAdd.x,
                    tank.pos.y + speedAdd.y, tank.pos.angleDeg);
            }

            foreach (DMove tank in allTanks.Values)
            {
                tank.Draw(frame);
            }

            return createdCommands;
        }*/
    }
}
