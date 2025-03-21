using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceViewerCLI.PerformMoveStrategies
{
    internal class MoveViaAPIStrategy : IPerformMoveStrategy
    {
        private string _serverAddress = "http://localhost:5295";
        private static HttpClient Client = new HttpClient();

        public void PerformMove(string move)
        {
            Task<bool> task = PerformMoveAsync(move);
            task.Wait();
            Console.WriteLine($"verdict: {task.Result}");
        }

        private async Task<bool> PerformMoveAsync(string move)
        {
            string requestUri = $"{_serverAddress}/api/Cube/PerformMove?move={move}";
            HttpResponseMessage response = await Client.PostAsync(requestUri, null);
            return response.IsSuccessStatusCode;
        }
    }
}
