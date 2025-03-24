using LibNetCube;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
                DrawFace(pCamera, i);
            }
        }

        private void DrawFace(Camera pCamera, int i)
        {
            // Get up vector
            Vector3 up = GetRelativeUpVector(i);

            int[,] face = _CubeState.GetFace(FACES[i]);
            for (int j = 0; j < 9; j++)
            {
                // up direction stays the same, but other two components change
                // Set the position of the face (cube is 3x3x3 with first cube at 0,0,0 and the opposite cube at 2,2,2)

                float x = SetXValue(i, j);
                float y = SetYValue(i, j);
                float z = SetZValue(i, j);

                _Cube.SetPosition(new Vector3(x, y, z) + (0.5f * up));

                // Set the face colour based on the cube state
                int pieceValue = face[j / 3, j % 3];
                _Cube.SetColor(COLOURS[pieceValue]);

                // Scale cube so that the width is 0.2 in up direction
                _Cube.SetScale((Vector3.One - (0.95f * up)) * 0.80f);
                _Cube.Draw(pCamera);
            }
        }

        private Vector3 GetRelativeUpVector(int i)
        {
            switch (i)
            {
                case 0:
                case 1:
                    return Vector3.UnitX;
                case 2:
                case 3:
                    return Vector3.UnitZ;
                case 4:
                case 5:
                    return Vector3.UnitY;
                default:
                    return Vector3.UnitX;
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
                default:
                    return j / 3;
            }
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
                default:
                    return 2;
            }
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
                default:
                    return j % 3;
            }
        }

        public void Draw(Camera pCamera)
        {
            DrawCubes(pCamera);
            DrawFaces(pCamera);
        }
    }
}
