using Microsoft.Xna.Framework;

namespace CubeVisualizer
{
    public static class Program
    {
        private const string WINDOW_NAME = "Cube Visualizer";
        private const int WINDOW_HEIGHT = 400;
        private const int WINDOW_WIDTH = 400;
        private static Color BG_COLOUR = Color.White;

        static void Main()
        {
            using var game = new CubeGame(WINDOW_NAME, WINDOW_HEIGHT, WINDOW_WIDTH, BG_COLOUR);
            game.Run();
        }
    }
}