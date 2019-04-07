using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGamePool1
{
    public static class General
    {
        static void FlipX(ref Vector2 a)
        {
            Vector2 Flip = new Vector2(-1, 1);
            a = Vector2.Multiply(a, Flip);
        }

        static void FlipY(ref Vector2 a)
        {
            Vector2 Flip = new Vector2(1, -1);
            a = Vector2.Multiply(a, Flip);
        }

        public static bool SameSign(double a, double b)
        {
            if ((a > 0) && (b > 0) || ((a < 0) && (b < 0)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool NoBallsMoving(List<Ball> BallsList)
        {
            bool status = true;
            foreach (Ball a in BallsList)
            {
                if (a.Velocity != Vector2.Zero)
                {
                    status = false;
                    break;
                }
            }
            return status;
        }

        public static float ToRotation(this Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        public static Vector2 ToVector2(this float radian)
        {
            return new Vector2((float)Math.Cos(radian), (float)Math.Sin(radian));
        }

        public static bool InAlpha(string letter)
        {
            List<char> Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();

            return Alphabet.Contains(Convert.ToChar(letter));
        }

        public static decimal ToExponential(string item)
        {
            Console.WriteLine(item);
            string[] mant = item.Split("E".ToCharArray());
            Console.WriteLine(mant[0]);
            decimal number = Convert.ToDecimal(mant[0]);
            number *= (decimal)Math.Pow(10, Convert.ToDouble(mant[1]));
            return number;
        }

        public static float RealDist(int pixels)
        {
            return pixels * 0.25f;
        }
    }
}
