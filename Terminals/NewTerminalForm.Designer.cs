namespace Terminals
{
  partial class NewTerminalForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewTerminalForm));
        this.btnOk = new System.Windows.Forms.Button();
        this.btnCancel = new System.Windows.Forms.Button();
        this.tabControl1 = new System.Windows.Forms.TabControl();
        this.tabPage1 = new System.Windows.Forms.TabPage();
        this.chkSavePassword = new System.Windows.Forms.CheckBox();
        this.txtName = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.txtPassword = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.cmbUsers = new System.Windows.Forms.ComboBox();
        this.label3 = new System.Windows.Forms.Label();
        this.cmbServers = new System.Windows.Forms.ComboBox();
        this.label2 = new System.Windows.Forms.Label();
        this.cmbDomains = new System.Windows.Forms.ComboBox();
        this.label1 = new System.Windows.Forms.Label();
        this.tabPage2 = new System.Windows.Forms.TabPage();
        this.txtPort = new System.Windows.Forms.TextBox();
        this.lblPort = new System.Windows.Forms.Label();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.chkAllowDesktopBG = new System.Windows.Forms.CheckBox();
        this.cmbColors = new System.Windows.Forms.ComboBox();
        this.label7 = new System.Windows.Forms.Label();
        this.cmbResolution = new System.Windows.Forms.ComboBox();
        this.label6 = new System.Windows.Forms.Label();
        this.chkConnectToConsole = new System.Windows.Forms.CheckBox();
        this.tabPage3 = new System.Windows.Forms.TabPage();
        this.chkRedirectSmartcards = new System.Windows.Forms.CheckBox();
        this.chkRedirectClipboard = new System.Windows.Forms.CheckBox();
        this.chkRedirectDevices = new System.Windows.Forms.CheckBox();
        this.btnBrowseShare = new System.Windows.Forms.Button();
        this.txtDesktopShare = new System.Windows.Forms.TextBox();
        this.label10 = new System.Windows.Forms.Label();
        this.chkSerialPorts = new System.Windows.Forms.CheckBox();
        this.chkPrinters = new System.Windows.Forms.CheckBox();
        this.chkDrives = new System.Windows.Forms.CheckBox();
        this.label9 = new System.Windows.Forms.Label();
        this.cmbSounds = new System.Windows.Forms.ComboBox();
        this.label8 = new System.Windows.Forms.Label();
        this.tabPage4 = new System.Windows.Forms.TabPage();
        this.txtInitialDirectory = new System.Windows.Forms.TextBox();
        this.label13 = new System.Windows.Forms.Label();
        this.chkExecuteBeforeConnect = new System.Windows.Forms.CheckBox();
        this.txtArguments = new System.Windows.Forms.TextBox();
        this.label12 = new System.Windows.Forms.Label();
        this.chkWaitForExit = new System.Windows.Forms.CheckBox();
        this.txtCommand = new System.Windows.Forms.TextBox();
        this.label11 = new System.Windows.Forms.Label();
        this.tpTags = new System.Windows.Forms.TabPage();
        this.btnAddNewTag = new System.Windows.Forms.Button();
        this.txtTag = new System.Windows.Forms.TextBox();
        this.label14 = new System.Windows.Forms.Label();
        this.panel1 = new System.Windows.Forms.Panel();
        this.groupBox3 = new System.Windows.Forms.GroupBox();
        this.btnRemoveTag = new System.Windows.Forms.Button();
        this.lvConnectionTags = new System.Windows.Forms.ListView();
        this.chkAddtoToolbar = new System.Windows.Forms.CheckBox();
        this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
        this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.tabControl1.SuspendLayout();
        this.tabPage1.SuspendLayout();
        this.tabPage2.SuspendLayout();
        this.groupBox1.SuspendLayout();
        this.tabPage3.SuspendLayout();
        this.tabPage4.SuspendLayout();
        this.tpTags.SuspendLayout();
        this.panel1.SuspendLayout();
        this.groupBox3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        this.SuspendLayout();
        // 
        // btnOk
        // 
        this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnOk.Enabled = false;
        this.btnOk.Location = new System.Drawing.Point(249, 298);
        this.btnOk.Name = "btnOk";
        this.btnOk.Size = new System.Drawing.Size(72, 24);
        this.btnOk.TabIndex = 1;
        this.btnOk.Text = "Co&nnect";
        this.btnOk.UseVisualStyleBackColor = true;
        this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
        // 
        // btnCancel
        // 
        this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.btnCancel.Location = new System.Drawing.Point(327, 298);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new System.Drawing.Size(72, 24);
        this.btnCancel.TabIndex = 2;
        this.btnCancel.Text = "Cancel";
        this.btnCancel.UseVisualStyleBackColor = true;
        // 
        // tabControl1
        // 
        this.tabControl1.Controls.Add(this.tabPage1);
        this.tabControl1.Controls.Add(this.tabPage2);
        this.tabControl1.Controls.Add(this.tabPage3);
        this.tabControl1.Controls.Add(this.tabPage4);
        this.tabControl1.Controls.Add(this.tpTags);
        this.tabControl1.Location = new System.Drawing.Point(6, 77);
        this.tabControl1.Name = "tabControl1";
        this.tabControl1.SelectedIndex = 0;
        this.tabControl1.Size = new System.Drawing.Size(394, 209);
        this.tabControl1.TabIndex = 0;
        // 
        // tabPage1
        // 
        this.tabPage1.Controls.Add(this.chkSavePassword);
        this.tabPage1.Controls.Add(this.txtName);
        this.tabPage1.Controls.Add(this.label5);
        this.tabPage1.Controls.Add(this.txtPassword);
        this.tabPage1.Controls.Add(this.label4);
        this.tabPage1.Controls.Add(this.cmbUsers);
        this.tabPage1.Controls.Add(this.label3);
        this.tabPage1.Controls.Add(this.cmbServers);
        this.tabPage1.Controls.Add(this.label2);
        this.tabPage1.Controls.Add(this.cmbDomains);
        this.tabPage1.Controls.Add(this.label1);
        this.tabPage1.Location = new System.Drawing.Point(4, 22);
        this.tabPage1.Name = "tabPage1";
        this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage1.Size = new System.Drawing.Size(386, 183);
        this.tabPage1.TabIndex = 0;
        this.tabPage1.Text = "General";
        this.tabPage1.UseVisualStyleBackColor = true;
        // 
        // chkSavePassword
        // 
        this.chkSavePassword.AutoSize = true;
        this.chkSavePassword.Location = new System.Drawing.Point(110, 142);
        this.chkSavePassword.Name = "chkSavePassword";
        this.chkSavePassword.Size = new System.Drawing.Size(99, 17);
        this.chkSavePassword.TabIndex = 10;
        this.chkSavePassword.Text = "S&ave password";
        this.chkSavePassword.UseVisualStyleBackColor = true;
        this.chkSavePassword.CheckedChanged += new System.EventHandler(this.chkSavePassword_CheckedChanged);
        // 
        // txtName
        // 
        this.txtName.Location = new System.Drawing.Point(110, 36);
        this.txtName.Name = "txtName";
        this.txtName.Size = new System.Drawing.Size(265, 21);
        this.txtName.TabIndex = 3;
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(11, 39);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(94, 13);
        this.label5.TabIndex = 2;
        this.label5.Text = "Connection na&me:";
        // 
        // txtPassword
        // 
        this.txtPassword.Location = new System.Drawing.Point(110, 116);
        this.txtPassword.Name = "txtPassword";
        this.txtPassword.PasswordChar = '*';
        this.txtPassword.Size = new System.Drawing.Size(265, 21);
        this.txtPassword.TabIndex = 9;
        this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(11, 119);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(57, 13);
        this.label4.TabIndex = 8;
        this.label4.Text = "&Password:";
        // 
        // cmbUsers
        // 
        this.cmbUsers.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
        this.cmbUsers.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.cmbUsers.Location = new System.Drawing.Point(110, 89);
        this.cmbUsers.Name = "cmbUsers";
        this.cmbUsers.Size = new System.Drawing.Size(265, 21);
        this.cmbUsers.TabIndex = 7;
        this.cmbUsers.TextChanged += new System.EventHandler(this.control_TextChanged);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(11, 92);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(62, 13);
        this.label3.TabIndex = 6;
        this.label3.Text = "&User name:";
        // 
        // cmbServers
        // 
        this.cmbServers.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
        this.cmbServers.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.cmbServers.Location = new System.Drawing.Point(110, 9);
        this.cmbServers.Name = "cmbServers";
        this.cmbServers.Size = new System.Drawing.Size(265, 21);
        this.cmbServers.TabIndex = 1;
        this.cmbServers.Leave += new System.EventHandler(this.cmbServers_Leave);
        this.cmbServers.TextChanged += new System.EventHandler(this.cmbServers_TextChanged);
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(11, 12);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(54, 13);
        this.label2.TabIndex = 0;
        this.label2.Text = "&Computer";
        // 
        // cmbDomains
        // 
        this.cmbDomains.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
        this.cmbDomains.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.cmbDomains.Location = new System.Drawing.Point(110, 62);
        this.cmbDomains.Name = "cmbDomains";
        this.cmbDomains.Size = new System.Drawing.Size(265, 21);
        this.cmbDomains.TabIndex = 5;
        this.cmbDomains.TextChanged += new System.EventHandler(this.control_TextChanged);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(11, 65);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(46, 13);
        this.label1.TabIndex = 4;
        this.label1.Text = "&Domain:";
        // 
        // tabPage2
        // 
        this.tabPage2.Controls.Add(this.txtPort);
        this.tabPage2.Controls.Add(this.lblPort);
        this.tabPage2.Controls.Add(this.groupBox1);
        this.tabPage2.Controls.Add(this.chkConnectToConsole);
        this.tabPage2.Location = new System.Drawing.Point(4, 22);
        this.tabPage2.Name = "tabPage2";
        this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage2.Size = new System.Drawing.Size(386, 183);
        this.tabPage2.TabIndex = 1;
        this.tabPage2.Text = "Advanced";
        this.tabPage2.UseVisualStyleBackColor = true;
        // 
        // txtPort
        // 
        this.txtPort.Location = new System.Drawing.Point(130, 123);
        this.txtPort.Name = "txtPort";
        this.txtPort.Size = new System.Drawing.Size(234, 21);
        this.txtPort.TabIndex = 2;
        // 
        // lblPort
        // 
        this.lblPort.AutoSize = true;
        this.lblPort.Location = new System.Drawing.Point(8, 126);
        this.lblPort.Name = "lblPort";
        this.lblPort.Size = new System.Drawing.Size(105, 13);
        this.lblPort.TabIndex = 1;
        this.lblPort.Text = "&Remote server port:";
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.chkAllowDesktopBG);
        this.groupBox1.Controls.Add(this.cmbColors);
        this.groupBox1.Controls.Add(this.label7);
        this.groupBox1.Controls.Add(this.cmbResolution);
        this.groupBox1.Controls.Add(this.label6);
        this.groupBox1.Location = new System.Drawing.Point(6, 6);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(374, 102);
        this.groupBox1.TabIndex = 0;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Display settings";
        // 
        // chkAllowDesktopBG
        // 
        this.chkAllowDesktopBG.AutoSize = true;
        this.chkAllowDesktopBG.Location = new System.Drawing.Point(124, 77);
        this.chkAllowDesktopBG.Name = "chkAllowDesktopBG";
        this.chkAllowDesktopBG.Size = new System.Drawing.Size(151, 17);
        this.chkAllowDesktopBG.TabIndex = 4;
        this.chkAllowDesktopBG.Text = "Allow desktop &background";
        this.chkAllowDesktopBG.UseVisualStyleBackColor = true;
        // 
        // cmbColors
        // 
        this.cmbColors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbColors.FormattingEnabled = true;
        this.cmbColors.Items.AddRange(new object[] {
            "256 Colors",
            "High Color (16 Bit)",
            "True Color (24 Bit)",
            "Highest Quality (32 Bit)"});
        this.cmbColors.Location = new System.Drawing.Point(124, 50);
        this.cmbColors.Name = "cmbColors";
        this.cmbColors.Size = new System.Drawing.Size(234, 21);
        this.cmbColors.TabIndex = 3;
        // 
        // label7
        // 
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(18, 53);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(37, 13);
        this.label7.TabIndex = 2;
        this.label7.Text = "Co&lors";
        // 
        // cmbResolution
        // 
        this.cmbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbResolution.FormattingEnabled = true;
        this.cmbResolution.Items.AddRange(new object[] {
            "640x480",
            "800x600",
            "1024x768",
            "Fit to Window",
            "Full Screen",
            "Auto Scale"});
        this.cmbResolution.Location = new System.Drawing.Point(124, 23);
        this.cmbResolution.Name = "cmbResolution";
        this.cmbResolution.Size = new System.Drawing.Size(234, 21);
        this.cmbResolution.TabIndex = 1;
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(18, 26);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(71, 13);
        this.label6.TabIndex = 0;
        this.label6.Text = "&Desktop size:";
        // 
        // chkConnectToConsole
        // 
        this.chkConnectToConsole.AutoSize = true;
        this.chkConnectToConsole.Location = new System.Drawing.Point(11, 151);
        this.chkConnectToConsole.Name = "chkConnectToConsole";
        this.chkConnectToConsole.Size = new System.Drawing.Size(120, 17);
        this.chkConnectToConsole.TabIndex = 3;
        this.chkConnectToConsole.Text = "Co&nnect to Console";
        this.chkConnectToConsole.UseVisualStyleBackColor = true;
        // 
        // tabPage3
        // 
        this.tabPage3.Controls.Add(this.chkRedirectSmartcards);
        this.tabPage3.Controls.Add(this.chkRedirectClipboard);
        this.tabPage3.Controls.Add(this.chkRedirectDevices);
        this.tabPage3.Controls.Add(this.btnBrowseShare);
        this.tabPage3.Controls.Add(this.txtDesktopShare);
        this.tabPage3.Controls.Add(this.label10);
        this.tabPage3.Controls.Add(this.chkSerialPorts);
        this.tabPage3.Controls.Add(this.chkPrinters);
        this.tabPage3.Controls.Add(this.chkDrives);
        this.tabPage3.Controls.Add(this.label9);
        this.tabPage3.Controls.Add(this.cmbSounds);
        this.tabPage3.Controls.Add(this.label8);
        this.tabPage3.Location = new System.Drawing.Point(4, 22);
        this.tabPage3.Name = "tabPage3";
        this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage3.Size = new System.Drawing.Size(386, 183);
        this.tabPage3.TabIndex = 2;
        this.tabPage3.Text = "Local Resources";
        this.tabPage3.UseVisualStyleBackColor = true;
        // 
        // chkRedirectSmartcards
        // 
        this.chkRedirectSmartcards.AutoSize = true;
        this.chkRedirectSmartcards.Location = new System.Drawing.Point(160, 120);
        this.chkRedirectSmartcards.Name = "chkRedirectSmartcards";
        this.chkRedirectSmartcards.Size = new System.Drawing.Size(126, 17);
        this.chkRedirectSmartcards.TabIndex = 8;
        this.chkRedirectSmartcards.Text = "Redirect Smart ca&rds";
        this.chkRedirectSmartcards.UseVisualStyleBackColor = true;
        // 
        // chkRedirectClipboard
        // 
        this.chkRedirectClipboard.AutoSize = true;
        this.chkRedirectClipboard.Checked = true;
        this.chkRedirectClipboard.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkRedirectClipboard.Location = new System.Drawing.Point(160, 97);
        this.chkRedirectClipboard.Name = "chkRedirectClipboard";
        this.chkRedirectClipboard.Size = new System.Drawing.Size(114, 17);
        this.chkRedirectClipboard.TabIndex = 7;
        this.chkRedirectClipboard.Text = "Redirect &Clipboard";
        this.chkRedirectClipboard.UseVisualStyleBackColor = true;
        // 
        // chkRedirectDevices
        // 
        this.chkRedirectDevices.AutoSize = true;
        this.chkRedirectDevices.Location = new System.Drawing.Point(160, 74);
        this.chkRedirectDevices.Name = "chkRedirectDevices";
        this.chkRedirectDevices.Size = new System.Drawing.Size(129, 17);
        this.chkRedirectDevices.TabIndex = 6;
        this.chkRedirectDevices.Text = "Plu&g and Play devices";
        this.chkRedirectDevices.UseVisualStyleBackColor = true;
        // 
        // btnBrowseShare
        // 
        this.btnBrowseShare.Image = global::Terminals.Properties.Resources.folder;
        this.btnBrowseShare.Location = new System.Drawing.Point(352, 149);
        this.btnBrowseShare.Name = "btnBrowseShare";
        this.btnBrowseShare.Size = new System.Drawing.Size(21, 21);
        this.btnBrowseShare.TabIndex = 11;
        this.btnBrowseShare.UseVisualStyleBackColor = true;
        this.btnBrowseShare.Click += new System.EventHandler(this.btnBrowseShare_Click);
        // 
        // txtDesktopShare
        // 
        this.txtDesktopShare.Location = new System.Drawing.Point(104, 149);
        this.txtDesktopShare.Name = "txtDesktopShare";
        this.txtDesktopShare.Size = new System.Drawing.Size(248, 21);
        this.txtDesktopShare.TabIndex = 10;
        this.toolTip1.SetToolTip(this.txtDesktopShare, "Enter a share on the server where files will be copied\r\nto when draging files fro" +
                "m your computer to the\r\nterminal window.");
        // 
        // label10
        // 
        this.label10.AutoSize = true;
        this.label10.Location = new System.Drawing.Point(11, 153);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(81, 13);
        this.label10.TabIndex = 9;
        this.label10.Text = "Desktop S&hare:";
        this.toolTip1.SetToolTip(this.label10, "Enter a share on the server where files will be copied\r\nto when draging files fro" +
                "m your computer to the\r\nterminal window.");
        // 
        // chkSerialPorts
        // 
        this.chkSerialPorts.AutoSize = true;
        this.chkSerialPorts.Location = new System.Drawing.Point(30, 120);
        this.chkSerialPorts.Name = "chkSerialPorts";
        this.chkSerialPorts.Size = new System.Drawing.Size(80, 17);
        this.chkSerialPorts.TabIndex = 5;
        this.chkSerialPorts.Text = "Seria&l ports";
        this.chkSerialPorts.UseVisualStyleBackColor = true;
        // 
        // chkPrinters
        // 
        this.chkPrinters.AutoSize = true;
        this.chkPrinters.Location = new System.Drawing.Point(30, 97);
        this.chkPrinters.Name = "chkPrinters";
        this.chkPrinters.Size = new System.Drawing.Size(63, 17);
        this.chkPrinters.TabIndex = 4;
        this.chkPrinters.Text = "&Printers";
        this.chkPrinters.UseVisualStyleBackColor = true;
        // 
        // chkDrives
        // 
        this.chkDrives.AutoSize = true;
        this.chkDrives.Location = new System.Drawing.Point(30, 74);
        this.chkDrives.Name = "chkDrives";
        this.chkDrives.Size = new System.Drawing.Size(77, 17);
        this.chkDrives.TabIndex = 3;
        this.chkDrives.Text = "&Disk drives";
        this.chkDrives.UseVisualStyleBackColor = true;
        // 
        // label9
        // 
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(11, 52);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(225, 13);
        this.label9.TabIndex = 2;
        this.label9.Text = "Automatically connect to these local devices :";
        // 
        // cmbSounds
        // 
        this.cmbSounds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbSounds.FormattingEnabled = true;
        this.cmbSounds.Items.AddRange(new object[] {
            "Play on remote computer",
            "Play on this computer",
            "Do not play"});
        this.cmbSounds.Location = new System.Drawing.Point(102, 12);
        this.cmbSounds.Name = "cmbSounds";
        this.cmbSounds.Size = new System.Drawing.Size(172, 21);
        this.cmbSounds.TabIndex = 1;
        // 
        // label8
        // 
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(11, 15);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(85, 13);
        this.label8.TabIndex = 0;
        this.label8.Text = "Remote &sounds:";
        // 
        // tabPage4
        // 
        this.tabPage4.Controls.Add(this.txtInitialDirectory);
        this.tabPage4.Controls.Add(this.label13);
        this.tabPage4.Controls.Add(this.chkExecuteBeforeConnect);
        this.tabPage4.Controls.Add(this.txtArguments);
        this.tabPage4.Controls.Add(this.label12);
        this.tabPage4.Controls.Add(this.chkWaitForExit);
        this.tabPage4.Controls.Add(this.txtCommand);
        this.tabPage4.Controls.Add(this.label11);
        this.tabPage4.Location = new System.Drawing.Point(4, 22);
        this.tabPage4.Name = "tabPage4";
        this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage4.Size = new System.Drawing.Size(386, 183);
        this.tabPage4.TabIndex = 3;
        this.tabPage4.Text = "Execute Before Connect";
        this.tabPage4.UseVisualStyleBackColor = true;
        // 
        // txtInitialDirectory
        // 
        this.txtInitialDirectory.Location = new System.Drawing.Point(104, 88);
        this.txtInitialDirectory.Name = "txtInitialDirectory";
        this.txtInitialDirectory.Size = new System.Drawing.Size(265, 21);
        this.txtInitialDirectory.TabIndex = 3;
        // 
        // label13
        // 
        this.label13.AutoSize = true;
        this.label13.Location = new System.Drawing.Point(8, 88);
        this.label13.Name = "label13";
        this.label13.Size = new System.Drawing.Size(84, 13);
        this.label13.TabIndex = 10;
        this.label13.Text = "Initial Directory:";
        // 
        // chkExecuteBeforeConnect
        // 
        this.chkExecuteBeforeConnect.AutoSize = true;
        this.chkExecuteBeforeConnect.Location = new System.Drawing.Point(8, 8);
        this.chkExecuteBeforeConnect.Name = "chkExecuteBeforeConnect";
        this.chkExecuteBeforeConnect.Size = new System.Drawing.Size(141, 17);
        this.chkExecuteBeforeConnect.TabIndex = 0;
        this.chkExecuteBeforeConnect.Text = "&Execute before connect";
        this.chkExecuteBeforeConnect.UseVisualStyleBackColor = true;
        // 
        // txtArguments
        // 
        this.txtArguments.Location = new System.Drawing.Point(104, 64);
        this.txtArguments.Name = "txtArguments";
        this.txtArguments.Size = new System.Drawing.Size(265, 21);
        this.txtArguments.TabIndex = 2;
        // 
        // label12
        // 
        this.label12.AutoSize = true;
        this.label12.Location = new System.Drawing.Point(8, 64);
        this.label12.Name = "label12";
        this.label12.Size = new System.Drawing.Size(59, 13);
        this.label12.TabIndex = 7;
        this.label12.Text = "Arguments";
        // 
        // chkWaitForExit
        // 
        this.chkWaitForExit.AutoSize = true;
        this.chkWaitForExit.Location = new System.Drawing.Point(8, 120);
        this.chkWaitForExit.Name = "chkWaitForExit";
        this.chkWaitForExit.Size = new System.Drawing.Size(86, 17);
        this.chkWaitForExit.TabIndex = 4;
        this.chkWaitForExit.Text = "&Wait for exit";
        this.chkWaitForExit.UseVisualStyleBackColor = true;
        // 
        // txtCommand
        // 
        this.txtCommand.Location = new System.Drawing.Point(104, 40);
        this.txtCommand.Name = "txtCommand";
        this.txtCommand.Size = new System.Drawing.Size(265, 21);
        this.txtCommand.TabIndex = 1;
        // 
        // label11
        // 
        this.label11.AutoSize = true;
        this.label11.Location = new System.Drawing.Point(8, 40);
        this.label11.Name = "label11";
        this.label11.Size = new System.Drawing.Size(58, 13);
        this.label11.TabIndex = 4;
        this.label11.Text = "Command:";
        // 
        // tpTags
        // 
        this.tpTags.Controls.Add(this.btnAddNewTag);
        this.tpTags.Controls.Add(this.txtTag);
        this.tpTags.Controls.Add(this.label14);
        this.tpTags.Controls.Add(this.panel1);
        this.tpTags.Location = new System.Drawing.Point(4, 22);
        this.tpTags.Name = "tpTags";
        this.tpTags.Padding = new System.Windows.Forms.Padding(3);
        this.tpTags.Size = new System.Drawing.Size(386, 183);
        this.tpTags.TabIndex = 4;
        this.tpTags.Text = "Tags";
        this.tpTags.UseVisualStyleBackColor = true;
        // 
        // btnAddNewTag
        // 
        this.btnAddNewTag.Image = global::Terminals.Properties.Resources.tag_blue_add;
        this.btnAddNewTag.Location = new System.Drawing.Point(355, 8);
        this.btnAddNewTag.Name = "btnAddNewTag";
        this.btnAddNewTag.Size = new System.Drawing.Size(21, 21);
        this.btnAddNewTag.TabIndex = 12;
        this.btnAddNewTag.UseVisualStyleBackColor = true;
        this.btnAddNewTag.Click += new System.EventHandler(this.btnAddNewTag_Click);
        // 
        // txtTag
        // 
        this.txtTag.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        this.txtTag.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
        this.txtTag.Location = new System.Drawing.Point(64, 8);
        this.txtTag.Name = "txtTag";
        this.txtTag.Size = new System.Drawing.Size(288, 21);
        this.txtTag.TabIndex = 2;
        // 
        // label14
        // 
        this.label14.AutoSize = true;
        this.label14.Location = new System.Drawing.Point(8, 8);
        this.label14.Name = "label14";
        this.label14.Size = new System.Drawing.Size(53, 13);
        this.label14.TabIndex = 1;
        this.label14.Text = "New Tag:";
        // 
        // panel1
        // 
        this.panel1.Controls.Add(this.groupBox3);
        this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.panel1.Location = new System.Drawing.Point(3, 40);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(380, 140);
        this.panel1.TabIndex = 0;
        // 
        // groupBox3
        // 
        this.groupBox3.Controls.Add(this.btnRemoveTag);
        this.groupBox3.Controls.Add(this.lvConnectionTags);
        this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
        this.groupBox3.Location = new System.Drawing.Point(0, 0);
        this.groupBox3.Name = "groupBox3";
        this.groupBox3.Size = new System.Drawing.Size(380, 140);
        this.groupBox3.TabIndex = 1;
        this.groupBox3.TabStop = false;
        this.groupBox3.Text = "Connection Tags";
        // 
        // btnRemoveTag
        // 
        this.btnRemoveTag.Image = global::Terminals.Properties.Resources.tag_blue_delete;
        this.btnRemoveTag.Location = new System.Drawing.Point(352, 24);
        this.btnRemoveTag.Name = "btnRemoveTag";
        this.btnRemoveTag.Size = new System.Drawing.Size(21, 21);
        this.btnRemoveTag.TabIndex = 13;
        this.btnRemoveTag.UseVisualStyleBackColor = true;
        this.btnRemoveTag.Click += new System.EventHandler(this.btnRemoveTag_Click);
        // 
        // lvConnectionTags
        // 
        this.lvConnectionTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.lvConnectionTags.HideSelection = false;
        this.lvConnectionTags.Location = new System.Drawing.Point(8, 24);
        this.lvConnectionTags.Name = "lvConnectionTags";
        this.lvConnectionTags.Size = new System.Drawing.Size(344, 104);
        this.lvConnectionTags.TabIndex = 1;
        this.lvConnectionTags.UseCompatibleStateImageBehavior = false;
        // 
        // chkAddtoToolbar
        // 
        this.chkAddtoToolbar.AutoSize = true;
        this.chkAddtoToolbar.Location = new System.Drawing.Point(10, 303);
        this.chkAddtoToolbar.Name = "chkAddtoToolbar";
        this.chkAddtoToolbar.Size = new System.Drawing.Size(97, 17);
        this.chkAddtoToolbar.TabIndex = 10;
        this.chkAddtoToolbar.Text = "Add to &Toolbar";
        this.chkAddtoToolbar.UseVisualStyleBackColor = true;
        // 
        // folderBrowserDialog
        // 
        this.folderBrowserDialog.Description = "Select Desktop Share:";
        this.folderBrowserDialog.ShowNewFolderButton = false;
        // 
        // pictureBox1
        // 
        this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
        this.pictureBox1.Image = global::Terminals.Properties.Resources.rdp;
        this.pictureBox1.Location = new System.Drawing.Point(0, 0);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(407, 67);
        this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
        this.pictureBox1.TabIndex = 9;
        this.pictureBox1.TabStop = false;
        // 
        // NewTerminalForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(407, 334);
        this.Controls.Add(this.chkAddtoToolbar);
        this.Controls.Add(this.tabControl1);
        this.Controls.Add(this.pictureBox1);
        this.Controls.Add(this.btnCancel);
        this.Controls.Add(this.btnOk);
        this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "NewTerminalForm";
        this.ShowIcon = false;
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "New Connection";
        this.Shown += new System.EventHandler(this.NewTerminalForm_Shown);
        this.tabControl1.ResumeLayout(false);
        this.tabPage1.ResumeLayout(false);
        this.tabPage1.PerformLayout();
        this.tabPage2.ResumeLayout(false);
        this.tabPage2.PerformLayout();
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.tabPage3.ResumeLayout(false);
        this.tabPage3.PerformLayout();
        this.tabPage4.ResumeLayout(false);
        this.tabPage4.PerformLayout();
        this.tpTags.ResumeLayout(false);
        this.tpTags.PerformLayout();
        this.panel1.ResumeLayout(false);
        this.groupBox3.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOk;
      private System.Windows.Forms.Button btnCancel;
      private System.Windows.Forms.PictureBox pictureBox1;
      private System.Windows.Forms.TabControl tabControl1;
      private System.Windows.Forms.TabPage tabPage1;
      private System.Windows.Forms.TextBox txtPassword;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TabPage tabPage2;
      private System.Windows.Forms.TextBox txtName;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.CheckBox chkSavePassword;
      private System.Windows.Forms.CheckBox chkConnectToConsole;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.ComboBox cmbColors;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.ComboBox cmbResolution;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.ComboBox cmbUsers;
      private System.Windows.Forms.ComboBox cmbServers;
      private System.Windows.Forms.ComboBox cmbDomains;
      private System.Windows.Forms.CheckBox chkAddtoToolbar;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.CheckBox chkDrives;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.ComboBox cmbSounds;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.CheckBox chkSerialPorts;
    private System.Windows.Forms.CheckBox chkPrinters;
      private System.Windows.Forms.TextBox txtPort;
      private System.Windows.Forms.Label lblPort;
      private System.Windows.Forms.TextBox txtDesktopShare;
      private System.Windows.Forms.Label label10;
      private System.Windows.Forms.Button btnBrowseShare;
      private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
      private System.Windows.Forms.ToolTip toolTip1;
      private System.Windows.Forms.TabPage tabPage4;
      private System.Windows.Forms.TextBox txtCommand;
      private System.Windows.Forms.Label label11;
      private System.Windows.Forms.CheckBox chkWaitForExit;
      private System.Windows.Forms.TextBox txtArguments;
      private System.Windows.Forms.Label label12;
      private System.Windows.Forms.CheckBox chkExecuteBeforeConnect;
      private System.Windows.Forms.TextBox txtInitialDirectory;
      private System.Windows.Forms.Label label13;
      private System.Windows.Forms.CheckBox chkRedirectDevices;
      private System.Windows.Forms.CheckBox chkRedirectClipboard;
      private System.Windows.Forms.CheckBox chkRedirectSmartcards;
      private System.Windows.Forms.CheckBox chkAllowDesktopBG;
      private System.Windows.Forms.TabPage tpTags;
      private System.Windows.Forms.Label label14;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.GroupBox groupBox3;
      private System.Windows.Forms.Button btnAddNewTag;
      private System.Windows.Forms.ListView lvConnectionTags;
      private System.Windows.Forms.Button btnRemoveTag;
      private System.Windows.Forms.TextBox txtTag;
  }
}