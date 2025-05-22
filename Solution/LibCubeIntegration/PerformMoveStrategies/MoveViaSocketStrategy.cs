﻿namespace LibCubeIntegration.PerformMoveStrategies;
using System.Net.Sockets;
using System.Text;
using LibNetCube;

class MoveViaSocketStrategy : IPerformMoveStrategy
{
    private string Hostname;
    private int Port;

    public MoveViaSocketStrategy(string serviceName = "DummyService")
    {
        Hostname = NetworkingConfiguration.BaseAddress;
        Port = NetworkingConfiguration.GetPortForServer(serviceName);
    }

    public async Task<bool> PerformMoveAsync(string move)
    {
        var response = await SendMoveRequest(move);
        return response is not null;
    }

    public async Task<CubeState?> SendMoveRequest(string message)
    {
        try
        {
            using var client = new TcpClient();
            await client.ConnectAsync(Hostname, Port);
            var nStream = client.GetStream();

            var request = Serialize(message);
            nStream.Write(request, 0, request.Length);
            var received = await ReadFromStreamAsync(nStream);

            return new CubeState(received);
        }
        catch
        {
            return null;
        }
    }

    static async Task<byte[]> ReadFromStreamAsync(NetworkStream stream)
    {
        var lengthBuffer = new byte[1];
        var bytesRead = await stream.ReadAsync(lengthBuffer.AsMemory(0, 1));
        if (bytesRead == 0)
        {
            throw new Exception("no data received");
        }

        int messageLength = lengthBuffer[0];
        var messageBytes = new byte[messageLength];
        var offset = 0;
        while (offset < messageLength)
        {
            var read = await stream.ReadAsync(messageBytes.AsMemory(offset, messageLength - offset));
            if (read == 0)
            {
                throw new Exception("stream closed unexpectedly");
            }
            offset += read;
        }
        return messageBytes;
    }

    static byte[] Serialize(string request)
    {
        var responseBytes = Encoding.ASCII.GetBytes(request);
        var responseLength = (byte)responseBytes.Length;

        var rawData = new byte[responseLength + 1];
        rawData[0] = responseLength;
        responseBytes.CopyTo(rawData, 1);
        return rawData;
    }
}
