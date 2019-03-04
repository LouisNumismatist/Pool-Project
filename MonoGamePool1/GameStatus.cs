using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGamePool1
{
    public class GameStatus
    {
        public static List<Vector2> Velocities = new List<Vector2>();


        public static void SaveGame(List<Ball> BallsList)
        {
            string filePath = @"C:\Users\Louis\source\repos\MonoGamePool1\MonoGamePool1\SaveFiles\";
            string moment = FileSaving.CurrentMoment();
            filePath += moment;
            if (!File.Exists(filePath))
            {
                Console.WriteLine(filePath);
                FileSaving.WriteToFile(filePath, BallsList);
            }
        }

        public static List<Ball> LoadGame(string filePath)
        {
            List<Ball> balls = new List<Ball>();
            string[] lines = FileSaving.ReadFromFile(filePath);
            foreach (string item in lines)
            {
                balls.Add(FileSaving.StringToObject(item));
            }
            return balls;
        }

        public static void ResetGame(ref List<Ball> BallsList, ref List<Ball> Graveyard, MiniGraph mg1, MiniGraph mg2, MiniGraph mg3, Texture2D circle, Texture2D box)
        {
            BallsList.Clear();
            Graveyard.Clear();
            Init.InitialiseBalls(ref BallsList, circle);
            new MiniGraph(mg1.Origin, (int)mg1.Dimensions.X, (int)mg1.Dimensions.Y, mg1.Label);
            new MiniGraph(mg2.Origin, (int)mg2.Dimensions.X, (int)mg2.Dimensions.Y, mg2.Label);
            new MiniGraph(mg3.Origin, (int)mg3.Dimensions.X, (int)mg3.Dimensions.Y, mg3.Label);
        }

        public static void PauseGame(ref List<Ball> BallsList)
        {
            if (Velocities.Count == 0)
            {
                for (int x = 0; x < BallsList.Count; x++)
                {
                    Velocities.Add(BallsList[x].Velocity);
                    BallsList[x] = ChangeBallVelocity(BallsList[x], Vector2.Zero);
                }
            }
            else
            {
                for (int x = 0; x < BallsList.Count; x++)
                {
                    BallsList[x] = ChangeBallVelocity(BallsList[x], Velocities[x]);
                }
                Velocities.Clear();
            }
        }

        public static Ball ChangeBallVelocity(Ball a, Vector2 NewVelocity)
        {
            a.Velocity = NewVelocity;
            return a;
        }

        public static void DebugGame()
        {
            Debug.visualCoords = !Debug.visualCoords;
            Debug.showBallNumbers = !Debug.showBallNumbers;
            Debug.speedTest = !Debug.speedTest;
        }
    }
}
