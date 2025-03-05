using System.Net.Sockets;
using System.Net;
using LibNetCube;
using System.Text;

namespace ViewProxy
{
    internal class Program
    {
        private static TcpListener tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5002);

        // Create 10 threads (max number of concurrent requests)
        private static Thread[] serverThreads = new Thread[10];

        private static Thread updateThread;

        public static CubeState State;

        static void Main(string[] args)
        {
            CubePuzzle temp = new CubePuzzle();
            State = temp.GetState();


            //ApplyMove("U");
            //ApplyMove("L");
            //ApplyMove("D");
            //ApplyMove("R");

            updateThread = new Thread(StateUpdateTicker);
            updateThread.Start();

            StartServer();
        }

        static void StateUpdateTicker()
        {
            while (true)
            {
                Thread.Sleep(500);
                if (TryGetCubeState() is CubeState state)
                {
                    State = state;
                    Console.WriteLine("Updated state successfully");
                }
                else
                {
                    Console.WriteLine("Failed to update state");
                }
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


        #region Reading Cube State

        public static CubeState? TryGetCubeState()
        {
            try
            {
                return ReadCubeStateFromProxyServer();
            }
            catch
            {
                return null;
            }
        }

        public static CubeState ReadCubeStateFromProxyServer()
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                tcpClient.Connect("127.0.0.1", 5000);

                using (NetworkStream nStream = tcpClient.GetStream())
                {
                    byte[] request = GetBytesToSend("X");
                    nStream.Write(request, 0, request.Length);
                    nStream.Flush();

                    byte[] received = ReadFromStream(nStream);
                    return new CubeState(received);
                }
            }
        }

        public static byte[] GetBytesToSend(string request)
        {
            byte[] responseBytes = Encoding.ASCII.GetBytes(request);
            byte responseLength = (byte)responseBytes.Length;

            byte[] rawData = new byte[responseLength + 1];
            rawData[0] = responseLength;
            responseBytes.CopyTo(rawData, 1);
            return rawData;
        }

        static byte[] ReadFromStream(NetworkStream stream)
        {
            int messageLength = stream.ReadByte();
            byte[] messageBytes = new byte[messageLength];
            stream.Read(messageBytes, 0, messageLength);
            return messageBytes;
        }

        #endregion

    }
}
