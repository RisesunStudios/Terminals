using System;
using System.Threading;
using System.Windows.Forms;
using TabControl;
using System.Runtime.InteropServices;

namespace Terminals.Connections
{
    internal abstract class Connection : Control, IConnection
    {
        #region IConnection Members

        private TerminalServices.TerminalServer server;
        private bool isTerminalServer = false;
        private FavoriteConfigurationElement favorite;
        private TerminalTabControlItem terminalTabPage;
        private MainForm parentForm;
        
        internal delegate void InvokeCloseTabPage(TabControlItem tabPage);
        
        public delegate void TerminalServerStateDiscovery(FavoriteConfigurationElement Favorite, bool IsTerminalServer, TerminalServices.TerminalServer Server);
        
        public event TerminalServerStateDiscovery OnTerminalServerStateDiscovery;

        public delegate void LogHandler(string Entry);

        public event LogHandler OnLog;

        public abstract bool Connect();

        public abstract void Disconnect();

        public abstract bool Connected 
        {
            get;
        }

        public TerminalServices.TerminalServer Server
        {
            get
            {
                return this.server;
            }
            
            set
            {
                this.server = value;
            }
        }

        public bool IsTerminalServer
        {
            get
            {
                return this.isTerminalServer;
            }

            set
            {
                this.isTerminalServer = value;
            }
        }
    
        private void CheckForTS(object state)
        {
            this.isTerminalServer = false;
            FavoriteConfigurationElement host = (FavoriteConfigurationElement)state;
            try
            {
                Thread.Sleep(3000);
                this.server = TerminalServices.TerminalServer.LoadServer(host.ServerName);
                this.isTerminalServer = this.server.IsATerminalServer;
            }
            catch (Exception)
            {
                Logging.Log.Error(string.Format("Checked to see if {0} is a terminal server.  {0} is not a terminal server", host.ServerName));
            }

            if (this.OnTerminalServerStateDiscovery != null)
                this.OnTerminalServerStateDiscovery(host, this.isTerminalServer, this.server);
        }

        public void CheckForTerminalServer(FavoriteConfigurationElement Fav)
        {
            if (Fav.Protocol == ConnectionManager.RDP)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.CheckForTS), Fav);
            }

            this.isTerminalServer = false;
        }

        protected void Log(string Text)
        {
            if (this.OnLog != null)
                this.OnLog(Text);
        }
        
        public abstract void ChangeDesktopSize(DesktopSize Size);
        
        public FavoriteConfigurationElement Favorite 
        {
            get
            {
                return this.favorite;
            }
            
            set
            {
                this.favorite = value;
            }
        }
        
        public TerminalTabControlItem TerminalTabPage
        {
            get
            {
                return this.terminalTabPage;
            }
            
            set
            {
                this.terminalTabPage = value;
            }
        }

        public MainForm ParentForm
        {
            get
            {
                return this.parentForm;
            }

            set
            {
                this.parentForm = value;
            }
        }

        internal void CloseTabPage(object tabObject)
        {
            if (!(tabObject is TabControlItem))
                return;

            TabControlItem tabPage = (TabControlItem)tabObject;
            bool wasSelected = tabPage.Selected;
            this.ParentForm.RemoveTabPage(tabPage);
            if (wasSelected)
                Native.Methods.PostMessage(new HandleRef(this, this.Handle), MainForm.WM_LEAVING_FULLSCREEN, IntPtr.Zero, IntPtr.Zero);

            this.ParentForm.UpdateControls();
        }
        
        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            this.ControlAdded += new ControlEventHandler(this.Connection_ControlAdded);
            this.ResumeLayout(false);
        }

        private void Connection_ControlAdded(object sender, ControlEventArgs e)
        {
        }
    }
}