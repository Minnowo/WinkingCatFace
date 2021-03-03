﻿using System;
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
        public SettingsForm settingsForm { get; private set; }
        public StylesForm stylesForm { get; private set; }


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
#if !DEBUG
            TopMost = MainFormSettings.alwaysOnTop;
#endif
            niTrayIcon.Visible = MainFormSettings.showInTray;

#region Capture dropdown buttons
            ToolStripDropDownButton_Capture.DropDown.Closing += toolStripDropDown_Closing;
            ToolStripDropDownButton_Capture.DropDownOpening += tsmiCapture_DropDownOpening;

            ToolStripMenuItem_region.Click += RegionCapture_Click;
            ToolStripMenuItem_fullscreen.Click += FullscreenCapture_Click;
            ToolStripMenuItem_lastRegion.Click += LastRegionCapture_Click;
            ToolStripMenuItem_captureCursor.Click += CursorCapture_Click;
#endregion

#region Clips dropdown buttons
            ToolStripDropDownButton_Clips.DropDown.Closing += toolStripDropDown_Closing;

            ToolStripMenuItem_newClip.Click += NewClip_Click;
            ToolStripMenuItem_clipFromClipboard.Click += ClipFromClipboard_Click;
            ToolStripMenuItem_clipFromFile.Click += ClipFromFile_Click;
            ToolStripMenuItem_createClipAfterRegionCapture.Click += CreateClipAfterRegionCapture_Click;
#endregion

#region Tools dropdown buttons
            ToolStripDropDownButton_Tools.DropDown.Closing += toolStripDropDown_Closing;

            ToolStripDropDownButton_screenColorPicker.Click += ScreenColorPicker_Click;
#endregion

#region Tray icon
            trayClickTimer = new System.Windows.Forms.Timer();
            trayClickTimer.Tick += TrayClickTimer_Interval;
            niTrayIcon.MouseUp += NiTrayIcon_MouseClick1Up;
            cmTray.Opening += tsmiCapture_DropDownOpening;

            // capture
            regionToolStripMenuItem.Click += RegionCapture_Click;
            fullscreenToolStripMenuItem.Click += FullscreenCapture_Click;
            lastRegionToolStripMenuItem.Click += LastRegionCapture_Click;
            captureCursorToolStripMenuItem.Click += CursorCapture_Click;

            // clip
            newClipToolStripMenuItem.Click += NewClip_Click;
            clipFromClipboardToolStripMenuItem.Click += ClipFromClipboard_Click;
            clipFromFileToolStripMenuItem.Click += ClipFromFile_Click;

            // other
            settingsToolStripMenuItem.Click += ToolStripDropDownButton_Settings_Click;
            openMainWindowToolStripMenuItem.Click += OpenMainWindow_Click;
            exitToolStripMenuItem.Click += ExitApplication_Click;
#endregion

            HandleCreated += MainForm_HandleCreated;
            LostFocus += mainForm_LostFocus;
            GotFocus += mainForm_GotFocus;
            Resize += MainForm_Resize;

            //ImageHandler.CaptureEvent += AfterCaptureEvent;
            ImageHandler.ImageSaved += ImageSaved_Event;

            lvListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            ResumeLayout();

            MainFormSettings.SettingsChangedEvent += UpdateSettings;
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
            if(ApplicationStyles.useImersiveDarkMode && isHandleCreated)
            {
                NativeMethods.UseImmersiveDarkMode(Handle, true);
            }

            this.Text = ApplicationStyles.mainFormName;
            this.BackColor = ApplicationStyles.backgroundColor;
            this.DisplayPanel.BackColor = ApplicationStyles.backgroundColor;
            tsMain.Renderer = new ToolStripCustomRenderer();

            cmTray.Renderer = new ToolStripCustomRenderer();
            cmTray.Opacity = ApplicationStyles.contextMenuOpacity;
        }

        public void ImageSaved_Event(object sender, ImageSavedEvent e)
        {
            Console.WriteLine("nyah");
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

       /* public void AfterCaptureEvent(object sender, LastRegionCaptureInfo e)
        {

        }*/

#region Capture dropdown buttons
        private async void tsmiCapture_DropDownOpening(object sender, EventArgs e)
        {
            if(sender.GetType().Name == "ToolStripDropDownButton") 
                await PrepareCaptureMenuAsync(ToolStripMenuItem_window, WindowItems_Click, ToolStripMenuItem_monitor, MonitorItems_Click);
            else
                await PrepareCaptureMenuAsync(windowToolStripMenuItem, WindowItems_Click, monitorToolStripMenuItem, MonitorItems_Click);
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
                    Hide();
                }
            }

            Thread.Sleep(MainFormSettings.waitHideTime);
            TaskHandler.CaptureWindow(win);
            //Thread.Sleep(MainFormSettings.waitHideTime);

            Show();
        }

        private void MonitorItems_Click(object sender, EventArgs e)
        {
            forceDropDownClose = true;
            mainForm_LostFocus(null, null);

            ToolStripItem tsi = (ToolStripItem)sender;

            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                Hide();
            }
            Thread.Sleep(MainFormSettings.waitHideTime);
            ImageHandler.Save(img: ScreenShotManager.CaptureRectangle((Rectangle)tsi.Tag));
            //Thread.Sleep(MainFormSettings.waitHideTime);

            Show();
        }

        private void RegionCapture_Click(object sender, EventArgs e)
        {
            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                Hide();
                Thread.Sleep(MainFormSettings.waitHideTime);
            }
            TaskHandler.ExecuteTask(Tasks.RegionCapture);

            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                //Thread.Sleep(MainFormSettings.waitHideTime);
                Show();
            }
        }
        private void MonitorCapture_Click(object sender, EventArgs e)
        {
            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                Hide();
                Thread.Sleep(MainFormSettings.waitHideTime);
            }
            TaskHandler.ExecuteTask(Tasks.CaptureActiveMonitor);

            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                //Thread.Sleep(MainFormSettings.waitHideTime);
                Show();
            }
        }
        private void FullscreenCapture_Click(object sender, EventArgs e)
        {
            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                Hide();
                Thread.Sleep(MainFormSettings.waitHideTime);
            }
            TaskHandler.ExecuteTask(Tasks.CaptureFullScreen);

            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                //Thread.Sleep(MainFormSettings.waitHideTime);
                Show();
            }
        }
        private void LastRegionCapture_Click(object sender, EventArgs e)
        {
            if (MainFormSettings.hideMainFormOnCapture)
            {
                Hide();
                Thread.Sleep(MainFormSettings.waitHideTime);
            }
            TaskHandler.ExecuteTask(Tasks.CaptureLastRegion);

            if (MainFormSettings.hideMainFormOnCapture)
            {
                //Thread.Sleep(MainFormSettings.waitHideTime);
                Show();
            }
        }
        private void CursorCapture_Click(object sender, EventArgs e)
        {
            if (ToolStripMenuItem_captureCursor.Checked || captureCursorToolStripMenuItem.Checked)
            {
                ScreenShotManager.captureCursor = true;
                ToolStripMenuItem_captureCursor.Checked = true;
                captureCursorToolStripMenuItem.Checked = true;
            }
            else
            {
                ScreenShotManager.captureCursor = false;
                ToolStripMenuItem_captureCursor.Checked = false;
                captureCursorToolStripMenuItem.Checked = false;

            }
        }
#endregion

#region Clips dropdown buttons
        private void NewClip_Click(object sender, EventArgs e)
        {
            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                Hide();
                Thread.Sleep(MainFormSettings.waitHideTime);
            }
            TaskHandler.ExecuteTask(Tasks.NewClipFromRegionCapture);

            if (MainFormSettings.hideMainFormOnCapture && !isInTrayOrMinimized)
            {
                Thread.Sleep(MainFormSettings.waitHideTime);
                Show();
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
            Helpers.ForceActivate(this);
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
            ToolStripDropDownButton_Capture.DropDown.Close();
            ToolStripDropDownButton_Clips.DropDown.Close();
            ToolStripDropDownButton_Tools.DropDown.Close();
        }

        private void mainForm_GotFocus(object sender, EventArgs e)
        {
            MainForm_Resize(null, EventArgs.Empty); // just to check the window state
            forceDropDownClose = true;
            ToolStripDropDownButton_Capture.DropDown.Close();
            ToolStripDropDownButton_Clips.DropDown.Close();
            ToolStripDropDownButton_Tools.DropDown.Close();
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

        private void ToolStripDropDownButton_Settings_Click(object sender, EventArgs e)
        {
            settingsForm?.Close();
            settingsForm?.Dispose();
            settingsForm = new SettingsForm();
            settingsForm.Owner = this;
            settingsForm.TopMost = MainFormSettings.alwaysOnTop;
            settingsForm.Show();
        }

        private void ToolStripDropDownButton_Styles_Click(object sender, EventArgs e)
        {
            stylesForm?.Close();
            stylesForm?.Dispose();
            stylesForm = new StylesForm();
            stylesForm.Owner = this;
            stylesForm.TopMost = MainFormSettings.alwaysOnTop;
            stylesForm.Show();
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(allowShowDisplay ? value : allowShowDisplay);
            allowShowDisplay = true;
        }


    }
}
