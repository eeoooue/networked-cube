namespace LibCubeIntegration.GetCubeStrategies;
using LibNetCube;
using PerformMoveStrategies;

public class GetCubeViaSocketStrategy : IGetCubeStrategy
{
    public async Task<CubeState?> GetCube()
    {
        return await MoveViaSocketStrategy.SendMoveRequest("X");
    }
}
