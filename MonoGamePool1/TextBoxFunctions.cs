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
    public class TextBoxFunctions
    {
        public static bool CapsLock = false;
        public static bool TempCapsLock = false;
        public static List<string> NumPadKeys = new List<string>() { "NumPad0", "NumPad1", "NumPad2", "NumPad3", "NumPad4", "NumPad5", "NumPad6", "NumPad7", "NumPad8", "NumPad9" };
        public static List<string> DKeys = new List<string>() { "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9" };

        public static void IdentifyCommand(ref TextBox textbox, string com)
        {
            if (com == "Space") //Spacebar (32)
            {
                textbox.Chars.Add(" ");
                textbox.Pointer += 1;
            }
            else if (com == "CapsLock" || com == "Capital") //CapsLock (20)
            {
                CapsLock = !CapsLock;
            }
            else if (com == "Back" && textbox.Pointer > 0) //Backspace (08)
            {
                textbox.Chars.RemoveAt(textbox.Pointer - 1);
                textbox.Pointer -= 1;
            }
            else if (com == "Delete" && textbox.Pointer < textbox.Chars.Count())
            {
                textbox.Chars.RemoveAt(textbox.Pointer);
            }
            else if (com == "Tab" || com == "Enter" || com == "Return" || com == "Escape") // Exit textbox (09 / 13 / 27) - Seperate Return and Enter to change depending on keyboard and maximise ability
            {
                textbox.Pressed = false;
            }
            else if (NumPadKeys.Contains(com))
            {
                textbox.Chars.Add(NumPadKeys.IndexOf(com).ToString());
                textbox.Pointer += 1;
            }
            else if (DKeys.Contains(com))
            {
                textbox.Chars.Add(DKeys.IndexOf(com).ToString());
                textbox.Pointer += 1;
            }
            //if (com == "LeftShift" || com == "RightShift")
            if (Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift))
            {
                TempCapsLock = true;
            }
            else
            {
                TempCapsLock = false;
            }
        }
        public static void MovePointer(ref TextBox textbox, string com)
        {
            if (com == "Left" && textbox.Pointer > 0)
            {
                textbox.Pointer -= 1;
            }
            else if (com == "Right" && textbox.Pointer < textbox.Chars.Count())
            {
                textbox.Pointer += 1;
            }
        }
    }
}
