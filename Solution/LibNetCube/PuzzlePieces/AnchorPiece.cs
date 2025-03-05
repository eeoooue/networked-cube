using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNetCube.PuzzlePieces
{
    internal class AnchorPiece : PuzzlePiece
    {
        public CubeFace Face { get; }
        public int Value { get; set; }
        
        public AnchorPiece(CubeFace face, int value)
        {
            Face = face;
            Value = value;
        }

        public override int GetPieceFacing(CubeFace face)
        {
            if (face == Face)
            {
                return Value;
            }

            throw new ArgumentException();
        }

        public override void SetPieceFacing(CubeFace face, int value)
        {
            if (face == Face)
            {
                Value = value;
            }

            throw new ArgumentException();
        }
    }
}
