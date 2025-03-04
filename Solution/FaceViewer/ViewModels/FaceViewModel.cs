using LibNetCube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FaceViewer.ViewModels
{
    internal class FaceViewModel
    {
        public CubeState? CubeState;

        public CubeFace Face;

        public int[,] Values;

        public int Row0Col0 { get { return Values[0, 0]; } }
        public int Row0Col1 { get { return Values[0, 1]; } }
        public int Row0Col2 { get { return Values[0, 2]; } }

        public int Row1Col0 { get { return Values[1, 0]; } }
        public int Row1Col1 { get { return Values[1, 1]; } }
        public int Row1Col2 { get { return Values[1, 2]; } }

        public int Row2Col0 { get { return Values[2, 0]; } }
        public int Row2Col1 { get { return Values[2, 1]; } }
        public int Row2Col2 { get { return Values[2, 2]; } }


        public FaceViewModel()
        {
            CubeState = TryGetCubeState();
            Values = new int[3, 3];
            Update();
        }

        public CubeState? TryGetCubeState()
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

        public CubeState ReadCubeStateFromProxyServer()
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                tcpClient.Connect("127.0.0.1", 5002);

                using (NetworkStream nStream = tcpClient.GetStream())
                {
                    byte[] request = GetBytesToSend("U");
                    nStream.Write(request, 0, request.Length);
                    byte[] received = ReadFromStream(nStream);
                    return new CubeState(received);
                }
            }
        }

        public void Update()
        {
            TryGetCubeState();

            for(int i=0; i<3; i++)
            {
                for(int j=0; j<3; j++)
                {
                    Values[i, j] = GetFacePiece(i, j);
                }
            }
        }

        public int GetFacePiece(int i, int j)
        {
            if (CubeState is CubeState state)
            {
                int[,] values = state.GetFace(Face);
                return values[i, j];
            }

            return 0;
        }

        public byte[] GetBytesToSend(string request)
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
    }
}
