using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2D.Game.DataClasses
{
    class DTankBody
    {
        public Vector2 pos;
        public double speed = 0.1; // сколько проедет за кадр
        public Point2? aimPoint = null; //куда едем, известно только для подконтрольных юнитов
        Point2 size = new Point2(10, 5);
        ESprite sprite;
        public DTankBody(Vector2 startPos, ESprite sprite)
        {
            this.pos = startPos;
            this.sprite = sprite;
        }

        public void Draw(Frame frame)
        {
            frame.Add(new Sprite(sprite, size.x, size.y, pos));
        }
    }
}
