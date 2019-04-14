using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MonoGamePool1
{
    /// <summary>
    /// Used for drawing collections of items to the screen in a specific way
    /// </summary>
    public class Graphics : Game1
    {
        public static int ballBorderWidth = 2;

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
                    if (HittingCueBall && Debug.canPingBall)
                    {
                        new Pocket(0, a.Center, a.Radius + ballBorderWidth, Color.Red).Draw(spriteBatch);
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

        public static void DrawScoreBox(SpriteBatch spriteBatch, List<Ball> Graveyard)
        {
            spriteBatch.Draw(BlankBox, new Rectangle(0, ScreenHeight, ScreenWidth, 100), Color.DarkGray);
            spriteBatch.Draw(BlankBox, new Rectangle(ScreenWidth, 0, 400, ScreenHeight + 100), Color.DarkGray);
            DrawBalls(spriteBatch, Graveyard);
        }

    }
}