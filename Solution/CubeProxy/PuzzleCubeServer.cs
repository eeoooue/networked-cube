using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CubeProxy
{
    internal class PuzzleCubeServer
    {
        private static TcpListener tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);
        // Create 10 threads (max number of concurrent requests)
        private static Thread[] serverThreads = new Thread[10];

        public static void StartServer()
        {
            tcpListener.Start();
            Console.WriteLine("[!] Server Active");

            // Start all of the threads
            for (int i = 0; i < 10; i++)
            {
                serverThreads[i] = new Thread(ServerThread);
                serverThreads[i].Start();
            }
        }

        private static void ServerThread()
        {
            PuzzleCubeWorker worker = new PuzzleCubeWorker(tcpListener);
            worker.Listen();
        }
    }
}
