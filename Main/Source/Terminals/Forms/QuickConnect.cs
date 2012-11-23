using System;
using System.Linq;
using System.Windows.Forms;
using Terminals.Data;

namespace Terminals
{
    // todo add missing feature QuickConnect
    internal partial class QuickConnect : Form
    {
        public QuickConnect()
        {
            InitializeComponent();
            LoadFavorites();
            InputTextbox.Focus();
        }

        void cmbServerList_GotFocus(object sender, EventArgs e)
        {
            this.InputTextbox.Focus();
        }
        
        private void LoadFavorites()
        {
            var q = InputTextbox.Text;
            var favorites = Persistence.Instance.Favorites;
            var favoriteNames = (
                from f in favorites 
                select f.Name
                ).ToArray();
            this.InputTextbox.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            this.InputTextbox.AutoCompleteCustomSource.AddRange(favoriteNames);
        }

        public string ConnectionName
        {
            get
            {
                return InputTextbox.Text;
            }
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void TestKeys(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }
            
        }
        private void QuickConnect_KeyUp(object sender, KeyEventArgs e)
        {
            TestKeys(e);
        }

        private void InputTextbox_KeyUp(object sender, KeyEventArgs e)
        {
            TestKeys(e);
        }

        private void ButtonConnect_KeyUp(object sender, KeyEventArgs e)
        {
            TestKeys(e);
        }

        private void ButtonCancel_KeyUp(object sender, KeyEventArgs e)
        {
            TestKeys(e);
        }

    }
}