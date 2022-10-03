using LSLib.Granny.Model;
using OpenTK.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EVEmuTool.Forms.Components
{
    public partial class ModelViewerComponent : UserControl
    {
        private GLControl mRender;

        public ModelViewerComponent(Root model)
        {
            InitializeComponent();

            this.mRender = new GR2RenderComponent(model);
            this.mRender.Dock = DockStyle.Fill;
            this.Controls.Add(this.mRender);

            this.surfaceCountLabel.Text = model.Meshes.Sum(x => x.PrimaryTopology.Groups.Count).ToString();
            this.surfaceTypeLabel.Text = "GR2";
            this.vertexCountLabel.Text = model.VertexDatas.Sum(x => x.Vertices.Count).ToString();
            this.vertexSizeLabel.Text = "GR2";
            // TODO: CALCULATE BOUNDING BOX SIZE
        }

        public ModelViewerComponent(Trinity.TriModel model)
        {
            InitializeComponent();

            this.mRender = new TriRenderComponent(model);
            this.mRender.Dock = DockStyle.Fill;
            this.Controls.Add(this.mRender);

            this.surfaceCountLabel.Text = model.Surfaces.Length.ToString();
            this.surfaceTypeLabel.Text = model.Surfaces[0].type.ToString();
            this.vertexCountLabel.Text = model.Vertices.Length.ToString();
            this.vertexSizeLabel.Text = model.VertexSize.ToString();
            this.boundingBoxMinLabel.Text = model.BoundingBox.min.ToString();
            this.boundingBoxMaxLabel.Text = model.BoundingBox.max.ToString();
        }
    }
}
