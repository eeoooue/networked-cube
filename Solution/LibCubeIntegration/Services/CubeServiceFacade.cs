namespace LibCubeIntegration.Services;
using GetCubeStrategies;
using LibCubeIntegration.ResetCubeStrategies;
using LibNetCube;
using PerformMoveStrategies;

public class CubeServiceFacade
{
    private IGetCubeStrategy GetCubeStrategy;
    private IPerformMoveStrategy PerformMoveStrategy;
    private IResetCubeStrategy ResetCubeStrategy;

    public CubeServiceFacade(IGetCubeStrategy getCubeStrategy, IPerformMoveStrategy performMoveStrategy)
    {
        GetCubeStrategy = getCubeStrategy;
        PerformMoveStrategy = performMoveStrategy;
        ResetCubeStrategy = new ResetCubeViaAPIStrategy();
    }

    public CubeServiceFacade(string service)
    {
        GetCubeStrategy = StrategyVendor.CreateGetCubeStrategy(service);
        PerformMoveStrategy = StrategyVendor.CreateMoveStrategy(service);
        ResetCubeStrategy = StrategyVendor.CreateResetStrategy(service);
    }

    public async Task<CubeState?> GetStateAsync()
    {
        return await GetCubeStrategy.GetCubeStateAsync();
    }

    public async Task PerformMoveAsync(string move)
    {
        await PerformMoveStrategy.PerformMoveAsync(move);
    }

    public async Task ResetCubeAsync()
    {
        await ResetCubeStrategy.ResetCubeAsync();
    }
}
