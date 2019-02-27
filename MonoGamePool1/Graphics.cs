using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MonoGamePool1
{
    public class Graphics
    {
        static int BorderWidth = Game1.BorderWidth;
        static int ScreenHeight = Game1.ScreenHeight;
        static int ScreenWidth = Game1.ScreenWidth;
        static int BallDiam = Game1.BallDiam;

        public static void DrawBoard(SpriteBatch spriteBatch, Texture2D TableInner, Texture2D TableOuter)
        {
            int BoxWidth = ScreenWidth - 2 * BorderWidth;
            int BoxHeight = ScreenHeight - 2 * BorderWidth;
            int ThinBorder = 4;
            //Outer and Inner Borders
            spriteBatch.Draw(TableOuter, new Rectangle(BorderWidth - ThinBorder, BorderWidth - ThinBorder, BoxWidth + 2 * ThinBorder, BoxHeight + 2 * ThinBorder), Color.LightGray);
            spriteBatch.Draw(TableInner, new Rectangle(BorderWidth, BorderWidth, ScreenWidth - 2 * BorderWidth, ScreenHeight - 2 * BorderWidth), Color.White);
        }
        public static void DrawBalls(SpriteBatch spriteBatch, List<Ball> BallsList, Texture2D EightBall)
        {
            foreach (Ball a in BallsList)
            {
                float radius = a.Radius;
                int diameter = (int)(2 * radius);
                int x = (int)(a.Center.X - radius);
                int y = (int)(a.Center.Y - radius);
                if (a.Color == EightBall && Debug.BlackEight())
                {
                    spriteBatch.Draw(a.Color, new Rectangle(x, y, diameter, diameter), Color.Black);
                    //Draws the Eight Ball with a black border (avoids excess white pixels)
                }
                else if (a.ID == 15)
                {
                    if (Game1.HittingCueBall && Debug.canPingBall)
                    {
                        spriteBatch.Draw(a.Color, new Rectangle(x - Debug.ballBorderWidth, y - Debug.ballBorderWidth, diameter + 2 * Debug.ballBorderWidth, diameter + 2* Debug.ballBorderWidth), Color.Red);
                        //Draws red ball behind ball when held
                    }
                    spriteBatch.Draw(a.Color, new Rectangle(x, y, diameter, diameter), Color.White);
                    //Overlaps red ball with cue ball to draw cue ball with red border

                }
                else
                {
                    spriteBatch.Draw(a.Color, new Rectangle(x, y, diameter, diameter), Color.White);
                    //Draws other balls to screen
                }
                Debug.NumberBalls(a, spriteBatch);
                //Draw numbers on balls if enabled
            }
        }
        public static void DrawPockets(SpriteBatch spriteBatch, List<Pocket> PocketList, Texture2D EightBall)
        {
            foreach (Pocket p in PocketList)
            {
                float radius = p.Radius;
                int diameter = (int)(2 * radius);
                int x = (int)(p.Center.X - radius);
                int y = (int)(p.Center.Y - radius);
                spriteBatch.Draw(EightBall, new Rectangle(x, y, diameter, diameter), Color.Black);
            }
        }
        public static void DrawOuterPockets(SpriteBatch spriteBatch, Texture2D TableOuter, Texture2D CueBall)
        {
            int Width = 64;
            int PocketDiam = 54;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (i == 1)
                    {
                        spriteBatch.Draw(CueBall, new Rectangle((ScreenWidth - PocketDiam) / 2, j * (ScreenHeight - 2 * BorderWidth) + BorderWidth - PocketDiam / 2, PocketDiam, PocketDiam), Color.LightGray);
                    }
                    else
                    {
                        spriteBatch.Draw(TableOuter, new Rectangle(i * ((ScreenWidth - Width) / 2), j * (ScreenHeight - Width), Width, Width), Color.LightGray);
                    }
                }
            }
        }
        public static void DrawScoreBox(SpriteBatch spriteBatch, List<Ball> Graveyard, Texture2D TableOuter, Texture2D EightBall)
        {
            spriteBatch.Draw(TableOuter, new Rectangle(0, ScreenHeight, ScreenWidth, 100), Color.DarkGray);
            spriteBatch.Draw(TableOuter, new Rectangle(ScreenWidth, 0, 200, ScreenHeight + 100), Color.DarkGray);
            DrawBalls(spriteBatch, Graveyard, EightBall);
        }
        /*public static void DrawPath(Ball a)
        {
            Vector2 MousePosition = Input.mousePosition;
            float LineLen = Vector2.Distance(a.Center, MousePosition);
            int LineWidth = 2;
            float Angle = 0f;
            Rectangle Line = new Rectangle((int)a.Center.X, (int)a.Center.Y, (int)LineLen, LineWidth);
            Rectangle RotatedLine = Vector2.Transform(Line, Angle);   
        }*/
        /**public static void DrawDiagonalLine_JOSH_VERSION(SpriteBatch sb, DiagonalLine a)
        {
            DrawLine(sb, a.Start, a.End, a.Thickness, Color.Brown);
        }**/
        public static void DrawDiagonalLine(SpriteBatch spriteBatch, DiagonalLine a, Texture2D PoolCueTexture)
        {
            float m = Physics.Gradient(a.Start, a.End);
            int count = 0;
            if (m != double.PositiveInfinity && m != double.NegativeInfinity)
            {
                float c = Physics.YIntercept(a.Start, m);
                int prevY = (int)a.Start.Y;
                int prevX = (int)a.Start.X;
                //Console.WriteLine(Physics.TanAngle(a.Start, a.End));
                int start = 0;
                int end = 0;
                if (a.Start.X < a.End.X)
                {
                    start = (int)a.Start.X;
                    end = (int)a.End.X;
                }
                else if (a.Start.X > a.End.X)
                {
                    start = (int)a.End.X;
                    end = (int)a.Start.X;
                }
                if (a.Start.Y < a.End.Y)
                {

                    end -= 1;
                }
                //Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}", m, c, a.Start.X, a.Start.Y, a.End.X, a.End.Y, Physics.TanAngle(a.Start, a.End));
                //Console.WriteLine("{0}, {1}, {2}, {3}", MathHelper.ToDegrees((float)Physics.TanAngle(a.Start, a.End)), MathHelper.ToDegrees((float)Math.PI / 4), a.Start, a.End);
                for (int x = start; x <= end + 1; x++)
                {
                    count += 1;
                    int y = (int)Physics.LineEquation(x, m, c);
                    int newY = Math.Abs(y - prevY);
                    int newX = x - prevX;
                    Tuple<int, int> prevTuple = InnerDrawDiagonalLines(spriteBatch, a, PoolCueTexture, y, x, m, c, prevY, newY, prevX, newX, count);
                    //Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", y, x, m, c, prevY, newY, prevX, newX);
                    prevY = prevTuple.Item1;
                    prevX = prevTuple.Item2;
                }
            }
            /**else if (a.Start.Y != a.End.Y)
            {
                if (a.Start.Y < a.End.Y)
                {
                    spriteBatch.Draw(PoolCueTexture, new Rectangle((int)a.Start.X, (int)a.Start.Y, a.Thickness, (int)a.End.Y - (int)a.Start.Y), Color.LightGray);
                    Console.WriteLine("Here1");
                }
                else if (a.Start.Y > a.End.Y)
                {
                    Console.WriteLine("{0}, {1}, {2}, {3}", (int)a.End.X, (int)a.End.Y, a.Thickness, (int)a.Start.Y - (int)a.End.Y);
                    spriteBatch.Draw(PoolCueTexture, new Rectangle((int)a.End.X, (int)a.End.Y, a.Thickness, (int)a.Start.Y - (int)a.End.Y), Color.LightGray);
                }
            }**/
        }
        public static Tuple<int, int> InnerDrawDiagonalLines(SpriteBatch spriteBatch, DiagonalLine a, Texture2D PoolCueTexture, int y, int x, float m, float c, int prevY, int newY, int prevX, int newX, int count)
        {
            if (!a.Dotted || x % 5 == 0)
            {
                if (newY < 0)
                {
                    newY *= -1;
                }
                if (newX < 0)
                {
                    newX *= -1;
                }
                if (prevY != a.Start.Y && prevX != a.Start.X)
                {
                    if (Math.Abs(Physics.TanAngle(a.Start, a.End)) < Math.PI / 4) //Far left or far right
                    {
                        spriteBatch.Draw(PoolCueTexture, new Rectangle(x, prevY, a.Thickness, newY), Color.LightGray);
                        //Console.WriteLine("{0}, {1}, {2}, {3}, {4}", count, x, prevY, a.Thickness, newY);
                        //spriteBatch.Draw(PoolCueTexture, new Rectangle(100, 100, 5, 5), Color.Navy);
                    }
                    else //Top or bottom sections
                    {
                        spriteBatch.Draw(PoolCueTexture, new Rectangle(x, prevY, a.Thickness, newY), Color.LightGray);
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

        public static void DrawDiagonalLineBROKEN(SpriteBatch spriteBatch, DiagonalLine a, Texture2D PoolCueTexture)
        {
            float m = Physics.Gradient(a.Start, a.End);
            float c = Physics.YIntercept(a.Start, m);
            int startX = (int)a.Start.X;
            int endX = (int)a.End.X;
            if (a.Start.X > a.End.X)
            {
                startX = (int)a.End.X;
                endX = (int)a.Start.X;
            }
            //Console.WriteLine("{0}, {1}, {2}, {3}", startX, endX, a.Start.X, a.End.X);
            int point1 = startX;
            int point2 = endX;
            if (Physics.TanAngle(a.Start, a.End) < Math.PI / 4)
            {
                point1 = (int)Physics.LineEquation(startX, m, c);
                point2 = (int)Physics.LineEquation(endX, m, c);
            }
            for (int i = point1; i < point2; i++)
            {
                int y = (int)Physics.LineEquation(i, m, c);
                spriteBatch.Draw(PoolCueTexture, new Rectangle(i, y, 1, 1), Color.White);
            }
        }

        public static void DrawCushionDiagonals(SpriteBatch spriteBatch, List<DiagonalLine> DiagonalLines, Texture2D TableOuter)
        {
            foreach (DiagonalLine a in DiagonalLines)
            {
                Console.WriteLine("a, {0}, {1}", a.Start, a.End);
                DrawDiagonalLine(spriteBatch, a, TableOuter);
            }
        }
        public static void DrawPoolCue(SpriteBatch spriteBatch, DiagonalLine PoolCue, Texture2D PoolCueTexture)
        {
            DrawDiagonalLine(spriteBatch, PoolCue, PoolCueTexture);
            //DrawDiagonalLine_JOSH_VERSION(spriteBatch, PoolCue);
        }
        public static void DrawSightLine(SpriteBatch spriteBatch, DiagonalLine SightLine, Texture2D SightLineTexture)
        {
            DrawDiagonalLine(spriteBatch, SightLine, SightLineTexture);
            //DrawDiagonalLine_JOSH_VERSION(spriteBatch, SightLine);
        }

        /**public static void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end, int thickness, Color colour)
        {
            Vector2 v = (end - start);
            sb.Draw(Game1.SmallWhiteSquare, start, null, colour, (float)Math.Atan2(v.Y, v.X), new Vector2(0f, 0.5f), new Vector2(v.Length(), thickness), SpriteEffects.None, 0f); 
        }**/
        public static void DrawButton(ref Button button, SpriteBatch spriteBatch)
        {
            int border = 2;
            spriteBatch.Draw(button.Color, new Rectangle((int)button.Origin.X - border, (int)button.Origin.Y - border * 2, (int)button.Dimensions.X + border * 2, (int)button.Dimensions.Y + border * 2), Color.Blue);
            spriteBatch.DrawString(button.ButtonFont, button.Text, button.Origin, Color.White);
            //Console.WriteLine(button.Origin);
        }
        public static void DrawTextBox(ref TextBox textbox, SpriteBatch spriteBatch)
        {
            int border = 2;
            Color color;
            if (textbox.Pressed)
            {
                color = Color.Red;
            }
            else
            {
                color = Color.Black;
            }
            spriteBatch.Draw(textbox.Color, new Rectangle((int)textbox.Origin.X - border, (int)textbox.Origin.Y - border, (int)textbox.Dimensions.X + border * 2, (int)textbox.Dimensions.Y + border * 2), color);
            spriteBatch.Draw(textbox.Color, new Rectangle((int)textbox.Origin.X, (int)textbox.Origin.Y, (int)textbox.Dimensions.X, (int)textbox.Dimensions.Y), Color.White);
            DrawTextBoxLetters(textbox, spriteBatch);
        }
        public static void DrawBlinkingKeyLine(TextBox textbox, SpriteBatch spriteBatch)
        {
            int depth = 2;
            spriteBatch.Draw(textbox.Color, new Rectangle((int)textbox.Origin.X + depth + textbox.Pointer * ButtonFunctions.CharacterGap, (int)textbox.Origin.Y + depth, 1, (int)textbox.Dimensions.Y - 2 * depth), Color.Black);
        }
        public static void DrawTextBoxLetters(TextBox textbox, SpriteBatch spriteBatch)
        {
            for (int x = 0; x < textbox.Chars.Count(); x++)
            {
                spriteBatch.DrawString(textbox.TextFont, textbox.Chars[x], new Vector2(textbox.Origin.X + x * ButtonFunctions.CharacterGap + 1, textbox.Origin.Y), Color.Black);
            }
        }
    }
}