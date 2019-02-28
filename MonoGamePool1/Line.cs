using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePool1
{
    /*public class Line
    {
        public int ID;
        public int Thickness;
        public Vector2 Start;
        public Vector2 End;
        public Color Colour;
        public bool Dotted;
    }*/
    /// <summary>
    /// Used for the Pool Cue, Sightline and any other diagonal lines around the table
    /// Draws the lines using the y=mx+c equation found broken up in the Physics class
    /// </summary>
    public class DiagonalLine
    {
        public int ID;
        public int Thickness;
        public Vector2 Start;
        public Vector2 End;
        public Color Colour;
        public bool Dotted;

        public DiagonalLine(int id, int thickness, Vector2 start, Vector2 end, Color colour, bool dotted)
        {
            ID = id;
            Thickness = thickness;
            Start = start;
            End = end;
            Colour = colour;
            Dotted = dotted;
        }

        /*public static DiagonalLine InitialiseDiagonalLine(Color color)
        {
            return new DiagonalLine(0, 3, new Vector2(100), new Vector2(200, 50), color, false);
        }*/
    }

    /*    public class SightLine : DiagonalLine
        {
            public SightLine(int id, int thickness, Vector2 start, Vector2 end, Color colour)
            {
                ID = id;
                Thickness = thickness;
                Start = start;
                End = end;
                Colour = colour;
                Dotted = false;
            }
        }

        public class PoolCue : DiagonalLine
        {
            public PoolCue(int id, int thickness, Vector2 start, Vector2 end, Color colour)
            {
                ID = id;
                Thickness = thickness;
                Start = start;
                End = end;
                Colour = colour;
                Dotted = true;
            }
        }
        public class GraphicsLine : DiagonalLine
        {
            public GraphicsLine(int id, int thickness, Vector2 start, Vector2 end, Color colour, bool dotted)
            {
                ID = id;
                Thickness = thickness;
                Start = start;
                End = end;
                Colour = colour;
                Dotted = dotted;
            }
        }*/
}
