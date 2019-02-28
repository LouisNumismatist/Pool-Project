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

        /*public static bool InsideGameSpace(Vector2 Coord, int ScreenWidth, int ScreenHeight)
        {
            return MouseWithinArea(Coord, Vector2.Zero, new Vector2(ScreenWidth, ScreenHeight));
            if (Coord.X >= 0 && Coord.X <= ScreenWidth && Coord.Y >= 0 && Coord.Y <= ScreenHeight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/

        /*public static void EndGame()
        {

        }
        //x = 10 - y^3*/

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



        public static bool InAlpha(string letter)
        {
            List<char> Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
            if (Alphabet.Contains(Convert.ToChar(letter)))
            {
                return true;
            }
            else
            {
                return false;
            }
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

        public static void IncPlayer(ref Player player)
        {
            player.Shots += 1;
        }

        public static void NullPlayer(ref Player player)
        {
            player.Shots = 0;
        }

        public static float RealDist(int pixels)
        {
            return pixels * 0.25f;
        }
    }
}
