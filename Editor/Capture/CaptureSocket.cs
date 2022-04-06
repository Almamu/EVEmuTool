using EVESharp.Common.Network;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Capture
{
    internal class ReceiveState
    {
        public Socket Socket { get; init; }
        public byte[] Buffer { get; init; }
        public StreamPacketizer Packetizer { get; init; }
        public Socket Destination { get; init; }
    }

    public class CaptureSocket
    {
        public Socket Server { get; init; } = null;
        public Socket Client { get; init; } = null;
        public int ClientID { get; init; }
        private StreamPacketizer mServerPacketizer = null;
        private StreamPacketizer mClientPacketizer = null;
        private CaptureProcessor mProcessor = null;
        private ConcurrentDictionary<string, string> mBoundServices = new ConcurrentDictionary<string, string>();
        private ConcurrentDictionary<long, string> CallIDToService = new ConcurrentDictionary<long, string>();
        private ConcurrentDictionary<long, string> CallIDToMethod = new ConcurrentDictionary<long, string>();

        public CaptureSocket(int clientIndex, CaptureProcessor processor, Socket server, Socket client)
        {
            this.ClientID = clientIndex;
            this.mProcessor = processor;
            this.Server = server;
            this.Client = client;
            this.mServerPacketizer = new StreamPacketizer();
            this.mClientPacketizer = new StreamPacketizer();

            // setup receive callbacks
            ReceiveState serverState = new ReceiveState()
            {
                Socket = server,
                Buffer = new byte[4096],
                Packetizer = this.mServerPacketizer,
                Destination = client
            };
            ReceiveState clientState = new ReceiveState()
            {
                Socket = client,
                Buffer = new byte[4096],
                Packetizer = this.mClientPacketizer,
                Destination = server
            };

            this.Client.BeginReceive(
                clientState.Buffer, 0, clientState.Buffer.Length,
                SocketFlags.None,
                ReceiveData,
                clientState
            );
            this.Server.BeginReceive(
                serverState.Buffer, 0, serverState.Buffer.Length,
                SocketFlags.None,
                ReceiveData,
                serverState
            );
        }

        private void ReceiveData(IAsyncResult ar)
        {
            try
            {
                // receiving data, send it to the other socket
                ReceiveState state = ar.AsyncState as ReceiveState;

                int bytes = state.Socket.EndReceive(ar);

                if (bytes == 0)
                {
                    // TODO: CONNECTION CLOSED!
                    this.Client?.Dispose();
                    this.Server?.Dispose();
                    return;
                }

                // enqueue the packet into the packetizer
                state.Packetizer.QueuePackets(state.Buffer, bytes);

                // enqueue the data to the processor, this will lock the socket until the data is processed
                // this way we prevent packets being read out of order, as it matters
                this.mProcessor.HandleMessage(
                    new CaptureMessage
                    {
                        Capturer = this,
                        Origin = state.Socket,
                        Packetizer = state.Packetizer
                    }
                );

                // send the same data to the destination
                state.Destination.Send(state.Buffer, 0, bytes, SocketFlags.None);

                // start receiving from the same socket again
                state.Socket.BeginReceive(
                    state.Buffer, 0, state.Buffer.Length,
                    SocketFlags.None,
                    ReceiveData,
                    state
                );
            }
            catch (Exception)
            {
                // something happened, go nuclear on the socket
                this.Client?.Dispose();
                this.Server?.Dispose();
            }
        }

        /// <summary>
        /// Registers a new bound service in this socket
        /// </summary>
        /// <param name="boundID">The boundID string that identifies the bound service</param>
        /// <param name="name">The name of the service</param>
        public void RegisterBoundService(string boundID, string name)
        {
            this.mBoundServices.TryAdd(boundID, name);
        }

        /// <summary>
        /// Searches for the given bound service ID in the list and returns the name if found
        /// </summary>
        /// <param name="boundID"></param>
        /// <returns></returns>
        public string ResolveBoundService(string boundID)
        {
            if (this.mBoundServices.TryGetValue(boundID, out string result) == false)
                return "";

            return result;
        }

        /// <summary>
        /// Registers a new service call so the packet response can be resolved to the right service and call
        /// </summary>
        /// <param name="callID">The call identifier</param>
        /// <param name="service">The service it calls</param>
        /// <param name="method">The method it calls</param>
        public void RegisterServiceCall(long callID, string service, string method)
        {
            this.CallIDToService.TryAdd(callID, service);
            this.CallIDToMethod.TryAdd(callID, method);
        }

        /// <summary>
        /// Resolves the given service call to a specific service and method
        /// </summary>
        /// <param name="callID">The call identifier</param>
        /// <param name="service">Where to put the service name</param>
        /// <param name="method">Where to put the service method</param>
        public void FinishServiceCall(long callID, out string service, out string method)
        {
            this.CallIDToService.TryRemove(callID, out service);
            this.CallIDToMethod.TryRemove(callID, out method);
        }
    }
}
