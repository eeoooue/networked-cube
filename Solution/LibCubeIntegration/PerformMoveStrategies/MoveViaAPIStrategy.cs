namespace LibCubeIntegration.PerformMoveStrategies;
public class MoveViaApiStrategy : IPerformMoveStrategy
{
    private string ServerAddress;
    static readonly HttpClient Client = new();

    public MoveViaApiStrategy(string serviceName = "CubeService")
    {
        ServerAddress = NetworkingConfiguration.GetFullAddressForServer(serviceName);
    }

    public async Task<bool> PerformMoveAsync(string move)
    {
        var requestUri = $"{ServerAddress}/api/Cube/PerformMove?move={move}";
        var response = await Client.PostAsync(requestUri, null);
        return response.IsSuccessStatusCode;
    }
}
