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

        public HighScore(string username, int ballspotted, string time)
        {
            UserName = username;
            BallsPotted = ballspotted;
            Time = time;
        }
    }

    public class HighScoresSorting
    {
        public static int lastSort = 0;
        public static bool asc = true;

        //public static List<HighScore> HighScoresList = new List<HighScore>(0);


        public static List<HighScore> GetHighScores()
        {
            List<HighScore> HighScoresList = new List<HighScore>();
            try
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

        public static void ReverseList(ref List<HighScore> HighScoresList)
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
            //return HighScoresList;
        }

        public static List<HighScore> IdentifyCommand(int type, bool direction, List<HighScore> HighScoresList)
        {
            if (!(type == lastSort && direction == asc)) //Same button not repeated
            {
                if (type == lastSort && direction != asc) //Reverse current sorted list
                {
                    ReverseList(ref HighScoresList);
                }
                else if (type != lastSort) //if new list to be sorted
                {
                    if (type == 0)
                    {
                        //Sort UserNames
                    }
                    else if (type == 1)
                    {
                        //Sort BallsPotted
                    }
                    else
                    {
                        //Sort TimeTaken
                    }

                    if (!asc)
                    {
                        ReverseList(ref HighScoresList);
                    }
                }
            }
            lastSort = type;
            asc = direction;

            return HighScoresList;
        }
    }
}
