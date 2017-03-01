using LionCraft.NetworkCapture.Core.Contracts;
using LionCraft.NetworkCapture.Core.Infrastructure.Enumerations;

namespace LionCraft.NetworkCapture.Core.Models.Factories
{
    public class PacketFactory : IPacketFactory
    {
        public IPacket Create(IPacket packet)
        {
            switch (packet.Header.Protocol)
            {
                case Protocol.TCP:
                case Protocol.UDP:
                default:
                    return packet;
            }
        }
    }
}
