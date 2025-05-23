using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCubeIntegration.ShuffleCubeStrategies
{
    public interface IShuffleCubeStrategy
    {
        public Task<bool> ShuffleCubeAsync();

        public Task<bool> ShuffleCubeAsync(string move);
    }
}
