using LionCraft.NetworkCapture.Core.Contracts;
using System.Collections.Generic;

namespace LionCraft.NetworkCapture.Core
{
    public interface INetworkManager
    {
        IEnumerable<IPacket> Recieve();
    }
}