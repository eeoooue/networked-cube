using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNetCube
{
    internal class PochmanWrapper
    {
        private CubeState State;

        public PochmanWrapper(CubeState state)
        {
            State = state;
        }

        public int GetPiece(char x)
        {
            throw new NotImplementedException();
        }

        public int GetPiece(CubeFace face, int i, int j)
        {
            throw new NotImplementedException();
        }

        public void SetPiece(char x, int value)
        {
            throw new NotImplementedException();
        }

        public void SetPiece(CubeFace face, int i, int j, int value)
        {
            throw new NotImplementedException();
        }

        public void GetPieceLocation(CubeFace face, int i, int j)
        {
            throw new NotImplementedException();
        }

        public List<int> GetPieceValues(char x)
        {
            throw new NotImplementedException();
        }

        public void SetPieceValues(List<char> positions, List<int> values)
        {
            throw new NotImplementedException();
        }
    }
}
