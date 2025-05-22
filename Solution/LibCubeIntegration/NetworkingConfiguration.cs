using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCubeIntegration
{
    public static class NetworkingConfiguration
    {
        private static string BaseAddress = "";

        public static string GetAddressForService(string service)
        {
            return "";
        }

        public static int GetPortForService(string service)
        {
            switch (service)
            {
                default:
                    return 0;
            }
        }
    }
}
