namespace LionCraft.NetworkCapture.Core.Contracts
{
    public interface IPacket
    {
        int Id { get; }

        IPacketHeader Header { get; }

        byte[] Data { get; }
    }
}