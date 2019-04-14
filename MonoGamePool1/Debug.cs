using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePool1
{
    /// <summary>
    /// All the functions for when the program is being tested and debugged, most are activated by the Debug Button on screen
    /// </summary>
    public class Debug : Game1
    {
        public static bool showBallNumbers = false;
        public static bool canPingBall = true;
        public static bool visualCoords = false;
        public static bool sightStatus = true;
        public static float speed = 11.75f; //11.75
        public static int rows = 5;
        public static bool speedTest = false;
        public static bool boundingBoxes = false;
        
        public static void NumberBalls(Ball a, SpriteBatch spriteBatch)
        {
            //Display each ball ID on top of the ball
            if (showBallNumbers)
            {
                string text = a.ID.ToString();
                Vector2 size = font.MeasureString(text);
                Color color;

                if (a.ID == 15 || a.Colour == Color.Yellow) //Use whichever colour will show up better
                {
                    color = Color.Black;
                }
                else
                {
                    color = Color.White;
                }
                spriteBatch.DrawString(font, text, a.Center, color, 0f, size * 0.5f, 1f, SpriteEffects.None, 0f);
            }
        }

        public static Ball PingBall(Ball a)
        {
            //Ball hitting mechanics for the cue ball
            if (canPingBall)
            {
                if (a.ID == 15)
                {
                    if (Vector2.Distance(Input.mousePosition, a.Center) < a.Radius)
                    {
                        if (a.Velocity == Vector2.Zero && Input.LeftMouseJustClicked())
                        {
                            HittingCueBall = !HittingCueBall;
                            //Begin ball ping if hovering over and clicking ball
                        }
                    }
                    else
                    {
                        if (HittingCueBall && Input.LeftMouseJustReleased())
                        {
                            Vector2 line = a.Center - Input.mousePosition;
                            line /= speed;

                            Ball cue = a;
                            cue.Velocity = line;
                            a = cue;

                            HittingCueBall = false;
                            //Ball let go of

                            Updates.UpdateCurrentPlayer(ref CurrentPlayer, Players);
                        }
                    }
                }
            }
            return a;
        }

        public static void ShowCoords(SpriteBatch spriteBatch)
        {
            //Shows the Vector2 coordinates of the user's cursor on screen
            if (visualCoords)
            {
                string text = "(" + Input.mousePosition.X + ", " + Input.mousePosition.Y + ")";
                spriteBatch.DrawString(font, text, new Vector2(Input.mousePosition.X + 15, Input.mousePosition.Y + 10), Color.Black, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
            }
        }

        public static void DrawBoundingBoxes(SpriteBatch spriteBatch, List<Ball> ballsList, Texture2D texture)
        {
            //Draws boxes around the balls and mini sight lines to show their path
            if (boundingBoxes)
            {
                foreach (Ball ball in ballsList)
                {
                    spriteBatch.Draw(texture, new Rectangle((int)(ball.Center.X - ball.Radius), (int)(ball.Center.Y - ball.Radius), (int)ball.Radius * 2, 1), Color.Black); //Top
                    spriteBatch.Draw(texture, new Rectangle((int)(ball.Center.X - ball.Radius), (int)(ball.Center.Y - ball.Radius), 1, (int)ball.Radius * 2), Color.Black); //Left
                    spriteBatch.Draw(texture, new Rectangle((int)(ball.Center.X + ball.Radius), (int)(ball.Center.Y - ball.Radius), 1, (int)ball.Radius * 2), Color.Black); //Right
                    spriteBatch.Draw(texture, new Rectangle((int)(ball.Center.X - ball.Radius), (int)(ball.Center.Y + ball.Radius), (int)ball.Radius * 2 + 1, 1), Color.Black); //Bottom

                    if (ball.Velocity.Length() > 0)
                    {
                        Vector2 unitVector = Physics.UnitVector(ball.Velocity) * ball.Radius * 2;
                        new DiagonalLine(1, ball.Center, new Vector2(ball.Center.X + unitVector.X, ball.Center.Y + unitVector.Y), Color.White, false).Draw(spriteBatch);
                    }
                }
            }            
        }
        public static void DebugGame()
        {
            //Changes the states of the Debug settings when entering debug mode
            visualCoords = !visualCoords;
            showBallNumbers = !showBallNumbers;
            speedTest = !speedTest;
            boundingBoxes = !boundingBoxes;
        }
    }
}
