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
        private StreamPacketizer mServerPacketizer = null;
        private StreamPacketizer mClientPacketizer = null;
        private CaptureProcessor mProcessor = null;
        private ConcurrentDictionary<string, string> mBoundServices = new ConcurrentDictionary<string, string>();
        private ConcurrentDictionary<long, string> CallIDToService = new ConcurrentDictionary<long, string>();
        private ConcurrentDictionary<long, string> CallIDToMethod = new ConcurrentDictionary<long, string>();

        public CaptureSocket(CaptureProcessor processor, Socket server, Socket client)
        {
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

        public void RegisterBoundService(string boundID, string name)
        {
            this.mBoundServices.TryAdd(boundID, name);
        }

        public string ResolveBoundService(string boundID)
        {
            if (this.mBoundServices.TryGetValue(boundID, out string result) == false)
                return "";

            return result;
        }

        public void RegisterServiceCall(long callID, string service, string method)
        {
            this.CallIDToService.TryAdd(callID, service);
            this.CallIDToMethod.TryAdd(callID, method);
        }

        public void FinishServiceCall(long callID, out string service, out string method)
        {
            this.CallIDToService.TryRemove(callID, out service);
            this.CallIDToMethod.TryRemove(callID, out method);
        }
    }
}
