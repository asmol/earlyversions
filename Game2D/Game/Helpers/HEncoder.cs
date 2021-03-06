﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Game2D.Game.Helpers
{
    class HEncoder
    {
        public enum Type{Byte, Int, Double, String}

        public static List<byte> Encode(params object[] vars)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binary = new BinaryWriter(stream, UnicodeEncoding.Unicode);
            foreach (object v in vars)
            {
                if (v is int) { binary.Write((int)v); }
                else if (v is double) binary.Write((double)v);
                else if (v is byte) binary.Write((byte)v);
                else if (v is string)
                {
                    string s = (string)v;
                    byte[] array = Encoding.UTF8.GetBytes(s);
                    binary.Write((byte)array.Length);
                    binary.Write(array);
                }
                else throw new Exception("Encoder: тип данных не поддерживается");
            }
            return new List<byte>( stream.ToArray());
        }

        /// <summary>
        /// делает 2 вещи - возвращает переменную и удаляет считанное из message. Возможен вылет с exception
        /// </summary>
        public static int GetInt(ref List<byte> message)
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(message.ToArray()), UnicodeEncoding.Unicode);
            message.RemoveRange(0, 4);
            return reader.ReadInt32();
        }
        /// <summary>
        /// делает 2 вещи - возвращает переменную и удаляет считанное из message. Возможен вылет с exception
        /// </summary>
        public static double GetDouble(ref List<byte> message)
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(message.ToArray()), UnicodeEncoding.Unicode);
            message.RemoveRange(0, 8);
            return reader.ReadDouble();
        }
        /// <summary>
        /// делает 2 вещи - возвращает переменную и удаляет считанное из message. Возможен вылет с exception
        /// </summary>
        public static string GetString(ref List<byte> message)
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(message.ToArray()), UnicodeEncoding.UTF8);
           
            byte n = reader.ReadByte();
            string str = Encoding.UTF8.GetString(message.ToArray(), 1, n);
            message.RemoveRange(0, 1 + n);
            return str;
        }
        /// <summary>
        /// делает 2 вещи - возвращает переменную и удаляет считанное из message. Возможен вылет с exception
        /// </summary>
        public static byte GetByte(ref List<byte> message)
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(message.ToArray()), UnicodeEncoding.Unicode);
            message.RemoveRange(0, 1);
            return reader.ReadByte();
        }
    }
}
