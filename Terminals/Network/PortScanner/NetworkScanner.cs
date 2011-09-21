using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using Terminals.Configuration;
using Terminals.Connections;
using Terminals.Forms.Controls;
using Terminals.Network;
using Terminals.Scanner;
using Unified;

namespace Terminals
{
    internal partial class NetworkScanner : Form
    {
        private NetworkScanManager manager;
        private bool validation = false;

        internal NetworkScanner()
        {
            InitializeComponent();
  
            FillTextBoxesFromLocalIp();
            InitScanManager();
            this.gridScanResults.AutoGenerateColumns = false;

            Server.OnClientConnection += new Server.ClientConnection(Server_OnClientConnection);
            Client.OnServerConnection += new Client.ServerConnection(Client_OnServerConnection);
            
            this.bsScanResults.DataSource = new SortableList<NetworkScanResult>();
        }

        private void FillTextBoxesFromLocalIp()
        {
            string localIP = TryGetLocalIP();
            string[] ipList = localIP.Split('.');
            this.ATextbox.Text = ipList[0];
            this.BTextbox.Text = ipList[1];
            this.CTextbox.Text = ipList[2];
            this.DTextbox.Text = "1";
            this.ETextbox.Text = "255";
            this.ServerAddressLabel.Text = localIP;
        }

        private static String TryGetLocalIP()
        {   
            string localIP = "127.0.0.1";
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface nic in nics)
                {
                    if (nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {
                        localIP = nic.GetIPProperties().GatewayAddresses[0].Address.ToString();
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Log.Error("Network Scanner Failed to Initialize", e);
            }
            return localIP;
        }

        private void InitScanManager()
        {
            this.manager = new NetworkScanManager();
            this.manager.OnAddressScanHit += new NetworkScanHandler(this.manager_OnScanHit);
            this.manager.OnAddressScanFinished += new NetworkScanHandler(this.manager_OnAddresScanFinished);
        }

        private void ScanButton_Click(object sender, EventArgs e)
        {
            scanProgressBar.Value = 0;

            if (ScanButton.Text == "&Scan")
            {
                this.StartScan();
            }
            else
            {
                StopScan();
            }
        }

        private void StopScan()
        {
            this.manager.StopScan();
            this.ScanStatusLabel.Text = "Scan Stopped.";
            this.ScanButton.Text = "&Scan";
        }

        private void StartScan()
        {
            this.bsScanResults.Clear();
            ScanStatusLabel.Text = "Initiating Scan...";
            ScanButton.Text = "Stop";
            List<int> ports = GetSelectedPorts();
            this.manager.StartScan(this.ATextbox.Text, this.BTextbox.Text, this.CTextbox.Text,
                                   this.DTextbox.Text, this.ETextbox.Text, ports);
        }

        private List<int> GetSelectedPorts()
        {
            List<Int32> ports = new List<Int32>();
            if (this.RDPCheckbox.Checked)
                ports.Add(ConnectionManager.RDPPort);
            if (this.VNCCheckbox.Checked || this.VMRCCheckbox.Checked)
                ports.Add(ConnectionManager.VNCVMRCPort);
            if(this.TelnetCheckbox.Checked)
                ports.Add(ConnectionManager.TelnetPort);
            if (this.SSHCheckbox.Checked)
                ports.Add(ConnectionManager.SSHPort);

            return ports;
        }

        private void manager_OnAddresScanFinished(ScanItemEventArgs args)
        {
            this.Invoke(new MethodInvoker(UpdateScanStatus));
        }

        /// <summary>
        /// Updates the status bar, button state and progress bar.
        /// The last who sends "is done" autoamticaly informs about the compleated state.
        /// </summary>
        private void UpdateScanStatus()
        {
            this.scanProgressBar.Maximum = this.manager.AllAddressesToScan;
            scanProgressBar.Value = this.manager.DoneAddressScans;
            Int32 pendingAddresses = this.manager.AllAddressesToScan - scanProgressBar.Value;
            Debug.WriteLine(String.Format("updating status with pending ({0}): {1}", 
                this.manager.ScanIsRunning, pendingAddresses));


            ScanStatusLabel.Text = String.Format("Pending items:{0}", pendingAddresses);
            if (scanProgressBar.Value >= scanProgressBar.Maximum)
                scanProgressBar.Value = 0;

            if (pendingAddresses == 0)
            {
                this.ScanButton.Text = "&Scan";
                ScanStatusLabel.Text = String.Format("Completed scan, found: {0} items.", this.bsScanResults.Count);
                scanProgressBar.Value = 0;
            }
        }

        private void manager_OnScanHit(ScanItemEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new NetworkScanHandler(manager_OnScanHit), new object[] { args });
            }
            else
            {
                this.bsScanResults.Add(args.ScanResult);
                this.gridScanResults.Refresh();
            }
        }

        private void AllCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.AllCheckbox.Checked)
            {
                this.RDPCheckbox.Checked = AllCheckbox.Checked;
                this.VNCCheckbox.Checked = AllCheckbox.Checked;
                this.VMRCCheckbox.Checked = AllCheckbox.Checked;
                this.TelnetCheckbox.Checked = AllCheckbox.Checked;
                this.SSHCheckbox.Checked = AllCheckbox.Checked;
            }
        }

        private void AddAllButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            String tags = GetTagsToApply();
            List<FavoriteConfigurationElement> favoritesToImport = GetFavoritesFromBindingSource(tags);
            ImportSelectedItems(favoritesToImport);
        }

        private List<FavoriteConfigurationElement> GetFavoritesFromBindingSource(String tags)
        {
            List<FavoriteConfigurationElement> favoritesToImport = new List<FavoriteConfigurationElement>();
            foreach (DataGridViewRow scanResultRow in this.gridScanResults.SelectedRows)
            {
                    var computer = scanResultRow.DataBoundItem as NetworkScanResult;
                    var favorite = computer.ToFavorite(tags);
                    favoritesToImport.Add(favorite);
            }
            return favoritesToImport;
        }

        private string GetTagsToApply()
        {
            String tags = this.TagsTextbox.Text;
            tags = tags.Replace("Tags...", String.Empty).Trim();
            return tags;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Server.ServerOnline)
            {
                this.button1.Text = "Start Server";
                Server.Stop();
            }
            else
            {
                this.button1.Text = "Stop Server";
                Server.Start();
            }
            if (Server.ServerOnline)
            {
                this.ServerStatusLabel.Text = "Server is ONLINE";
            }
            else
            {
                this.ServerStatusLabel.Text = "Server is OFFLINE";
            }
        }

        private void Client_OnServerConnection(MemoryStream Response)
        {
            if (Response.Length == 0)
            {
                MessageBox.Show("The server has nothing to share with you.");
            }
            else
            {
                ArrayList favorites = (ArrayList)Serialize.DeSerializeBinary(Response);
                SortableList<FavoriteConfigurationElement> favoritesToImport = GetReceivedFavorites(favorites);
                ImportSelectedItems(favoritesToImport);
            }
        }

        private void ImportSelectedItems(List<FavoriteConfigurationElement> favoritesToImport)
        {
            var managedImport = new ImportWithDialogs(this, false);
            managedImport.Import(favoritesToImport);
        }

        private static SortableList<FavoriteConfigurationElement> GetReceivedFavorites(ArrayList favorites)
        {
            var importedFavorites = new SortableList<FavoriteConfigurationElement>();
            foreach (object item in favorites)
            {
                SharedFavorite favorite = item as SharedFavorite;
                if (favorite != null)
                    importedFavorites.Add(ImportSharedFavorite(favorite));
            }
            return importedFavorites;
        }

        private static FavoriteConfigurationElement ImportSharedFavorite(SharedFavorite favorite)
        {
            FavoriteConfigurationElement newfav = SharedFavorite.ConvertFromFavorite(favorite);
            newfav.UserName = Environment.UserName;
            newfav.DomainName = Environment.UserDomainName;
            return newfav;
        }

        private void Server_OnClientConnection(String Username, Socket Socket)
        {
            ArrayList list = FavoritesToSharedList();
            Byte[] data = SharedListToBinaryData(list);
            Socket.Send(data);
            Server.FinishDisconnect(Socket);
        }

        private static Byte[] SharedListToBinaryData(ArrayList list)
        {
            MemoryStream favs = Serialize.SerializeBinary(list);

            if (favs != null && favs.Length > 0)
            {
                if (favs.CanRead && favs.Position > 0) 
                    favs.Position = 0;
                Byte[] data = favs.ToArray();
                favs.Close();
                favs.Dispose();
                return data;
            }

            return new byte[0];
        }

        private static ArrayList FavoritesToSharedList()
        {
            FavoriteConfigurationElementCollection favorites = Settings.GetFavorites();
            ArrayList list = new ArrayList();
            foreach (FavoriteConfigurationElement elem in favorites)
            {
                list.Add(SharedFavorite.ConvertFromFavorite(elem));
            }
            return list;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Client.Start(this.ServerAddressTextbox.Text);
        }

        private void NetworkScanner_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.manager.StopScan();
                Server.Stop();
                Client.Stop();
            }
            catch (Exception exc)
            {
                Logging.Log.Info("Network Scanner failed to stop server and client at close", exc);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PortCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if(!(sender as CheckBox).Checked)
              this.AllCheckbox.Checked = false;
        }

        /// <summary>
        /// Validate text boxes to allow inser only byte.
        /// </summary>
        private void IPTextbox_TextChanged(object sender, EventArgs e)
        {
            if (validation)
                return; // prevent stack overflow

            byte testValue;
            validation = true;
            TextBox textBox = sender as TextBox;
            bool isValid = Byte.TryParse(textBox.Text, NumberStyles.None, null,  out testValue);

            if (!isValid && validation)
                textBox.Text = textBox.Tag.ToString();
            else
                textBox.Tag = textBox.Text;

            validation = false;
        }

        private void gridScanResults_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn lastSortedColumn = this.gridScanResults.FindLastSortedColumn();
            DataGridViewColumn column = this.gridScanResults.Columns[e.ColumnIndex];

            SortOrder newSortDirection = SortableUnboundGrid.GetNewSortDirection(lastSortedColumn, column);
            var data = this.bsScanResults.DataSource as SortableList<NetworkScanResult>;
            this.bsScanResults.DataSource = data.SortByProperty(column.DataPropertyName, newSortDirection);
            column.HeaderCell.SortGlyphDirection = newSortDirection;
        }
    }
}
