using LionCraft.NetworkCapture.Core.Contracts;
using LionCraft.NetworkCapture.Core.Infrastructure.Constants;
using System;
using System.Collections.Generic;

namespace LionCraft.NetworkCapture.Core.Models.Managers
{
    public class NetworkManager : INetworkManager
    {
        private ISocket socket;
        private IPacketBuilder packetBuilder;

        private byte[] buffer;

        public NetworkManager(ISocket socket, IPacketBuilder packetBuilder)
        {
            this.socket = socket;
            this.packetBuilder = packetBuilder;

            this.buffer = new byte[GeneralConstants.MaxBufferSize];
        }

        public IEnumerable<IPacket> Recieve()
        {
            while (true)
            {
                var bytesReceived = this.socket.Receive(this.buffer);

                var packet = this.packetBuilder.Build(buffer, bytesReceived);

                Array.Clear(this.buffer, 0, this.buffer.Length);

                yield return packet;
            }
        }
    }
}