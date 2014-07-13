using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

using gameserver.Structures;

namespace gameserver.Concrete.Configuration
{
    static class Settings
    {
        private static bool loadFailed = false;
        
        public const string SettingsFile = "settings.cfg",
            LogFile = "log.txt";
        public static readonly IPAddress Address = IPAddress.Any;

        public static readonly TimeSpan PositionUpdateTime = new TimeSpan(0,0,0,0,800);
        public static readonly AngleF RotationSpeed = new AngleF(5, false); // В секунду
        public static readonly int Speed = 5; // В секунду

        public static readonly int Port = 7777;
        public static readonly int MaxPlayers = 16;
        public static readonly string Password = "none";

        public static readonly int MainLoopInterval = 32;

        public static bool LoadFailed {get {return loadFailed;}}

        static Settings()
        {
            if (!File.Exists(SettingsFile))
            {
                write();
                return;
            }
            using (StreamReader reader = new StreamReader(new FileStream(SettingsFile,FileMode.Open,FileAccess.Read)))
            {
                string[] setting;
                char[] separator = new Char[] {' '};
                while (!reader.EndOfStream)
                {
                    setting = reader.ReadLine().Split(separator,StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        switch (setting[0])
                        {
                            case "port": Port = Int32.Parse(setting[1]); break;
                            case "max_players": MaxPlayers = Int32.Parse(setting[1]); break;
                            case "password": Password = setting[1]; break;
                        }
                    }
                    catch (Exception e)
                    {
                        if (e is IndexOutOfRangeException || e is ArgumentException || e is FormatException || e is OverflowException)
                        {
                            loadFailed = true;
                            continue;
                        }
                        throw;
                    }
                }
            }
        }

        private static void write()
        {
            using (StreamWriter writer = new StreamWriter(new FileStream(SettingsFile,FileMode.Create,FileAccess.Write)))
            {
                writer.WriteLine("port " + Port);
                writer.WriteLine("max_players " + MaxPlayers);
                writer.WriteLine("password " + Password);
            }
        }
    }
}