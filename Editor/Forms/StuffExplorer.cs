using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Editor.EmbedFS;
using Editor.Trinity;
using Editor.Forms.Components;

namespace Editor.Forms
{

    public partial class StuffExplorer : Form
    {
        private BackgroundWorker mWorker;
        private EmbedFSFile mFile;
        private List<TreeNode> mNodes = new List<TreeNode> ();
        private TreeNode mTree = new TreeNode("/");
        private Control mPreviewComponent;

        public StuffExplorer(Stream input, string title)
        {
            this.mFile = new EmbedFSFile(input);

            InitializeComponent();

            this.Text += $" - {title}";

            // start the background loader for the stuff
            this.mWorker = new BackgroundWorker();
            this.mWorker.WorkerSupportsCancellation = false;
            this.mWorker.WorkerReportsProgress = false;
            this.mWorker.DoWork += DoWork;
            this.mWorker.RunWorkerCompleted += OnLoadCompleted;
            this.mWorker.RunWorkerAsync();
        }

        private void DecideTreeIcon (TreeNode node, string part)
        {
            // set to lowercase so we can properly compare them
            part = part.ToLower();

            // depending on the extension, select a different icon
            if (part.EndsWith(".tri") == true)
            {
                node.SelectedImageIndex = node.ImageIndex = 2;
            }
            else if (
                part.EndsWith(".dds") == true ||
                part.EndsWith (".png") == true ||
                part.EndsWith (".tga") == true || 
                part.EndsWith (".jpg") == true ||
                part.EndsWith (".jpeg") == true)
            {
                node.SelectedImageIndex = node.ImageIndex = 1;
            }
            else if (part.EndsWith (".blue") == true)
            {
                node.SelectedImageIndex = node.ImageIndex = 4;
            }
            else if (part.EndsWith (".red") == true)
            {
                node.SelectedImageIndex = node.ImageIndex = 5;
            }
            else if (part.EndsWith (".css") == true)
            {
                node.SelectedImageIndex = node.ImageIndex = 6;
            }
            else if (part.EndsWith (".pickle") == true)
            {
                node.SelectedImageIndex = node.ImageIndex = 7;
            }
            else if (part.EndsWith (".txt") == true)
            {
                node.SelectedImageIndex = node.ImageIndex = 8;
            }
            else if (
                part.EndsWith (".ogg") == true ||
                part.EndsWith (".wav") == true ||
                part.EndsWith (".mp3") == true)
            {
                node.SelectedImageIndex = node.ImageIndex = 9;
            }
            else if (
                part.EndsWith (".sm_3_0_lo") == true ||
                part.EndsWith (".sm_3_0_hi") == true ||
                part.EndsWith (".sm_2_0_lo") == true ||
                part.EndsWith (".sm_2_0_hi") == true ||
                part.EndsWith (".sm_1_1") == true)
            {
                node.SelectedImageIndex = node.ImageIndex = 10;
            }
            else if (part.EndsWith (".pink") == true)
            {
                node.SelectedImageIndex = node.ImageIndex = 11;
            }
            else
            {
                node.ImageIndex = node.SelectedImageIndex = 3;
            }
        }

        private void DoWork(object sender, EventArgs e)
        {
            // load the file information
            this.mFile.ReadFile();

            // get the file list sorted by filename
            foreach (StuffEntry file in this.mFile.Files.OrderByDescending(x => x.FileName))
            {
                TreeNode node = new TreeNode(file.FileName);

                this.DecideTreeIcon (node, file.FileName);
                this.mNodes.Add(node);
            }

            foreach (StuffEntry file in this.mFile.Files)
            {
                string[] parts = file.FileName.Split('/');
                TreeNode current = this.mTree;

                for (int i = 0; i < parts.Length; i ++)
                {
                    string part = parts[i];
                    TreeNode[] list = current.Nodes.Find(part, false);

                    if (list.Length == 0)
                        current = current.Nodes.Add(part, part);
                    else
                        current = list[0];

                    if (i < parts.Length - 1)
                    {
                        current.ImageIndex = 0;
                    }
                    else
                    {
                        this.DecideTreeIcon(current, part);
                    }
                }
            }
        }

        private void OnLoadCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.fileListView.Nodes.AddRange(this.mNodes.ToArray());
            this.fileTreeView.Nodes.Add(this.mTree);

            // expand first and second level of fileTreeView
            foreach (TreeNode node in this.fileTreeView.Nodes)
            {
                node.Expand();

                foreach (TreeNode child in node.Nodes)
                {
                    child.Expand();
                }
            }
        }

        private StuffEntry FindSelectedEntry()
        {
            StuffEntry entry;
            string filename;

            // first get the file name
            if (this.stuffExplorerModeTabs.SelectedIndex == 0)
            {
                filename = this.fileListView.SelectedNode.Text;
            }
            else
            {
                // walk upwards to get the filename
                TreeNode current = this.fileTreeView.SelectedNode;

                // ensure no folder can be selected
                if (current.Nodes.Count > 0)
                {
                    return null;
                }

                filename = current.Text;

                while (current.Parent is not null && current.Parent.Text != "/")
                {
                    current = current.Parent;
                    filename = current.Text + "/" + filename;
                }
            }

            entry = this.mFile.Files.Find(x => x.FileName == filename);

            if (entry == null)
            {
                MessageBox.Show ("Cannot determine file information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return entry;
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StuffEntry entry = this.FindSelectedEntry();

            if (entry is null)
                return;

            // create a temporal file
            string outputFilename = System.IO.Path.GetTempFileName() + "." + entry.FileName.Split(".")[^1];
            Stream output = File.OpenWrite(outputFilename);
            this.mFile.Export(output, entry);
            output.Flush();
            output.Close();

            // have the OS handle openning the file
            System.Diagnostics.Process.Start("explorer", outputFilename);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StuffEntry entry = this.FindSelectedEntry ();

            if (entry is null)
                return;

            // show the save dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                FileName = entry.FileName.Split("/")[^1]
            };

            if (saveFileDialog.ShowDialog () == DialogResult.OK)
            {
                // get the file contents
                Stream output = saveFileDialog.OpenFile();
                this.mFile.Export(output, entry);
                output.Flush();
                output.Close();
            }
        }

        private MemoryStream LoadToMemory (StuffEntry entry)
        {
            MemoryStream stream = new MemoryStream(entry.Length);

            this.mFile.Export(stream, entry);

            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        private void LoadPreview()
        {
            // remove the old component (if any)
            if (this.mPreviewComponent is not null)
                this.splitContainer1.Panel2.Controls.Remove(this.mPreviewComponent);

            StuffEntry entry = this.FindSelectedEntry();

            this.exportToolStripMenuItem.Enabled = false;
            this.viewToolStripMenuItem.Enabled = false;

            if (entry is not null)
            {
                this.exportToolStripMenuItem.Enabled = true;
                this.viewToolStripMenuItem.Enabled = true;
            }

            if (entry is null)
                return;

            this.helpText.Visible = false;

            try
            {
                // decide what to do based on extension
                if (
                    entry.FileName.EndsWith(".png") == true ||
                    entry.FileName.EndsWith(".bmp") == true ||
                    entry.FileName.EndsWith(".jpg") == true ||
                    entry.FileName.EndsWith(".jpeg") == true)
                {
                    MemoryStream stream = this.LoadToMemory(entry);
                    PictureBox pictureBox = new PictureBox
                    {
                        Image = Image.FromStream(stream),
                        SizeMode = PictureBoxSizeMode.CenterImage
                    };

                    stream.Close();

                    this.mPreviewComponent = pictureBox;
                }
                else if (entry.FileName.EndsWith(".dds") == true)
                {
                    MemoryStream stream = this.LoadToMemory(entry);
                    PictureBox pictureBox = new PictureBox
                    {
                        Image = DDSReader.DDSImage.ConvertDDSToPng(stream.ToArray()),
                        SizeMode = PictureBoxSizeMode.CenterImage
                    };

                    stream.Close();

                    this.mPreviewComponent = pictureBox;
                }
                else if (
                    entry.FileName.EndsWith(".txt") == true ||
                    entry.FileName.EndsWith(".css") == true ||
                    entry.FileName.EndsWith(".pink") == true ||
                    entry.FileName.EndsWith(".red") == true)
                {
                    MemoryStream stream = this.LoadToMemory(entry);
                    RichTextBox textBox = new RichTextBox
                    {
                        Text = Encoding.ASCII.GetString(stream.ToArray()),
                        ReadOnly = true
                    };

                    stream.Close();

                    this.mPreviewComponent = textBox;
                }
                else if(entry.FileName.EndsWith(".tri") == true)
                {
                    MemoryStream stream = this.LoadToMemory(entry);
                    Model model = new Model(stream);

                    ModelViewerComponent viewer = new ModelViewerComponent(model);

                    stream.Close();

                    this.mPreviewComponent = viewer;
                }
                else
                {
                    this.helpText.Text = "Preview not available";
                    this.helpText.Visible = true;
                }
            }
            catch (Exception ex)
            {
                this.helpText.Text = $"Cannot generate preview: {ex.Message}";
                this.helpText.Visible = true;
            }

            // do not setup the component if it's not in use
            if (this.mPreviewComponent is null)
                return;

            // setup some of the component's information
            this.mPreviewComponent.Dock = DockStyle.Fill;

            // add the component to the panel
            this.splitContainer1.Panel2.Controls.Add(this.mPreviewComponent);
        }

        private void fileTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.LoadPreview();
        }

        private void fileListView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.LoadPreview();
        }
    }
}
