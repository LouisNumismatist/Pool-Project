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
    public class FileSaving
    {
        public static void WriteToFile(string filePath, List<Ball> BallsList)
        {
            List<string> TextList = new List<string> { };
            foreach (Ball a in BallsList)
            {
                TextList.Add(ObjectToString(a));
            }
            File.AppendAllLines(filePath, TextList);
        }

        public static string[] ReadFromFile(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

        /*public static void createFile(string filePath, string fileName)
        {

        }*/

        public static string ObjectToString(Ball a)
        {
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
            //Vector2 velocity = new Vector2((float)Convert.ToDecimal(props[4]), (float)Convert.ToDecimal(props[5]));
            //Vector2 acceleration = new Vector2((float)General.ToExponential(props[6]), (float)General.ToExponential(props[7]));
            //Console.WriteLine(props[8]);
            Color colour = Colours[props[4]];
            bool collision = Convert.ToBoolean(props[5]);
            int prevball = Convert.ToInt32(props[6]);
            return new Ball(id, center, radius, Vector2.Zero, Vector2.Zero, colour, collision, prevball);
        }

        public static string CurrentMoment()
        {
            DateTime current = DateTime.Now;
            return (current.Year + "-" + current.Month + "-" + current.Day + "-" + current.Hour + "-" + current.Minute + "-" + current.Second);
        }
    }
}
//Selection From File for higher marks (is GraveYard?)