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
    /// Base textbox class for regular and stack-based textboxes which users can enter letters and numbers into
    /// </summary>
    public class TextBox : SelectBox
    {
        public bool Pressed;
        public int Timer;
        public static int BlinkTimer = 60;
        public int MaxChars;
        public string TempText;

        public static bool CapsLock = false;
        public static bool TempCapsLock = false;
        public static readonly List<string> NumPadKeys = new List<string>() { "NumPad0", "NumPad1", "NumPad2", "NumPad3", "NumPad4", "NumPad5", "NumPad6", "NumPad7", "NumPad8", "NumPad9" };
        public static readonly List<string> DKeys = new List<string>() { "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9" };

        public static void CheckCaps(string com)
        {
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

        public static bool Exit(string com)
        {
            bool press = true;
            if (com == "Tab" || com == "Enter" || com == "Return" || com == "Escape") // Exit textbox (09 / 13 / 27) - Seperate Return and Enter to change depending on keyboard and maximise ability
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
            int depth = 2;
            spriteBatch.Draw(Texture, new Rectangle((int)Origin.X + depth + pointer * LetterWidth, (int)Origin.Y + depth, 1, (int)Dimensions.Y - 2 * depth), Color.Black);
        }
    }
    /// <summary>
    /// Textbox allowing for scrolling through characters as uses a list structure to store contents
    /// </summary>
    public class RegTextBox : TextBox
    {
        public List<string> Chars;
        public int Pointer;
        public int LinePos;

        public RegTextBox(Vector2 origin, SpriteFont font, int maxchars, string temptext)
        {
            Origin = origin;
            Dimensions = new Vector2((maxchars + 0.4f) * LetterWidth, LetterHeight);
            Chars = new List<string>();
            Pointer = 0;
            Colour = Color.Red;
            Font = font;
            Pressed = false;
            Timer = 0;
            MaxChars = maxchars;
            Border = 2;
            LinePos = 0;
            TempText = temptext;
        }

        public void IdentifyCommand(string com)
        {
            CheckCaps(com);

            if (com == "Space") //Spacebar (32)
            {
                Chars.Add(" ");
                Pointer += 1;
                if (LinePos < MaxChars)
                {
                    LinePos += 1;
                }
            }
            else if (com == "Back" && Pointer > 0) //Backspace (08)
            {
                Chars.RemoveAt(Pointer - 1);
                MovePointer("Left");
                if (LinePos > 0)
                {
                    LinePos -= 1;
                }
            }
            else if (com == "Delete" && Pointer < Chars.Count())
            {
                Chars.RemoveAt(Pointer);
            }
            else if (NumPadKeys.Contains(com))
            {
                Chars.Add(NumPadKeys.IndexOf(com).ToString());
                MovePointer("Right");
            }
            else if (DKeys.Contains(com))
            {
                Chars.Add(DKeys.IndexOf(com).ToString());
                MovePointer("Right");
            }
            Pressed = Exit(com);
        }

        public void MovePointer(string com)
        {
            if (com == "Left" && Pointer > 0)
            {
                Pointer -= 1;
            }
            else if (com == "Right" && Pointer < MaxChars)
            {
                Console.Write(Pointer);
                Console.Write(" ");
                Console.WriteLine(MaxChars);
                Pointer += 1;
            }
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
                            Chars.Insert(Pointer, letter);
                        }
                        else
                        {
                            Chars.Insert(Pointer, letter.ToLower());
                        }
                        Pointer += 1;
                    }
                }

                IdentifyCommand(letter);
                MovePointer(letter);

            }
        }

        public void AddChar(Keys key)
        {
            if (Chars.Count() <= MaxChars)
            {
                Chars.Insert(Pointer, key.ToString());
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
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

            if (Chars.Count > 0 | Pressed)
            {
                DrawLetters(spriteBatch);
            }
            else
            {
                spriteBatch.DrawString(Font, TempText, Origin, Color.Gray);
            }
        }

        public void DrawLetters(SpriteBatch spriteBatch)
        {
            if (Chars.Count() <= MaxChars)
            {
                for (int x = 0; x < Chars.Count(); x++)
                {
                    spriteBatch.DrawString(Font, Chars[x], new Vector2(Origin.X + x * LetterWidth + 1, Origin.Y), Color.Black);
                }
            }
            else
            {
                for (int x = 0; x < MaxChars; x++)
                {
                    int y = x + Chars.Count() - MaxChars;
                    spriteBatch.DrawString(Font, Chars[y], new Vector2(Origin.X + x * LetterWidth + 1, Origin.Y), Color.Black);
                }
            }
        }
    }
    /// <summary>
    /// Textbox which uses a stack structure to store and alter data
    /// </summary>
    public class StackTextBox : TextBox
    {
        public Stack<string> Chars;

        public StackTextBox(Vector2 origin, int width, SpriteFont font, string text)
        {
            Origin = origin;
            Dimensions = new Vector2((width + 0.4f) * LetterWidth, LetterHeight);

            Chars = new Stack<string>(0, new List<string>());
            Colour = Color.Red;
            Font = font;
            Pressed = false;
            Timer = 0;
            MaxChars = width;
            Border = 2;
            TempText = text;
        }

        public void IdentifyCommand(string com)
        {
            CheckCaps(com);

            if (com == "Space") //Spacebar (32)
            {
                Chars.Push(" ");
                //Chars.Add(" ");
                /*Pointer += 1;
                if (LinePos < MaxChars)
                {
                    LinePos += 1;
                }*/
            }
            else if (com == "Back") //Backspace (08)
            {
                Chars.Pop();
                //Chars.RemoveAt(Pointer - 1);
                //MovePointer("Left");
                /*if (LinePos > 0)
                {
                    LinePos -= 1;
                }*/
            }
            else if (NumPadKeys.Contains(com))
            {
                Chars.Push(NumPadKeys.IndexOf(com).ToString());
                //MovePointer("Right");
            }
            else if (DKeys.Contains(com))
            {
                Chars.Push(DKeys.IndexOf(com).ToString());
                //MovePointer("Right");
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
            }
        }

        public void DrawTextBox(SpriteBatch spriteBatch)
        {
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

            if (Chars.GetLength() > 0 | Pressed)
            {
                DrawLetters(spriteBatch);
            }
            else
            {
                spriteBatch.DrawString(Font, TempText, Origin, Color.Gray);
            }
        }

        public void DrawLetters(SpriteBatch spriteBatch)
        {
            List<string> letters = Chars.GetContents();
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
    }
}
