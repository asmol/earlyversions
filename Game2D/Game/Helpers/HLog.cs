using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Game2D.Game.Helpers
{
    class HLog
    {
        static Stopwatch timer = new Stopwatch();
        static HLog()
        {
            timer.Start();
        }
        public static void Log(string text)
        {
            File.AppendAllText("d:/logfile.txt", timer.ElapsedMilliseconds.ToString() + ": " + text + "\r\n");
        }
    }
}
