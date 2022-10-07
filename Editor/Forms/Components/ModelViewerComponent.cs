using EVEmuTool.Trinity.Objects;
using LSLib.Granny;
using LSLib.Granny.Model;
using OpenTK.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EVEmuTool.Forms.Components
{
    public partial class ModelViewerComponent : UserControl
    {
        private Control mRender;

        public ModelViewerComponent(Root model)
        {
            InitializeComponent();
            LoadGR2Model(model);
        }

        public ModelViewerComponent(Trinity.TriModel model)
        {
            InitializeComponent();
            LoadTrinityModel(model);
        }

        private void LoadTrinityModel(Trinity.TriModel model)
        {
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

        private void LoadGR2Model(Root model)
        {
            this.mRender = new GR2RenderComponent(model);
            this.mRender.Dock = DockStyle.Fill;
            this.Controls.Add(this.mRender);

            this.surfaceCountLabel.Text = model.Meshes.Sum(x => x.PrimaryTopology.Groups.Count).ToString();
            this.surfaceTypeLabel.Text = "GR2";
            this.vertexCountLabel.Text = model.VertexDatas.Sum(x => x.Vertices.Count).ToString();
            this.vertexSizeLabel.Text = "GR2";
            // TODO: CALCULATE BOUNDING BOX SIZE
        }

        public ModelViewerComponent(EveEntity entity)
        {
            InitializeComponent();

            // determine what mesh we're using
            string extension = entity.HighDetailMesh.GeometryResPath.Split(".")[^1];
            MemoryStream stream = entity.Source.ResolveFile(entity.HighDetailMesh.GeometryResPath);

            try
            {
                if (extension == "tri")
                {
                    LoadTrinityModel(new Trinity.TriModel(stream));
                }
                else if (extension == "gr2")
                {
                    LoadGR2Model(GR2Utils.LoadModel(stream));
                }
                else
                {
                    MessageBox.Show("Cannot determine high poly model", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                this.Controls.Add(new Label() { Text = ex.ToString(), Dock = DockStyle.Fill });
            }
            finally
            {
                stream.Close();
            }
        }
    }
}
