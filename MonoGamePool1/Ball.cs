using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePool1
{
    public struct Ball
    {
        public int ID;
        public Vector2 Center;
        public float Radius;
        public Vector2 Velocity;
        public Vector2 Acceleration;
        public Texture2D Color;
        public bool Collision;
        public int PrevBall;

        public Ball(int id, Vector2 center, float radius, Vector2 velocity, Vector2 acceleration, Texture2D color, bool collision, int prevBall)
        {
            ID = id;
            Center = center;
            Radius = radius;
            Velocity = velocity;
            Acceleration = acceleration;
            Color = color;
            Collision = collision;
            PrevBall = prevBall;
        }
    }

    public struct Pocket
    {
        public int ID;
        public Vector2 Center;
        public float Radius;

        public Pocket(int id, Vector2 center, float radius)
        {
            ID = id;
            Center = center;
            Radius = radius;
        }
    }

    public struct DiagonalLine
    {
        public int ID;
        public int Thickness;
        public Vector2 Start;
        public Vector2 End;
        public Texture2D Color;
        public bool Dotted;

        public DiagonalLine(int id, int thickness, Vector2 start, Vector2 end, Texture2D color, bool dotted)
        {
            ID = id;
            Thickness = thickness;
            Start = start;
            End = end;
            Color = color;
            Dotted = dotted;

        }
    }
}