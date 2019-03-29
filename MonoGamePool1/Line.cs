using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePool1
{
    /*public class Line
    {
        public int ID;
        public int Thickness;
        public Vector2 Start;
        public Vector2 End;
        public Color Colour;
        public bool Dotted;
    }*/
    /// <summary>
    /// Used for the Pool Cue, Sightline and any other diagonal lines around the table
    /// Draws the lines using the y=mx+c equation found broken up in the Physics class
    /// </summary>
    public class DiagonalLine
    {
        //public int ID;
        public int Thickness;
        public Vector2 Start;
        public Vector2 End;
        public Color Colour;
        public bool Dotted;
        public Texture2D Texture;

        public DiagonalLine(int thickness, Vector2 start, Vector2 end, Color colour, bool dotted)
        {
            //ID = id;
            Thickness = thickness;
            Start = start;
            End = end;
            Colour = colour;
            Dotted = dotted;
            Texture = Graphics.BlankBox;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Get the vector between the start and the end. (from the start point)
            Vector2 vectorBetween = End - Start;
            //Get the angle of the vector.
            float angleVector = (float)Math.Atan2(vectorBetween.Y, vectorBetween.X);
            //The point on the blank square to draw from.
            Vector2 origin = new Vector2(0f, 0.5f);

            spriteBatch.Draw(Game1.PixelBox, Start, null, Colour, angleVector, origin, new Vector2(vectorBetween.Length(), Thickness), SpriteEffects.None, 0f);
            //Graphics.DrawLine(spriteBatch, Start, End, Thickness, Colour, Dotted);
            /*
            float m = Physics.Gradient(Start, End);
            int count = 0;
            if (m != double.PositiveInfinity && m != double.NegativeInfinity)
            {
                float c = Physics.YIntercept(Start, m);
                int prevY = (int)Start.Y;
                int prevX = (int)Start.X;
                int start = 0;
                int end = 0;
                if (Start.X < End.X)
                {
                    start = (int)Start.X;
                    end = (int)End.X;
                }
                else if (Start.X > End.X)
                {
                    start = (int)End.X;
                    end = (int)Start.X;
                }
                if (Start.Y < End.Y)
                {
                    end -= 1;
                }
                for (int x = start; x <= end + 1; x++)
                {
                    count += 1;
                    int y = (int)Physics.LineEquation(x, m, c);
                    int newY = Math.Abs(y - prevY);
                    int newX = x - prevX;
                    Tuple<int, int> prevTuple = InnerDraw(spriteBatch, y, x, m, c, prevY, newY, prevX, newX, count);
                    prevY = prevTuple.Item1;
                    prevX = prevTuple.Item2;
                }
            }
            */
        }

        /*
        public Tuple<int, int> InnerDraw(SpriteBatch spriteBatch, int y, int x, float m, float c, int prevY, int newY, int prevX, int newX, int count)
        {
            if (!Dotted || x % 5 == 0)
            {
                if (newY < 0)
                {
                    newY *= -1;
                }
                if (newX < 0)
                {
                    newX *= -1;
                }
                if (prevY != Start.Y && prevX != Start.X)
                {
                    if (Math.Abs(Physics.TanAngle(Start, End)) < Math.PI / 4) //Far left or far right
                    {
                        spriteBatch.Draw(Texture, new Rectangle(x, prevY, Thickness, newY), Colour);
                    }
                    else //Top or bottom sections
                    {
                        spriteBatch.Draw(Texture, new Rectangle(prevX, y, newX, Thickness), Colour);
                    }

                }
            }
            if (m < 0)
            {
                prevY = y + 1;
                prevX = x + 1;
            }
            else
            {
                prevY = y - 1;
                prevX = x - 1;
            }
            return new Tuple<int, int>(prevY, prevX);
        }
        */

        /*public static DiagonalLine InitialiseDiagonalLine(Color color)
        {
            return new DiagonalLine(0, 3, new Vector2(100), new Vector2(200, 50), color, false);
        }*/
    }

        /*public class SightLine : DiagonalLine
        {
            public SightLine(int id, int thickness, Vector2 start, Vector2 end, Color colour)
            {
                ID = id;
                Thickness = thickness;
                Start = start;
                End = end;
                Colour = colour;
                Dotted = false;
            }
        }*/

        /*public class PoolCue : DiagonalLine
        {
            public PoolCue(int id, int thickness, Vector2 start, Vector2 end, Color colour)
            {
                ID = id;
                Thickness = thickness;
                Start = start;
                End = end;
                Colour = colour;
                Dotted = true;
            }
        }
        public class GraphicsLine : DiagonalLine
        {
            public GraphicsLine(int id, int thickness, Vector2 start, Vector2 end, Color colour, bool dotted)
            {
                ID = id;
                Thickness = thickness;
                Start = start;
                End = end;
                Colour = colour;
                Dotted = dotted;
            }
        }*/
}
