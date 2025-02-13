using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibNetCube;

namespace FaceViewerCLI
{
    public class FacePresenter
    {
        public CubeFace ActiveFace = CubeFace.Left;

        public void PresentCube(CubeState state)
        {
            PresentFace(state, CubeFace.Top);

            PresentFace(state, CubeFace.Front);

            PresentFace(state, CubeFace.Bottom);

            PresentFace(state, CubeFace.Back);

            PresentFace(state, CubeFace.Left);

            PresentFace(state, CubeFace.Right);
        }

        public void PresentFace(CubeState state)
        {
            PresentFace(state, ActiveFace);
        }

        public void PresentFace(CubeState state, CubeFace face)
        {
            Console.WriteLine($"Presenting {face.ToString()}");
            int[,] values = state.GetFace(face);
            PresentFace(values);
        }

        public void PresentFace(int[,] values)
        {
            for(int i=0; i<3; i++)
            {
                PresentRow(values, i);
            }
            Console.WriteLine();
        }

        public void PresentRow(int[,] values, int i)
        {
            foreach(int x in values)
            {
                PresentCell(x);
            }
            Console.WriteLine();
        }

        public void PresentCell(int i)
        {
            ConsoleColor colour = GetColour(i);
            Console.ForegroundColor = colour;
            Console.BackgroundColor = colour;

            Console.Write('X');

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public ConsoleColor GetColour(int i)
        {
            switch (i)
            {
                case 1:
                    return ConsoleColor.Yellow;
                case 2:
                    return ConsoleColor.Green;
                case 3:
                    return ConsoleColor.Blue;
                case 4:
                    return ConsoleColor.Red;
                case 5:
                    return ConsoleColor.Magenta;
                default:
                    return ConsoleColor.White;
            }
        }

    }
}
