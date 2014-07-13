using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using gameserver.Structures;
using System.IO;
using System.Diagnostics;

namespace gameserver.Concrete.Miscellaneous
{
    static class Utilities
    {
        public static void Invoke(Control target, Action action)
        {
            if (target.InvokeRequired)
                target.Invoke((MethodInvoker)delegate() {Invoke(target,action);});
            else
                action();
        }

        public static void CutFromStart<T>(ref T[] array, int length)
        {
            if (length > array.Length)
                return;
            int resultLength = array.Length-length;
            T[] result = new T[resultLength];
            Array.Copy(array,length,result,0,resultLength);
            array = result;
        }

        public static void Append<T>(ref T[] target, int targetIndex, T[] source)
        {
            Array.Resize(ref target,targetIndex+source.Length);
            Array.Copy(source,0,target,targetIndex,source.Length);
        }

        public static double DistanceBetweenPoints(PointF a, PointF b)
        {
            return Math.Sqrt(Math.Pow(b.X-a.X,2)+Math.Pow(b.Y-a.Y,2));
        }

        static Stopwatch timer = new Stopwatch();
        static Utilities()
        {
            timer.Start();
        }
        public static void Log(string text)
        {
            File.AppendAllText("d:/logfile.txt", timer.ElapsedMilliseconds.ToString() + ": " + text + "\r\n");
        }
    }
}