using EVESharp.Common.Logging;
using EVESharp.Common.Network.Messages;
using EVESharp.PythonTypes.Marshal;
using EVESharp.PythonTypes.Types.Collections;
using EVESharp.PythonTypes.Types.Network;
using EVESharp.PythonTypes.Types.Primitives;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Capture
{
    public class CaptureProcessor
    {
        public event EventHandler<CaptureEntry> OnPacketCaptured;

        public CaptureProcessor()
        {
        }

        public void HandleMessage(CaptureMessage message)
        {
            // process the packetizer
            int packets = message.Packetizer.ProcessPackets();

            while (packets-- > 0)
            {
                byte[] data = message.Packetizer.PopItem();
                PyDataType processed = Unmarshal.ReadFromByteArray(data);
                PyPacket parsed = null;
                long callID = 0;
                string service = "";
                string method = "";
                string source = "";
                string destination = "";
                string packetType = "LLV";
                PyPacket.PacketType type = PyPacket.PacketType.__Fake_Invalid_Type;

                try
                {
                    parsed = processed;

                    source = ConvertAddress(parsed.Source);
                    destination = ConvertAddress(parsed.Destination);

                    if (parsed.Type == PyPacket.PacketType.CALL_REQ ||
                        parsed.Type == PyPacket.PacketType.CALL_RSP)
                    {
                        callID = ExtractCallID(parsed);
                        service = ExtractService(parsed.Source) + ExtractService(parsed.Destination);
                        packetType = ExtractPacketType(parsed);
                        type = parsed.Type;

                        if (parsed.Type == PyPacket.PacketType.CALL_REQ)
                        {
                            PyTuple callInformation = ((parsed.Payload[0] as PyTuple)[1] as PySubStream).Stream as PyTuple;

                            // there was no name, try to infer it from bound services
                            if (service.Length == 0 && parsed.Type == PyPacket.PacketType.CALL_REQ)
                            {
                                PyString id = callInformation[0] as PyString;

                                try
                                {
                                    // resolve the bound service with the id
                                    service = message.Capturer.ResolveBoundService(id);
                                }
                                catch (Exception ex)
                                {

                                }
                            }

                            service ??= "Unknown";

                            method = ExtractMethod(message.Capturer, callInformation);
                        }

                        if (parsed.Type == PyPacket.PacketType.CALL_REQ)
                            message.Capturer.RegisterServiceCall(callID, service, method);

                        if (parsed.Type == PyPacket.PacketType.CALL_RSP)
                            message.Capturer.FinishServiceCall(callID, out service, out method);

                        if (parsed.Type == PyPacket.PacketType.CALL_RSP)
                        {
                            PySubStream subStream = parsed.Payload[0] as PySubStream;

                            // call information should be properly resolved now
                            if (method.EndsWith(" (MachoBindObject)") == true || method == "MachoBindObject")
                            {
                                // store the information for resolving this bound
                                PyString id =
                                    ((((subStream.Stream as PyTuple)[0] as PySubStruct)
                                        .Definition as PySubStream).Stream as PyTuple)[0] as PyString;

                                message.Capturer.RegisterBoundService(id, service + " (bound)");
                            }
                            else if (method == "GetInventory" || method == "GetInventoryFromId")
                            {
                                // store the information for resolving this bound
                                PyString id =
                                    ((((subStream.Stream as PyTuple)[0] as PySubStruct)
                                        .Definition as PySubStream).Stream as PyTuple)[0] as PyString;

                                message.Capturer.RegisterBoundService(id, "BoundInventory (bound)");
                            }
                            else if(subStream.Stream is PyObjectData == true)
                            {
                                PyObjectData objectData = subStream.Stream as PyObjectData;

                                if (objectData.Name == "util.SparseRowset")
                                {
                                    // get the data off it
                                    PyString id = ((((objectData.Arguments as PyTuple)[1] as PySubStruct).Definition as PySubStream)
                                        .Stream as PyTuple)[0] as PyString;

                                    message.Capturer.RegisterBoundService(id, "SparseRowset (bound) " + id.Value);

                                }
                            }
                        }
                    }
                }
                catch(Exception)
                {
                    // ignored
                }

                CaptureEntry entry = new CaptureEntry()
                {
                    Call = method,
                    CallID = callID,
                    ClientID = 0,
                    Direction = message.Origin == message.Capturer.Server ? PacketDirection.ServerToClient : PacketDirection.ClientToServer,
                    RawData = data,
                    Service = service,
                    Timestamp = DateTime.Now,
                    PacketType = packetType,
                    Destination = destination,
                    Source = source
                };

                this.OnPacketCaptured?.Invoke(this, entry);
            }
        }

        private static string ExtractPacketType(PyPacket packet)
        {
            return packet.TypeString;
        }

        private static string ConvertAddress(PyAddress address)
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

        private static string ExtractService(PyAddress address)
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

        private static long ExtractCallID(PyPacket packet)
        {
            if (packet.Source is PyAddressClient srcClient && srcClient.CallID is not null)
                return srcClient.CallID;
            if (packet.Source is PyAddressNode srcNode && srcNode.CallID is not null)
                return srcNode.CallID;
            if (packet.Destination is PyAddressClient dstClient && dstClient.CallID is not null)
                return dstClient.CallID;
            if (packet.Destination is PyAddressNode dstNode && dstNode.CallID is not null)
                return dstNode.CallID;

            return 0;
        }

        private static string ExtractMethod(CaptureSocket capturer, PyTuple callInformation)
        {
            // inspect the packet and extract the method's name
            PyString call = callInformation[1] as PyString;

            if (call == "MachoBindObject")
            {
                // get the resulting bound id
                if (callInformation[2] is PyTuple data)
                {
                    if (data[1] is PyTuple boundCallInfo)
                    {
                        if (boundCallInfo[0] is PyString boundCall)
                            return $"{boundCall.Value} ({call.Value}";
                    }
                }
            }

            return call;
        }
    }
}
