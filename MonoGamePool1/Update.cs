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
    /// Class for updating objects which do not have a class to themselves
    /// </summary>
    class Updates
    {
        public static DiagonalLine UpdatePoolCue(DiagonalLine PoolCue, Vector2 mousePosition, Vector2 CueBall)
        {
            Vector2 end = mousePosition; //One end of the Pool Cue

            Vector2 start = new Vector2(CueBall.X, CueBall.Y); //The other end of the Pool Cue

            Vector2 between = end - start; //The distance between the mouse cursor and the center of the Cue Ball (as a Vector2)

            float dist = between.Length(); //Above distance but as a scalar quantity (one without direction, only magnitude)

            between = Physics.UnitVector(between); //Vector between the cursor and the Cue Ball but conserving direction and with a magnitude of one

            Vector2 offset = between * Math.Max(0, (dist - 400) * 0.4f); //For pulling the Cue Stick away from the Cue Ball when it reaches a certain distance

            end = start + between * 400f; //Above continued

            return new DiagonalLine(PoolCue.Thickness, start + offset, end + offset, PoolCue.Colour, false);
        }

        public static DiagonalLine UpdateSightLine(DiagonalLine SightLine, Vector2 mousePosition, Vector2 CueBall, DiagonalLine PoolCue)
        {
            //Length of the sight line is proportional to the speed at which the Cue Ball comes away from the collision
            SightLine.End.X = CueBall.X + 3 * (CueBall.X - mousePosition.X);
            SightLine.End.Y = CueBall.Y + 3 * (CueBall.Y - mousePosition.Y);

            return new DiagonalLine(SightLine.Thickness, CueBall, SightLine.End, SightLine.Colour, true);
        }

        public static void UpdateCurrentPlayer(ref int currentPlayer, List<Player> players)
        {
            GamePlay.InTurn = true;
        }
    }
}
