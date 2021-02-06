using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Network;
using PythonTypes.Types.Network;

namespace Editor
{
    public partial class MainWindow : Form
    {
        private delegate void ClientUpdateDelegate(Client newClient);
        private delegate void ClientPacketReceivedDelegate(PacketEntry entry);
        private ClientUpdateDelegate mClientAdd;
        private ClientUpdateDelegate mClientRemove;
        private ClientPacketReceivedDelegate mPacketReceived;
        
        private EVEBridgeServer mServer = new EVEBridgeServer();
        private List<Client> mClients = new List<Client>();
        private BindingSource mClientBinding = new BindingSource();
        private List<PacketEntry> mPackets = new List<PacketEntry>();
        private BindingSource mPacketListBinding = new BindingSource();

        private string mServerAddress = "127.0.0.1";
        private int mServerPort = 25999;
        
        public MainWindow()
        {
            this.mClientAdd = AddClient;
            this.mClientRemove = RemoveClient;
            this.mPacketReceived = FilterPacket;

            InitializeComponent();
            ExtendComponents();
            
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
            EVEBridgeServer server = ar.AsyncState as EVEBridgeServer;
            EVEClientSocket client = server.EndAccept(ar);
            
            // open a connection to the server to relay info from this client
            EVEClientSocket serverSocket = new EVEClientSocket(server.Log);
            serverSocket.Connect(this.mServerAddress, this.mServerPort);

            Client newClient = new Client(this.mClients.Count, client, serverSocket, this);

            this.Invoke(this.mClientAdd, newClient);
            
            // put the socket in accept state again
            this.mServer.BeginAccept(ServerConnectionAccept);
        }

        private void AddClient(Client newClient)
        {
            // add the client to the list and data source
            int index = this.mClientBinding.Add(newClient);

            newClient.ClientIndex = index;
        }

        private void RemoveClient(Client client)
        {
            this.mClientBinding.Remove(client);
        }

        private void serverAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            this.mServerAddress = this.serverAddressTextBox.Text;
        }

        private void serverPortTextBox_TextChanged(object sender, EventArgs e)
        {
            this.mServerPort = int.Parse(this.serverPortTextBox.Text);
        }

        public void OnClientDisconnected(Client client)
        {
            this.Invoke(this.mClientRemove, client);
        }

        public void OnPacketReceived(PacketEntry entry)
        {
            this.mPackets.Add(entry);
            this.Invoke(this.mPacketReceived, entry);
        }

        private void packetGridView_SelectionChanged(object sender, EventArgs e)
        {
            // update the rich text box
            if (this.packetGridView.SelectedRows.Count == 0)
            {
                this.packetTextBox.Text = "";
                return;
            }

            PacketEntry packet = this.packetGridView.SelectedRows[0].DataBoundItem as PacketEntry;

            this.packetTextBox.Text = packet.PacketString;
        }

        private void FilterPacket(PacketEntry packet)
        {
            if (packet.Client.IncludePackets == false)
                return;

            // check the type of packet
            if (undeterminedCheckbox.Checked == false && packet.Packet == null)
                return;
            if (callReqCheckbox.Checked == false && packet.Packet.Type == PyPacket.PacketType.CALL_REQ)
                return;
            if (callRspCheckbox.Checked == false && packet.Packet.Type == PyPacket.PacketType.CALL_RSP)
                return;
            if (notificationCheckbox.Checked == false && packet.Packet.Type == PyPacket.PacketType.NOTIFICATION)
                return;
            if (sessionChangeCheckbox.Checked == false && (packet.Packet.Type == PyPacket.PacketType.SESSIONCHANGENOTIFICATION || packet.Packet.Type == PyPacket.PacketType.SESSIONINITIALSTATENOTIFICATION))
                return;
            if (exceptionCheckbox.Checked == false && packet.Packet.Type == PyPacket.PacketType.ERRORRESPONSE)
                return;
            if (pingReqCheckbox.Checked == false && packet.Packet.Type == PyPacket.PacketType.PING_REQ)
                return;
            if (pingRspCheckbox.Checked == false && packet.Packet.Type == PyPacket.PacketType.PING_RSP)
                return;
            
            // check for service calls ONLY if CallRes/CallRsp are selected and the packet is an actual packet
            if (serviceNameTextbox.Text.Length > 0)
            {
                if (packet.Service != serviceNameTextbox.Text)
                    return;
            }
            
            this.mPacketListBinding.Add(packet);
        }

        private void FilterFullPacketList()
        {
            // clear the packets first
            this.mPacketListBinding.Clear();
            
            foreach (PacketEntry entry in this.mPackets)
                this.FilterPacket(entry);
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            // clear lists to free memory
            this.mPackets.Clear();
            this.mPacketListBinding.Clear();
            // clear client's calls cache
            foreach (Client client in this.mClients)
                client.CallIDList.Clear();
        }

        private void packetGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.packetGridView.SelectedCells.Count == 0)
            {
                this.packetTextBox.Text = "";
                return;
            }


            PacketEntry packet = this.packetGridView.Rows[this.packetGridView.SelectedCells[0].RowIndex].DataBoundItem as PacketEntry;

            this.packetTextBox.Text = packet.PacketString;
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
    }
}