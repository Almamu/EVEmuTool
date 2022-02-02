using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Editor.LogServer
{
    public class WorkspaceReader : BinaryReader
    {
        private long _ReadInteger()
        {
            switch (this.ReadByte())
            {
                case 0x02: return this.ReadSByte();
                case 0x03: return this.ReadInt16();
                case 0x04: return this.ReadInt32();
                case 0x11: return this.ReadInt64();
                default: throw new Exception("Unknown integer");
            }
        }
        
        public long ExpectPrefixedInteger()
        {
            switch (this.PeekChar())
            {
                case 0x02:
                case 0x03:
                case 0x04:
                    return this._ReadInteger();
                
                default: throw new Exception("Expected prefixed normal integer value");
            }
        }

        public long ExpectPrefixedLong()
        {
            switch (this.PeekChar())
            {
                case 0x02:
                case 0x03:
                case 0x04:
                case 0x11:
                    return this._ReadInteger();
                default:
                    throw new Exception("Expected prefixed long integer");
            }
        }
        
        public string ExpectString()
        {
            switch (this.ReadByte())
            {
                case 0x06: return Encoding.ASCII.GetString(this.ReadBytes(this.ReadByte()));
                case 0x0C: return Encoding.ASCII.GetString(this.ReadBytes(this.ReadInt32()));
                default: throw new Exception("Expected typed string");
            }
        }

        public bool ExpectBoolean()
        {
            byte type = this.ReadByte();

            if (type != 0x08 && type != 0x09)
                throw new Exception("Expected typed boolean");

            return type == 0x09;
        }

        public long ExpectNumber()
        {
            switch (this.ReadByte())
            {
                case 0x02: return this.ReadByte();
                case 0x03: return this.ReadInt16();
                case 0x04: return this.ReadInt32();
                case 0x08: return 0;
                case 0x09: return 1;
                case 0x0D: return 0; // nil value
                case 0x11: return this.ReadInt64();
                default: throw new Exception("Expected number type");
            }
        }

        public string ReadUnprefixedString(int length)
        {
            byte[] buffer = this.ReadBytes(length);
            
            // find the first 0x00 and erase everything else
            int index = Array.FindIndex(buffer, x => x == 0);
            
            // fill the rest of the array with the right value
            Array.Fill(buffer, (byte) 0x00, index, length - index);

            // finally convert it into a string
            return Encoding.ASCII.GetString(buffer);
        }

        public WorkspaceReader(Stream input) : base(input)
        {
        }

        public WorkspaceReader(Stream input, Encoding encoding) : base(input, encoding)
        {
        }

        public WorkspaceReader(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen)
        {
        }
    }
}