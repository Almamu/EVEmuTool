using EVESharp.Common;
using EVESharp.Types;
using EVESharp.Types.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace EVEmuTool.CustomMarshal
{
    public class CustomUnmarshal : Unmarshal
    {
        /// <summary>
        /// Extracts the PyDataType off the given byte array
        /// </summary>
        /// <param name="data">Byte array to extract the PyDataType from</param>
        /// <param name="expectHeader">Whether a marshal header is expected or not</param>
        /// <returns>The unmarshaled PyDataType</returns>
        public new static PyDataType ReadFromByteArray(byte[] data, bool expectHeader = true)
        {
            MemoryStream stream = new MemoryStream(data);

            return ReadFromStream(stream, expectHeader);
        }

        /// <summary>
        /// Extracts the PyDataType off the given stream
        /// </summary>
        /// <param name="stream">Stream to extract the PyDataType from</param>
        /// <param name="expectHeader">Whether a marshal header is expected or not</param>
        /// <returns>The unmarshaled PyDataType</returns>
        public new static PyDataType ReadFromStream(Stream stream, bool expectHeader = true)
        {
            CustomUnmarshal processor = new CustomUnmarshal(stream);

            return processor.Process(expectHeader);
        }
        
        protected CustomUnmarshal(Stream stream) : base(stream)
        {
        }

        protected override PyDataType ProcessString(Opcode opcode)
        {
            switch (opcode)
            {
                case Opcode.StringLong:
                    // select some arbitrary limit for pybuffer diferentiation
                    int length = (int) this.mReader.ReadSizeEx();

                    if (length < 10000)
                        return new PyString(Encoding.ASCII.GetString(this.mReader.ReadBytes(length)));

                    return new PyBuffer(this.mReader.ReadBytes(length));
                
                default:
                    return base.ProcessString(opcode);
            }
        }
    }
}