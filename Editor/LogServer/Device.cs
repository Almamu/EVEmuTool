using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Editor.LogServer
{
    public class Device
    {
        public long ID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public long Capacity { get; init; }
        public long FlushInterval { get; init; }
        public long Created { get; init; }
        public long Modified { get; init; }
        public long LogChannelCounter { get; init; }
        public long NumLogChannels { get; init; }

        public Channel[] Channels { get; init; }
        public Storage[] Storages { get; init; }

        public static Device[] BuildFromByteData(WorkspaceReader reader)
        {
            long deviceCount = reader.ExpectPrefixedInteger();
            Device[] devices = new Device[deviceCount];

            for (long current = 0; current < deviceCount; current++)
            {
                string name = reader.ExpectString();
                string description = reader.ExpectString();
                long created = reader.ExpectPrefixedLong();
                long modified = reader.ExpectPrefixedLong();
                long id = reader.ReadInt64(); // get the mapping's ID (this is not prefixed by anything)
                reader.ExpectString(); // ignore this string as the logserver forces it to be tha same as the name
                long flushInterval = reader.ExpectPrefixedInteger();
                long capacity = reader.ExpectPrefixedInteger();
                reader.ReadInt64(); // ignore this id as it's repeated for some reason
                long logChannelCounter = reader.ExpectPrefixedInteger();
                long logChannelNumChannels = reader.ExpectPrefixedInteger();
                
                // now things get a little messy, information about the channels is stored as raw bytes
                // there's no nice marshalled data in there
                // so the only way is to bruteforce them with what we know (which is not much)
                Channel[] channels = new Channel[logChannelNumChannels];

                for (long currentChannel = 0; currentChannel < logChannelNumChannels; currentChannel++)
                    channels[currentChannel] = Channel.BuildFromByteData(reader);

                foreach (Channel channel in channels)
                    channel.Source = Source.BuildFromByteData(reader);
                
                // now on to reading the log storages configuration
                reader.ReadInt16(); // ignore two bytes that we do not understand
                long storageCount = reader.ExpectNumber();

                Storage[] storages = new Storage[storageCount];
                
                for(long currentStorage = 0; currentStorage < storageCount; currentStorage++)
                    storages[currentStorage] = Storage.BuildFromByteData(reader);

                devices[current] = new Device()
                {
                    Name = name,
                    Description = description,
                    Capacity = capacity,
                    Created = created,
                    FlushInterval = flushInterval,
                    ID = id,
                    Modified = modified,
                    LogChannelCounter = logChannelCounter,
                    NumLogChannels = logChannelNumChannels,
                    Channels = channels,
                    Storages = storages
                };
            }

            return devices;
        }
    }
}