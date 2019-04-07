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
    /// Used for the Pool Cue, Sightline and any other diagonal lines around the table
    /// </summary>
    public class DiagonalLine
    {
        public int Thickness;
        public Vector2 Start;
        public Vector2 End;
        public Color Colour;
        public bool Dotted;
        public Texture2D Texture;

        public DiagonalLine(int thickness, Vector2 start, Vector2 end, Color colour, bool dotted)
        {
            Thickness = thickness;
            Start = start;
            End = end;
            Colour = colour;
            Dotted = dotted;
            Texture = Graphics.BlankBox;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Get the vector between the start and the end. (from the start point)
            Vector2 vectorBetween = End - Start;
            //Get the angle of the vector.
            float angleVector = (float)Math.Atan2(vectorBetween.Y, vectorBetween.X);
            //The point on the blank square to draw from.
            Vector2 origin = new Vector2(0f, 0.5f);

            spriteBatch.Draw(Game1.PixelBox, Start, null, Colour, angleVector, origin, new Vector2(vectorBetween.Length(), Thickness), SpriteEffects.None, 0f);
        }
    }
}
