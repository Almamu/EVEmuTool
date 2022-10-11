using EVESharp.Types;
using EVESharp.Types.Serialization;
using System.Windows.Forms;

namespace EVEmuTool.CustomMarshal;

public class InsightEntry
{
    public long StartPosition { get; init; }
    public long EndPosition { get; init; }
    public PyDataType Value { get; init; }
    public Opcode Opcode { get; init; }
    public bool HasSaveFlag { get; init; }
    public TreeNode TreeNode { get; set; }
}