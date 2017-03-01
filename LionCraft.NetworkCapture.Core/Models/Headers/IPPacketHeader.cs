using LionCraft.NetworkCapture.Core.Contracts;
using LionCraft.NetworkCapture.Core.Infrastructure.Enumerations;

namespace LionCraft.NetworkCapture.Core.Models.Headers
{
    public class IPPacketHeader : IPacketHeader
    {
        public int Version { get; set; }
        public Protocol Protocol { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public int Length { get; set; }
        public string Info { get; set; }

        public int HeaderLength { get; set; }

        public byte TypeOfService { get; set; }

        public ushort DatagramLength { get; set; }

        public int Identification { get; set; }

        public int Flags { get; set; }

        public int FragmentationOffset { get; set; }

        public byte TimeToLive { get; set; }

        public short Checksum { get; set; }
    }
}