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

        public void GetPieceLocation(in char x, out CubeFace face, out int i, out int j)
        {
            if ("AaBbCcDd".Contains(x))
            {
                GetPieceLocationOnTopFace(x, out face, out i, out j);
            }
            else if ("EFGHefgh".Contains(x))
            {
                GetPieceLocationOnFrontFace(x, out face, out i, out j);
            }
            else if("IJKLijkl".Contains(x))
            {
                GetPieceLocationOnRightFace(x, out face, out i, out j);
            }
            else if("MNOPmnop".Contains(x))
            {
                GetPieceLocationOnBackFace(x, out face, out i, out j);
            }
            else if("QRSTqrst".Contains(x))
            {
                GetPieceLocationOnLeftFace(x, out face, out i, out j);
            }
            else if ("UVWXuvwx".Contains(x))
            {
                GetPieceLocationOnBottomFace(x, out face, out i, out j);
            }
            else
            {
                throw new ArgumentException("Invalid piece indentifier");
            }
        }

        private void GetPieceLocationOnTopFace(in char x, out CubeFace face, out int i, out int j)
        {
            face = CubeFace.Top;
            string mapping = "AaBbCcDd";
            int index = mapping.IndexOf(x);
            GetLocationWithinFace(index, out i, out j);
        }

        private void GetPieceLocationOnFrontFace(in char x, out CubeFace face, out int i, out int j)
        {
            face = CubeFace.Front;
            string mapping = "EeFfGgHh";
            int index = mapping.IndexOf(x);
            GetLocationWithinFace(index, out i, out j);
        }

        private void GetPieceLocationOnRightFace(in char x, out CubeFace face, out int i, out int j)
        {
            face = CubeFace.Right;
            string mapping = "IiJjKkLl";
            int index = mapping.IndexOf(x);
            GetLocationWithinFace(index, out i, out j);
        }

        private void GetPieceLocationOnBackFace(in char x, out CubeFace face, out int i, out int j)
        {
            face = CubeFace.Back;
            string mapping = "MmNnOoPp";
            int index = mapping.IndexOf(x);
            GetLocationWithinFace(index, out i, out j);
        }

        private void GetPieceLocationOnLeftFace(in char x, out CubeFace face, out int i, out int j)
        {
            face = CubeFace.Left;
            string mapping = "QqRrSsTt";
            int index = mapping.IndexOf(x);
            GetLocationWithinFace(index, out i, out j);
        }

        private void GetPieceLocationOnBottomFace(in char x, out CubeFace face, out int i, out int j)
        {
            face = CubeFace.Bottom;
            string mapping = "UuVvWwXx";
            int index = mapping.IndexOf(x);
            GetLocationWithinFace(index, out i, out j);
        }

        private void GetLocationWithinFace(int index, out int i, out int j)
        {
            int[] iValues = [0, 0, 0, 1, 2, 2, 2, 1];
            int[] jValues = [0, 1, 2, 2, 2, 1, 0, 0];
            i = iValues[index];
            j = jValues[index];
        }

        public List<int> GetPieceValues(List<char> positions)
        {
            throw new NotImplementedException();
        }

        public void SetPieceValues(List<char> positions, List<int> values)
        {
            throw new NotImplementedException();
        }
    }
}
