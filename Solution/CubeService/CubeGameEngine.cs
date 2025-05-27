using CubeService.Models;
using LibNetCube;

namespace CubeService
{
    public class CubeGameEngine
    {
        private readonly CubePuzzle _cubePuzzle;
        private readonly MoveTransactionRepository _repo;
        private readonly object _lock = new();

        public CubeGameEngine()
        {
            _cubePuzzle = new CubePuzzle();
            _repo = new MoveTransactionRepository();
            HydrateCubeState();
        }

        private void HydrateCubeState()
        {
            IEnumerable<MoveTransaction> moves = _repo.GetAllMoves();
            foreach (MoveTransaction move in moves)
            {
                if (Enum.TryParse<CubeMove>(move.MovePerformed, out var parsed))
                {
                    _cubePuzzle.PerformMove(parsed);
                }
            }
        }

        public void PerformMove(CubeMove move)
        {
            lock (_lock)
            {
                _cubePuzzle.PerformMove(move);
                _repo.InsertMove(move);
            }
        }

        public void PerformMoveSeries(IEnumerable<CubeMove> moves)
        {
            lock (_lock)
            {
                foreach(CubeMove move in moves)
                {
                    _cubePuzzle.PerformMove(move);
                }
                _repo.InsertMoves(moves);
            }
        }

        public void Reset()
        {
            lock (_lock)
            {
                _cubePuzzle.Reset();
                _repo.ClearAllMoves();
            }
        }

        public CubeState GetCubeState()
        {
            CubeState result;
            lock (_lock)
            {
                result = _cubePuzzle.GetState();
            }

            return result;
        }

        public int[,] GetCubeFace(CubeFace face)
        {
            int[,] result;
            lock (_lock)
            {
                result = _cubePuzzle.ReadFace(face);
            }

            return result;
        }
    }
}
