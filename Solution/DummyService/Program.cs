using System.Net.Sockets;
using System.Net;
using System.Text;
using LibNetCube;

namespace DummyService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);
            tcpListener.Start();

            TcpClient tcpClient = tcpListener.AcceptTcpClient();
            NetworkStream nStream = tcpClient.GetStream();

            byte[] message = ReadFromStream(nStream);
            Console.WriteLine("Received bytes: \"" + message + "\"");


            // SEND CUBE STATE

            CubeState state = GetExampleCubeState();
            byte[] response = CreateCubeResponse(state);
            nStream.Write(response, 0, response.Length);

            Console.WriteLine($"Sent: cube state {response}");

            Console.ReadKey(); // Wait for keypress before exit
        }

        static CubeState GetExampleCubeState()
        {
            CubePuzzle puzzle = new CubePuzzle();
            puzzle.PerformMove('U');

            return puzzle.GetState();
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



        static byte[] ReadFromStream(NetworkStream stream)
        {
            int messageLength = stream.ReadByte();
            byte[] messageBytes = new byte[messageLength];

            // stream.Read(messageBytes, 0, messageLength);
            return messageBytes;

            // return Encoding.ASCII.GetString(messageBytes);
        }

    }
}
