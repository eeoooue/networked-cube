using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCubeIntegration.ShuffleCubeStrategies
{
    internal class ShuffleCubeViaAPIStrategy : IShuffleCubeStrategy
    {
        private string ServerAddress;
        static readonly HttpClient Client = new();

        public ShuffleCubeViaAPIStrategy(string serviceName = "CubeService")
        {
            ServerAddress = NetworkingConfiguration.GetFullAddressForServer(serviceName);
        }

        public async Task<bool> ShuffleCubeAsync()
        {
            return await ShuffleCubeAsync("");
        }

        public async Task<bool> ShuffleCubeAsync(string moveString)
        {
            var requestUri = $"{ServerAddress}/api/Cube/ApplyShuffle?shuffle={moveString}";
            var response = await Client.PostAsync(requestUri, null);
            return response.IsSuccessStatusCode;
        }
    }
}
