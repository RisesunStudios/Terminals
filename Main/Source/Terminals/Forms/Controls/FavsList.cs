﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Threading.Tasks;
using System.Windows.Forms;
using Terminals.Configuration;
using Terminals.Connections;
using Terminals.Credentials;
using Terminals.Data;
using Terminals.Forms;
using Terminals.Forms.Controls;
using Terminals.Integration;
using Terminals.Network;
using TreeView = Terminals.Forms.Controls.TreeView;

namespace Terminals
{
    internal partial class FavsList : UserControl
    {
        private FavoriteTreeListLoader treeLoader;
        private static readonly string shutdownFailMessage = Program.Resources.GetString("UnableToRemoteShutdown");
        internal ConnectionsUiFactory ConnectionsUiFactory { private get; set; }

        private static IFavorites PersistedFavorites
        {
            get { return Persistence.Instance.Favorites; }
        }

        public FavsList()
        {
            InitializeComponent();

            // Update the old treeview theme to the new theme from Win Vista and up
            Native.Methods.SetWindowTheme(this.favsTree.Handle, "Explorer", null);
            Native.Methods.SetWindowTheme(this.historyTreeView.Handle, "Explorer", null);

            this.historyTreeView.DoubleClick += new EventHandler(this.HistoryTreeView_DoubleClick);
        }

        #region Private methods

        private void CloseMenuStrips()
        {
            this.favoritesContextMenu.Close();
            this.groupsContextMenu.Close();
        }

        #endregion

        #region Private event handler methods

        private void FavsList_Load(object sender, EventArgs e)
        {
            this.treeLoader = new FavoriteTreeListLoader(this.favsTree, Persistence.Instance);
            this.treeLoader.LoadGroups();
            this.historyTreeView.Load();
            this.LoadState();
            this.favsTree.MouseUp += new MouseEventHandler(this.FavsTree_MouseUp);
            this.searchPanel1.LoadEvents();
        }

        private void HistoryTreeView_DoubleClick(object sender, EventArgs e)
        {
            this.StartConnection(this.historyTreeView);
        }

        private void PingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenNetworkingTool(NettworkingTools.Ping);
        }

        private void DNsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenNetworkingTool(NettworkingTools.Dns);
        }

        private void TraceRouteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenNetworkingTool(NettworkingTools.Trace);
        }

        private void TsAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenNetworkingTool(NettworkingTools.TsAdmin);
        }

        private void OpenNetworkingTool(NettworkingTools toolName)
        {
            IFavorite fav = this.GetSelectedFavorite();
            if (fav != null)
                this.ConnectionsUiFactory.OpenNetworkingTools(toolName, fav.ServerName);
        }

        private void CreateFavoriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frmNewTerminal = new NewTerminalForm(string.Empty))
            {
                var groupNode = this.favsTree.SelectedNode as GroupTreeNode;
                if (groupNode != null)
                    frmNewTerminal.AssingSelectedGroup(groupNode.Group);

                frmNewTerminal.ShowDialog();
            }
        }

        private void PropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFavorite fav = this.GetSelectedFavorite();
            if (fav != null)
                this.ShowManageTerminalForm(fav);
        }

        private void ShowManageTerminalForm(IFavorite favorite)
        {
            using (var frmNewTerminal = new NewTerminalForm(favorite))
            {
                TerminalFormDialogResult result = frmNewTerminal.ShowDialog();

                if (result != TerminalFormDialogResult.Cancel)
                {
                    if (result == TerminalFormDialogResult.SaveAndConnect)
                        this.ConnectionsUiFactory.CreateTerminalTab(frmNewTerminal.Favorite);
                }
            }
        }

        private void RebootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Reboot();
        }

        private void Reboot()
        {
            this.ProcessRemoteShutdownOpearation("reboot", ShutdownCommands.ForcedReboot);
        }

        private void ShutdownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ProcessRemoteShutdownOpearation("shutdown", ShutdownCommands.ForcedShutdown);
        }

        private void ProcessRemoteShutdownOpearation(string operationName, ShutdownCommands shutdownStyle)
        {
            IFavorite fav = this.GetSelectedFavorite();
            if (fav == null)
                return;

            const string QUESTION_TEMPLATE = "Are you sure you want to {0} this machine: {1}\r\n" +
                                             "Operation requires administrator priviledges and can take a while.";
            var question = String.Format(QUESTION_TEMPLATE, operationName, fav.ServerName);
            string title = Program.Resources.GetString("Confirmation");
            var confirmResult = MessageBox.Show(question, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult != DialogResult.Yes)
                return;

            var options = new Tuple<ShutdownCommands, IFavorite>(shutdownStyle, fav);
            var shutdownTask = Task.Factory.StartNew(new Func<object, string>(TryPerformRemoteShutdown), options);
            shutdownTask.ContinueWith(new Action<Task<string>>(ShowRebootResult), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void ShowRebootResult(Task<string> shutdownTask)
        {
            MessageBox.Show(shutdownTask.Result, "Remote action result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static string TryPerformRemoteShutdown(object state)
        {
            try
            {
                var options = state as Tuple<ShutdownCommands, IFavorite>;
                if (options != null && RemoteManagement.ForceShutdown(options.Item2, options.Item1))
                    return "Terminals successfully sent the shutdown command.";

                return shutdownFailMessage;
            }
            catch (ManagementException ex)
            {
                Logging.Log.Info(ex.ToString(), ex);
                return shutdownFailMessage + "\r\nPlease check the log file for more details.";
            }
            catch (UnauthorizedAccessException)
            {
                return shutdownFailMessage + "\r\n\r\nAccess is Denied.";
            }
        }

        private void EnableRDPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFavorite fav = this.GetSelectedFavorite();
            Task<bool?> enableRdpTask = Task.Factory.StartNew(new Func<object, bool?>(TryEnableRdp), fav);
            enableRdpTask.ContinueWith(this.ShowEnableRdpResult, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void ShowEnableRdpResult(Task<bool?> enableRdpTask)
        {
            bool? operationResult = enableRdpTask.Result;
            if (operationResult.HasValue)
                this.ShowEnableRdpResult(operationResult.Value);
            else
                MessageBox.Show("Terminals was not able to enable RDP remotely.");
        }

        private void ShowEnableRdpResult(bool operationResult)
        {
            if (operationResult)
            {
                const string MESSAGE = "Terminals enabled the RDP on the remote machine, " +
                                       "would you like to reboot that machine for the change to take effect?";
                if (MessageBox.Show(MESSAGE, "Reboot Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.OK)
                    this.Reboot();
            }
            else
            {
                MessageBox.Show("RDP is already enabled.");
            }
        }

        private static bool? TryEnableRdp(object state)
        {
            try
            {
                var fav = state as IFavorite;
                if (fav == null)
                    return null;
                return RemoteManagement.EnableRdp(fav);
            }
            catch // dont need to recover (registry problem, RPC problem, UAC not happy)
            {
                return null;
            }
        }

        private void ConnectFromContextMenu(List<IFavorite> favorites)
        {
            var definition = new ConnectionDefinition(favorites);
            this.ConnectionsUiFactory.Connect(definition);
            this.CloseMenuStrips();
        }

        private void ExtraConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFavorite favorite = this.GetSelectedFavorite();
            if (favorite == null)
                return;

            var favorites = new List<IFavorite>() { favorite };
            this.ConnectToFavoritesExtra(favorites);
            this.CloseMenuStrips();
        }

        private void ConnectToAllExtraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IFavorite> selectedFavorites = this.GetSelectedGroupFavorites();
            this.ConnectToFavoritesExtra(selectedFavorites);
            this.CloseMenuStrips();
        }

        private void ConnectToFavoritesExtra(List<IFavorite> selectedFavorites)
        {
            using (var usrForm = new ConnectExtraForm())
            {
                if (usrForm.ShowDialog() != DialogResult.OK)
                    return;

                var definition = new ConnectionDefinition(selectedFavorites, usrForm.Console, usrForm.NewWindow, usrForm.Credentials);
                this.ConnectionsUiFactory.Connect(definition);
            }
        }

        private void ComputerManagementMmcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFavorite fav = this.GetSelectedFavorite();
            if (fav != null)
                Process.Start("mmc.exe", "compmgmt.msc /a /computer=" + fav.ServerName);
        }

        private void SystemInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFavorite fav = this.GetSelectedFavorite();
            if (fav != null)
            {
                String programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                //if(programFiles.Contains("(x86)")) programFiles = programFiles.Replace(" (x86)","");
                String path = String.Format(@"{0}\common files\Microsoft Shared\MSInfo\msinfo32.exe", programFiles);
                if (File.Exists(path))
                {
                    Process.Start(String.Format("\"{0}\"", path), String.Format("/computer {0}", fav.ServerName));
                }
            }
        }

        private void SetCredentialByTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string VARIABLE = "Credential";
            InputBoxResult result = this.PromptForVariableChange(VARIABLE);
            if (result.ReturnCode == DialogResult.OK)
            {
                ICredentialSet credential = Persistence.Instance.Credentials[result.Text];
                if (credential == null)
                {
                    MessageBox.Show("The credential you specified does not exist.");
                    return;
                }

                List<IFavorite> selectedFavorites = this.StartBatchUpdate();
                PersistedFavorites.ApplyCredentialsToAllFavorites(selectedFavorites, credential);
                this.FinishBatchUpdate(VARIABLE);
            }
        }

        private void SetPasswordByTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string VARIABLE = "Password";
            InputBoxResult result = this.PromptForVariableChange(VARIABLE, '*');
            if (result.ReturnCode == DialogResult.OK)
            {
                List<IFavorite> selectedFavorites = this.StartBatchUpdate();
                PersistedFavorites.SetPasswordToAllFavorites(selectedFavorites, result.Text);
                this.FinishBatchUpdate(VARIABLE);
            }
        }

        private void SetDomainByTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string VARIABLE = "Domain name";
            InputBoxResult result = this.PromptForVariableChange(VARIABLE);
            if (result.ReturnCode == DialogResult.OK)
            {
                List<IFavorite> selectedFavorites = this.StartBatchUpdate();
                PersistedFavorites.ApplyDomainNameToAllFavorites(selectedFavorites, result.Text);
                this.FinishBatchUpdate(VARIABLE);
            }
        }

        private void SetUsernameByTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string VARIABLE = "User name";
            InputBoxResult result = this.PromptForVariableChange(VARIABLE);
            if (result.ReturnCode == DialogResult.OK)
            {
                List<IFavorite> selectedFavorites = this.StartBatchUpdate();
                PersistedFavorites.ApplyUserNameToAllFavorites(selectedFavorites, result.Text);
                this.FinishBatchUpdate(VARIABLE);
            }
        }

        private List<IFavorite> StartBatchUpdate()
        {
            if (this.ParentForm != null)
                this.ParentForm.Cursor = Cursors.WaitCursor;
            return this.GetSelectedGroupFavorites();
        }

        private void FinishBatchUpdate(string variable)
        {
            if (this.ParentForm != null)
                this.ParentForm.Cursor = Cursors.Default;
            string message = string.Format("Set {0} by group Complete.", variable);
            MessageBox.Show(message);
        }

        private InputBoxResult PromptForVariableChange(string variable, char passwordChar = '\0')
        {
            String groupName = this.favsTree.SelectedNode.Text;
            string prompt = String.Format("This will replace the {0} for all Favorites within this group.\r\nUse at your own risk!\r\n\r\nEnter new {0}:",
                                            variable);
            string title = string.Format("Change {0} - {1}", variable, groupName);
            if (passwordChar != '\0')
                return InputBox.Show(prompt, title, passwordChar);

            return InputBox.Show(prompt, title);
        }

        private void DeleteAllFavoritesByTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String groupName = this.favsTree.SelectedNode.Text;
            string title = "Delete all Favorites by group - " + groupName;
            const string PROMPT = "This will DELETE all Favorites within this group.\r\nDo you realy want to delete them?";
            DialogResult result = MessageBox.Show(PROMPT, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                List<IFavorite> selectedFavorites = this.StartBatchUpdate();
                Persistence.Instance.Favorites.Delete(selectedFavorites);
                if (this.ParentForm != null)
                    this.ParentForm.Cursor = Cursors.Default;
                MessageBox.Show("Delete all Favorites by group Complete.");
            }
        }

        private void RemoveSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFavorite favorite = this.GetSelectedFavorite();
            if (favorite != null && OrganizeFavoritesForm.AskIfRealyDelete("favorite"))
                PersistedFavorites.Delete(favorite);
        }

        private void FavsTree_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            var clickedPoint = new Point(e.X, e.Y);
            TreeNode clickedNode = this.favsTree.GetNodeAt(clickedPoint);
            this.favsTree.SelectedNode = clickedNode;

            if (clickedNode != null)
                this.FavsTreeNodeMenuOpening(clickedPoint);
            else
                this.defaultContextMenu.Show(this.favsTree, clickedPoint);
        }

        private void FavsTreeNodeMenuOpening(Point clickedPoint)
        {
            if (this.favsTree.SelectedFavorite != null)
                this.favoritesContextMenu.Show(this.favsTree, clickedPoint);
            else
                this.groupsContextMenu.Show(this.favsTree, clickedPoint);
        }

        private void FavsTree_DoubleClick(object sender, EventArgs e)
        {
            this.StartConnection(favsTree);
        }

        private void FavsTree_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
        }

        private void FavsTree_DragDrop(object sender, DragEventArgs e)
        {
            var files = e.Data.GetData(DataFormats.FileDrop) as String[];
            if (files != null)
            {
                List<FavoriteConfigurationElement> favoritesToImport = Integrations.Importers.ImportFavorites(files);
                var managedImport = new ImportWithDialogs(this.ParentForm);
                managedImport.Import(favoritesToImport);
            }
        }

        private void StartConnection(TreeView tv)
        {
            // connections are always under some parent node in History and in Favorites
            // dont connect in rename in favorites tree
            if (tv.SelectedNode != null && tv.SelectedNode.Level > 0 && !tv.SelectedNode.IsEditing)
            {
                this.ConnectionsUiFactory.Connect(new ConnectionDefinition(tv.SelectedNode.Text));
            }
        }

        private void HistoryTreeView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.StartConnection(historyTreeView);
        }

        private void CreateGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string newGroupName = NewGroupForm.AskFroGroupName();
            if (string.IsNullOrEmpty(newGroupName))
                return;

            FavoritesFactory.GetOrAddNewGroup(newGroupName);
        }

        private void DeleteGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var groupNode = this.favsTree.SelectedNode as GroupTreeNode;
            if (groupNode != null && OrganizeFavoritesForm.AskIfRealyDelete("group"))
                Persistence.Instance.Groups.Delete(groupNode.Group);
        }

        private void DuplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFavorite selected = this.GetSelectedFavorite();
            if (selected != null)
                OrganizeFavoritesForm.CopyFavorite(selected);
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            this.ConnectToSelectedFavorites();
        }

        private List<IFavorite> GetSelectedFavorites()
        {
            var selected = this.GetSelectedFavorite();
            if (selected != null)
                return new List<IFavorite>() { selected };

            return this.GetSelectedGroupFavorites();
        }

        /// <summary>
        /// Because the favorite context menu is shared between tree view and search results list,
        /// we have to distinguish, from where to obtain the selected favorite.
        /// </summary>
        private IFavorite GetSelectedFavorite()
        {
            if (this.tabControl1.SelectedTab == this.searchTabPage)
                return this.searchPanel1.SelectedFavorite;

            // favorites tree view is selected
            return this.favsTree.SelectedFavorite;
        }

        private List<IFavorite> GetSelectedGroupFavorites()
        {
            var groupNode = this.favsTree.SelectedNode as GroupTreeNode;
            if (groupNode == null)
                return new List<IFavorite>();

            return groupNode.Favorites;
        }

        private void CollapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.favsTree.CollapseAll();
        }

        private void CollpseHistoryButton_Click(object sender, EventArgs e)
        {
            this.historyTreeView.CollapseAll();
        }

        private void ClearHistoryButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Persistence.Instance.ConnectionHistory.Clear();
            this.Cursor = Cursors.Default;
        }

        private void FavsTree_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.StartConnection(this.favsTree);
                    break;
                case Keys.F2:
                    this.BeginRenameInFavsTree();
                    break;
            }
        }

        private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BeginRenameInFavsTree();
        }

        private void BeginRenameInFavsTree()
        {
            if (this.tabControl1.SelectedTab == this.FavoritesTabPage && this.favsTree.SelectedNode != null)
                this.favsTree.SelectedNode.BeginEdit();
        }

        private void FavsTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Label))
            {
                e.CancelEdit = true;
                return;
            }

            // following methods are called with the beginInvoke to realy perform the action. 
            // This is a trick how to deal with the WinForms fact, that in this event handler the action isnt realy finished.
            this.TryRenameFavoriteNode(e);
            this.TryRenameGroupNode(e);
        }

        private void TryRenameFavoriteNode(NodeLabelEditEventArgs e)
        {
            IFavorite favorite = this.favsTree.SelectedFavorite;
            this.SheduleFavoriteRename(favorite, e.Label);
        }

        private void TryRenameGroupNode(NodeLabelEditEventArgs e)
        {
            var groupNode = this.favsTree.SelectedNode as GroupTreeNode;
            if (groupNode != null)
            {
                var groupArguments = new object[] { groupNode.Group, e.Label };
                this.favsTree.BeginInvoke(new Action<IGroup, string>(RenameGroup), groupArguments);
            }
        }

        private static void RenameGroup(IGroup group, string newName)
        {
            // todo validate the name
            group.Name = newName;
            Persistence.Instance.Groups.Update(group);
        }

        private void SearchPanel1_ResultListAfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            IFavorite favorite = this.GetSelectedFavorite();
            // user canceled the rename
            if (string.IsNullOrEmpty(e.Label))
                return;

            this.SheduleFavoriteRename(favorite, e.Label);
        }

        private void SheduleFavoriteRename(IFavorite favorite, string newName)
        {
            if (favorite == null)
                return;

            var favoriteArguments = new object[] { favorite, newName };
            this.favsTree.BeginInvoke(new Action<IFavorite, string>(RenameFavorite), favoriteArguments);
        }

        private static void RenameFavorite(IFavorite favorite, string newName)
        {
            // todo validate the name
            favorite.Name = newName;
            PersistedFavorites.Update(favorite);
        }

        private void SearchPanel_ResultListKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            this.ConnectToSelectedFavorites();
        }

        private void ConnectToSelectedFavorites()
        {
            List<IFavorite> favorites = this.GetSelectedFavorites();
            this.ConnectFromContextMenu(favorites);
        }

        #endregion

        public void SaveState()
        {
            Settings.StartDelayedUpdate();
            Settings.ExpandedFavoriteNodes = this.favsTree.ExpandedNodes;
            Settings.ExpandedHistoryNodes = this.historyTreeView.ExpandedNodes;
            Settings.SaveAndFinishDelayedUpdate();
            this.searchPanel1.UnLoadEvents();
        }

        private void LoadState()
        {
            this.favsTree.ExpandedNodes = Settings.ExpandedFavoriteNodes;
            this.historyTreeView.ExpandedNodes = Settings.ExpandedHistoryNodes;
        }

        private void NewGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string newGroupName = NewGroupForm.AskFroGroupName();
            if (string.IsNullOrEmpty(newGroupName))
                return;

            var newGroup = Persistence.Instance.Factory.CreateGroup(newGroupName);
            var parentGroupNode = this.favsTree.SelectedNode as GroupTreeNode;
            if (parentGroupNode != null)
            {
                newGroup.Parent = parentGroupNode.Group;
            }

            Persistence.Instance.Groups.Add(newGroup);
        }
    }
}
