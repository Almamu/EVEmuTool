using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EVEmuLivePacketEditor.Network;
using EVEmuLivePacketEditor.Client;

namespace EVEmuLivePacketEditor
{
    class Program
    {
        static private TCPSocket socket = null;
        static public List<Client.Client> clientList = new List<Client.Client>();

        static void Main(string[] args)
        {
            Log.Init("packet-editor");

            Log.Info("Main", "Starting listening socket");
            socket = new TCPSocket(26000, false);

            if (socket.Listen(5) == false)
            {
                Log.Error("Main", "Cannot start listening socket on port 26000.");
                Log.Info("Main", "You should have your EVEmu server working on port 25999");
                Log.Info("Main", "and port 26000 free of any server.");

                while (true) Thread.Sleep(1);
            }

            Log.Info("Main", "Listening socket started succesful");

            while (true)
            {
                TCPSocket client = socket.Accept();

                if (client != null)
                {
                    client.Blocking = false;
                    clientList.Add(new Client.Client(client));
                }
            }
        }
    }
}
