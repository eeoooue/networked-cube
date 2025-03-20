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

            for(int i=0; i<3; i++)
            {
                int value;

                // set new top
                value = original.GetPiece(CubeFace.Front, i, 1);
                state.SetPiece(value, CubeFace.Top, i, 1); // either i or 2 - i

                // set new back
                value = original.GetPiece(CubeFace.Top, i, 1);
                state.SetPiece(value, CubeFace.Back, 2 - i, 1); // either i or 2 - i

                // set new bottom
                value = original.GetPiece(CubeFace.Back, i, 1);
                state.SetPiece(value, CubeFace.Bottom, 2 - i, 1); // either i or 2 - i

                // set new front
                value = original.GetPiece(CubeFace.Bottom, i, 1);
                state.SetPiece(value, CubeFace.Front, i, 1); // either i or 2 - i
            }

            FaceRotation.RotateFaceClockwise(state, CubeFace.Right);
            FaceRotation.RotateFaceClockwise(state, CubeFace.Left);
            FaceRotation.RotateFaceClockwise(state, CubeFace.Left);
            FaceRotation.RotateFaceClockwise(state, CubeFace.Left);
        }

        private static void RotateLeftwise(CubeState state)
        {
            CubeState original = new CubeState(state);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int value;

                    // set new left
                    value = original.GetPiece(CubeFace.Front, i, j);
                    state.SetPiece(value, CubeFace.Left, i, j); // either j or 2 - j

                    // set new back
                    value = original.GetPiece(CubeFace.Left, i, j);
                    state.SetPiece(value, CubeFace.Back, i, 2 - j); // either j or 2 - j

                    // set new right
                    value = original.GetPiece(CubeFace.Back, i, j);
                    state.SetPiece(value, CubeFace.Right, i, j); // either j or 2 - j

                    // set new front
                    value = original.GetPiece(CubeFace.Right, i, j);
                    state.SetPiece(value, CubeFace.Front, i, j); // either j or 2 - j
                }
            }
        }
    }
}
