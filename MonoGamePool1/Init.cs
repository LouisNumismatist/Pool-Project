using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;



namespace MonoGamePool1
{
    public static class Init
    {
        static int BorderWidth = Game1.BorderWidth;
        static int ScreenHeight = Game1.ScreenHeight;
        static int ScreenWidth = Game1.ScreenWidth;
        static int BallDiam = Game1.BallDiam;
        static int BallRad = BallDiam / 2;

        public static void InitialiseBalls(ref List<Ball> BallsList, Texture2D CueBall, Texture2D YellowBall, Texture2D RedBall, Texture2D EightBall)
        {
            int Red = (Physics.CountTotal(Debug.rows) - 1) / 2;
            int Yel = (Physics.CountTotal(Debug.rows) - 1) / 2;
            int ID = -1;
            Texture2D color;

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
                        color = EightBall;
                    }
                    else
                    {
                        if (((random.Next(2) == 1) && (Red > 0)) || (Yel == 0))
                        {
                            color = RedBall;
                            Red -= 1;
                        }
                        else
                        {
                            color = YellowBall;
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
            BallsList.Add(new Ball(15,CuePos, 11, Vector2.Zero, new Vector2(-0.008f, -0.008f), CueBall, false, 15));
            //BallsList.Insert(0, new Ball(15, new Vector2(274, 274), 11, Vector2.Zero, new Vector2(-0.008f, -0.008f), CueBall, false, 15));
        }
        public static void InitialisePockets(ref List<Pocket> PocketList)
        {
            int ID = -1;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    ID += 1;
                    PocketList.Add(new Pocket(ID, new Vector2(BorderWidth + j * 483, BorderWidth + i * 468), 22));
                }
            }
        }
        public static void InitialiseOuterPockets(ref List<Rectangle> OuterPockets)
        {
            int ID = -1;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    ID += 1;
                    OuterPockets.Add(new Rectangle(i, j, 40, 40));
                }
            }
        }
        public static void InitialiseDiagonalLines(ref List<DiagonalLine> DiagonalLines, Texture2D TableOuter)
        {
            DiagonalLines.Add(new DiagonalLine(0, 3, new Vector2(100), new Vector2(200, 50), TableOuter, false));
        }
        public static DiagonalLine InitialisePoolCue(Texture2D PoolCueTexture)
        {
            return new DiagonalLine(0, 5, new Vector2(174, 274), new Vector2(274, 274), PoolCueTexture, false); // - (5 / 2)
        }
        public static DiagonalLine InitialiseSightLine(Texture2D SightLineTexture)
        {
            return new DiagonalLine(0, 3, new Vector2(174, 274), new Vector2(ScreenWidth, 274), SightLineTexture, true);
        }
        public static Button InitialiseButton(Vector2 Origin, String text, Texture2D ButtonTexture, SpriteFont font)
        {
            return new Button(Origin, new Vector2(1 + 11 * text.Length, 20), text, ButtonTexture, font, false);
        }
        public static void InitialiseButtonList(ref List<Button> ButtonList)
        {

        }
        public static TextBox InitialiseTextBox(Vector2 Origin, int Width, Texture2D TextBoxTexture, SpriteFont font)
        {
            return new TextBox(Origin, new Vector2(Width, 20), new List<string>(), 0, TextBoxTexture, font, false, 0);
        }
        /**public static StackTextBox InitialiseStackTextBox(Vector2 Origin, int Width, Texture2D TextBoxTexture, SpriteFont font)
        {
            return new TextBox(Origin, new Vector2(Width, 20), new Stack<string>(new List<string>(), 0), TextBoxTexture, font, false, 0);
        }**/
    }
}
