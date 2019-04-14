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
    /// <summary>
    /// Base textbox class for textboxes which users can enter letters and numbers into
    /// Room left for expansion into other types of textboxes
    /// </summary>
    public class TextBox : SelectBox
    {
        public bool Pressed;
        public int Timer;
        public static int BlinkTimer = 60;
        public int MaxChars;
        public string TempText;
        public bool Valid;
        public string Output;

        public bool CapsLock = false;
        public bool TempCapsLock = false;
        public readonly List<string> NumPadKeys = new List<string>() { "NumPad0", "NumPad1", "NumPad2", "NumPad3", "NumPad4", "NumPad5", "NumPad6", "NumPad7", "NumPad8", "NumPad9" };
        public readonly List<string> DKeys = new List<string>() { "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9" };

        public void CheckCaps(string com)
        {
            //Updates if the letters should be written in uppercase or lowercase by updating different caps lock statuses
            if (com == "CapsLock" || com == "Capital") //CapsLock (20)
            {
                CapsLock = !CapsLock;
            }
            if (Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift))
            {
                TempCapsLock = true;
            }
            else
            {
                TempCapsLock = false;
            }
        }

        public bool Exit(string com)
        {
            bool press = true;
            if (com == "Tab" || com == "Enter" || com == "Return" || com == "Escape") // Exit textbox (09 / 13 / 27) - Different Return and Enter to change depending on keyboard and maximise ability
            {
                press = false;
            }
            return press;
        }

        public void UpdatePressed()
        {
            if (Input.MouseWithinArea(Origin, new Vector2(Origin.X + Dimensions.X, Origin.Y + Dimensions.Y)) && Input.LeftMouseJustClicked())
            {
                Pressed = !Pressed;
            }
            if (Pressed)
            {
                Timer += 1;
                if (Timer > BlinkTimer - 1)
                {
                    Timer = 0;
                }
            }
            if (!Pressed)
            {
                Timer = 0;
            }
        }

        public void DrawBlinkingKeyLine(SpriteBatch spriteBatch, int pointer)
        {
            //Draws the blinking line to the screen which shows where the next letter would be entered
            int depth = 2;
            spriteBatch.Draw(Texture, new Rectangle((int)Origin.X + depth + pointer * LetterWidth, (int)Origin.Y + depth, 1, (int)Dimensions.Y - 2 * depth), Color.Black);
        }

        public bool ValidEntry()
        {
            //Checks whether the entered text is of an acceptable length
            Valid = Output.Length > 0 && Output.Length < MaxChars - 2;
            return Valid;
        }

        public string TempWrite()
        {
            //Temporary gray text for inside textbox when not in use
            if (Valid)
            {
                return TempText;
            }
            else
            {
                return "INVALID";
            }
        }
    }

    /// <summary>
    /// Textbox which uses a stack structure to store and alter data
    /// </summary>
    public class StackTextBox : TextBox
    {
        public Stack<string> Chars;

        public StackTextBox(Vector2 origin, SpriteFont font, int maxchars, string temptext)
        {
            Origin = origin;
            Dimensions = new Vector2((maxchars + 0.4f) * LetterWidth, LetterHeight);
            Chars = new Stack<string>(new string[maxchars], maxchars);
            Colour = Color.Red;
            Font = font;
            Pressed = false;
            Timer = 0;
            MaxChars = maxchars;
            Border = 2;
            TempText = temptext;
            Valid = true;
            Output = "";
        }

        public void IdentifyCommand(string com)
        {
            CheckCaps(com);
            if (com == "Enter" || com == "Return")
            {
                if (Chars.GetLength() > 0)
                {
                    if (Chars.GetLength() > 0 && Chars.GetLength() < MaxChars - 2)
                    {
                        string letter;
                        while (!Chars.IsEmpty())
                        {
                            letter = Chars.Pop();
                            Output = letter + Output;
                        }
                    }
                    else
                    {
                        Valid = false;
                        Clear();
                    }
                }
            }
            else if (com == "Space") //Spacebar (32)
            {
                Chars.Push(" ");
            }
            else if (com == "Back") //Backspace (08)
            {
                Chars.Pop();
            }
            else if (NumPadKeys.Contains(com))
            {
                Chars.Push(NumPadKeys.IndexOf(com).ToString());
            }
            else if (DKeys.Contains(com))
            {
                Chars.Push(DKeys.IndexOf(com).ToString());
            }

            Pressed = Exit(com);
        }

        public void UpdateLetters()
        {
            List<string> letters = Input.IdentifyKeysJustClicked();

            foreach (string letter in letters)
            {
                if (letter.Length == 1)
                {
                    if (General.InAlpha(letter))
                    {
                        if (CapsLock || TempCapsLock) //CapsLock and TempCapsLock (shift key held) check
                        {
                            Chars.Push(letter);
                        }
                        else
                        {
                            Chars.Push(letter.ToLower());
                        }
                    }
                }
                IdentifyCommand(letter);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draws textboxes to the screen
            Color color;
            if (Pressed)
            {
                color = Colour;
            }
            else
            {
                color = Color.Black;
            }
            spriteBatch.Draw(Texture, new Rectangle((int)Origin.X - Border, (int)Origin.Y - Border, (int)Dimensions.X + Border * 2, (int)Dimensions.Y + Border * 2), color);
            spriteBatch.Draw(Texture, new Rectangle((int)Origin.X, (int)Origin.Y, (int)Dimensions.X, (int)Dimensions.Y), Color.White);

            if (Chars.GetLength() > 0 || Pressed)
            {
                DrawLetters(spriteBatch);
            }
            else
            {
                spriteBatch.DrawString(Font, TempWrite(), Origin, Color.Gray);
            }
        }

        public void DrawLetters(SpriteBatch spriteBatch)
        {
            //Draws letters to the screen character by character
            string[] letters = Chars.GetContents();

            if (Chars.GetLength() <= MaxChars)
            {
                for (int x = 0; x < Chars.GetLength(); x++)
                {
                    spriteBatch.DrawString(Font, letters[x], new Vector2(Origin.X + x * LetterWidth + 1, Origin.Y), Color.Black);
                }
            }
            else
            {
                for (int x = 0; x < MaxChars; x++)
                {
                    int y = x + Chars.GetLength() - MaxChars;
                    spriteBatch.DrawString(Font, letters[y], new Vector2(Origin.X + x * LetterWidth + 1, Origin.Y), Color.Black);
                }
            }
        }

        public void Clear()
        {
            Chars.Clear();
            Output = "";
        }
    }
}
