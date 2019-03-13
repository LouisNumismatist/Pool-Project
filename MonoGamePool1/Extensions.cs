using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework; //pls die lol

namespace MonoGamePool1
{
    public static class Extensions
    {
        public static float ToRotation(this Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        public static Vector2 ToVector2(this float radian)
        {
            return new Vector2((float)Math.Cos(radian), (float)Math.Sin(radian));
        }

        public static Vector2 Normalized(this Vector2 vector2)
        {
            return vector2 / vector2.Length();
        }
    }
}
