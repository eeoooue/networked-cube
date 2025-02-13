using System.Net.Sockets;
using System.Net;
using System.Text;
using LibNetCube;

namespace DummyService
{
    internal class Program
    {
        static private CubePuzzle Puzzle = new CubePuzzle();

        static void Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);
            tcpListener.Start();
            TcpClient tcpClient = tcpListener.AcceptTcpClient();

            while (true)
            {
                NetworkStream nStream = tcpClient.GetStream();

                string move = ReadFromStream(nStream);
                Console.WriteLine("Received bytes: \"" + move + "\"");

                ApplyMove(move);

                // SEND CUBE STATE

                CubeState state = GetCubeState();
                byte[] response = CreateCubeResponse(state);
                nStream.Write(response, 0, response.Length);
                Console.WriteLine($"Sent: cube state {response}");
            }

            Console.ReadKey(); // Wait for keypress before exit
        }

        static CubeState GetCubeState()
        {
            return Puzzle.GetState();
        }


        static void ApplyMove(byte[] bytes)
        {
            string move = Encoding.ASCII.GetString(bytes);

            if (move.Length == 1)
            {
                char x = move[0];
                Puzzle.PerformMove(x);
            }
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


        static byte[] CreateCubeResponse(CubeState state)
        {
            byte[] payload = state.Serialize();

            int messageLength = payload.Length;

            byte[] response = new byte[payload.Length + 1];

            response[0] = (byte)messageLength;
            payload.CopyTo(response, 1);

            return response;
        }



        static string ReadFromStream(NetworkStream stream)
        {
            int messageLength = stream.ReadByte();
            byte[] messageBytes = new byte[messageLength];
            stream.Read(messageBytes, 0, messageLength);

            return Encoding.ASCII.GetString(messageBytes).ToUpper();
        }

    }
}
