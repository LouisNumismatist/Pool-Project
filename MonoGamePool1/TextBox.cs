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
        public bool Valid;
        public string Output;

        public bool CapsLock = false;
        public bool TempCapsLock = false;
        public readonly List<string> NumPadKeys = new List<string>() { "NumPad0", "NumPad1", "NumPad2", "NumPad3", "NumPad4", "NumPad5", "NumPad6", "NumPad7", "NumPad8", "NumPad9" };
        public readonly List<string> DKeys = new List<string>() { "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9" };

        public void CheckCaps(string com)
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

        public bool Exit(string com)
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

        public bool ValidEntry()
        {
            Valid = Output.Length > 0 && Output.Length < MaxChars - 2;
            return Valid;
        }

        public string TempWrite()
        {
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
            Valid = true;
            Output = "";
        }

        public void IncreaseLinePos()
        {
            if (LinePos < MaxChars)
            {
                LinePos += 1;
            }
            else
            {
                Pointer += 1;
            }
        }

        public void DecreaseLinePos()
        {
            if (LinePos > 0)
            {
                LinePos -= 1;
            }
            else if (Pointer > 0)
            {
                Pointer -= 1;
            }
        }

        public void IdentifyCommand(string com)
        {
            CheckCaps(com);
            MovePointer(com);

            if (com == "Enter" | com == "Return")
            {
                if (Chars.Count > 0)
                {
                    if (Chars.Count > 0 && Chars.Count < MaxChars - 2)
                    {
                        foreach (string letter in Chars)
                        {
                            Output += letter;
                        }
                        Chars = new List<string>();
                    }
                    else
                    {
                        Valid = false;
                        Clear();
                    }
                }
            }
            if (com == "Space") //Spacebar (32)
            {
                Chars.Insert(Pointer + LinePos, " ");
                IncreaseLinePos();
            }
            else if (com == "Back" && Pointer + LinePos > 0) //Backspace (08)
            {
                DecreaseLinePos();
                Chars.RemoveAt(Pointer + LinePos);
                
            }
            else if (com == "Delete" && Pointer + LinePos < Chars.Count())
            {
                Chars.RemoveAt(Pointer + LinePos);
            }
            else if (NumPadKeys.Contains(com))
            {
                Chars.Insert(Pointer + LinePos, NumPadKeys.IndexOf(com).ToString());
                IncreaseLinePos();
            }
            else if (DKeys.Contains(com))
            {
                Chars.Insert(Pointer + LinePos, DKeys.IndexOf(com).ToString());
                IncreaseLinePos();
            }
            Pressed = Exit(com);
        }

        public void MovePointer(string com)
        {
            if (com == "Left")
            {
                DecreaseLinePos();
            }
            else if (com == "Right")
            {
                IncreaseLinePos();
            }
        }

        public void UpdateLetters()
        {
            //Gets list of keys clicked by the user in that frame
            List<string> letters = Input.IdentifyKeysJustClicked();

            foreach (string letter in letters)
            {
                if (letter.Length == 1)
                {
                    if (General.InAlpha(letter) && Pointer + LinePos < MaxChars)
                    {
                        if (CapsLock || TempCapsLock) //CapsLock and TempCapsLock (shift key held) check
                        {
                            Chars.Insert(Pointer + LinePos, letter);
                        }
                        else
                        {
                            Chars.Insert(Pointer + LinePos, letter.ToLower());
                        }
                        IncreaseLinePos();
                        Console.WriteLine(Pointer + " " + LinePos);
                    }
                }
                IdentifyCommand(letter);
            }
        }

        public void AddChar(string com)
        {
            if (Pointer + LinePos < Chars.Count())
            {
                Chars.Insert(Pointer + LinePos, com);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color color;
            //Choose border colour depending on whether clicked or not
            if (Pressed)
            {
                color = Colour;
            }
            else
            {
                color = Color.Black;
            }
            //Draw textbox boxes
            spriteBatch.Draw(Texture, new Rectangle((int)Origin.X - Border, (int)Origin.Y - Border, (int)Dimensions.X + Border * 2, (int)Dimensions.Y + Border * 2), color);
            spriteBatch.Draw(Texture, new Rectangle((int)Origin.X, (int)Origin.Y, (int)Dimensions.X, (int)Dimensions.Y), Color.White);

            if (Chars.Count > 0 | Pressed)
            {
                //Draw all letters player has entered
                DrawLetters(spriteBatch);
            }
            else
            {
                //Write temp text
                spriteBatch.DrawString(Font, TempWrite(), Origin, Color.Gray);
            }
        }

        public void DrawLetters(SpriteBatch spriteBatch)
        {
            if (Chars.Count() <= MaxChars)
            {
                //Textbox doesn't need extension
                for (int x = 0; x < Chars.Count(); x++)
                {
                    spriteBatch.DrawString(Font, Chars[x], new Vector2(Origin.X + x * LetterWidth + 1, Origin.Y), Color.Black);
                }
            }
            else
            {
                //Textbox needs extension
                for (int x = 0; x < MaxChars; x++)
                {
                    int y = x + Chars.Count() - MaxChars;
                    spriteBatch.DrawString(Font, Chars[y], new Vector2(Origin.X + x * LetterWidth + 1, Origin.Y), Color.Black);
                }
            }
        }

        public void Clear()
        {
            //Resets textbox when item successfully entered
            Chars.Clear();
            Output = "";
            Pointer = LinePos = 0;
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
            if (com == "Enter" | com == "Return")
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
                spriteBatch.DrawString(Font, TempWrite(), Origin, Color.Gray);
            }
        }

        public void DrawLetters(SpriteBatch spriteBatch)
        {
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
