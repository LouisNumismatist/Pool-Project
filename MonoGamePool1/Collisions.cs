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

                if (Vector2.Distance(a.Center, b.Center) != dist) //REPLACE WITH DISTANCE()
                {
                    Console.Write(dist);
                    Console.Write(" , ");
                    Console.WriteLine(Vector2.Distance(a.Center, b.Center));
                }

                float overlap = a.Radius + b.Radius - dist;

                Vector2 minimumTranslationVector = componentsDist * (overlap / dist);

                //float mass = 0.170097f; // a.Mass
                //float mass2 = 0.170097f; // b.Mass

                //float invMass1 = 1f / a.Mass;
                //float invMass2 = 1f / b.Mass;

                float totalMass = (1 / a.Mass) + (1 / b.Mass); // (m2 + m1)/(m1*m2)

                //based on proportion of total mass, move the balls apart more or less
                a.Center += minimumTranslationVector * ((1 / a.Mass) / totalMass);
                b.Center -= minimumTranslationVector * ((1 / b.Mass) / totalMass);

                //calculate impact speed
                Vector2 impactVelocity = a.Velocity - b.Velocity;

                Vector2 vectorNormalised = componentsDist / componentsDist.Length();

                float vn = Vector2.Dot(impactVelocity, vectorNormalised);

                //if balls are not already moving away from each other
                if (vn <= 0f)
                {
                    //calculate magnitude of the impulse
                    float i = (-(1 - Physics.coefficient_of_restitution_ball) * vn) / totalMass;
                    Vector2 impulse = componentsDist * i;

                    a.Velocity += impulse / a.Mass;
                    b.Velocity -= impulse / b.Mass;
                }
                
            }

            return new Tuple<Ball, Ball>(a, b);
        }

        public static Tuple<Ball, Ball> Ball_Ball_Old(Ball a, Ball b)
        {
            //Ball TempA = new Ball(a.ID, new Vector2(a.Center.X - a.Velocity.X, a.Center.Y - a.Velocity.Y), a.Radius, a.Velocity, a.Acceleration, a.Texture);
            //Ball TempB = new Ball(a.ID, new Vector2(b.Center.X - b.Velocity.X, b.Center.Y - b.Velocity.Y), b.Radius, b.Velocity, b.Acceleration, b.Texture);
            float Decrease = 0.9f;
            float depth;
            //Console.WriteLine("{0}: {1}, {2}: {3}", a.ID, a.Collision, b.ID, b.Collision);
            if (BallsTouching(a.Center, b.Center, (int)a.Radius, (int)b.Radius, out depth) && !a.Collision && a.PrevBall != b.ID)
            //if (BallsTouching(a.Center, b.Center, (int)a.Radius, (int)b.Radius) == true && (General.SameSign(a.Center.X - a.PrevCenter.X, a.Velocity.X) == true || General.SameSign(a.Center.Y - a.PrevCenter.Y, a.Velocity.Y) == true))
            {
                a.Write();
                b.Write();
                Console.WriteLine("\n");
                a.Collision = true;
                b.Collision = true;
                a.PrevBall = b.ID;
                b.PrevBall = a.ID;
                if (a.ID == 15 || b.ID == 15)
                {
                    //Graphics.WriteBall(a);
                    //Graphics.WriteBall(b);
                }
                //Console.WriteLine("\n");
                //Graphics.WriteBall(a);
                //Graphics.WriteBall(b);
                //Change direction code
                //Console.WriteLine("Pre: {0}, {1}, {2}, {3}", a.Velocity, b.Velocity, a.ID, b.ID);
                if (b.Velocity == Vector2.Zero)
                {
                    Console.WriteLine("a 1");
                    //Graphics.WriteBall(a);
                    //Graphics.WriteBall(b);
                    Tuple<Ball, Ball> TempTuple = Static_Ball(b, a);
                    b = TempTuple.Item1;
                    a = TempTuple.Item2;
                    //Graphics.WriteBall(a);
                    //Graphics.WriteBall(b);
                    //Console.WriteLine("\n");
                    /*b.Velocity = Decrease * a.Velocity;
                    a.Velocity *= -1 * Decrease;
                    b.Acceleration = Decrease * a.Acceleration;
                    a.Acceleration *= -1 * Decrease;*/
                    a.Collision = true;
                    b.Collision = true;
                    a.PrevBall = a.ID;
                    b.PrevBall = b.ID;
                }
                else if (a.Velocity == Vector2.Zero)
                {
                    Console.WriteLine("a 2");
                    //Graphics.WriteBall(a);
                    //Graphics.WriteBall(b);
                    Tuple<Ball, Ball> TempTuple = Static_Ball(a, b);
                    a = TempTuple.Item1;
                    b = TempTuple.Item2;
                    //Graphics.WriteBall(a);
                    //Graphics.WriteBall(b);
                    //Console.WriteLine("\n");
                    a.Collision = true;
                    b.Collision = true;
                    a.PrevBall = a.ID;
                    b.PrevBall = b.ID;

                    /*a.Velocity = Decrease * b.Velocity;
                    b.Velocity *= -1 * Decrease;
                    a.Acceleration = Decrease * b.Acceleration;
                    b.Acceleration *= -1 * Decrease;*/
                }
                //if (General.SameSign(a.Center.X - a.PrevCenter.X, a.Velocity.X) == true || General.SameSign(a.Center.Y - a.PrevCenter.Y, a.Velocity.Y) == true)
                if (((a.Velocity.X > 0) && (b.Velocity.X > 0)) || ((a.Velocity.X < 0) && (b.Velocity.X < 0)))
                {
                    Console.WriteLine("b 1");
                    Vector2 TempA = a.Velocity;
                    a.Velocity.Y = b.Velocity.Y * Decrease;
                    b.Velocity.Y = TempA.Y * Decrease;
                    a.Acceleration.Y *= -1 * Decrease;
                    b.Acceleration.Y *= -1 * Decrease;
                    a.Collision = true;
                    b.Collision = true;
                    a.PrevBall = a.ID;
                    b.PrevBall = b.ID;
                }
                else if (((a.Velocity.Y > 0) && (b.Velocity.Y > 0)) || ((a.Velocity.Y < 0) && (b.Velocity.Y < 0)))
                {
                    Console.WriteLine("b 2");
                    Vector2 TempA = a.Velocity;
                    //a.Velocity.X *= -1 * Decrease;
                    //b.Velocity.X *= -1 * Decrease;
                    a.Velocity.X = b.Velocity.X * Decrease;
                    b.Velocity.X = TempA.X * Decrease;
                    a.Acceleration.X *= -1 * Decrease;
                    b.Acceleration.X *= -1 * Decrease;
                    a.Collision = true;
                    b.Collision = true;
                    a.PrevBall = a.ID;
                    b.PrevBall = b.ID;
                }
                //else
                //{
                float depth1;
                if (BallsTouching(a.Center, b.Center, (int)a.Radius, (int)b.Radius, out depth1) == false)
                {
                    Console.WriteLine("c");
                    Vector2 tempA = a.Velocity;
                    Vector2 tempB = b.Velocity;
                    //SWITCH STATEMENT HERE
                    a.Velocity.X = -tempB.Y;
                    a.Velocity.Y = -tempB.X;

                    b.Velocity.X = -tempA.Y;
                    b.Velocity.Y = -tempA.X;
                    //a.Velocity *= -1 * Decrease;
                    //b.Velocity *= -1 * Decrease;
                    a.Acceleration *= -Decrease;
                    b.Acceleration *= -Decrease;

                    a.Collision = true;
                    b.Collision = true;
                    a.PrevBall = a.ID;
                    b.PrevBall = b.ID;
                }
                if ((a.ID == 15) || (b.ID == 15))
                {
                    //Console.WriteLine("HERE");
                    //Graphics.WriteBall(a);
                    //Graphics.WriteBall(b);
                    //Console.WriteLine("\n");
                    //Console.WriteLine("\n");
                }
                //}
                /*Vector2 temp = a.Velocity;
                a.Velocity = 0.9f * b.Velocity;
                b.Velocity = 0.9f * temp;*/

                //b.Velocity += a.Velocity;
                //a.Velocity *= -1;
                //a.Velocity /= 2;
                //b.Acceleration += a.Acceleration;
                //a.Acceleration *= -1;
                //a.Acceleration /= 2;
                //Console.WriteLine("End: {0}, {1}, {2}, {3}", a.Velocity, b.Velocity, a.ID, b.ID);

                //a.Velocity *= -1;
                //b.Velocity *= -1;
                //a.Acceleration *= -1;
                //b.Acceleration *= -1;
                //Console.WriteLine("End: {0}, {1}", a.Velocity, b.Velocity);
                // Graphics.WriteBall(a);
                //Graphics.WriteBall(b);
            }
            else
            {
                //Console.Write("U ");
                a.Collision = false;
                b.Collision = false;
                a.PrevBall = a.ID;
                b.PrevBall = b.ID;
            }
            if (a.Collision)
            {
                a.Write();
                b.Write();
                Console.WriteLine("\n");
                Console.WriteLine("\n");
            }

            return new Tuple<Ball, Ball>(a, b);
        }

        public static Tuple<Ball, Ball> Ball_Ball_New(Ball a, Ball b, float e)
        {
            float distance = Vector2.Distance(a.Center, b.Center); //For collision corection - visual
            float overlap = (a.Radius + b.Radius) - distance;

            if (BallsTouching(a.Center, b.Center, (int)a.Radius, (int)b.Radius, out overlap) && !a.Collision && !b.Collision)
            {
                float verticalComponent = Math.Abs(a.Center.Y - b.Center.Y); //Between balls
                float horizontalComponent = Math.Abs(a.Center.X - b.Center.X);
                float angle = (float)Math.Atan2(verticalComponent, horizontalComponent);

                float aAngle = (float)Math.Atan2(a.Velocity.Y, a.Velocity.X); //Angle at which the balls interact with eachother
                float bAngle = (float)Math.Atan2(b.Velocity.Y, b.Velocity.X);

                float aCompAngle = angle - aAngle;
                float bCompAngle = MathHelper.Pi - angle - bAngle;

                float aSpeedPrev = a.Velocity.Length(); //Scalar speeds of balls
                float bSpeedPrev = b.Velocity.Length();

                Vector2 aCompPrev = new Vector2(); //Rotated angles of interaction
                Vector2 bCompPrev = new Vector2();

                aCompPrev.X = (float)(aSpeedPrev * Math.Cos(aCompAngle)); //Convert to rotated components
                bCompPrev.X = (float)(bSpeedPrev * Math.Cos(bCompAngle));

                aCompPrev.Y = (float)(aSpeedPrev * Math.Sin(aCompAngle));
                bCompPrev.Y = (float)(bSpeedPrev * Math.Sin(bCompAngle));

                Vector2 aCompPost = new Vector2();
                Vector2 bCompPost = new Vector2();

                aCompPost.Y = aCompPrev.Y;
                bCompPost.Y = bCompPrev.Y;

                if (a.ID == 15 || b.ID == 15) //Cue Ball Collision
                {
                    aCompPost.X = (aCompPrev.X * (a.Mass + e * a.Mass) - bCompPrev.X * (e * b.Mass)) / (a.Mass + b.Mass);
                    bCompPost.X = (bCompPrev.X * (e * b.Mass - b.Mass) + aCompPrev.X * (e * a.Mass)) / (a.Mass + b.Mass);
                }
                else //Two normal Balls colliding
                {
                    aCompPost.X = (aCompPrev.X * (1 + e) + bCompPrev.X * (e - 1)) / 2;
                    bCompPost.X = (aCompPrev.X * (1 - e) - bCompPrev.X * (e + 1)) / 2;
                }

                float aAnglePost = (float)Math.Atan2(aCompPost.Y, aCompPost.X);
                float bAnglePost = (float)Math.Atan2(bCompPost.Y, bCompPost.X);

                float aSpeedPost = aCompPost.Length();
                float bSpeedPost = bCompPost.Length();

                float aCompAnglePost = angle - aAnglePost;
                float bCompAnglePost = MathHelper.ToRadians(180) - angle - bAnglePost;

                Vector2 aFinalComp = new Vector2();
                Vector2 bFinalComp = new Vector2();

                aFinalComp.X = (float)(aSpeedPost * Math.Cos(aCompAnglePost)); //Convert to rotated components
                bFinalComp.X = (float)(bSpeedPost * Math.Cos(bCompAnglePost));

                aFinalComp.Y = (float)(aSpeedPost * Math.Sin(aCompAnglePost));
                bFinalComp.Y = (float)(bSpeedPost * Math.Sin(bCompAnglePost));

                a.Velocity = aFinalComp;
                b.Velocity = bFinalComp;
            }
            return new Tuple<Ball, Ball>(a, b);

        }

        public static Tuple<Ball, Ball> Ball_Ball_New_2(Ball a, Ball b, float e)
        {
            float collDepth;
            if (BallsTouching(a.Center, b.Center, a.Radius, b.Radius, out collDepth))
            {
                /*
                float line_of_centres_angle = (float)Math.Atan2(b.Center.Y - a.Center.Y, b.Center.X - a.Center.X);

                float aSpeed = (float)Physics.Pythagoras1(a.Velocity.X, a.Velocity.Y);
                float bSpeed = (float)Physics.Pythagoras1(b.Velocity.X, b.Velocity.Y);

                float aAngle = (MathHelper.Pi - line_of_centres_angle) - (float)Math.Atan2(a.Velocity.X, a.Velocity.Y);
                float bAngle = line_of_centres_angle - (float)Math.Atan2(b.Velocity.Y, b.Velocity.X);
                */

                Vector2 aVelocity = a.Velocity;
                Vector2 bVelocity = b.Velocity;

                float deltaAngle = (a.Center - b.Center).ToRotation();

                float alpha = Math.Abs(deltaAngle - a.Velocity.ToRotation());
                float beta = Math.Abs(deltaAngle - (b.Velocity * -1f).ToRotation());

                Vector2 newAVelocity, newBVelocity;

                float mu = a.Velocity.Length();
                float ro = b.Velocity.Length();

                newAVelocity.X = (mu * (float)Math.Cos(alpha) * (a.Mass - e * b.Mass) - ro * (float)Math.Cos(beta) * (b.Mass + e * b.Mass)) / (a.Mass + b.Mass);
                newAVelocity.Y = mu * (float)Math.Sin(alpha);

                newBVelocity.X = (mu * (float)Math.Cos(alpha) * (a.Mass + e * a.Mass) + ro * (float)Math.Cos(beta) * (e * a.Mass - b.Mass)) / (a.Mass + b.Mass);
                newBVelocity.Y = ro * (float)Math.Sin(beta);

                float angleToUseA = a.Velocity == Vector2.Zero ? newAVelocity.ToRotation() : alpha;
                float angleToUseB = b.Velocity == Vector2.Zero ? newBVelocity.ToRotation() : beta;

                a.Velocity = angleToUseA.ToVector2() * newAVelocity.Length();
                b.Velocity = angleToUseB.ToVector2() * newBVelocity.Length();

                newAVelocity.Normalize();

                a.Center += collDepth * newAVelocity;
            }

            return new Tuple<Ball, Ball>(a, b);
        }

        public static Tuple<Ball, Ball> Static_Ball(Ball a, Ball b)
        {
            //a static, b dynamic
            a.Velocity = b.Velocity;
            b.Velocity = new Vector2(b.Velocity.Y, b.Velocity.X);
            a.Acceleration = b.Acceleration;
            b.Acceleration = new Vector2(b.Acceleration.Y, b.Acceleration.X);
            float DiffX = 0f;
            float DiffY = 0f;
            if (b.Velocity.X > 0)
            {
                DiffX = a.Center.X - b.Center.X;
            }
            else
            {
                DiffX = b.Center.X - a.Center.X;
            }
            if (b.Velocity.Y > 0)
            {
                DiffY = a.Center.Y - b.Center.Y;
            }
            else
            {
                DiffY = b.Center.Y - a.Center.Y;
            }
            //float DiffX = Math.Abs(a.Center.X - b.Center.X);
            //float DiffY = Math.Abs(a.Center.Y - b.Center.Y);
            float Distance = Vector2.Distance(a.Center, b.Center);
            float Velocity = (float)Physics.Pythagoras1(a.Velocity.X, a.Velocity.Y);
            double Angle = Math.Atan(DiffX / DiffY);
            double CompX = Velocity * Math.Sin(Angle);
            double CompY = Velocity * Math.Cos(Angle);
            a.Velocity = new Vector2((float)CompX, (float)CompY);
            b.Velocity = new Vector2((float)CompY, (float)CompX);
            return new Tuple<Ball, Ball>(a, b);
        }

        public static void Dynamic_Ball(Ball a, Ball b)
        {

        }

        public static Ball Ball_Wall(Ball a, int BorderWidth, int ScreenWidth, int ScreenHeight)
        {
            float Decrease = 0.95f;
            float Increase = 1 - Decrease;
            bool leftWallCollision = (a.Center.X - a.Radius <= BorderWidth && a.Velocity.X < 0);
            bool rightWallCollision = ((a.Center.X + a.Radius >= ScreenWidth - BorderWidth) && a.Velocity.X > 0);
            bool topWallCollision = (a.Center.Y - a.Radius <= BorderWidth && a.Velocity.Y < 0);
            bool bottomWallCollision = ((a.Center.Y + a.Radius >= ScreenHeight - BorderWidth) && a.Velocity.Y > 0);

            if (leftWallCollision || rightWallCollision)
            {
                //Graphics.WriteBall(a);
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

        public static List<Ball> Ball_Pocket(Ball a, List<Pocket> PocketList, List<Ball> BallsList, int index, Texture2D CueBall, List<Ball> Graveyard, int ScreenWidth)
        {
            //int Tracker = 0;
            foreach (Pocket p in PocketList)
            {
                if (Vector2.Distance(a.Center, p.Center) <= PocketList[0].Radius)
                {
                    if (a.ID == 15)
                    {
                        BallsList[index] = new Ball(15, new Vector2(274, 274), a.Radius, Vector2.Zero, Vector2.Zero, a.Colour, false, 15);

                        //a.Center = new Vector2(274, 274);
                        //a.Velocity = new Vector2(0, 0);
                        //return 0; //Cue Potted
                    }
                    /*if (a.ID == 6)
                    {
                        BallsList.Remove(a);
                        for (int i = 0; i < BallsList.Count; i++)
                        {
                            BallsList[i].Velocity = Vector2.Zero;
                        }
                        //return 1; //Eight Ball Potted
                    }*/
                    else
                    {
                        if (a.ID != 6)
                        {
                            Graveyard.Add(new Ball(a.ID, new Vector2((ScreenWidth / 2 - 25 * 13) + (Graveyard.Count) * 50, 600), a.Radius, Vector2.Zero, Vector2.Zero, a.Colour, false, a.ID));
                        }
                        BallsList.Remove(a);
                        //return 2; //Red or Yellow Ball Potted
                    }
                }
            }
            return BallsList;
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
