using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCreator
{
    public class RandomValueCreator
    {
        private static Random rnd = new Random();

        public static string GetRandomString()
        {
            StringBuilder str_build = new StringBuilder();

            char letter;
            int length = rnd.Next(10, 100);

            for (int i = 0; i < length; i++)
            {
                double flt = rnd.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            return str_build.ToString();
        }

        public static int GetRandomInt()
        {
            return rnd.Next(1, 1000);
        }

        public static int NextInt()
        {
            int firstBits = rnd.Next(0, 1 << 4) << 28;
            int lastBits = rnd.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        public static decimal GetRandomDecimal()
        {
            byte scale = (byte)rnd.Next(29);
            bool sign = rnd.Next(2) == 1;
            return new decimal(NextInt(),
                               NextInt(),
                               NextInt(),
                               sign,
                               scale);
        }

        public static float GetRandomFloat()
        {
            return Convert.ToSingle(rnd.Next(1, 1000));
        }

        public static double GetRandomDouble()
        {
            return rnd.NextDouble();
        }

        public static short GetRandomShort()
        {
            return (short)GetRandomInt();
        }

        public static bool GetRandomBool()
        {
            return Convert.ToBoolean(rnd.Next(0, 1));
        }

        public static byte GetRandomByte()
        {
            return (byte)rnd.Next(byte.MinValue, byte.MaxValue);
        }

        public static DateTime GetRandomDateTime()
        {
            var start = DateTime.MinValue;
            var range = (DateTime.Today - start).Days;
            return start.AddDays(rnd.Next(range)); // todo add random time
        }

        public static DateOnly GetRandomDateOnly()
        {
            return DateOnly.FromDateTime(GetRandomDateTime());
        }

        public static TimeOnly GetRandomTimeOnly()
        {
            return TimeOnly.FromDateTime(GetRandomDateTime());
        }
    }
}
