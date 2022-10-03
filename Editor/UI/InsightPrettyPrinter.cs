using EVEmuTool.CustomMarshal;
using EVESharp.PythonTypes.Marshal;
using EVESharp.PythonTypes.Types.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EVEmuTool.UI
{
    internal class InsightPrettyPrinter
    {
        public static void Process(byte[] data, InsightUnmarshal unmarshal, out TreeNode node)
        {
            node = new TreeNode();
            node.Nodes.Add("Data length: " + data.Length.ToString());
            node.Nodes.Add("Save list entries: " + unmarshal.SaveListCount);

            TreeNode insightListParent = node.Nodes.Add("Marshal Data");

            // add elements to the insight view in the order they were unmarshaled
            foreach (InsightEntry entry in unmarshal.Insight)
            {
                string extra = "";

                if (entry.HasSaveFlag == true)
                    extra += " (Saved)";

                string hexValue = data[entry.StartPosition].ToString("X2");
                string line = $"[{hexValue}] {entry.Opcode.ToString()}{extra}: {entry.StartPosition.ToString()} to {entry.EndPosition.ToString()}";

                entry.TreeNode = insightListParent.Nodes.Add(line);

                TreeViewPrettyPrinter.Process(entry.Value, out TreeNode child);

                entry.TreeNode.Nodes.Add(child);
            }

            TreeNode savedObjectsParent = node.Nodes.Add("Saved list");

            if (unmarshal.SaveList is not null)
            {
                int i = 0;

                // add saved list elements to the insight viewer
                foreach (PyDataType entry in unmarshal.SaveList)
                {
                    TreeNode savedObject = savedObjectsParent.Nodes.Add(i.ToString());
                    i++;

                    TreeViewPrettyPrinter.Process(entry, savedObject);
                }
            }

            TreeNode savedIndexesParent = node.Nodes.Add("Saved list indexes");

            if (unmarshal.SaveListIndexes is not null)
            {
                int i = 0;

                foreach (int entry in unmarshal.SaveListIndexes)
                {
                    savedIndexesParent.Nodes.Add(i.ToString() + " => " + entry.ToString());
                    i++;
                }
            }
        }
    }
}
