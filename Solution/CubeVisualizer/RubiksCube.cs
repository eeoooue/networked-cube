using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CubeVisualizer
{
    class RubiksCube
    {
        readonly Object3D _Cube;

        private enum Colours { White, Yellow, Red, Blue, Green, Orange }
        static private readonly Color[] COLOURS = { Color.White, Color.Yellow, Color.Red, Color.Blue, Color.Green, Color.Orange };
        Colours[,] _CubeState = new Colours[9, 6];

        public RubiksCube(Model pCube)
        {
            _Cube = new Object3D(pCube, Color.White);

            // Set the initial state of the cube
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    _CubeState[j, i] = (Colours)i;
                }
            }
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

                for (int j = 0; j < 9; j++)
                {
                    // up direction stays the same, but other two components change
                    float x = 0, y = 0, z = 0;
                    // Set the position of the face (cube is 3x3x3 with first cube at 0,0,0 and the opposite cube at 2,2,2)
                    switch (i)
                    {
                        case 0:
                            x = -1;
                            y = j / 3;
                            z = j % 3;
                            break;
                        case 1:
                            x = 2;
                            y = j / 3;
                            z = j % 3;
                            break;
                        case 2:
                            x = j % 3;
                            y = j / 3;
                            z = -1;
                            break;
                        case 3:
                            x = j % 3;
                            y = j / 3;
                            z = 2;
                            break;
                        case 4:
                            x = j % 3;
                            y = -1;
                            z = j / 3;
                            break;
                        case 5:
                            x = j % 3;
                            y = 2;
                            z = j / 3;
                            break;
                    }
                    _Cube.SetPosition(new Vector3(x, y, z) + (0.5f * up));

                    // Set the face colour based on the cube state
                    _Cube.SetColor(COLOURS[(int)_CubeState[j, i]]);

                    // Scale cube so that the width is 0.2 in up direction
                    _Cube.SetScale((Vector3.One - (0.95f * up)) * 0.80f);
                    _Cube.Draw(pCamera);
                }
            }
        }

        public void Draw(Camera pCamera)
        {
            DrawCubes(pCamera);
            DrawFaces(pCamera);
        }
    }
}
