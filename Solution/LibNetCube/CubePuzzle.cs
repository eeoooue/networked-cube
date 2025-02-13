namespace LibNetCube
{
    public class CubePuzzle
    {
        public const int FaceWidth = 3;

        private int[,] TopFace = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
        private int[,] BottomFace = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

        private int[,] LeftFace = { { 3, 3, 3 }, { 3, 3, 3 }, { 3, 3, 3 } };
        private int[,] RightFace = { { 4, 4, 4 }, { 4, 4, 4 }, { 4, 4, 4 } };

        private int[,] FrontFace = { { 2, 2, 2 }, { 2, 2, 2 }, { 2, 2, 2 } };
        private int[,] BackFace = { { 5, 5, 5 }, { 5, 5, 5 }, { 5, 5, 5 } };

        public CubeState GetState()
        {
            List<int[,]> reads = new List<int[,]>();
            List<CubeFace> faces = CubeState.GetFaceNames();
            foreach (CubeFace face in faces)
            {
                int[,] read = ReadFace(face);
                reads.Add(read);
            }

            return new CubeState(reads);
        }

        public int[,] ReadFace(CubeFace face)
        {
            int[,] result = new int[FaceWidth, FaceWidth];
            int[,] source = GetFaceRef(face);
            for(int i=0; i<FaceWidth; i++)
            {
                for(int j=0; j<FaceWidth; j++)
                {
                    result[i, j] = source[i, j];
                }
            }

            return result;
        }

        private int[,] GetFaceRef(CubeFace face)
        {
            switch (face)
            {
                case CubeFace.Back:
                    return BackFace;
                case CubeFace.Bottom:
                    return BottomFace;
                case CubeFace.Front:
                    return FrontFace;
                case CubeFace.Left:
                    return LeftFace;
                case CubeFace.Right:
                    return RightFace;
                default:
                    return TopFace;
            }
        }

        private void RotateTopFaceClockwise()
        {
            ReplaceFaceWithClockwiseRotation(CubeFace.Top);
            RotateTopRowsLeftwise(CubeFace.Front, CubeFace.Right, CubeFace.Back, CubeFace.Left);
        }

        private void ReplaceFaceWithClockwiseRotation(CubeFace face)
        {
            CubeState previous = GetState();
            int[,] prevTopFace = previous.GetFace(face);
            int[,] newTopFace = RotateFaceClockwise(prevTopFace);
            ReplaceEntireFace(face, newTopFace);
        }

        private void ReplaceEntireFace(CubeFace face, int[,] values)
        {
            int[,] destination = GetFaceRef(face);
            for (int i = 0; i < FaceWidth; i++)
            {
                for (int j = 0; j < FaceWidth; j++)
                {
                    destination[i, j] = values[i, j];
                }
            }
        }

        private void RotateTopRowsLeftwise(CubeFace a, CubeFace b, CubeFace c, CubeFace d)
        {
            int[] nextA = ReadTopRowOfFace(b);
            int[] nextB = ReadTopRowOfFace(c);
            int[] nextC = ReadTopRowOfFace(d);
            int[] nextD = ReadTopRowOfFace(a);
            ReplaceTopRowOfFace(a, nextA);
            ReplaceTopRowOfFace(b, nextB);
            ReplaceTopRowOfFace(c, nextC);
            ReplaceTopRowOfFace(d, nextD);
        }

        private void ReplaceTopRowOfFace(CubeFace face, int[] values)
        {
            int[,] destination = GetFaceRef(face);
            for (int j = 0; j < FaceWidth; j++)
            {
                destination[0, j] = values[j];
            }
        }

        private int[] ReadTopRowOfFace(CubeFace face)
        {
            int[] result = new int[FaceWidth];
            int[,] source = GetFaceRef(face);
            for (int j = 0; j < FaceWidth; j++)
            {
                result[j] = source[0, j];
            }

            return result;
        }

        public static int[,] RotateFaceClockwise(int[,] values)
        {
            int[,] result = new int[3, 3];

            // corners 
            result[0, 0] = values[2, 0];
            result[0, 2] = values[0, 0];
            result[2, 2] = values[0, 2];
            result[2, 0] = values[2, 2];

            // center point
            result[1, 1] = values[1, 1];

            // edge pieces
            result[0, 1] = values[1, 0];
            result[1, 2] = values[0, 1];
            result[2, 1] = values[1, 2];
            result[1, 0] = values[2, 1];

            return result;
        }

        private void RollForwards()
        {

        }

        private void RollBackwards()
        {
            RollForwards();
            RollForwards();
            RollForwards();
        }

        private void RollClockwise()
        {

        }

        private void RollAntiClockwise()
        {
            RollClockwise();
            RollClockwise();
            RollClockwise();
        }
    }
}
