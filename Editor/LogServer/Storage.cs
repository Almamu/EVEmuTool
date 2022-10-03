using System;
using System.IO;

namespace EVEmuTool.LogServer
{
    public class Storage
    {
        public long ID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public long InitialCapacity { get; init; }
        public long IncrementCapacity { get; init; }
        public long LogStreamSize { get; init; }
        public long OverlapLogStreamSize { get; init; }
        public long NumLogs { get; init; }
        public long NumOverlapLogs { get; init; }
        public LogLine[] Lines { get; init; }

        public static Storage BuildFromByteData(WorkspaceReader reader)
        {
            string name = reader.ExpectString();
            string description = reader.ExpectString();
            long created = reader.ExpectPrefixedLong();
            long modified = reader.ExpectPrefixedLong();
            long logbaseID = reader.ReadInt64(); // unknown value
            long initialCapacity = reader.ExpectPrefixedInteger();
            long incrementCapacity = reader.ExpectPrefixedInteger();
            bool allwaysShowErrors = reader.ExpectBoolean();
            bool channelFilter = reader.ExpectBoolean();
            // this is exclusive to version 8 and 9 of the file, older versions do not have this int64
            reader.ReadInt64(); // ignore these bytes
            // read all the bytes that indicate the filters, these can be ignored as they're not useful to us
            while (reader.ExpectPrefixedInteger() >= 0) ;

            bool filterFlags = reader.ExpectBoolean();
            reader.ExpectPrefixedInteger();
            bool hasMessages = reader.ExpectBoolean();
            LogLine[] logLines = null;

            if (hasMessages)
            {
                long logCount = reader.ExpectPrefixedInteger();
                long overlappedLogCount = reader.ExpectPrefixedInteger();

                if (overlappedLogCount > 0)
                    throw new Exception("Found overlapped logs in a storage! Do not know how to handle them yet!");

                logLines = new LogLine[logCount];

                for (long currentLog = 0; currentLog < logCount; currentLog++)
                    logLines[currentLog] = LogLine.BuildFromByteData(reader);
            }
            
            return new Storage()
            {
                Name = name,
                Description = description,
                ID = logbaseID,
                IncrementCapacity = incrementCapacity,
                InitialCapacity = initialCapacity,
                Lines = logLines,
                LogStreamSize = 0,
                NumLogs = logLines == null ? 0 : logLines.Length,
                NumOverlapLogs = 0,
                OverlapLogStreamSize = 0
            };
        }
    }
}