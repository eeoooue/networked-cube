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
            // double moves

            if (IsTwoMove(move))
            {
                CubeMove baseMove = SimplifyComplexMove(move);
                PerformMove(baseMove);
                PerformMove(baseMove);
            }

            if (IsPrimeMove(move))
            {
                CubeMove baseMove = SimplifyComplexMove(move);
                PerformMove(baseMove);
                PerformMove(baseMove);
                PerformMove(baseMove);
            }

            CubeState state = GetState();

            if (move == CubeMove.M)
            {
                PuzzleRotation.RotateEntirePuzzle(state, "up");
                PerformMove(CubeMove.RPrime);
                PerformMove(CubeMove.L);
            }

            if (move == CubeMove.U)
            {
                FaceRotation.RotateFaceClockwise(state, CubeFace.Top);
            }

            if (move == CubeMove.D)
            {
                FaceRotation.RotateFaceClockwise(state, CubeFace.Bottom);
            }

            if (move == CubeMove.L)
            {
                FaceRotation.RotateFaceClockwise(state, CubeFace.Left);
            }

            if (move == CubeMove.R)
            {
                FaceRotation.RotateFaceClockwise(state, CubeFace.Right);
            }

            if (move == CubeMove.F)
            {
                FaceRotation.RotateFaceClockwise(state, CubeFace.Front);
            }

            if (move == CubeMove.B)
            {
                FaceRotation.RotateFaceClockwise(state, CubeFace.Back);
            }
        }

        private bool IsTwoMove(CubeMove move)
        {
            List<CubeMove> moves = [CubeMove.TwoU, CubeMove.TwoD, CubeMove.TwoL, CubeMove.TwoR, CubeMove.TwoF, CubeMove.TwoB, CubeMove.TwoM];
            return moves.Contains(move);
        }

        private bool IsPrimeMove(CubeMove move)
        {
            List<CubeMove> moves = [CubeMove.UPrime, CubeMove.DPrime, CubeMove.LPrime, CubeMove.RPrime, CubeMove.FPrime, CubeMove.BPrime, CubeMove.MPrime];
            return moves.Contains(move);
        }

        private CubeMove SimplifyComplexMove(CubeMove move)
        {
            switch (move)
            {
                case CubeMove.TwoU:
                case CubeMove.UPrime:
                    return CubeMove.U;
                case CubeMove.TwoD:
                case CubeMove.DPrime:
                    return CubeMove.D;
                case CubeMove.TwoL:
                case CubeMove.LPrime:
                    return CubeMove.L;
                case CubeMove.TwoR:
                case CubeMove.RPrime:
                    return CubeMove.R;
                case CubeMove.TwoF:
                case CubeMove.FPrime:
                    return CubeMove.F;
                case CubeMove.TwoB:
                case CubeMove.BPrime:
                    return CubeMove.B;
                case CubeMove.TwoM:
                case CubeMove.MPrime:
                    return CubeMove.M;
                default:
                    throw new ArgumentException();
            }
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
                PuzzleRotation.RotateEntirePuzzle(state, "up");

                FaceRotation.RotateFaceClockwise(state, CubeFace.Right);
                FaceRotation.RotateFaceClockwise(state, CubeFace.Right);
                FaceRotation.RotateFaceClockwise(state, CubeFace.Right);

                FaceRotation.RotateFaceClockwise(state, CubeFace.Left);
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
