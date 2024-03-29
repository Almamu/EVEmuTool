﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EVEmuTool.EmbedFS
{
    /// <summary>
    /// Simple class that allows working on .stuff files from EVE Online
    /// </summary>
    public class EmbedFSFile : IEmbedFS
    {
        private BinaryReader mInput;
        private List<StuffEntry> mFiles = new List<StuffEntry>();
        /// <summary>
        /// The found files in the stuff file
        /// </summary>
        public IEnumerable<StuffEntry> Files => this.mFiles;

        public EmbedFSFile (Stream input)
        {
            this.mInput = new BinaryReader (input);
        }

        public void InitializeFile ()
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

                this.mFiles.Add(
                    new StuffEntry
                    {
                        Origin = this,
                        FileName = filename.Replace ('\\', '/'),
                        Length = length
                    }
                );
            }

            long baseOffset = this.mInput.BaseStream.Position;

            for (int i = 0; i < fileCount; i++)
            {
                this.mFiles[i].Offset = baseOffset;
                baseOffset += this.mFiles[i].Length;
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
