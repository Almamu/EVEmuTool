using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Editor.EmbedFS
{
    public class EmbedFSFile
    {
        private BinaryReader mInput;
        /// <summary>
        /// The found files in the stuff file
        /// </summary>
        public List<StuffEntry> Files { get; } = new List<StuffEntry>();

        public EmbedFSFile (Stream input)
        {
            this.mInput = new BinaryReader (input);
        }

        public void ReadFile ()
        {
            this.ReadFileHeader();
            this.ReadFileTree();
        }

        private void ReadFileHeader()
        {
            // first ensure the footer of the file is right
            this.mInput.BaseStream.Seek(-(Definition.FOOTER.Length + 1), SeekOrigin.End);
            byte[] footer = this.mInput.ReadBytes (Definition.FOOTER.Length);

            if (Encoding.ASCII.GetString (footer) != Definition.FOOTER)
            {
                throw new InvalidDataException("File Footer does not match expected value");
            }
        }

        private void ReadFileTree()
        {
            // go to the beginning of the file
            this.mInput.BaseStream.Seek(0, SeekOrigin.Begin);
            int fileCount = this.mInput.ReadInt32();

            for (int i = 0; i < fileCount; i++)
            {
                int length = this.mInput.ReadInt32();
                int nameLength = this.mInput.ReadInt32();
                string filename = Encoding.ASCII.GetString(this.mInput.ReadBytes(nameLength));
                this.mInput.BaseStream.Seek(1, SeekOrigin.Current); // skip the nullbyte

                this.Files.Add(
                    new StuffEntry
                    {
                        FileName = filename.Replace ('\\', '/'),
                        Length = length
                    }
                );
            }

            long baseOffset = this.mInput.BaseStream.Position;

            for (int i = 0; i < fileCount; i++)
            {
                this.Files[i].Offset = baseOffset;
                baseOffset += this.Files[i].Length;
            }
        }

        public void Export (Stream output, StuffEntry entry)
        {
            this.mInput.BaseStream.Seek (entry.Offset, SeekOrigin.Begin);

            byte[] buffer = new byte[32768];
            long read;
            long left = entry.Length;

            while (left > 0 && 
                (read = this.mInput.BaseStream.Read (buffer, 0, (int) Math.Min(left, buffer.Length))) > 0)
            {
                output.Write(buffer, 0, (int) read);
                left -= read;
            }
        }
    }
}
