using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MonoGamePool1
{
    public static class Physics
    {
        public const float coefficient_of_friction_ball = 0.08f; //0.03-0.08  - FRICTION IN COLLISION
        public const float coefficient_sliding_friction_cloth = 0.4f; //0.15-0.4 - IGNORE: FOR SPIN
        public const float coefficient_of_rolling_resistance_cloth = 0.015f; //0.005-0.015 - FRICTION ON TABLE

        //FOR INSTANTANIOUS ACTIONS
        public const float coefficient_of_restitution_ball = 0.98f; // 0.92-0.98
        public const float coefficient_of_restitution_rail = 0.9f; //0.6-0.9
        public const float coefficient_of_restitution_table = 0.5f; //IGNORE: BOUNCING BALL
        public const float coefficient_of_restitution_cue = 0.75f; //0.71-0.75 (leather tip), 0.81-0.87 (phenolic tip)

        public const float ball_mass_moment_of_inertia = 0.4f; //2/5 mR^2
        public const float spin_deceleration_rate_cloth = 15f; //5-15 rad/sec^2

        public const float g = 9.81f;

        public static double Pythagoras1(double opp, double adj) //Finding hyp
        {
            return Math.Sqrt(Math.Pow(opp, 2) + Math.Pow(adj, 2));
        }

        public static double Pythagoras2(double hyp, double adj) //Finding opp or adj
        {
            return Math.Sqrt(Math.Pow(hyp, 2) - Math.Pow(adj, 2));
        }

        public static float Gradient(Vector2 a, Vector2 b)
        {
            return ((b.Y - a.Y) / (b.X - a.X));
        }

        public static float YIntercept(Vector2 a, float Gradient)
        {
            return (a.Y - (a.X * Gradient));
        }

        public static float LineEquation(float x, float m, float c)
        {
            return (m * x + c);
        }

        public static float FindX(float y, float m, float c)
        {
            return ((y - c) / m);
        }

        /*public static double SUVATSelect(string miss, string find, float aa, float bb, float cc)
        {
            float s;
            float u;
            float v;
            float a;
            float t;

            if (find == "s")
            {
                if (miss == "u")
                {
                    v = aa;
                    a = bb;
                    t = cc;
                    return (v * t - 0.5 * a * Math.Pow(t, 2));
                }
            }
        }*/

        public static double Impulse()
        {
            return 0;
            //return Impulse;
        }

        public static bool CircleLine(Ball Circle, Vector2 LinePos)
        {
            if (Vector2.Distance(Circle.Center, LinePos) <= Circle.Radius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int CountTotal(int i)
        {
            int total = 0;
            for (int j = 1; j <= i; j++)
            {
                total += j;
            }
            return total;
        }

        public static double SinAngle(Vector2 opp, Vector2 hyp)
        {
            return Math.Asin((double)Gradient(hyp, opp));
        }

        public static double CosAngle(Vector2 hyp, Vector2 adj)
        {
            return Math.Acos((double)Gradient(adj, hyp));
        }

        public static double TanAngle(Vector2 adj, Vector2 opp)
        {
            return Math.Atan((double)Gradient(opp, adj));
        }

        public static double Friction(float F, float Coefficient)
        {
            return F * Coefficient;
        }

        public static double NewtonAcc(Ball ball)
        {
            return ball.Mass * Pythagoras1(ball.Acceleration.X, ball.Acceleration.Y);
        }

        public static double Acceleration(float v1, float v2, float t)
        {
            return (v2 - v1) / t;
        }

        public static double RestitutionVelocity(float u, float coefficient)
        {
            return u * coefficient;
        }

        public static double KineticEnergy(float m, float v)
        {
            return 0.5 * m * Math.Pow(v, 2);
        }

        public static double Momentum(float m, float v)
        {
            return m * v;
        }

        public static Tuple<float, float> FindVelocities(float m1, float m2, float u1, float u2, float e)
        {
            float v1 = (m1 * u1 + m2 * u2 - e * m2 * (u2 - u1)) / (m1 + m2);

            float v2 = e * (u2 - u1) + v1;

            return new Tuple<float, float>(v1, v2);
        }

        public static Vector2 UnitVector(Vector2 vector)
        {
            float length = (float)Pythagoras1(vector.X, vector.Y);
            return (1 / length) * vector;
        }

        //Circular Motion Equations

        public static float SphereCircumference(float radius)
        {
            return (float)Math.PI * radius * 2;
        }

        public static float Frequency(float radius, Vector2 velocity)
        {
            float C = SphereCircumference(radius);
            float S = velocity.Length();
            float T = C / S;
            return 1 / T;
        }

        public static float AngularSpeed(Vector2 velocity, float radius)
        {
            float S = velocity.Length();
            return S / radius;
        }

        public static float CentripetalAcceleration(Vector2 velocity, float radius)
        {
            float angularSpeed = AngularSpeed(velocity, radius);
            return (float)Math.Pow(angularSpeed, 2) * radius;
        }

        public static float CentripetalForce(Vector2 velocity, float radius, float mass)
        {
            return mass * CentripetalAcceleration(velocity, radius);
        }
    }
}