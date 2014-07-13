using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2D.Game.DataClasses
{
    class DTankBody
    {
        public Vector2 pos;
        public double speed = 5; // в секунду
        public Point2? aimPoint = null; 
        Point2 size = new Point2(10, 5);
        public DTankBody(Vector2 startPos)
        {
            this.pos = startPos;
        }
    }
}
