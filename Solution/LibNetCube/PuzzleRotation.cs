using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNetCube
{
    internal static class PuzzleRotation
    {
        public static void RotateEntirePuzzle(CubeState state, string direction)
        {
            switch (direction)
            {
                case "up":
                    RotateUpwards(state);
                    return;
                case "down":
                    RotateUpwards(state);
                    RotateUpwards(state);
                    RotateUpwards(state);
                    return;
                case "left":
                    RotateLeftwise(state);
                    return;
                case "right":
                    RotateLeftwise(state);
                    RotateLeftwise(state);
                    RotateLeftwise(state);
                    return;
                default:
                    throw new ArgumentException();
            }
        }

        private static void RotateUpwards(CubeState state)
        {
            CubeState original = new CubeState(state);


            throw new NotImplementedException();
        }

        private static void RotateLeftwise(CubeState state)
        {
            CubeState original = new CubeState(state);


            throw new NotImplementedException();
        }
    }
}
