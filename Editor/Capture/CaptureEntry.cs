using EVESharp.PythonTypes.Types.Network;
using EVESharp.PythonTypes.Types.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Capture
{
    public class CaptureEntry
    {
        public DateTime Timestamp { get; init; }
        public byte[] RawData { get; init; }
        public PacketDirection Direction { get; init; }
        public string PacketType { get; init; }
        public string Service { get; init; }
        public string Call { get; init; }
        public long CallID { get; init; }
        public int ClientID { get; init; }
        public string Source { get; init; }
        public string Destination { get; init; }
        public PyPacket.PacketType Type { get; init; }
    }
}
