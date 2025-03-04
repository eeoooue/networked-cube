using System.Net.Sockets;
using System.Net;
using LibNetCube;

namespace ViewProxy
{
    internal class Program
    {
        private static TcpListener tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5002);

        // Create 10 threads (max number of concurrent requests)
        private static Thread[] serverThreads = new Thread[10];

        public static CubePuzzle Puzzle = new CubePuzzle();

        static void Main(string[] args)
        {
            //ApplyMove("U");
            //ApplyMove("L");
            //ApplyMove("D");
            //ApplyMove("R");
            StartServer();
        }

        static void ApplyMove(string move)
        {
            if (move.Length == 1)
            {
                char x = move[0];
                Puzzle.PerformMove(x);
            }

            if (move.Length == 2 && move[1] == '\'')
            {
                string c = move[0].ToString();
                ApplyMove(c);
                ApplyMove(c);
                ApplyMove(c);
            }
        }

        public static void StartServer()
        {
            tcpListener.Start();
            Console.WriteLine($"[!] Server Active: ViewProxy @ port = 5002");

            // Start all of the threads
            for (int i = 0; i < 10; i++)
            {
                serverThreads[i] = new Thread(ServerThread);
                serverThreads[i].Start();
            }
        }

        private static void ServerThread()
        {
            CubeViewingHost worker = new CubeViewingHost(tcpListener);
            worker.Listen();
        }
    }
}
