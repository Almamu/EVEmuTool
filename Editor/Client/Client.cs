using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

using EVEmuLivePacketEditor.Network;

using Marshal;

namespace EVEmuLivePacketEditor.Client
{
    class AsyncState
    {
        public byte[] buffer = new byte[8192];
    }

    class Client
    {
        private TCPSocket socket = null;
        private StreamPacketizer clientPacketizer = null;
        private StreamPacketizer serverPacketizer = null;
        private TCPSocket connection = null;
        private bool versionExchangeSwitch = false; // false -> From server to client, true -> From client to server
        private AsyncCallback serverReceive = null;
        private AsyncCallback clientReceive = null;

        public Client(TCPSocket sock)
        {
            Log.Info("Client", "Creating new connection to EVEmu Server");
            connection = new TCPSocket(26000, false);

            if (connection.Connect("mmoemulators.com") == false)
            {
                Log.Error("Client", "Cannot connect to EVEmu Server on port 25999");
                CloseConnection();
                return;
            }

            clientPacketizer = new StreamPacketizer();
            serverPacketizer = new StreamPacketizer();
            socket = sock;

            serverReceive = new AsyncCallback(ServerAsyncReceive);
            clientReceive = new AsyncCallback(ClientAsyncReceive);

            // Give time to the socket
            Thread.Sleep(2000);

            AsyncState serverState = new AsyncState();
            connection.Socket.BeginReceive(serverState.buffer, 0, 8192, SocketFlags.None, serverReceive, serverState);

            AsyncState clientState = new AsyncState();
            socket.Socket.BeginReceive(clientState.buffer, 0, 8192, SocketFlags.None, clientReceive, clientState);
        }

        public void ServerAsyncReceive(IAsyncResult ar)
        {
            try
            {
                AsyncState state = (AsyncState)(ar.AsyncState);

                byte[] serverData = state.buffer;
                int serverBytes = 0;

                serverBytes = connection.Socket.EndReceive(ar);

                if (serverBytes > 0)
                {
                    // Add the packets
                    serverPacketizer.QueuePackets(serverData, serverBytes);

                    int packets = serverPacketizer.ProcessPackets();

                    for (int i = 0; i < packets; i++)
                    {
                        byte[] packet = serverPacketizer.PopItem();

                        PyObject data = Unmarshal.Process<PyObject>(packet);

                        Log.Warning("PacketTracer::Server", PrettyPrinter.Print(data));

                        data = OldHandle(data);

                        SendClient(data);
                    }
                }

                connection.Socket.BeginReceive(state.buffer, 0, 8192, SocketFlags.None, serverReceive, state);
            }
            catch (Exception ex)
            {
                Log.Error("ExceptionHandler", ex.ToString());
                CloseConnection();
            }
        }

        private void ClientAsyncReceive(IAsyncResult ar)
        {
            try
            {
                AsyncState state = (AsyncState)(ar.AsyncState);

                byte[] clientData = state.buffer;
                int clientBytes = 0;

                clientBytes = socket.Socket.EndReceive(ar);

                if (clientBytes > 0)
                {
                    clientPacketizer.QueuePackets(clientData, clientBytes);

                    int packets = clientPacketizer.ProcessPackets();

                    for (int i = 0; i < packets; i++)
                    {
                        byte[] packet = clientPacketizer.PopItem();

                        PyObject data = Unmarshal.Process<PyObject>(packet);

                        Log.Warning("PacketTracer::Client", PrettyPrinter.Print(data));

                        data = OldHandle(data);

                        SendServer(data);
                    }
                }

                socket.Socket.BeginReceive(state.buffer, 0, 8192, SocketFlags.None, clientReceive, state);
            }
            catch(Exception ex)
            {
                Log.Error("ExceptionHandler", ex.ToString());
                CloseConnection();
            }
        }

        private PyObject OldHandle(PyObject packet)
        {
            // Check for login packets and just forward them
            if (packet is PyTuple)
            {
                // Those are the first packets, sent by both client and server
                return packet;
            }
            else if (packet is PyInt)
            {
                // This is only sent by the server
                return packet;
            }
            else if (packet is PyString)
            {
                // This is only sent by the server
                return packet;
            }
            else if (packet is PyDict)
            {
                // Packet sent by the client(HandshakeAck)
                // We need to modify it in order to put our own client address, as it isnt the same as the address that the server sends
                PyDict handshake = packet as PyDict;

                PyDict session = handshake.Get("session_init") as PyDict;

                session.Set("address", new PyString(socket.GetAddress()));

                handshake.Set("session_init", session);

                return handshake;
            }
            else if (packet is PyObjectEx) // Exceptions... just check the type and decide what to do
            {
                PyException exception = new PyException();

                if (exception.Decode(packet) == true) // Ignore the error
                {
                    Log.Debug("Exceptions", "Got exception of type " + exception.exception_type);
                }

                return packet;
            }
            else // Normal packets
            {
                PyPacket p = new PyPacket();
                if (p.Decode(packet) == false)
                {
                    // Big problem here, we dont know who to send this
                    Log.Error("Client", "Cannot decode PyPacket");
                }
                else
                {
                    return HandlePyPacket(p);
                }

                return packet;
            }
        }

        private PyObject HandlePyPacket(PyPacket data)
        {
            // Just forward it if we dont want to look for a specific one
            if (data.dest.type == PyAddress.AddrType.Client)
            {
                // Look for SessionChangeNotifications
                if (data.type == Macho.MachoNetMsg_Type.SESSIONCHANGENOTIFICATION)
                {
                    // Search for address change in session
                    PyTuple payload = data.payload;
                    PyDict session = payload.Items[0].As<PyTuple>().Items[1].As<PyDict>();

                    if (session.Contains("address") == true)
                    {
                        PyTuple address = session.Get("address").As<PyTuple>();

                        address[0] = new PyString(socket.GetAddress());
                        address[1] = new PyString(socket.GetAddress());
                        session.Set("address", address);
                    }

                    payload.Items[0].As<PyTuple>().Items[1] = session;

                    data.payload = payload;
                }

                // SendClient(data);
            }
            else if (data.dest.type == PyAddress.AddrType.Node)
            {
                // SendServer(data);
            }
            else if (data.dest.type == PyAddress.AddrType.Broadcast)
            {
                // What to do now ?
                Log.Error("Client", "Broadcast packets not supported yet");
                // throw new NotImplementedException("Broadcast packets are not supported yet");
            }

            return data.Encode();
        }

        private void SendClient(PyPacket data)
        {
            SendClient(data.Encode());
        }

        private void SendServer(PyPacket data)
        {
            SendServer(data.Encode());
        }

        private void SendClient(PyObject data)
        {
            byte[] packet = Marshal.Marshal.Process(data);
            Send(packet, socket);
        }

        private void SendServer(PyObject data)
        {
            byte[] packet = Marshal.Marshal.Process(data);
            Send(packet, connection);
        }

        private void Send(byte[] data, TCPSocket to)
        {
            byte[] packet = new byte[data.Length + 4];

            Array.Copy(data, 0, packet, 4, data.Length);
            Array.Copy(BitConverter.GetBytes(data.Length), packet, 4);

            to.Send(packet);
        }

        private void CloseConnection()
        {
            try
            {
                socket.Socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch
            {

            }

            try
            {
                connection.Socket.Shutdown(SocketShutdown.Both);
                connection.Close();
            }
            catch
            {

            }

            Program.clientList.Remove(this);
        }
    }
}
