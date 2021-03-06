﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2D
{
    /// <summary>
    /// точка или вектор, есть нормализация и расстояние
    /// </summary>
    struct Point2
    {
        public double x, y;
        public Point2(double x, double y)
        {
            this.x = x; this.y = y;
        }
        public void Normalize()
        {
            double m = Math.Sqrt(x * x + y * y);
            x /= m;
            y /= m;
        }
        public double Length()
        {
            return Math.Sqrt(x * x + y * y);
        }
        public double DistTo(Point2 to)
        {
            return Math.Sqrt((x - to.x) * (x - to.x) + (y - to.y) * (y - to.y));
        }
        /// <summary>
        /// Угол в градусах
        /// </summary>
        public double angleTo(Point2 to)
        {
            double angle = Math.Acos((x * to.x + y * to.y) / (Length() * to.Length())) / Math.PI * 180;
            if (PointRelativelyVector(to) > 0)
                angle = 360 - angle;

            return angle;
        }

        //внутренняя вспомогательная
        //1- точка слева от вектора, -1 - точка справа от вектора, 0 - на прямой вектора
        int PointRelativelyVector(Point2 to)
        {
            double s = x * (to.y - y) - y * (to.x - x);
            if (s > 0) return 1;
            else if (s < 0) return -1;
            else return 0;
        }

    }
}
