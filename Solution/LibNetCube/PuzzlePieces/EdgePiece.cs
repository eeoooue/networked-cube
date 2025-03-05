using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNetCube.PuzzlePieces
{
    internal class EdgePiece : PuzzlePiece
    {
        public List<CubeFace> Faces { get; }
        public List<int> Values { get; set; }

        public EdgePiece(List<CubeFace> faces, List<int> values)
        {
            Faces = faces;
            Values = new List<int>();
        }

        public void FlipPiece()
        {
            int x = Values[0];
            int y = Values[1];
            Values.Clear();
            Values.Add(y);
            Values.Add(x);
        }

        public override int GetPieceFacing(CubeFace face)
        {
            for (int i = 0; i < 2; i++)
            {
                if (face == Faces[i])
                {
                    return Values[i];
                }
            }

            throw new ArgumentException();
        }

        public override void SetPieceFacing(CubeFace face, int value)
        {
            for (int i = 0; i < 2; i++)
            {
                if (face == Faces[i])
                {
                    Values[i] = value;
                }
            }

            throw new ArgumentException();
        }
    }
}
