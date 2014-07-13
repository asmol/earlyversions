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
        /// <summary>
        /// также добавляется время в мс, прошедшее со времени первого лога.
        /// Путь можно не указывать
        /// </summary>
        public static void Log(string text, string path = "d:/logfile.txt")
        {
            File.AppendAllText(path, timer.ElapsedMilliseconds.ToString() + ": " + text + "\r\n");
        }
    }
}
