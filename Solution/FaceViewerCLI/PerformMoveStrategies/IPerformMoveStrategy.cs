using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceViewerCLI.PerformMoveStrategies
{
    internal interface IPerformMoveStrategy
    {
        public void PerformMove(string move);
    }
}
