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
using EVEmuTool.EmbedFS;
using EVEmuTool.Trinity;
using EVEmuTool.Forms.Components;
using EVESharp.PythonTypes.Compression;
using Org.BouncyCastle.Utilities.Zlib;
using LSLib.Granny;
using LSLib.Granny.Model;
using EVEmuTool.UI;
using System.Threading;
using EVEmuTool.Trinity.Objects;

namespace EVEmuTool.Forms
{

    public partial class StuffExplorer : Form
    {
        private BackgroundWorker mWorker;
        private BackgroundWorker mContentLoader;
        private IEmbedFS mSource;
        private List<TreeNode> mNodes = new List<TreeNode> ();
        private TreeNode mTree = new TreeNode("/");
        private Control mPreviewComponent;
        private StuffEntry mSelectedEntry;

        public StuffExplorer(Stream input, string title)
        {
            this.mSource = new EmbedFSFile(input);

            InitializeComponent();

            this.Text += $" - {title}";
            this.LoadStuff();
        }

        public StuffExplorer(string basepath, string[] files)
        {
            this.mSource = new EmbedFSDirectory(files);
            InitializeComponent();

            this.Text = $"EVE Online Client Tool - {basepath}";
            this.LoadStuff();
        }

        private void LoadStuff()
        {
            // start the background loader for the stuff
            this.mWorker = new BackgroundWorker();
            this.mWorker.WorkerSupportsCancellation = false;
            this.mWorker.WorkerReportsProgress = false;
            this.mWorker.DoWork += DoWork;
            this.mWorker.RunWorkerCompleted += OnLoadCompleted;
            this.mWorker.RunWorkerAsync();
            // setup the content loader worker
            this.mContentLoader = new BackgroundWorker();
            this.mContentLoader.WorkerSupportsCancellation = true;
            this.mContentLoader.WorkerReportsProgress = false;
            this.mContentLoader.DoWork += LoadPreviewWork;
            this.mContentLoader.RunWorkerCompleted += OnContentLoadCompleted;
            this.mContentLoader.RunWorkerAsync();
        }

        private void DecideTreeIcon (TreeNode node, string part)
        {
            // set to lowercase so we can properly compare them
            part = part.ToLower();

            // depending on the extension, select a different icon
            if (
                part.EndsWith(".tri") == true ||
                part.EndsWith(".gr2") == true)
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
                part.EndsWith (".sm_1_1") == true ||
                part.EndsWith (".fxp") == true ||
                part.EndsWith (".fx") == true ||
                part.EndsWith (".fxh") == true)
            {
                node.SelectedImageIndex = node.ImageIndex = 10;
            }
            else if (part.EndsWith (".pink") == true)
            {
                node.SelectedImageIndex = node.ImageIndex = 11;
            }
            else if(part.EndsWith (".yaml") == true)
            {
                node.SelectedImageIndex = node.ImageIndex = 12;
            }
            else if(
                part.EndsWith (".color") == true ||
                part.EndsWith(".type") == true ||
                part.EndsWith(".prs") == true ||
                part.EndsWith(".face") == true ||
                part.EndsWith(".info") == true ||
                part.EndsWith(".trif") == true ||
                part.EndsWith(".proj") == true ||
                part.EndsWith(".base") == true)
            {
                node.SelectedImageIndex = node.ImageIndex = 13;
            }
            else
            {
                node.ImageIndex = node.SelectedImageIndex = 3;
            }
        }

        private void DoWork(object sender, EventArgs e)
        {
            // load the file information
            this.mSource.InitializeFile();

            // get the file list sorted by filename
            foreach (StuffEntry entry in this.mSource.Files.OrderByDescending(x => x.FileName))
            {
                TreeNode node = new TreeNode(entry.FileName);

                this.DecideTreeIcon(node, entry.FileName);
                this.mNodes.Add(node);
            }

            foreach (StuffEntry entry in this.mSource.Files)
            {
                string[] parts = entry.FileName.Split('/');
                TreeNode current = this.mTree;

                for (int i = 0; i < parts.Length; i++)
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

            filename = filename.ToLower();
            entry = this.mSource.Files.FirstOrDefault(y => y.FileName.ToLower() == filename);

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
            string extension = "";
            string filename = entry.FileName.Split("/")[^1];
            if (filename.IndexOf(".") != -1)
                extension = entry.FileName.Split(".")[^1];

            string outputFilename = Path.GetTempFileName() + ((extension != "") ? ("." + extension) : "");
            Stream output = File.OpenWrite(outputFilename);
            entry.Origin.Export(output, entry);
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
                entry.Origin.Export(output, entry);
                output.Flush();
                output.Close();
            }
        }

        private MemoryStream LoadToMemory (StuffEntry entry)
        {
            MemoryStream stream = new MemoryStream(entry.Length);

            entry.Origin.Export(stream, entry);

            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        private void OnContentLoadCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // remove everything in the second panel
            this.splitContainer1.Panel2.Controls.Clear();

            if (this.mSelectedEntry is not null)
            {
                this.exportToolStripMenuItem.Enabled = true;
                this.viewToolStripMenuItem.Enabled = true;
            }

            if (this.mPreviewComponent is null)
                return;

            // add the preview component back
            this.splitContainer1.Panel2.Controls.Add(this.mPreviewComponent);
        }

        private void LoadPreview()
        {
            this.exportToolStripMenuItem.Enabled = false;
            this.viewToolStripMenuItem.Enabled = false;

            // update the selected stuff entry
            this.mSelectedEntry = this.FindSelectedEntry ();

            // clear the panel and add a "Loading..." text
            this.splitContainer1.Panel2.Controls.Clear();
            this.splitContainer1.Panel2.Controls.Add(new Label() { Text = "Loading...", Dock = DockStyle.Fill });

            // remove the old component (if any)
            if (this.mPreviewComponent is not null)
            {
                this.mPreviewComponent.Dispose();
                this.mPreviewComponent = null;
            }

            if (this.mContentLoader.IsBusy)
                this.mContentLoader.CancelAsync();

            this.mContentLoader.RunWorkerAsync();
        }

        private void LoadPreviewWork(object sender, EventArgs e)
        {
            if (this.mSelectedEntry is null)
                return;

            /*try
            {*/
                string filename = this.mSelectedEntry.FileName.ToLower();

                // decide what to do based on extension
                if (
                    filename.EndsWith(".png") == true ||
                    filename.EndsWith(".bmp") == true ||
                    filename.EndsWith(".jpg") == true ||
                    filename.EndsWith(".jpeg") == true)
                {
                    MemoryStream stream = this.LoadToMemory(this.mSelectedEntry);
                    PictureBox pictureBox = new PictureBox
                    {
                        Image = Image.FromStream(stream),
                        SizeMode = PictureBoxSizeMode.CenterImage
                    };

                    stream.Close();

                    this.mPreviewComponent = pictureBox;
                }
                else if (filename.EndsWith(".dds") == true)
                {
                    MemoryStream stream = this.LoadToMemory(this.mSelectedEntry);
                    PictureBox pictureBox = new PictureBox
                    {
                        Image = DDSReader.DDSImage.ConvertDDSToPng(stream.ToArray()),
                        SizeMode = PictureBoxSizeMode.CenterImage
                    };

                    stream.Close();

                    this.mPreviewComponent = pictureBox;
                }
                else if (
                    filename.EndsWith(".txt") == true ||
                    filename.EndsWith(".css") == true ||
                    filename.EndsWith(".pink") == true ||
                    filename.EndsWith(".yaml") == true ||
                    filename.EndsWith(".color") == true ||
                    filename.EndsWith(".type") == true ||
                    filename.EndsWith(".prs") == true ||
                    filename.EndsWith(".face") == true ||
                    filename.EndsWith(".info") == true ||
                    filename.EndsWith(".trif") == true ||
                    filename.EndsWith(".proj") == true ||
                    filename.EndsWith(".base") == true ||
                    filename.EndsWith(".fx") == true ||
                    filename.EndsWith(".fxh") == true ||
                    filename.EndsWith(".fxp") == true)
                {
                    MemoryStream stream = this.LoadToMemory(this.mSelectedEntry);
                    RichTextBox textBox = new RichTextBox
                    {
                        Text = Encoding.ASCII.GetString(stream.ToArray()),
                        ReadOnly = true
                    };

                    stream.Close();

                    this.mPreviewComponent = textBox;
                }
                else if (filename.EndsWith (".red") == true)
                {
                    MemoryStream stream = this.LoadToMemory(this.mSelectedEntry);

                    this.mPreviewComponent = new RedInspector(stream, this.mSource);
                }
                else if(filename.EndsWith(".tri") == true)
                {
                    MemoryStream stream = this.LoadToMemory(this.mSelectedEntry);
                    TriModel model = new TriModel(stream);

                    ModelViewerComponent viewer = new ModelViewerComponent(model);

                    stream.Close();

                    this.mPreviewComponent = viewer;
                }
                else if(filename.EndsWith(".ogg") == true)
                {
                    MemoryStream stream = this.LoadToMemory(this.mSelectedEntry);
                    this.mPreviewComponent = new AudioPlayer(stream, "ogg");
                }
                else if (filename.EndsWith(".mp3") == true)
                {
                    MemoryStream stream = this.LoadToMemory(this.mSelectedEntry);
                    this.mPreviewComponent = new AudioPlayer(stream, "mp3");
                }
                else if (filename.EndsWith(".wav") == true)
                {
                    MemoryStream stream = this.LoadToMemory(this.mSelectedEntry);
                    this.mPreviewComponent = new AudioPlayer(stream, "wav");
                }
                else if (filename.EndsWith(".blue") == true)
                {
                    MemoryStream stream = this.LoadToMemory(this.mSelectedEntry);
                    Thread t = new Thread(() =>
                    {
                        WpfHexaEditor.HexEditor editor = new WpfHexaEditor.HexEditor()
                        {
                            Stream = stream,
                        };

                        this.mPreviewComponent = new System.Windows.Forms.Integration.ElementHost()
                        {
                            Child = editor
                        };
                    });

                    t.SetApartmentState(ApartmentState.STA);
                    t.Start();
                    while (t.ThreadState != ThreadState.Stopped) Thread.Sleep(100);
                }
                else if (filename.EndsWith(".gr2") == true)
                {
                    MemoryStream stream = this.LoadToMemory(this.mSelectedEntry);
                    Root result = GR2Utils.LoadModel(stream);

                    ModelViewerComponent viewer = new ModelViewerComponent(result);

                    stream.Close();

                    this.mPreviewComponent = viewer;
                }
                else if (filename.EndsWith(".pickle") == true)
                {
                    MemoryStream stream = this.LoadToMemory(this.mSelectedEntry);
                    TreeView view = new TreeView();
                    Razorvine.Pickle.Unpickler unpickler = new Razorvine.Pickle.Unpickler();
                    TreeViewPrettyPrinter.Process(NativeToPython.Convert(unpickler.load(stream)), view.Nodes.Add(""));
                    this.mPreviewComponent = view;
                }
                else
                {
                    this.mPreviewComponent = new Label()
                    {
                        Text = "Preview not available"
                    };
                }
            /*}
            catch (Exception ex)
            {
                this.mPreviewComponent = new Label()
                {
                    Text = $"Cannot generate preview: {ex.Message}"
                };
            }*/

            // setup some of the component's information
            if (this.mPreviewComponent is not null)
                this.mPreviewComponent.Dock = DockStyle.Fill;
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
