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

        public void SetState(CubeState state)
        {
            List<CubeFace> faces = CubeState.GetFaceNames();
            foreach(CubeFace face in faces)
            {
                ReplaceEntireFace(face, state.GetFace(face));
            }
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

        public void PerformMove(CubeMove move)
        {
            throw new NotImplementedException();
        }

        public void PerformMove(string move)
        {
            CubeMove parsedMove = MoveParser.ParseMove(move);
            PerformMove(parsedMove);
        }

        public void PerformMove(char move)
        {
            string s = move.ToString().ToUpper();
            char letter = s[0];

            Console.WriteLine($"Performing move {move}");

            CubeState state = GetState();

            if (letter == 'M')
            {
                // RotateMiddle(); // not implemented
            }

            if (letter == 'U')
            {
                FaceRotation.RotateFaceClockwise(state, CubeFace.Top);
            }
            else if (letter == 'D')
            {
                FaceRotation.RotateFaceClockwise(state, CubeFace.Bottom);
            }
            else if (letter == 'F')
            {
                FaceRotation.RotateFaceClockwise(state, CubeFace.Front);
            }
            else if (letter == 'B')
            {
                FaceRotation.RotateFaceClockwise(state, CubeFace.Back);
            }
            else if (letter == 'L')
            {
                FaceRotation.RotateFaceClockwise(state, CubeFace.Left);
            }
            else if (letter == 'R')
            {
                FaceRotation.RotateFaceClockwise(state, CubeFace.Right);
            }

            SetState(state);
        }


        private void RotateMiddle()
        {

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
    }
}
