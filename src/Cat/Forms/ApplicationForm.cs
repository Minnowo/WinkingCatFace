using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Text;
using System.Drawing.Drawing2D;

using WinkingCat.HelperLibs;
using WinkingCat.HelperLibs.Enums;
using WinkingCat.Uploaders;

namespace WinkingCat
{

    public partial class ApplicationForm : BaseForm
    {
        List<BaseForm> _childrenHandles = new List<BaseForm>();

        private Task _loadImageThread;

        private Form _fullscreenImageForm;

        private TIMER _trayClickTimer = new TIMER();
        private TIMER _loadImageTimer = new TIMER();

        private int _trayClickCount = 0;

        private bool _forceClose = false;
        private bool _allowShowDisplay = !SettingsManager.MainFormSettings.Start_In_Tray;
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
                tsi.Click += CopySelectedFileSize_Click;
                copySizeToolStripMenuItem.DropDownItems.Add(tsi);
            }

            folderView1.ListView_.View = View.Details;
            folderView1.ListView_.Columns.Add(new ColumnHeader() { Name = "Filename", Text = "Filename", Width=500});
            folderView1.ListView_.Columns.Add(new ColumnHeader() { Name = "Size", Text = "Size", Width =30 });
            folderView1.CurrentDirectory = PathHelper.GetScreenshotFolder();

            _preventOverflow = true;

            tsMain.ClickThrough = true;

            tsmiCaptureCursor.Checked = SettingsManager.RegionCaptureSettings.Capture_Cursor;
            tsmiCaptureCursorTray.Checked = SettingsManager.RegionCaptureSettings.Capture_Cursor;

            cbInterpolationMode.Items.Add(InterpolationMode.NearestNeighbor);
            cbInterpolationMode.Items.Add(InterpolationMode.HighQualityBicubic);
            cbInterpolationMode.Items.Add(InterpolationMode.HighQualityBilinear);
            cbInterpolationMode.SelectedItem = SettingsManager.MiscSettings.Default_Interpolation_Mode;

            cbDrawMode.Items.Add(WinkingCat.HelperLibs.Controls.ImageDrawMode.ActualSize);
            cbDrawMode.Items.Add(WinkingCat.HelperLibs.Controls.ImageDrawMode.FitImage);
            cbDrawMode.Items.Add(WinkingCat.HelperLibs.Controls.ImageDrawMode.DownscaleImage);
            cbDrawMode.Items.Add(WinkingCat.HelperLibs.Controls.ImageDrawMode.Resizeable);
            cbDrawMode.SelectedItem = SettingsManager.MiscSettings.Default_Draw_Mode;

            _trayClickTimer.SetInterval(SettingsManager.MainFormSettings.Tray_Double_Click_Time);
            _loadImageTimer.SetInterval(SettingsManager.MainFormSettings.Load_Image_Delay);

#if !DEBUG
            MaximizeBox        = SettingsManager.MainFormSettings.Show_Maximize_Box;
            TopMost            = SettingsManager.MainFormSettings.Always_On_Top;
            niTrayIcon.Visible = SettingsManager.MainFormSettings.Show_In_Tray;
            
#endif
            tsmiAlwaysOnTop.Checked = TopMost;
            imageDisplay1.ClearImagePathOnReplace = true;
            imageDisplay1.DisposeImageOnReplace = true;
            imageDisplay1.ResetOffsetButton = MouseButtons.None;


            RegisterEvents();

            _preventOverflow = false;
            ResumeLayout();
            // this.pbPreviewBox.previewOnClick = true;
        }

        /// <summary>
        /// Opens the application settings.
        /// </summary>
        public void OpenSettings()
        {
            using (SettingsForm sf = new SettingsForm())
            {
                sf.Owner = this;
                sf.TopMost = SettingsManager.MainFormSettings.Always_On_Top;
                sf.StartPosition = FormStartPosition.CenterScreen;
                sf.ShowDialog();
            }
        }

        /// <summary>
        /// Opens the application styles editor.
        /// </summary>
        public void OpenStyles()
        {
            using (StylesForm sf = new StylesForm())
            {
                sf.Owner = this;
                sf.TopMost = SettingsManager.MainFormSettings.Always_On_Top;
                sf.StartPosition = FormStartPosition.CenterScreen;
                sf.ShowDialog();
            }
        }

        /// <summary>
        /// Force close all the dropdowns.
        /// </summary>
        public void CloseDropDowns()
        {
            _forceDropDownClose = true;
            tsddbCapture.DropDown.Close();
            tsddbClips.DropDown.Close();
            tsddbTools.DropDown.Close();
        }

        /// <summary>
        /// Sets the image display interpolation mode.
        /// </summary>
        public void SetInterpolationMode(InterpolationMode mode)
        {
            imageDisplay1.InterpolationMode = mode;
        }

        /// <summary>
        /// Sets the imagae display draw mode.
        /// </summary>
        /// <param name="mode"></param>
        public void SetDrawMode(HelperLibs.Controls.ImageDrawMode mode)
        {
            imageDisplay1.DrawMode = mode;

            if(mode == HelperLibs.Controls.ImageDrawMode.Resizeable)
            {
                imageDisplay1.CenterCurrentImage();
            }
        }

        /// <summary>
        /// Copies the filename of all selected listview items.
        /// </summary>
        public void CopySelectedItemFilename()
        {
            if (folderView1.ListView_.SelectedIndices.Count < 2)
            {
                if (folderView1.ListView_.FocusedItem == null)
                    return;

                if (folderView1.ListView_.FocusedItem.Tag is DirectoryInfo)
                {
                    ClipboardHelper.CopyStringDefault(((DirectoryInfo)(folderView1.ListView_.FocusedItem.Tag)).Name);
                }
                else if (folderView1.ListView_.FocusedItem.Tag is FileInfo)
                {
                    ClipboardHelper.CopyStringDefault(((FileInfo)(folderView1.ListView_.FocusedItem.Tag)).Name);
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
        
        /// <summary>
        /// Copies the path of all selected listview items.
        /// </summary>
        public void CopySelectedItemPath()
        {
            if (folderView1.ListView_.SelectedIndices.Count < 2)
            {
                if (folderView1.ListView_.FocusedItem == null)
                    return;

                if (folderView1.ListView_.FocusedItem.Tag is DirectoryInfo)
                {
                    ClipboardHelper.CopyStringDefault(((DirectoryInfo)(folderView1.ListView_.FocusedItem.Tag)).FullName);
                }
                else if (folderView1.ListView_.FocusedItem.Tag is FileInfo)
                {
                    ClipboardHelper.CopyStringDefault(((FileInfo)(folderView1.ListView_.FocusedItem.Tag)).FullName);
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
        /// Copies the file size of all selected listview items.
        /// </summary>
        public void CopySelectedItemsSize(FileSizeUnit size, int decimalPlaces = 1)
        {
            FileInfo info;

            if (folderView1.ListView_.SelectedIndices.Count < 2)
            {
                if (folderView1.ListView_.FocusedItem == null)
                    return;

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
                return;
            }

            StringBuilder paths = new StringBuilder();

            int i = 0;
            foreach (int ii in folderView1.ListView_.SelectedIndices)
            {
                if (folderView1.ListView_.Items[ii].Tag is DirectoryInfo)
                {
                    paths.AppendLine("Unknown Folder Size");
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

        /// <summary>
        /// Copies the selected files / folders to the clipboard
        /// </summary>
        public void CopySelectedFiles()
        {
            if (folderView1.ListView_.SelectedIndices.Count < 2)
            {
                if (folderView1.ListView_.FocusedItem == null)
                    return;

                if (folderView1.ListView_.FocusedItem.Tag is DirectoryInfo)
                {
                    ClipboardHelper.CopyFile(((DirectoryInfo)folderView1.ListView_.FocusedItem.Tag).FullName);
                }
                else if (folderView1.ListView_.FocusedItem.Tag is FileInfo)
                {
                    ClipboardHelper.CopyFile(((FileInfo)folderView1.ListView_.FocusedItem.Tag).FullName);
                }
                return;
            }

            string[] files = new string[folderView1.ListView_.SelectedIndices.Count];

            int i = 0;
            foreach (int ii in folderView1.ListView_.SelectedIndices)
            {
                if (folderView1.ListView_.Items[ii].Tag is DirectoryInfo)
                {
                    files[i] = ((DirectoryInfo)folderView1.ListView_.Items[ii].Tag).FullName;
                }
                else if (folderView1.ListView_.Items[ii].Tag is FileInfo)
                {
                    files[i] = ((FileInfo)folderView1.ListView_.Items[ii].Tag).FullName;
                }
                i++;
            }
            ClipboardHelper.CopyFile(files);
        }

        /// <summary>
        /// Copies the selected image to the clipboard
        /// </summary>
        /// <returns></returns>
        public async Task CopySelectedImage()
        {
            if (folderView1.ListView_.FocusedItem == null)
                return;

            if (!(folderView1.ListView_.FocusedItem.Tag is FileInfo))
                return;

            string path = ((FileInfo)(folderView1.ListView_.FocusedItem.Tag)).FullName;

            if (imageDisplay1.ImagePath != null && path == imageDisplay1.ImagePath.FullName)
            {
                if (_loadImageThread != null)
                    if(_loadImageThread.Status == TaskStatus.Running ||
                       _loadImageThread.Status == TaskStatus.WaitingForActivation)
                        await _loadImageThread;

                imageDisplay1.CopyImage();
                return;
            }

            if (_loadImageThread != null)
            {
                if (_loadImageThread.Status == TaskStatus.Running ||
                    _loadImageThread.Status == TaskStatus.WaitingForActivation)
                    await _loadImageThread;

                _loadImageThread?.Dispose();
            }

            _loadImageThread = Task.Run(() =>
            {
                using (Image i = ImageHelper.LoadImageAsBitmap(path))
                {
                    this.InvokeSafe(() => { ClipboardHelper.CopyImage(i); });
                }
            });
        }

        /// <summary>
        /// Copies the image shown in the ImageDisplay
        /// </summary>
        public void CopyShownImage()
        {
            if (imageDisplay1.Image == null)
                return;

            ClipboardHelper.CopyImage(imageDisplay1.Image);
        }

        /// <summary>
        /// Loads the selected item as a clip
        /// </summary>
        /// <returns></returns>
        public async Task LoadSelectedImageAsClip()
        {
            if (folderView1.ListView_.FocusedItem == null)
                return;

            if (!(folderView1.ListView_.FocusedItem.Tag is FileInfo))
                return;

            string path = ((FileInfo)(folderView1.ListView_.FocusedItem.Tag)).FullName;
            string clip;

            if (imageDisplay1.ImagePath != null && path == imageDisplay1.ImagePath.FullName)
            {
                if (_loadImageThread != null && _loadImageThread.Status == TaskStatus.Running || 
                    _loadImageThread.Status == TaskStatus.WaitingForActivation)
                    await _loadImageThread;

                clip = ClipManager.CreateClipAtCursor(imageDisplay1.Image, true);
                ClipManager.Clips[clip].Options.FilePath = imageDisplay1.ImagePath.FullName;
                return;
            }

            if (_loadImageThread != null)
            {
                if (_loadImageThread.Status == TaskStatus.Running || 
                    _loadImageThread.Status == TaskStatus.WaitingForActivation)
                    await _loadImageThread;

                _loadImageThread?.Dispose();
            }

            _loadImageThread = Task.Run(() =>
            {
                Image i = ImageHelper.LoadImageAsBitmap(path);

                if (i == null)
                    return;

                this.InvokeSafe(() =>
                {
                    clip = ClipManager.CreateClipAtCursor(imageDisplay1.Image, false);
                    ClipManager.Clips[clip].Options.FilePath = imageDisplay1.ImagePath.FullName;
                });
            });
        }

        /// <summary>
        /// Open explorer at location of selected item
        /// </summary>
        public void OpenExplorerAtSelectedItem()
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

        /// <summary>
        /// Loads the selected item in the listview
        /// </summary>
        public void LoadSelectedImage()
        {
            _loadImageTimer.Stop();

            if (folderView1.SelectedIndex == -1)
                return;

            if (!(folderView1.ListView_.Items[folderView1.SelectedIndex].Tag is FileInfo))
                return;

            FileInfo f = folderView1.ListView_.Items[folderView1.SelectedIndex].Tag as FileInfo;

            if (!File.Exists(f.FullName))
                return;

            if (imageDisplay1.ImagePath != null && f.FullName == imageDisplay1.ImagePath.FullName)
                return;

            if (_loadImageThread != null) 
                if (_loadImageThread.Status == TaskStatus.Running ||
                    _loadImageThread.Status == TaskStatus.WaitingForActivation)
                    return;

            _loadImageThread?.Dispose();
            _loadImageThread = imageDisplay1.TryLoadImageAsync(f.FullName);
        }

        /// <summary>
        /// Updates the colored icons in the ImageDisplay context menu.
        /// </summary>
        public void UpdateImageDisplayContextMenuIcons()
        {
            setBackColor1ToolStripMenuItem.Image = ImageProcessor.CreateSolidColorBitmap(
                InternalSettings.TSMI_Generated_Icon_Size, SettingsManager.MainFormSettings.imageDisplayBG1);
            setBackColor2ToolStripMenuItem.Image = ImageProcessor.CreateSolidColorBitmap(
                InternalSettings.TSMI_Generated_Icon_Size, SettingsManager.MainFormSettings.imageDisplayBG2);
        }

        private void ListView__MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            
            listViewContextMenu.Show(folderView1.ListView_, e.Location);
        }

        private void ImageDisplay_ImageChanged()
        {
            tbImageDimensionsDisplay.Text = "";

            if (imageDisplay1.Image == null)
                return;

            tbImageDimensionsDisplay.Text = string.Format("{0} x {1} : {2}%", 
                imageDisplay1.Image.Width, 
                imageDisplay1.Image.Height, 
                imageDisplay1.ZoomPercent);
        }

        private void ImageDisplay_ZoomLevelChanged(int zoomLevelPercent)
        {
            ImageDisplay_ImageChanged();
        }

        private void ImageDrawMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_preventOverflow)
                return;

            SetDrawMode((HelperLibs.Controls.ImageDrawMode)cbDrawMode.SelectedItem);
        }

        private void ImageInterpolationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_preventOverflow)
                return;

            SetInterpolationMode((InterpolationMode)cbInterpolationMode.SelectedItem);
        }

        #region Capture dropdown buttons

        private async void Capture_DropDownOpening(object sender, EventArgs e)
        {
            if(sender is ToolStripDropDownButton) 
                await PrepareCaptureMenuAsync(tsmiToolStripMenuItem_window, WindowItems_Click, tsmiToolStripMenuItem_monitor, MonitorItems_Click);
            else
                await PrepareCaptureMenuAsync(tsmiWindowToolStripMenuItem, WindowItems_Click, tsmiMonitorToolStripMenuItem, MonitorItems_Click);
        }

        private void WindowItems_Click(object sender, EventArgs e)
        {
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
                tsmiCaptureCursor.Checked = true;
                tsmiCaptureCursorTray.Checked = true;
            }
            else
            {
                ScreenshotHelper.CaptureCursor = false;
                tsmiCaptureCursor.Checked = false;
                tsmiCaptureCursorTray.Checked = false;
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
            CloseDropDowns();
        }

        private void MainForm_Resize_LostFocus_GotFocus(object sender, EventArgs e)
        {
            CloseDropDowns();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && SettingsManager.MainFormSettings.Hide_In_Tray_On_Close && !_forceClose)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void LoadImageTimer_Tick(object sender, EventArgs e)
        {
            LoadSelectedImage();
        }

        #endregion



        private void LvListView_ItemSelectionChanged(object sender, EventArgs e)
        {
            _loadImageTimer.Stop();
            _loadImageTimer.Start();
        }

        private void DropDownClosing_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason != ToolStripDropDownCloseReason.AppFocusChange &&
                e.CloseReason != ToolStripDropDownCloseReason.CloseCalled)
                return;

            if (_forceDropDownClose)
            {
                _forceDropDownClose = false;
            }
            else
            {
                e.Cancel = true;
            }
        }



        private void ToolStripDropDownButton_Settings_Click(object sender, EventArgs e)
        {
            OpenSettings();
        }

        private void ToolStripDropDownButton_Styles_Click(object sender, EventArgs e)
        {
            OpenStyles();
        }

        

        private async Task PrepareCaptureMenuAsync(
                                                    ToolStripMenuItem tsmiWindow, 
                                                    EventHandler handlerWindow, 
                                                    ToolStripMenuItem tsmiMonitor, 
                                                    EventHandler handlerMonitor)
        {
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




        private void CopyShownImage_Click(object sender, EventArgs e)
        {
            CopyShownImage();
        }


        private void CloseShownImage_Click(object sender, EventArgs e)
        {
            imageDisplay1.Image = null;
        }

        private void CloseImageDisplayClick_Click(object sender, EventArgs e)
        {
            if (_showingFullscreenImage)
            {
                if(_fullscreenImageForm != null)
                {
                    _fullscreenImageForm.Close();
                }
                return;
            }
         
            imageDisplay1.Image = null;
        }

        private void ShownFullscreenImage_Click(object sender, EventArgs e)
        {
            if (imageDisplay1.Image == null || _showingFullscreenImage)
                return;

            ShowFullScreenImage();
            imageDisplay1.CenterCurrentImage();
        }

        private void ShowFullScreenImage()
        {
            if (this._showingFullscreenImage)
                return;

            Control ctl = this.imageDisplay1;

            Point        og_loc  = ctl.Location;
            Size         og_size = ctl.Size;
            DockStyle    og_dock = ctl.Dock;
            AnchorStyles og_anch = ctl.Anchor;
            Control      parent  = ctl.Parent;

            HelperLibs.Controls.ImageDrawMode d = imageDisplay1.DrawMode;
            imageDisplay1.DrawMode = HelperLibs.Controls.ImageDrawMode.Resizeable;

            _fullscreenImageForm = new Form()
            {
                FormBorderStyle = FormBorderStyle.None,
                WindowState = FormWindowState.Maximized,
                ShowInTaskbar = false,
                KeyPreview = true
            };

            _fullscreenImageForm.FormClosing += delegate
            {
                ctl.Parent   = parent;
                ctl.Location = og_loc;
                ctl.Dock     = og_dock;
                ctl.Anchor   = og_anch;
                ctl.Size     = og_size;
                imageDisplay1.DrawMode = d;

                if (d == HelperLibs.Controls.ImageDrawMode.Resizeable)
                    imageDisplay1.CenterCurrentImage();

                this._showingFullscreenImage = false;
                this.Show();
            };

            _fullscreenImageForm.KeyUp += (KeyEventHandler)((s, e) =>
            {
                switch (e.KeyData)
                {
                    case Keys.End:
                    case Keys.Back:
                    case Keys.Control | Keys.W:
                    case Keys.Alt | Keys.F4:
                    case Keys.Escape:
                        _fullscreenImageForm.Close();
                        break;
                }
            });


            // Move control to host
            ctl.Parent   = _fullscreenImageForm;
            ctl.Location = Point.Empty;
            ctl.Dock     = DockStyle.Fill;

            // And go full screen
            _fullscreenImageForm.Show();

            this._showingFullscreenImage = true;
            this.Hide();
        }

        private void ShowOnlyGridColor1_Click(object sender, EventArgs e)
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

        private void SetGridColor1_Click(object sender, EventArgs e)
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

        private void SetGridColor2_Click(object sender, EventArgs e)
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

        private void ResetGridColors_Click(object sender, EventArgs e)
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

        private void ResetImageXYOffsets_Click(object sender, EventArgs e)
        {
            imageDisplay1.ResetOffsets();
        }

        private void ImageDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            imageDisplayContextMenu.Show(imageDisplay1, e.Location);
        }

        private void CopySelectedFiles_Click(object sender, EventArgs e)
        {
            CopySelectedFiles();
        }

        private async void CopySelectedItemImage_Click(object sender, EventArgs e)
        {
            await CopySelectedImage();
        }

        private void CopySelectedItemPath_Click(object sender, EventArgs e)
        {
            CopySelectedItemPath();
        }

        private void CopySelectedItemFilename_Click(object sender, EventArgs e)
        {
            CopySelectedItemFilename();
        }

        private void CopySelectedFileSize_Click(object sender, EventArgs e)
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

        private void AlwaysOnTop_Click(object sender, EventArgs e)
        {
            if (tsmiAlwaysOnTop.Checked)
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
        
        private async void openAsClipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await LoadSelectedImageAsClip();
        }

        private void openLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenExplorerAtSelectedItem();
        }

        private void SetCurrentFolderToScreenshotFolder_Click(object sender, EventArgs e)
        {
            folderView1.CurrentDirectory = PathHelper.GetScreenshotFolder();
        }

        private void SetCurrentFolderToDrivesFolder_Click(object sender, EventArgs e)
        {
            folderView1.CurrentDirectory = InternalSettings.DRIVES_FOLDERNAME;
        }



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

            _trayClickTimer.SetInterval(SettingsManager.MainFormSettings.Tray_Double_Click_Time);
            _loadImageTimer.SetInterval(SettingsManager.MainFormSettings.Load_Image_Delay);

            if (SettingsManager.MainFormSettings.Show_Image_Display_Color_1_Only)
            {
                imageDisplay1.CellColor2 = imageDisplay1.CellColor1;
            }

            UpdateImageDisplayContextMenuIcons();
        }


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

            ApplicationStyles.ApplyCustomThemeToControl(scMain.Panel2);
            Refresh();
        }


        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(_allowShowDisplay ? value : _allowShowDisplay);
            _allowShowDisplay = true;
            UpdateTheme();
        }


        protected override void RegisterEvents()
        {
            base.RegisterEvents();

            this.Resize += MainForm_Resize_LostFocus_GotFocus;
            this.LostFocus += MainForm_Resize_LostFocus_GotFocus;
            this.GotFocus += MainForm_Resize_LostFocus_GotFocus;


            folderView1.ListView_.MouseClick += ListView__MouseClick;
            folderView1.ListView_.SelectedIndexChanged += LvListView_ItemSelectionChanged;

            cbInterpolationMode.SelectedIndexChanged += ImageInterpolationMode_SelectedIndexChanged;
            cbDrawMode.SelectedIndexChanged += ImageDrawMode_SelectedIndexChanged;
            imageDisplay1.ImageChanged += ImageDisplay_ImageChanged;
            imageDisplay1.ZoomLevelChanged += ImageDisplay_ZoomLevelChanged; ;


            // timer 
            _trayClickTimer.Tick += TrayClickTimer_Interval;
            _loadImageTimer.Tick += LoadImageTimer_Tick;


            // left toolstrip // 
            // capture drop down
            tsddbCapture.DropDown.Closing += DropDownClosing_Closing;
            tsddbCapture.DropDownOpening  += Capture_DropDownOpening;

            tsmiRegionCapture.Click     += RegionCapture_Click;
            tsmiFullscreenCapture.Click += FullscreenCapture_Click;
            tsmiLastRegionCapture.Click += LastRegionCapture_Click;
            tsmiCaptureCursor.Click     += CursorCapture_Click;

            // clips drop down 
            tsddbClips.DropDown.Closing += DropDownClosing_Closing;

            tsmiNewClip.Click              += NewClip_Click;
            tsmiNewClipFromClipboard.Click += ClipFromClipboard_Click;
            tsmiNewClipFromFile.Click      += ClipFromFile_Click;

            // tools drop down
            tsddbTools.DropDown.Closing += DropDownClosing_Closing;

            tsmiScreenColorPicker.Click += ScreenColorPicker_Click;
            tsmiColorPicker.Click       += ColorPicker_Click;
            tsmiQrCode.Click            += QrCode_Click;
            tsmiHashCheck.Click         += HashCheck_Click;
            tsmiRegex.Click             += Regex_Click;


            // system tray stuff // 
            // capture 
            cmTray.Opening += Capture_DropDownOpening;
            niTrayIcon.MouseUp += NiTrayIcon_MouseClick1Up;

            tsmiRegionCaptureTray.Click += RegionCapture_Click;
            tsmiFullscreenCaptureTray.Click += FullscreenCapture_Click;
            tsmiLastRegionCaptureTray.Click += LastRegionCapture_Click;
            tsmiCaptureCursorTray.Click += CursorCapture_Click;

            // clip
            tsmiNewClipTray.Click += NewClip_Click;
            tsmiNewClipFromClipboardTray.Click += ClipFromClipboard_Click;
            tsmiNewClipFromFileTray.Click += ClipFromFile_Click;

            // tools
            tsmiScreenColorPickerTray.Click += ScreenColorPicker_Click;
            tsmiColorPickerTray.Click += ColorPicker_Click;
            tsmiHashCheckTray.Click += HashCheck_Click;
            tsmiQrCodeTray.Click += QrCode_Click;

            // other
            tsmiStylesTray.Click += ToolStripDropDownButton_Styles_Click;
            tsmiSettingsTray.Click += ToolStripDropDownButton_Settings_Click;
            tsmiOpenMainWindowTray.Click += OpenMainWindow_Click;
            tsmiExitTray.Click += ExitApplication_Click;
        }

        
    }
}
