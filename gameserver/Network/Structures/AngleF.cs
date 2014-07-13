using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace gameserver.Structures
{
    /// <summary>
    /// Представляет вещественный угол в радианах и градусах.
    /// </summary>
    struct AngleF
    {
        private double radians;

        /// <summary>
        /// Возвращает угол в радианах от 0 до 2π.
        /// </summary>
        public double Radians
        {
            get {return radians;}
            set
            {
                double remainder = value % (2 * Math.PI);
                radians = (value < 0 && remainder != 0 ? 2 * Math.PI : 0) + remainder;
            }
        }

        /// <summary>
        /// Возвращает угол в градусах от 0 до 360.
        /// </summary>
        public double Degrees
        {
            get {return (radians / Math.PI) * 180;}
            set {Radians = (value / 180) * Math.PI;}
        }

        /// <summary>
        /// Создаёт новый угол в радианах или градусах.
        /// </summary>
        /// <param name="value">Значение угла.</param>
        /// <param name="inRadians">В радианах ли значение заданного угла.</param>
        public AngleF(double value, bool inRadians = true)
        {
            this.radians = 0;
            if (inRadians)
                Radians = value;
            else
                Degrees = value;
        }

        /// <summary>
        /// Меняет знак угла (прибавляет π/180°).
        /// </summary>
        /// <param name="that">Значение угла.</param>
        /// <returns>Угол с новым знаком.</returns>
        public static AngleF operator -(AngleF that)
        {
            return new AngleF(-that.Radians);
        }

        /// <summary>
        /// Складывает один угол с другим.
        /// </summary>
        /// <param name="a">Угол A.</param>
        /// <param name="b">Угол B.</param>
        /// <returns>Результат сложения углов.</returns>
        public static AngleF operator +(AngleF a, AngleF b)
        {
            return new AngleF(a.Radians + b.Radians);
        }

        /// <summary>
        /// Вычитает один угол из другого.
        /// </summary>
        /// <param name="a">Угол A.</param>
        /// <param name="b">Угол B.</param>
        /// <returns>Результат вычитания углов.</returns>
        public static AngleF operator -(AngleF a, AngleF b)
        {
            return new AngleF(a.Radians - b.Radians);
        }

        public static bool operator ==(AngleF a, AngleF b)
        {
            return a.Radians == b.Radians;
        }

        public static bool operator !=(AngleF a, AngleF b)
        {
            return a.Radians != b.Radians;
        }

        public static bool operator >(AngleF a, AngleF b)
        {
            return a.Radians > b.Radians;
        }

        public static bool operator <(AngleF a, AngleF b)
        {
            return a.Radians < b.Radians;
        }

        public static bool operator >=(AngleF a, AngleF b)
        {
            return a.Radians >= b.Radians;
        }

        public static bool operator <=(AngleF a, AngleF b)
        {
            return a.Radians <= b.Radians;
        }

        /// <summary>
        /// Определяет, в какую сторону поворачивать угол будет быстрее, чтобы он соответствовал
        /// заданному: по часовой или против часовой стрелки.
        /// </summary>
        /// <param name="that">Заданный угол.</param>
        /// <returns>По часовой ли стрелке поворачивать угол.</returns>
        public bool IsClockwiseRotationFaster(AngleF that)
        {
            return Math.Abs((this-that).Degrees) < 180;
        }

        /// <summary>
        /// Возвращает угол между двумя точками.
        /// </summary>
        /// <param name="a">Точка A.</param>
        /// <param name="b">Точка B.</param>
        /// <returns>Угол между точками A и B в радианах.</returns>
        public static AngleF AngleBetweenPoints(PointF a, PointF b)
        {
            return new AngleF(Math.Atan2(b.Y-a.Y,b.X-a.X));
        }

        /// <summary>
        /// Возвращает значение угла в градусах.
        /// </summary>
        /// <returns>Угол в градусах.</returns>
        public override string ToString()
        {
            return Degrees.ToString();
        }
    }
}