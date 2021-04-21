using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Network;
using PythonTypes.Types.Network;
using PythonTypes.Types.Primitives;

namespace Editor
{
    public class LiveClient : Client
    {
        private CustomEVEClientSocket mClientSocket = null;
        private CustomEVEClientSocket mServerSocket = null;
        protected MainWindow mMainWindow = null;

        public LiveClient(int index, CustomEVEClientSocket socket, CustomEVEClientSocket serverSocket, MainWindow mainWindow) : base(index)
        {
            this.mServerSocket = serverSocket;
            this.mClientSocket = socket;
            this.Address = socket.GetRemoteAddress();
            this.mMainWindow = mainWindow;
            
            // setup receive callbacks for the client and server connections
            this.mServerSocket.SetReceiveCallback(ServerReceive);
            this.mClientSocket.SetReceiveCallback(ClientReceive);
            // setup connection close callbacks
            this.mServerSocket.SetOnConnectionLostHandler(Stop);
            this.mClientSocket.SetOnConnectionLostHandler(Stop);
        }

        private void ServerReceive(byte[] byteData, PyDataType data)
        {
            // relay packet to client
            this.mClientSocket.Send(data);

            PacketEntry entry = new PacketEntry
            {
                RawPacket = data,
                Client = this,
                Direction = PacketDirection.ServerToClient,
                Timestamp = DateTime.UtcNow,
                PacketBytes = byteData
            };


            // try to parse a PyPacket, if it fails just store the raw data
            try
            {
                entry.Packet = data;
                this.HandlePacket(entry);
            }
            catch (Exception)
            {
                // ignored
            }
            
            this.mMainWindow.OnPacketReceived(entry);
        }

        private void ClientReceive(byte[] byteData, PyDataType data)
        {
            // relay packet to server
            this.mServerSocket.Send(data);

            PacketEntry entry = new PacketEntry
            {
                RawPacket = data,
                Client = this,
                Direction = PacketDirection.ClientToServer,
                Timestamp = DateTime.UtcNow,
                PacketBytes = byteData
            };

            // try to parse a PyPacket, if it fails just store the raw data
            try
            {
                entry.Packet = data;
                this.HandlePacket(entry);
            }
            catch (Exception)
            {
                // ignored
            }
            
            this.mMainWindow.OnPacketReceived(entry);
        }

        public void Stop()
        {
            // forcefully disconnect sockets
            try
            {
                this.mClientSocket.ForcefullyDisconnect();
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                this.mServerSocket.ForcefullyDisconnect();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}