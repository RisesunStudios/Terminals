﻿using System.Windows.Forms;

namespace Terminals.Plugins.Putty
{
    partial class TelnetOptionsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        private Label labelSession;
        private ComboBox cmbSessionName;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbSessionName = new System.Windows.Forms.ComboBox();
            this.labelSession = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbSessionName
            // 
            this.cmbSessionName.FormattingEnabled = true;
            this.cmbSessionName.Location = new System.Drawing.Point(53, 41);
            this.cmbSessionName.Name = "cmbSessionName";
            this.cmbSessionName.Size = new System.Drawing.Size(165, 21);
            this.cmbSessionName.TabIndex = 0;
            // 
            // labelSession
            // 
            this.labelSession.AutoSize = true;
            this.labelSession.Location = new System.Drawing.Point(3, 44);
            this.labelSession.Name = "labelSession";
            this.labelSession.Size = new System.Drawing.Size(44, 13);
            this.labelSession.TabIndex = 1;
            this.labelSession.Text = "Session";
            // 
            // PuttyOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelSession);
            this.Controls.Add(this.cmbSessionName);
            this.Name = "PuttyOptionsControl";
            this.Size = new System.Drawing.Size(650, 443);
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion
    }
}
