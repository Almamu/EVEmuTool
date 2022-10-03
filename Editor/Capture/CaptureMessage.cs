using EVESharp.Common.Network;
using EVESharp.Common.Network.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EVEmuTool.Capture
{
    public class CaptureMessage : IMessage
    {
        public CaptureSocket Capturer { get; init; }
        public StreamPacketizer Packetizer { get; init; }
        public Socket Origin { get; init; }
    }
}
