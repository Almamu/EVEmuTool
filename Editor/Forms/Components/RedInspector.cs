using EVEmuTool.Trinity.Objects;
using EVEmuTool.Trinity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EVEmuTool.EmbedFS;

namespace EVEmuTool.Forms.Components
{
    public partial class RedInspector : UserControl
    {
        public RedInspector(MemoryStream stream, IEmbedFS source)
        {
            InitializeComponent();

            try
            {
                RedObject result = Red.Parse(Encoding.ASCII.GetString(stream.ToArray()), source);

                if (result is EveEntity)
                {
                    this.viewerTab.Controls.Add (
                        new ModelViewerComponent(result as EveEntity)
                        {
                            Dock = DockStyle.Fill
                        }
                    );
                }
            }
            catch (Exception ex)
            {
                this.viewerTab.Controls.Add(
                    new Label()
                    {
                        Text = ex.Message,
                        Dock = DockStyle.Fill
                    }
                );
            }

            stream.Seek(0, SeekOrigin.Begin);

            this.contentRichText.Text = Encoding.ASCII.GetString(stream.ToArray());
            this.contentRichText.ReadOnly = true;

            stream.Close();
        }
    }
}
