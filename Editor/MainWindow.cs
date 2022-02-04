using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Network;
using Editor.CustomMarshal;
using PythonTypes;
using PythonTypes.Marshal;
using PythonTypes.Types.Collections;
using PythonTypes.Types.Network;
using PythonTypes.Types.Primitives;
using System.ComponentModel.Design;
using EVE.Packets.Complex;
using Editor.LogServer;

namespace Editor
{
    public partial class MainWindow : Form
    {
        private delegate void ClientUpdateDelegate(Client newLiveClient);
        private delegate void ClientPacketReceivedDelegate(PacketEntry entry);
        private ClientUpdateDelegate mClientAdd;
        private ClientPacketReceivedDelegate mPacketReceived;

        private EVEBridgeServer mServer;
        private List<Client> mClients = new List<Client>();
        private BindingSource mClientBinding = new BindingSource();
        private List<PacketEntry> mPackets = new List<PacketEntry>();
        private BindingSource mPacketListBinding = new BindingSource();
        private BindingSource mWorkspaceListBinding = new BindingSource();

        private string mServerAddress = "127.0.0.1";
        private int mServerPort = 25999;
        private bool mIgnoreIncomingPackets = false;
        
        public MainWindow()
        {
            this.mClientAdd = AddClient;
            this.mPacketReceived = FilterPacket;

            // setup hex views as the editor removes the setup for some absurd reason
            this.hexView = new WpfHexaEditor.HexEditor();
            this.fileHexView = new WpfHexaEditor.HexEditor();
            this.cacheHexView = new WpfHexaEditor.HexEditor();

            InitializeComponent();
            ExtendComponents();
            
            StartListening();

            listenStatusLabel.Text = "Listen started";
            clientCountLabel.Text = "0 clients connected";
        }

        private void StartListening()
        {
            this.mServer = new EVEBridgeServer();
            this.mServer.Listen();
            this.mServer.BeginAccept(ServerConnectionAccept);
        }

        private void ExtendComponents()
        {
            this.mClientBinding.DataSource = this.mClients;
            this.clientListGridView.AutoGenerateColumns = false;
            this.clientListGridView.DataSource = this.mClientBinding;
            this.packetGridView.AutoGenerateColumns = false;
            this.packetGridView.DataSource = this.mPacketListBinding;
            this.workspaceGridView.AutoGenerateColumns = false;
            this.workspaceGridView.DataSource = this.mWorkspaceListBinding;
            // disable multiselect
            this.packetGridView.MultiSelect = false;
            // set hex views as childs of whatever is needed
            this.hexViewHost.Child = this.hexView;
            this.cacheHexViewHost.Child = this.cacheHexView;
            this.fileHexViewHost.Child = this.fileHexView;
            // setup the options for log viewer
            parseAsMarshalDataOption.Click += parseAsMarshalDataOption_Click; 
        }

        public void ServerConnectionAccept(IAsyncResult ar)
        {
            CustomEVEClientSocket client = this.mServer.EndAccept(ar);
            
            try
            {
                // open a connection to the server to relay info from this client
                CustomEVEClientSocket serverSocket = new CustomEVEClientSocket(this.mServer.Log);
                serverSocket.Connect(this.mServerAddress, this.mServerPort);

                LiveClient newLiveClient = new LiveClient(this.mClients.Count, client, serverSocket, this);

                this.Invoke(this.mClientAdd, newLiveClient);

                clientCountLabel.Text = $"{this.mClients.Count} clients connected";
            }
            catch (Exception)
            {
                try
                {
                    // connection to the server failed, force connection closing for the client
                    client.ForcefullyDisconnect();
                }
                catch
                {
                    // ignored
                }
            }

            // put the socket in accept state again
            this.mServer.BeginAccept(ServerConnectionAccept);
        }

        private void AddClient(Client newLiveClient)
        {
            if(this.mIgnoreIncomingPackets == true)
                return;
                
            // add the client to the list and data source
            int index = this.mClientBinding.Add(newLiveClient);

            newLiveClient.ClientIndex = index;
        }
        
        private void serverAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            this.mServerAddress = this.serverAddressTextBox.Text;
        }

        private void serverPortTextBox_TextChanged(object sender, EventArgs e)
        {
            this.mServerPort = int.Parse(this.serverPortTextBox.Text);
        }

        public void OnPacketReceived(PacketEntry entry)
        {
            // ignore incoming packets if we're reading a saved capture
            if(this.mIgnoreIncomingPackets == true)
                return;
                
            lock(this.mPackets)
                this.mPackets.Add(entry);
            
            this.Invoke(this.mPacketReceived, entry);
        }

        private void LoadPacketDetails(int index)
        {
            Cursor.Current = Cursors.WaitCursor;
            PacketEntry packet = this.packetGridView.Rows[index].DataBoundItem as PacketEntry;

            this.packetTextBox.Text = packet.PacketString;
            if (this.hexView.Stream != null)
                this.hexView.Stream.Close();
            this.hexView.Stream = new MemoryStream(packet.PacketBytes);
            this.hexView.RefreshView();

            this.packetTreeView.BeginUpdate();
            this.packetTreeView.Nodes.Clear();

            if (packet.Packet != null)
            {
                if (packet.Packet.Type == PyPacket.PacketType.CALL_REQ)
                {
                    PyTuple callInfo = ((packet.Packet.Payload[0] as PyTuple)[1] as PySubStream).Stream as PyTuple;

                    TreeViewPrettyPrinter.Process(callInfo[2], this.packetTreeView.Nodes.Add("Call Arguments"));
                    TreeViewPrettyPrinter.Process(callInfo[3], this.packetTreeView.Nodes.Add("Named Call Arguments"));
                }
                else if (packet.Packet.Type == PyPacket.PacketType.CALL_RSP)
                {
                    PyDataType result = (packet.Packet.Payload[0] as PySubStream).Stream;

                    TreeViewPrettyPrinter.Process(result, this.packetTreeView.Nodes.Add("Result"));
                }
                
                TreeViewPrettyPrinter.Process(packet.Packet.OutOfBounds,
                    packetTreeView.Nodes.Add("Out of bounds data"));
            }
            
            TreeViewPrettyPrinter.Process(packet.RawPacket, this.packetTreeView.Nodes.Add("RawData"));

            foreach (TreeNode node in this.packetTreeView.Nodes)
                node.ExpandAll();

            this.packetTreeView.Nodes[0].EnsureVisible();
            this.packetTreeView.EndUpdate();
            Cursor.Current = Cursors.Default;
        }

        private void LoadFileDetails(byte[] contents, PyDataType packet)
        {
            if (this.fileHexView.Stream != null)
                this.fileHexView.Stream.Close();
            this.fileHexView.Stream = new MemoryStream(contents);
            this.fileHexView.RefreshView();

            this.fileTextBox.Text = PrettyPrinter.FromDataType(packet);
            
            this.fileTreeView.BeginUpdate();
            this.fileTreeView.Nodes.Clear();

            TreeViewPrettyPrinter.Process(packet, this.fileTreeView.Nodes.Add("RawData"));

            foreach (TreeNode node in this.fileTreeView.Nodes)
                node.ExpandAll();

            this.fileTreeView.Nodes[0].EnsureVisible();
            this.fileTreeView.EndUpdate();
        }

        private void LoadCacheDetails(byte[] contents, PyDataType cache)
        {
            if (this.cacheHexView.Stream != null)
                this.cacheHexView.Stream.Close();
            this.cacheHexView.Stream = new MemoryStream(contents);
            this.cacheHexView.RefreshView();

            if (cache is PyTuple == false)
                throw new Exception("Expected PyTuple");

            PyTuple tuple = cache as PyTuple;

            if (tuple.Count != 2)
                throw new Exception("Expected PyTuple with 2 elements");

            PyDataType name = tuple[0];
            PyDataType data = tuple[1];

            if (name is PyString == false)
                throw new Exception("Cache name not found");
            
            if (data is PyObjectData == false)
                throw new Exception("Expected PyObjectData as second element");

            PyString cacheName = name as PyString;
            CachedObject objectData = data as PyObjectData;
            PyDataType cacheContent = Unmarshal.ReadFromByteArray(objectData.Cache.Value);
            
            this.cacheTextBox.Text = $"Object name: {cacheName.Value}" + Environment.NewLine + PrettyPrinter.FromDataType(cacheContent);
            
            this.cacheTreeView.BeginUpdate();
            this.cacheTreeView.Nodes.Clear();
            
            TreeViewPrettyPrinter.Process(cacheContent, this.cacheTreeView.Nodes.Add("RawData"));
            
            foreach (TreeNode node in this.cacheTreeView.Nodes)
                node.ExpandAll();

            this.cacheTreeView.Nodes[0].EnsureVisible();
            this.cacheTreeView.EndUpdate();
        }

        private void LoadWorkspaceDetails(WorkspaceFile workspace)
        {
            // clear the tabs list and make sure it's visible
            this.storagesTabs.TabPages.Clear();
            this.storagesTabs.Visible = true;

            // go through the device list
            foreach (Device device in workspace.Devices)
            {
                // go through the storages
                foreach (Storage storage in device.Storages)
                {
                    // generate the controls
                    LogViewerHelper.CreateTabPage(storage.Name, out TabPage tabPage, out DataGridView gridView);
                    // create a new binding for the grid view and add the data to it
                    BindingSource binding = new BindingSource();

                    // go through every line
                    foreach (LogLine line in storage.Lines)
                        binding.Add(new LogLineEntry(line, device));

                    // add the data source
                    gridView.DataSource = binding;
                    // also add listener for index changed so we can show proper, full-length messages
                    gridView.SelectionChanged += LogGrid_SelectionChanged;

                    this.storagesTabs.TabPages.Add(tabPage);
                }
            }
        }

        private string PrepareLogStringForConcatenation(int index, LogLineEntry line)
        {
            string text = line;

            if (text.Length < 253)
                text += "\n";

            if (index > 0 && text.StartsWith("- ") == true)
                text = text.Substring(2);

            return text;
        }

        private void LogGrid_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView gridView = sender as DataGridView;
            BindingSource dataSource = gridView.DataSource as BindingSource;
            int index = 0;

            // reset the text
            logViewExpanded.Text = "";

            if (gridView.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in gridView.SelectedRows.Cast<DataGridViewRow>().OrderBy(x => x.Index))
                    logViewExpanded.Text += this.PrepareLogStringForConcatenation(index++, dataSource[row.Index] as LogLineEntry);
            }
            else if (gridView.SelectedCells.Count > 0)
            {
                Dictionary<int, bool> pairs = new Dictionary<int, bool>();

                foreach(DataGridViewCell cell in gridView.SelectedCells.Cast<DataGridViewCell>().OrderBy(x => x.RowIndex))
                {
                    if (pairs.ContainsKey(cell.RowIndex) == true)
                        continue;

                    logViewExpanded.Text += this.PrepareLogStringForConcatenation(index++, dataSource[cell.RowIndex] as LogLineEntry);
                    pairs[cell.RowIndex] = true;
                }
            }
        }

        private void ClearLogPreview()
        {
            logViewExpanded.Text = "";
        }

        private void ClearPacketDetails()
        {
            this.packetTextBox.Text = "";
            this.packetTreeView.BeginUpdate();
            this.packetTreeView.Nodes.Clear();
            this.packetTreeView.EndUpdate();
        }
        
        private void packetGridView_SelectionChanged(object sender, EventArgs e)
        {
            // update the rich text box
            if (this.packetGridView.SelectedRows.Count == 0)
            {
                this.ClearPacketDetails();
                return;
            }

            this.LoadPacketDetails(this.packetGridView.SelectedRows[0].Index);
        }

        private void FilterPacket(PacketEntry packet)
        {
            if (packet.Client.IncludePackets == false)
                return;

            // check the type of packet
            if (undeterminedCheckbox.Checked == false && packet.Packet == null)
                return;
            if (packet.Packet != null && callReqCheckbox.Checked == false && packet.Packet.Type == PyPacket.PacketType.CALL_REQ)
                return;
            if (packet.Packet != null && callRspCheckbox.Checked == false && packet.Packet.Type == PyPacket.PacketType.CALL_RSP)
                return;
            if (packet.Packet != null && notificationCheckbox.Checked == false && packet.Packet.Type == PyPacket.PacketType.NOTIFICATION)
                return;
            if (packet.Packet != null && sessionChangeCheckbox.Checked == false && (packet.Packet.Type == PyPacket.PacketType.SESSIONCHANGENOTIFICATION || packet.Packet.Type == PyPacket.PacketType.SESSIONINITIALSTATENOTIFICATION))
                return;
            if (packet.Packet != null && exceptionCheckbox.Checked == false && packet.Packet.Type == PyPacket.PacketType.ERRORRESPONSE)
                return;
            if (packet.Packet != null && pingReqCheckbox.Checked == false && packet.Packet.Type == PyPacket.PacketType.PING_REQ)
                return;
            if (packet.Packet != null && pingRspCheckbox.Checked == false && packet.Packet.Type == PyPacket.PacketType.PING_RSP)
                return;
            
            // check for service calls ONLY if CallRes/CallRsp are selected and the packet is an actual packet
            if (packet.Packet != null)
            {
                if (serviceNameTextbox.Text.Length > 0 && (packet.Service is null || packet.Service.Contains(serviceNameTextbox.Text) == false))
                    return;
                if (callNameTextbox.Text.Length > 0 && (packet.Call is null || packet.Call.Contains(callNameTextbox.Text) == false))
                    return;
            }
            
            this.mPacketListBinding.Add(packet);
        }

        private void FilterFullPacketList()
        {
            lock (this.mPackets)
            {
                // clear the packets first
                this.mPacketListBinding.Clear();
                
                foreach (PacketEntry entry in this.mPackets)
                    this.FilterPacket(entry);
            }
        }

        private void packetGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.packetGridView.SelectedCells.Count == 0)
            {
                this.ClearPacketDetails();
                return;
            }

            this.LoadPacketDetails(this.packetGridView.SelectedCells[0].RowIndex);
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            // filter all the packets if any changes to the filters were performed
            if (tabControl1.SelectedIndex == 1)
                this.FilterFullPacketList();
        }

        private void packetGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            PacketEntry entry = this.mPacketListBinding[e.RowIndex] as PacketEntry;

            if (entry.Packet == null)
            {
                this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Cyan;
            }
            else
            {
                if (entry.Packet.Type == PyPacket.PacketType.CALL_REQ)
                {
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Aquamarine;
                }
                else if (entry.Packet.Type == PyPacket.PacketType.CALL_RSP)
                {
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.IndianRed;
                }
                else if (entry.Packet.Type == PyPacket.PacketType.SESSIONCHANGENOTIFICATION ||
                         entry.Packet.Type == PyPacket.PacketType.SESSIONINITIALSTATENOTIFICATION)
                {
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                }
                else if (entry.Packet.Type == PyPacket.PacketType.ERRORRESPONSE)
                {
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.DarkRed;
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                }
                else if (entry.Packet.Type == PyPacket.PacketType.NOTIFICATION)
                {
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.DodgerBlue;
                }
                else if (entry.Packet.Type == PyPacket.PacketType.PING_REQ)
                {
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MediumSlateBlue;
                }
                else if (entry.Packet.Type == PyPacket.PacketType.PING_RSP)
                {
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.SeaGreen;
                }
            }
        }
        
        private void newCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // disable current clients (if any) so the connection is still relayed and not closed
            foreach (Client client in this.mClients)
                client.IncludePackets = false;
                
            lock (this.mPackets)
            {
                // clear lists to free memory
                this.mPackets.Clear();
                this.mPacketListBinding.Clear();
            }

            // clear clients too
            this.mClients.Clear();
            this.mClientBinding.Clear();
            // clear textboxes and treeviews too
            this.ClearPacketDetails();
            
            this.mIgnoreIncomingPackets = false;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveCaptureDialog.ShowDialog() == DialogResult.OK)
                PacketListFile.SavePackets(saveCaptureDialog.FileName, this.mPackets, this.mClients);                
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // forcefully close the socket
                this.mServer.ForcefullyDisconnect();
            }
            catch (Exception)
            {
                // ignored
            }

            foreach (Client client in this.mClients)
            {
                if (client is LiveClient liveClient)
                {
                    try
                    {
                        liveClient.Stop();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
            
            // close the form
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.openCaptureDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (Client client in this.mClients)
                    {
                        if (client is LiveClient liveClient)
                        {
                            try
                            {
                                liveClient.Stop();
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                        }
                    }
                    
                    this.mIgnoreIncomingPackets = true;
                    
                    PacketListFile.LoadPackets(this.openCaptureDialog.FileName, out this.mPackets, out this.mClients);
                    
                    listenStatusLabel.Text = "Listener stopped on capture load. Start a new capture to listen again";

                    // clear the list of clients in the grid view first
                    this.mClientBinding.Clear();
                    
                    // add all clients to the grid view
                    foreach (Client client in this.mClients)
                        this.mClientBinding.Add(client);
                    
                    // re-create the packet window
                    this.FilterFullPacketList();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            lock (this.mPackets)
            {
                this.mPackets.Clear();
                this.mPacketListBinding.Clear();
            }
        }

        private void openMarshalFileButton_Click(object sender, EventArgs e)
        {
            if (this.openMarshalFileDialog.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                byte[] fileContents = File.ReadAllBytes(this.openMarshalFileDialog.FileName);
                LoadFileDetails(fileContents, Unmarshal.ReadFromByteArray(fileContents));
                Cursor.Current = Cursors.Default;
            }
        }

        private void openCacheFileButton_Click(object sender, EventArgs e)
        {
            if (this.openCacheFileDialog.ShowDialog () == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                byte[] fileContents = File.ReadAllBytes(this.openCacheFileDialog.FileName);
                LoadCacheDetails(fileContents, CustomUnmarshal.ReadFromByteArray(fileContents));
                Cursor.Current = Cursors.Default;
            }
        }

        private void packetGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.packetGridView.SelectedCells.Count == 0)
            {
                this.ClearPacketDetails();
                return;
            }

            this.LoadPacketDetails(this.packetGridView.SelectedCells[0].RowIndex);
        }

        private void loadWorkspaceButton_Click(object sender, EventArgs e)
        {
            if (this.openWorkspaceFileDialog.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                WorkspaceReader reader = new WorkspaceReader(this.openWorkspaceFileDialog.OpenFile());
                this.LoadWorkspaceDetails(WorkspaceFile.BuildFromByteData(reader));
                Cursor.Current = Cursors.Default;
            }
        }

        private void parseAsMarshalDataOption_Click(object sender, EventArgs e)
        {
            // get the selected string and parse it properly
            string selected = this.logViewExpanded.SelectedText;
            List<byte> bytes = new List<byte>();

            for(int i = 0; i < selected.Length; i ++)
            {
                char current = selected[i];

                if (current == '\\')
                {
                    int indicatorIndex = i + 1;

                    if (indicatorIndex > selected.Length)
                    {
                        bytes.Add(Encoding.ASCII.GetBytes(new char[] { current })[0]);
                        break;
                    }

                    // check next value
                    if (selected[indicatorIndex] == 'x')
                    {
                        string value = selected.Substring(indicatorIndex + 1, 2);

                        // okay, time to handle an hex number
                        byte number = byte.Parse(value, System.Globalization.NumberStyles.HexNumber);
                        bytes.Add(number);
                        i += 3;
                    }
                    else if (selected[indicatorIndex] == '\\')
                    {
                        bytes.Add(Encoding.ASCII.GetBytes("\\")[0]);
                        i++;
                    }
                }
                else
                {
                    bytes.Add(Encoding.ASCII.GetBytes(new char[] { current })[0]);
                }
            }

            byte[] marshal = bytes.ToArray();

            // use the marshal section for this
            this.LoadFileDetails(marshal, Unmarshal.ReadFromByteArray(marshal));
            // make sure to focus it too!
            marshalDataTab.Show();
        }
    }
}