using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace MonoGamePool1
{
    /// <summary>
    /// Class used for data saving or retrieval from one or more files
    /// Used for saving and retrieving the state of a game
    /// </summary>
    public class FileSaving
    {
        public static void WriteToFile(string filePath, List<Ball> BallsList)
        {
            //Writes text to a file
            List<string> TextList = new List<string> { };
            foreach (Ball a in BallsList)
            {
                TextList.Add(ObjectToString(a));
            }
            File.AppendAllLines(filePath, TextList);
        }

        public static string[] ReadFromFile(string filePath)
        {
            //Gets text from file
            return File.ReadAllLines(filePath);
        }

        public static string ObjectToString(Ball a)
        {
            //Converts a ball object into a string
            Dictionary<Color, string> Colours = new Dictionary<Color, string>()
            {
                { Color.Red, "Red" },
                { Color.Yellow, "Yellow" },
                { Color.White, "White" },
                { Color.Black, "Black" }
            };

            return a.ID + "|" + a.Center.X + "|" + a.Center.Y + "|" + a.Radius + "|" + Colours[a.Colour] + "|" + a.Collision + "|" + a.PrevBall;
        }

        public static Ball StringToObject(string text)
        {
            //Converts a string into a ball object
            Dictionary<string, Color> Colours = new Dictionary<string, Color>()
            {
                { "Red", Color.Red },
                { "Yellow", Color.Yellow },
                { "White", Color.White },
                { "Black", Color.Black }
            };

            string[] props = text.Split('|');
            int id = Convert.ToInt32(props[0]);
            Vector2 center = new Vector2((float)Convert.ToDecimal(props[1]), (float)Convert.ToDecimal(props[2]));
            int radius = Convert.ToInt32(props[3]);
            Color colour = Colours[props[4]];
            bool collision = Convert.ToBoolean(props[5]);
            int prevball = Convert.ToInt32(props[6]);
            return new Ball(id, center, radius, Vector2.Zero, Vector2.Zero, colour, collision, prevball);
        }

        public static string CurrentMoment()
        {
            //Turns the current moment to a string
            DateTime current = DateTime.Now;
            return (current.Year + "-" + current.Month + "-" + current.Day + "-" + current.Hour + "-" + current.Minute + "-" + current.Second);
        }
    }
}
