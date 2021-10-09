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
            this.lvListView = new WinkingCat.NoCheckboxListView();
            this.chColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chColumnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chColumnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chColumnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pbPreviewBox = new WinkingCat.HelperLibs._PictureBox();
            //this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsMain = new WinkingCat.HelperLibs.ToolStripEx(); // enable click through
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
            this.tsmiToolStripDropDownButton_Regex = new System.Windows.Forms.ToolStripMenuItem();
            this.tssToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbToolStripButton_Style = new System.Windows.Forms.ToolStripButton();
            this.tsbToolStripDropDownButton_Settings = new System.Windows.Forms.ToolStripButton();
            this.oCRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmTray.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.tsMain.SuspendLayout();
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
            this.scMain.Panel1.Controls.Add(this.lvListView);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.pbPreviewBox);
            this.scMain.Panel2Collapsed = true;
            this.scMain.Size = new System.Drawing.Size(902, 211);
            this.scMain.SplitterDistance = 600;
            this.scMain.SplitterWidth = 6;
            this.scMain.TabIndex = 2;
            // 
            // lvListView
            // 
            this.lvListView.autoFillColumn = true;
            this.lvListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.lvListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chColumnHeader1,
            this.chColumnHeader5,
            this.chColumnHeader2,
            this.chColumnHeader3,
            this.chColumnHeader4});
            this.lvListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvListView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
            this.lvListView.FullRowSelect = true;
            this.lvListView.HideSelection = false;
            this.lvListView.Location = new System.Drawing.Point(0, 0);
            this.lvListView.Name = "lvListView";
            this.lvListView.OwnerDraw = true;
            this.lvListView.Size = new System.Drawing.Size(902, 211);
            this.lvListView.TabIndex = 2;
            this.lvListView.UseCompatibleStateImageBehavior = false;
            this.lvListView.View = System.Windows.Forms.View.Details;
            // 
            // chColumnHeader1
            // 
            this.chColumnHeader1.Text = "Name";
            this.chColumnHeader1.Width = 200;
            // 
            // chColumnHeader5
            // 
            this.chColumnHeader5.Text = "Type";
            this.chColumnHeader5.Width = 100;
            // 
            // chColumnHeader2
            // 
            this.chColumnHeader2.Text = "Dimensions";
            this.chColumnHeader2.Width = 120;
            // 
            // chColumnHeader3
            // 
            this.chColumnHeader3.Text = "Size";
            this.chColumnHeader3.Width = 120;
            // 
            // chColumnHeader4
            // 
            this.chColumnHeader4.Text = "DateModified";
            this.chColumnHeader4.Width = 200;
            // 
            // pbPreviewBox
            // 
            this.pbPreviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPreviewBox.Location = new System.Drawing.Point(0, 0);
            this.pbPreviewBox.Name = "pbPreviewBox";
            this.pbPreviewBox.Size = new System.Drawing.Size(96, 100);
            this.pbPreviewBox.TabIndex = 0;
            // 
            // tsMain
            // 
            this.tsMain.AutoSize = false;
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
            this.tsMain.Size = new System.Drawing.Size(184, 211);
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
            this.tsbToolStripDropDownButton_Settings.Size = new System.Drawing.Size(108, 36);
            this.tsbToolStripDropDownButton_Settings.Text = "Settings";
            this.tsbToolStripDropDownButton_Settings.Click += new System.EventHandler(this.ToolStripDropDownButton_Settings_Click);
            // 
            // oCRToolStripMenuItem
            // 
            this.oCRToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.oCRToolStripMenuItem.Name = "oCRToolStripMenuItem";
            this.oCRToolStripMenuItem.Size = new System.Drawing.Size(244, 38);
            this.oCRToolStripMenuItem.Text = "OCR";
            this.oCRToolStripMenuItem.Click += new System.EventHandler(this.OCRToolStripMenuItem_Click);
            // 
            // ApplicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 211);
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.tsMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(666, 250);
            this.Name = "ApplicationForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.cmTray.ResumeLayout(false);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
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
        private System.Windows.Forms.ToolStrip tsMain;
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
        private NoCheckboxListView lvListView;
        private System.Windows.Forms.ColumnHeader chColumnHeader1;
        private System.Windows.Forms.ColumnHeader chColumnHeader5;
        private System.Windows.Forms.ColumnHeader chColumnHeader2;
        private System.Windows.Forms.ColumnHeader chColumnHeader3;
        private System.Windows.Forms.ColumnHeader chColumnHeader4;
        private HelperLibs._PictureBox pbPreviewBox;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayScreenColorPicker;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayColorWheel;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayHashCheck;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayQrCodeScan;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolStripDropDownButton_Regex;
        private System.Windows.Forms.ToolStripMenuItem oCRToolStripMenuItem;
    }
}

