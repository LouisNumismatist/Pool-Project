using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MonoGamePool1
{
    class UpdateClass
    {
        public static Ball UpdateBall(Ball a)
        {
            float stop = 0.05f;
            bool flag = false;
            if ((a.Velocity.X < stop && a.Velocity.X > 0) || (a.Velocity.X > -stop && a.Velocity.X < 0))
            {
                flag = true;
                a.Velocity.X = 0;
            }
            if ((a.Velocity.Y < stop && a.Velocity.Y > 0) || (a.Velocity.Y > -stop && a.Velocity.Y < 0))
            {
                flag = true;
                a.Velocity.Y = 0;
            }
            if (!flag)
            {
                //a.Velocity += a.Velocity * a.Acceleration;
                //a.Velocity += a.Acceleration;
                a.Center += a.Velocity;
                //Graphics.WriteBall(a);
            }
            a.Velocity *= 0.992f;
            return a;
            //return a;
            //a.Velocity = Vector2.Add(a.Velocity, Vector2.Multiply(a.Velocity, a.Acceleration));
            //a.Center = Vector2.Add(a.Center, a.Velocity);
        }
        public static DiagonalLine UpdatePoolCue(DiagonalLine PoolCue, Vector2 mousePosition, Vector2 CueBall)
        {
            //PoolCue.Start = mousePosition;
            //PoolCue.End = CueBall;
            return new DiagonalLine(PoolCue.ID, 5, mousePosition, new Vector2(CueBall.X, CueBall.Y), PoolCue.Color, PoolCue.Dotted); // - (5 / 2)
        }
        public static DiagonalLine UpdateSightLine(DiagonalLine SightLine, Vector2 mousePosition, Vector2 CueBall)
        {
            SightLine.End.X = CueBall.X + 2 * (CueBall.X - mousePosition.X) - 1;
            SightLine.End.Y = CueBall.Y + 2 * (CueBall.Y - mousePosition.Y) - 1;
            //SightLine.End = new Vector2(CueBall.X + 2 * (CueBall.X - mousePosition.X), CueBall.Y + 2 * (CueBall.Y - mousePosition.Y));
            return new DiagonalLine(SightLine.ID, 5, CueBall, SightLine.End, SightLine.Color, SightLine.Dotted);
        }
        public static Button UpdateButton(Button button, Vector2 MousePosition)
        {
            int ClickSize = 1;
            if (General.MouseWithinArea(MousePosition, button.Origin, new Vector2(button.Origin.X + button.Dimensions.X, button.Origin.Y + button.Dimensions.Y)) && !button.Pressed && Input.LeftMouseJustClicked())
            {
                button.Origin = button.Origin + new Vector2(ClickSize);
                button.Pressed = true;
            }
            if (button.Pressed && Input.LeftMouseJustReleased())
            {
                button.Origin = button.Origin - new Vector2(ClickSize);
                button.Pressed = false;
            }
            return button;
        }
        public static TextBox UpdateTextBoxPressed(TextBox textbox, Vector2 MousePosition)
        {
            //Console.WriteLine(textbox.Timer);
            //Console.WriteLine(textbox.Pressed);
            if (General.MouseWithinArea(MousePosition, textbox.Origin, new Vector2(textbox.Origin.X + textbox.Dimensions.X, textbox.Origin.Y + textbox.Dimensions.Y)) && Input.LeftMouseJustClicked())
            {
                textbox.Pressed = !textbox.Pressed;
            }
            if (textbox.Pressed)
            {
                textbox.Timer += 1;
                if (textbox.Timer > ButtonFunctions.BlinkTimer - 1)
                {
                    textbox.Timer = 0;
                }
            }
            if (!textbox.Pressed)
            {
                textbox.Timer = 0;
            }
            return textbox;
        }
        public static TextBox UpdateTextBoxLetters(TextBox textbox)
        {
            List<string> letters = Input.IdentifyKeysJustClicked();
            foreach (string x in letters)
            {
                Console.WriteLine(x);
            }
            foreach (string letter in letters)
            {
                if (letter.Length == 1)
                {
                    if (General.InAlpha(letter))
                    {
                        if (TextBoxFunctions.CapsLock || TextBoxFunctions.TempCapsLock) //CapsLock and TempCapsLock (shift key held) check
                        {
                            textbox.Chars.Insert(textbox.Pointer, letter);
                            textbox.Pointer += 1;
                        }
                        else
                        {
                            textbox.Chars.Insert(textbox.Pointer, letter.ToLower());
                            textbox.Pointer += 1;
                        }
                    }
                }

                TextBoxFunctions.IdentifyCommand(ref textbox, letter);
                TextBoxFunctions.MovePointer(ref textbox, letter);

            }
            return textbox;
        }
    }
}
