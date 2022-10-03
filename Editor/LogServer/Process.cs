namespace EVEmuTool.LogServer
{
    public class Process
    {
        public int ID { get; init; }
        public string ComputerName { get; init; }
        public string Name { get; init; }
        public string Binary { get; init; }
        public long ProcessID { get; init; }
        public string Module { get; init; }
        public long StartTime { get; init; } 
        
        public static Process BuildFromByteData(WorkspaceReader reader)
        {
            int id = reader.ReadInt32();
            string computerName = reader.ExpectString();
            string process = reader.ExpectString();
            long processId = reader.ExpectPrefixedInteger();
            reader.ExpectPrefixedInteger();
            string module = reader.ExpectString();
            long starttime = reader.ReadInt64();

            return new Process()
            {
                ID = id,
                ComputerName = computerName,
                Module = module,
                Binary = process,
                ProcessID = processId,
                StartTime = starttime
            };
        }
    }
}