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
        /**public static void createFile(string filePath, string fileName)
        {

        }**/
        public static string ObjectToString(Ball a)
        {
            return a.ID + "|" + a.Center.X + "|" + a.Center.Y + "|" + a.Radius + "|" + a.Velocity.X + "|" + a.Velocity.Y + "|" + a.Acceleration.X + "|" + a.Acceleration.Y + "|" + a.Color + "|" + a.Collision + "|" + a.PrevBall;
        }
        public static Ball StringToObject(string text, Dictionary<string, Texture2D> Dict)
        {
            string[] props = text.Split('|');
            int id = Convert.ToInt32(props[0]);
            Vector2 center = new Vector2((float)Convert.ToDecimal(props[1]), (float)Convert.ToDecimal(props[2]));
            int radius = Convert.ToInt32(props[3]);
            Vector2 velocity = new Vector2((float)Convert.ToDecimal(props[4]), (float)Convert.ToDecimal(props[5]));
            Vector2 acceleration = new Vector2((float)Convert.ToDecimal(props[6]), (float)Convert.ToDecimal(props[7]));
            Texture2D color = Dict[props[8]];
            Console.WriteLine(props[8]);
            bool collision = Convert.ToBoolean(props[9]);
            int prevball = Convert.ToInt32(props[10]);
            return new Ball(id, center, radius, velocity, acceleration, color, collision, prevball);
        }
        public static string CurrentMoment()
        {
            string text = "";
            DateTime current = DateTime.Now;
            text += current.Year;
            text += "-";
            text += current.Month;
            text += "-";
            text += current.Day;
            text += "-";
            text += current.Hour;
            text += "-";
            text += current.Minute;
            text += "-";
            text += current.Second;
            return text;
        }
    }
}
