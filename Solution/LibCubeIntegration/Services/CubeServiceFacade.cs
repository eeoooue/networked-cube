namespace LibCubeIntegration.Services;
using GetCubeStrategies;
using LibNetCube;
using PerformMoveStrategies;

public class CubeServiceFacade
{
    private IGetCubeStrategy GetCubeStrategy;
    private IPerformMoveStrategy PerformMoveStrategy;

    public CubeServiceFacade(IGetCubeStrategy getCubeStrategy, IPerformMoveStrategy performMoveStrategy)
    {
        GetCubeStrategy = getCubeStrategy;
        PerformMoveStrategy = performMoveStrategy;
    }

    public CubeServiceFacade(string service)
    {
        GetCubeStrategy = StrategyVendor.CreateGetCubeStrategy(service);
        PerformMoveStrategy = StrategyVendor.CreateMoveStrategy(service);
    }

    public async Task<CubeState?> GetStateAsync()
    {
        return await GetCubeStrategy.GetCubeStateAsync();
    }

    public async Task PerformMoveAsync(string move)
    {
        await PerformMoveStrategy.PerformMoveAsync(move);
    }
}
