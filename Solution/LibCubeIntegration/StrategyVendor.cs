using LibCubeIntegration.GetCubeStrategies;
using LibCubeIntegration.PerformMoveStrategies;
using LibCubeIntegration.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCubeIntegration
{
    public static class StrategyVendor
    {
        public static IGetCubeStrategy CreateGetCubeStrategy(string service)
        {
            switch (service)
            {
                case "CubeService":
                    return new GetCubeViaApiStrategy(service);
                case "DummyService":
                    return new GetCubeViaSocketStrategy(service);
                default:
                    return new GetCubeViaApiStrategy(service);
            }
        }

        public static IPerformMoveStrategy CreateMoveStrategy(string service)
        {
            switch (service)
            {
                case "CubeService":
                    return new MoveViaApiStrategy(service);
                case "DummyService":
                    return new MoveViaSocketStrategy(service);
                default:
                    return new MoveViaApiStrategy(service);
            }
        }
    }
}
