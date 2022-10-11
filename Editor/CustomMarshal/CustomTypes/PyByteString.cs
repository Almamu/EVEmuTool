using EVESharp.Types;
using EVESharp.Types.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEmuTool.CustomMarshal.CustomTypes
{
    public class PyByteString : PyString
    {
        public byte[] ByteData { get; }
        public PyByteString(StringTableUtils.EntryList entry) : base(entry)
        {
        }

        public PyByteString(string value, bool isUTF8 = false) : base(value, isUTF8)
        {
        }

        public PyByteString(byte[] data, bool isUTF8 = false) : base(Encoding.ASCII.GetString(data), isUTF8)
        {
            this.ByteData = data;
        }
    }
}
