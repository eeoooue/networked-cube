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
            if (IsTwoMove(move))
            {
                CubeMove baseMove = SimplifyComplexMove(move);
                PerformMove(baseMove);
                PerformMove(baseMove);
                return;
            }

            if (IsPrimeMove(move))
            {
                CubeMove baseMove = SimplifyComplexMove(move);
                PerformMove(baseMove);
                PerformMove(baseMove);
                PerformMove(baseMove);
                return;
            }

            if (move == CubeMove.M)
            {
                CubeState state = GetState();
                PuzzleRotation.RotateEntirePuzzle(state, "up");
                SetState(state);
                PerformMove(CubeMove.RPrime);
                PerformMove(CubeMove.L);
                return;
            }

            List<CubeFace> faces = [CubeFace.Top, CubeFace.Bottom, CubeFace.Left, CubeFace.Right, CubeFace.Front, CubeFace.Back];
            List<CubeMove> moves = [CubeMove.U, CubeMove.D, CubeMove.L, CubeMove.R, CubeMove.F, CubeMove.B];

            for(int i=0; i<6; i++)
            {
                if (move == moves[i])
                {
                    CubeState state = GetState();
                    FaceRotation.RotateFaceClockwise(state, faces[i]);
                    SetState(state);
                }
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
