using LionCraft.NetworkCapture.Core.Contracts;
using LionCraft.NetworkCapture.Core.Infrastructure.Constants;
using System.Net;
using System.Net.Sockets;

namespace LionCraft.NetworkCapture.Core.Models.Sockets
{
    public class RawSocket : ISocket
    {
        private Socket socket;

        public RawSocket(IPEndPoint endpoint)
        {
            this.IpEndpoint = endpoint;

            Initialize(GeneralConstants.InBufferDefaultValue, GeneralConstants.OutBufferDefaultValue);
        }

        public IPEndPoint IpEndpoint { get; private set; }

        public int Receive(byte[] buffer)
        {
            return socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);
        }

        /// <summary>
        /// Initializes the <see cref="socket"/> and binds it to the Ip endpoint.
        /// </summary>
        /// <param name="inValue">Byte array used for capturing ingoing packets.</param>
        /// <param name="outValue">Byte array used for capturing outgoing packets.</param>
        private void Initialize(byte[] inValue, byte[] outValue)
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);

            this.socket.Bind(IpEndpoint);
            this.socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);

            this.socket.IOControl(IOControlCode.ReceiveAll, inValue, outValue);
        }
    }
}