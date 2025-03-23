namespace LibCubeIntegration.PerformMoveStrategies;
public interface IPerformMoveStrategy
{
    public Task<bool> PerformMoveAsync(string move);
}
