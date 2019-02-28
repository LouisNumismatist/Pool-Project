using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGamePool1
{
    public static class Input
    {
        private static MouseState currentMouseState;
        private static MouseState previousMouseState;
        public static KeyboardState currentKeyboardState;
        public static KeyboardState previousKeyboardState;
        public static Vector2 mousePosition;

        public static void UpdateInputs()
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);
            //Updates mouse states (new to prev)
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }

        public static bool LeftMouseJustClicked()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
            //Identifies if first click frame
        }

        public static bool LeftMouseJustReleased()
        {
            return currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed;
            //Identifies if released click frame
        }

        public static bool LeftMouseHeld()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed;
            //Hold click
        }

        public static void BallFollow(Ball a)
        {
            if (LeftMouseHeld())
            {
                a.Center = new Vector2(currentMouseState.X, currentMouseState.Y);
            }
            //Move ball with mouse
        }

        public static bool KeyJustClicked(Keys key)
        {
            return previousKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key);
        }

        public static bool KeyJustReleased(Keys key)
        {
            return previousKeyboardState.IsKeyDown(key) && currentKeyboardState.IsKeyUp(key);
        }

        public static bool KeyHeld(Keys key)
        {
            return previousKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key);
        }

        public static List<string> IdentifyKeysJustClicked()
        {
            List<string> finalLetters = new List<string>();
            foreach (string letter in KeysList(currentKeyboardState))
            {
                if (!KeysList(previousKeyboardState).Contains(letter))
                {
                    finalLetters.Add(letter);
                }
            }
            return finalLetters;
        }

        public static List<string> KeysList(KeyboardState keyboardstate)
        {
            Keys[] keys = keyboardstate.GetPressedKeys();
            List<string> letters = new List<string>();
            foreach (Keys key in keys)
            {
                letters.Add(key.ToString());
            }
            return letters;
        }

        public static bool MouseWithinArea(Vector2 TopLeft, Vector2 LowRight)
        {
            if (mousePosition.X >= TopLeft.X && mousePosition.X <= LowRight.X &&
                mousePosition.Y >= TopLeft.Y && mousePosition.Y <= LowRight.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
