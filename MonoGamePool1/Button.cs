using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePool1
{
    /// <summary>
    /// Base class for most rectangular objects drawn to screen
    /// </summary>
    public class Box
    {
        public Vector2 Origin;
        public Vector2 Dimensions;
        public Color Colour;
        public SpriteFont Font;
        public int Border = 2;
        public Texture2D Texture = Game1.BlankBox;

        public readonly int LetterHeight = 20;
        public readonly int LetterWidth = 11;

    }
    /// <summary>
    /// SelectBox used for any box with main functionality based on user interaction (eg. buttons & textboxes)
    /// </summary>
    public class SelectBox : Box
    {
        public string Text;
    }
    /// <summary>
    /// Buttons can be clicked by user and are used for changing gamestate options
    /// </summary>
    public class Button : SelectBox
    {
        public bool Pressed;

        public Button(Vector2 origin, string text, Color colour, SpriteFont font)
        {
            Origin = origin;
            Dimensions = new Vector2(1 + 11 * text.Length, 20);
            Text = text;
            Colour = colour;
            Font = font;
            Pressed = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Origin.X - Border, (int)Origin.Y - Border * 2, (int)Dimensions.X + Border * 2, (int)Dimensions.Y + Border * 2), Colour);
            spriteBatch.DrawString(Font, Text, Origin, Color.White);
        }

        public void Update(Vector2 MousePosition)
        {
            int clickSize = 1;
            if (Input.MouseWithinArea(Origin, new Vector2(Origin.X + Dimensions.X, Origin.Y + Dimensions.Y)) && !Pressed && Input.LeftMouseJustClicked())
            {
                Origin = Origin + new Vector2(clickSize);
                Pressed = true;
            }
            if (Pressed && Input.LeftMouseJustReleased())
            {
                Origin = Origin - new Vector2(clickSize);
                Pressed = false;
            }
        }
    }
    /// <summary>
    /// Box with label in centre and left & right arrows signifying up & down of value in centre retrospectivly depending on user input
    /// Made up of a box with two buttons eitherside
    /// </summary>
    public class NumBox : SelectBox
    {
        public int Max;
        public Button Left;
        public Button Right;

        public NumBox(Vector2 origin, SpriteFont font, int max)
        {
            int digits = (int)Math.Log10(max + 1) + 3;
            Origin = origin;
            Dimensions = new Vector2(digits * LetterWidth, LetterHeight);
            Colour = Color.Blue;
            Font = font;
            Max = max;
            Left = new Button(origin, "<", Color.Red, font);
            Right = new Button(new Vector2(origin.X + digits * LetterWidth - LetterWidth + 4 * Border, origin.Y), ">", Color.Green, font);
        }

        public void Update()
        {
            bool leftState = Left.Pressed;
            bool rightState = Right.Pressed;
            Left.Update(Input.mousePosition);
            Right.Update(Input.mousePosition);
            if (leftState == true && Left.Pressed == false && Debug.rows > 1)
            {
                Debug.rows -= 1;
            }
            else if (rightState == true && Right.Pressed == false && Debug.rows < Max)
            {
                Debug.rows += 1;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Left.Draw(spriteBatch);
            spriteBatch.Draw(Texture, new Rectangle((int)Origin.X + LetterWidth + Border, (int)Origin.Y - Border * 2, (int)Dimensions.X + 2 * (Border - LetterWidth), LetterHeight + 2 * Border), Colour); //Middle TextBox
            spriteBatch.DrawString(Font, Debug.rows.ToString(), new Vector2((int)Origin.X + LetterWidth + 3 * Border, (int)Origin.Y - Border), Color.White); //Text (number)
            Right.Draw(spriteBatch);
        }
    }

    public class SwitchBox : SelectBox
    {
        public List<Color> ActiveColours = new List<Color> { Color.LightCyan, Color.Green };
        public List<Color> DeactiveColours = new List<Color> { Color.Red, Color.LightCyan };
        public Button On;
        public Button Off;
        public bool State = true;

        public SwitchBox(Vector2 origin, SpriteFont font)
        {
            Origin = origin;
            Dimensions = new Vector2(2 * LetterWidth, LetterWidth);
            Colour = Color.LightGray;
            Font = font;
            On = new Button(origin, " ", Color.White, font);
            Off = new Button(new Vector2(origin.X + LetterWidth + Border * 2, Origin.Y), " ", Color.White, font);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (State)
            {
                On.Colour = ActiveColours[0];
                Off.Colour = ActiveColours[1];
                On.Draw(spriteBatch);
                Off.Draw(spriteBatch);
            }
            else
            {
                On.Colour = DeactiveColours[0];
                Off.Colour = DeactiveColours[1];
                On.Draw(spriteBatch);
                Off.Draw(spriteBatch);
            }
        }

        public void Update()
        {
            //Changes the state of the SwitchBox from on to off or vice versa
            On.Update(Input.mousePosition);
            Off.Update(Input.mousePosition);
            if ((On.Pressed && State == false) || (Off.Pressed && State == true))
            {
                State = !State;
            }
        }
    }
}
