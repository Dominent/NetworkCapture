using LionCraft.NetworkCapture.Core.Infrastructure.Enumerations;

namespace LionCraft.NetworkCapture.Core.Contracts
{
    public interface IPacketHeader
    {
        int Version { get; }

        Protocol Protocol { get; }

        string Source { get; }

        string Destination { get; }

        int Length { get; }

        string Info { get; }
    }
}
