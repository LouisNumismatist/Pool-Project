using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MonoGamePool1
{
    class HighScore
    {
        string UserName;
        int BallsPotted;
        string Time;

        public HighScore(string username, int ballspotted, string time)
        {
            UserName = username;
            BallsPotted = ballspotted;
            Time = time;
        }
    }

    class HighScoresSorting
    {
        public static List<HighScore> GetHighScores()
        {
            List<HighScore> HighScoresList = new List<HighScore>();
            try
            {
                string path = @"C:\Users\Louis\source\repos\MonoGamePool1\MonoGamePool1\FullHighScores.txt";
                string[] items = File.ReadAllLines(path);
                foreach (string item in items)
                {
                    string[] parts = item.Split(("|".ToCharArray())[0]);
                    HighScoresList.Add(new HighScore(parts[0], Convert.ToInt32(parts[1]), parts[2]));
                }
            }
            catch
            {
                Console.WriteLine("ERROR: File not found.");
            }
            return HighScoresList;
        }
    }
}
