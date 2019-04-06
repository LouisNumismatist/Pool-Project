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

            Vector2 end = mousePosition;
            Vector2 start = new Vector2(CueBall.X, CueBall.Y);
            Vector2 between = end - start;
            float dist = between.Length();
            between = Physics.UnitVector(between);
            Vector2 offset = between * Math.Max(0, (dist - 400) * 0.4f);
            end = start + between * 400f;

            return new DiagonalLine(PoolCue.Thickness, start + offset, end + offset, PoolCue.Colour, false); // - (5 / 2)
        }

        public static DiagonalLine UpdateSightLine(DiagonalLine SightLine, Vector2 mousePosition, Vector2 CueBall, DiagonalLine PoolCue)
        {
            SightLine.End.X = CueBall.X + 3 * (CueBall.X - mousePosition.X);
            SightLine.End.Y = CueBall.Y + 3 * (CueBall.Y - mousePosition.Y);
            //SightLine.End.X = CueBall.X + 2 * (CueBall.X - mousePosition.X) - 1;
            //SightLine.End.Y = CueBall.Y + 2 * (CueBall.Y - mousePosition.Y) - 1;
            //SightLine.End = new Vector2(CueBall.X + 2 * (CueBall.X - mousePosition.X), CueBall.Y + 2 * (CueBall.Y - mousePosition.Y));
            return new DiagonalLine(SightLine.Thickness, CueBall, SightLine.End, SightLine.Colour, true);
        }

        public static void UpdateCurrentPlayer(ref int currentPlayer, List<Player> players)
        {
            players[currentPlayer].Shots -= 1;
            if (players[currentPlayer].Shots == 0)
            {
                currentPlayer = 1 - currentPlayer;
            }
            if (players[currentPlayer].Shots == 0)
            {
                players[currentPlayer].Shots += 1;
            }
        }
    }
}
