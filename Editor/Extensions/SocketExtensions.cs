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
        public static string GetRemoteAddress(this Socket socket)
        {
            IPEndPoint endPoint = socket.RemoteEndPoint as IPEndPoint;

            return endPoint?.Address.ToString();
        }
    }
}
