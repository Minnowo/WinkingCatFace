﻿namespace WinkingCat
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
            this.DisplayPanel = new System.Windows.Forms.Panel();
            this.lvListView = new WinkingCat.NoCheckboxListView();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.ToolStripDropDownButton_Capture = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripMenuItem_region = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_monitor = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_window = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_fullscreen = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_lastRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_captureCursor = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripDropDownButton_Clips = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripMenuItem_newClip = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_clipFromClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_clipFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_createClipAfterRegionCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripDropDownButton_Tools = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripDropDownButton_screenColorPicker = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripDropDownButton_ColorPicker = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripDropDownButton_HashCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripDropDownButton_QrCode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripDropDownButton_Settings = new System.Windows.Forms.ToolStripButton();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.niTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.captureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullscreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastRegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.captureCursorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clipsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newClipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clipFromClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clipFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.stylesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.openMainWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripButton_Style = new System.Windows.Forms.ToolStripButton();
            this.DisplayPanel.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.cmTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // DisplayPanel
            // 
            this.DisplayPanel.BackColor = System.Drawing.Color.White;
            this.DisplayPanel.Controls.Add(this.lvListView);
            this.DisplayPanel.Controls.Add(this.tsMain);
            this.DisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayPanel.Location = new System.Drawing.Point(0, 0);
            this.DisplayPanel.Name = "DisplayPanel";
            this.DisplayPanel.Size = new System.Drawing.Size(871, 211);
            this.DisplayPanel.TabIndex = 1;
            // 
            // lvListView
            // 
            this.lvListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader5,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvListView.FullRowSelect = true;
            this.lvListView.HideSelection = false;
            this.lvListView.Location = new System.Drawing.Point(184, 0);
            this.lvListView.Name = "lvListView";
            this.lvListView.Size = new System.Drawing.Size(687, 211);
            this.lvListView.TabIndex = 2;
            this.lvListView.UseCompatibleStateImageBehavior = false;
            this.lvListView.View = System.Windows.Forms.View.Details;
            // 
            // toolStrip1
            // 
            this.tsMain.AutoSize = false;
            this.tsMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripDropDownButton_Capture,
            this.ToolStripDropDownButton_Clips,
            this.ToolStripDropDownButton_Tools,
            this.toolStripSeparator1,
            this.ToolStripButton_Style,
            this.ToolStripDropDownButton_Settings});
            this.tsMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "toolStrip1";
            this.tsMain.ShowItemToolTips = false;
            this.tsMain.Size = new System.Drawing.Size(184, 211);
            this.tsMain.TabIndex = 1;
            this.tsMain.Text = "toolStrip1";
            // 
            // ToolStripDropDownButton_Capture
            // 
            this.ToolStripDropDownButton_Capture.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_region,
            this.ToolStripMenuItem_monitor,
            this.ToolStripMenuItem_window,
            this.ToolStripMenuItem_fullscreen,
            this.ToolStripMenuItem_lastRegion,
            this.ToolStripMenuItem_captureCursor});
            this.ToolStripDropDownButton_Capture.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStripDropDownButton_Capture.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripDropDownButton_Capture.Image")));
            this.ToolStripDropDownButton_Capture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolStripDropDownButton_Capture.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripDropDownButton_Capture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripDropDownButton_Capture.Name = "ToolStripDropDownButton_Capture";
            this.ToolStripDropDownButton_Capture.Size = new System.Drawing.Size(182, 36);
            this.ToolStripDropDownButton_Capture.Text = "Capture";
            this.ToolStripDropDownButton_Capture.ToolTipText = "Capture";
            // 
            // ToolStripMenuItem_region
            // 
            this.ToolStripMenuItem_region.Image = global::WinkingCat.Properties.Resources.Region_icon;
            this.ToolStripMenuItem_region.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripMenuItem_region.Name = "ToolStripMenuItem_region";
            this.ToolStripMenuItem_region.Size = new System.Drawing.Size(196, 38);
            this.ToolStripMenuItem_region.Text = "Region";
            // 
            // ToolStripMenuItem_monitor
            // 
            this.ToolStripMenuItem_monitor.Image = global::WinkingCat.Properties.Resources.monitor_icon;
            this.ToolStripMenuItem_monitor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripMenuItem_monitor.Name = "ToolStripMenuItem_monitor";
            this.ToolStripMenuItem_monitor.Size = new System.Drawing.Size(196, 38);
            this.ToolStripMenuItem_monitor.Text = "Monitor";
            // 
            // ToolStripMenuItem_window
            // 
            this.ToolStripMenuItem_window.Image = global::WinkingCat.Properties.Resources.Aero_Window_Explorer_icon;
            this.ToolStripMenuItem_window.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripMenuItem_window.Name = "ToolStripMenuItem_window";
            this.ToolStripMenuItem_window.Size = new System.Drawing.Size(196, 38);
            this.ToolStripMenuItem_window.Text = "Window";
            // 
            // ToolStripMenuItem_fullscreen
            // 
            this.ToolStripMenuItem_fullscreen.Image = global::WinkingCat.Properties.Resources.Fullscreen_icon;
            this.ToolStripMenuItem_fullscreen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripMenuItem_fullscreen.Name = "ToolStripMenuItem_fullscreen";
            this.ToolStripMenuItem_fullscreen.Size = new System.Drawing.Size(196, 38);
            this.ToolStripMenuItem_fullscreen.Text = "Fullscreen";
            // 
            // ToolStripMenuItem_lastRegion
            // 
            this.ToolStripMenuItem_lastRegion.Image = global::WinkingCat.Properties.Resources.lastRegion_icon;
            this.ToolStripMenuItem_lastRegion.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripMenuItem_lastRegion.Name = "ToolStripMenuItem_lastRegion";
            this.ToolStripMenuItem_lastRegion.Size = new System.Drawing.Size(196, 38);
            this.ToolStripMenuItem_lastRegion.Text = "Last Region";
            // 
            // ToolStripMenuItem_captureCursor
            // 
            this.ToolStripMenuItem_captureCursor.CheckOnClick = true;
            this.ToolStripMenuItem_captureCursor.Image = global::WinkingCat.Properties.Resources.cursor_icon;
            this.ToolStripMenuItem_captureCursor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripMenuItem_captureCursor.Name = "ToolStripMenuItem_captureCursor";
            this.ToolStripMenuItem_captureCursor.Size = new System.Drawing.Size(196, 38);
            this.ToolStripMenuItem_captureCursor.Text = "Capture Cursor";
            // 
            // ToolStripDropDownButton_Clips
            // 
            this.ToolStripDropDownButton_Clips.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_newClip,
            this.ToolStripMenuItem_clipFromClipboard,
            this.ToolStripMenuItem_clipFromFile,
            this.ToolStripMenuItem_createClipAfterRegionCapture});
            this.ToolStripDropDownButton_Clips.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStripDropDownButton_Clips.Image = global::WinkingCat.Properties.Resources.Binder_Clip_icon;
            this.ToolStripDropDownButton_Clips.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolStripDropDownButton_Clips.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripDropDownButton_Clips.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripDropDownButton_Clips.Name = "ToolStripDropDownButton_Clips";
            this.ToolStripDropDownButton_Clips.Size = new System.Drawing.Size(182, 36);
            this.ToolStripDropDownButton_Clips.Text = "Clips";
            // 
            // ToolStripMenuItem_newClip
            // 
            this.ToolStripMenuItem_newClip.Image = global::WinkingCat.Properties.Resources.new_document_icon;
            this.ToolStripMenuItem_newClip.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripMenuItem_newClip.Name = "ToolStripMenuItem_newClip";
            this.ToolStripMenuItem_newClip.Size = new System.Drawing.Size(316, 38);
            this.ToolStripMenuItem_newClip.Text = "New Clip";
            // 
            // ToolStripMenuItem_clipFromClipboard
            // 
            this.ToolStripMenuItem_clipFromClipboard.Image = global::WinkingCat.Properties.Resources.Clipboard_2_icon;
            this.ToolStripMenuItem_clipFromClipboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolStripMenuItem_clipFromClipboard.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripMenuItem_clipFromClipboard.Name = "ToolStripMenuItem_clipFromClipboard";
            this.ToolStripMenuItem_clipFromClipboard.Size = new System.Drawing.Size(316, 38);
            this.ToolStripMenuItem_clipFromClipboard.Text = "Clip From Clipboard";
            // 
            // ToolStripMenuItem_clipFromFile
            // 
            this.ToolStripMenuItem_clipFromFile.Image = global::WinkingCat.Properties.Resources.new_doc_icon;
            this.ToolStripMenuItem_clipFromFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripMenuItem_clipFromFile.Name = "ToolStripMenuItem_clipFromFile";
            this.ToolStripMenuItem_clipFromFile.Size = new System.Drawing.Size(316, 38);
            this.ToolStripMenuItem_clipFromFile.Text = "Clip From File";
            // 
            // ToolStripMenuItem_createClipAfterRegionCapture
            // 
            this.ToolStripMenuItem_createClipAfterRegionCapture.Name = "ToolStripMenuItem_createClipAfterRegionCapture";
            this.ToolStripMenuItem_createClipAfterRegionCapture.Size = new System.Drawing.Size(316, 38);
            this.ToolStripMenuItem_createClipAfterRegionCapture.Text = "Create Clip Afte rRegion Capture";
            // 
            // ToolStripDropDownButton_Tools
            // 
            this.ToolStripDropDownButton_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripDropDownButton_screenColorPicker,
            this.ToolStripDropDownButton_ColorPicker,
            this.ToolStripDropDownButton_HashCheck,
            this.ToolStripDropDownButton_QrCode});
            this.ToolStripDropDownButton_Tools.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStripDropDownButton_Tools.Image = global::WinkingCat.Properties.Resources.google_webmaster_tools_icon;
            this.ToolStripDropDownButton_Tools.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolStripDropDownButton_Tools.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripDropDownButton_Tools.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripDropDownButton_Tools.Name = "ToolStripDropDownButton_Tools";
            this.ToolStripDropDownButton_Tools.Size = new System.Drawing.Size(182, 36);
            this.ToolStripDropDownButton_Tools.Text = "Tools";
            // 
            // ToolStripDropDownButton_screenColorPicker
            // 
            this.ToolStripDropDownButton_screenColorPicker.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripDropDownButton_screenColorPicker.Image")));
            this.ToolStripDropDownButton_screenColorPicker.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripDropDownButton_screenColorPicker.Name = "ToolStripDropDownButton_screenColorPicker";
            this.ToolStripDropDownButton_screenColorPicker.Size = new System.Drawing.Size(316, 38);
            this.ToolStripDropDownButton_screenColorPicker.Text = "Screen Color Picker";
            // 
            // ToolStripDropDownButton_ColorPicker
            // 
            this.ToolStripDropDownButton_ColorPicker.Image = global::WinkingCat.Properties.Resources.color_wheel_icon;
            this.ToolStripDropDownButton_ColorPicker.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolStripDropDownButton_ColorPicker.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripDropDownButton_ColorPicker.Name = "ToolStripDropDownButton_ColorPicker";
            this.ToolStripDropDownButton_ColorPicker.Size = new System.Drawing.Size(316, 38);
            this.ToolStripDropDownButton_ColorPicker.Text = "Color Picker";
            // 
            // ToolStripDropDownButton_HashCheck
            // 
            this.ToolStripDropDownButton_HashCheck.Image = global::WinkingCat.Properties.Resources.hashcheck;
            this.ToolStripDropDownButton_HashCheck.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripDropDownButton_HashCheck.Name = "ToolStripDropDownButton_HashCheck";
            this.ToolStripDropDownButton_HashCheck.Size = new System.Drawing.Size(316, 38);
            this.ToolStripDropDownButton_HashCheck.Text = "Hash Check";
            // 
            // ToolStripDropDownButton_QrCode
            // 
            this.ToolStripDropDownButton_QrCode.Image = global::WinkingCat.Properties.Resources.qrCode;
            this.ToolStripDropDownButton_QrCode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripDropDownButton_QrCode.Name = "ToolStripDropDownButton_QrCode";
            this.ToolStripDropDownButton_QrCode.Size = new System.Drawing.Size(316, 38);
            this.ToolStripDropDownButton_QrCode.Text = "QrCode Scan";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(167, 10);
            // 
            // ToolStripDropDownButton_Settings
            // 
            this.ToolStripDropDownButton_Settings.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStripDropDownButton_Settings.Image = global::WinkingCat.Properties.Resources.gear_in_icon;
            this.ToolStripDropDownButton_Settings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolStripDropDownButton_Settings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripDropDownButton_Settings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripDropDownButton_Settings.Name = "ToolStripDropDownButton_Settings";
            this.ToolStripDropDownButton_Settings.Size = new System.Drawing.Size(108, 36);
            this.ToolStripDropDownButton_Settings.Text = "Settings";
            this.ToolStripDropDownButton_Settings.Click += new System.EventHandler(this.ToolStripDropDownButton_Settings_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Type";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Dimensions";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Size";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "DateModified";
            this.columnHeader4.Width = 200;
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
            this.captureToolStripMenuItem,
            this.clipsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.toolStripSeparator2,
            this.stylesToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolStripSeparator3,
            this.openMainWindowToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.cmTray.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.cmTray.Name = "cmTray";
            this.cmTray.Size = new System.Drawing.Size(197, 282);
            // 
            // captureToolStripMenuItem
            // 
            this.captureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.regionToolStripMenuItem,
            this.monitorToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.fullscreenToolStripMenuItem,
            this.lastRegionToolStripMenuItem,
            this.captureCursorToolStripMenuItem});
            this.captureToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.Camera_icon;
            this.captureToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.captureToolStripMenuItem.Name = "captureToolStripMenuItem";
            this.captureToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.captureToolStripMenuItem.Text = "Capture";
            // 
            // regionToolStripMenuItem
            // 
            this.regionToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.Region_icon;
            this.regionToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.regionToolStripMenuItem.Name = "regionToolStripMenuItem";
            this.regionToolStripMenuItem.Size = new System.Drawing.Size(167, 38);
            this.regionToolStripMenuItem.Text = "Region";
            // 
            // monitorToolStripMenuItem
            // 
            this.monitorToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.monitor_icon;
            this.monitorToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.monitorToolStripMenuItem.Name = "monitorToolStripMenuItem";
            this.monitorToolStripMenuItem.Size = new System.Drawing.Size(167, 38);
            this.monitorToolStripMenuItem.Text = "Monitor";
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.Aero_Window_Explorer_icon;
            this.windowToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(167, 38);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // fullscreenToolStripMenuItem
            // 
            this.fullscreenToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.Fullscreen_icon;
            this.fullscreenToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.fullscreenToolStripMenuItem.Name = "fullscreenToolStripMenuItem";
            this.fullscreenToolStripMenuItem.Size = new System.Drawing.Size(167, 38);
            this.fullscreenToolStripMenuItem.Text = "Fullscreen";
            // 
            // lastRegionToolStripMenuItem
            // 
            this.lastRegionToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.lastRegion_icon;
            this.lastRegionToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.lastRegionToolStripMenuItem.Name = "lastRegionToolStripMenuItem";
            this.lastRegionToolStripMenuItem.Size = new System.Drawing.Size(167, 38);
            this.lastRegionToolStripMenuItem.Text = "LastRegion";
            // 
            // captureCursorToolStripMenuItem
            // 
            this.captureCursorToolStripMenuItem.CheckOnClick = true;
            this.captureCursorToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.cursor_icon;
            this.captureCursorToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.captureCursorToolStripMenuItem.Name = "captureCursorToolStripMenuItem";
            this.captureCursorToolStripMenuItem.Size = new System.Drawing.Size(167, 38);
            this.captureCursorToolStripMenuItem.Text = "CaptureCursor";
            // 
            // clipsToolStripMenuItem
            // 
            this.clipsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newClipToolStripMenuItem,
            this.clipFromClipboardToolStripMenuItem,
            this.clipFromFileToolStripMenuItem});
            this.clipsToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.Binder_Clip_icon;
            this.clipsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clipsToolStripMenuItem.Name = "clipsToolStripMenuItem";
            this.clipsToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.clipsToolStripMenuItem.Text = "Clips";
            // 
            // newClipToolStripMenuItem
            // 
            this.newClipToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.new_document_icon;
            this.newClipToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newClipToolStripMenuItem.Name = "newClipToolStripMenuItem";
            this.newClipToolStripMenuItem.Size = new System.Drawing.Size(191, 38);
            this.newClipToolStripMenuItem.Text = "NewClip";
            // 
            // clipFromClipboardToolStripMenuItem
            // 
            this.clipFromClipboardToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.Clipboard_2_icon;
            this.clipFromClipboardToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clipFromClipboardToolStripMenuItem.Name = "clipFromClipboardToolStripMenuItem";
            this.clipFromClipboardToolStripMenuItem.Size = new System.Drawing.Size(191, 38);
            this.clipFromClipboardToolStripMenuItem.Text = "ClipFromClipboard";
            // 
            // clipFromFileToolStripMenuItem
            // 
            this.clipFromFileToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.new_doc_icon;
            this.clipFromFileToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clipFromFileToolStripMenuItem.Name = "clipFromFileToolStripMenuItem";
            this.clipFromFileToolStripMenuItem.Size = new System.Drawing.Size(191, 38);
            this.clipFromFileToolStripMenuItem.Text = "ClipFromFile";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.google_webmaster_tools_icon;
            this.toolsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(193, 6);
            // 
            // stylesToolStripMenuItem
            // 
            this.stylesToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.style_icon;
            this.stylesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stylesToolStripMenuItem.Name = "stylesToolStripMenuItem";
            this.stylesToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.stylesToolStripMenuItem.Text = "Styles";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.gear_in_icon;
            this.settingsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(193, 6);
            // 
            // openMainWindowToolStripMenuItem
            // 
            this.openMainWindowToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.Open_icon;
            this.openMainWindowToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openMainWindowToolStripMenuItem.Name = "openMainWindowToolStripMenuItem";
            this.openMainWindowToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.openMainWindowToolStripMenuItem.Text = "Open Main Window";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::WinkingCat.Properties.Resources.Error_Symbol_icon;
            this.exitToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // ToolStripButton_Style
            // 
            this.ToolStripButton_Style.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStripButton_Style.Image = global::WinkingCat.Properties.Resources.style_icon;
            this.ToolStripButton_Style.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolStripButton_Style.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripButton_Style.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton_Style.Name = "ToolStripButton_Style";
            this.ToolStripButton_Style.Size = new System.Drawing.Size(182, 36);
            this.ToolStripButton_Style.Text = "Styles";
            this.ToolStripButton_Style.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolStripButton_Style.Click += new System.EventHandler(this.ToolStripDropDownButton_Styles_Click);
            // 
            // ApplicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 211);
            this.Controls.Add(this.DisplayPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(666, 250);
            this.Name = "ApplicationForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.DisplayPanel.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.cmTray.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel DisplayPanel;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripDropDownButton ToolStripDropDownButton_Capture;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_region;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_monitor;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_window;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_fullscreen;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_lastRegion;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_captureCursor;
        private System.Windows.Forms.ToolStripDropDownButton ToolStripDropDownButton_Clips;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_newClip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_clipFromClipboard;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_clipFromFile;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_createClipAfterRegionCapture;
        private System.Windows.Forms.ToolStripDropDownButton ToolStripDropDownButton_Tools;
        private System.Windows.Forms.ToolStripMenuItem ToolStripDropDownButton_screenColorPicker;
        private System.Windows.Forms.ToolStripMenuItem ToolStripDropDownButton_ColorPicker;
        private System.Windows.Forms.ToolStripMenuItem ToolStripDropDownButton_HashCheck;
        private System.Windows.Forms.ToolStripMenuItem ToolStripDropDownButton_QrCode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.NotifyIcon niTrayIcon;
        private System.Windows.Forms.ContextMenuStrip cmTray;
        private System.Windows.Forms.ToolStripMenuItem captureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem regionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullscreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lastRegionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem captureCursorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clipsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem stylesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem openMainWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newClipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clipFromClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clipFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton ToolStripDropDownButton_Settings;
        private NoCheckboxListView lvListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ToolStripButton ToolStripButton_Style;
    }
}

