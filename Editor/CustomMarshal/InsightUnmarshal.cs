using System.Collections.Generic;
using System.IO;
using EVEmuTool.CustomMarshal.CustomTypes;
using EVESharp.PythonTypes;
using EVESharp.PythonTypes.Marshal;
using EVESharp.PythonTypes.Types.Primitives;

namespace EVEmuTool.CustomMarshal;

public class InsightUnmarshal : Unmarshal
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
        InsightUnmarshal processor = new InsightUnmarshal(stream);

        PyDataType parent = processor.Process(expectHeader);

        processor.Output = parent;

        return processor;
    }
    
    public PyDataType[] SaveList => this.mSavedList;
    public int[] SaveListIndexes => this.mSavedElementsMap;
    public int SaveListCount => this.mSavedList?.Length ?? 0;
    public List<InsightEntry> Insight { get; } = new List<InsightEntry>();
    public PyDataType Output { protected set; get; }
    
    protected InsightUnmarshal(Stream stream) : base(stream)
    {
    }

    /// <summary>
    /// Processes a python type from the current position of mStream
    /// </summary>
    /// <param name="expectHeader">Whether the unmarshaler should check for a Marshal header or not</param>
    /// <returns></returns>
    /// <exception cref="InvalidDataException">If any error was found during the unmarshal process</exception>
    protected override PyDataType Process(bool expectHeader = true)
    {
        if (expectHeader)
            this.ProcessPacketHeader();

        long start = this.mReader.BaseStream.Position;
        // read the type's opcode from the stream
        byte header = this.mReader.ReadByte();
        Opcode opcode = (Opcode)(header & Specification.OPCODE_MASK);
        bool save = (header & Specification.SAVE_MASK) == Specification.SAVE_MASK;

        PyDataType data = this.ProcessOpcode(opcode);

        // check if the element has to be saved
        if (save == true)
            this.mSavedList[this.mSavedElementsMap[this.mCurrentSavedIndex++] - 1] = data;

        long end = this.mReader.BaseStream.Position;

        InsightEntry entry = new InsightEntry()
        {
            StartPosition = start,
            EndPosition = end - 1,
            Opcode = opcode,
            Value = data,
            HasSaveFlag = save
        };

        this.Insight.Add(entry);

        return data;
    }

    /// <summary>
    /// Custom implementation of string parsing to ensure the byte data is kept as sometimes this is required and useful
    /// </summary>
    /// <param name="opcode"></param>
    /// <returns></returns>
    protected override PyDataType ProcessString(Opcode opcode)
    {
        switch (opcode)
        {
            case Opcode.StringShort:
                return new PyByteString(this.mReader.ReadBytes(this.mReader.ReadByte()));
            case Opcode.StringLong:
                return new PyByteString(this.mReader.ReadBytes((int)this.mReader.ReadSizeEx()));
            default:
                return base.ProcessString(opcode);
        }
    }

    /// <summary>
    /// <seealso cref="Marshal.ProcessSubStream"/>
    /// 
    /// Opcodes supported:
    /// <seealso cref="Opcode.SubStream"/>
    /// </summary>
    /// <returns>The decoded python type</returns>
    /// <exception cref="InvalidDataException">If any error was found in the data</exception>
    protected override PyDataType ProcessSubStream()
    {
        uint length = this.mReader.ReadSizeEx();
        byte[] buffer = new byte[length];

        this.mReader.Read(buffer, 0, buffer.Length);

        PyInsightSubStream substream = new PyInsightSubStream(buffer);

        // run the child substream and add the insights to this one too
        foreach (InsightEntry entry in substream.UnmarshalStream.Insight)
            this.Insight.Add(entry);

        return substream;
    }
}