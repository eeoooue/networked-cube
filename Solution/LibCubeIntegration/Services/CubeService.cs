namespace LibCubeIntegration.Services;
using GetCubeStrategies;
using LibNetCube;
using PerformMoveStrategies;

public class CubeService(IGetCubeStrategy getCubeStrategy, IPerformMoveStrategy performMoveStrategy)
{
    public async Task<CubeState?> GetStateAsync()
    {
        return await getCubeStrategy.GetCubeStateAsync();
    }

    public async Task PerformMoveAsync(string move)
    {
        await performMoveStrategy.PerformMoveAsync(move);
    }
}
