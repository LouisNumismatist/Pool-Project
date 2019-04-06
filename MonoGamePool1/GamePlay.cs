using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePool1
{
    public class GamePlay
    {
        public static void BadPot(ref List<Player> Players, int id) //Potted inner
        {
            for (int index = 0; index < Players.Count; index++)
            {
                if (index == id)
                {
                    Players[index].Shots = 0;
                }
                else if (Players[index].Shots < 2)
                {
                    Players[index].Shots += 1;
                }
            }
        }

        public static bool Potted(ref List<Player> Players, int PlayerTurn, Ball ball, ref bool EndGame)
        {
            Console.WriteLine(Players[PlayerTurn].Colour.ToString());
            if (ball.Colour == Color.Black) //End Game if 8 ball potted
            {
                EndGame = true;
                return false;
            }
            else if (ball.Colour == Players[PlayerTurn].Colour | Players[PlayerTurn].Colour == Color.Black) //Add turn to current player if own ball potted
            {
                Console.WriteLine(Players[PlayerTurn].Colour.ToString());
                if (Players[PlayerTurn].Shots < 2)
                {
                    Players[PlayerTurn].Shots += 1;
                }
                if (Players[PlayerTurn].Colour == Color.Black)
                {
                    Players[PlayerTurn].SetColour(ball.Colour);
                    if (ball.Colour == Color.Yellow)
                    {
                        Players[1 - PlayerTurn].SetColour(Color.Red);
                    }
                    else
                    {
                        Players[1 - PlayerTurn].SetColour(Color.Yellow);
                    }
                }
                return true;
            }
            else //Forfeit turn if other player's ball or cue ball potted
            {
                BadPot(ref Players, PlayerTurn);
                return false;
            }
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
