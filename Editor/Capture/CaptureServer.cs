using Editor.Configuration;
using Editor.Extensions;
using EVESharp.Common.Network;
using EVESharp.Common.Network.Messages;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor.Capture
{
    internal class CaptureServer : IDisposable
    {
        public int Port { get; }
        private ILogger Log { get; }
        private Socket Socket { get; }
        private CaptureProcessor Processor { get; }
        public EventHandler<string> OnStatusChange;

        public CaptureServer(int port, CaptureProcessor processor)
        {
            this.Log = Serilog.Log.ForContext<CaptureServer>();
            this.Port = port;
            // setup the socket
            this.Socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
            // ensure we support both ipv4 and ipv6
            this.Socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
            this.Processor = processor;
        }

        public new void Listen()
        {
            Log.Information("Starting listener...");
            this.OnStatusChange?.Invoke(this, "Starting listener...");
            // start listening
            this.Socket.Bind(new IPEndPoint(IPAddress.IPv6Any, this.Port));
            this.Socket.Listen(20);
            this.OnStatusChange?.Invoke(this, "Listener started, accepting connections");
            Log.Information("Listener started, accepting connections");
            // setup accept callbacks so the connections can be accepted
            this.Socket.BeginAccept(ServerConnectionAccept, null);
        }

        private void ServerConnectionAccept(IAsyncResult ar)
        {
            try
            {
                Socket client = this.Socket.EndAccept(ar);
                // begin accepting again as quickly as possible
                this.Socket.BeginAccept(ServerConnectionAccept, null);
                // do some logging to get a better overview of what's happening
                Log.Information("Received new connection from " + client.GetRemoteAddress() + ". Connecting to the server");
                // create a socket to communicate with the server
                Socket server = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                // connect to the server
                server.Connect(CaptureSettings.ServerAddress, ushort.Parse(CaptureSettings.ServerPort));
                Log.Information("Connection to EVE Server established on " + server.GetRemoteAddress());

                // do some logging so the user can get a better overview of what's happening

                // connection established, setup reading/receiving and continue the work
                CaptureSocket socket = new CaptureSocket(this.Processor, server, client);

                this.OnStatusChange?.Invoke(this, "Accepted new connection on " + client.GetRemoteAddress());
            }
            catch(ObjectDisposedException ex)
            {
                // ignored, object already disposed usually means the socket got disposed
            }
            catch (Exception ex)
            {
                Log.Error($"Exception detected:{Environment.NewLine}{ex.Message}{Environment.NewLine}Stack trace: {Environment.NewLine}{ex.StackTrace}");
                MessageBox.Show($"{ex.Message}{Environment.NewLine}Stack trace: {Environment.NewLine}{ex.StackTrace}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Dispose()
        {
            this.Socket.Dispose();
            this.OnStatusChange?.Invoke(this, "The server has been stopped");
            Log.Information("The server has been stopped");
        }
    }
}
