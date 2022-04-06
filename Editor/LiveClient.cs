using EVESharp.PythonTypes.Types.Primitives;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Editor.CustomMarshal;

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

        private void ServerReceive(byte[] byteData, InsightUnmarshal unmarshal)
        {
            PyDataType data = unmarshal.Output;

            // generate the final buffer
            byte[] packetBuffer = new byte[byteData.Length + sizeof(int)];

            // write the packet size and the buffer to the new buffer
            Buffer.BlockCopy(BitConverter.GetBytes(byteData.Length), 0, packetBuffer, 0, sizeof(int));
            Buffer.BlockCopy(byteData, 0, packetBuffer, sizeof(int), byteData.Length);
            // relay packet to client
            this.mClientSocket.Send(packetBuffer);

            PacketEntry entry = new PacketEntry
            {
                RawPacket = data,
                Client = this,
                Direction = PacketDirection.ServerToClient,
                Timestamp = DateTime.UtcNow,
                PacketBytes = byteData,
                Unmarshal = unmarshal
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

        private void ClientReceive(byte[] byteData, InsightUnmarshal unmarshal)
        {
            PyDataType data = unmarshal.Output;

            // generate the final buffer
            byte[] packetBuffer = new byte[byteData.Length + sizeof(int)];

            // write the packet size and the buffer to the new buffer
            Buffer.BlockCopy(BitConverter.GetBytes(byteData.Length), 0, packetBuffer, 0, sizeof(int));
            Buffer.BlockCopy(byteData, 0, packetBuffer, sizeof(int), byteData.Length);
            // relay packet to server
            this.mServerSocket.Send(packetBuffer);

            PacketEntry entry = new PacketEntry
            {
                RawPacket = data,
                Client = this,
                Direction = PacketDirection.ClientToServer,
                Timestamp = DateTime.UtcNow,
                PacketBytes = byteData,
                Unmarshal = unmarshal
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