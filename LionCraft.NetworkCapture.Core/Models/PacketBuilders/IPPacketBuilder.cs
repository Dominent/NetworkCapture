using LionCraft.NetworkCapture.Core.Contracts;
using LionCraft.NetworkCapture.Core.Infrastructure.Enumerations;
using LionCraft.NetworkCapture.Core.Infrastructure.Constants;
using LionCraft.NetworkCapture.Core.Models.Headers;
using LionCraft.NetworkCapture.Core.Models.Packets;
using System;
using System.IO;
using System.Net;

namespace LionCraft.NetworkCapture.Core.Models.PacketBuilders
{
    public class IPPacketBuilder : IPacketBuilder
    {
        private MemoryStream memoryStream;
        private BinaryReader binaryReader;

        private byte[] buffer;
        private int bytesReceived;

        public IPacket Build(byte[] buffer, int bytesReceived)
        {
            this.buffer = buffer;
            this.bytesReceived = bytesReceived;

            this.memoryStream = new MemoryStream(buffer, 0, bytesReceived);
            this.binaryReader = new BinaryReader(this.memoryStream);

            var header = this.BuildHeader();

            var data = this.BuildData(header.HeaderLength, header.DatagramLength);

            return new IPPacket(header, data);
        }

        private IPPacketHeader BuildHeader()
        {
            /// <summary>
            /// First 8 bits contain version (4 bits) and (4 bits) internet header length.
            /// </summary>
            byte versionAndHeaderLength = binaryReader.ReadByte();

            var version = (versionAndHeaderLength >> 4);

            //Multiply by four to get the exact header length [0x0F = 15]
            var headerLength = (versionAndHeaderLength & 0x0F) * 4;

            /// <summary>
            /// Second 8 bits contain information about the type of service.
            /// </summary>
            var typeOfService = binaryReader.ReadByte();

            /// <summary>
            /// Third 16 bits contain the datagram length in bytes.
            /// Max datagram length is 65535 bytes.
            /// </summary>
            var datagramLength = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            /// <summary>
            /// Fourth 16 bits contain This field is used for uniquely identifying the group of fragments of a single IP datagram.
            /// </summary>
            var identification = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            /// <summary>
            /// Fifth 16 bits contain reserved bit (1 bit), dont fragment bit (1 bit), more fragment bit (1 bit) and 13 bits fragmentation offset.
            /// </summary>
            var flagsAndFragmentationOffset = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            var flags = (flagsAndFragmentationOffset >> 13);

            int fragmentationOffset = flagsAndFragmentationOffset << 3;
            fragmentationOffset >>= 3;

            /// <summary>
            /// Sixt 8 bits for time to live (TTL) specified in seconds.
            /// </summary>
            var timeToLive = binaryReader.ReadByte();

            /// <summary>
            /// Seventh 8 bits for higher level protocol.
            /// </summary>
            var protocol = binaryReader.ReadByte();

            /// <summary>
            /// Ninth 16 bits for checksum (this checksum is for header only).
            /// </summary>
            var checksum = IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

            /// <summary>
            /// 32 bits for source address.
            /// </summary>
            var sourceAddress = new IPAddress((uint)binaryReader.ReadInt32()).ToString();

            /// <summary>
            /// 32 bits for destination address.
            /// </summary>
            var destinationAddress = new IPAddress((uint)binaryReader.ReadInt32()).ToString();

            return new IPPacketHeader()
            {
                Version = version,
                Protocol = (Protocol)protocol,
                Source = sourceAddress.ToString(),
                Destination = destinationAddress.ToString(),
                Length = datagramLength,
                Info = String.Empty,

                HeaderLength = headerLength,
                TypeOfService = typeOfService,
                DatagramLength = datagramLength,
                Identification = identification,
                Flags = flags,
                FragmentationOffset = fragmentationOffset,
                TimeToLive = timeToLive,
                Checksum = checksum,
            };
        }

        private byte[] BuildData(int headerLength, int datagramLength)
        {
            /// <summary>
            /// The data carried by the datagram.
            /// </summary>
            var data = new byte[GeneralConstants.MaxBufferSize];

            Array.Copy(buffer, headerLength, data, 0, datagramLength - headerLength);

            return data;
        }
    }
}