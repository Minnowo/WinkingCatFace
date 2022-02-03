namespace WinkingCat
{
    partial class ApplicationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationForm));
            this.niTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFullscreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLastRegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCaptureCursorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClipsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewClipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClipFromClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClipFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayScreenColorPicker = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayColorWheel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayHashCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayQrCodeScan = new System.Windows.Forms.ToolStripMenuItem();
            this.tssToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiStylesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tssToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiOpenMainWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.folderView1 = new WinkingCat.Controls.FolderView();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.imageDisplay1 = new WinkingCat.HelperLibs.Controls.ImageDisplay();
            this.tsMain = new WinkingCat.HelperLibs.ToolStripEx();
            this.tsddbToolStripDropDownButton_Capture = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiToolStripMenuItem_region = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiToolStripMenuItem_monitor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiToolStripMenuItem_window = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiToolStripMenuItem_fullscreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiToolStripMenuItem_lastRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiToolStripMenuItem_captureCursor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddbToolStripDropDownButton_Clips = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiToolStripMenuItem_newClip = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiToolStripMenuItem_clipFromClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiToolStripMenuItem_clipFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddbToolStripDropDownButton_Tools = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiToolStripDropDownButton_screenColorPicker = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiToolStripDropDownButton_ColorPicker = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiToolStripDropDownButton_HashCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiToolStripDropDownButton_QrCode = new System.Windows.Forms.ToolStripMenuItem();
            this.oCRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiToolStripDropDownButton_Regex = new System.Windows.Forms.ToolStripMenuItem();
            this.tssToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbToolStripButton_Style = new System.Windows.Forms.ToolStripButton();
            this.tsbToolStripDropDownButton_Settings = new System.Windows.Forms.ToolStripButton();
            this.imageDisplayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useBackColor1OnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setBackColor1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setBackColor2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetXYOffsetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.resetBackColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmTray.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.imageDisplayContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // niTrayIcon
            // 
            this.niTrayIcon.ContextMenuStrip = this.cmTray;
            this.niTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("niTrayIcon.Icon")));
            this.niTrayIcon.Text = ";3c";
            this.niTrayIcon.Visible = true;
            // 
            // cmTray
            // 
            this.cmTray.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCaptureToolStripMenuItem,
            this.tsmiClipsToolStripMenuItem,
            this.tsmiToolsToolStripMenuItem,
            this.tssToolStripSeparator2,
            this.tsmiStylesToolStripMenuItem,
            this.tsmiSettingsToolStripMenuItem,
            this.tssToolStripSeparator3,
            this.tsmiOpenMainWindowToolStripMenuItem,
            this.tsmiExitToolStripMenuItem});
            this.cmTray.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.cmTray.Name = "cmTray";
            this.cmTray.Size = new System.Drawing.Size(197, 282);
            // 
            // tsmiCaptureToolStripMenuItem
            // 
            this.tsmiCaptureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRegionToolStripMenuItem,
            this.tsmiMonitorToolStripMenuItem,
            this.tsmiWindowToolStripMenuItem,
            this.tsmiFullscreenToolStripMenuItem,
            this.tsmiLastRegionToolStripMenuItem,
            this.tsmiCaptureCursorToolStripMenuItem});
            this.tsmiCaptureToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.Camera_icon;
            this.tsmiCaptureToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiCaptureToolStripMenuItem.Name = "tsmiCaptureToolStripMenuItem";
            this.tsmiCaptureToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiCaptureToolStripMenuItem.Text = "Capture";
            // 
            // tsmiRegionToolStripMenuItem
            // 
            this.tsmiRegionToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.Region_icon;
            this.tsmiRegionToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiRegionToolStripMenuItem.Name = "tsmiRegionToolStripMenuItem";
            this.tsmiRegionToolStripMenuItem.Size = new System.Drawing.Size(167, 38);
            this.tsmiRegionToolStripMenuItem.Text = "Region";
            // 
            // tsmiMonitorToolStripMenuItem
            // 
            this.tsmiMonitorToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.monitor_icon;
            this.tsmiMonitorToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiMonitorToolStripMenuItem.Name = "tsmiMonitorToolStripMenuItem";
            this.tsmiMonitorToolStripMenuItem.Size = new System.Drawing.Size(167, 38);
            this.tsmiMonitorToolStripMenuItem.Text = "Monitor";
            // 
            // tsmiWindowToolStripMenuItem
            // 
            this.tsmiWindowToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.Aero_Window_Explorer_icon;
            this.tsmiWindowToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiWindowToolStripMenuItem.Name = "tsmiWindowToolStripMenuItem";
            this.tsmiWindowToolStripMenuItem.Size = new System.Drawing.Size(167, 38);
            this.tsmiWindowToolStripMenuItem.Text = "Window";
            // 
            // tsmiFullscreenToolStripMenuItem
            // 
            this.tsmiFullscreenToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.Fullscreen_icon;
            this.tsmiFullscreenToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiFullscreenToolStripMenuItem.Name = "tsmiFullscreenToolStripMenuItem";
            this.tsmiFullscreenToolStripMenuItem.Size = new System.Drawing.Size(167, 38);
            this.tsmiFullscreenToolStripMenuItem.Text = "Fullscreen";
            // 
            // tsmiLastRegionToolStripMenuItem
            // 
            this.tsmiLastRegionToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.lastRegion_icon;
            this.tsmiLastRegionToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiLastRegionToolStripMenuItem.Name = "tsmiLastRegionToolStripMenuItem";
            this.tsmiLastRegionToolStripMenuItem.Size = new System.Drawing.Size(167, 38);
            this.tsmiLastRegionToolStripMenuItem.Text = "LastRegion";
            // 
            // tsmiCaptureCursorToolStripMenuItem
            // 
            this.tsmiCaptureCursorToolStripMenuItem.CheckOnClick = true;
            this.tsmiCaptureCursorToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.cursor_icon;
            this.tsmiCaptureCursorToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiCaptureCursorToolStripMenuItem.Name = "tsmiCaptureCursorToolStripMenuItem";
            this.tsmiCaptureCursorToolStripMenuItem.Size = new System.Drawing.Size(167, 38);
            this.tsmiCaptureCursorToolStripMenuItem.Text = "CaptureCursor";
            // 
            // tsmiClipsToolStripMenuItem
            // 
            this.tsmiClipsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewClipToolStripMenuItem,
            this.tsmiClipFromClipboardToolStripMenuItem,
            this.tsmiClipFromFileToolStripMenuItem});
            this.tsmiClipsToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.Binder_Clip_icon;
            this.tsmiClipsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiClipsToolStripMenuItem.Name = "tsmiClipsToolStripMenuItem";
            this.tsmiClipsToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiClipsToolStripMenuItem.Text = "Clips";
            // 
            // tsmiNewClipToolStripMenuItem
            // 
            this.tsmiNewClipToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.new_document_icon;
            this.tsmiNewClipToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiNewClipToolStripMenuItem.Name = "tsmiNewClipToolStripMenuItem";
            this.tsmiNewClipToolStripMenuItem.Size = new System.Drawing.Size(191, 38);
            this.tsmiNewClipToolStripMenuItem.Text = "NewClip";
            // 
            // tsmiClipFromClipboardToolStripMenuItem
            // 
            this.tsmiClipFromClipboardToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.Clipboard_2_icon;
            this.tsmiClipFromClipboardToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiClipFromClipboardToolStripMenuItem.Name = "tsmiClipFromClipboardToolStripMenuItem";
            this.tsmiClipFromClipboardToolStripMenuItem.Size = new System.Drawing.Size(191, 38);
            this.tsmiClipFromClipboardToolStripMenuItem.Text = "ClipFromClipboard";
            // 
            // tsmiClipFromFileToolStripMenuItem
            // 
            this.tsmiClipFromFileToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.new_doc_icon;
            this.tsmiClipFromFileToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiClipFromFileToolStripMenuItem.Name = "tsmiClipFromFileToolStripMenuItem";
            this.tsmiClipFromFileToolStripMenuItem.Size = new System.Drawing.Size(191, 38);
            this.tsmiClipFromFileToolStripMenuItem.Text = "ClipFromFile";
            // 
            // tsmiToolsToolStripMenuItem
            // 
            this.tsmiToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTrayScreenColorPicker,
            this.tsmiTrayColorWheel,
            this.tsmiTrayHashCheck,
            this.tsmiTrayQrCodeScan});
            this.tsmiToolsToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.google_webmaster_tools_icon;
            this.tsmiToolsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiToolsToolStripMenuItem.Name = "tsmiToolsToolStripMenuItem";
            this.tsmiToolsToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiToolsToolStripMenuItem.Text = "Tools";
            // 
            // tsmiTrayScreenColorPicker
            // 
            this.tsmiTrayScreenColorPicker.Image = global::WinkingCat.Properties.Resources.color_picker_icon;
            this.tsmiTrayScreenColorPicker.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiTrayScreenColorPicker.Name = "tsmiTrayScreenColorPicker";
            this.tsmiTrayScreenColorPicker.Size = new System.Drawing.Size(192, 38);
            this.tsmiTrayScreenColorPicker.Text = "Screen Color Picker";
            // 
            // tsmiTrayColorWheel
            // 
            this.tsmiTrayColorWheel.Image = global::WinkingCat.Properties.Resources.color_wheel_icon;
            this.tsmiTrayColorWheel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiTrayColorWheel.Name = "tsmiTrayColorWheel";
            this.tsmiTrayColorWheel.Size = new System.Drawing.Size(192, 38);
            this.tsmiTrayColorWheel.Text = "Color Picker";
            // 
            // tsmiTrayHashCheck
            // 
            this.tsmiTrayHashCheck.Image = global::WinkingCat.Properties.Resources.hashcheck;
            this.tsmiTrayHashCheck.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiTrayHashCheck.Name = "tsmiTrayHashCheck";
            this.tsmiTrayHashCheck.Size = new System.Drawing.Size(192, 38);
            this.tsmiTrayHashCheck.Text = "Hash Check";
            // 
            // tsmiTrayQrCodeScan
            // 
            this.tsmiTrayQrCodeScan.Image = global::WinkingCat.Properties.Resources.qrCode;
            this.tsmiTrayQrCodeScan.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiTrayQrCodeScan.Name = "tsmiTrayQrCodeScan";
            this.tsmiTrayQrCodeScan.Size = new System.Drawing.Size(192, 38);
            this.tsmiTrayQrCodeScan.Text = "QrCode Scan";
            // 
            // tssToolStripSeparator2
            // 
            this.tssToolStripSeparator2.Name = "tssToolStripSeparator2";
            this.tssToolStripSeparator2.Size = new System.Drawing.Size(193, 6);
            // 
            // tsmiStylesToolStripMenuItem
            // 
            this.tsmiStylesToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.style_icon;
            this.tsmiStylesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiStylesToolStripMenuItem.Name = "tsmiStylesToolStripMenuItem";
            this.tsmiStylesToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiStylesToolStripMenuItem.Text = "Styles";
            // 
            // tsmiSettingsToolStripMenuItem
            // 
            this.tsmiSettingsToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.gear_in_icon;
            this.tsmiSettingsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiSettingsToolStripMenuItem.Name = "tsmiSettingsToolStripMenuItem";
            this.tsmiSettingsToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiSettingsToolStripMenuItem.Text = "Settings";
            // 
            // tssToolStripSeparator3
            // 
            this.tssToolStripSeparator3.Name = "tssToolStripSeparator3";
            this.tssToolStripSeparator3.Size = new System.Drawing.Size(193, 6);
            // 
            // tsmiOpenMainWindowToolStripMenuItem
            // 
            this.tsmiOpenMainWindowToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.Open_icon;
            this.tsmiOpenMainWindowToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiOpenMainWindowToolStripMenuItem.Name = "tsmiOpenMainWindowToolStripMenuItem";
            this.tsmiOpenMainWindowToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiOpenMainWindowToolStripMenuItem.Text = "Open Main Window";
            // 
            // tsmiExitToolStripMenuItem
            // 
            this.tsmiExitToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.Error_Symbol_icon;
            this.tsmiExitToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiExitToolStripMenuItem.Name = "tsmiExitToolStripMenuItem";
            this.tsmiExitToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiExitToolStripMenuItem.Text = "Exit";
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(184, 0);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.folderView1);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.button2);
            this.scMain.Panel2.Controls.Add(this.button1);
            this.scMain.Panel2.Controls.Add(this.textBox1);
            this.scMain.Panel2.Controls.Add(this.comboBox2);
            this.scMain.Panel2.Controls.Add(this.comboBox1);
            this.scMain.Panel2.Controls.Add(this.imageDisplay1);
            this.scMain.Size = new System.Drawing.Size(955, 406);
            this.scMain.SplitterDistance = 590;
            this.scMain.SplitterWidth = 6;
            this.scMain.TabIndex = 2;
            // 
            // folderView1
            // 
            this.folderView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(47)))), ((int)(((byte)(56)))));
            this.folderView1.CurrentDirectory = "C:\\";
            this.folderView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folderView1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.folderView1.Location = new System.Drawing.Point(0, 0);
            this.folderView1.Name = "folderView1";
            this.folderView1.Size = new System.Drawing.Size(590, 406);
            this.folderView1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button2.Location = new System.Drawing.Point(308, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(23, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "^";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(330, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(219, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(89, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(109, 2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(109, 21);
            this.comboBox2.TabIndex = 2;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(105, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // imageDisplay1
            // 
            this.imageDisplay1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageDisplay1.CellColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.imageDisplay1.CellColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.imageDisplay1.CellScale = 2F;
            this.imageDisplay1.CellSize = 32;
            this.imageDisplay1.DrawMode = WinkingCat.HelperLibs.Controls.DrawMode.FitImage;
            this.imageDisplay1.Image = null;
            this.imageDisplay1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.imageDisplay1.Location = new System.Drawing.Point(3, 26);
            this.imageDisplay1.Name = "imageDisplay1";
            this.imageDisplay1.Size = new System.Drawing.Size(349, 377);
            this.imageDisplay1.TabIndex = 0;
            this.imageDisplay1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.imageDisplay1_MouseClick);
            // 
            // tsMain
            // 
            this.tsMain.AutoSize = false;
            this.tsMain.ClickThrough = false;
            this.tsMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddbToolStripDropDownButton_Capture,
            this.tsddbToolStripDropDownButton_Clips,
            this.tsddbToolStripDropDownButton_Tools,
            this.tssToolStripSeparator1,
            this.tsbToolStripButton_Style,
            this.tsbToolStripDropDownButton_Settings});
            this.tsMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.ShowItemToolTips = false;
            this.tsMain.Size = new System.Drawing.Size(184, 406);
            this.tsMain.TabIndex = 1;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsddbToolStripDropDownButton_Capture
            // 
            this.tsddbToolStripDropDownButton_Capture.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiToolStripMenuItem_region,
            this.tsmiToolStripMenuItem_monitor,
            this.tsmiToolStripMenuItem_window,
            this.tsmiToolStripMenuItem_fullscreen,
            this.tsmiToolStripMenuItem_lastRegion,
            this.tsmiToolStripMenuItem_captureCursor});
            this.tsddbToolStripDropDownButton_Capture.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsddbToolStripDropDownButton_Capture.Image = ((System.Drawing.Image)(resources.GetObject("tsddbToolStripDropDownButton_Capture.Image")));
            this.tsddbToolStripDropDownButton_Capture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbToolStripDropDownButton_Capture.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsddbToolStripDropDownButton_Capture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbToolStripDropDownButton_Capture.Name = "tsddbToolStripDropDownButton_Capture";
            this.tsddbToolStripDropDownButton_Capture.Size = new System.Drawing.Size(182, 36);
            this.tsddbToolStripDropDownButton_Capture.Text = "Capture";
            this.tsddbToolStripDropDownButton_Capture.ToolTipText = "Capture";
            // 
            // tsmiToolStripMenuItem_region
            // 
            this.tsmiToolStripMenuItem_region.Image = global::WinkingCat.Properties.Resources.Region_icon;
            this.tsmiToolStripMenuItem_region.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiToolStripMenuItem_region.Name = "tsmiToolStripMenuItem_region";
            this.tsmiToolStripMenuItem_region.Size = new System.Drawing.Size(204, 38);
            this.tsmiToolStripMenuItem_region.Text = "Region";
            // 
            // tsmiToolStripMenuItem_monitor
            // 
            this.tsmiToolStripMenuItem_monitor.Image = global::WinkingCat.Properties.Resources.monitor_icon;
            this.tsmiToolStripMenuItem_monitor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiToolStripMenuItem_monitor.Name = "tsmiToolStripMenuItem_monitor";
            this.tsmiToolStripMenuItem_monitor.Size = new System.Drawing.Size(204, 38);
            this.tsmiToolStripMenuItem_monitor.Text = "Monitor";
            // 
            // tsmiToolStripMenuItem_window
            // 
            this.tsmiToolStripMenuItem_window.Image = global::WinkingCat.Properties.Resources.Aero_Window_Explorer_icon;
            this.tsmiToolStripMenuItem_window.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiToolStripMenuItem_window.Name = "tsmiToolStripMenuItem_window";
            this.tsmiToolStripMenuItem_window.Size = new System.Drawing.Size(204, 38);
            this.tsmiToolStripMenuItem_window.Text = "Window";
            // 
            // tsmiToolStripMenuItem_fullscreen
            // 
            this.tsmiToolStripMenuItem_fullscreen.Image = global::WinkingCat.Properties.Resources.Fullscreen_icon;
            this.tsmiToolStripMenuItem_fullscreen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiToolStripMenuItem_fullscreen.Name = "tsmiToolStripMenuItem_fullscreen";
            this.tsmiToolStripMenuItem_fullscreen.Size = new System.Drawing.Size(204, 38);
            this.tsmiToolStripMenuItem_fullscreen.Text = "Fullscreen";
            // 
            // tsmiToolStripMenuItem_lastRegion
            // 
            this.tsmiToolStripMenuItem_lastRegion.Image = global::WinkingCat.Properties.Resources.lastRegion_icon;
            this.tsmiToolStripMenuItem_lastRegion.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiToolStripMenuItem_lastRegion.Name = "tsmiToolStripMenuItem_lastRegion";
            this.tsmiToolStripMenuItem_lastRegion.Size = new System.Drawing.Size(204, 38);
            this.tsmiToolStripMenuItem_lastRegion.Text = "Last Region";
            // 
            // tsmiToolStripMenuItem_captureCursor
            // 
            this.tsmiToolStripMenuItem_captureCursor.CheckOnClick = true;
            this.tsmiToolStripMenuItem_captureCursor.Image = global::WinkingCat.Properties.Resources.cursor_icon;
            this.tsmiToolStripMenuItem_captureCursor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiToolStripMenuItem_captureCursor.Name = "tsmiToolStripMenuItem_captureCursor";
            this.tsmiToolStripMenuItem_captureCursor.Size = new System.Drawing.Size(204, 38);
            this.tsmiToolStripMenuItem_captureCursor.Text = "Capture Cursor";
            // 
            // tsddbToolStripDropDownButton_Clips
            // 
            this.tsddbToolStripDropDownButton_Clips.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiToolStripMenuItem_newClip,
            this.tsmiToolStripMenuItem_clipFromClipboard,
            this.tsmiToolStripMenuItem_clipFromFile});
            this.tsddbToolStripDropDownButton_Clips.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsddbToolStripDropDownButton_Clips.Image = global::WinkingCat.Properties.Resources.Binder_Clip_icon;
            this.tsddbToolStripDropDownButton_Clips.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbToolStripDropDownButton_Clips.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsddbToolStripDropDownButton_Clips.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbToolStripDropDownButton_Clips.Name = "tsddbToolStripDropDownButton_Clips";
            this.tsddbToolStripDropDownButton_Clips.Size = new System.Drawing.Size(182, 36);
            this.tsddbToolStripDropDownButton_Clips.Text = "Clips";
            // 
            // tsmiToolStripMenuItem_newClip
            // 
            this.tsmiToolStripMenuItem_newClip.Image = global::WinkingCat.Properties.Resources.new_document_icon;
            this.tsmiToolStripMenuItem_newClip.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiToolStripMenuItem_newClip.Name = "tsmiToolStripMenuItem_newClip";
            this.tsmiToolStripMenuItem_newClip.Size = new System.Drawing.Size(244, 38);
            this.tsmiToolStripMenuItem_newClip.Text = "New Clip";
            // 
            // tsmiToolStripMenuItem_clipFromClipboard
            // 
            this.tsmiToolStripMenuItem_clipFromClipboard.Image = global::WinkingCat.Properties.Resources.Clipboard_2_icon;
            this.tsmiToolStripMenuItem_clipFromClipboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiToolStripMenuItem_clipFromClipboard.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiToolStripMenuItem_clipFromClipboard.Name = "tsmiToolStripMenuItem_clipFromClipboard";
            this.tsmiToolStripMenuItem_clipFromClipboard.Size = new System.Drawing.Size(244, 38);
            this.tsmiToolStripMenuItem_clipFromClipboard.Text = "Clip From Clipboard";
            // 
            // tsmiToolStripMenuItem_clipFromFile
            // 
            this.tsmiToolStripMenuItem_clipFromFile.Image = global::WinkingCat.Properties.Resources.new_doc_icon;
            this.tsmiToolStripMenuItem_clipFromFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiToolStripMenuItem_clipFromFile.Name = "tsmiToolStripMenuItem_clipFromFile";
            this.tsmiToolStripMenuItem_clipFromFile.Size = new System.Drawing.Size(244, 38);
            this.tsmiToolStripMenuItem_clipFromFile.Text = "Clip From File";
            // 
            // tsddbToolStripDropDownButton_Tools
            // 
            this.tsddbToolStripDropDownButton_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiToolStripDropDownButton_screenColorPicker,
            this.tsmiToolStripDropDownButton_ColorPicker,
            this.tsmiToolStripDropDownButton_HashCheck,
            this.tsmiToolStripDropDownButton_QrCode,
            this.oCRToolStripMenuItem,
            this.tsmiToolStripDropDownButton_Regex});
            this.tsddbToolStripDropDownButton_Tools.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsddbToolStripDropDownButton_Tools.Image = global::WinkingCat.Properties.Resources.google_webmaster_tools_icon;
            this.tsddbToolStripDropDownButton_Tools.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbToolStripDropDownButton_Tools.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsddbToolStripDropDownButton_Tools.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbToolStripDropDownButton_Tools.Name = "tsddbToolStripDropDownButton_Tools";
            this.tsddbToolStripDropDownButton_Tools.Size = new System.Drawing.Size(182, 36);
            this.tsddbToolStripDropDownButton_Tools.Text = "Tools";
            // 
            // tsmiToolStripDropDownButton_screenColorPicker
            // 
            this.tsmiToolStripDropDownButton_screenColorPicker.Image = ((System.Drawing.Image)(resources.GetObject("tsmiToolStripDropDownButton_screenColorPicker.Image")));
            this.tsmiToolStripDropDownButton_screenColorPicker.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiToolStripDropDownButton_screenColorPicker.Name = "tsmiToolStripDropDownButton_screenColorPicker";
            this.tsmiToolStripDropDownButton_screenColorPicker.Size = new System.Drawing.Size(244, 38);
            this.tsmiToolStripDropDownButton_screenColorPicker.Text = "Screen Color Picker";
            // 
            // tsmiToolStripDropDownButton_ColorPicker
            // 
            this.tsmiToolStripDropDownButton_ColorPicker.Image = global::WinkingCat.Properties.Resources.color_wheel_icon;
            this.tsmiToolStripDropDownButton_ColorPicker.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiToolStripDropDownButton_ColorPicker.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiToolStripDropDownButton_ColorPicker.Name = "tsmiToolStripDropDownButton_ColorPicker";
            this.tsmiToolStripDropDownButton_ColorPicker.Size = new System.Drawing.Size(244, 38);
            this.tsmiToolStripDropDownButton_ColorPicker.Text = "Color Picker";
            // 
            // tsmiToolStripDropDownButton_HashCheck
            // 
            this.tsmiToolStripDropDownButton_HashCheck.Image = global::WinkingCat.Properties.Resources.hashcheck;
            this.tsmiToolStripDropDownButton_HashCheck.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiToolStripDropDownButton_HashCheck.Name = "tsmiToolStripDropDownButton_HashCheck";
            this.tsmiToolStripDropDownButton_HashCheck.Size = new System.Drawing.Size(244, 38);
            this.tsmiToolStripDropDownButton_HashCheck.Text = "Hash Check";
            // 
            // tsmiToolStripDropDownButton_QrCode
            // 
            this.tsmiToolStripDropDownButton_QrCode.Image = global::WinkingCat.Properties.Resources.qrCode;
            this.tsmiToolStripDropDownButton_QrCode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiToolStripDropDownButton_QrCode.Name = "tsmiToolStripDropDownButton_QrCode";
            this.tsmiToolStripDropDownButton_QrCode.Size = new System.Drawing.Size(244, 38);
            this.tsmiToolStripDropDownButton_QrCode.Text = "QrCode Scan";
            // 
            // oCRToolStripMenuItem
            // 
            this.oCRToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.oCRToolStripMenuItem.Name = "oCRToolStripMenuItem";
            this.oCRToolStripMenuItem.Size = new System.Drawing.Size(244, 38);
            this.oCRToolStripMenuItem.Text = "OCR";
            this.oCRToolStripMenuItem.Click += new System.EventHandler(this.OCRToolStripMenuItem_Click);
            // 
            // tsmiToolStripDropDownButton_Regex
            // 
            this.tsmiToolStripDropDownButton_Regex.Name = "tsmiToolStripDropDownButton_Regex";
            this.tsmiToolStripDropDownButton_Regex.Size = new System.Drawing.Size(244, 38);
            this.tsmiToolStripDropDownButton_Regex.Text = "Regex";
            // 
            // tssToolStripSeparator1
            // 
            this.tssToolStripSeparator1.AutoSize = false;
            this.tssToolStripSeparator1.Name = "tssToolStripSeparator1";
            this.tssToolStripSeparator1.Size = new System.Drawing.Size(167, 10);
            // 
            // tsbToolStripButton_Style
            // 
            this.tsbToolStripButton_Style.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbToolStripButton_Style.Image = global::WinkingCat.Properties.Resources.style_icon;
            this.tsbToolStripButton_Style.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbToolStripButton_Style.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbToolStripButton_Style.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbToolStripButton_Style.Name = "tsbToolStripButton_Style";
            this.tsbToolStripButton_Style.Size = new System.Drawing.Size(182, 36);
            this.tsbToolStripButton_Style.Text = "Styles";
            this.tsbToolStripButton_Style.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbToolStripButton_Style.Click += new System.EventHandler(this.ToolStripDropDownButton_Styles_Click);
            // 
            // tsbToolStripDropDownButton_Settings
            // 
            this.tsbToolStripDropDownButton_Settings.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbToolStripDropDownButton_Settings.Image = global::WinkingCat.Properties.Resources.gear_in_icon;
            this.tsbToolStripDropDownButton_Settings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbToolStripDropDownButton_Settings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbToolStripDropDownButton_Settings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbToolStripDropDownButton_Settings.Name = "tsbToolStripDropDownButton_Settings";
            this.tsbToolStripDropDownButton_Settings.Size = new System.Drawing.Size(182, 36);
            this.tsbToolStripDropDownButton_Settings.Text = "Settings";
            this.tsbToolStripDropDownButton_Settings.Click += new System.EventHandler(this.ToolStripDropDownButton_Settings_Click);
            // 
            // imageDisplayContextMenu
            // 
            this.imageDisplayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyImageToolStripMenuItem,
            this.useBackColor1OnlyToolStripMenuItem,
            this.toolStripSeparator2,
            this.setBackColor1ToolStripMenuItem,
            this.setBackColor2ToolStripMenuItem,
            this.toolStripSeparator1,
            this.resetXYOffsetToolStripMenuItem,
            this.resetBackColorsToolStripMenuItem});
            this.imageDisplayContextMenu.Name = "contextMenuStrip1";
            this.imageDisplayContextMenu.Size = new System.Drawing.Size(191, 170);
            // 
            // copyImageToolStripMenuItem
            // 
            this.copyImageToolStripMenuItem.Name = "copyImageToolStripMenuItem";
            this.copyImageToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.copyImageToolStripMenuItem.Text = "Copy Image";
            this.copyImageToolStripMenuItem.Click += new System.EventHandler(this.copyImageToolStripMenuItem_Click);
            // 
            // useBackColor1OnlyToolStripMenuItem
            // 
            this.useBackColor1OnlyToolStripMenuItem.CheckOnClick = true;
            this.useBackColor1OnlyToolStripMenuItem.Name = "useBackColor1OnlyToolStripMenuItem";
            this.useBackColor1OnlyToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.useBackColor1OnlyToolStripMenuItem.Text = "Use Back Color 1 Only";
            this.useBackColor1OnlyToolStripMenuItem.Click += new System.EventHandler(this.useBackColor1OnlyToolStripMenuItem_Click);
            // 
            // setBackColor1ToolStripMenuItem
            // 
            this.setBackColor1ToolStripMenuItem.Name = "setBackColor1ToolStripMenuItem";
            this.setBackColor1ToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.setBackColor1ToolStripMenuItem.Text = "Set Back Color 1";
            this.setBackColor1ToolStripMenuItem.Click += new System.EventHandler(this.setBackColor1ToolStripMenuItem_Click);
            // 
            // setBackColor2ToolStripMenuItem
            // 
            this.setBackColor2ToolStripMenuItem.Name = "setBackColor2ToolStripMenuItem";
            this.setBackColor2ToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.setBackColor2ToolStripMenuItem.Text = "Set Back Color 2";
            this.setBackColor2ToolStripMenuItem.Click += new System.EventHandler(this.setBackColor2ToolStripMenuItem_Click);
            // 
            // resetXYOffsetToolStripMenuItem
            // 
            this.resetXYOffsetToolStripMenuItem.Name = "resetXYOffsetToolStripMenuItem";
            this.resetXYOffsetToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.resetXYOffsetToolStripMenuItem.Text = "Reset XY Offset";
            this.resetXYOffsetToolStripMenuItem.Click += new System.EventHandler(this.resetXYOffsetToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(187, 6);
            // 
            // resetBackColorsToolStripMenuItem
            // 
            this.resetBackColorsToolStripMenuItem.Name = "resetBackColorsToolStripMenuItem";
            this.resetBackColorsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.resetBackColorsToolStripMenuItem.Text = "Reset Back Colors";
            this.resetBackColorsToolStripMenuItem.Click += new System.EventHandler(this.resetBackColorsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(187, 6);
            // 
            // ApplicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 406);
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.tsMain);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(666, 250);
            this.Name = "ApplicationForm";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.cmTray.ResumeLayout(false);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.imageDisplayContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.NotifyIcon niTrayIcon;
        private System.Windows.Forms.ContextMenuStrip cmTray;
        private System.Windows.Forms.ToolStripMenuItem tsmiCaptureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiRegionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiFullscreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiLastRegionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiCaptureCursorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiClipsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator tssToolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiStylesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator tssToolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenMainWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewClipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiClipFromClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiClipFromFileToolStripMenuItem;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.ToolStripDropDownButton tsddbToolStripDropDownButton_Capture;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolStripMenuItem_region;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolStripMenuItem_monitor;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolStripMenuItem_window;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolStripMenuItem_fullscreen;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolStripMenuItem_lastRegion;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolStripMenuItem_captureCursor;
        private System.Windows.Forms.ToolStripDropDownButton tsddbToolStripDropDownButton_Clips;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolStripMenuItem_newClip;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolStripMenuItem_clipFromClipboard;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolStripMenuItem_clipFromFile;
        private System.Windows.Forms.ToolStripDropDownButton tsddbToolStripDropDownButton_Tools;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolStripDropDownButton_screenColorPicker;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolStripDropDownButton_ColorPicker;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolStripDropDownButton_HashCheck;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolStripDropDownButton_QrCode;
        private System.Windows.Forms.ToolStripSeparator tssToolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbToolStripButton_Style;
        private System.Windows.Forms.ToolStripButton tsbToolStripDropDownButton_Settings;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayScreenColorPicker;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayColorWheel;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayHashCheck;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayQrCodeScan;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolStripDropDownButton_Regex;
        private System.Windows.Forms.ToolStripMenuItem oCRToolStripMenuItem;
        private HelperLibs.ToolStripEx tsMain;
        private Controls.FolderView folderView1;
        private HelperLibs.Controls.ImageDisplay imageDisplay1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ContextMenuStrip imageDisplayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useBackColor1OnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setBackColor1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setBackColor2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetXYOffsetToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem resetBackColorsToolStripMenuItem;
    }
}

