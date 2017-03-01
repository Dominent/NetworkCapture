using LionCraft.NetworkCapture.Core.Contracts;
using LionCraft.NetworkCapture.Core.Infrastructure.Constants;
using LionCraft.NetworkCapture.Core.Models.Factories;
using LionCraft.NetworkCapture.Core.Models.Helpers;
using LionCraft.NetworkCapture.Core.Models.PacketBuilders;
using LionCraft.NetworkCapture.Core.Models.Sockets;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows.Data;

namespace LionCraft.NetworkCapture.Core.Models.Managers
{
    public class CaptureManager
    {
        private INetworkManager networkManager;
        private IPacketFactory packetFactory;
        private object lockObject;

        public CaptureManager()
            : this(new NetworkManager(
                    new RawSocket(
                        new IPEndPoint(IPAddressHelper.GetLocalIpAddress(), GeneralConstants.DefaultSocketPort)),
                    new IPPacketBuilder()), new PacketFactory())
        {
        }

        public ObservableCollection<IPacket> Packets { get; private set; }

        private CaptureManager(INetworkManager networkManager, IPacketFactory packetFactory)
        {
            this.networkManager = networkManager;
            this.packetFactory = packetFactory;

            this.lockObject = new object();

            this.Packets = new ObservableCollection<IPacket>();
            BindingOperations.EnableCollectionSynchronization(this.Packets, this.lockObject);
        }

        public void Capture(Action<object> action)
        {
            foreach (var packet in this.networkManager.Recieve())
            {
                var parsedPacket = packetFactory.Create(packet);

                this.Packets.Add(parsedPacket);

                action?.Invoke(parsedPacket);
            }
        }
    }
}
