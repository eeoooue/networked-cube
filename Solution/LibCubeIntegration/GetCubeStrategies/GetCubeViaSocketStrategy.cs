namespace LibCubeIntegration.GetCubeStrategies;
using LibNetCube;
using PerformMoveStrategies;

public class GetCubeViaSocketStrategy : IGetCubeStrategy
{
    public async Task<CubeState?> GetCubeStateAsync()
    {
        return await MoveViaSocketStrategy.SendMoveRequest("X");
    }
}
