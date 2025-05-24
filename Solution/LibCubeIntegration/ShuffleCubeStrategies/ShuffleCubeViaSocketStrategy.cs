using LibCubeIntegration.PerformMoveStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCubeIntegration.ShuffleCubeStrategies
{
    internal class ShuffleCubeViaSocketStrategy : IShuffleCubeStrategy
    {
        private MoveViaSocketStrategy MoveStrategy;

        public ShuffleCubeViaSocketStrategy(string serviceName = "DummyService")
        {
            MoveStrategy = new MoveViaSocketStrategy(serviceName);
        }

        public async Task<bool> ShuffleCubeAsync()
        {
            return await MoveStrategy.PerformMoveAsync("SHUFFLE");
        }

        public async Task<bool> ShuffleCubeAsync(string move)
        {
            return await MoveStrategy.PerformMoveAsync(move);
        }
    }
}
