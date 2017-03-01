namespace LionCraft.NetworkCapture.Core.Contracts
{
    public interface IPacketFactory
    {
        IPacket Create(IPacket packet);
    }
}
