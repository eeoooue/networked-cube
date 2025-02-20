namespace CubeProxy
{
    internal class Program
    {
        /// <summary>
        /// Will be multithreaded in using Threadpool threads
        /// </summary>
        static void Main(string[] args)
        {
            PuzzleCubeServer.StartServer();
        }
    }
}
