using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePool1
{
    public static class GamePlay
    {
        public static Ball FirstBallHit;
        public static List<Ball> BallsPotted;
        public static bool InTurn;

        static GamePlay()
        {
            BallsPotted = new List<Ball>();
        }

        public static void Foul(ref List<Player> Players, int playerTurn) //Potted inner
        {
            Players[playerTurn].Shots = 0;
            Players[1 - playerTurn].Shots = 2;
        }

        public static bool Potted(ref List<Player> Players, int PlayerTurn, Ball ball, ref bool EndGame)
        {
            if (!InTurn) return false;

            Console.WriteLine(Players[PlayerTurn].Colour.ToString());
            if (ball.Colour == Color.Black) //End Game if 8 ball potted
            {
                EndGame = true;

                Environment.Exit(0);

                return false;
            }
            else if (Players[PlayerTurn].Colour == Color.Black) //Add turn to current player if own ball potted
            {
                Console.WriteLine(Players[PlayerTurn].Colour.ToString());
                Players[PlayerTurn].SetColour(ball.Colour);
                if (ball.Colour == Color.Yellow)
                {
                    Players[1 - PlayerTurn].SetColour(Color.Red);
                }
                else
                {
                    Players[1 - PlayerTurn].SetColour(Color.Yellow);
                }
                return true;
            }
            else //Forfeit turn if other player's ball or cue ball potted
            {
                Foul(ref Players, PlayerTurn);
                return false;
            }
        }

        public static void EndTurn(ref List<Player> Players, ref int PlayerTurn, ref List<Ball> BallsList)
        {
            Player current = Players[PlayerTurn];
            bool doneFoul = false;
            bool anyMyColourLeft = false;
            for (int i = 0; i < BallsList.Count; i++)
            {
                if (BallsList[i].Colour == current.Colour)
                {
                    anyMyColourLeft = true;
                    break;
                }
            }
            for (int i = 0; i < BallsPotted.Count; i++)
            {
                if (BallsPotted[i].Colour == current.Colour)
                {
                    anyMyColourLeft = true;
                    break;
                }
            }

            if (FirstBallHit == null)
            {
                //we didn't hit a ball, foul
                Foul(ref Players, PlayerTurn);
                doneFoul = true;
            }
            else
            {
                if (current.Colour != Color.Black)
                {
                    if ((anyMyColourLeft && FirstBallHit.Colour != current.Colour) || (!anyMyColourLeft && FirstBallHit.Colour != Color.Black))
                    {
                        Foul(ref Players, PlayerTurn);
                        doneFoul = true;
                    }

                    foreach (Ball ball in BallsPotted)
                    {
                        if (ball.Colour != current.Colour)
                        {
                            Foul(ref Players, PlayerTurn);
                            doneFoul = true;
                        }
                    }
                }
            }

            current.Shots--;
            if (doneFoul)
            {
                current.Shots = 0;
            }
            else
            {
                if (BallsPotted.Count > 0)
                {
                    current.Shots = 1;
                    
                }
            }
            if (current.Shots <= 0)
            {
                current.Shots = 0;
                PlayerTurn = 1 - PlayerTurn;
                if (!doneFoul)
                {
                    Players[PlayerTurn].Shots = 1;
                }
            }

            FirstBallHit = null;
            BallsPotted.Clear();
        }

        public static void PlaceCueBall(ref Ball CueBall, Vector2 mousePosition, ref bool PlacingCueBall)
        {
            if (mousePosition.Y > Game1.BorderWidth + CueBall.Radius && mousePosition.Y < Game1.ScreenHeight - Game1.BorderWidth - CueBall.Radius)
            {
                CueBall.Center.Y = mousePosition.Y;
            }
            if (Input.LeftMouseJustClicked())
            {
                PlacingCueBall = false;
            }
        }
    }
}
