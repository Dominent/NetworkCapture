using LionCraft.NetworkCapture.Core.Contracts;
using System.Threading;

namespace LionCraft.NetworkCapture.Core.Models.Packets
{
    public class IPPacket : IPacket
    {
        private static int id;

        public IPPacket(IPacketHeader header, byte[] data)
        {
            this.Header = header;
            this.Data = data;

            Interlocked.Increment(ref id);
        }

        public int Id { get { return id; } }

        public IPacketHeader Header { get; private set; }

        public byte[] Data { get; private set; }

        public override string ToString()
        {
            return $"Id: {Id}, Version: {this.Header.Version}, Protocol: {this.Header.Protocol}, Source: {this.Header.Source}, Destination: {this.Header.Destination}, Length: {this.Header.Length}, Info: {this.Header.Info}";
        }
    }
}
