using LibNetCube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FaceViewerCLI.PerformMoveStrategies
{
    internal class MoveViaSocketStrategy : IPerformMoveStrategy
    {
        private static readonly string _hostname = "127.0.0.1";
        private static readonly int _port = 5000;
        private static TcpClient Client = new TcpClient();

        public void PerformMove(string move)
        {
            SendMoveRequest(move);
        }

        public CubeState? SendMoveRequest(string message)
        {
            try
            {
                Client.Connect(_hostname, _port);
                NetworkStream nStream = Client.GetStream();

                byte[] request = Serialize(message);
                nStream.Write(request, 0, request.Length);

                byte[] received = ReadFromStream(nStream);
                return new CubeState(received);
            }
            catch
            {
                return null;
            }
        }

        public byte[] ReadFromStream(NetworkStream stream)
        {
            int messageLength = stream.ReadByte();
            byte[] messageBytes = new byte[messageLength];
            stream.Read(messageBytes, 0, messageLength);
            return messageBytes;
        }

        public byte[] Serialize(string request)
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
