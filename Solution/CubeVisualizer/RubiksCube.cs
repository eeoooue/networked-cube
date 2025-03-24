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
            foreach(CubeFace face in FACES)
            {
                DrawFace(pCamera, face);
            }
        }

        private void DrawFace(Camera pCamera, CubeFace currentFace)
        {
            // Get up vector
            Vector3 up = GetRelativeUpVector(currentFace);
            int[,] face = _CubeState.GetFace(currentFace);
            int rotationsNeeded = GetClockwiseRotationsNeeded(currentFace);
            face = RotateClockwiseNTimes(face, rotationsNeeded);

            for (int j = 0; j < 9; j++)
            {
                // up direction stays the same, but other two components change
                // Set the position of the face (cube is 3x3x3 with first cube at 0,0,0 and the opposite cube at 2,2,2)

                float x = SetXValue(currentFace, j);
                float y = SetYValue(currentFace, j);
                float z = SetZValue(currentFace, j);

                _Cube.SetPosition(new Vector3(x, y, z) + (0.5f * up));

                // Set the face colour based on the cube state
                int pieceValue = face[j / 3, j % 3];
                _Cube.SetColor(COLOURS[pieceValue]);

                // Scale cube so that the width is 0.2 in up direction
                _Cube.SetScale((Vector3.One - (0.95f * up)) * 0.80f);
                _Cube.Draw(pCamera);
            }
        }

        private int GetClockwiseRotationsNeeded(CubeFace face)
        {
            return 0;
        }

        private int[,] RotateClockwiseNTimes(int[,] face, int times)
        {
            int[,] result = face;
            for(int i=0; i<times; i++)
            {
                result = RotateFaceClockwise(result);
            }

            return result;
        }

        private int[,] RotateFaceClockwise(int[,] face)
        {
            int[,] result = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result[j, 2 - i] = face[i, j];
                }
            }

            return result;
        }

        private Vector3 GetRelativeUpVector(CubeFace face)
        {
            switch (face)
            {
                case CubeFace.Front:
                case CubeFace.Back:
                    return Vector3.UnitX;
                case CubeFace.Left:
                case CubeFace.Right:
                    return Vector3.UnitZ;
                case CubeFace.Bottom:
                case CubeFace.Top:
                    return Vector3.UnitY;
                default:
                    return Vector3.UnitX;
            }
        }

        public float SetXValue(CubeFace face, int j)
        {
            switch (face)
            {
                case CubeFace.Front: // Front
                    return -1;
                case CubeFace.Back: // Back
                    return 2;
                case CubeFace.Left: // Left
                    return j % 3;
                case CubeFace.Right: // Right
                    return j % 3;
                case CubeFace.Bottom: // Bottom
                    return j / 3;
                case CubeFace.Top: // Top
                default:
                    return j / 3;
            }
        }

        public float SetYValue(CubeFace face, int j)
        {
            switch (face)
            {
                case CubeFace.Front: // Front
                    return j / 3;
                case CubeFace.Back: // Back
                    return j / 3;
                case CubeFace.Left: // Left
                    return j / 3;
                case CubeFace.Right: // Right
                    return j / 3;
                case CubeFace.Bottom: // Bottom
                    return -1;
                case CubeFace.Top: // Top
                default:
                    return 2;
            }
        }

        public float SetZValue(CubeFace face, int j)
        {
            switch (face)
            {
                case CubeFace.Front: // Front
                    return j % 3;
                case CubeFace.Back: // Back
                    return j % 3;
                case CubeFace.Left: // Left
                    return -1;
                case CubeFace.Right: // Right
                    return 2;
                case CubeFace.Bottom: // Bottom
                    return j % 3;
                case CubeFace.Top: // Top
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
