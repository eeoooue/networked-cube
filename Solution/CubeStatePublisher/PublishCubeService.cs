using LibCubeIntegration.GetCubeStrategies;
using LibCubeIntegration;
using LibNetCube;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeStatePublisher
{
    public class PublishCubeService : BackgroundService
    {
        private readonly IHubContext<CubeHub> _hubContext;
        private readonly IGetCubeStrategy _getCubeStrategy;
        private readonly TimeSpan _pollingInterval = TimeSpan.FromMilliseconds(200);

        public PublishCubeService(IHubContext<CubeHub> hubContext)
        {
            _hubContext = hubContext;
            _getCubeStrategy = new GetCubeViaApiStrategy("CubeProxy");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CubeState? previousState = null;

            while (!stoppingToken.IsCancellationRequested)
            {
                var newState = await _getCubeStrategy.GetCubeStateAsync();

                if (newState is not null && !newState.Equals(previousState))
                {
                    var dto = new JsonFriendlyCubeState(newState);
                    await _hubContext.Clients.All.SendAsync("CubeStateUpdated", dto, stoppingToken);
                    previousState = newState;
                }

                await Task.Delay(_pollingInterval, stoppingToken);
            }
        }
    }
}
