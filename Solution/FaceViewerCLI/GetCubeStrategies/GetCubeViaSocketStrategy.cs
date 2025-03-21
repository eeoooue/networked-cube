using FaceViewerCLI.PerformMoveStrategies;
using LibNetCube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceViewerCLI.GetCubeStrategies
{
    internal class GetCubeViaSocketStrategy : IGetCubeStrategy
    {
        public CubeState? GetCube()
        {
            MoveViaSocketStrategy strategy = new MoveViaSocketStrategy();
            CubeState? result = strategy.SendMoveRequest("X");

            return result;
        }
    }
}
