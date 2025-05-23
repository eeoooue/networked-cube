using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCubeIntegration.ResetCubeStrategies
{
    public interface IResetCubeStrategy
    {
        public Task<bool> ResetCubeAsync();
    }
}
