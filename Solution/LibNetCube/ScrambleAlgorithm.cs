using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNetCube
{
    public static class ScrambleAlgorithm
    {
        private static Random Randomizer = new Random();
        
        private const int DefaultScrambleLength = 20;
        
        private static readonly CubeMove[] AllMoves = Enum.GetValues<CubeMove>();

        /// <summary>
        /// Generates a random scramble sequence
        /// </summary>
        /// <returns>A list of random CubeMove values for scrambling a cube</returns>
        public static List<CubeMove> GenerateScramble()
        {
            return GenerateScramble(DefaultScrambleLength);
        }
        
        /// <summary>
        /// Helper method to generate a scramble of specified length
        /// </summary>
        private static List<CubeMove> GenerateScramble(int length)
        {
            if (length <= 0)
            {
                return [];
            }
            
            List<CubeMove> scramble = new(length);
            CubeMove? lastMove = null;
            
            for (int i = 0; i < length; i++)
            {
                CubeMove nextMove = GetRandomMove(lastMove);
                scramble.Add(nextMove);
                lastMove = nextMove;
            }
            
            return scramble;
        }
        
        /// <summary>
        /// Gets a random move that doesn't conflict with the previos move
        /// </summary>
        private static CubeMove GetRandomMove(CubeMove? previousMove)
        {
            if (previousMove == null)
            {
                return AllMoves[Randomizer.Next(AllMoves.Length)];
            }
            
            CubeMove[] oppositeGroup = CubeMoveOppositionGroups.OppositionGroups[(CubeMove)previousMove];
            
            CubeMove nextMove;
            do
            {
                nextMove = AllMoves[Randomizer.Next(AllMoves.Length)];
            } while (oppositeGroup.Contains(nextMove));
            
            return nextMove;
        }
    }
}
