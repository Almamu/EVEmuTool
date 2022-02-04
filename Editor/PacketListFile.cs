using EVESharp.PythonTypes.Marshal;
using EVESharp.PythonTypes.Types.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Editor
{
    public static class PacketListFile
    {
        public static uint MAGIC_HEADER = 0xDEADBEEF;

        public static void LoadPackets(string input, out List<PacketEntry> packets, out List<Client> clients)
        {
            FileStream inputStream = File.OpenRead(input);
            BinaryReader reader = new BinaryReader(inputStream);

            if (reader.ReadUInt32() != MAGIC_HEADER)
                throw new Exception("Unexpected file header");
            int clientCount = reader.ReadInt32();
            int packetCount = reader.ReadInt32();
            
            packets = new List<PacketEntry>(packetCount);
            clients = new List<Client>(clientCount);
            
            // read clients first
            for (int i = 0; i < clientCount; i++)
            {
                int index = reader.ReadInt32();
                Client client = new Client(index);

                client.Address = reader.ReadString();
                client.Username = reader.ReadString();

                int boundServicesCount = reader.ReadInt32();
                int serviceListCount = reader.ReadInt32();
                int callIdListCount = reader.ReadInt32();

                for (int n = 0; n < boundServicesCount; n++)
                    client.BoundServices[reader.ReadString()] = reader.ReadString();

                for (int n = 0; n < serviceListCount; n++)
                    client.ServiceList[reader.ReadInt64()] = reader.ReadString();

                for (int n = 0; n < callIdListCount; n++)
                    client.CallIDList[reader.ReadInt64()] = reader.ReadString();
                
                clients.Insert(index, client);
            }
            
            // read packets
            for (int i = 0; i < packetCount; i++)
            {
                int clientIndex = reader.ReadInt32();
                long timestamp = reader.ReadInt64();
                int direction = reader.ReadInt32();
                bool isPacket = reader.ReadBoolean();
                PyDataType data = Unmarshal.ReadFromStream(inputStream);
                
                PacketEntry entry = new PacketEntry()
                {
                    Client = clients[clientIndex],
                    Direction = (PacketDirection) direction,
                    RawPacket = data,
                    Timestamp = DateTime.FromFileTimeUtc(timestamp)
                };

                if (isPacket)
                    entry.Packet = data;

                packets.Add(entry);
            }
            
            inputStream.Close();
        }

        public static void SavePackets(string output, List<PacketEntry> packets, List<Client> clients)
        {
            // create the output file
            FileStream outputStream = File.OpenWrite(output);
            BinaryWriter writer = new BinaryWriter(outputStream);
            
            // write the magic header
            writer.Write(MAGIC_HEADER);
            // write the amount of clients registered
            writer.Write(clients.Count);
            // write the amount of packets registered
            writer.Write(packets.Count);
            
            // start writing clients
            foreach (Client client in clients)
            {
                // write basic information
                writer.Write(client.ClientIndex);
                writer.Write(client.Address);
                writer.Write(client.Username);
                // write length of caches
                writer.Write(client.BoundServices.Count);
                writer.Write(client.ServiceList.Count);
                writer.Write(client.CallIDList.Count);
                // write caches
                foreach (KeyValuePair<string, string> boundPair in client.BoundServices)
                {
                    writer.Write(boundPair.Key);
                    writer.Write(boundPair.Value);
                }
                foreach (KeyValuePair<long, string> boundPair in client.ServiceList)
                {
                    writer.Write(boundPair.Key);
                    writer.Write(boundPair.Value);
                }
                foreach (KeyValuePair<long, string> boundPair in client.CallIDList)
                {
                    writer.Write(boundPair.Key);
                    writer.Write(boundPair.Value);
                }
            }
            // write packet data
            foreach (PacketEntry entry in packets)
            {
                writer.Write(entry.ClientIndex);
                writer.Write(entry.Timestamp.ToFileTimeUtc());
                writer.Write((int) entry.Direction);
                writer.Write(entry.Packet != null);
                // write marshal data
                Marshal.WriteToStream(outputStream, entry.RawPacket);
            }

            writer.Flush();
            writer.Close();
            outputStream.Close();
        }
    }
}