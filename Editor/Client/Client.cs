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
    class Client
    {
        private TCPSocket socket = null;
        private StreamPacketizer packetizer = null;
        private Thread thread = null;
        private TCPSocket connection = null;
        private bool versionExchangeSwitch = false; // false -> From server to client, true -> From client to server

        public Client(TCPSocket sock)
        {
            Log.Info("Client", "Creating new connection to EVEmu Server");
            connection = new TCPSocket(25999, false);

            if (connection.Connect("loopback") == false)
            {
                Log.Error("Client", "Cannot connect to EVEmu Server on port 25999");
                sock.Close();
                Program.clientList.Remove(this);
                return;
            }

            packetizer = new StreamPacketizer();
            socket = sock;
            thread = new Thread(Run);
            thread.Start();
        }

        private void Run()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(1);

                    // Get server and client packets and put them into the queue
                    byte[] serverData = new byte[connection.Available];
                    int serverBytes = 0;

                    byte[] clientData = new byte[socket.Available];
                    int clientBytes = 0;

                    try
                    {
                        serverBytes = connection.Recv(serverData);
                    }
                    catch (SocketException ex)
                    {
                        if (ex.ErrorCode != 10035)
                        {
                            Log.Error("Client", "Client disconnected");
                            thread.Abort();
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Client", "Exception: " + ex.ToString());
                        thread.Abort();
                    }

                    try
                    {
                        clientBytes = socket.Recv(clientData);
                    }
                    catch (SocketException ex)
                    {
                        if (ex.ErrorCode != 10035)
                        {
                            Log.Error("Client", "Client disconnected");
                            thread.Abort();
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Client", "Exception: " + ex.ToString());
                        thread.Abort();
                    }

                    int packets = 0;

                    // Put the data in the packetizer
                    packets += packetizer.QueuePackets(serverData);
                    packets += packetizer.QueuePackets(clientData);

                    // Get every packet
                    for (int i = 0; i < packets; i++)
                    {
                        byte[] marshaled = packetizer.PopItem();
                        PyObject packet = Unmarshal.Process<PyObject>(marshaled);
                        Log.Warning("PacketTracer", PrettyPrinter.Print(packet));

                        // Check for login packets and just forward them
                        if (packet is PyTuple)
                        {
                            // We need to know which packet it is, so we need to handle them mostly like a server will do
                            IdentifyAuthPacket(packet as PyTuple);
                        }
                        else if (packet is PyInt)
                        {
                            // This is only sent by the server
                            SendClient(packet);
                        }
                        else if (packet is PyString)
                        {
                            // This is only sent by the server
                            SendClient(packet);
                        }
                        else if (packet is PyDict)
                        {
                            // Packet sent by the client(HandshakeAck)
                            // We need to modify it in order to put our own client address, as it isnt the same as the address that the server sends
                            PyDict handshake = packet as PyDict;

                            PyDict session = handshake.Get("session_init") as PyDict;

                            session.Set("address", new PyString(socket.GetAddress()));

                            handshake.Set("session_init", session);

                            SendClient(handshake);
                        }
                        else if (packet is PyObjectEx) // Exceptions... just check the type and decide what to do
                        {
                            PyException exception = new PyException();

                            if (exception.Decode(packet) == true) // Ignore the error
                            {
                                if (exception.reason == "LoginAuthFailed") // Sent by the server
                                {
                                    // TODO: Handle MakeUserError exceptions
                                    SendClient(packet);
                                }
                                else // We do not know about more exceptions, so send them to the server by now
                                {
                                    SendServer(packet);
                                }
                            }
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
                                HandlePyPacket(p);
                            }
                        }
                    }
                }
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception)
            {

            }

            try
            {
                connection.Close();
                socket.Close();
            }
            catch (Exception)
            {

            }

            Program.clientList.Remove(this);
        }

        private void HandlePyPacket(PyPacket data)
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

                SendClient(data);
            }
            else if (data.dest.type == PyAddress.AddrType.Node)
            {
                SendServer(data);
            }
            else if (data.dest.type == PyAddress.AddrType.Broadcast)
            {
                // What to do now ?
                Log.Error("Client", "Broadcast packets not supported yet");
                throw new NotImplementedException("Broadcast packets are not supported yet");
            }
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
            Send(packet, socket);
        }

        private void Send(byte[] data, TCPSocket to)
        {
            byte[] packet = new byte[data.Length + 4];

            Array.Copy(data, 0, packet, 4, data.Length);
            Array.Copy(BitConverter.GetBytes(data.Length), packet, 4);

            to.Send(packet);
        }

        private void IdentifyAuthPacket(PyTuple packet)
        {
            int items = packet.Count();

            if ((items == 7) || (items == 6)) // LowLevelVersionExchange
            {
                // Care about Apocrypha and Incursion
                if (versionExchangeSwitch == false)
                {
                    SendClient(packet);
                }
                else if (versionExchangeSwitch == true)
                {
                    SendServer(packet);
                }

                // Change the value
                versionExchangeSwitch = (versionExchangeSwitch == true) ? false : true;
            }
            else if (items == 2) // Placebo request, login packet or QueueCheck
            {
                SendServer(packet);
            }
            else if (items == 4) // Authentication Response
            {
                SendClient(packet);
            }
            else if (items == 3) // VipKey
            {
                SendServer(packet);
            }
        }
    }
}
