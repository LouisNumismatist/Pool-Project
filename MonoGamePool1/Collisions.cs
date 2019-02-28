using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePool1
{
    public static class Collisions
    {
        public static Tuple<Ball, Ball> Ball_Ball(Ball a, Ball b)
        {
            if (BallsTouching(a.Center, b.Center, (int)a.Radius, (int)b.Radius) && !a.Collision && !b.Collision)
            {
                Vector2 componentsDist = a.Center - b.Center; // Magnitude instead of distance() to keep sign, Vector2: DISTANCE BETWEEN BALLS IN VECTOR2 FORM
                float dist = componentsDist.Length(); // Pythagoras of magnitude to find hypotenuse : DISTANCE BETWEEN BALLS AS FLOAT

                /*if (Vector2.Distance(a.Center, b.Center) != distanceBetweenBalls) //REPLACE WITH DISTANCE()
                {
                    Console.Write(distanceBetweenBalls);
                    Console.Write(" , ");
                    Console.WriteLine(Vector2.Distance(a.Center, b.Center));
                }*/

                float overlap = a.Radius + b.Radius - dist;

                Vector2 minimumTranslationVector = componentsDist * (overlap / dist);

                float mass = 0.170097f; // a.Mass
                float mass2 = 0.170097f; // b.Mass

                float invMass1 = 1f / mass;
                float invMass2 = 1f / mass2;

                float totalMass = invMass1 + invMass2; // (m2 + m1)/(m1*m2)

                //based on proportion of total mass, move the balls apart more or less
                a.Center += minimumTranslationVector * (invMass1 / totalMass);
                b.Center -= minimumTranslationVector * (invMass2 / totalMass);

                //calculate impact speed
                Vector2 impactVelocity = a.Velocity - b.Velocity;

                Vector2 vectorNormalised = componentsDist / componentsDist.Length();

                float vn = Vector2.Dot(impactVelocity, vectorNormalised);

                //if balls are not already moving away from each other
                if (vn <= 0f)
                {
                    //calculate magnitude of the impulse
                    float i = (-(1f - Physics.coefficient_of_restitution_ball) * vn) / totalMass;
                    Vector2 impulse = componentsDist * i;

                    a.Velocity += impulse * invMass1;
                    b.Velocity -= impulse * invMass2;
                }
            }

            return new Tuple<Ball, Ball>(a, b);
        }

        public static Tuple<Ball, Ball> Ball_Ball_Old(Ball a, Ball b)
        {
            //Ball TempA = new Ball(a.ID, new Vector2(a.Center.X - a.Velocity.X, a.Center.Y - a.Velocity.Y), a.Radius, a.Velocity, a.Acceleration, a.Texture);
            //Ball TempB = new Ball(a.ID, new Vector2(b.Center.X - b.Velocity.X, b.Center.Y - b.Velocity.Y), b.Radius, b.Velocity, b.Acceleration, b.Texture);
            float Decrease = 0.9f;
            //Console.WriteLine("{0}: {1}, {2}: {3}", a.ID, a.Collision, b.ID, b.Collision);
            if (BallsTouching(a.Center, b.Center, (int)a.Radius, (int)b.Radius) && !a.Collision && a.PrevBall != b.ID)
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

                if (BallsTouching(a.Center, b.Center, (int)a.Radius, (int)b.Radius) == false)
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

        public static bool BallsTouching(Vector2 a, Vector2 b, int Rad1, int Rad2)
        {
            if ((Vector2.Distance(a, b)) <= (Rad1 + Rad2))
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
