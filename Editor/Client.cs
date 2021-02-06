using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Network;
using PythonTypes.Types.Network;
using PythonTypes.Types.Primitives;

namespace Editor
{
    public class Client
    {
        public bool IncludePackets { get; set; } = true;
        public int ClientIndex { get; set; } = 0;
        public string Username { get; set; } = "";
        public string Address { get; set; } = "";
        public Dictionary<long, string> CallIDList = new Dictionary<long, string>();
        public Dictionary<long, string> ServiceList = new Dictionary<long, string>();
        public Dictionary<string, string> BoundServices = new Dictionary<string, string>();

        private EVEClientSocket mClientSocket = null;
        private EVEClientSocket mServerSocket = null;
        private MainWindow mMainWindow = null;

        public Client(int index, EVEClientSocket socket, EVEClientSocket serverSocket, MainWindow mainWindow)
        {
            this.mServerSocket = serverSocket;
            this.mClientSocket = socket;
            this.Address = socket.GetRemoteAddress();
            this.ClientIndex = index;
            
            // setup receive callbacks for the client and server connections
            this.mServerSocket.SetReceiveCallback(ServerReceive);
            this.mClientSocket.SetReceiveCallback(ClientReceive);
            // setup connection close callbacks
            this.mServerSocket.SetOnConnectionLostHandler(OnConnectionLost);
            this.mClientSocket.SetOnConnectionLostHandler(OnConnectionLost);
            // store the window reference
            this.mMainWindow = mainWindow;
        }

        private void HandlePacket(PacketEntry entry)
        {
            int callID = 0;
            string call = "";
            
            if (entry.Packet.Type == PyPacket.PacketType.CALL_REQ)
            {
                // get callID
                if (entry.Packet.Source is PyAddressNode node)
                    callID = node.CallID;
                else if (entry.Packet.Source is PyAddressClient client)
                    callID = client.CallID;

                this.CallIDList[callID] = entry.Call;
                call = entry.Call;
                this.ServiceList[callID] = entry.Service;
            }

            if (entry.Packet.Type == PyPacket.PacketType.CALL_RSP)
            {
                if (entry.Packet.Destination is PyAddressNode node)
                    callID = node.CallID;
                else if (entry.Packet.Destination is PyAddressClient client)
                    callID = client.CallID;

                call = this.CallIDList[callID];

                if (call.EndsWith(" (MachoBindObject)") == true || call == "MachoBindObject")
                {
                    // store the information for resolving this bound
                    PyString id =
                        ((((((entry.Packet.Payload[0] as PySubStream)).Stream as PyTuple)[0] as PySubStruct)
                            .Definition as PySubStream).Stream as PyTuple)[0] as PyString;

                    this.BoundServices.Add(id, entry.Service + " (bound)");
                }
                
                // special case, GetInventory and GetInventoryFromId
                if (call == "GetInventory" || call == "GetInventoryFromId")
                {
                    // store the information for resolving this bound
                    PyString id =
                        (((((entry.Packet.Payload[0] as PySubStream)).Stream as PySubStruct).Definition as PySubStream)
                            .Stream as PyTuple)[0] as PyString;

                    this.BoundServices.Add(id, "BoundInventory (bound)");
                }
            }
        }

        private void ServerReceive(PyDataType data)
        {
            // relay packet to client
            this.mClientSocket.Send(data);

            PacketEntry entry = new PacketEntry
            {
                RawPacket = data,
                Client = this,
                Direction = PacketDirection.ServerToClient,
                Timestamp = DateTime.UtcNow
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

        private void ClientReceive(PyDataType data)
        {
            // relay packet to server
            this.mServerSocket.Send(data);

            PacketEntry entry = new PacketEntry
            {
                RawPacket = data,
                Client = this,
                Direction = PacketDirection.ClientToServer,
                Timestamp = DateTime.UtcNow
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

        private void OnConnectionLost()
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
            
            // let the form know we're out
            this.mMainWindow.OnClientDisconnected(this);
        }
    }
}