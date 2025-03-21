using LibNetCube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceViewerCLI.GetCubeStrategies
{
    internal class GetCubeViaAPIStrategy : IGetCubeStrategy
    {
        private string _serverAddress = "http://localhost:5295";
        private static HttpClient Client = new HttpClient();

        public CubeState? GetCube()
        {
            Task<CubeState?> task = GetCubeAsync();
            task.Wait();

            return task.Result;
        }

        private async Task<CubeState?> GetCubeAsync()
        {
            List<string> faces = ["Back", "Bottom", "Front", "Left", "Right", "Top"];
            List<int[,]> faceMatrices = new List<int[,]>();

            foreach (string face in faces)
            {
                int[,]? result = await GetFaceAsync(face);
                if (result is int[,] matrix)
                {
                    faceMatrices.Add(matrix);
                }
                else
                {
                    return null;
                }
            }

            if (faceMatrices.Count == 6)
            {
                return new CubeState(faceMatrices);
            }
            else
            {
                return null;
            }
        }

        private async Task<int[,]?> GetFaceAsync(string face)
        {
            string requestUri = $"{_serverAddress}/api/Cube/Face?face={face}";
            HttpResponseMessage response = await Client.GetAsync(requestUri);
            string responseString = await response.Content.ReadAsStringAsync();
            return ExtractFaceMatrix(responseString);
        }

        private int[,]? ExtractFaceMatrix(string response)
        {
            try
            {
                response = response.Substring(1, response.Length - 2);
                string[] values = response.Split(',');
                int[,] result = new int[3, 3];

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int p = (i*3) + j;
                        result[i, j] = int.Parse(values[p]);
                    }
                }

                return result;
            }
            catch
            {
                return null;
            }
        }

    }
}
