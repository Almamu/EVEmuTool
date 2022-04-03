using System.Collections.Generic;
using System.IO;
using EVESharp.PythonTypes.Marshal;
using EVESharp.PythonTypes.Types.Primitives;

namespace Editor.CustomMarshal;

public class InsightUnmarshal : Unmarshal
{
    /// <summary>
    /// Extracts the PyDataType off the given byte array
    /// </summary>
    /// <param name="data">Byte array to extract the PyDataType from</param>
    /// <param name="expectHeader">Whether a marshal header is expected or not</param>
    /// <returns>The unmarshaled PyDataType</returns>
    public static InsightUnmarshal ReadFromByteArray(byte[] data, bool expectHeader = true)
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
    public static InsightUnmarshal ReadFromStream(Stream stream, bool expectHeader = true)
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
}