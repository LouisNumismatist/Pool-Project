﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePool1
{
    public static class Collisions
    {
        public static Tuple<Ball, Ball> Ball_Ball(Ball a, Ball b, out bool collided)
        {
            float t;
            collided = false;
            if (BallsTouching(a.Center, b.Center, (int)a.Radius, (int)b.Radius, out t) && !a.Collision && !b.Collision)
            {
                collided = true;

                Vector2 componentsDist = a.Center - b.Center; // Magnitude instead of distance() to keep sign, Vector2: DISTANCE BETWEEN BALLS IN VECTOR2 FORM
                float dist = componentsDist.Length(); // Pythagoras of magnitude to find hypotenuse : DISTANCE BETWEEN BALLS AS FLOAT

                float overlap = a.Radius + b.Radius - dist;

                Vector2 minimumTranslationVector = componentsDist * (overlap / dist);

                float totalMass = (1 / a.Mass) + (1 / b.Mass); // (m2 + m1)/(m1*m2)

                //based on proportion of total mass, move the balls apart more or less
                a.Center += minimumTranslationVector * ((1 / a.Mass) / totalMass);
                b.Center -= minimumTranslationVector * ((1 / b.Mass) / totalMass);

                //calculate impact speed
                Vector2 impactVelocity = a.Velocity - b.Velocity;

                Vector2 vectorNormalised = Physics.UnitVector(componentsDist);

                float vn = Vector2.Dot(impactVelocity, vectorNormalised);

                //if balls are not already moving away from each other
                if (vn <= 0)
                {
                    //calculate magnitude of the impulse
                    float i = (-(1 - Physics.coefficient_of_restitution_ball) * vn) / totalMass;
                    Vector2 impulse = componentsDist * i;

                    a.Velocity += impulse / a.Mass;
                    b.Velocity -= impulse / b.Mass;
                }
                
            }

            if (collided && (a.ID == 15 || b.ID == 15) && GamePlay.FirstBallHit == null)
            {
                if (a.ID == 15) GamePlay.FirstBallHit = b;
                else GamePlay.FirstBallHit = a;
            }

            return new Tuple<Ball, Ball>(a, b);
        }

        public static Ball Ball_Wall(Ball a, int BorderWidth, int ScreenWidth, int ScreenHeight)
        {
            float Decrease = Physics.coefficient_of_restitution_rail;
            float Increase = 1 - Decrease;
            bool leftWallCollision = (a.Center.X - a.Radius <= BorderWidth && a.Velocity.X < 0);
            bool rightWallCollision = ((a.Center.X + a.Radius >= ScreenWidth - BorderWidth) && a.Velocity.X > 0);
            bool topWallCollision = (a.Center.Y - a.Radius <= BorderWidth && a.Velocity.Y < 0);
            bool bottomWallCollision = ((a.Center.Y + a.Radius >= ScreenHeight - BorderWidth) && a.Velocity.Y > 0);

            if (leftWallCollision || rightWallCollision)
            {
                if (leftWallCollision)
                {
                    a.Center.X += BorderWidth - a.Center.X + a.Radius;
                }
                else
                {
                    a.Center.X -= (a.Center.X + a.Radius) - (ScreenWidth - BorderWidth);
                }
                a.Velocity *= new Vector2(-1 * Decrease, Decrease);
                a.Acceleration *= new Vector2(-1 * Increase, Increase);
            }

            if (topWallCollision || bottomWallCollision)
            {
                if (topWallCollision)
                {
                    a.Center.Y += BorderWidth - a.Center.Y + a.Radius;
                }
                else
                {
                    a.Center.Y -= (a.Center.Y + a.Radius) - (ScreenHeight - BorderWidth);
                }
                a.Velocity *= new Vector2(Decrease, -1 * Decrease);
                a.Acceleration *= new Vector2(Increase, -1 * Increase);
            }
            return a;
        }

        public static bool Ball_Pocket(Ball a, List<Pocket> PocketList, ref List<Ball> BallsList, int index, Texture2D CueBall, List<Ball> Graveyard, int ScreenWidth, ref bool CueBallPlacing)
        {
            bool potted = false;
            foreach (Pocket p in PocketList)
            {
                if (Vector2.Distance(a.Center, p.Center) <= PocketList[0].Radius)
                {
                    potted = true;
                    //Cue Potted
                    if (a.ID == 15)
                    {
                        BallsList[index] = new Ball(15, new Vector2(274, 274), a.Radius, Vector2.Zero, Vector2.Zero, a.Colour, false, 15);
                        CueBallPlacing = true;
                    }
                    else
                    {
                        //Red or Yellow Ball Potted
                        if (a.Colour != Color.Black)
                        {
                            Graveyard.Add(new Ball(a.ID, new Vector2((ScreenWidth / 2 - 25 * 13) + (Graveyard.Count) * 50, 600), a.Radius, Vector2.Zero, Vector2.Zero, a.Colour, false, a.ID));
                            GamePlay.BallsPotted.Add(a);
                            GamePlay.Potted(ref Game1.Players, Game1.CurrentPlayer, a, ref Game1.EndGame);
                        }
                        BallsList.Remove(a);
                    }
                }
            }
            return potted;
        }

        public static bool BallsTouching(Vector2 a, Vector2 b, float Rad1, float Rad2, out float depth)
        {
            depth = 0;
            float distance = Vector2.Distance(a, b);
            if (distance <= (Rad1 + Rad2))
            {
                depth = (Rad1 + Rad2) - distance;
                return true;
            }
            return false;
        }
    }
}
