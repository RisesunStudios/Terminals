﻿namespace WindowsFormsApplication2 {
    partial class PacketCapture {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.StopCaptureButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.hexBox1 = new Be.Windows.Forms.HexBox();
            this.CaptureButton = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FilterTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AmberPicture = new System.Windows.Forms.PictureBox();
            this.RedPicture = new System.Windows.Forms.PictureBox();
            this.GreenPicture = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.FilteringTabPage = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmberPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenPicture)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.FilteringTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // StopCaptureButton
            // 
            this.StopCaptureButton.Location = new System.Drawing.Point(376, 3);
            this.StopCaptureButton.Name = "StopCaptureButton";
            this.StopCaptureButton.Size = new System.Drawing.Size(89, 23);
            this.StopCaptureButton.TabIndex = 1;
            this.StopCaptureButton.Text = "Stop Capture";
            this.StopCaptureButton.UseVisualStyleBackColor = true;
            this.StopCaptureButton.Click += new System.EventHandler(this.StopCaptureButton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(0, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(224, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // hexBox1
            // 
            this.hexBox1.BytesPerLine = 8;
            this.hexBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexBox1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexBox1.LineInfoForeColor = System.Drawing.Color.Empty;
            this.hexBox1.LineInfoVisible = true;
            this.hexBox1.Location = new System.Drawing.Point(3, 3);
            this.hexBox1.Name = "hexBox1";
            this.hexBox1.ReadOnly = true;
            this.hexBox1.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox1.Size = new System.Drawing.Size(548, 325);
            this.hexBox1.StringViewVisible = true;
            this.hexBox1.TabIndex = 2;
            this.hexBox1.UseFixedBytesPerLine = true;
            this.hexBox1.VScrollBarVisible = true;
            // 
            // CaptureButton
            // 
            this.CaptureButton.Location = new System.Drawing.Point(281, 3);
            this.CaptureButton.Name = "CaptureButton";
            this.CaptureButton.Size = new System.Drawing.Size(89, 23);
            this.CaptureButton.TabIndex = 0;
            this.CaptureButton.Text = "Start Capture";
            this.CaptureButton.UseVisualStyleBackColor = true;
            this.CaptureButton.Click += new System.EventHandler(this.CaptureButton_Click);
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 30);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(562, 147);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 21);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(224, 513);
            this.propertyGrid1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.FilterTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.AmberPicture);
            this.panel1.Controls.Add(this.RedPicture);
            this.panel1.Controls.Add(this.GreenPicture);
            this.panel1.Controls.Add(this.StopCaptureButton);
            this.panel1.Controls.Add(this.CaptureButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(562, 30);
            this.panel1.TabIndex = 0;
            // 
            // FilterTextBox
            // 
            this.FilterTextBox.Location = new System.Drawing.Point(43, 4);
            this.FilterTextBox.Name = "FilterTextBox";
            this.FilterTextBox.Size = new System.Drawing.Size(232, 20);
            this.FilterTextBox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Filter:";
            // 
            // AmberPicture
            // 
            this.AmberPicture.Image = global::Terminals.Properties.Resources.amber;
            this.AmberPicture.Location = new System.Drawing.Point(471, 3);
            this.AmberPicture.Name = "AmberPicture";
            this.AmberPicture.Size = new System.Drawing.Size(24, 24);
            this.AmberPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AmberPicture.TabIndex = 2;
            this.AmberPicture.TabStop = false;
            // 
            // RedPicture
            // 
            this.RedPicture.Image = global::Terminals.Properties.Resources.red;
            this.RedPicture.Location = new System.Drawing.Point(531, 3);
            this.RedPicture.Name = "RedPicture";
            this.RedPicture.Size = new System.Drawing.Size(24, 24);
            this.RedPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.RedPicture.TabIndex = 4;
            this.RedPicture.TabStop = false;
            // 
            // GreenPicture
            // 
            this.GreenPicture.Image = global::Terminals.Properties.Resources.green;
            this.GreenPicture.Location = new System.Drawing.Point(501, 3);
            this.GreenPicture.Name = "GreenPicture";
            this.GreenPicture.Size = new System.Drawing.Size(24, 24);
            this.GreenPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GreenPicture.TabIndex = 5;
            this.GreenPicture.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(548, 325);
            this.textBox1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.propertyGrid1);
            this.splitContainer1.Panel1.Controls.Add(this.comboBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.listBox1);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(790, 534);
            this.splitContainer1.SplitterDistance = 224;
            this.splitContainer1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.FilteringTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 177);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(562, 357);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(554, 331);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Text View";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.hexBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(554, 331);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Hex View";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FilteringTabPage
            // 
            this.FilteringTabPage.Controls.Add(this.webBrowser1);
            this.FilteringTabPage.Location = new System.Drawing.Point(4, 22);
            this.FilteringTabPage.Name = "FilteringTabPage";
            this.FilteringTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.FilteringTabPage.Size = new System.Drawing.Size(554, 331);
            this.FilteringTabPage.TabIndex = 2;
            this.FilteringTabPage.Text = "Filtering Help";
            this.FilteringTabPage.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowNavigation = false;
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(3, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(548, 325);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            // 
            // PacketCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "PacketCapture";
            this.Size = new System.Drawing.Size(790, 534);
            this.Load += new System.EventHandler(this.PacketCapture_Load);
            this.Resize += new System.EventHandler(this.PacketCapture_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmberPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenPicture)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.FilteringTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StopCaptureButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private Be.Windows.Forms.HexBox hexBox1;
        private System.Windows.Forms.Button CaptureButton;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox AmberPicture;
        private System.Windows.Forms.PictureBox RedPicture;
        private System.Windows.Forms.PictureBox GreenPicture;
        private System.Windows.Forms.TextBox FilterTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage FilteringTabPage;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}
