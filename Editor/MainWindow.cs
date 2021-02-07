using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Network;
using PythonTypes.Types.Network;
using PythonTypes.Types.Primitives;

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

        private string mServerAddress = "127.0.0.1";
        private int mServerPort = 25999;
        private bool mIgnoreIncomingPackets = false;
        
        public MainWindow()
        {
            this.mClientAdd = AddClient;
            this.mPacketReceived = FilterPacket;

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
            // disable multiselect
            this.packetGridView.MultiSelect = false;
        }
        

        public void ServerConnectionAccept(IAsyncResult ar)
        {
            try
            {
                EVEBridgeServer server = ar.AsyncState as EVEBridgeServer;
                EVEClientSocket client = server.EndAccept(ar);
            
                // open a connection to the server to relay info from this client
                EVEClientSocket serverSocket = new EVEClientSocket(server.Log);
                serverSocket.Connect(this.mServerAddress, this.mServerPort);

                LiveClient newLiveClient = new LiveClient(this.mClients.Count, client, serverSocket, this);

                this.Invoke(this.mClientAdd, newLiveClient);

                clientCountLabel.Text = $"{this.mClients.Count} clients connected";
            }
            catch (Exception)
            {
                // ignored
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
            PacketEntry packet = this.packetGridView.Rows[index].DataBoundItem as PacketEntry;

            this.packetTextBox.Text = packet.PacketString;
            
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
            if (packet.Packet != null && serviceNameTextbox.Text.Length > 0)
            {
                if (packet.Service != serviceNameTextbox.Text)
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
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                PacketListFile.SavePackets(saveFileDialog1.FileName, this.mPackets, this.mClients);                
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
                if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
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
                    
                    PacketListFile.LoadPackets(this.openFileDialog1.FileName, out this.mPackets, out this.mClients);
                    
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
    }
}