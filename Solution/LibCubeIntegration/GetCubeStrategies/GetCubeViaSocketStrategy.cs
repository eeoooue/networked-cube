namespace LibCubeIntegration.GetCubeStrategies;
using LibNetCube;
using PerformMoveStrategies;

public class GetCubeViaSocketStrategy : IGetCubeStrategy
{
    private MoveViaSocketStrategy _moveStrategy;

    public GetCubeViaSocketStrategy(string serviceName = "DummyService")
    {
        _moveStrategy = new MoveViaSocketStrategy(serviceName);
    }

    public async Task<CubeState?> GetCubeStateAsync()
    {
        return await _moveStrategy.SendMoveRequest("X");
    }
}
