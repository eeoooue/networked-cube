namespace LibCubeIntegration.GetCubeStrategies;
using LibNetCube;

public interface IGetCubeStrategy
{
    public Task<CubeState?> GetCube();
}
