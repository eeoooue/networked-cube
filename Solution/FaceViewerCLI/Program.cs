using System.Net.Sockets;
using System.Text;
using LibNetCube;

namespace FaceViewerCLI
{
    internal class Program
    {
        private static FacePresenter Presenter = new FacePresenter();

        static void Main(string[] args)
        {
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 5000);
            NetworkStream nStream = tcpClient.GetStream();
            Console.WriteLine("Enter a message to be translated...");
            string? message = Console.ReadLine();
            if (message != null)
            {
                byte[] request = Serialize(message);
                nStream.Write(request, 0, request.Length);
                // TODO: Read response from stream and display to user
                byte[] received = ReadFromStream(nStream);


                CubeState state = new CubeState(received);
                Presenter.PresentCube(state);

                Console.WriteLine($"received msg: {received}");

            }
            Console.ReadKey(); // Wait for keypress before exit
        }

        static byte[] ReadFromStream(NetworkStream stream)
        {
            int messageLength = stream.ReadByte();
            byte[] messageBytes = new byte[messageLength];
            stream.Read(messageBytes, 0, messageLength);
            return messageBytes;
        }

        static byte[] Serialize(string request)
        {
            byte[] responseBytes = Encoding.ASCII.GetBytes(request);
            byte responseLength = (byte)responseBytes.Length;

            byte[] rawData = new byte[responseLength + 1];
            rawData[0] = responseLength;
            responseBytes.CopyTo(rawData, 1);
            return rawData;
        }


    }
}
