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
using System.Threading;
using WinkingCat.HelperLibs;
using WinkingCat.ScreenCaptureLib;
using WinkingCat.HotkeyLib;

namespace WinkingCat
{
    
    public partial class ApplicationForm : Form
    {
        private Image image { get; set; } = null;
        public SettingsForm settingsForm { get; private set; }

        public bool forceClose { get; set; } = false;
        private bool forceDropDownClose = false;
        public ApplicationForm()
        {
            InitializeComponent();
            FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
            LostFocus += mainForm_LostFocus;


            #region Capture dropdown buttons
            ToolStripDropDownButton_Capture.DropDown.Closing += toolStripDropDown_Closing;
            ToolStripDropDownButton_Capture.DropDownOpening += tsmiCapture_DropDownOpening;

            ToolStripMenuItem_region.Click += RegionCapture_Click;
            //ToolStripMenuItem_monitor.MouseHover += MonitorCapture_Click;
            //ToolStripMenuItem_window.Click += WindowCapture_Click;
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

            #region Style button
            ToolStripDropDownButton_Style.DropDown.Closing += toolStripDropDown_Closing;

            #endregion

            #region Settings button
            //ToolStripDropDownButton_Settings.DropDown.Closing += toolStripDropDown_Closing;

            #endregion

            #region Tray icon context menu
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
            openMainWindowToolStripMenuItem.Click += OpenMainWindow_Click;
            exitToolStripMenuItem.Click += ExitApplication_Click;
            #endregion
            HotkeyManager.UpdateHotkeys(HotkeyManager.GetDefaultHotkeyList(), true);
        }

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

                if (MainFormSettings.hideMainFormOnCapture)
                {
                    Hide();
                }
            }
            
            Task t = Task.Run(async delegate
            {
                await Task.Delay(MainFormSettings.waitHideTime);
                TaskHandler.CaptureWindow(win);
                await Task.Delay(MainFormSettings.waitHideTime);
            });

            t.Wait(); t.Dispose();
            Show();
        }

        private void MonitorItems_Click(object sender, EventArgs e)
        {
            forceDropDownClose = true;
            mainForm_LostFocus(null, null);

            ToolStripItem tsi = (ToolStripItem)sender;

            if (MainFormSettings.hideMainFormOnCapture)
            {
                Hide();
            }

            Task t = Task.Run(async delegate
            {
                await Task.Delay(MainFormSettings.waitHideTime);
                ImageHandler.Save(img : ScreenShotManager.CaptureRectangle((Rectangle)tsi.Tag));
                await Task.Delay(MainFormSettings.waitHideTime);
            });

            t.Wait(); t.Dispose();
            Show();
        }

        private void RegionCapture_Click(object sender, EventArgs e)
        {
            TaskHandler.ExecuteTask(Tasks.RegionCapture);
        }
        private void MonitorCapture_Click(object sender, EventArgs e)
        {
            TaskHandler.ExecuteTask(Tasks.CaptureActiveMonitor);
        }
        private void FullscreenCapture_Click(object sender, EventArgs e)
        {
            TaskHandler.ExecuteTask(Tasks.CaptureFullScreen);
        }
        private void LastRegionCapture_Click(object sender, EventArgs e)
        {
            TaskHandler.ExecuteTask(Tasks.CaptureLastRegion);
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
            TaskHandler.ExecuteTask(Tasks.NewClipFromRegionCapture);
        }
        private void ClipFromClipboard_Click(object sender, EventArgs e)
        {

        }
        private void ClipFromFile_Click(object sender, EventArgs e)
        {

        }
        private void CreateClipAfterRegionCapture_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Tools dropdown buttons
        private void ScreenColorPicker_Click(object sender, EventArgs e)
        {
            TaskHandler.ExecuteTask(Tasks.ScreenColorPicker);
        }
        #endregion

        #region tray icon context menu
        private void OpenMainWindow_Click(object sender, EventArgs e)
        {
            ForceActivate(this);
        }

        private void ExitApplication_Click(object sender, EventArgs e)
        {
            forceClose = true;
            Close();
        }
        #endregion

        #region MainForm events

        private void mainForm_LostFocus(object sender, EventArgs e)
        {
            forceDropDownClose = true;
            ToolStripDropDownButton_Capture.DropDown.Close();
            ToolStripDropDownButton_Clips.DropDown.Close();
            ToolStripDropDownButton_Tools.DropDown.Close();
            ToolStripDropDownButton_Style.DropDown.Close();
        }

        private void mainForm_GotFocus(object sender, EventArgs e)
        {
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && MainFormSettings.showInTray && !forceClose)
            {
                e.Cancel = true;
                Hide();
            }
        }
        #endregion

        #region main form functions
        public static void ForceActivate(Form form)
        {
            if (!form.IsDisposed)
            {
                if (!form.Visible)
                {
                    form.Show();
                }

                if (form.WindowState == FormWindowState.Minimized)
                {
                    form.WindowState = FormWindowState.Normal;
                }

                form.BringToFront();
                form.Activate();
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
            settingsForm.Show();
        }
    }
}
