using LibCubeIntegration.GetCubeStrategies;
using LibCubeIntegration.PerformMoveStrategies;
using LibCubeIntegration.ResetCubeStrategies;
using LibCubeIntegration.Services;
using LibCubeIntegration.ShuffleCubeStrategies;
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
                case "CubeProxy":
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
                case "CubeProxy":
                    return new MoveViaApiStrategy(service);
                case "DummyService":
                    return new MoveViaSocketStrategy(service);
                default:
                    return new MoveViaApiStrategy(service);
            }
        }

        public static IResetCubeStrategy CreateResetStrategy(string service)
        {
            switch (service)
            {
                case "CubeService":
                case "CubeProxy":
                    return new ResetCubeViaAPIStrategy(service);
                case "DummyService":
                    return new ResetCubeViaSocketStrategy(service);
                default:
                    return new ResetCubeViaAPIStrategy(service);
            }
        }

        public static IShuffleCubeStrategy CreateShuffleStrategy(string service)
        {
            switch (service)
            {
                case "CubeService":
                case "CubeProxy":
                    return new ShuffleCubeViaAPIStrategy(service);
                case "DummyService":
                    return new ShuffleCubeViaSocketStrategy(service);
                default:
                    return new ShuffleCubeViaAPIStrategy(service);
            }
        }
    }
}
