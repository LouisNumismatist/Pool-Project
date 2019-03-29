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
        static readonly int BorderWidth = Game1.BorderWidth;
        static readonly int ScreenHeight = Game1.ScreenHeight;
        static readonly int ScreenWidth = Game1.ScreenWidth;
        static readonly int BallDiam = Game1.BallDiam;

        public static Texture2D BlankBox = Game1.BlankBox;
        public static Texture2D PixelBox;
        public static Texture2D BlankCircle = Game1.BlankCircle;

        public static void DrawBoard(SpriteBatch spriteBatch)
        {
            int BoxWidth = ScreenWidth - 2 * BorderWidth;
            int BoxHeight = ScreenHeight - 2 * BorderWidth;
            int ThinBorder = 4;
            //Outer and Inner Borders
            spriteBatch.Draw(BlankBox, new Rectangle(BorderWidth - ThinBorder, BorderWidth - ThinBorder, BoxWidth + 2 * ThinBorder, BoxHeight + 2 * ThinBorder), Color.LightGray);
            spriteBatch.Draw(BlankBox, new Rectangle(BorderWidth, BorderWidth, ScreenWidth - 2 * BorderWidth, ScreenHeight - 2 * BorderWidth), Color.ForestGreen);
        }

        public static void DrawBalls(SpriteBatch spriteBatch, List<Ball> BallsList)
        {
            foreach (Ball a in BallsList)
            {
                if (a.ID == 15)
                {
                    if (Game1.HittingCueBall && Debug.canPingBall)
                    {
                        new Pocket(0, a.Center, a.Radius + Debug.ballBorderWidth, Color.Red).Draw(spriteBatch);
                        //Draws red ball behind ball when held
                    }
                }
                a.Draw(spriteBatch);
                Debug.NumberBalls(a, spriteBatch);
                //Draw numbers on balls if enabled
            }
        }

        public static void DrawPockets(SpriteBatch spriteBatch, List<Pocket> PocketList)
        {
            foreach (Pocket p in PocketList)
            {
                p.Draw(spriteBatch);
            }
        }

        /*public static void DrawOuterPockets(SpriteBatch spriteBatch)
        {
            int Width = 64;
            int PocketDiam = 54;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (i == 1)
                    {
                        spriteBatch.Draw(BlankCircle, new Rectangle((ScreenWidth - PocketDiam) / 2, j * (ScreenHeight - 2 * BorderWidth) + BorderWidth - PocketDiam / 2, PocketDiam, PocketDiam), Color.LightGray);
                    }
                    else
                    {
                        spriteBatch.Draw(BlankBox, new Rectangle(i * ((ScreenWidth - Width) / 2), j * (ScreenHeight - Width), Width, Width), Color.LightGray);
                    }
                }
            }
            
        }*/

        public static void DrawScoreBox(SpriteBatch spriteBatch, List<Ball> Graveyard)
        {
            spriteBatch.Draw(BlankBox, new Rectangle(0, ScreenHeight, ScreenWidth, 100), Color.DarkGray);
            spriteBatch.Draw(BlankBox, new Rectangle(ScreenWidth, 0, 400, ScreenHeight + 100), Color.DarkGray);
            DrawBalls(spriteBatch, Graveyard);
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

        /*public static void DrawDiagonalLine_JOSH_VERSION(SpriteBatch sb, DiagonalLine a)
        {
            DrawLine(sb, a.Start, a.End, a.Thickness, Color.Brown);
        }*/

        /*public static void DrawDiagonalLine(SpriteBatch spriteBatch, DiagonalLine a)
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
                    Tuple<int, int> prevTuple = InnerDrawDiagonalLines(spriteBatch, a, y, x, m, c, prevY, newY, prevX, newX, count);
                    //Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", y, x, m, c, prevY, newY, prevX, newX);
                    prevY = prevTuple.Item1;
                    prevX = prevTuple.Item2;
                }
            }
            /*else if (a.Start.Y != a.End.Y)
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
            }
        }*/

        /*public static Tuple<int, int> InnerDrawDiagonalLines(SpriteBatch spriteBatch, DiagonalLine a, int y, int x, float m, float c, int prevY, int newY, int prevX, int newX, int count)
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
                        spriteBatch.Draw(BlankBox, new Rectangle(x, prevY, a.Thickness, newY), a.Colour);
                        //Console.WriteLine("{0}, {1}, {2}, {3}, {4}", count, x, prevY, a.Thickness, newY);
                        //spriteBatch.Draw(PoolCueTexture, new Rectangle(100, 100, 5, 5), Color.Navy);
                    }
                    else //Top or bottom sections
                    {
                        spriteBatch.Draw(BlankBox, new Rectangle(x, prevY, a.Thickness, newY), a.Colour);
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
        }*/

        /*public static void DrawDiagonalLineBROKEN(SpriteBatch spriteBatch, DiagonalLine a)
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
                spriteBatch.Draw(BlankBox, new Rectangle(i, y, 1, 1), Color.White);
            }
        }*/

        /*public static void DrawCushionDiagonals(SpriteBatch spriteBatch, List<DiagonalLine> DiagonalLines)
        {
            foreach (DiagonalLine a in DiagonalLines)
            {
                Console.WriteLine("a, {0}, {1}", a.Start, a.End);
                a.Draw(spriteBatch);
            }
        }*/

        /*public static void DrawPoolCue(SpriteBatch spriteBatch, DiagonalLine PoolCue)
        {
            PoolCue.Draw(spriteBatch);
            //DrawDiagonalLine_JOSH_VERSION(spriteBatch, PoolCue);
        }*/

        /*public static void DrawSightLine(SpriteBatch spriteBatch, DiagonalLine SightLine)
        {
            SightLine.Draw(spriteBatch);
            //DrawDiagonalLine_JOSH_VERSION(spriteBatch, SightLine);
        }*/

        /*public static void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end, int thickness, Color colour, bool dotted)
        {
            //Get the vector between the start and the end. (from the start point)
            Vector2 vectorBetween = end - start;
            //Get the angle of the vector.
            float angleVector = (float)Math.Atan2(vectorBetween.Y, vectorBetween.X);
            //The point on the blank square to draw from.
            Vector2 origin = new Vector2(0f, 0.5f);

            sb.Draw(Game1.PixelBox, start, null, colour, angleVector, origin, new Vector2(vectorBetween.Length(), thickness), SpriteEffects.None, 0f);

            //Draw the line.
            if (!dotted)
            {
                sb.Draw(Game1.PixelBox, start, null, colour, angleVector, origin, new Vector2(vectorBetween.Length(), thickness), SpriteEffects.None, 0f);
            }
            else
            {
                float m = Physics.Gradient(start, end);
                float c = Physics.YIntercept(start, m);
                for (int x = (int)start.X; x < (int)end.X; x++)
                {
                    float y = Physics.LineEquation(x, m, c);
                    sb.Draw(Game1.PixelBox, new Vector2(x, y), null, colour, angleVector, origin, new Vector2(vectorBetween.Length() / 20, thickness), SpriteEffects.None, 0f);
                }
            }

        }*/

        /*public static void DrawButton(ref Button button, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BlankBox, new Rectangle((int)button.Origin.X - button.Border, (int)button.Origin.Y - button.Border * 2, (int)button.Dimensions.X + button.Border * 2, (int)button.Dimensions.Y + button.Border * 2), button.Colour);
            spriteBatch.DrawString(button.Font, button.Text, button.Origin, Color.White);
            //Console.WriteLine(button.Origin);
        }*/

        /*public static void DrawTextBox(ref RegTextBox textbox, SpriteBatch spriteBatch)
        {
            Color color;
            if (textbox.Pressed)
            {
                color = textbox.Colour;
            }
            else
            {
                color = Color.Black;
            }
            spriteBatch.Draw(BlankBox, new Rectangle((int)textbox.Origin.X - textbox.Border, (int)textbox.Origin.Y - textbox.Border, (int)textbox.Dimensions.X + textbox.Border * 2, (int)textbox.Dimensions.Y + textbox.Border * 2), color);
            spriteBatch.Draw(BlankBox, new Rectangle((int)textbox.Origin.X, (int)textbox.Origin.Y, (int)textbox.Dimensions.X, (int)textbox.Dimensions.Y), Color.White);
            if (textbox.Chars.Count > 0 | textbox.Pressed)
            {
                DrawTextBoxLetters(textbox, spriteBatch);
            }
            else
            {
                spriteBatch.DrawString(textbox.Font, textbox.TempText, textbox.Origin, Color.Gray);
            }
        }*/

        /*public static void DrawBlinkingKeyLine(RegTextBox textbox, SpriteBatch spriteBatch)
        {
            int depth = 2;
            spriteBatch.Draw(BlankBox, new Rectangle((int)textbox.Origin.X + depth + textbox.Pointer * LetterWidth, (int)textbox.Origin.Y + depth, 1, (int)textbox.Dimensions.Y - 2 * depth), Color.Black);
        }*/

        /*public static void DrawTextBoxLetters(RegTextBox textbox, SpriteBatch spriteBatch)
        {
            if (textbox.Chars.Count() <= textbox.MaxChars)
            {
                for (int x = 0; x < textbox.Chars.Count(); x++)
                {
                    spriteBatch.DrawString(textbox.Font, textbox.Chars[x], new Vector2(textbox.Origin.X + x * LetterWidth + 1, textbox.Origin.Y), Color.Black);
                }
            }
            else
            {
                for (int x = 0; x < textbox.MaxChars; x++)
                {
                    int y = x + textbox.Chars.Count() - textbox.MaxChars;
                    spriteBatch.DrawString(textbox.Font, textbox.Chars[y], new Vector2(textbox.Origin.X + x * LetterWidth + 1, textbox.Origin.Y), Color.Black);
                }
            }
        }*/

        /*public static void DrawMiniGraph(MiniGraph mg, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BlankBox, new Rectangle((int)mg.Origin.X, (int)mg.Origin.Y, (int)mg.Dimensions.X, (int)mg.Dimensions.Y), Color.Black); //Background
            spriteBatch.Draw(BlankBox, new Rectangle((int)(mg.Origin.X + 0.25 * mg.Dimensions.X), (int)mg.Origin.Y, 1, (int)mg.Dimensions.Y), Color.White); //y-axis
            spriteBatch.Draw(BlankBox, new Rectangle((int)(mg.Origin.X), (int)(mg.Origin.Y + mg.Dimensions.Y - 5), (int)(mg.Dimensions.X), 1), Color.White); //x-axis
            spriteBatch.DrawString(Game1.TextBoxFont, ((int)(mg.Max + 0.5f)).ToString(), new Vector2(mg.Origin.X, mg.Origin.Y - (LetterHeight + 2)), Color.White);
            spriteBatch.DrawString(Game1.TextBoxFont, "0", new Vector2(mg.Origin.X, mg.Origin.Y + mg.Dimensions.Y + 2), Color.White);
            //Console.WriteLine(mg.Values.Peek(mg.Values));
            float[] items = mg.Values.GetContents();
            for (int x = 0; x < mg.Values.GetTail(); x++)
            {
                spriteBatch.Draw(BlankBox, new Rectangle((int)mg.Origin.X + x, (int)(((mg.Origin.Y + mg.Dimensions.Y) - (items[x] / mg.Max) * mg.Dimensions.Y)), 1, 1), Color.Red);
            }
        }*/

        /*public static void DrawNumBox(NumBox nb, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(BlankBox, new Rectangle((int)nb.Origin.X - BorderWidth, (int)nb.Origin.Y - BorderWidth, (int)nb.Dimensions.X + 2 * BorderWidth, (int)nb.Dimensions.Y + 2 * BorderWidth), Color.Black); //Border
            DrawButton(ref nb.Left, spriteBatch);
            spriteBatch.Draw(BlankBox, new Rectangle((int)nb.Origin.X + LetterWidth + nb.Border, (int)nb.Origin.Y - nb.Border * 2, (int)nb.Dimensions.X + 2 * (nb.Border - LetterWidth), LetterHeight + 2 * nb.Border), nb.Colour); //Middle TextBox
            spriteBatch.DrawString(nb.Font, Debug.rows.ToString(), new Vector2((int)nb.Origin.X + LetterWidth + 3 * nb.Border, (int)nb.Origin.Y - nb.Border), Color.White); //Text (number)
            DrawButton(ref nb.Right, spriteBatch);
        }*/

        /*public static void DrawPlayerNames(SpriteBatch spriteBatch,  SpriteFont font, Vector2 pos1, Vector2 pos2, string name1, string name2)
        {
            spriteBatch.DrawString(font, name1, pos1, Color.Black);
            spriteBatch.DrawString(font, name2, pos2, Color.Black);
        }*/


    }
}