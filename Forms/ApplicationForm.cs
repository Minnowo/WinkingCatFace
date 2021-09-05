using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using WinkingCat.HelperLibs;
using WinkingCat.ScreenCaptureLib;

namespace WinkingCat
{

    public partial class ApplicationForm : Form
    {
        public bool IsReady { get; private set; } = false;
        public SettingsForm settingsForm { get; private set; } = null;
        public StylesForm stylesForm { get; private set; } = null;
        public ColorPickerForm colorPickerForm { get; private set; } = null;
        public BarcodeForm qrCodeForm { get; private set; } = null;
        public HashCheckForm hashCheckForm { get; private set; } = null;
        public RegexForm regexForm { get; private set; } = null;

        private System.Windows.Forms.Timer trayClickTimer;

        private int trayClickCount = 0;

        private bool forceClose = false;
        private bool allowShowDisplay = !SettingsManager.MainFormSettings.Start_In_Tray;
        private bool isInTrayOrMinimized = SettingsManager.MainFormSettings.Start_In_Tray;
        private bool forceDropDownClose = false;
        private bool isHandleCreated = false;

        public ApplicationForm()
        {
            InitializeComponent();
            SuspendLayout();

#if !DEBUG
            TopMost = MainFormSettings.alwaysOnTop;
            niTrayIcon.Visible = MainFormSettings.showInTray;
#endif

            #region Capture dropdown buttons event bindings
            tsddbToolStripDropDownButton_Capture.DropDown.Closing += toolStripDropDown_Closing;
            tsddbToolStripDropDownButton_Capture.DropDownOpening += tsmiCapture_DropDownOpening;

            tsmiToolStripMenuItem_region.Click += RegionCapture_Click;
            tsmiToolStripMenuItem_fullscreen.Click += FullscreenCapture_Click;
            tsmiToolStripMenuItem_lastRegion.Click += LastRegionCapture_Click;
            tsmiToolStripMenuItem_captureCursor.Click += CursorCapture_Click;
            #endregion

            #region Clips dropdown buttons event bindings
            tsddbToolStripDropDownButton_Clips.DropDown.Closing += toolStripDropDown_Closing;

            tsmiToolStripMenuItem_newClip.Click += NewClip_Click;
            tsmiToolStripMenuItem_clipFromClipboard.Click += ClipFromClipboard_Click;
            tsmiToolStripMenuItem_clipFromFile.Click += ClipFromFile_Click;
            #endregion

            #region Tools dropdown buttons event bindings
            tsddbToolStripDropDownButton_Tools.DropDown.Closing += toolStripDropDown_Closing;

            tsmiToolStripDropDownButton_screenColorPicker.Click += ScreenColorPicker_Click;
            tsmiToolStripDropDownButton_ColorPicker.Click += ColorPicker_Click;
            tsmiToolStripDropDownButton_QrCode.Click += QrCode_Click;
            tsmiToolStripDropDownButton_HashCheck.Click += HashCheck_Click;
            tsmiToolStripDropDownButton_Regex.Click += Regex_Click;
            #endregion

            #region Tray icon event bindings
            trayClickTimer = new System.Windows.Forms.Timer();
            trayClickTimer.Tick += TrayClickTimer_Interval;
            niTrayIcon.MouseUp += NiTrayIcon_MouseClick1Up;
            cmTray.Opening += tsmiCapture_DropDownOpening;

            // capture
            tsmiRegionToolStripMenuItem.Click += RegionCapture_Click;
            tsmiFullscreenToolStripMenuItem.Click += FullscreenCapture_Click;
            tsmiLastRegionToolStripMenuItem.Click += LastRegionCapture_Click;
            tsmiCaptureCursorToolStripMenuItem.Click += CursorCapture_Click;

            // clip
            tsmiNewClipToolStripMenuItem.Click += NewClip_Click;
            tsmiClipFromClipboardToolStripMenuItem.Click += ClipFromClipboard_Click;
            tsmiClipFromFileToolStripMenuItem.Click += ClipFromFile_Click;

            // tools
            tsmiTrayScreenColorPicker.Click += ScreenColorPicker_Click;
            tsmiTrayColorWheel.Click += ColorPicker_Click;
            tsmiTrayHashCheck.Click += HashCheck_Click;
            tsmiTrayQrCodeScan.Click += QrCode_Click;

            // other
            tsmiStylesToolStripMenuItem.Click += ToolStripDropDownButton_Styles_Click;
            tsmiSettingsToolStripMenuItem.Click += ToolStripDropDownButton_Settings_Click;
            tsmiOpenMainWindowToolStripMenuItem.Click += OpenMainWindow_Click;
            tsmiExitToolStripMenuItem.Click += ExitApplication_Click;
#endregion

            HandleCreated += MainForm_HandleCreated;
            LostFocus += MainForm_LostFocus;
            GotFocus += MainForm_GotFocus;
            Resize += MainForm_Resize;
            ResizeEnd += MainForm_Resize;
            Shown += MainForm_Shown;

            ImageHandler.ImageSaved += ImageSaved_Event;
            ApplicationStyles.UpdateStylesEvent += ApplicationStyles_UpdateSylesEvent;
            //MainFormSettings.SettingsChangedEvent += UpdateSettings;
            lvListView.ItemSelectionChanged += LvListView_ItemSelectionChanged;
            pbPreviewBox.pbMain.MouseClick += PbPreviewBox_MouseClick;

            ResumeLayout();

            lvListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.pbPreviewBox.previewOnClick = true;
        }

        /// <summary>
        /// Force close all the dropdowns.
        /// </summary>
        public void CloseDropDowns()
        {
            forceDropDownClose = true;
            tsddbToolStripDropDownButton_Capture.DropDown.Close();
            tsddbToolStripDropDownButton_Clips.DropDown.Close();
            tsddbToolStripDropDownButton_Tools.DropDown.Close();
        }

        /// <summary>
        /// Hide the main, settings, and styles form.
        /// </summary>
        public void HideAll()
        {
            Hide();
            settingsForm?.Hide();
            stylesForm?.Hide();
        }

        /// <summary>
        /// Show the main, settings, and styles form.
        /// </summary>
        public void ShowAll()
        {
            Show();
            settingsForm?.Show();
            stylesForm?.Show();
        }

        /// <summary>
        /// Updates the isInTrayOrMinimized variable.
        /// </summary>
        public void CheckWindowState()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Minimized:
                    isInTrayOrMinimized = true;
                    break;
                case FormWindowState.Maximized:
                case FormWindowState.Normal:
                    isInTrayOrMinimized = false;
                    break;
            }
        }

        /// <summary>
        /// Updates the theme.
        /// </summary>
        public void UpdateTheme()
        {
            if (ApplicationStyles.currentStyle.mainFormStyle.useImersiveDarkMode && isHandleCreated)
            {
                NativeMethods.UseImmersiveDarkMode(Handle, true);
                this.Icon = ApplicationStyles.whiteIcon;
            }
            else
            {
                NativeMethods.UseImmersiveDarkMode(Handle, false);
                this.Icon = ApplicationStyles.blackIcon;
            }

            this.Text = "";
            this.BackColor = ApplicationStyles.currentStyle.mainFormStyle.backgroundColor;
            tsMain.Renderer = new ToolStripCustomRenderer();

            cmTray.Renderer = new ToolStripCustomRenderer();
            cmTray.Opacity = ApplicationStyles.currentStyle.mainFormStyle.contextMenuOpacity;

            lvListView.ForeColor = ApplicationStyles.currentStyle.mainFormStyle.textColor;
            Refresh();
        }


        #region Capture dropdown buttons

        private async void tsmiCapture_DropDownOpening(object sender, EventArgs e)
        {
            if(sender.GetType().Name == "ToolStripDropDownButton") 
                await PrepareCaptureMenuAsync(tsmiToolStripMenuItem_window, WindowItems_Click, tsmiToolStripMenuItem_monitor, MonitorItems_Click);
            else
                await PrepareCaptureMenuAsync(tsmiWindowToolStripMenuItem, WindowItems_Click, tsmiMonitorToolStripMenuItem, MonitorItems_Click);
        }

        private void WindowItems_Click(object sender, EventArgs e)
        {
            forceDropDownClose = true;
            MainForm_LostFocus(null, null);

            ToolStripItem tsi = (ToolStripItem)sender;
            WindowInfo win = (WindowInfo)tsi.Tag;

            if(Handle != win.Handle)
            {
                win.Activate();
                if(win.IsMinimized)
                    win.Restore();

                if (SettingsManager.MainFormSettings.Hide_Form_On_Captrue && !isInTrayOrMinimized)
                {
                    HideAll();
                }
            }

            Thread.Sleep(SettingsManager.MainFormSettings.Wait_Hide_Time);
            TaskHandler.CaptureWindow(win);

            ShowAll();
        }

        private void MonitorItems_Click(object sender, EventArgs e)
        {
            forceDropDownClose = true;
            MainForm_LostFocus(null, null);

            ToolStripItem tsi = (ToolStripItem)sender;

            if (SettingsManager.MainFormSettings.Hide_Form_On_Captrue && !isInTrayOrMinimized)
            {
                HideAll();
            }
            Thread.Sleep(SettingsManager.MainFormSettings.Wait_Hide_Time);

            using (Bitmap img = ScreenShotManager.CaptureRectangle((Rectangle)tsi.Tag))
            {
                ImageHandler.Save(PathHelper.GetNewImageFileName(), img);
                if (RegionCaptureOptions.AutoCopyImage)
                {
                    ClipboardHelper.CopyImage(img);
                }
            }

            ShowAll();
        }

        private void RegionCapture_Click(object sender, EventArgs e)
        {
            if (SettingsManager.MainFormSettings.Hide_Form_On_Captrue && !isInTrayOrMinimized)
            {
                HideAll();
                Thread.Sleep(SettingsManager.MainFormSettings.Wait_Hide_Time);
            }
            TaskHandler.ExecuteTask(Function.RegionCapture);

            if (SettingsManager.MainFormSettings.Hide_Form_On_Captrue && !isInTrayOrMinimized)
            {
                ShowAll();
            }
        }
        private void MonitorCapture_Click(object sender, EventArgs e)
        {
            if (SettingsManager.MainFormSettings.Hide_Form_On_Captrue && !isInTrayOrMinimized)
            {
                HideAll();
                Thread.Sleep(SettingsManager.MainFormSettings.Wait_Hide_Time);
            }
            TaskHandler.ExecuteTask(Function.CaptureActiveMonitor);

            if (SettingsManager.MainFormSettings.Hide_Form_On_Captrue && !isInTrayOrMinimized)
            {
                ShowAll();
            }
        }
        private void FullscreenCapture_Click(object sender, EventArgs e)
        {
            if (SettingsManager.MainFormSettings.Hide_Form_On_Captrue && !isInTrayOrMinimized)
            {
                HideAll();
                Thread.Sleep(SettingsManager.MainFormSettings.Wait_Hide_Time);
            }
            TaskHandler.ExecuteTask(Function.CaptureFullScreen);

            if (SettingsManager.MainFormSettings.Hide_Form_On_Captrue && !isInTrayOrMinimized)
            {
                ShowAll();
            }
        }
        private void LastRegionCapture_Click(object sender, EventArgs e)
        {
            if (SettingsManager.MainFormSettings.Hide_Form_On_Captrue)
            {
                HideAll();
                Thread.Sleep(SettingsManager.MainFormSettings.Wait_Hide_Time);
            }
            TaskHandler.ExecuteTask(Function.CaptureLastRegion);

            if (SettingsManager.MainFormSettings.Hide_Form_On_Captrue)
            {
                ShowAll();
            }
        }
        private void CursorCapture_Click(object sender, EventArgs e)
        {
            if (tsmiToolStripMenuItem_captureCursor.Checked || tsmiCaptureCursorToolStripMenuItem.Checked)
            {
                ScreenShotManager.captureCursor = true;
                tsmiToolStripMenuItem_captureCursor.Checked = true;
                tsmiCaptureCursorToolStripMenuItem.Checked = true;
            }
            else
            {
                ScreenShotManager.captureCursor = false;
                tsmiToolStripMenuItem_captureCursor.Checked = false;
                tsmiCaptureCursorToolStripMenuItem.Checked = false;

            }
        }
#endregion

        #region Clips dropdown buttons
        private void NewClip_Click(object sender, EventArgs e)
        {
            if (SettingsManager.MainFormSettings.Hide_Form_On_Captrue && !isInTrayOrMinimized)
            {
                HideAll();
                Thread.Sleep(SettingsManager.MainFormSettings.Wait_Hide_Time);
            }
            TaskHandler.ExecuteTask(Function.NewClipFromRegionCapture);

            if (SettingsManager.MainFormSettings.Hide_Form_On_Captrue && !isInTrayOrMinimized)
            {
                ShowAll();
            }
        }
        private void ClipFromClipboard_Click(object sender, EventArgs e)
        {
            TaskHandler.ExecuteTask(Function.NewClipFromClipboard);
        }
        private void ClipFromFile_Click(object sender, EventArgs e)
        {
            TaskHandler.ExecuteTask(Function.NewClipFromFile);
        }
#endregion

        #region Tools dropdown buttons
        private void ScreenColorPicker_Click(object sender, EventArgs e)
        {
            if (SettingsManager.MainFormSettings.Hide_Form_On_Captrue && !isInTrayOrMinimized)
            {
                Hide();
                Thread.Sleep(SettingsManager.MainFormSettings.Wait_Hide_Time);
            }
            TaskHandler.ExecuteTask(Function.ScreenColorPicker);

            if (SettingsManager.MainFormSettings.Hide_Form_On_Captrue && !isInTrayOrMinimized)
            {
                Thread.Sleep(SettingsManager.MainFormSettings.Wait_Hide_Time);
                Show();
            }
        }

        internal void ColorPicker_Click(object sender, EventArgs e)
        {
            if(colorPickerForm != null)
            {
                colorPickerForm.ForceActivate();
            }
            else
            {
                colorPickerForm = new ColorPickerForm();
                colorPickerForm.TopMost = SettingsManager.MainFormSettings.Always_On_Top;
                colorPickerForm.FormClosing += ChildFormClosing;
                colorPickerForm.Show();
            }
        }

        internal void QrCode_Click(object sender, EventArgs e) 
        {
            if(qrCodeForm != null)
            {
                qrCodeForm.ForceActivate();
            }
            else
            {
                qrCodeForm = new BarcodeForm();
                qrCodeForm.TopMost = SettingsManager.MainFormSettings.Always_On_Top;
                qrCodeForm.FormClosing += ChildFormClosing;
                qrCodeForm.Show();
            }
        }

        internal void HashCheck_Click(object sender, EventArgs e)
        {
            if (hashCheckForm != null)
            {
                hashCheckForm.ForceActivate();
            }
            else
            {
                hashCheckForm = new HashCheckForm();
                hashCheckForm.TopMost = SettingsManager.MainFormSettings.Always_On_Top;
                hashCheckForm.FormClosing += ChildFormClosing;
                hashCheckForm.Show();
            }
        }

        internal void Regex_Click(object sender, EventArgs e)
        {
            if (regexForm != null)
            {
                regexForm.ForceActivate();
            }
            else
            {
                regexForm = new RegexForm();
                regexForm.TopMost = SettingsManager.MainFormSettings.Always_On_Top;
                regexForm.FormClosing += ChildFormClosing;
                regexForm.Show();
            }
        }
        #endregion

        #region tray icon

        private void TrayClickTimer_Interval(object sender, EventArgs e)
        {
            if (trayClickCount == 1)
            {
                trayClickCount = 0;
                trayClickTimer.Stop();

                TaskHandler.ExecuteTask(SettingsManager.MainFormSettings.On_Tray_Left_Click);
            }
        }

        private void NiTrayIcon_MouseClick1Up(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    trayClickCount++;

                    if (trayClickCount == 1)
                    {
                        trayClickTimer.Interval = SystemInformation.DoubleClickTime;
                        trayClickTimer.Start();
                    }
                    else
                    {
                        trayClickCount = 0;
                        trayClickTimer.Stop();

                        TaskHandler.ExecuteTask(SettingsManager.MainFormSettings.On_Tray_Double_Click);
                    }
                    break;
                case MouseButtons.Middle:
                    TaskHandler.ExecuteTask(SettingsManager.MainFormSettings.On_Tray_Middle_Click);
                    break;
            }
        }

        private void OpenMainWindow_Click(object sender, EventArgs e)
        {
            isInTrayOrMinimized = false;
            this.ForceActivate();
        }

        private void ExitApplication_Click(object sender, EventArgs e)
        {
            forceClose = true;
            Close();
        }

        #endregion

        #region MainForm events

        private void MainForm_Shown(object sender, EventArgs e)
        {
            UpdateTheme();
        }

        private void MainForm_HandleCreated(object sender, EventArgs e)
        {
            isHandleCreated = true;
            UpdateTheme();
            IsReady = true;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            CheckWindowState();
            CloseDropDowns();
        }

        private void MainForm_LostFocus(object sender, EventArgs e)
        {
            CheckWindowState();
            CloseDropDowns();
        }

        private void MainForm_GotFocus(object sender, EventArgs e)
        {
            CheckWindowState();
            CloseDropDowns();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && SettingsManager.MainFormSettings.Hide_In_Tray_On_Close && !forceClose)
            {
                isInTrayOrMinimized = true;
                e.Cancel = true;
                Hide();
            }
        }

        #endregion

        #region Settings / Styles events

        private void UpdateSettings(object sender, EventArgs e)
        {
            TopMost = SettingsManager.MainFormSettings.Always_On_Top;
            niTrayIcon.Visible = SettingsManager.MainFormSettings.Show_In_Tray;
            
            foreach(Form a in Application.OpenForms)
            {
                a.TopMost = SettingsManager.MainFormSettings.Always_On_Top;
            }
        }

        private void ApplicationStyles_UpdateSylesEvent(object sender, EventArgs e)
        {
            UpdateTheme();
        }

        #endregion

        #region Control Events

        private void PbPreviewBox_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    this.lvListView.UnselectAll();
                    this.scMain.Panel2Collapsed = true;
                    this.scMain.Panel2.Hide();
                    this.pbPreviewBox.Reset();
                    break;
            }
        }

        private void LvListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
                if (e.IsSelected)
                {
                    this.scMain.Panel2.Show();
                    this.scMain.Panel2Collapsed = false;
                    this.pbPreviewBox.SetImage((string)e.Item.Tag);
                }
                else
                {
                    this.scMain.Panel2Collapsed = true;
                    this.scMain.Panel2.Hide();
                    this.pbPreviewBox.Reset();
                }
        }

        private void toolStripDropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case ToolStripDropDownCloseReason.AppFocusChange:
                    if (!forceDropDownClose)
                        e.Cancel = true;
                    else
                        forceDropDownClose = false;
                    break;

                case ToolStripDropDownCloseReason.CloseCalled:
                    if (!forceDropDownClose)
                        e.Cancel = true;
                    else
                        forceDropDownClose = false;
                    break;
            }
        }

        #endregion

        #region other events

        public void ImageSaved_Event(object sender, ImageSavedEvent e)
        {
            string[] row1 = {
                e.FileInfo.Extension,                                   // file type
                $"{e.Dimensions.Width}, {e.Dimensions.Height}",     // dimensions
                Helpers.SizeSuffix(e.SizeInBytes),                         // size
                File.GetLastWriteTime(e.FullName).ToString()   // date modified
            };

            ListViewItem item = new ListViewItem() { Text = e.Name, Tag = e.FullName };

            item.SubItems.AddRange(row1);

            if (lvListView.Items.Count <= 0)
            {
                lvListView.AddItem(item);
            }
            else
            {
                lvListView.InsertItem(0, item);
            }
        }

        #endregion

        #region ChildForms

        private void ChildFormClosing(object sender, EventArgs e)
        {
            switch (((Form)sender).Text)
            {
                case "Styles":
                    stylesForm?.Dispose();
                    stylesForm = null;
                    break;
                case "Settings":
                    settingsForm?.Dispose();
                    settingsForm = null;
                    break;
                case "ColorPicker":
                    colorPickerForm?.Dispose();
                    colorPickerForm = null;
                    break;
                case "Qr Code":
                    qrCodeForm?.Dispose();
                    qrCodeForm = null;
                    break;
                case "HashCheck":
                    hashCheckForm?.Dispose();
                    hashCheckForm = null;
                    break;
                case "Regex":
                    regexForm?.Dispose();
                    regexForm = null;
                    break;
            }
        }

        private void ToolStripDropDownButton_Settings_Click(object sender, EventArgs e)
        {
            settingsForm?.Close();
            settingsForm?.Dispose();
            settingsForm = new SettingsForm();
            settingsForm.Owner = this;
            settingsForm.TopMost = SettingsManager.MainFormSettings.Always_On_Top;
            settingsForm.FormClosing += ChildFormClosing;
            settingsForm.Show();
        }

        private void ToolStripDropDownButton_Styles_Click(object sender, EventArgs e)
        {
            stylesForm?.Close();
            stylesForm?.Dispose();
            stylesForm = new StylesForm();
            stylesForm.Owner = this;
            stylesForm.TopMost = SettingsManager.MainFormSettings.Always_On_Top;
            stylesForm.FormClosing += ChildFormClosing;
            stylesForm.Show();
        }

        #endregion

        

        private async Task PrepareCaptureMenuAsync(ToolStripMenuItem tsmiWindow, EventHandler handlerWindow, ToolStripMenuItem tsmiMonitor, EventHandler handlerMonitor)
        {// taken from sharex source code
            tsmiWindow.DropDownItems.Clear();

            WindowsList windowsList = new WindowsList();
            List<WindowInfo> windows = null;

            await Task.Run(() =>
            {
                windows = windowsList.GetVisibleWindowsList();
            });

            if (windows != null)
            {
                foreach (WindowInfo window in windows)
                {
                    try
                    {
                        string title = window.Text.Truncate(50, "...");
                        ToolStripMenuItem tsmi = new ToolStripMenuItem(title);
                        tsmi.Tag = window;
                        tsmi.Click += handlerWindow;
                        try
                        {
                            using (Icon icon = window.Icon)
                            {
                                if (icon != null && icon.Width > 0 && icon.Height > 0)
                                {
                                    tsmi.Image = icon.ToBitmap();
                                }
                            }
                        }catch
                        {
                        }
                        

                        tsmiWindow.DropDownItems.Add(tsmi);
                    }
                    catch
                    {
                    }
                }
            }

            tsmiMonitor.DropDownItems.Clear();

            Screen[] screens = Screen.AllScreens;

            for (int i = 0; i < screens.Length; i++)
            {
                Screen screen = screens[i];
                string text = string.Format("{0}. {1}x{2}", i + 1, screen.Bounds.Width, screen.Bounds.Height);
                ToolStripItem tsi = tsmiMonitor.DropDownItems.Add(text);
                tsi.Tag = screen.Bounds;
                tsi.Click += handlerMonitor;
            }

            tsmiWindow.Invalidate();
            tsmiMonitor.Invalidate();
        }


        #region overrides

        // this hides the window before its shown if requested 
        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(allowShowDisplay ? value : allowShowDisplay);
            allowShowDisplay = true;
            UpdateTheme();
        }

        #endregion
    }
}
