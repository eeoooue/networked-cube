using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCubeIntegration.ResetCubeStrategies
{
    public class ResetCubeViaAPIStrategy : IResetCubeStrategy
    {
        private string ServerAddress;
        static readonly HttpClient Client = new();

        public ResetCubeViaAPIStrategy(string serviceName = "CubeService")
        {
            ServerAddress = NetworkingConfiguration.GetFullAddressForServer(serviceName);
        }

        public async Task<bool> ResetCubeAsync()
        {
            var requestUri = $"{ServerAddress}/api/Cube/Reset";
            var response = await Client.PostAsync(requestUri, null);
            return response.IsSuccessStatusCode;
        }
    }
}
