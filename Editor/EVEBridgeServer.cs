using EVESharp.Common.Configuration;
using EVESharp.Common.Logging;
using EVESharp.Common.Network;
using System;

namespace Editor
{
    public class EVEBridgeServer : EVEServerSocket
    {
        // logger is not needed, create a dummy one to be passed down
        public EVEBridgeServer() : base(26000, new Logger(new Logging()).CreateLogChannel("", true))
        {
        }

        public new CustomEVEClientSocket EndAccept(IAsyncResult asyncResult)
        {
            return new CustomEVEClientSocket(this.Socket.EndAccept(asyncResult), Log);
        }
    }
}