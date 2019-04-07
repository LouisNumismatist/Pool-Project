﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePool1
{
    /// <summary>
    /// Init Class is used to initialise any group of objects that cannot be initialised using their object constructors alone
    /// </summary>
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
            BallsList.Add(new Ball(5, EightPos, 11, Vector2.Zero, Vector2.Zero, Color.Black, false, 5));
            BallsList.Add(new Ball(7, EightPos, 11, Vector2.Zero, Vector2.Zero, Color.Black, false, 5));
            BallsList.Add(new Ball(15, CuePos, 11, Vector2.Zero, new Vector2(-0.008f, -0.008f), Color.White, false, 15));

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
    }
}
