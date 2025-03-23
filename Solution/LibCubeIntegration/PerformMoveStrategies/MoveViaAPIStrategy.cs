namespace LibCubeIntegration.PerformMoveStrategies;
public class MoveViaApiStrategy : IPerformMoveStrategy
{
    static readonly HttpClient Client = new();
    const string ServerAddress = "http://localhost:5295";

    public async Task<bool> PerformMoveAsync(string move)
    {
        var requestUri = $"{ServerAddress}/api/Cube/PerformMove?move={move}";
        var response = await Client.PostAsync(requestUri, null);
        return response.IsSuccessStatusCode;
    }
}
