using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNetCube
{
    internal static class CubeMoveMap
    {
        public static readonly Dictionary<string, CubeMove> MoveMap = new Dictionary<string, CubeMove>
        {
            // Standard moves
            { "U", CubeMove.U },
            { "D", CubeMove.D },
            { "L", CubeMove.L },
            { "R", CubeMove.R },
            { "F", CubeMove.F },
            { "B", CubeMove.B },
            { "M", CubeMove.M },
            
            // Prime moves
            { "U'", CubeMove.UPrime },
            { "D'", CubeMove.DPrime },
            { "L'", CubeMove.LPrime },
            { "R'", CubeMove.RPrime },
            { "F'", CubeMove.FPrime },
            { "B'", CubeMove.BPrime },
            { "M'", CubeMove.MPrime },
            
            // Double moves
            { "U2", CubeMove.TwoU },
            { "D2", CubeMove.TwoD },
            { "L2", CubeMove.TwoL },
            { "R2", CubeMove.TwoR },
            { "F2", CubeMove.TwoF },
            { "B2", CubeMove.TwoB },
            { "M2", CubeMove.TwoM }
        };
    }
} 