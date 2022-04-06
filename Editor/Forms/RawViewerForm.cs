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

namespace Editor.Forms
{
    public partial class RawViewerForm : Form
    {
        private string mExtraMessage = "";

        private void SetupDataViewer(Components.MarshalDataViewerComponent component)
        {
            // first create the component as it needs information from us
            this.marshalDataViewerComponent1 = component;
            this.marshalDataViewerComponent1.OnLoadCompleted += OnLoadCompleted;
            this.marshalDataViewerComponent1.OnUnmarshalError += OnUnmarshalError;
            this.marshalDataViewerComponent1.PrepareDataAsync();
        }
        public RawViewerForm(Stream stream)
        {
            this.SetupDataViewer(new Components.MarshalDataViewerComponent(stream));
            InitializeComponent();
        }

        public RawViewerForm(byte[] data)
        {
            this.SetupDataViewer(new Components.MarshalDataViewerComponent(data));
            InitializeComponent();
        }

        private void OnLoadCompleted(object? sender, EventArgs e)
        {
            // things finished loading, update the corresponding text
            this.toolStripStatusLabel1.Text = "Unmarshalled data ready! " + this.mExtraMessage;
        }

        private void OnUnmarshalError(object? sender, string e)
        {
            this.mExtraMessage = e;
        }
    }
}
