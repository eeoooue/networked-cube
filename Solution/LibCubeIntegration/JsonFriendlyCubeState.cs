using LibNetCube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCubeIntegration
{
    public class JsonFriendlyCubeState
    {
        public Dictionary<string, int[][]> Faces { get; set; }

        public JsonFriendlyCubeState()
        {
            Faces = new Dictionary<string, int[][]>();
        }

        public JsonFriendlyCubeState(CubeState state)
        {
            Faces = CubeState.GetFaceNames().ToDictionary(
                face => face.ToString(),
                face => ToJagged(state.GetFace(face))
            );
        }

        public CubeState ToCubeState()
        {
            var matrices = CubeState.GetFaceNames().Select(face =>
            {
                if (Faces.TryGetValue(face.ToString(), out var jagged))
                    return ToRectangular(jagged);
                else
                    throw new Exception($"Missing face: {face}");
            }).ToList();

            return new CubeState(matrices);
        }

        private static int[][] ToJagged(int[,] array)
        {
            var result = new int[array.GetLength(0)][];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                result[i] = new int[array.GetLength(1)];
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    result[i][j] = array[i, j];
                }
            }
            return result;
        }

        private static int[,] ToRectangular(int[][] array)
        {
            int rows = array.Length;
            int cols = array[0].Length;
            var result = new int[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i, j] = array[i][j];
            return result;
        }
    }

}
