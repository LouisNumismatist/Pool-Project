using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePool1
{
    public static class Init
    {
        static readonly int BorderWidth = Game1.BorderWidth;
        static readonly int ScreenHeight = Game1.ScreenHeight;
        static readonly int ScreenWidth = Game1.ScreenWidth;
        static readonly int BallDiam = Game1.BallDiam;
        static readonly int BallRad = BallDiam / 2;

        public static void InitialiseBalls(ref List<Ball> BallsList, Texture2D texture)
        {
            int Red = (Physics.CountTotal(Debug.rows) - 1) / 2;
            int Yel = (Physics.CountTotal(Debug.rows) - 1) / 2;
            int ID = -1;
            Color color;

            Vector2 CuePos = new Vector2((ScreenWidth - 2 * BorderWidth) / 4 + BorderWidth, ScreenHeight / 2);
            Vector2 EightPos = new Vector2((ScreenWidth - 2 * BorderWidth) / 4 * 3 + BorderWidth, ScreenHeight / 2);
            //Console.WriteLine("\nPOSITIONS: [{0}, {1}], [{2}, {3}]\n", CuePos.X, CuePos.Y, EightPos.X, EightPos.Y);

            Random random = new Random();
            for (int i = 0; i < Debug.rows; i++)
            {
                for (int j = 0; j < Debug.rows - i; j++)
                {
                    Vector2 position = new Vector2(EightPos.X + (i - 1) * (BallDiam - 2) + (j - 1) * (BallDiam - 2), EightPos.Y + (-BallRad - 1) * j + i * (BallRad + 1));
                    if (i == 1 && j == 1)
                    {
                        color = Color.Black;
                    }
                    else
                    {
                        if (((random.Next(2) == 1) && (Red > 0)) || (Yel == 0))
                        {
                            color = Color.Red;
                            Red -= 1;
                        }
                        else
                        {
                            color = Color.Yellow;
                            Yel -= 1;
                        }
                    }
                    ID += 1;
                    if (ID == 15)
                    {
                        ID += 1;
                    }
                    BallsList.Add(new Ball(ID, position, 11, Vector2.Zero, Vector2.Zero, color, false, ID));
                }
            }
            BallsList.Add(new Ball(15, CuePos, 11, Vector2.Zero, new Vector2(-0.008f, -0.008f), Color.White, false, 15));
            //BallsList.Insert(0, new Ball(15, new Vector2(274, 274), 11, Vector2.Zero, new Vector2(-0.008f, -0.008f), CueBall, false, 15));
        }

        public static void InitialisePockets(ref List<Pocket> PocketList, int radius, Color colour)
        {
            int ID = -1;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    ID += 1;
                    PocketList.Add(new Pocket(ID, new Vector2(BorderWidth + j * ((ScreenWidth - 80) / 2), BorderWidth + i * 468), radius, colour));
                }
            }
        }
        public static void InitialiseButtonList(ref List<Button> ButtonList)
        {

        }

        /*public static void InitialiseOuterPockets(ref List<Pocket> OuterPockets)
        {
            //int radius = 26;
            int ID = -1;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    ID += 1;
                    OuterPockets.Add(new Pocket(ID, new Vector2(BorderWidth + j * ((ScreenWidth - 80) / 2), BorderWidth + i * 468), 26, Color.LightGray));
                }
            }
        }*/

        /*public static DiagonalLine InitialiseDiagonalLine(Color color)
        {
            return new DiagonalLine(0, 3, new Vector2(100), new Vector2(200, 50), color, false);
        }*/

        /*public static DiagonalLine InitialisePoolCue()
        {
            return new DiagonalLine(0, 5, new Vector2(174, 274), new Vector2(274, 274), Color.Sienna, false); // - (5 / 2)
        }*/

        /*public static DiagonalLine InitialiseSightLine()
        {
            return new DiagonalLine(0, 3, new Vector2(174, 274), new Vector2(ScreenWidth, 274), Color.DarkOrange, true);
        }*/

        /*public static Button InitialiseButton(Vector2 Origin, String text, SpriteFont font)
        {
            return new Button(Origin, new Vector2(1 + 11 * text.Length, 20), text, Color.Blue, font, false, 2);
        }*/

        /*public static RegTextBox InitialiseTextBox(Vector2 Origin, SpriteFont font, int maxchars, string temptext)
        {
            return new RegTextBox(Origin, new Vector2((maxchars + 0.4f) * LetterWidth, LetterHeight), new List<string>(), 0, Color.Red, font, false, 0, maxchars, 2, temptext);
        }*/

        /*public static StackTextBox InitialiseStackTextBox(Vector2 Origin, int Width, SpriteFont font, string text)
        {
            return new StackTextBox(Origin, new Vector2(Width, 20), new Stack<string>(0, new List<string>()), Color.Red, font, false, 0, Width, 2, text);
        }*/

        /*public static Player InitialisePlayer(int id, string name)
        {
            return new Player(id, name, 1, Color.White);
        }*/

        /*public static MiniGraph InitialiseMiniGraph(Vector2 Origin, int width, int height)
        {
            return new MiniGraph(Origin, new Vector2(width, height), new Queue<float>(width));
        }*/

        /*public static NumBox InitialiseNumBox(Vector2 origin, SpriteFont font, int max)
        {
            int digits = (int)Math.Log10(max + 1) + 3;
            int border = 2;
            Console.WriteLine(digits);
            Button left = new Button(origin, "<", Color.Red, font);
            Button right = new Button(new Vector2(origin.X + digits * left.LetterWidth - left.LetterWidth + 4 * border, origin.Y), ">", Color.Green, font);
            return new NumBox(origin, new Vector2(digits * left.LetterWidth, left.LetterHeight), Color.Blue, font, max, left, right, border);
        }*/
    }
}
