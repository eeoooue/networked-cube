using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNetCube
{
    public static class MoveParser
    {
        public static CubeMove ParseMove(string move)
        {
            if (string.IsNullOrWhiteSpace(move))
            {
                throw new ArgumentException("Move string cannot be null or empty", nameof(move));
            }

            // Trim whitespace and convert to uppercase
            string normalizedMove = move.Trim().ToUpper();

            // Single dictionary lookup - O(1) operation
            if (CubeMoveMap.MoveMap.TryGetValue(normalizedMove, out CubeMove result))
            {
                return result;
            }

            throw new ArgumentException($"Invalid move format: {move}", nameof(move));
        }

        public static List<CubeMove> ParseMoveSequence(string sequence)
        {
            if (string.IsNullOrWhiteSpace(sequence))
            {
                throw new ArgumentException("Move sequence string cannot be null or empty", nameof(sequence));
            }

            List<CubeMove> result = new List<CubeMove>();

            foreach (var move in sequence.Split(' ')) //Splitting the string by spaces
            {
                CubeMove currentMove = ParseMove(move);

                result.Add(currentMove);
            }

            return result;
        }
    }
}
