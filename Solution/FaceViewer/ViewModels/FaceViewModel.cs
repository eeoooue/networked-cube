using LibNetCube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FaceViewer.ViewModels
{
    internal class FaceViewModel : BaseViewModel
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

        public Brush ColourR0C0 { get { return CalcColour(0, 0); } }
        public Brush ColourR0C1 { get { return CalcColour(0, 1); } }
        public Brush ColourR0C2 { get { return CalcColour(0, 2); } }

        public Brush ColourR1C0 { get { return CalcColour(1, 0); } }
        public Brush ColourR1C1 { get { return CalcColour(1, 1); } }
        public Brush ColourR1C2 { get { return CalcColour(1, 2); } }

        public Brush ColourR2C0 { get { return CalcColour(2, 0); } }
        public Brush ColourR2C1 { get { return CalcColour(2, 1); } }
        public Brush ColourR2C2 { get { return CalcColour(2, 2); } }


        public FaceViewModel(CubeFace face)
        {
            Face = face;
            Values = new int[3, 3];
            Thread thread = new Thread(StateUpdateTicker);
            thread.IsBackground = true;
            thread.Start();
            Update();
        }

        public void StateUpdateTicker()
        {
            while (true)
            {
                Thread.Sleep(500);
                Update();
            }
        }


        public Brush CalcColour(int i, int j)
        {
            int value = Values[i, j];
            switch (value)
            {
                case 1:
                    return Brushes.Gold;
                case 2:
                    return Brushes.Crimson;
                case 3:
                    return Brushes.DodgerBlue;
                case 4:
                    return Brushes.LimeGreen;
                case 5:
                    return Brushes.Coral;
                case 0:
                    return Brushes.White;
                default:
                    return Brushes.White;
            }
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
            CubeState = TryGetCubeState();

            for(int i=0; i<3; i++)
            {
                for(int j=0; j<3; j++)
                {
                    Values[i, j] = GetFacePiece(i, j);

                    string memberVariableNameA = $"Row{i}Col{j}";
                    OnPropertyChanged(memberVariableNameA);

                    string memberVariableNameB = $"ColourR{i}C{j}";
                    OnPropertyChanged(memberVariableNameB);
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
