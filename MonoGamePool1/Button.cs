using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePool1
{
    public struct Button
    {
        public Vector2 Origin;
        public Vector2 Dimensions;
        public string Text;
        public Texture2D Color;
        public SpriteFont ButtonFont;
        public bool Pressed;

        public Button(Vector2 origin, Vector2 dimensions, string text, Texture2D color, SpriteFont buttonfont, bool pressed)
        {
            Origin = origin;
            Dimensions = dimensions;
            Text = text;
            Color = color;
            ButtonFont = buttonfont;
            Pressed = pressed;
        }
        /**public static bool IsClicked()
        {
            return true;
        }**/
    }
    public struct TextBox
    {
        public Vector2 Origin;
        public Vector2 Dimensions;
        public List<string> Chars;
        public int Pointer;
        public Texture2D Color;
        public SpriteFont TextFont;
        public bool Pressed;
        public int Timer;

        public TextBox(Vector2 origin, Vector2 dimensions, List<string> chars, int pointer, Texture2D color, SpriteFont textfont, bool pressed, int timer)
        {
            Origin = origin;
            Dimensions = dimensions;
            Chars = chars;
            Pointer = pointer;
            Color = color;
            TextFont = textfont;
            Pressed = pressed;
            Timer = timer;
        }
    }
    public struct StackTextBox
    {
        public Vector2 Origin;
        public Vector2 Dimensions;
        public Stack<string> Chars;
        public Texture2D Color;
        public SpriteFont TextFont;
        public bool Pressed;
        public int Timer;

        public StackTextBox(Vector2 origin, Vector2 dimensions, Stack<string> chars, Texture2D color, SpriteFont textfont, bool pressed, int timer)
        {
            Origin = origin;
            Dimensions = dimensions;
            Chars = chars;
            Color = color;
            TextFont = textfont;
            Pressed = pressed;
            Timer = timer;
        }
    }
}
