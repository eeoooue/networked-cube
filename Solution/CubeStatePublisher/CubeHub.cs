using LibCubeIntegration;
using LibCubeIntegration.GetCubeStrategies;
using Microsoft.AspNetCore.SignalR;

namespace CubeStatePublisher
{
    public class CubeHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Client connected: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Client disconnected: {Context.ConnectionId}");
            return base.OnDisconnectedAsync(exception);
        }

        public async Task BroadcastState(JsonFriendlyCubeState state)
        {
            await Clients.All.SendAsync("CubeStateUpdated", state);
        }

        public async Task RequestState()
        {
            // Ideally inject a state source, but for now call the strategy directly:
            var strategy = new GetCubeViaApiStrategy("CubeProxy");
            var state = await strategy.GetCubeStateAsync();

            if (state != null)
            {
                var dto = new JsonFriendlyCubeState(state);
                await Clients.Caller.SendAsync("CubeStateUpdated", dto);
            }
        }

    }
}
