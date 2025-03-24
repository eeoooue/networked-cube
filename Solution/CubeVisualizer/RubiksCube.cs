using LibNetCube;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CubeVisualizer
{
    class RubiksCube
    {
        static private readonly Color[] COLOURS = { Color.White, Color.Yellow, Color.Red, Color.Blue, Color.Green, Color.Orange };
        static private readonly CubeFace[] FACES = { CubeFace.Front, CubeFace.Back, CubeFace.Left, CubeFace.Right, CubeFace.Bottom, CubeFace.Top };

        readonly Object3D _Cube;
        private CubeState _CubeState;

        public RubiksCube(Model pCube, CubeState pCubeState)
        {
            _Cube = new Object3D(pCube, Color.White);
            _CubeState = pCubeState;
        }

        public void SetCubeState(CubeState pCubeState)
        {
            _CubeState = pCubeState;
        }

        private void DrawCubes(Camera pCamera)
        {
            _Cube.SetColor(Color.Black);
            _Cube.SetScale(Vector3.One);
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        _Cube.SetPosition(new Vector3(x, y, z));
                        _Cube.Draw(pCamera);
                    }
                }
            }
        }

        //TODO: refactor this method so it isn't using ugly switch statements
        private void DrawFaces(Camera pCamera) // Copilot wrote the inner switch, it works but it is disgusting
        {
            for (int i = 0; i < 6; i++)
            {
                // Get up vector
                Vector3 up = Vector3.Zero;
                switch (i)
                {
                    case 0:
                    case 1:
                        up = Vector3.UnitX;
                        break;
                    case 2:
                    case 3:
                        up = Vector3.UnitZ;
                        break;
                    case 4:
                    case 5:
                        up = Vector3.UnitY;
                        break;
                }

                int[,] face = _CubeState.GetFace(FACES[i]);
                for (int j = 0; j < 9; j++)
                {
                    // up direction stays the same, but other two components change
                    float x = 0, y = 0, z = 0;
                    // Set the position of the face (cube is 3x3x3 with first cube at 0,0,0 and the opposite cube at 2,2,2)

                    x = SetXValue(i, j);
                    y = SetYValue(i, j);
                    z = SetZValue(i, j);

                    _Cube.SetPosition(new Vector3(x, y, z) + (0.5f * up));

                    // Set the face colour based on the cube state
                    _Cube.SetColor(COLOURS[face[j / 3, j % 3]]);

                    // Scale cube so that the width is 0.2 in up direction
                    _Cube.SetScale((Vector3.One - (0.95f * up)) * 0.80f);
                    _Cube.Draw(pCamera);
                }
            }
        }

        public float SetXValue(int i, int j)
        {
            switch (i)
            {
                case 0: // Front
                    return -1;
                case 1: // Back
                    return 2;
                case 2: // Left
                    return j % 3;
                case 3: // Right
                    return j % 3;
                case 4: // Bottom
                    return j / 3;
                case 5: // Top
                    return j / 3;
            }

            return 0;
        }

        public float SetYValue(int i, int j)
        {
            switch (i)
            {
                case 0: // Front
                    return j / 3;
                case 1: // Back
                    return j / 3;
                case 2: // Left
                    return j / 3;
                case 3: // Right
                    return j / 3;
                case 4: // Bottom
                    return -1;
                case 5: // Top
                    return 2;
            }

            return 0;
        }

        public float SetZValue(int i, int j)
        {
            switch (i)
            {
                case 0: // Front
                    return j % 3;
                case 1: // Back
                    return j % 3;
                case 2: // Left
                    return -1;
                case 3: // Right
                    return 2;
                case 4: // Bottom
                    return j % 3;
                case 5: // Top
                    return j % 3;
            }

            return 0;
        }

        public void Draw(Camera pCamera)
        {
            DrawCubes(pCamera);
            DrawFaces(pCamera);
        }
    }
}
