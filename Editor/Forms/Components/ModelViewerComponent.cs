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
    public partial class ModelViewerComponent : UserControl
    {
        private TriRenderComponent mRender;

        public ModelViewerComponent(Trinity.Model model)
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
