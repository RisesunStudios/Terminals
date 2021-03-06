using System;
using System.Linq;
using System.Windows.Forms;
using Terminals.Data;
using Terminals.Data.Credentials;

namespace Terminals.Credentials
{
    internal partial class CredentialManager : Form
    {
        private readonly IPersistence persistence;

        private ICredentials Credentials
        {
            get { return this.persistence.Credentials; }
        }

        internal CredentialManager(IPersistence persistence)
        {
            this.InitializeComponent();

            this.persistence = persistence;
            this.Credentials.CredentialsChanged += new EventHandler(this.CredentialsChanged);
        }

        private void CredentialsChanged(object sender, EventArgs e)
        {
            this.BindList();
        }

        private void BindList()
        {
            this.gridCredentials.AutoGenerateColumns = false;
            var toShow = this.Credentials.Select(this.ToEditedCredentials);
            this.gridCredentials.DataSource = new SortableList<EditedCredentials>(toShow);
        }

        private EditedCredentials ToEditedCredentials(ICredentialSet editedCredentials)
        {
            var guarded = new GuardedCredential(editedCredentials, this.persistence.Security);
            return new EditedCredentials(editedCredentials, guarded.UserName, guarded.Domain);
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CredentialManager_Load(object sender, EventArgs e)
        {
            this.BindList();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            this.EditCredential(null);
        }

        private ICredentialSet GetSelectedCredentials()
        {
            if (this.gridCredentials.SelectedRows.Count > 0)
            {
                var selected = this.gridCredentials.SelectedRows[0].DataBoundItem as EditedCredentials;
                return selected.Edited;
            }

            return null;
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            this.EditSelectedCredential();
        }

        private void EditSelectedCredential()
        {
            ICredentialSet selected = this.GetSelectedCredentials();
            if (selected != null)
            {
                this.EditCredential(selected);
            }
        }

        private void EditCredential(ICredentialSet selected)
        {
            using (var mgr = new ManageCredentialForm(this.persistence, selected))
            {
                if (mgr.ShowDialog() == DialogResult.OK)
                    this.BindList();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            ICredentialSet toRemove = this.GetSelectedCredentials();
            if (toRemove != null)
            {
                if (MessageBox.Show("Are you sure you want to delete credential " + toRemove.Name + "?",
                                    "Credential manager", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Credentials.Remove(toRemove);
                    this.BindList();
                }
            }
        }

        private void CredentialManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Credentials.CredentialsChanged -= this.CredentialsChanged;
        }

        private void GridCredentials_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn lastSortedColumn = this.gridCredentials.FindLastSortedColumn();
            DataGridViewColumn column = this.gridCredentials.Columns[e.ColumnIndex];

            SortOrder newSortDirection = SortableUnboundGrid.GetNewSortDirection(lastSortedColumn, column);
            var data = this.gridCredentials.DataSource as SortableList<EditedCredentials>;
            this.gridCredentials.DataSource = data.SortByProperty(column.DataPropertyName, newSortDirection);
            column.HeaderCell.SortGlyphDirection = newSortDirection;
        }

        private void GridCredentials_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // dont allow double click on column row
                this.EditSelectedCredential();
        }
    }
}
