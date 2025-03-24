namespace FaceViewerCLI;
using LibCubeIntegration.GetCubeStrategies;
using LibCubeIntegration.PerformMoveStrategies;
using LibCubeIntegration.Services;
using LibNetCube;

class Program
{
    static readonly FacePresenter Presenter = new();
    static CubeState _cubeState = new(new CubePuzzle().GetState());

    static readonly CubeServiceFacade _cubeService = new(
        new GetCubeViaApiStrategy(),
        new MoveViaApiStrategy()
    );

    static async Task Main()
    {
        while (true)
        {
            if (await _cubeService.GetStateAsync() is { } state)
            {
                _cubeState = state;
            }
            else
            {
                Console.WriteLine("Lost connection to server.");
                Thread.Sleep(500);
                Console.WriteLine("Attempting to reconnect...");
                continue;
            }

            Presenter.PresentCube(_cubeState);

            Console.WriteLine("Enter a move to be performed...");
            if (Console.ReadLine() is { } message) await _cubeService.PerformMoveAsync(message);
        }
    }
}
