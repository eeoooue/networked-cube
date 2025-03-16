using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNetCube
{
    /// <summary>
    /// Contains move opposition groups for cube moves
    /// </summary>
    internal static class CubeMoveOppositionGroups
    {
        /// <summary>
        /// Dictionary mapping each move to an array of moves that shouldn't follow it
        /// </summary>
        public static readonly Dictionary<CubeMove, CubeMove[]> OppositionGroups = new()
        {
            { CubeMove.U, new[] { CubeMove.U, CubeMove.UPrime, CubeMove.TwoU } },
            { CubeMove.D, new[] { CubeMove.D, CubeMove.DPrime, CubeMove.TwoD } },
            { CubeMove.L, new[] { CubeMove.L, CubeMove.LPrime, CubeMove.TwoL } },
            { CubeMove.R, new[] { CubeMove.R, CubeMove.RPrime, CubeMove.TwoR } },
            { CubeMove.F, new[] { CubeMove.F, CubeMove.FPrime, CubeMove.TwoF } },
            { CubeMove.B, new[] { CubeMove.B, CubeMove.BPrime, CubeMove.TwoB } },
            { CubeMove.M, new[] { CubeMove.M, CubeMove.MPrime, CubeMove.TwoM } },
            
            { CubeMove.UPrime, new[] { CubeMove.U, CubeMove.UPrime, CubeMove.TwoU } },
            { CubeMove.DPrime, new[] { CubeMove.D, CubeMove.DPrime, CubeMove.TwoD } },
            { CubeMove.LPrime, new[] { CubeMove.L, CubeMove.LPrime, CubeMove.TwoL } },
            { CubeMove.RPrime, new[] { CubeMove.R, CubeMove.RPrime, CubeMove.TwoR } },
            { CubeMove.FPrime, new[] { CubeMove.F, CubeMove.FPrime, CubeMove.TwoF } },
            { CubeMove.BPrime, new[] { CubeMove.B, CubeMove.BPrime, CubeMove.TwoB } },
            { CubeMove.MPrime, new[] { CubeMove.M, CubeMove.MPrime, CubeMove.TwoM } },
            
            { CubeMove.TwoU, new[] { CubeMove.U, CubeMove.UPrime, CubeMove.TwoU } },
            { CubeMove.TwoD, new[] { CubeMove.D, CubeMove.DPrime, CubeMove.TwoD } },
            { CubeMove.TwoL, new[] { CubeMove.L, CubeMove.LPrime, CubeMove.TwoL } },
            { CubeMove.TwoR, new[] { CubeMove.R, CubeMove.RPrime, CubeMove.TwoR } },
            { CubeMove.TwoF, new[] { CubeMove.F, CubeMove.FPrime, CubeMove.TwoF } },
            { CubeMove.TwoB, new[] { CubeMove.B, CubeMove.BPrime, CubeMove.TwoB } },
            { CubeMove.TwoM, new[] { CubeMove.M, CubeMove.MPrime, CubeMove.TwoM } }
        };
    }
} 