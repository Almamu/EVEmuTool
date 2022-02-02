namespace Editor.LogServer
{
    public class Source
    {
        public string Name { get; init; }
        public Process[] Processes { get; init; }

        public static Source BuildFromByteData(WorkspaceReader reader)
        {
            string name = reader.ExpectString();
            string description = reader.ExpectString();
            long created = reader.ExpectPrefixedLong();
            long modified = reader.ExpectPrefixedLong();
            long processesCount = reader.ExpectPrefixedInteger();

            Process[] processes = new Process[processesCount];

            for (long currentProcess = 0; currentProcess < processesCount; currentProcess++)
                processes[currentProcess] = Process.BuildFromByteData(reader);
            
            return new Source()
            {
                Name = name,
                Processes = processes
            };
        }
    }
}