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
            if (ball.Colour == Color.Black) //End Game if 8 ball potted
            {
                EndGame = true;
                return false;
            }
            else if (ball.Colour == Players[PlayerTurn].Colour) //Add turn to current player if own ball potted
            {
                if (Players[PlayerTurn].Shots < 2)
                {
                    Players[PlayerTurn].Shots += 1;
                }
                return true;
            }
            else //Forfeit turn if other player's ball or cue ball potted
            {
                BadPot(ref Players, PlayerTurn);
                return false;
            }
        }

    }
}
