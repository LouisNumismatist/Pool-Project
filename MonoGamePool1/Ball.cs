﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePool1
{
    /// <summary>
    /// Circle class is base class to Ball and Pocket derived classes
    /// </summary>
    public class Circle
    {
        public int ID;
        public Vector2 Center;
        public float Radius;
        public Color Colour;
        public Texture2D Texture = Game1.BlankCircle;

        public void Draw(SpriteBatch spriteBatch)
        {
            float radius = Radius;
            int diameter = (int)(2 * radius);
            int x = (int)(Center.X - radius);
            int y = (int)(Center.Y - radius);
            spriteBatch.Draw(Texture, new Rectangle(x, y, diameter, diameter), Colour);
            if (Colour == Color.Black && !Debug.showBallNumbers && diameter == Game1.BallDiam)
            {
                spriteBatch.DrawString(Game1.TextBoxFont, "8", new Vector2(Center.X - Radius + 6, Center.Y - radius + 2), Color.White);
            }
        }
    }
    /// <summary>
    /// Main ball class for all types of balls on table
    /// </summary>
    public class Ball : Circle
    {
        public Vector2 Velocity;
        public Vector2 Acceleration;
        public bool Collision;
        public int PrevBall;
        public float Mass;

        public Ball(int id, Vector2 center, float radius, Vector2 velocity, Vector2 acceleration, Color colour, bool collision, int prevBall)
        {
            ID = id;
            Center = center;
            Radius = radius;
            Velocity = velocity;
            Acceleration = acceleration;
            Colour = colour;
            Collision = collision;
            PrevBall = prevBall;
            if (id == 15)
            {
                Mass = 0.16f;
            }
            else
            {
                Mass = 0.17f;
            }
        }

        public void Update()
        {
            float stop = 0.05f;
            bool flag = false;
            if ((Velocity.X < stop && Velocity.X > 0) || (Velocity.X > -stop && Velocity.X < 0))
            {
                flag = true;
                Velocity.X = 0;
            }
            if ((Velocity.Y < stop && Velocity.Y > 0) || (Velocity.Y > -stop && Velocity.Y < 0))
            {
                flag = true;
                Velocity.Y = 0;
            }
            if (!flag)
            {
                Center += Velocity;
            }
            Velocity *= 1 - Physics.coefficient_of_rolling_resistance_cloth;
        }

        public void Write()
        {
            Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", ID, Center, Radius, Velocity, Acceleration, Collision, PrevBall);
        }
    }
    /// <summary>
    /// Pockets used for outer and inner circles, as well as any drawing of pockets and other circles (eg. cue ball selected red border)
    /// </summary>
    public class Pocket: Circle
    {
        public Pocket(int id, Vector2 center, float radius, Color colour)
        {
            ID = id;
            Center = center;
            Radius = radius;
            Colour = colour;
        }
    }
}