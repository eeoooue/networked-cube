﻿using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using LibNetCube;
using LibCubeIntegration.GetCubeStrategies;
using Microsoft.AspNetCore.SignalR;
using LibCubeIntegration;


namespace CubeStatePublisher
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = BuildHost(args);
            await host.RunAsync();
        }

        private static IHost BuildHost(string[] args)
        {
            string baseAddress = NetworkingConfiguration.GetBaseAddressForProject("CubeStatePublisher");
            int portNumber = NetworkingConfiguration.GetPortForServer("CubeStatePublisher");
            string cubeHubUrl = $"{baseAddress}:{portNumber}";

            IHostBuilder builder = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(web =>
                {
                    web.UseUrls(cubeHubUrl);
                    web.ConfigureServices(services =>
                    {
                        services.AddSignalR();
                    });
                    web.Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapHub<CubeHub>("/cubehub");
                        });
                    });
                });

            return builder.Build();
        }
    }
}
