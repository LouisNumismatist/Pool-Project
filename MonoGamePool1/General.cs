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
        public static void WriteBall(Ball a)
        {
            Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", a.ID, a.Center, a.Radius, a.Velocity, a.Acceleration, a.Color, a.Collision, a.PrevBall);
        }
        /**public static bool InsideGameSpace(Vector2 Coord, int ScreenWidth, int ScreenHeight)
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
        }**/

        /*public static void EndGame()
        {

        }*/
        //x = 10 - y^3
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
        public static bool MouseWithinArea(Vector2 MousePosition, Vector2 TopLeft, Vector2 LowRight)
        {
            if (MousePosition.X >= TopLeft.X && MousePosition.X <= LowRight.X &&
                MousePosition.Y >= TopLeft.Y && MousePosition.Y <= LowRight.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
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
    }
}
