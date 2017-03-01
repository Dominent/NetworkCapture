namespace LionCraft.NetworkCapture.Core.Contracts
{
    public interface IPacketBuilder
    {
        IPacket Build(byte[] buffer, int bytesReceived);
    }
}