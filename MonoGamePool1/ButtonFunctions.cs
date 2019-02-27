using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGamePool1
{
    public class ButtonFunctions
    {
        public static List<Vector2> Velocities = new List<Vector2>();

        public static int BlinkTimer = 60;
        public static int CharacterGap = 11;

        public static void SaveGame(List<Ball> BallsList)
        {
            string filePath = @"C:\Users\Louis\source\repos\MonoGamePool1\MonoGamePool1\SaveFiles\";
            string moment = FileSaving.CurrentMoment();
            filePath += moment;
            Console.WriteLine(filePath);
            FileSaving.WriteToFile(filePath, BallsList);
        }
        public static List<Ball> LoadGame(string filePath, Dictionary<string, Texture2D> dict)
        {
            List<Ball> balls = new List<Ball>();
            string[] lines = FileSaving.ReadFromFile(filePath);
            foreach(string item in lines)
            {
                balls.Add(FileSaving.StringToObject(item, dict));
            }
            return balls;
        }
        public static void ResetGame(ref List<Ball> BallsList, ref List<Ball> Graveyard, Texture2D CueBall, Texture2D YellowBall, Texture2D RedBall, Texture2D EightBall)
        {
            BallsList.Clear();
            Graveyard.Clear();
            Init.InitialiseBalls(ref BallsList, CueBall, YellowBall, RedBall, EightBall);
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
        public static void TextBoxAddChar(ref TextBox textbox, Keys key)
        {
            textbox.Chars.Insert(textbox.Pointer, key.ToString());
        }
    }
}
