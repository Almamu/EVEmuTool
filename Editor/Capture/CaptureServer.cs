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
        private int mClientCount = 0;
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

        public void Listen()
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
            Socket client = null;
            Socket server = null;
            try
            {
                client = this.Socket.EndAccept(ar);
                // begin accepting again as quickly as possible
                this.Socket.BeginAccept(ServerConnectionAccept, null);
                // do some logging to get a better overview of what's happening
                Log.Information("Received new connection from " + client.GetRemoteAddress() + ". Connecting to the server");
                // create a socket to communicate with the server
                server = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                // ensure we support both ipv4 and ipv6
                server.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
                // connect to the server
                server.Connect(CaptureSettings.ServerAddress, ushort.Parse(CaptureSettings.ServerPort));
                Log.Information("Connection to EVE Server established on " + server.GetRemoteAddress());

                // do some logging so the user can get a better overview of what's happening

                // connection established, setup reading/receiving and continue the work
                CaptureSocket socket = new CaptureSocket(++this.mClientCount, this.Processor, server, client);

                this.OnStatusChange?.Invoke(this, "Accepted new connection on " + client.GetRemoteAddress());
                return;
            }
            catch(ObjectDisposedException)
            {
                // ignored, object already disposed usually means the socket got disposed
            }
            catch(SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                {
                    Log.Warning("Cannot establish connection to the EVEmu server on {CaptureSettings.ServerAddress} {CaptureSettings.ServerPort}", CaptureSettings.ServerAddress, CaptureSettings.ServerPort);
                    this.OnStatusChange?.Invoke(this, $"Cannot establish connection to the EVEmu server on {CaptureSettings.ServerAddress} {CaptureSettings.ServerPort}");
                }
                else
                {
                    this.ReportError(ex);
                }
            }
            catch (Exception ex)
            {
                this.ReportError(ex);
            }

            // dispose of the sockets so they're closed
            try
            {
                server?.Dispose();
            }
            catch(ObjectDisposedException)
            {

            }

            try
            {
                client?.Dispose();
            }
            catch (ObjectDisposedException)
            {

            }
        }

        private void ReportError(Exception ex)
        {
            Log.Error($"Exception detected:{Environment.NewLine}{ex.Message}{Environment.NewLine}Stack trace: {Environment.NewLine}{ex.StackTrace}");
            MessageBox.Show($"{ex.Message}{Environment.NewLine}Stack trace: {Environment.NewLine}{ex.StackTrace}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void Dispose()
        {
            this.Socket.Dispose();
            this.OnStatusChange?.Invoke(this, "The server has been stopped");
            Log.Information("The server has been stopped");
        }
    }
}
