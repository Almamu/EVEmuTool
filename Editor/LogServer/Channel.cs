using System.Collections.Generic;
using System.Text;

namespace Editor.LogServer
{
    public class Channel
    {
        public long FacilityHash { get; init; }
        public string Facility { get; init; }
        public long ObjectHash { get; init; }
        public string Object { get; init; }
        public Source Source { get; set; }

        public static Channel BuildFromByteData(WorkspaceReader reader)
        {
            // channels contain direct data from the logserver memory
            // so these do not have any prefixed values in it
            // parse every object and extract it's facility
            long facilityHash = reader.ReadInt64();
            long objectHash = reader.ReadInt64();
            string facilityName = reader.ReadUnprefixedString(32);
            reader.ReadByte(); // ignore this byte for unknown reasons for now
            string objectName = reader.ReadUnprefixedString(32);
            reader.ReadByte(); // ignore this byte for unknown reasons for now

            reader.ReadInt16(); // ignore these two bytes as they're not understood yet
            reader.ReadInt32(); // ignore log channel suppression
            reader.ReadInt32(); // ignore log type suppression
            reader.ReadInt32(); // ignore unknown data
            
            return new Channel()
            {
                Facility = facilityName,
                FacilityHash = facilityHash,
                Object = objectName,
                ObjectHash = objectHash
            };
        }
    }
}