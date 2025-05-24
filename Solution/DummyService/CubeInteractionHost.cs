﻿using LibNetCube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DummyService
{
    internal class CubeInteractionHost
    {
        // static private CubePuzzle Puzzle = new CubePuzzle();

        private static CubePuzzle Puzzle = new CubePuzzle();

        private TcpListener TCPListener;

        public CubeInteractionHost(TcpListener listener)
        {
            TCPListener = listener;
        }

        public void Listen()
        {
            TCPListener.Start();

            while (true)
            {
                try
                {
                    TcpClient tcpClient = TCPListener.AcceptTcpClient();

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
                catch
                {
                    Console.WriteLine($"Error caught within ViewingHostWorker, continuing execution...");
                }
            }

        }

        public CubeState GetCubeState()
        {
            return Puzzle.GetState();
        }

        public void ApplyMove(string move)
        {
            if (move == "RESET")
            {
                ResetCube();
                return;
            }

            try
            {
                Puzzle.PerformMove(move);
            }
            catch
            {
                return;
            }
        }

        private void ResetCube()
        {
            Puzzle.Reset();
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

        public string ReadFromStream(NetworkStream stream)
        {
            int messageLength = stream.ReadByte();
            byte[] messageBytes = new byte[messageLength];
            stream.Read(messageBytes, 0, messageLength);

            return Encoding.ASCII.GetString(messageBytes).ToUpper();
        }
    }
}
