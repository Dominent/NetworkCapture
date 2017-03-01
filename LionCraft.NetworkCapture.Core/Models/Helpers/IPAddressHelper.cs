using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace LionCraft.NetworkCapture.Core.Models.Helpers
{
    public static class IPAddressHelper
    {
        public static IPAddress GetLocalIpAddress()
        {
            var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

            var ipAddress = ipHostInfo.AddressList
                .Where(address => address.AddressFamily == AddressFamily.InterNetwork)
                .Select(x => IPAddress.Parse(x.ToString()))
                .FirstOrDefault();

            return ipAddress;
        }
    }
}
