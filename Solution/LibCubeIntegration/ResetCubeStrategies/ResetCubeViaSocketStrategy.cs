using LibCubeIntegration.PerformMoveStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCubeIntegration.ResetCubeStrategies
{
    public class ResetCubeViaSocketStrategy : IResetCubeStrategy
    {
        private MoveViaSocketStrategy MoveStrategy;

        public ResetCubeViaSocketStrategy(string serviceName = "DummyService")
        {
            MoveStrategy = new MoveViaSocketStrategy(serviceName);
        }

        public async Task<bool> ResetCubeAsync()
        {
            return await MoveStrategy.PerformMoveAsync("RESET");
        }
    }
}
