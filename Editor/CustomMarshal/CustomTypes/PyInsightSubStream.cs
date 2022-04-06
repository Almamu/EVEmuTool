using EVESharp.PythonTypes;
using EVESharp.PythonTypes.Marshal;
using EVESharp.PythonTypes.Types.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.CustomMarshal.CustomTypes
{
    public class PyInsightSubStream : PySubStream
    {
        private bool mIsUnmarshaled = false;
        private InsightUnmarshal mCurrentStream = null;
        private InsightUnmarshal mOriginalStream = null;
        private byte[] mByteStream = null;

        public InsightUnmarshal UnmarshalStream
        {
            get
            {
                if (this.mIsUnmarshaled == false)
                    this.mOriginalStream = this.mCurrentStream = PartialUnmarshal.ReadFromByteArray(this.mByteStream);

                return this.mCurrentStream;
            }

            set
            {
                throw new NotImplementedException("Setting PySubStream objects is not supported for PyInsightSubStream");
            }
        }

        public new PyDataType Stream
        {
            get => this.UnmarshalStream.Output;
            set => throw new NotImplementedException("Setting PySubStream object is not supported for PyInsightSubStream");
        }

        public new byte[] ByteStream
        {
            get
            {
                // check hash codes and types to ensure they're equal
                if (this.mByteStream is not null && (this.mIsUnmarshaled == false || this.mCurrentStream == this.mOriginalStream))
                    return this.mByteStream;

                // make sure the old and new value are the same so checks work fine
                this.mOriginalStream = this.mCurrentStream;

                // update the byte stream with the new value
                return this.mByteStream = Marshal.ToByteArray(this.mCurrentStream.Output);
            }
        }

        public PyInsightSubStream(byte[] from) : base(null)
        {
            this.mIsUnmarshaled = false;
            this.mByteStream = from;
        }

        public override int GetHashCode()
        {
            return (int)CRC32.Checksum(this.ByteStream) ^ 0x35415879;
        }
    }
}
