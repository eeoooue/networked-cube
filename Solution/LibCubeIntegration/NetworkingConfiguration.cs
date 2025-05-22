using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCubeIntegration
{
    public static class NetworkingConfiguration
    {
        private static string BaseAddress = "http://localhost";

        public static string GetAddressForServer(string project)
        {
            int port = GetPortForServer(project);
            return $"{BaseAddress}:{port}";
        }

        public static int GetPortForClient(string project)
        {
            switch (project)
            {
                default:
                    return 0;
            }
        }

        public static int GetPortForServer(string project)
        {
            switch (project)
            {
                case "CubeService":
                    return 5295;
                default:
                    return 0;
            }
        }
    }
}
