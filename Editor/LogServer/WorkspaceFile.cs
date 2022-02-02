using System.IO;

namespace Editor.LogServer
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
            return new WorkspaceFile()
            {
                Version = reader.ExpectPrefixedInteger(),
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