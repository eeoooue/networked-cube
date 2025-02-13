using System.Net.Sockets;
using System.Net;
using System.Text;

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
            Console.WriteLine("Received: \"" + message + "\"");

            //byte[] request = Serialize(translatedMessage);
            //nStream.Write(request, 0, request.Length);
            //Console.WriteLine("Sent: \"" + translatedMessage + "\"");

            Console.ReadKey(); // Wait for keypress before exit
        }


        static byte[] ReadFromStream(NetworkStream stream)
        {
            int messageLength = stream.ReadByte();
            byte[] messageBytes = new byte[messageLength];

            // stream.Read(messageBytes, 0, messageLength);
            return messageBytes;

            // return Encoding.ASCII.GetString(messageBytes);
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
