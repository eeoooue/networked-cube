using LibNetCube;

namespace FaceViewerCLI
{
    internal class Program
    {
        private static FacePresenter Presenter = new FacePresenter();

        static void Main(string[] args)
        {
            CubePuzzle puzzle = new CubePuzzle();
            puzzle.PerformMove('U');


            CubeState state = puzzle.GetState();
            Presenter.PresentCube(state);
        }
    }
}
