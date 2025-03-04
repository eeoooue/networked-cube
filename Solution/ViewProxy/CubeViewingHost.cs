using LibNetCube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ViewProxy
{
    internal class CubeViewingHost
    {
        // static private CubePuzzle Puzzle = new CubePuzzle();

        private TcpListener TCPListener;

        public CubeViewingHost(TcpListener listener)
        {
            TCPListener = listener;
        }

        public void Listen()
        {
            while (true)
            {
                try
                {
                    TcpClient tcpClient = TCPListener.AcceptTcpClient();

                    NetworkStream nStream = tcpClient.GetStream();
                    ReadFromStream(nStream);

                    // SEND CUBE STATE

                    CubeState state = GetCubeState();
                    byte[] response = CreateCubeResponse(state);
                    nStream.Write(response, 0, response.Length);
                    Console.WriteLine($"Sent: cube state {response}");
                }
                catch
                {
                    Console.WriteLine($"Error caught within ViewingHostWorker, continuing execution...");
                }
            }
            
        }

        public CubeState GetCubeState()
        {
            return Program.Puzzle.GetState();
        }

        

        public byte[] CreateCubeResponse(CubeState state)
        {
            byte[] payload = state.Serialize();
            int messageLength = payload.Length;
            byte[] response = new byte[payload.Length + 1];
            response[0] = (byte)messageLength;
            payload.CopyTo(response, 1);

            return response;
        }

        public void ReadFromStream(NetworkStream stream)
        {
            try
            {
                int messageLength = stream.ReadByte();
                byte[] messageBytes = new byte[messageLength];
                stream.Read(messageBytes, 0, messageLength);
            }
            catch
            {

            }
        }
    }
}
