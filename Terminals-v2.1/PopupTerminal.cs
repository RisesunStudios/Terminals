﻿using System;
using System.Windows.Forms;
using Terminals.Data;

namespace Terminals
{
    internal partial class PopupTerminal : Form
    {
        private TerminalTabsSelectionControler mainTabsControler;
        private bool fullScreen = false;

        internal PopupTerminal()
        {
            InitializeComponent();
        }

        internal PopupTerminal(TerminalTabsSelectionControler mainTabsControler)
            : this()
        {
            this.mainTabsControler = mainTabsControler;
        }

        internal IFavorite Favorite
        {
            get
            {
                var tabControl = this.tabControl1.Items[0] as TerminalTabControlItem;
                return tabControl.Connection.Favorite;
            }
        }

        internal void AddTerminal(TerminalTabControlItem TabControlItem)
        {
            this.tabControl1.AddTab(TabControlItem);
            this.Text = TabControlItem.Connection.Favorite.Name;
        }

        internal void UpdateTitle()
        {
            this.Text = this.Favorite.Name;
            this.tabControl1.Items[0].Title = this.Text;
        }

        private void attachToTerminalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TerminalTabControlItem activeTab = this.tabControl1.SelectedItem as TerminalTabControlItem;
            if (activeTab != null)
            {
                this.mainTabsControler.AttachTabFromWindow(activeTab);
            }

            this.Close();
        }

        private void PopupTerminal_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.mainTabsControler.UnRegisterPopUp(this);
            TerminalTabControlItem activeTab = this.tabControl1.SelectedItem as TerminalTabControlItem;
            if (activeTab != null)
            {
                // doesnt metter yet. Nobody closes connections
                this.tabControl1.CloseTab(activeTab);
            }
        }

        private void CaptureToolStripButton_Click(object sender, EventArgs e)
        {
            CaptureScreen();
        }

        private void CaptureScreen()
        {
            CaptureManager.CaptureManager.PerformScreenCapture(this.tabControl1);
            this.mainTabsControler.RefreshCaptureManagerAndCreateItsTab(false);
        }

        internal void UpdateCaptureButtonEnabled(bool newEnabledState)
        {
            this.CaptureToolStripButton.Enabled = newEnabledState;
        }

        private void PopupTerminal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F12)
                CaptureScreen();

            if (e.KeyCode == Keys.F11)
                this.SwithFullScreen();
        }

        private void toolStripButtonFullScreen_Click(object sender, EventArgs e)
        {
            this.SwithFullScreen();
        }

        private void SwithFullScreen()
        {
            this.fullScreen = !this.fullScreen;
            if (this.fullScreen)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
            }
        }
    }
}
