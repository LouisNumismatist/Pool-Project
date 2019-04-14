using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MonoGamePool1
{
    public class HighScoreNameComparer : IComparer<HighScore>
    {
        public int Compare(HighScore h1, HighScore h2)
        {
            return StringComparer.CurrentCulture.Compare(h1.UserName, h2.UserName);
        }
    }

    public class HighScoreBallsPottedComparer : IComparer<HighScore>
    {
        public int Compare(HighScore h1, HighScore h2)
        {
            if (h1.BallsPotted < h2.BallsPotted)
            {
                return -1;
            }
            else if (h1.BallsPotted > h2.BallsPotted)
            {
                return 1;
            }
            return 0;
        }
    }

    public class HighScoreTimeComparer : IComparer<HighScore>
    {
        public int Compare(HighScore h1, HighScore h2)
        {
            return StringComparer.CurrentCulture.Compare(h1.Time, h2.Time);
        }
    }

    public class HighScore
    {
        public string UserName;
        public int BallsPotted;
        public string Time;

        public HighScore(string username, int ballsPotted, string time)
        {
            UserName = username;
            BallsPotted = ballsPotted;
            Time = time;
        }
    }

    public class HighScoresSorting : HighScores
    {
        public static List<HighScore> GetHighScores()
        {            
            try //Doesn't stop program if save file cannot be found
            {
                string path = @"C:\Users\Louis\source\repos\MonoGamePool1\MonoGamePool1\FullHighScores.txt";
                string[] items = File.ReadAllLines(path);
                foreach (string item in items)
                {
                    Console.WriteLine(item);
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

        public static void ReverseList()
        {
            if (HighScoresList.Count() > 0)
            {
                List<HighScore> reversed = new List<HighScore>();
                Stack<HighScore> tempstack = new Stack<HighScore>(new HighScore[HighScoresList.Count()], HighScoresList.Count());
                foreach (HighScore hs in HighScoresList)
                {
                    tempstack.Push(hs);
                }
                for (int x = 0; x < HighScoresList.Count(); x++)
                {
                    reversed.Add(tempstack.Pop());
                }
                HighScoresList = reversed;
            }
        }
    }
}
