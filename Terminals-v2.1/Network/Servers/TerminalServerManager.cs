using System;
using System.Windows.Forms;
using Terminals.Configuration;
using Terminals.Connections;
using Terminals.Data;
using Terminals.Forms;

namespace Terminals.Network.Servers
{
    public partial class TerminalServerManager : UserControl
    {
        public TerminalServerManager()
        {
            InitializeComponent();
        }

        public void ForceTSAdmin(string Host)
        {
            this.ServerNameComboBox.Text = Host;
            this.button1_Click(null, null);
        }
        TerminalServices.TerminalServer server;
        private void button1_Click(object sender, EventArgs e)
        {
            SelectedSession = null;
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            this.propertyGrid1.SelectedObject = null;
            Application.DoEvents();
            server = TerminalServices.TerminalServer.LoadServer(this.ServerNameComboBox.Text);
            if(server.IsATerminalServer)
            {
                dataGridView1.DataSource = server.Sessions;
                dataGridView1.Columns[1].Visible = false;
            }
            else
            {
                MessageBox.Show("This machine does not appear to be a Terminal Server");
            }

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(server.IsATerminalServer)
            {
                if(dataGridView1.DataSource != null)
                {
                    SelectedSession = server.Sessions[e.RowIndex];
                    this.propertyGrid1.SelectedObject = SelectedSession.Client;
                    this.dataGridView2.DataSource = SelectedSession.Processes;
                }
            }
            
        }
        
        public void Connect(string server, bool headless)
        {
            try
            {
                splitContainer1.Panel1Collapsed = headless;
                if(server != "")
                {
                    this.ServerNameComboBox.Text = server;
                    button1_Click(null, null);
                }
            }
            catch(Exception exc)
            {
                Logging.Log.Error("Connection Failure.", exc);
            }
        }
        TerminalServices.Session SelectedSession = null;
        private void TerminalServerManager_Load(object sender, EventArgs e)
        {
            ServerNameComboBox.Items.Clear();
            var favorites = Persistance.Instance.Favorites.GetFavorites();
            if (favorites != null)
            {
                foreach (FavoriteConfigurationElement elm in favorites)
                {
                    if (elm.Protocol == ConnectionManager.RDP)
                    {
                        this.ServerNameComboBox.Items.Add(elm.ServerName);
                    }
                }
            }
        }


        private void sendMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(SelectedSession != null)
            {
                InputBoxResult result = InputBox.Show("Please enter the message to send..");
                if(result.ReturnCode == DialogResult.OK && result.Text.Trim() != null)
                {
                    TerminalServices.TerminalServicesAPI.SendMessage(SelectedSession, "Message from your Administrator", result.Text.Trim(), 0, 10, false);
                }
            }
        }

        private void logoffSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(SelectedSession != null)
            {
                if(MessageBox.Show("Are you sure you want to log off the selected session?", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                {
                    TerminalServices.TerminalServicesAPI.LogOffSession(SelectedSession, false);
                }
            }
        }
        private void rebootServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(server.IsATerminalServer)
            {
                if(MessageBox.Show("Are you sure you want to reboot this server?", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                {
                    TerminalServices.TerminalServicesAPI.ShutdownSystem(this.server, true);
                }
            }
        }
        private void shutdownServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(server.IsATerminalServer)
            {
                if(MessageBox.Show("Are you sure you want to shutdown this server?", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                {
                    TerminalServices.TerminalServicesAPI.ShutdownSystem(this.server, false);
                }
            }
        }

        private void ServerNameComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }
    }
}