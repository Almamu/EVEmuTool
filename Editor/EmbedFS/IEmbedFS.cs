using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEmuTool.EmbedFS
{
    public interface IEmbedFS
    {
        /// <summary>
        /// Provides access to all the files inside this container
        /// </summary>
        public IEnumerable<StuffEntry> Files { get; }

        /// <summary>
        /// Initializes the file and reads the headers
        /// </summary>
        public void InitializeFile();

        /// <summary>
        /// Obtains a MemoryStream of the given path inside the EmbedFS container
        /// </summary>
        /// <param name="path">File to get contents from</param>
        /// <returns>The contents of the file</returns>
        public MemoryStream ResolveFile(string path)
        {
            path = path.ToLower();

            // remove the : from the beginning as that's not used
            path = path.Replace("res:/", "res/");

            StuffEntry entry = this.Files.FirstOrDefault(y => y.FileName.ToLower() == path);
            MemoryStream stream = new MemoryStream(entry.Length);

            entry.Origin.Export(stream, entry);

            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        /// <summary>
        /// Exports a file from the container into the given output stream
        /// </summary>
        /// <param name="output"></param>
        /// <param name="entry"></param>
        public void Export(Stream output, StuffEntry entry);
    }
}
