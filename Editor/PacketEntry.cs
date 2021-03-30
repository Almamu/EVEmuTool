using System;
using System.Runtime.CompilerServices;
using Google.Protobuf.WellKnownTypes;
using PythonTypes;
using PythonTypes.Types.Collections;
using PythonTypes.Types.Network;
using PythonTypes.Types.Primitives;

namespace Editor
{
    public enum PacketDirection
    {
        ClientToServer,
        ServerToClient
    }
    
    public class PacketEntry
    {
        private PyDataType mPacket;
        
        public DateTime Timestamp { get; set; }
        public Client Client { get; set; }

        public PyDataType RawPacket
        {
            get => this.mPacket;
            set
            {
                this.PacketString = PrettyPrinter.FromDataType(value);
                this.mPacket = value;
            }
        }

        public PyPacket Packet;
        public int ClientIndex => Client.ClientIndex;
        public string PacketString { get; private set; }
        public PacketDirection Direction { get; set; }
        public string PacketType => this.Packet == null ? "Undetermined" : this.Packet.TypeString;
        public string Origin => this.Packet == null ? Direction.ToString() : this.GetAddressFormat(this.Packet.Source);
        public string Destination => this.Packet == null ? "" : this.GetAddressFormat(this.Packet.Destination);

        public string Service => this.Packet == null ? "" : this.GetService();
        public string Call => this.Packet == null ? "" : this.GetCall();
        public long CallID => this.Packet == null ? 0 : this.GetCallID();

        private string GetAddressFormat(PyAddress address)
        {
            switch (address)
            {
                case PyAddressAny any:
                    return $"Any, id={any.ID}";
                case PyAddressNode node:
                    return $"Node, id={node.NodeID}";
                case PyAddressClient client:
                    return $"Client, id={client.ClientID}";
                case PyAddressBroadcast broadcast:
                    return $"Broadcast {broadcast.IDType}";
            }

            return "Unknown";
        }

        private string GetServiceFromAddress(PyAddress address)
        {
            switch (address)
            {
                case PyAddressAny any:
                    return any.Service;
                case PyAddressNode node:
                    return node.Service;
                case PyAddressClient client:
                    return client.Service;
                case PyAddressBroadcast broadcast:
                    return broadcast.Service;
            }

            return "";
        }

        private string GetService()
        {
            string service = this.GetServiceFromAddress(this.Packet.Source) + this.GetServiceFromAddress(this.Packet.Destination);

            if (service.Length == 0 && this.Packet.Type == PyPacket.PacketType.CALL_REQ)
            {
                // most likely bound service, extract from the call info
                PyTuple callInfo = ((this.Packet.Payload[0] as PyTuple)[1] as PySubStream).Stream as PyTuple;
                PyString id = callInfo[0] as PyString;

                try
                {
                    return this.Client.BoundServices[id];
                }
                catch (Exception e)
                {
                    throw new Exception(
                        $"Cannot find bound service {id.Value}, maybe you need to add extra handlers for new bounds?");
                }
            }

            if (service.Length == 0 && this.Packet.Type == PyPacket.PacketType.CALL_RSP)
                return this.Client.ServiceList[this.CallID];

            return service;
        }

        private string GetCall()
        {
            if ((this.Packet.Type == PyPacket.PacketType.CALL_RSP || this.Packet.Type == PyPacket.PacketType.ERRORRESPONSE) && this.Client.CallIDList.ContainsKey(this.CallID) == true)
                return this.Client.CallIDList[this.CallID];

            if (this.Packet.Type != PyPacket.PacketType.CALL_REQ)
                return "";
            
            PyTuple callInfo = ((this.Packet.Payload[0] as PyTuple)[1] as PySubStream).Stream as PyTuple;
            PyString call = callInfo[1] as PyString;

            // special case, bound objects are different
            if (call == "MachoBindObject")
            {
                // get the resulting bound ID
                if (callInfo[2] is PyTuple data)
                {
                    if (data[1] is PyTuple boundCallInfo)
                    {
                        if (boundCallInfo[0] is PyString boundCall)
                            return $"{boundCall.Value} ({call.Value})";
                    }
                }
            }

            return call;
        }

        private long GetCallID()
        {
            if (this.Packet.Source is PyAddressClient srcClient && srcClient.CallID != null)
                return srcClient.CallID.Value;
            if (this.Packet.Source is PyAddressNode srcNode && srcNode.CallID != null)
                return srcNode.CallID.Value;
            if (this.Packet.Destination is PyAddressClient dstClient && dstClient.CallID != null)
                return dstClient.CallID.Value;
            if (this.Packet.Destination is PyAddressNode dstNode && dstNode.CallID != null)
                return dstNode.CallID.Value;

            return 0;
        }
    }
}