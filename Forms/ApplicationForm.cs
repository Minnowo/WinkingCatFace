using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
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
        private Image image { get; set; } = null;
        private System.Windows.Forms.Timer trayClickTimer { get; set; }
        public SettingsForm settingsForm { get; private set; } = null;
        public StylesForm stylesForm { get; private set; } = null;
        public ColorPickerForm colorPickerForm { get; private set; } = null;
        public BarcodeForm qrCodeForm { get; private set; } = null;
        public HashCheckForm hashCheckForm { get; private set; } = null;

        private int trayClickCount = 0;
        private bool forceClose = false;
        private bool allowShowDisplay = !MainFormSettings.startInTray;
        private bool isInTrayOrMinimized = MainFormSettings.startInTray;
        private bool forceDropDownClose = false;
        private bool isHandleCreated = false;

        public ApplicationForm()
        {
            InitializeComponent();
            SuspendLayout();
            this.MaximizeBox = false;
#if !DEBUG
            TopMost = MainFormSettings.alwaysOnTop;
#endif
            niTrayIcon.Visible = MainFormSettings.showInTray;

#region Capture dropdown buttons
            tsddbToolStripDropDownButton_Capture.DropDown.Closing += toolStripDropDown_Closing;
            tsddbToolStripDropDownButton_Capture.DropDownOpening += tsmiCapture_DropDownOpening;

            tsmiToolStripMenuItem_region.Click += RegionCapture_Click;
            tsmiToolStripMenuItem_fullscreen.Click += FullscreenCapture_Click;
            tsmiToolStripMenuItem_lastRegion.Click += LastRegionCapture_Click;
            tsmiToolStripMenuItem_captureCursor.Click += CursorCapture_Click;
#endregion

#region Clips dropdown buttons
            tsddbToolStripDropDownButton_Clips.DropDown.Closing += toolStripDropDown_Closing;

            tsmiToolStripMenuItem_newClip.Click += NewClip_Click;
            tsmiToolStripMenuItem_clipFromClipboard.Click += ClipFromClipboard_Click;
            tsmiToolStripMenuItem_clipFromFile.Click += ClipFromFile_Click;
            tsmiToolStripMenuItem_createClipAfterRegionCapture.Click += CreateClipAfterRegionCapture_Click;
#endregion

#region Tools dropdown buttons
            tsddbToolStripDropDownButton_Tools.DropDown.Closing += toolStripDropDown_Closing;

            tsmiToolStripDropDownButton_screenColorPicker.Click += ScreenColorPicker_Click;
            tsmiToolStripDropDownButton_ColorPicker.Click += ColorPicker_Click;
            tsmiToolStripDropDownButton_QrCode.Click += QrCode_Click;
            tsmiToolStripDropDownButton_HashCheck.Click += HashCheck_Click;
#endregion

            #region Tray icon
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

            // other
            tsmiStylesToolStripMenuItem.Click += ToolStripDropDownButton_Styles_Click;
            tsmiSettingsToolStripMenuItem.Click += ToolStripDropDownButton_Settings_Click;
            tsmiOpenMainWindowToolStripMenuItem.Click += OpenMainWindow_Click;
            tsmiExitToolStripMenuItem.Click += ExitApplication_Click;
#endregion

            HandleCreated += MainForm_HandleCreated;
            LostFocus += mainForm_LostFocus;
            GotFocus += mainForm_GotFocus;
            Resize += MainForm_Resize;

            ImageHandler.ImageSaved += ImageSaved_Event;
            ApplicationStyles.UpdateStylesEvent += ApplicationStyles_UpdateSylesEvent;

            lvListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            ResumeLayout();

            MainFormSettings.SettingsChangedEvent += UpdateSettings;
            lvListView.ItemSelectionChanged += LvListView_ItemSelectionChanged;

            this.pbPreviewBox.previewOnClick = true;
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

        private void ApplicationStyles_UpdateSylesEvent(object sender, EventArgs e)
        {
            UpdateTheme();
        }

        private void MainForm_HandleCreated(object sender, EventArgs e)
        {
            isHandleCreated = true;
            UpdateTheme();
        }

        private void UpdateSettings(object sender, EventArgs e)
        {
            TopMost = MainFormSettings.alwaysOnTop;
            niTrayIcon.Visible = MainFormSettings.showInTray;
        }

        public void UpdateTheme()
        {
            if(ApplicationStyles.currentStyle.mainFormStyle.useImersiveDarkMode && isHandleCreated)
            {
                NativeMethods.UseImmersiveDarkMode(Handle, true);
                this.Icon = Properties.Resources._3white;
            }
            else
            {
                NativeMethods.UseImmersiveDarkMode(Handle, false);
                this.Icon = Properties.Resources._3black;
            }

            this.Text = "";// ApplicationStyles.mainFormName;
            this.BackColor = ApplicationStyles.currentStyle.mainFormStyle.backgroundColor;
            tsMain.Renderer = new ToolStripCustomRenderer();

            cmTray.Renderer = new ToolStripCustomRenderer();
            cmTray.Opacity = ApplicationStyles.currentStyle.mainFormStyle.contextMenuOpacity;

            lvListView.ForeColor = ApplicationStyles.currentStyle.mainFormStyle.textColor;
            Refresh();
        }

        public void ImageSaved_Event(object sender, ImageSavedEvent e)
        {
            string[] row1 = { e.info.Extension,  // name
                $"{e.dimensions.Width}, {e.dimensions.Height}", // dimensions
                Helpers.SizeSuffix(e.size), // size
                File.GetLastWriteTime(e.info.FullName).ToString() }; // date modified

            ListViewItem item = new ListViewItem() { Text = e.info.Name, Tag = e.info.FullName};

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
            mainForm_LostFocus(null, null);

            ToolStripItem tsi = (ToolStripItem)sender;
            WindowInfo win = (WindowInfo)tsi.Tag;

            if(Handle != win.Handle)
            {
                win.Activate();
                if(win.IsMinimized)
                    win.Restore();

                if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
                {
                    HideAll();
                }
            }

            Thread.Sleep(MainFormSettings.waitHideTime);
            TaskHandler.CaptureWindow(win);

            ShowAll();
        }

        private void MonitorItems_Click(object sender, EventArgs e)
        {
            forceDropDownClose = true;
            mainForm_LostFocus(null, null);

            ToolStripItem tsi = (ToolStripItem)sender;

            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                HideAll();
            }
            Thread.Sleep(MainFormSettings.waitHideTime);

            using (Bitmap img = ScreenShotManager.CaptureRectangle((Rectangle)tsi.Tag))
            {
                ImageHandler.Save(img: img);
                if (RegionCaptureOptions.autoCopyImage)
                {
                    ClipboardHelpers.CopyImageDefault(img);
                }
            }

            ShowAll();
        }

        private void RegionCapture_Click(object sender, EventArgs e)
        {
            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                HideAll();
                Thread.Sleep(MainFormSettings.waitHideTime);
            }
            TaskHandler.ExecuteTask(Tasks.RegionCapture);

            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                ShowAll();
            }
        }
        private void MonitorCapture_Click(object sender, EventArgs e)
        {
            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                HideAll();
                Thread.Sleep(MainFormSettings.waitHideTime);
            }
            TaskHandler.ExecuteTask(Tasks.CaptureActiveMonitor);

            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                ShowAll();
            }
        }
        private void FullscreenCapture_Click(object sender, EventArgs e)
        {
            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                HideAll();
                Thread.Sleep(MainFormSettings.waitHideTime);
            }
            TaskHandler.ExecuteTask(Tasks.CaptureFullScreen);

            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                ShowAll();
            }
        }
        private void LastRegionCapture_Click(object sender, EventArgs e)
        {
            if (MainFormSettings.hideMainFormOnCapture)
            {
                HideAll();
                Thread.Sleep(MainFormSettings.waitHideTime);
            }
            TaskHandler.ExecuteTask(Tasks.CaptureLastRegion);

            if (MainFormSettings.hideMainFormOnCapture)
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
            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                HideAll();
                Thread.Sleep(MainFormSettings.waitHideTime);
            }
            TaskHandler.ExecuteTask(Tasks.NewClipFromRegionCapture);

            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                ShowAll();
            }
        }
        private void ClipFromClipboard_Click(object sender, EventArgs e)
        {
            TaskHandler.ExecuteTask(Tasks.NewClipFromClipboard);
        }
        private void ClipFromFile_Click(object sender, EventArgs e)
        {
            TaskHandler.ExecuteTask(Tasks.NewClipFromFile);
        }
        private void CreateClipAfterRegionCapture_Click(object sender, EventArgs e)
        {

        }
#endregion

#region Tools dropdown buttons
        private void ScreenColorPicker_Click(object sender, EventArgs e)
        {
            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                Hide();
                Thread.Sleep(MainFormSettings.waitHideTime);
            }
            TaskHandler.ExecuteTask(Tasks.ScreenColorPicker);

            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                Thread.Sleep(MainFormSettings.waitHideTime);
                Show();
            }
        }

        private void ColorPicker_Click(object sender, EventArgs e)
        {
            if(colorPickerForm != null)
            {
                colorPickerForm.ForceActivate();
            }
            else
            {
                colorPickerForm = new ColorPickerForm();
                colorPickerForm.FormClosing += ChildFormClosing;
                colorPickerForm.Show();
            }
        }

        private void QrCode_Click(object sender, EventArgs e) 
        {
            if(qrCodeForm != null)
            {
                qrCodeForm.ForceActivate();
            }
            else
            {
                qrCodeForm = new BarcodeForm();
                qrCodeForm.FormClosing += ChildFormClosing;
                qrCodeForm.Show();
            }
        }

        private void HashCheck_Click(object sender, EventArgs e)
        {
            if (hashCheckForm != null)
            {
                hashCheckForm.ForceActivate();
            }
            else
            {
                hashCheckForm = new HashCheckForm();
                hashCheckForm.FormClosing += ChildFormClosing;
                hashCheckForm.Show();
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

                TaskHandler.ExecuteTask(MainFormSettings.onTrayLeftClick);
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

                        TaskHandler.ExecuteTask(MainFormSettings.onTrayDoubleLeftClick);
                    }
                    break;
                case MouseButtons.Middle:
                    TaskHandler.ExecuteTask(MainFormSettings.onTrayMiddleClick);
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

        private void MainForm_Resize(object sender, EventArgs e)
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

        private void mainForm_LostFocus(object sender, EventArgs e)
        {
            MainForm_Resize(null, EventArgs.Empty); // just to check the window state
            forceDropDownClose = true;
            tsddbToolStripDropDownButton_Capture.DropDown.Close();
            tsddbToolStripDropDownButton_Clips.DropDown.Close();
            tsddbToolStripDropDownButton_Tools.DropDown.Close();
        }

        private void mainForm_GotFocus(object sender, EventArgs e)
        {
            MainForm_Resize(null, EventArgs.Empty); // just to check the window state
            forceDropDownClose = true;
            tsddbToolStripDropDownButton_Capture.DropDown.Close();
            tsddbToolStripDropDownButton_Clips.DropDown.Close();
            tsddbToolStripDropDownButton_Tools.DropDown.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && MainFormSettings.minimizeToTray && !forceClose)
            {
                isInTrayOrMinimized = true;
                e.Cancel = true;
                Hide();
            }
        }
#endregion

        public void HideAll()
        {
            Hide();
            settingsForm?.Hide();
            stylesForm?.Hide();
        }

        public void ShowAll()
        {
            Show();
            settingsForm?.Show();
            stylesForm?.Show();
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
                        }catch(Exception e)
                        {
                            Logger.WriteException(e, "Exception failed to load window icon");
                        }
                        

                        tsmiWindow.DropDownItems.Add(tsmi);
                    }
                    catch (Exception e)
                    {
                        Logger.WriteException(e);
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
            }
        }

        private void ToolStripDropDownButton_Settings_Click(object sender, EventArgs e)
        {
            settingsForm?.Close();
            settingsForm?.Dispose();
            settingsForm = new SettingsForm();
            settingsForm.Owner = this;
            settingsForm.TopMost = MainFormSettings.alwaysOnTop;
            settingsForm.FormClosing += ChildFormClosing;
            settingsForm.Show();
        }

        private void ToolStripDropDownButton_Styles_Click(object sender, EventArgs e)
        {
            stylesForm?.Close();
            stylesForm?.Dispose();
            stylesForm = new StylesForm();
            stylesForm.Owner = this;
            stylesForm.TopMost = MainFormSettings.alwaysOnTop;
            stylesForm.FormClosing += ChildFormClosing;
            stylesForm.Show();
        }

        private void ShowMe()
        {
            isInTrayOrMinimized = false;
            this.ForceActivate();

            bool top = TopMost;
            TopMost = true;
            TopMost = top;
        }

        #region overrides

        // this hides the window before its shown if requested 
        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(allowShowDisplay ? value : allowShowDisplay);
            allowShowDisplay = true;
        }
        
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME) // will be posted if instance is running
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }

        #endregion
    }
}
