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
using System.Text;
using WinkingCat.HelperLibs.Enums;

namespace WinkingCat
{

    public partial class ApplicationForm : BaseForm
    {
        List<BaseForm> _childrenHandles = new List<BaseForm>();

        private Task _loadImageThread;

        private TIMER _trayClickTimer = new TIMER();
        private TIMER _loadImageTimer = new TIMER();

        private int _trayClickCount = 0;

        private bool _forceClose = false;
        private bool _allowShowDisplay = !SettingsManager.MainFormSettings.Start_In_Tray;
        private bool _isInTrayOrMinimized = SettingsManager.MainFormSettings.Start_In_Tray;
        private bool _forceDropDownClose = false;
        private bool _preventOverflow = false;
        private bool _showingFullscreenImage = false;

        public ApplicationForm()
        {
            InitializeComponent();
            SuspendLayout();

            for(int i = 0; i < 7; i++)
            {
                FileSizeUnit fsu = (FileSizeUnit)i;
                ToolStripItem tsi = new ToolStripMenuItem();
                tsi.Text = EnumToString.FileSizeUnitToString(fsu);
                tsi.Tag = fsu;
                tsi.Click += FileSizeUnit_Click;
                copySizeToolStripMenuItem.DropDownItems.Add(tsi);
            }

            folderView1.ListView_.View = View.Details;
            folderView1.ListView_.Columns.Add(new ColumnHeader() { Name = "Filename", Text = "Filename", Width=500});
            folderView1.ListView_.Columns.Add(new ColumnHeader() { Name = "Size", Text = "Size", Width =30 });
            folderView1.CurrentDirectory = PathHelper.GetScreenshotFolder();

            _preventOverflow = true;

            tsmiToolStripMenuItem_captureCursor.Checked = SettingsManager.RegionCaptureSettings.Capture_Cursor;
            tsmiCaptureCursorToolStripMenuItem.Checked = SettingsManager.RegionCaptureSettings.Capture_Cursor;

            comboBox1.Items.Add(System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor);
            comboBox1.Items.Add(System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);
            comboBox1.Items.Add(System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear);
            comboBox1.SelectedItem = SettingsManager.MiscSettings.Default_Interpolation_Mode;

            comboBox2.Items.Add(WinkingCat.HelperLibs.Controls.DrawMode.ActualSize);
            comboBox2.Items.Add(WinkingCat.HelperLibs.Controls.DrawMode.FitImage);
            comboBox2.Items.Add(WinkingCat.HelperLibs.Controls.DrawMode.ScaleImage);
            comboBox2.SelectedItem = SettingsManager.MiscSettings.Default_Draw_Mode;

            _trayClickTimer.SetInterval(SystemInformation.DoubleClickTime + 1000);
            _loadImageTimer.SetInterval(250);

#if !DEBUG
            MaximizeBox        = SettingsManager.MainFormSettings.Show_Maximize_Box;
            TopMost            = SettingsManager.MainFormSettings.Always_On_Top;
            niTrayIcon.Visible = SettingsManager.MainFormSettings.Show_In_Tray;
            
#endif
            toolStripButton1.Checked = TopMost;
            imageDisplay1.ClearImagePathOnReplace = true;
            imageDisplay1.DisposeImageOnReplace = true;
            imageDisplay1.ResetOffsetOnRightClick = false;


            RegisterEvents();

            _preventOverflow = false;
            ResumeLayout();
            // this.pbPreviewBox.previewOnClick = true;
        }

        protected override void RegisterEvents()
        {
            base.RegisterEvents();

            folderView1.ListView_.MouseClick += ListView__MouseClick;

            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
            imageDisplay1.ImageChanged += ImageDisplay1_ImageChanged;
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
            _trayClickTimer.Tick += TrayClickTimer_Interval;
            niTrayIcon.MouseUp += NiTrayIcon_MouseClick1Up;
            cmTray.Opening += tsmiCapture_DropDownOpening;

            _loadImageTimer.Tick += LoadImageTimer_Tick;
            folderView1.ListView_.SelectedIndexChanged += LvListView_ItemSelectionChanged;

            //lvListView.ItemSelectionChanged += LvListView_ItemSelectionChanged;
            // pbPreviewBox.pbMain.MouseClick += PbPreviewBox_MouseClick;

        }

        
        public void CopySelectedItemFilename()
        {
            if (folderView1.ListView_.SelectedIndices.Count < 2)
            {
                if (folderView1.ListView_.FocusedItem != null)
                {
                    if (folderView1.ListView_.FocusedItem.Tag is DirectoryInfo)
                    {
                        ClipboardHelper.CopyStringDefault(((DirectoryInfo)(folderView1.ListView_.FocusedItem.Tag)).Name);
                    }
                    else if (folderView1.ListView_.FocusedItem.Tag is FileInfo)
                    {
                        ClipboardHelper.CopyStringDefault(((FileInfo)(folderView1.ListView_.FocusedItem.Tag)).Name);
                    }
                }
                return;
            }

            StringBuilder paths = new StringBuilder();

            int i = 0;
            foreach (int ii in folderView1.ListView_.SelectedIndices)
            {
                if (folderView1.ListView_.Items[ii].Tag is DirectoryInfo)
                {
                    paths.AppendLine(((DirectoryInfo)(folderView1.ListView_.Items[ii].Tag)).Name);
                }
                else if (folderView1.ListView_.Items[ii].Tag is FileInfo)
                {
                    paths.AppendLine(((FileInfo)(folderView1.ListView_.Items[ii].Tag)).Name);
                }
                i++;
            }
            ClipboardHelper.CopyStringDefault(paths.ToString());
        }
        public void CopySelectedItemPath()
        {
            if (folderView1.ListView_.SelectedIndices.Count < 2)
            {
                if (folderView1.ListView_.FocusedItem != null)
                {
                    if (folderView1.ListView_.FocusedItem.Tag is DirectoryInfo)
                    {
                        ClipboardHelper.CopyStringDefault(((DirectoryInfo)(folderView1.ListView_.FocusedItem.Tag)).FullName);
                    }
                    else if (folderView1.ListView_.FocusedItem.Tag is FileInfo)
                    {
                        ClipboardHelper.CopyStringDefault(((FileInfo)(folderView1.ListView_.FocusedItem.Tag)).FullName);
                    }
                }
                return;
            }

            StringBuilder paths = new StringBuilder();

            int i = 0;
            foreach (int ii in folderView1.ListView_.SelectedIndices)
            {
                if (folderView1.ListView_.Items[ii].Tag is DirectoryInfo)
                {
                    paths.AppendLine(((DirectoryInfo)(folderView1.ListView_.Items[ii].Tag)).FullName);
                }
                else if (folderView1.ListView_.Items[ii].Tag is FileInfo)
                {
                    paths.AppendLine(((FileInfo)(folderView1.ListView_.Items[ii].Tag)).FullName);
                }
                i++;
            }
            ClipboardHelper.CopyStringDefault(paths.ToString());
        }

        /// <summary>
        /// Copies the file size in the given unit for all selected listview items.
        /// </summary>
        /// <param name="size">The size unit.</param>
        /// <param name="decimalPlaces">The number of decimal places.</param>
        public void CopySelectedItemsSize(FileSizeUnit size, int decimalPlaces = 1)
        {
            FileInfo info;

            if (folderView1.ListView_.SelectedIndices.Count < 2)
            {
                if (folderView1.ListView_.FocusedItem != null)
                {
                    if (folderView1.ListView_.FocusedItem.Tag is DirectoryInfo)
                    {
                        ClipboardHelper.CopyStringDefault("Unknown Folder Size");
                        return;
                    }
                    else if (folderView1.ListView_.FocusedItem.Tag is FileInfo)
                    {
                        info = (FileInfo)(folderView1.ListView_.FocusedItem.Tag);
                        ClipboardHelper.CopyStringDefault(Helper.SizeSuffix(info.Length, size, decimalPlaces));
                    }
                }
                return;
            }

            StringBuilder paths = new StringBuilder();

            int i = 0;
            foreach (int ii in folderView1.ListView_.SelectedIndices)
            {
                if (folderView1.ListView_.Items[ii].Tag is DirectoryInfo)
                {
                    paths.AppendLine("Unknown Folder Size");
                    return;
                }
                else if (folderView1.ListView_.Items[ii].Tag is FileInfo)
                {
                    info = (FileInfo)(folderView1.ListView_.Items[ii].Tag);
                    ClipboardHelper.CopyStringDefault(Helper.SizeSuffix(info.Length, size, decimalPlaces));
                    paths.AppendLine(Helper.SizeSuffix(info.Length, size, decimalPlaces));
                }
                i++;
            }
            ClipboardHelper.CopyStringDefault(paths.ToString());
        }

        private void ListView__MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    listViewContextMenu.Show(folderView1.ListView_, e.Location);
                    break;
            }
        }

        private void ImageDisplay1_ImageChanged()
        {
            textBox1.Text = "";

            if (imageDisplay1.Image == null)
                return;

            textBox1.Text = string.Format("{0} x {1}", imageDisplay1.Image.Width, imageDisplay1.Image.Height);
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_preventOverflow)
                return;

            imageDisplay1.DrawMode = (WinkingCat.HelperLibs.Controls.DrawMode)comboBox2.SelectedItem;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_preventOverflow)
                return;

            imageDisplay1.InterpolationMode = (System.Drawing.Drawing2D.InterpolationMode)comboBox1.SelectedItem;
        }

        /// <summary>
        /// Force close all the dropdowns.
        /// </summary>
        public void CloseDropDowns()
        {
            _forceDropDownClose = true;
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
                    _isInTrayOrMinimized = true;
                    break;
                case FormWindowState.Maximized:
                case FormWindowState.Normal:
                    _isInTrayOrMinimized = false;
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

            imageDisplayContextMenu.Renderer = new ToolStripCustomRenderer();
            imageDisplayContextMenu.Opacity = SettingsManager.MainFormSettings.contextMenuOpacity;

            listViewContextMenu.Renderer = new ToolStripCustomRenderer();
            listViewContextMenu.Opacity = SettingsManager.MainFormSettings.contextMenuOpacity;

            //lvListView.ForeColor = SettingsManager.MainFormSettings.textColor;
            ApplicationStyles.ApplyCustomThemeToControl(scMain.Panel2);
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
            _forceDropDownClose = true;
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
            _forceDropDownClose = true;
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
            if (_preventOverflow)
                return;

            _preventOverflow = true;
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
            _preventOverflow = false;
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
            if (_trayClickCount == 1)
            {
                _trayClickCount = 0;
                _trayClickTimer.Stop();

                TaskHandler.ExecuteTask(SettingsManager.MainFormSettings.On_Tray_Left_Click);
            }
        }

        private void NiTrayIcon_MouseClick1Up(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    _trayClickCount++;

                    if (_trayClickCount == 1)
                    {
                        _trayClickTimer.Start();
                    }
                    else
                    {
                        _trayClickCount = 0;
                        _trayClickTimer.Stop();

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
            _isInTrayOrMinimized = false;
            this.ForceActivate();
        }

        private void ExitApplication_Click(object sender, EventArgs e)
        {
            _forceClose = true;
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
            if (e.CloseReason == CloseReason.UserClosing && SettingsManager.MainFormSettings.Hide_In_Tray_On_Close && !_forceClose)
            {
                _isInTrayOrMinimized = true;
                e.Cancel = true;
                Hide();
            }
        }

        private void LoadImageTimer_Tick(object sender, EventArgs e)
        {
            _loadImageTimer.Stop();

            if (folderView1.SelectedIndex == -1)
                return;

            if(folderView1.ListView_.Items[folderView1.SelectedIndex].Tag is FileInfo)
            {
                FileInfo f = folderView1.ListView_.Items[folderView1.SelectedIndex].Tag as FileInfo;

                if (!File.Exists(f.FullName))
                    return;

                if (imageDisplay1.ImagePath != null && f.FullName == imageDisplay1.ImagePath.FullName)
                    return;

                if (_loadImageThread != null && _loadImageThread.Status == TaskStatus.Running)
                    return;

                _loadImageThread?.Dispose();
                _loadImageThread = imageDisplay1.TryLoadImageAsync(f.FullName);
            }
        }

        #endregion

        #region Settings / Styles events

        public override void UpdateSettings()
        {
            TopMost = SettingsManager.MainFormSettings.Always_On_Top;
            niTrayIcon.Visible = SettingsManager.MainFormSettings.Show_In_Tray;
            MaximizeBox = SettingsManager.MainFormSettings.Show_Maximize_Box;
            imageDisplay1.CellColor1 = SettingsManager.MainFormSettings.imageDisplayBG1;
            imageDisplay1.CellColor2 = SettingsManager.MainFormSettings.imageDisplayBG2;
            imageDisplay1.InterpolationMode = SettingsManager.MiscSettings.Default_Interpolation_Mode;
            imageDisplay1.DrawMode = SettingsManager.MiscSettings.Default_Draw_Mode;
            folderView1.FileSortOrder = SettingsManager.MainFormSettings.FileSortOrder;
            folderView1.FolderSortOrder = SettingsManager.MainFormSettings.FolderSortOrder;

            if (SettingsManager.MainFormSettings.Show_Image_Display_Color_1_Only)
            {
                imageDisplay1.CellColor2 = imageDisplay1.CellColor1;
            }
            
            UpdateImageDisplayContextMenuIcons();
        }

        #endregion

        #region Control Events


        private void LvListView_ItemSelectionChanged(object sender, EventArgs e)
        {
            _loadImageTimer.Stop();
            _loadImageTimer.Start();
        }

        private void toolStripDropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case ToolStripDropDownCloseReason.AppFocusChange:
                    if (!_forceDropDownClose)
                        e.Cancel = true;
                    else
                        _forceDropDownClose = false;
                    break;

                case ToolStripDropDownCloseReason.CloseCalled:
                    if (!_forceDropDownClose)
                        e.Cancel = true;
                    else
                        _forceDropDownClose = false;
                    break;
            }
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
            base.SetVisibleCore(_allowShowDisplay ? value : _allowShowDisplay);
            _allowShowDisplay = true;
            UpdateTheme();
        }


        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            imageDisplay1.Image = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (imageDisplay1.Image == null || _showingFullscreenImage)
                return;

            _showingFullscreenImage = true;
            this.imageDisplay1.Enabled = false;
            
            ImageViewerForm.ShowImage(this.imageDisplay1.Image);

            this.imageDisplay1.Enabled = true;
            _showingFullscreenImage = false;
        }

        private void copyImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageDisplay1.Image == null)
                return;

            ClipboardHelper.CopyImage(imageDisplay1.Image);
        }

        private void useBackColor1OnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (useBackColor1OnlyToolStripMenuItem.Checked)
            {
                SettingsManager.MainFormSettings.Show_Image_Display_Color_1_Only = true;
                imageDisplay1.CellColor2 = imageDisplay1.CellColor1;
            }
            else
            {
                SettingsManager.MainFormSettings.Show_Image_Display_Color_1_Only = false;
                imageDisplay1.CellColor2 = SettingsManager.MainFormSettings.imageDisplayBG2;
            }
        }

        private void setBackColor1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ColorPickerForm.PickColorDialogue(out Color newColor, SettingsManager.MainFormSettings.imageDisplayBG1))
            {
                SettingsManager.MainFormSettings.imageDisplayBG1 = newColor;
                imageDisplay1.CellColor1 = newColor;

                if (SettingsManager.MainFormSettings.Show_Image_Display_Color_1_Only)
                {
                    imageDisplay1.CellColor2 = newColor;
                }

                UpdateImageDisplayContextMenuIcons();
            }
        }

        private void setBackColor2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ColorPickerForm.PickColorDialogue(out Color newColor, SettingsManager.MainFormSettings.imageDisplayBG2))
            {
                SettingsManager.MainFormSettings.imageDisplayBG2 = newColor;
                
                if (!SettingsManager.MainFormSettings.Show_Image_Display_Color_1_Only)
                {
                    imageDisplay1.CellColor2 = newColor;
                }

                UpdateImageDisplayContextMenuIcons();
            }
        }

        private void resetXYOffsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageDisplay1.ResetOffsets();
        }

        private void imageDisplay1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            imageDisplayContextMenu.Show(imageDisplay1, e.Location);
        }

        private void resetBackColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsManager.MainFormSettings.imageDisplayBG1 = HelperLibs.Controls.ImageDisplay.DefaultCellColor1;
            SettingsManager.MainFormSettings.imageDisplayBG2 = HelperLibs.Controls.ImageDisplay.DefaultCellColor2;

            imageDisplay1.CellColor1 = HelperLibs.Controls.ImageDisplay.DefaultCellColor1;
            imageDisplay1.CellColor2 = HelperLibs.Controls.ImageDisplay.DefaultCellColor2;

            if (SettingsManager.MainFormSettings.Show_Image_Display_Color_1_Only)
            {
                imageDisplay1.CellColor2 = imageDisplay1.CellColor1;
            }
            UpdateImageDisplayContextMenuIcons();
        }

        private void UpdateImageDisplayContextMenuIcons()
        {
            setBackColor1ToolStripMenuItem.Image = ImageProcessor.CreateSolidColorBitmap(
                InternalSettings.TSMI_Generated_Icon_Size, SettingsManager.MainFormSettings.imageDisplayBG1);
            setBackColor2ToolStripMenuItem.Image = ImageProcessor.CreateSolidColorBitmap(
                InternalSettings.TSMI_Generated_Icon_Size, SettingsManager.MainFormSettings.imageDisplayBG2);
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderView1.ListView_.FocusedItem == null)
                return;

            if (folderView1.ListView_.FocusedItem.Tag is DirectoryInfo)
                return;

            if (folderView1.ListView_.FocusedItem.Tag is FileInfo)
            {
                ClipboardHelper.CopyFile(((FileInfo)(folderView1.ListView_.FocusedItem.Tag)).FullName);
            }
        }

        private async void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path;

            if (folderView1.ListView_.FocusedItem == null)
                return;

            if (folderView1.ListView_.FocusedItem.Tag is DirectoryInfo)
                return;

            if (folderView1.ListView_.FocusedItem.Tag is FileInfo)
            {
                path = ((FileInfo)(folderView1.ListView_.FocusedItem.Tag)).FullName;

                if (imageDisplay1.ImagePath != null && path == imageDisplay1.ImagePath.FullName)
                {
                    if (_loadImageThread != null && !_loadImageThread.IsCompleted)
                    {
                        await _loadImageThread;
                        imageDisplay1.CopyImage();
                    }
                    else
                    {
                        imageDisplay1.CopyImage();
                    }
                }
                else
                {
                    if (_loadImageThread != null && _loadImageThread.IsCompleted) 
                    {
                        _loadImageThread?.Dispose();
                        _loadImageThread = Task.Run(() => {
                            using (Image i = ImageHelper.LoadImageAsBitmap(path))
                            {
                                this.InvokeSafe(() => { ClipboardHelper.CopyImage(i); });
                            }
                        });
                    }
                    else
                    {
                        await _loadImageThread;
                        _loadImageThread?.Dispose();
                        _loadImageThread = Task.Run(() => { 
                            using(Image i = ImageHelper.LoadImageAsBitmap(path)) 
                            {
                                this.InvokeSafe(() => { ClipboardHelper.CopyImage(i); });
                            }
                                });
                    }
                }
            }
        }

        private void fullPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedItemPath();
        }

        private void filenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedItemFilename();
        }

        private void FileSizeUnit_Click(object sender, EventArgs e)
        {
            FileSizeUnit fsu = (FileSizeUnit)((ToolStripItem)sender).Tag;

            switch (fsu)
            {
                case FileSizeUnit.Byte:
                    CopySelectedItemsSize(fsu, 0);
                    return;
                case FileSizeUnit.Kilobyte:
                    CopySelectedItemsSize(fsu, 0);
                    return;
                case FileSizeUnit.Megabyte:
                    CopySelectedItemsSize(fsu, 1);
                    return;
                case FileSizeUnit.Gigabyte:
                    CopySelectedItemsSize(fsu, 2);
                    return;
                case FileSizeUnit.Terabyte:
                    CopySelectedItemsSize(fsu, 3);
                    return;
                case FileSizeUnit.Petabyte:
                    CopySelectedItemsSize(fsu, 4);
                    return;
                case FileSizeUnit.Exabyte:
                    CopySelectedItemsSize(fsu, 5);
                    return;
                case FileSizeUnit.Zettabyte:
                    CopySelectedItemsSize(fsu, 6);
                    return;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (toolStripButton1.Checked)
            {
                this.TopMost = true;
                SettingsManager.MainFormSettings.Always_On_Top = true;
            }
            else
            {
                this.TopMost = false;
                SettingsManager.MainFormSettings.Always_On_Top = false;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            folderView1.CurrentDirectory = PathHelper.GetScreenshotFolder();
        }

        private async void openAsClipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderView1.ListView_.FocusedItem == null)
                return;

            if (folderView1.ListView_.FocusedItem == null)
                return;

            if (folderView1.ListView_.FocusedItem.Tag is DirectoryInfo)
                return;

            if (folderView1.ListView_.FocusedItem.Tag is FileInfo)
            {
                string path = ((FileInfo)(folderView1.ListView_.FocusedItem.Tag)).FullName;

                if (imageDisplay1.ImagePath != null && path == imageDisplay1.ImagePath.FullName)
                {
                    if (_loadImageThread != null && !_loadImageThread.IsCompleted)
                    {
                        await _loadImageThread;
                        ClipManager.Clips[ClipManager.CreateClipAtCursor(imageDisplay1.Image, true)].Options.FilePath = imageDisplay1.ImagePath.FullName;
                    }
                    else
                    {
                        ClipManager.Clips[ClipManager.CreateClipAtCursor(imageDisplay1.Image, true)].Options.FilePath = imageDisplay1.ImagePath.FullName;
                    }
                }
                else
                {
                    if (_loadImageThread != null && _loadImageThread.IsCompleted)
                    {
                        _loadImageThread?.Dispose();
                        _loadImageThread = Task.Run(() => {
                            Image i = ImageHelper.LoadImageAsBitmap(path);
                                this.InvokeSafe(() => {
                                    ClipManager.Clips[ClipManager.CreateClipAtCursor(i, false)].Options.FilePath = imageDisplay1.ImagePath.FullName;
                                });
                        });
                    }
                    else
                    {
                        await _loadImageThread;
                        _loadImageThread?.Dispose();
                        _loadImageThread = Task.Run(() => {
                            Image i = ImageHelper.LoadImageAsBitmap(path);
                            this.InvokeSafe(() => {
                                ClipManager.Clips[ClipManager.CreateClipAtCursor(i, false)].Options.FilePath = imageDisplay1.ImagePath.FullName;
                            });
                        });
                    }
                }
            }
        }

        private void openLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderView1.ListView_.FocusedItem == null)
                return;

            string path;

            if (folderView1.ListView_.FocusedItem.Tag is DirectoryInfo)
            {
                path = ((DirectoryInfo)(folderView1.ListView_.FocusedItem.Tag)).FullName;
            }
            else if (folderView1.ListView_.FocusedItem.Tag is FileInfo)
            {
                path = ((FileInfo)(folderView1.ListView_.FocusedItem.Tag)).FullName;
            }
            else
            {
                return;
            }

            PathHelper.OpenExplorerAtLocation(path);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            folderView1.CurrentDirectory = InternalSettings.DRIVES_FOLDERNAME;
        }
    }
}
