using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MonoGamePool1
{
    class Updates
    {
        public static DiagonalLine UpdatePoolCue(DiagonalLine PoolCue, Vector2 mousePosition, Vector2 CueBall)
        {
            //PoolCue.Start = mousePosition;
            //PoolCue.End = CueBall;
            return new DiagonalLine(PoolCue.ID, 5, mousePosition, new Vector2(CueBall.X, CueBall.Y), PoolCue.Colour, false); // - (5 / 2)
        }

        public static DiagonalLine UpdateSightLine(DiagonalLine SightLine, Vector2 mousePosition, Vector2 CueBall)
        {
            SightLine.End.X = CueBall.X + 2 * (CueBall.X - mousePosition.X) - 1;
            SightLine.End.Y = CueBall.Y + 2 * (CueBall.Y - mousePosition.Y) - 1;
            //SightLine.End = new Vector2(CueBall.X + 2 * (CueBall.X - mousePosition.X), CueBall.Y + 2 * (CueBall.Y - mousePosition.Y));
            return new DiagonalLine(SightLine.ID, 5, CueBall, SightLine.End, SightLine.Colour, true);
        }

        public static void UpdateCurrentPlayer(ref int currentplayer, List<Player> players)
        {
            if (players[currentplayer].Shots == 0)
            {
                currentplayer = (currentplayer + 1) % 2;
            }
        }
    }
}
