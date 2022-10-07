using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EVEmuTool.EmbedFS
{
    public class EmbedFSDirectory : IEmbedFS
    {
        private List<IEmbedFS> mFiles = new List<IEmbedFS>();

        public IEnumerable<StuffEntry> Files => this.mFiles.SelectMany(x => x.Files);

        public EmbedFSDirectory(string[] files)
        {
            foreach (string file in files)
                this.mFiles.Add(new EmbedFSFile(File.OpenRead(file)));
        }


        public void InitializeFile()
        {
            foreach (IEmbedFS file in this.mFiles)
                file.InitializeFile();
        }

        public void Export(Stream output, StuffEntry entry)
        {
            // directories are just a holder for a list of embedFS files
            // call the original container and export the file
            entry.Origin.Export(output, entry);
        }
    }
}
