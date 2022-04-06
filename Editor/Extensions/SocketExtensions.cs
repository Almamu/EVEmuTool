using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Extensions
{
    public static class SocketExtensions
    {
        public static void Connect(this Socket socket, string address, ushort port)
        {
            IPAddress ip;

            // check if the address is an IP, if not, query the DNS servers to resolve it
            if (IPAddress.TryParse(address, out ip) == false)
            {
                IPHostEntry entry = Dns.GetHostEntry(address);
                int i = 0;

                do
                {
                    ip = entry.AddressList[i++];
                } while (ip.AddressFamily != AddressFamily.InterNetwork && i < entry.AddressList.Length);
            }

            socket.Connect(new IPEndPoint(ip, port));
        }

        public static string GetRemoteAddress(this Socket socket)
        {
            IPEndPoint endPoint = socket.RemoteEndPoint as IPEndPoint;

            return endPoint?.Address.ToString();
        }
    }
}
