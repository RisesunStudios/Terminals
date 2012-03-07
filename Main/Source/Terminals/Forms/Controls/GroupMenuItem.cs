﻿using System;
using System.Windows.Forms;
using Terminals.Data;

namespace Terminals.Forms.Controls
{
    /// <summary>
    /// Custom toolstrip menu item with lazy loading
    /// </summary>
    internal class GroupMenuItem : ToolStripMenuItem
    {
        /// <summary>
        /// Stored in context menu Tag to identify virtual context menu groups by tag
        /// </summary>
        internal const String TAG = "tag";

        /// <summary>
        /// Gets or sets associated favorites group
        /// </summary>
        internal IGroup Group { get; set; }

        internal GroupMenuItem(IGroup group, bool createDummyItem = true)
        {
            this.Group = group;
            this.Text = group.Name;
            this.Tag = TAG;

            if (createDummyItem)
                this.DropDown.Items.Add(GroupTreeNode.DUMMY_NODE);
        }

        /// <summary>
        /// Gets the value indicating, that this node contains only dummy node
        /// and contains no favorite nodes
        /// </summary>
        internal Boolean IsEmpty
        {
            get
            {
                return this.DropDown.Items.Count == 1 &&
                String.IsNullOrEmpty(this.DropDown.Items[0].Name);
            }
        }

        internal void ClearDropDownsToEmpty()
        {
            this.DropDown.Items.Clear();
            this.DropDown.Items.Add(GroupTreeNode.DUMMY_NODE);
        }
    }
}
