using System;

namespace EVEmuTool.LogServer
{
    public class WorkspaceFile
    {
        public long Version { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public long Created { get; init; }
        public long Modified { get; init; }
        public string Filename { get; init; }

        public Device[] Devices { get; init; }

        public static WorkspaceFile BuildFromByteData(WorkspaceReader reader)
        {
            long version = reader.ExpectPrefixedInteger();

            if (version != 0x09)
                throw new Exception("Only version 9 of the LBW file is supported");

            return new WorkspaceFile()
            {
                Version = version,
                Name = reader.ExpectString(),
                Description = reader.ExpectString(),
                Created = reader.ExpectPrefixedLong(),
                Modified = reader.ExpectPrefixedLong(),
                Filename = reader.ExpectString(),
                Devices = Device.BuildFromByteData(reader)
            };
        }
    }
}