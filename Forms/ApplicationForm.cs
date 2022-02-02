using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Threading;
using WinkingCat.HelperLibs;
using WinkingCat.Uploaders;

namespace WinkingCat
{

    public partial class ApplicationForm : BaseForm
    {
        List<BaseForm> _childrenHandles = new List<BaseForm>();

        private System.Windows.Forms.Timer trayClickTimer;

        private int trayClickCount = 0;

        private bool forceClose = false;
        private bool allowShowDisplay = !SettingsManager.MainFormSettings.Start_In_Tray;
        private bool isInTrayOrMinimized = SettingsManager.MainFormSettings.Start_In_Tray;
        private bool forceDropDownClose = false;
        private bool preventOverflow = false;

        public ApplicationForm()
        {
            InitializeComponent();
            SuspendLayout();
            preventOverflow = true;

            trayClickTimer = new System.Windows.Forms.Timer();

            tsmiToolStripMenuItem_captureCursor.Checked = SettingsManager.RegionCaptureSettings.Capture_Cursor;
            tsmiCaptureCursorToolStripMenuItem.Checked = SettingsManager.RegionCaptureSettings.Capture_Cursor;

#if !DEBUG
            MaximizeBox        = SettingsManager.MainFormSettings.Show_Maximize_Box;
            TopMost            = SettingsManager.MainFormSettings.Always_On_Top;
            niTrayIcon.Visible = SettingsManager.MainFormSettings.Show_In_Tray;
#endif

            RegisterEvents();

            preventOverflow = false;
            ResumeLayout();

            lvListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.pbPreviewBox.previewOnClick = true;
        }

        protected override void RegisterEvents()
        {
            base.RegisterEvents();
            
            tsddbToolStripDropDownButton_Capture.DropDown.Closing += toolStripDropDown_Closing;
            tsddbToolStripDropDownButton_Capture.DropDownOpening += tsmiCapture_DropDownOpening;

            tsmiToolStripMenuItem_region.Click += RegionCapture_Click;
            tsmiToolStripMenuItem_fullscreen.Click += FullscreenCapture_Click;
            tsmiToolStripMenuItem_lastRegion.Click += LastRegionCapture_Click;
            tsmiToolStripMenuItem_captureCursor.Click += CursorCapture_Click;

            tsddbToolStripDropDownButton_Clips.DropDown.Closing += toolStripDropDown_Closing;

            tsmiToolStripMenuItem_newClip.Click += NewClip_Click;
            tsmiToolStripMenuItem_clipFromClipboard.Click += ClipFromClipboard_Click;
            tsmiToolStripMenuItem_clipFromFile.Click += ClipFromFile_Click;

            tsddbToolStripDropDownButton_Tools.DropDown.Closing += toolStripDropDown_Closing;

            tsmiToolStripDropDownButton_screenColorPicker.Click += ScreenColorPicker_Click;
            tsmiToolStripDropDownButton_ColorPicker.Click += ColorPicker_Click;
            tsmiToolStripDropDownButton_QrCode.Click += QrCode_Click;
            tsmiToolStripDropDownButton_HashCheck.Click += HashCheck_Click;
            tsmiToolStripDropDownButton_Regex.Click += Regex_Click;

            // system tray stuff // 
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

            // timer 
            trayClickTimer.Tick += TrayClickTimer_Interval;
            niTrayIcon.MouseUp += NiTrayIcon_MouseClick1Up;
            cmTray.Opening += tsmiCapture_DropDownOpening;

            RegionCaptureHelper.ImageSaved += ImageSaved_Event;

            lvListView.ItemSelectionChanged += LvListView_ItemSelectionChanged;
            pbPreviewBox.pbMain.MouseClick += PbPreviewBox_MouseClick;

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
        }

        /// <summary>
        /// Show the main, settings, and styles form.
        /// </summary>
        public void ShowAll()
        {
            Show();
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
        public override void UpdateTheme()
        {
            SettingsManager.ApplyImmersiveDarkTheme(this, IsHandleCreated);

            tsMain.Renderer = new ToolStripCustomRenderer();

            cmTray.Renderer = new ToolStripCustomRenderer();
            cmTray.Opacity = SettingsManager.MainFormSettings.contextMenuOpacity;

            lvListView.ForeColor = SettingsManager.MainFormSettings.textColor;
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
                if (win.IsMinimized)
                {
                    win.Restore();
                }

                BaseForm bf = this._childrenHandles.FirstOrDefault(x => x.Handle == win.Handle);

                if(bf != null)
                {
                    bf.PreventHideNext = true;
                }
            }

            TaskHandler.CaptureWindow(win);
        }

        private void MonitorItems_Click(object sender, EventArgs e)
        {
            forceDropDownClose = true;
            MainForm_LostFocus(null, null);

            ToolStripItem tsi = (ToolStripItem)sender;

            RegionCaptureHelper.RequestFormsHide(false, true);

            using (Bitmap img = ScreenshotHelper.CaptureRectangle((Rectangle)tsi.Tag))
            {
                RegionCaptureHelper.Save(PathHelper.GetNewImageFileName(), img);
                if (SettingsManager.RegionCaptureSettings.Auto_Copy_Image)
                {
                    ClipboardHelper.CopyImage(img);
                }
            }

            RegionCaptureHelper.RequestFormsHide(true, false);
        }

        private void RegionCapture_Click(object sender, EventArgs e)
        {
            TaskHandler.ExecuteTask(Function.RegionCapture);
        }

        private void MonitorCapture_Click(object sender, EventArgs e)
        {
            TaskHandler.ExecuteTask(Function.CaptureActiveMonitor);
        }

        private void FullscreenCapture_Click(object sender, EventArgs e)
        {
            TaskHandler.ExecuteTask(Function.CaptureFullScreen);
        }

        private void LastRegionCapture_Click(object sender, EventArgs e)
        {
            TaskHandler.ExecuteTask(Function.CaptureLastRegion);
        }

        private void CursorCapture_Click(object sender, EventArgs e)
        {
            if (preventOverflow)
                return;

            preventOverflow = true;
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (tsmi == null)
                return;

            if (tsmi.Checked)
            {
                ScreenshotHelper.CaptureCursor = true;
                tsmiToolStripMenuItem_captureCursor.Checked = true;
                tsmiCaptureCursorToolStripMenuItem.Checked = true;
            }
            else
            {
                ScreenshotHelper.CaptureCursor = false;
                tsmiToolStripMenuItem_captureCursor.Checked = false;
                tsmiCaptureCursorToolStripMenuItem.Checked = false;
            }
            preventOverflow = false;
        }
#endregion

        #region Clips dropdown buttons
        private void NewClip_Click(object sender, EventArgs e)
        {
            TaskHandler.ExecuteTask(Function.NewClipFromRegionCapture);
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
            TaskHandler.ExecuteTask(Function.ScreenColorPicker);
        }

        internal void ColorPicker_Click(object sender, EventArgs e)
        {
            ColorPickerForm cpf = new ColorPickerForm();
            cpf.FormClosing += Childform_Closing;
            cpf.TopMost = SettingsManager.MainFormSettings.Always_On_Top;
            cpf.StartPosition = FormStartPosition.CenterScreen;
            cpf.Show();
            _childrenHandles.Add(cpf);
        }

        internal void QrCode_Click(object sender, EventArgs e) 
        {
            BarcodeForm cpf = new BarcodeForm();
            cpf.FormClosing += Childform_Closing;
            cpf.TopMost = SettingsManager.MainFormSettings.Always_On_Top;
            cpf.StartPosition = FormStartPosition.CenterScreen;
            cpf.Show();
            _childrenHandles.Add(cpf);
        }

        internal void HashCheck_Click(object sender, EventArgs e)
        {
            HashCheckForm cpf = new HashCheckForm();
            cpf.FormClosing += Childform_Closing;
            cpf.TopMost = SettingsManager.MainFormSettings.Always_On_Top;
            cpf.StartPosition = FormStartPosition.CenterScreen;
            cpf.Show();
            _childrenHandles.Add(cpf);
        }

        internal void Regex_Click(object sender, EventArgs e)
        {
            RegexForm cpf = new RegexForm();
            cpf.FormClosing += Childform_Closing;
            cpf.TopMost = SettingsManager.MainFormSettings.Always_On_Top;
            cpf.StartPosition = FormStartPosition.CenterScreen;
            cpf.Show();
            _childrenHandles.Add(cpf);
        }

        private void OCRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OCRForm cpf = new OCRForm();
            cpf.FormClosing += Childform_Closing;
            cpf.TopMost = SettingsManager.MainFormSettings.Always_On_Top;
            cpf.StartPosition = FormStartPosition.CenterScreen;
            cpf.Show();
            _childrenHandles.Add(cpf);
        }

        private void Childform_Closing(object sender, EventArgs e)
        {
            _childrenHandles.Remove((BaseForm)sender);
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

        public override void UpdateSettings()
        {
            TopMost = SettingsManager.MainFormSettings.Always_On_Top;
            niTrayIcon.Visible = SettingsManager.MainFormSettings.Show_In_Tray;
            MaximizeBox = SettingsManager.MainFormSettings.Show_Maximize_Box;
        }

        #endregion

        #region Control Events

        private void PbPreviewBox_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    this.lvListView.DeselectAll();
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
            /*string[] row1 = {
                e.FileInfo.Extension,                                   // file type
                $"{e.Dimensions.Width}, {e.Dimensions.Height}",     // dimensions
                Helper.SizeSuffix(e.SizeInBytes),                         // size
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
            }*/
        }

        #endregion

        #region ChildForms

        private void ToolStripDropDownButton_Settings_Click(object sender, EventArgs e)
        {
            using (SettingsForm sf = new SettingsForm())
            {
                sf.Owner = this;
                sf.TopMost = SettingsManager.MainFormSettings.Always_On_Top;
                sf.StartPosition = FormStartPosition.CenterScreen;
                sf.ShowDialog();
            }
        }

        private void ToolStripDropDownButton_Styles_Click(object sender, EventArgs e)
        {
            using (StylesForm sf = new StylesForm())
            {
                sf.Owner = this;
                sf.TopMost = SettingsManager.MainFormSettings.Always_On_Top;
                sf.StartPosition = FormStartPosition.CenterScreen;
                sf.ShowDialog();
            }
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
