using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using FaceViewerCLI.GetCubeStrategies;
using FaceViewerCLI.PerformMoveStrategies;
using LibNetCube;

namespace FaceViewerCLI
{
    internal class Program
    {
        private static FacePresenter Presenter = new FacePresenter();
        private static CubeState CubeState = new CubeState(new CubePuzzle().GetState());

        private static IPerformMoveStrategy PerformMoveStrategy = new MoveViaSocketStrategy();
        private static IGetCubeStrategy GetCubeStrategy = new GetCubeViaSocketStrategy();

        static void Main(string[] args)
        {
            while (true)
            {
                if (GetCubeStrategy.GetCube() is CubeState state)
                {
                    CubeState = state;
                }
                else
                {
                    Console.WriteLine("Lost connection to server.");
                    Thread.Sleep(500);
                    Console.WriteLine("Attempting to reconnect...");
                    continue;
                }

                Presenter.PresentCube(CubeState);

                Console.WriteLine("Enter a move to be performed...");
                if (Console.ReadLine() is string message)
                {
                    PerformMoveStrategy.PerformMove(message);
                }
            }
        }
    }
}
