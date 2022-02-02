using System.Text;

namespace Editor.LogServer
{
    public class LogLine
    {
        public int ThreadID { get; init; }
        public long Timestamp { get; init; }
        public int ProcessID { get; init; }
        public int SourceID { get; init; }
        public int LogLevel { get; init; }
        public string Line { get; init; }

        public static LogLine BuildFromByteData(WorkspaceReader reader)
        {
            short sourceId = (short) (reader.ReadInt16() - 1); // the id is 1-based instead of 0-based
            //reader.ReadByte();
            //reader.ReadByte();
            int threadId = reader.ReadInt32();
            long timestamp = reader.ReadInt64();
            int logLevel = reader.ReadInt32();
            
            // different versions handle this differently, versions 8 or lower read the string
            // as a 0xFF long string, regardless of what it contains
            // 9 or greater uses a prefix with the string length
            int length = reader.ReadInt32();
            string line = Encoding.ASCII.GetString(reader.ReadBytes(length));
            // reader.ReadByte();
            int processId = reader.ReadInt32();
            int unk = reader.ReadInt32();
            // reader.ReadByte();
            // reader.ReadByte();
            // reader.ReadByte();
            // reader.ReadByte();
            
            return new LogLine()
            {
                Line = line,
                ThreadID = threadId,
                Timestamp = timestamp,
                ProcessID = processId,
                SourceID = sourceId,
                LogLevel = logLevel
            };
        }
    }
}