using LibCubeIntegration.GetCubeStrategies;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace CubeVisualizer
{
    public static class Program
    {
        private const string WINDOW_NAME = "3D CUBE";
        private const int WINDOW_HEIGHT = 400;
        private const int WINDOW_WIDTH = 400;
        private static Color BG_COLOUR = Color.White;

        static readonly IGetCubeStrategy GetCubeStrategy = new GetCubeViaApiStrategy();

        static async Task Main()
        {
            if (await GetCubeStrategy.GetCube() is { } state)
            {
                using var game = new CubeGame(WINDOW_NAME, WINDOW_HEIGHT, WINDOW_WIDTH, BG_COLOUR, state);
                game.Run();
            }
        }
    }
}