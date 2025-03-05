using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNetCube
{
    internal abstract class PuzzlePiece
    {
        public abstract int GetPieceFacing(CubeFace face);

        public abstract void SetPieceFacing(CubeFace face, int value);
    }
}
