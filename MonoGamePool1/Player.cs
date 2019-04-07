using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePool1
{
    /// <summary>
    /// Player class used for player data and validation of names (and passwords / highscores later?)
    /// </summary>
    public class Player
    {
        public int ID;
        public string Name;
        public int Shots;
        public Color Colour;

        public Player(int id, string name)
        {
            ID = id;
            Name = name;
            Shots = 1;
            Colour = Color.Black;
        }

        public static bool ValidateName(string name)
        {
            bool Valid = true;
            if (name.Length < 11)
            {
                foreach (char a in name.ToCharArray())
                {
                    if (!(((a > 47 && a < 58) || (a > 64 && a < 123))))
                    {
                        Valid = false;
                        break;
                    }
                }
            }
            else
            {
                Valid = false;
            }
            return Valid;
        }
        public void SetName(string newName)
        {
            Name = newName;
        }

        public void SetColour(Color colour)
        {
            Colour = colour;
        }

        public void IncreaseShots(int num)
        {
            Shots += num;
        }
    }
}
