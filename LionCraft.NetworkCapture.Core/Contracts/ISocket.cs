using System.Net;

namespace LionCraft.NetworkCapture.Core.Contracts
{
    public interface ISocket
    {
        int Receive(byte[] buffer);

        IPEndPoint IpEndpoint { get; }
    }
}