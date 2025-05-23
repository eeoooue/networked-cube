using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCubeIntegration
{
    public static class NetworkingConfiguration
    {
        public static string BaseAddress = "http://localhost";

        public static string GetFullAddressForServer(string project)
        {
            int port = GetPortForServer(project);
            return $"{BaseAddress}:{port}";
        }

        public static string GetBaseAddressForProject(string project)
        {
            switch (project)
            {
                case "DummyService":
                    return "127.0.0.1";
                case "CubeStatePublisher":
                case "CubeService":
                default:
                    return "http://localhost";
            }
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
                case "DummyService":
                    return 5000;
                case "CubeProxy":
                    return 5290;
                case "CubeService":
                    return 5295;
                case "CubeStatePublisher":
                    return 5002;
                default:
                    return 0;
            }
        }
    }
}
