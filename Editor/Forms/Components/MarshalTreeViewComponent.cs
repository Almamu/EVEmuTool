using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor.Forms.Components
{
    public partial class MarshalTreeViewComponent : UserControl
    {
        public MarshalTreeViewComponent()
        {
            InitializeComponent();
        }

        public void SetPacketData(TreeNode node)
        {
            this.packetTreeView.BeginUpdate();
            this.packetTreeView.Nodes.Clear();

            this.packetTreeView.Nodes.Add(node);

            this.packetTreeView.ExpandAll();
            this.packetTreeView.EndUpdate();
        }
    }
}
