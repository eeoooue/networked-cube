using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;


namespace LibCubeIntegration.Services
{
    public static class CubePublisherService
    {
        public static HubConnection CreateHubConnection()
        {
            IHubConnectionBuilder builder = new HubConnectionBuilder();

            string baseAddress = NetworkingConfiguration.GetBaseAddressForProject("CubeStatePublisher");
            int portNumber = NetworkingConfiguration.GetPortForServer("CubeStatePublisher");
            string cubeHubUrl = $"{baseAddress}:{portNumber}/cubehub";

            builder = builder.WithUrl(cubeHubUrl);
            builder = builder.WithAutomaticReconnect();
            HubConnection connection = builder.Build();

            return connection;
        }
    }
}
