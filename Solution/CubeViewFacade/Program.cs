using LibNetCube;
using LibCubeIntegration.GetCubeStrategies;
using System.Net;
using System.Net.Sockets;

namespace CubeViewFacade;
class Program
{
    static readonly TcpListener TcpListener = new(IPAddress.Parse("127.0.0.1"), 5002);
    static readonly Thread[] ServerThreads = new Thread[10];

    static GetCubeViaApiStrategy? _getCubeStrategy;
    static readonly CancellationTokenSource Cts = new();

    public static CubeState? State;

    static async Task Main()
    {
        _getCubeStrategy = new GetCubeViaApiStrategy();

        try
        {
            State = await _getCubeStrategy.GetCube()
                    ?? throw new InvalidOperationException("CubeState couldn't be retrieved");

            _ = Task.Run(() => StateUpdateTicker(Cts.Token));

            StartServer();

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

            Cts.Cancel();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in Main: {ex.Message}");
        }
    }

    static async Task StateUpdateTicker(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                if (_getCubeStrategy is not null)
                {
                    var newState = await _getCubeStrategy.GetCube();

                    if (newState is null)
                    {
                        Console.WriteLine("Failed to update state");
                    }
                    else
                    {
                        State = newState;
                        Console.WriteLine("Updated state successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during state update: {ex.Message}");
            }

            await Task.Delay(500, cancellationToken);
        }
    }

    static void StartServer()
    {
        TcpListener.Start();
        Console.WriteLine("[!] Server Active: ViewProxy @ port = 5002");

        for (var i = 0; i < ServerThreads.Length; i++)
        {
            ServerThreads[i] = new Thread(ServerThread) { IsBackground = true };
            ServerThreads[i].Start();
        }
    }

    static void ServerThread()
    {
        var worker = new CubeViewingHost(TcpListener);
        worker.Listen();
    }
}
