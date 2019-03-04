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
    /// MiniGraphs are used to display the physics of the Cue Ball during the simulation (eg. change in speed over time)
    /// </summary>
    public class MiniGraph : Box
    {
        public Queue<float> Values;
        public float Max;
        public string Label;

        public MiniGraph(Vector2 origin, int width, int height, string label)
        {
            Origin = origin;
            Dimensions = new Vector2(width, height);
            Values = new Queue<float>(width);
            Max = 0;
            Label = label;
        }

        public void Update(float value)
        {
            Values.Enqueue(value);
            if (Values.GetTail() == 0)
            {
                Max = 0;
            }
            if (value > Max)
            {
                Max = value;
            }
            //Console.Write(Max + ": ");
            /*foreach(float x in Values.GetContents(Values))
            {
                Console.Write(x + ", ");
            }*/
            //Console.WriteLine("");
            //Console.WriteLine(Values.GetContents(Values));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Origin.X, (int)Origin.Y, (int)Dimensions.X, (int)Dimensions.Y), Color.Black); //Background
            spriteBatch.Draw(Texture, new Rectangle((int)(Origin.X + 0.25 * Dimensions.X), (int)Origin.Y, 1, (int)Dimensions.Y), Color.White); //y-axis
            spriteBatch.Draw(Texture, new Rectangle((int)(Origin.X), (int)(Origin.Y + Dimensions.Y - 5), (int)(Dimensions.X), 1), Color.White); //x-axis
            spriteBatch.DrawString(Game1.TextBoxFont, ((int)(Max + 0.5f)).ToString(), new Vector2(Origin.X, Origin.Y - (LetterHeight + 2)), Color.White);
            spriteBatch.DrawString(Game1.TextBoxFont, "0", new Vector2(Origin.X, Origin.Y + Dimensions.Y + 2), Color.White);
            spriteBatch.DrawString(Game1.TextBoxFont, Label, new Vector2(Origin.X + 2 * LetterWidth, Origin.Y + Dimensions.Y + 2), Color.White, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
            //Console.WriteLine(Values.Peek(Values));
            float[] items = Values.GetContents();
            for (int x = 0; x < Values.GetTail(); x++)
            {
                spriteBatch.Draw(Texture, new Rectangle((int)Origin.X + x, (int)(((Origin.Y + Dimensions.Y) - (items[x] / Max) * Dimensions.Y)), 1, 1), Color.Red);
            }
        }
    }
}
