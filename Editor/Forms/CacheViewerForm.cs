using EVEmuTool.CustomMarshal.CustomTypes;
using EVEmuTool.CustomMarshal.CustomTypes.Complex;
using EVESharp.EVE.Packets.Complex;
using EVESharp.Types;
using EVESharp.Types.Collections;
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

namespace EVEmuTool.Forms
{
    public partial class CacheViewerForm : Form
    {
        private void SetupContainerViewer(Components.MarshalDataViewerComponent component)
        {
            // first create the component as it needs information from us
            this.cacheContainerViewer = component;
            this.cacheContainerViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cacheContainerViewer.Location = new System.Drawing.Point(3, 3);
            this.cacheContainerViewer.Name = "cacheContainerViewer";
            this.cacheContainerViewer.Size = new System.Drawing.Size(636, 434);
            this.cacheContainerViewer.TabIndex = 0;
            this.cacheContainerViewer.OnLoadCompleted += OnContainerLoaded;
            this.cacheContainerViewer.OnUnmarshalError += OnContainerError;
            this.cacheContainerViewer.PrepareDataAsync();
        }
        private void SetupContentViewer(Components.MarshalDataViewerComponent component)
        {
            // first create the component as it needs information from us
            this.cacheContentViewer = component;
            this.cacheContentViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cacheContentViewer.Location = new System.Drawing.Point(3, 3);
            this.cacheContentViewer.Name = "cacheContentViewer";
            this.cacheContentViewer.Size = new System.Drawing.Size(636, 434);
            this.cacheContentViewer.TabIndex = 0;
            this.cacheContentViewer.OnLoadCompleted += OnContentLoaded;
            this.cacheContentViewer.OnUnmarshalError += OnContentError;
            this.cacheContentViewer.PrepareDataAsync();
            // add the content viewer to the proper parent
            this.tabPage2.Controls.Add(this.cacheContentViewer);
        }

        public CacheViewerForm(Stream origin, string title)
        {
            this.SetupContainerViewer(new Components.MarshalDataViewerComponent(origin));

            InitializeComponent();

            this.Enabled = false;
            this.Text += $" - {title}";
            this.SuspendLayout();
        }

        private void OnContentLoaded(object sender, EventArgs e)
        {
            // everything is loaded, resume the layout and enable the form
            this.Enabled = true;
            this.ResumeLayout();
        }

        private void OnContentError(object sender, string error)
        {
            // TODO: REFLECT THE ERROR
        }

        private void ProcessError(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Enabled = true;
            this.ResumeLayout();
        }

        private void OnContainerLoaded(object sender, EventArgs e)
        {
            try
            {
                // extract the right data from here and start the initialization of the second
                if (this.cacheContainerViewer.Unmarshaler.Output is not PyTuple pyTuple)
                    throw new Exception("Cache container is not the right type, expected tuple");

                if (pyTuple.Count != 2)
                    throw new Exception("Container tuple doesn't have two elements");

                PyDataType name = pyTuple[0];
                PyDataType data = pyTuple[1];

                if (name is not PyString nameString)
                    throw new Exception("Cache container name is not string");
                if (data is not PyObjectData dataObject)
                    throw new Exception("Cache data is not an ObjectData");

                CustomCachedObject cachedObject = dataObject;

                // cachedObject parsed, now it's time for the unmarshaler to do it's thing
                SetupContentViewer(new Components.MarshalDataViewerComponent(cachedObject.Cache.Value));
            }
            catch (Exception ex)
            {
                this.ProcessError(ex.Message);
            }
        }

        private void OnContainerError(object sender, string error)
        {
            // TODO: REFLECT THE ERROR
        }
    }
}
