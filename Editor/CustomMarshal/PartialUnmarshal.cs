using Editor.CustomMarshal.CustomTypes;
using EVESharp.PythonTypes;
using EVESharp.PythonTypes.Compression;
using EVESharp.PythonTypes.Marshal;
using EVESharp.PythonTypes.Types.Collections;
using EVESharp.PythonTypes.Types.Database;
using EVESharp.PythonTypes.Types.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Editor.CustomMarshal
{
    // TODO: THIS IMPLEMENTATION SHOULD BE BETTER HANDLED BECAUSE RIGHT NOW THERE'S CODE DUPLICATED FOR UNMARSHALLING
    class PartialUnmarshal : InsightUnmarshal
    {
        /// <summary>
        /// Extracts the PyDataType off the given byte array
        /// </summary>
        /// <param name="data">Byte array to extract the PyDataType from</param>
        /// <param name="expectHeader">Whether a marshal header is expected or not</param>
        /// <returns>The unmarshaled PyDataType</returns>
        public static new InsightUnmarshal ReadFromByteArray(byte[] data, bool expectHeader = true)
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
        public static new InsightUnmarshal ReadFromStream(Stream stream, bool expectHeader = true)
        {
            PartialUnmarshal processor = new PartialUnmarshal(stream);

            PyDataType parent = processor.Process(expectHeader);

            processor.Output = parent;

            return processor;
        }

        protected PartialUnmarshal(Stream stream) : base(stream)
        {
        }

        protected override PyDataType Process(bool expectHeader = true)
        {
            long start = 0;
            bool save = false;
            PyDataType result = null;
            Opcode opcode = Opcode.Error;

            try
            {
                if (expectHeader)
                    this.ProcessPacketHeader();

                start = this.mReader.BaseStream.Position;
                // read the type's opcode from the stream
                byte header = this.mReader.ReadByte();
                opcode = (Opcode)(header & Specification.OPCODE_MASK);
                save = (header & Specification.SAVE_MASK) == Specification.SAVE_MASK;

                result = this.ProcessOpcode(opcode);
            }
            catch (UnmarshallException e)
            {
                result = e.CurrentObject;
            }

            // check if the element has to be saved
            if (save == true)
                this.mSavedList[this.mSavedElementsMap[this.mCurrentSavedIndex++] - 1] = result;

            long end = this.mReader.BaseStream.Position;

            InsightEntry entry = new InsightEntry()
            {
                StartPosition = start,
                EndPosition = end - 1,
                Opcode = opcode,
                Value = result,
                HasSaveFlag = save
            };

            this.Insight.Add(entry);

            return result;
        }

        private PyDataType ProcessGuard(PyDataType partial, bool expectHeader = true)
        {
            try
            {
                return this.Process(expectHeader);
            }
            catch(Exception ex) // catch all for situations like not enough buffer data, etc
            {
                throw new UnmarshallException(ex.Message, partial, this);
            }
        }

        protected override PyDataType ProcessOpcode(Opcode opcode)
        {
            try
            {
                return base.ProcessOpcode(opcode);
            }
            catch(UnmarshallException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new UnmarshallException(ex.Message, null, this);
            }
        }

        /// <summary>
        /// <seealso cref="Marshal.ProcessTuple"/>
        /// 
        /// Opcodes supported:
        /// <seealso cref="Opcode.TupleEmpty"/>
        /// <seealso cref="Opcode.TupleOne"/>
        /// <seealso cref="Opcode.TupleTwo"/>
        /// <seealso cref="Opcode.Tuple"/>
        /// </summary>
        /// <param name="opcode">Type of object to parse</param>
        /// <returns>The decoded python type</returns>
        /// <exception cref="InvalidDataException">If any error was found in the data</exception>
        protected override PyDataType ProcessTuple(Opcode opcode)
        {
            uint count = opcode switch
            {
                Opcode.Tuple => this.mReader.ReadSizeEx(),
                Opcode.TupleOne => 1,
                Opcode.TupleTwo => 2,
                Opcode.TupleEmpty => 0,
                _ => throw new InvalidDataException($"Requested tuple processing with wrong opcode {opcode}")
            };

            PyTuple result = new PyTuple((int) count);

            for (int i = 0; i < count; i++)
            {
                bool exception = false;
                PyDataType element = null;

                try
                {
                    element = this.Process(false);
                }
                catch(UnmarshallException ex)
                {
                    element = ex.CurrentObject;
                    exception = true;
                }

                result[i] = element;

                if (exception)
                    throw new UnmarshallException("Cannot completely parse tuple", result, this);
            }

            return result;
        }

        /// <summary>
        /// <seealso cref="Marshal.ProcessList"/>
        /// 
        /// Opcodes supported:
        /// <seealso cref="Opcode.ListEmpty"/>
        /// <seealso cref="Opcode.List"/>
        /// </summary>
        /// <param name="opcode">Type of object to parse</param>
        /// <returns>The decoded python type</returns>
        /// <exception cref="InvalidDataException">If any error was found in the data</exception>
        protected override PyDataType ProcessList(Opcode opcode)
        {
            uint count = opcode switch
            {
                Opcode.ListEmpty => 0,
                Opcode.ListOne => 1,
                Opcode.List => this.mReader.ReadSizeEx(),
                _ => throw new InvalidDataException($"Requested list processing with wrong opcode {opcode}")
            };

            PyList list = new PyList((int)count);

            for (int i = 0; i < count; i++)
            {
                bool exception = false;
                PyDataType element = null;

                try
                {
                    element = this.Process(false);
                }
                catch (UnmarshallException ex)
                {
                    element = ex.CurrentObject;
                    exception = true;
                }

                list[i] = element;

                if (exception)
                    throw new UnmarshallException("Cannot completely parse list", list, this);
            }

            return list;
        }

        /// <summary>
        /// <seealso cref="Marshal.ProcessDictionary"/>
        /// 
        /// Opcodes supported:
        /// <seealso cref="Opcode.Dictionary"/>
        /// </summary>
        /// <returns>The decoded python type</returns>
        /// <exception cref="InvalidDataException">If any error was found in the data</exception>
        protected override PyDataType ProcessDictionary()
        {
            PyDictionary dictionary = new PyDictionary();
            uint size = this.mReader.ReadSizeEx();

            while (size-- > 0)
            {
                bool exception = false;
                PyDataType value = null;
                PyDataType key = null;

                try
                {
                    value = this.Process(false);
                }
                catch (UnmarshallException ex)
                {
                    exception = true;
                    value = ex.CurrentObject;
                }
                catch(Exception)
                {
                    exception = true;
                }

                try
                {
                    key = this.Process(false) ?? new PyNone();
                }
                catch(UnmarshallException ex)
                {
                    exception = true;
                    key = ex.CurrentObject ?? new PyNone();
                }
                catch(Exception)
                {
                    exception = true;
                    key = new PyNone();
                }

                dictionary[key] = value;

                if (exception)
                    throw new UnmarshallException("One of the dictionary elements cannot be parsed", dictionary, this);
            }

            return dictionary;
        }

        /// <summary>
        /// <seealso cref="Marshal.ProcessPackedRow"/>
        /// 
        /// Opcodes supported:
        /// <seealso cref="Opcode.PackedRow"/>
        /// </summary>
        /// <returns>The decoded python type</returns>
        /// <exception cref="InvalidDataException">If any error was found in the data</exception>
        protected override PyDataType ProcessPackedRow()
        {
            DBRowDescriptor descriptor = this.Process(false);
            Dictionary<string, PyDataType> data = new Dictionary<string, PyDataType>();
            int wholeBytes = 0;
            int nullBits = 0;
            int boolBits = 0;

            List<DBRowDescriptor.Column> booleanColumns = new List<DBRowDescriptor.Column>();

            foreach (DBRowDescriptor.Column column in descriptor.Columns)
            {
                int bitLength = Utils.GetTypeBits(column.Type);

                if (column.Type == FieldType.Bool)
                {
                    booleanColumns.Add(column);
                    boolBits++;
                }

                nullBits++;

                if (bitLength >= 8)
                    wholeBytes += bitLength >> 3;
            }

            // sort columns by the bit size and calculate other statistics for the PackedRow
            IOrderedEnumerable<DBRowDescriptor.Column> enumerator = descriptor.Columns.OrderByDescending(c => Utils.GetTypeBits(c.Type));

            MemoryStream decompressedStream = ZeroCompressionUtils.LoadZeroCompressed(this.mReader, wholeBytes + ((nullBits + boolBits) >> 3) + 1);
            BinaryReader decompressedReader = new BinaryReader(decompressedStream);
            byte[] fullBuffer = decompressedStream.GetBuffer();

            foreach (DBRowDescriptor.Column column in enumerator)
            {
                int bit = (wholeBytes << 3) + descriptor.Columns.IndexOf(column) + boolBits;
                bool isNull = (fullBuffer[bit >> 3] & (1 << (bit & 0x7))) == (1 << (bit & 0x7));

                switch (column.Type)
                {
                    case FieldType.UI8:
                        data[column.Name] = new PyInteger((long)decompressedReader.ReadUInt64());
                        break;
                    case FieldType.I8:
                    case FieldType.CY:
                    case FieldType.FileTime:
                        data[column.Name] = new PyInteger(decompressedReader.ReadInt64());
                        break;
                    case FieldType.I4:
                        data[column.Name] = new PyInteger(decompressedReader.ReadInt32());
                        break;
                    case FieldType.UI4:
                        data[column.Name] = new PyInteger(decompressedReader.ReadUInt32());
                        break;
                    case FieldType.UI2:
                        data[column.Name] = new PyInteger(decompressedReader.ReadUInt16());
                        break;
                    case FieldType.I2:
                        data[column.Name] = new PyInteger(decompressedReader.ReadInt16());
                        break;
                    case FieldType.I1:
                        data[column.Name] = new PyInteger(decompressedReader.ReadSByte());
                        break;
                    case FieldType.UI1:
                        data[column.Name] = new PyInteger(decompressedReader.ReadByte());
                        break;
                    case FieldType.R8:
                        data[column.Name] = new PyDecimal(decompressedReader.ReadDouble());
                        break;
                    case FieldType.R4:
                        data[column.Name] = new PyDecimal(decompressedReader.ReadSingle());
                        break;
                    case FieldType.Bool:
                        {
                            int boolBit = (wholeBytes << 3) + booleanColumns.IndexOf(column);
                            bool isTrue = (fullBuffer[boolBit >> 3] & (1 << (boolBit & 0x7))) == (1 << (boolBit & 0x7));

                            data[column.Name] = new PyBool(isTrue);
                        }
                        break;
                    case FieldType.Bytes:
                    case FieldType.WStr:
                    case FieldType.Str:
                        data[column.Name] = this.Process(false);
                        break;

                    default:
                        throw new InvalidDataException($"Unknown column type {column.Type}");
                }

                if (isNull == true)
                {
                    data[column.Name] = null;
                }
            }

            return new PyPackedRow(descriptor, data);
        }

        /// <summary>
        /// <seealso cref="Marshal.ProcessObjectData"/>
        /// 
        /// Opcodes supported:
        /// <seealso cref="Opcode.ObjectData"/>
        /// </summary>
        /// <returns>The decoded python type</returns>
        /// <exception cref="InvalidDataException">If any error was found in the data</exception>
        protected override PyDataType ProcessObjectData()
        {
            PyString name = this.ProcessGuard(new PyObjectData(null, null), false) as PyString;
            PyDataType data = null;

            try
            {
                data = this.Process(false);
            }
            catch(UnmarshallException ex)
            {
                throw new UnmarshallException($"Error parsing object data information", new PyObjectData(name, ex.CurrentObject), this);
            }

            return new PyObjectData(
                name, data
            );
        }

        /// <summary>
        /// <seealso cref="Marshal.ProcessObject"/>
        /// 
        /// Opcodes supported:
        /// <seealso cref="Opcode.ObjectType1"/>
        /// <seealso cref="Opcode.ObjectType2"/>
        /// </summary>
        /// <param name="opcode">Type of object to parse</param>
        /// <returns>The decoded python type</returns>
        /// <exception cref="InvalidDataException">If any error was found in the data</exception>
        protected override PyDataType ProcessObject(Opcode opcode)
        {
            bool isType2 = opcode == Opcode.ObjectType2;

            PyTuple header = this.ProcessGuard(new PyObject(isType2, null, null, null), false) as PyTuple;
            PyList list = new PyList();
            PyDictionary dict = new PyDictionary();

            while (this.mReader.PeekChar() != Marshal.PACKED_TERMINATOR)
                list.Add(this.ProcessGuard(new PyObject(isType2, null, list, null), false));

            // ignore packed terminator
            this.mReader.ReadByte();

            while (this.mReader.PeekChar() != Marshal.PACKED_TERMINATOR)
            {
                PyString key = this.ProcessGuard(new PyObject(isType2, null, list, dict), false) as PyString;
                PyDataType value = this.ProcessGuard(new PyObject(isType2, null, list, dict), false);

                dict[key] = value;
            }

            // ignore packed terminator
            this.mReader.ReadByte();

            return new PyObject(opcode == Opcode.ObjectType2, header, list, dict);
        }

        /// <summary>
        /// <seealso cref="Marshal.ProcessSubStruct"/>
        /// 
        /// Opcodes supported:
        /// <seealso cref="Opcode.SubStruct"/>
        /// </summary>
        /// <returns>The decoded python type</returns>
        /// <exception cref="InvalidDataException">If any error was found in the data</exception>
        protected override PyDataType ProcessSubStruct()
        {
            return new PySubStruct(
                this.ProcessGuard(new PySubStruct(null), false)
            );
        }

        /// <summary>
        /// <seealso cref="Marshal.ProcessChecksumedStream"/>
        /// 
        /// Opcodes supported:
        /// <seealso cref="Opcode.ChecksumedStream"/>
        /// </summary>
        /// <returns>The decoded python type</returns>
        /// <exception cref="InvalidDataException">If any error was found in the data</exception>
        protected override PyDataType ProcessChecksumedStream()
        {
            uint checksum = this.mReader.ReadUInt32();

            return new PyChecksumedStream(
                this.ProcessGuard(new PyChecksumedStream(null), false)
            );
        }
    }
}
