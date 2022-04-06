using EVESharp.PythonTypes.Marshal;
using EVESharp.PythonTypes.Types.Primitives;
using System.Windows.Forms;

namespace Editor.CustomMarshal;

public class InsightEntry
{
    public long StartPosition { get; init; }
    public long EndPosition { get; init; }
    public PyDataType Value { get; init; }
    public Opcode Opcode { get; init; }
    public bool HasSaveFlag { get; init; }
    public TreeNode TreeNode { get; set; }
}