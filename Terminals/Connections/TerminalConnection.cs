using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Terminals.Properties;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using WalburySoftware;
using SSHClient;
using Org.Mentalis.Network.ProxySocket;

namespace Terminals.Connections
{
    public class TerminalConnection : Connection
    {
        #region Connection Members
        private bool connected = false;
        public override bool Connected { get { return connected; } }
        public override void ChangeDesktopSize(Terminals.DesktopSize Size)
        {
        }

        private TerminalEmulator term;
        private ProxySocket client;
        private SSHClient.Protocol sshProtocol;

        public override bool Connect()
        {
            string protocol = "unknown";
            try
            {
                Terminals.Logging.Log.Info("Connecting to a "+Favorite.Protocol+" Connection");
                term = new TerminalEmulator();

                Controls.Add(term);
                term.BringToFront();
                this.BringToFront();

                term.Parent = base.TerminalTabPage;
                this.Parent = TerminalTabPage;
                term.Dock = DockStyle.Fill;

                term.BackColor = Color.FromName(Favorite.ConsoleBackColor);
                term.Font = FontParser.ParseFontName(Favorite.ConsoleFont);
                term.ForeColor = Color.FromName(Favorite.ConsoleTextColor);

                term.Rows = Favorite.ConsoleRows;
                term.Columns = Favorite.ConsoleCols;

                client = new ProxySocket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
                client.ProxyUser="";
                client.ProxyPass="";
                client.Connect(Favorite.ServerName,Favorite.Port);

                string domainName = Favorite.DomainName;
                string pass = Favorite.Password;
                string userName = Favorite.UserName;

                if (string.IsNullOrEmpty(domainName)) domainName = Settings.DefaultDomain;
                if (string.IsNullOrEmpty(pass)) pass = Settings.DefaultPassword;
                if (string.IsNullOrEmpty(userName)) userName = Settings.DefaultUsername;


                if (Favorite.Protocol == "Telnet")
                {
                    protocol = "Telnet";
                    TcpProtocol t = new TcpProtocol(new NetworkStream(client));
                    TelnetProtocol p = new TelnetProtocol();
                    t.OnDataIndicated += p.IndicateData;
	            	t.OnDisconnect += this.OnDisconnected;
                    p.TerminalType = term.TerminalType;
	            	p.Username=userName;
	            	p.Password = pass;
	            	p.OnDataIndicated += term.IndicateData;
	            	p.OnDataRequested += t.RequestData;
	            	term.OnDataRequested += p.RequestData;
	                connected = client.Connected;
                }
                else
                {
                    sshProtocol = new SSHClient.Protocol();
                    sshProtocol.setTerminalParams(term.TerminalType,
	                                   term.Rows, term.Columns);
                    sshProtocol.OnDataIndicated += term.IndicateData;
                    sshProtocol.OnDisconnect += this.OnDisconnected;
                    term.OnDataRequested += sshProtocol.RequestData;
                	string key = "";
                	SSHClient.KeyConfigElement e = Settings.SSHKeys.Keys[Favorite.KeyTag];
            		if(e!=null)
                		key = e.Key;
                    sshProtocol.setProtocolParams(
	            		Favorite.AuthMethod,
	            		userName,
	            		pass,
	            		key,
	            		Favorite.SSH1);

                    if (Favorite.SSH1)
                    {
                        protocol = "SSH1";
                    }
                    else
                    {
                        protocol = "SSH2";
                    }
                    sshProtocol.Connect(client);
                    connected = true; // SSH will throw if fails
                }
                term.Focus();
                return true;
            }
            catch(Exception exc)
            {
                Terminals.Logging.Log.Fatal("Connecting to "+protocol+" Connection", exc);
                return false;
            }
        }

        void OnDisconnected()
        {
            Terminals.Logging.Log.Fatal(this.Favorite.Protocol + " Connection Lost" + this.Favorite.Name);
            this.connected = false;
            if (ParentForm.InvokeRequired)
            {
                InvokeCloseTabPage d = new InvokeCloseTabPage(CloseTabPage);
                this.Invoke(d, new object[] { this.Parent });
            }
            else
                CloseTabPage(this.Parent);
        }

        public override void Disconnect()
        {
            try
            {
                client.Close();
                if (sshProtocol != null)
                    sshProtocol.OnConnectionClosed();
            }
            catch(Exception e)
            {
                Terminals.Logging.Log.Error("Disconnect", e);
            }
        }

        #endregion

    }
}
