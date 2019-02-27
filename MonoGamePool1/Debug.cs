using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePool1
{
    public static class Debug
    {
        public static bool showBallNumbers = false;
        public static bool canPingBall = true;
        public static bool visualCoords = false;
        public static bool sightStatus = true;
        public static string color = "green";
        public static float speed = 11.75f; //11.75
        public static int rows = 5;
        public static int ballBorderWidth = 2;
        public static bool speedTest = false;
        
        public static void NumberBalls(Ball a, SpriteBatch spriteBatch)
        {
            if (showBallNumbers)
            {
                string text = a.ID.ToString();
                Vector2 size = Game1.font.MeasureString(text);
                Color color;

                if (a.ID == 15 || a.Color.Equals(Game1.YellowBall))
                {
                    color = Color.Black;
                }
                else
                {
                    color = Color.White;
                }
                spriteBatch.DrawString(Game1.font, text, a.Center, color, 0f, size * 0.5f, 1f, SpriteEffects.None, 0f);
            }
        }
        public static Ball PingBall(Ball a)
        {
            if (canPingBall)
            {
                if (a.ID == 15)
                {
                    if (Vector2.Distance(Input.mousePosition, a.Center) < a.Radius)
                    {
                        if (a.Velocity == Vector2.Zero && Input.LeftMouseJustClicked())
                        {
                            Game1.HittingCueBall = !Game1.HittingCueBall;
                            //Begin ball ping if hovering over and clicking ball
                        }
                    }
                    else
                    {
                        if (Game1.HittingCueBall && Input.LeftMouseJustReleased())
                        {
                            Vector2 line = a.Center - Input.mousePosition;
                            line /= Debug.speed;

                            Ball cue = a;
                            cue.Velocity = line;
                            a = cue;

                            Game1.HittingCueBall = false;
                            //Ball let go of
                        }
                    }
                }
            }
            return a;
        }
        public static void ShowCoords(SpriteBatch spriteBatch)
        {
            if (visualCoords)
            {
                string text = "(" + Input.mousePosition.X + ", " + Input.mousePosition.Y + ")";
                spriteBatch.DrawString(Game1.font, text, new Vector2(Input.mousePosition.X + 15, Input.mousePosition.Y + 10), Color.Black, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
            }
        }
        public static Tuple<Color, string> ChangeTextures()
        {
            if (color == "green" || color == "mario")
            {
                return new Tuple<Color, string>(Color.SaddleBrown, "Green Felt");
            }
            else if (color == "blue")
            {
                return new Tuple<Color, string>(Color.DimGray, "Blue Felt");
            }
            else
            {
                return new Tuple<Color, string>(Color.DimGray, "KERMIE_Saturated_Gradient");
            }
        }
        public static bool BlackEight()
        {
            if (color == "green" || color == "blue")
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
