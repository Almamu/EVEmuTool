using Common.Configuration;
using Common.Logging;
using Common.Network;

namespace Editor
{
    public class EVEBridgeServer : EVEServerSocket
    {
        // logger is not needed, create a dummy one to be passed down
        public EVEBridgeServer() : base(26000, new Logger(new Logging()).CreateLogChannel("", true))
        {
        }
    }
}