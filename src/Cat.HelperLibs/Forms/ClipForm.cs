using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{
    public enum DragLoc
    {
        Top,
        Left,
        Right,
        Bottom
    }
    public partial class ClipForm : BaseForm, IUndoable
    {
        /// <summary>
        /// The name of the clip.
        /// </summary>
        public string ClipName { get; private set; }

        /// <summary>
        /// The options given to the clip.
        /// </summary>
        public ClipOptions Options { get; private set; }

        /// <summary>
        /// The image.
        /// </summary>
        public Bitmap Image 
        { 
            get { return _ChangeTracker.CurrentBitmap; } 
        }

        /// <summary>
        /// The image the scaled to the size of the form.
        /// </summary>
        public Bitmap ScaledImage { get; private set; }

        /// <summary>
        /// The window size of the clip when it was first created.
        /// </summary>
        public Size StartWindowSize { get; private set; }


        private BitmapUndo _ChangeTracker;

        private Stopwatch _ZoomRefreshLimiter = new Stopwatch();        

        private DragLoc _DragLocation;

        private Size _ZoomControlSize;

        private Point _LastClickLocation;

        private double _ZoomLevel = 1;
        private double _AspectRatio = 0;

        private bool _IsLeftClicking = false;
        private bool _IsResizing = false;
        private bool _IsResizable = true;
        private bool _IsMoving = false;
        private bool _MagnifierShown = false;

        public ClipForm(ClipOptions options, Image image)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
            SuspendLayout();

            Options = options;

            ClipName = Options.Name;

            _ChangeTracker = new BitmapUndo(IMAGE.ProperCast(image, InternalSettings.Default_Image_Format));

            _AspectRatio = image.Width / (double)image.Height;

            StartWindowSize = new Size(
                image.Width + (Options.BorderThickness << 1),
                image.Height + (Options.BorderThickness << 1));

            _ZoomControlSize = new Size(
                (int)Math.Round(StartWindowSize.Width * SettingsManager.ClipSettings.Zoom_Size_From_Percent),
                (int)Math.Round(StartWindowSize.Height * SettingsManager.ClipSettings.Zoom_Size_From_Percent));

            MinimumSize = StartWindowSize;
            MaximumSize = StartWindowSize;

            
            Bounds = new Rectangle(options.Location, StartWindowSize);
            BackColor = Options.Color;

            zdbZoomedImageDisplay.Enabled = false;
            zdbZoomedImageDisplay.borderColor = SettingsManager.ClipSettings.Border_Color;
            zdbZoomedImageDisplay.replaceTransparent = SettingsManager.ClipSettings.Zoom_Color;
            if (SettingsManager.ClipSettings.Zoom_Size_From_Percent == 1f)
            {
                zdbZoomedImageDisplay.BorderThickness = 0;
            }
            else
            {
                zdbZoomedImageDisplay.BorderThickness = SettingsManager.ClipSettings.Zoom_Border_Thickness;
            }


            MouseDown += MouseDown_Event;
            MouseUp += MouseUp_Event;
            MouseMove += MouseMove_Event;
            ResizeEnd += ResizeEnded;
            KeyDown += FormKeyDown;
            MouseWheel += ClipForm_MouseWheel;

            base.PreventHide = SettingsManager.ClipSettings.Never_Hide_Clips;
            base.RegisterEvents();

            #region context menu
            cmMain.Opening += CmMain_Opening;
            tsmiCopyToolStripMenuItem.Click += CopyImage_Click;
            tsmiAllowResizeToolStripMenuItem.Click += AllowResize_Click;
            tsmiOCRToolStripMenuItem.Click += OCR_Click;
            tsmiSaveToolStripMenuItem.Click += Save_Click;
            tsmiDestroyToolStripMenuItem.Click += Destroy_Click;
            tsmiDestroyAllClipsToolStripMenuItem.Click += DestroyAllClips_Click;

            tsmiCopyDefaultContextMenuItem.Click += CopyImage_Click;
            tsmiCopyZoomScaledContextMenuItem.Click += CopyScaledImage_Click;
            tsmiCopyZoomedImage.Click += CopyZoomedImage_Click;

            tsmiOpenInEditor.Click += OpenInEditor_Click;
            tsmiInvertColor.Click += InvertColor_Click;
            tsmiConvertToGray.Click += ConvertToGray_Click;
            tsmiRotateLeft.Click += RotateLeft_Click;
            tsmiRotateRight.Click += RotateRight_Click;
            tsmiFlipHorizontal.Click += FlipHorizontal_Click;
            tsmiFlipVertical.Click += FlipVertical_Click;

            if (_IsResizable) tsmiAllowResizeToolStripMenuItem.Checked = true;
            #endregion

            TopMost = true;
            Text = ClipName;

            ResumeLayout(true);

            Show();

            UpdateTheme();

            //for some reason if the clip is on a display that is scaled it makes it bigger than its supposed to be
            //so only allow it to be resized after 1000 ms
            Timer updateMaxSizeTimer = new Timer() { Interval = 1000 };
            updateMaxSizeTimer.Tick += UpdateMaxSizeTimerTick_Event;
            updateMaxSizeTimer.Start();
        }

        // https://github.com/Minnowo/WinkingCatFace/issues/3
        // override the update settings because the BaseForm sets topmost upon setting update
        public override void UpdateSettings()
        {
        }

        /// <summary>
        /// Checks if the window should adjust its size to match a newly rotated image.
        /// </summary>
        public void UpdateRotation()
        {
            double aspectR = _ChangeTracker.CurrentBitmap.Width / (double)_ChangeTracker.CurrentBitmap.Height;

            if(aspectR.CompareTo(_AspectRatio) == 0)
                return;

            // if the aspect ratio changed it means that the image has been rotated left or right
            // so swap the width and height to rotate
            _AspectRatio = aspectR;

            Size oldWindowSize = new Size(Width, Height);

            MinimumSize =   new Size(MinimumSize.Height, MinimumSize.Width);
            Size =          new Size(oldWindowSize.Height, oldWindowSize.Width);
        }

        /// <summary>
        /// Updates the theme of the form.
        /// </summary>
        public override void UpdateTheme()
        {
            if (SettingsManager.MainFormSettings.useImersiveDarkMode)
            {
                this.Icon = Properties.Resources._3white;
            }
            else
            {
                this.Icon = Properties.Resources._3black;
            }

            cmMain.Renderer = new ToolStripCustomRenderer();
            cmMain.Opacity = SettingsManager.MainFormSettings.contextMenuOpacity;

            BackColor = SettingsManager.ClipSettings.Border_Color;

            zdbZoomedImageDisplay.borderColor = SettingsManager.ClipSettings.Border_Color;
            zdbZoomedImageDisplay.replaceTransparent = SettingsManager.ClipSettings.Zoom_Color;

            _ZoomControlSize = new Size(
                (int)Math.Round(ClientSize.Width * SettingsManager.ClipSettings.Zoom_Size_From_Percent),
                (int)Math.Round(ClientSize.Height * SettingsManager.ClipSettings.Zoom_Size_From_Percent));

            Refresh();
        }

        /// <summary>
        /// Opens a save file dialog for the current image.
        /// </summary>
        public void AskSaveImage()
        {
            using (Image img = (Image)Image.CloneSafe())
            {
                ImageHelper.SaveImageFileDialog(img);
            }
        }

        /// <summary>
        /// Opens an OCR form for the image.
        /// </summary>
        public void OCR_Image()
        {
            // cause i'm really smart, you can't access OCRForm because this class is in the helper dll 
            /*if (File.Exists(Options.FilePath))
            {
                OCRForm form = new OCRForm(Options.FilePath);
                form.Owner = this;
                form.TopMost = true;
                form.Show();
                return;
            }
            
            string fileName = PathHelper.GetNewImageFileName();

            if (ImageHelper.SaveImage(this.Image, fileName))
            {
                OCRForm form = new OCRForm(fileName);
                form.Owner = this;
                form.TopMost = true;
                form.Show();
                return;
            }
            
            MessageBox.Show("The path to the image has does not exist, and the image failed to save");*/
        }

        /// <summary>
        /// Toggles the ability to resize the form.
        /// </summary>
        public void ToggleResize()
        {
            if (_IsResizable)
            {
                _IsResizable = false;
                tsmiAllowResizeToolStripMenuItem.Checked = false;
            }
            else
            {
                _IsResizable = true;
                tsmiAllowResizeToolStripMenuItem.Checked = true;
            }
            Invalidate();
        }

        /// <summary>
        /// Copies the original image.
        /// </summary>
        public void CopyImage()
        {
            ClipboardHelper.CopyImage(Image);
            cmMain.Close();
        }

        /// <summary>
        /// Copies the zoomed image shown when the mouse wheel is zoomed in.
        /// </summary>
        public void CopyZoomedImage()
        {
            if (zdbZoomedImageDisplay.image != null)
            {
                ClipboardHelper.CopyImage(zdbZoomedImageDisplay.image);
            }
        }

        /// <summary>
        /// Copies the image scaled to fill the form when its resized.
        /// </summary>
        public void CopyScaledImage()
        {
            using (Bitmap img = ImageProcessor.GetResizedBitmap(this.Image, new Size(Width - Options.BorderThickness, Height - Options.BorderThickness)))
            {
                ClipboardHelper.CopyImage(img);
            }
        }

        /// <summary>
        /// Opens the image in an editor for basic image manipulation.
        /// </summary>
        public void OpenInEditor()
        {

        }

        /// <summary>
        /// Inverts the image.
        /// </summary>
        public void InvertImage()
        {
            _ChangeTracker.InvertBitmap();
            Invalidate();
        }

        /// <summary>
        /// Converts the image to grayscale.
        /// </summary>
        public void ConvertImageToGray()
        {
            _ChangeTracker.ConvertToGray();
            Invalidate();
        }

        /// <summary>
        /// Rotate the image 90 to the left.
        /// </summary>
        public void RotateLeft()
        {
            _ChangeTracker.RotateLeft();
            UpdateRotation();
            Invalidate();
        }

        /// <summary>
        /// Rotate the image 90 to the right.
        /// </summary>
        public void RotateRight()
        {
            _ChangeTracker.RotateRight();
            UpdateRotation();
            Invalidate();
        }

        /// <summary>
        /// Flip the image horizontally.
        /// </summary>
        public void FlipHorizontal()
        {
            _ChangeTracker.FlipHorizontal();
            Invalidate();
        }

        /// <summary>
        /// Flip the image vertically.
        /// </summary>
        public void FlipVertical()
        {
            _ChangeTracker.FlipVertical();
            Invalidate();
        }

        /// <summary>
        /// Redo the last change to the image.
        /// </summary>
        public void Redo()
        {
            _ChangeTracker.Redo();
            Invalidate();
        }

        /// <summary>
        /// Undo the last change to the image.
        /// </summary>
        public void Undo()
        {
            _ChangeTracker.Undo();
            Invalidate();
        }

        private void DrawZoomedImage(Point mousePos)
        {
            zdbZoomedImageDisplay.Size = new Size(
                _ZoomControlSize.Width + (zdbZoomedImageDisplay.BorderThickness << 1),
                _ZoomControlSize.Height + (zdbZoomedImageDisplay.BorderThickness << 1));

            if (SettingsManager.ClipSettings.Zoom_Follow_Mouse)
            {
                zdbZoomedImageDisplay.Location = new Point(
                    mousePos.X - (zdbZoomedImageDisplay.ClientSize.Width >> 1),
                    mousePos.Y - (zdbZoomedImageDisplay.ClientSize.Height >> 1));
            }

            // if the clip has been dragged larger then we need 
            // to resize the image before trying to zoom in on it
            if (ClientSize != StartWindowSize)
            {
                Size scaledImageSize = new Size(
                    Width - (Options.BorderThickness << 1),
                    Height - (Options.BorderThickness << 1));

                // if the zoomed image is null or not the size its supposed to be
                // try and dispose of it, then remake it
                if (ScaledImage == null || ScaledImage.Size != scaledImageSize)
                {
                    ScaledImage?.Dispose();
                    ScaledImage = new Bitmap(scaledImageSize.Width, scaledImageSize.Height);

                    using (Graphics g = Graphics.FromImage(ScaledImage))
                    {
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;
                        g.PixelOffsetMode = PixelOffsetMode.Half;

                        g.DrawImage(
                            Image,
                            new Rectangle(new Point(Options.BorderThickness, Options.BorderThickness), scaledImageSize),
                            new Rectangle(0, 0, Image.Width, Image.Height), GraphicsUnit.Pixel);
                    }
                }
            

                zdbZoomedImageDisplay.DrawImage(ScaledImage,
                        new Rectangle(0, 0, _ZoomControlSize.Width, _ZoomControlSize.Height),
                        new Rectangle(
                            mousePos.X - (int)Math.Round(zdbZoomedImageDisplay.Size.Width / _ZoomLevel / 2),
                            mousePos.Y - (int)Math.Round(zdbZoomedImageDisplay.Size.Height / _ZoomLevel / 2),
                            (int)Math.Round(_ZoomControlSize.Width / _ZoomLevel),
                            (int)Math.Round(_ZoomControlSize.Height / _ZoomLevel)));
            }
            else
            {
                zdbZoomedImageDisplay.DrawImage(Image,
                    new Rectangle(0, 0, _ZoomControlSize.Width, _ZoomControlSize.Height),
                    new Rectangle(
                        mousePos.X - (int) Math.Round(zdbZoomedImageDisplay.Size.Width / _ZoomLevel / 2),
                        mousePos.Y - (int) Math.Round(zdbZoomedImageDisplay.Size.Height / _ZoomLevel / 2),
                        (int) Math.Round(_ZoomControlSize.Width / _ZoomLevel),
                        (int) Math.Round(_ZoomControlSize.Height / _ZoomLevel)));
            }
}


        #region context menu functions

        private void CmMain_Opening(object sender, CancelEventArgs e)
        {
            if (zdbZoomedImageDisplay.image == null)
            {
                tsmiCopyZoomedImage.Enabled = false;
            }
            else
            {
                tsmiCopyZoomedImage.Enabled = true;
            }
        }

        private void CopyZoomedImage_Click(object sender, EventArgs e)
        {
            CopyZoomedImage();
        }

        private void CopyScaledImage_Click(object sender, EventArgs e)
        {
            CopyScaledImage();
        }

        private void CopyImage_Click(object sender, EventArgs e)
        {
            CopyImage();
        }

        private void OpenInEditor_Click(object sender, EventArgs e)
        {
            OpenInEditor();
        }

        private void InvertColor_Click(object sender, EventArgs e)
        {
            InvertImage();
        }

        private void ConvertToGray_Click(object sender, EventArgs e)
        {
            ConvertImageToGray();
        }

        private void FlipVertical_Click(object sender, EventArgs e)
        {
            FlipVertical();
        }

        private void FlipHorizontal_Click(object sender, EventArgs e)
        {
            FlipHorizontal();
        }

        private void RotateRight_Click(object sender, EventArgs e)
        {
            RotateRight();
        }

        private void RotateLeft_Click(object sender, EventArgs e)
        {
            RotateLeft();
        }

        private void AllowResize_Click(object sender, EventArgs e)
        {
            ToggleResize();
        }

        private void OCR_Click(object sender, EventArgs e)
        {
            OCR_Image();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            AskSaveImage();
        }

        private void Destroy_Click(object sender, EventArgs e)
        {
            ClipManager.DestroyClip(ClipName);
        }

        private void DestroyAllClips_Click(object sender, EventArgs e)
        {
            ClipManager.DestroyAllClips();
        }

        #endregion



        #region other events

        private void UpdateTheme(object sender, EventArgs e)
        {
            UpdateTheme();
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            switch (e.KeyData)
            {
                case (Keys.Z | Keys.Shift | Keys.Control):
                case (Keys.Y | Keys.Control):
                    Redo();
                    break;

                case (Keys.Z | Keys.Control):
                    Undo();
                    break;

                case (Keys.C | Keys.Control):
                    CopyImage();
                    break;

                case (Keys.T | Keys.Control):
                    OCR_Image();
                    break;

                case (Keys.R | Keys.Control):
                    ToggleResize();
                    break;

                case (Keys.S | Keys.Control):
                    AskSaveImage();
                    break;

                case Keys.Escape:
                    ClipManager.DestroyClip(ClipName);
                    break;

                case Keys.Tab:
                    WindowState = FormWindowState.Minimized;
                    break;
            }
        }

        private void ResizeEnded(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void ClipForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                _ZoomRefreshLimiter.Restart();
                _ZoomLevel = Math.Round(_ZoomLevel * 1.1d, 2);
                _MagnifierShown = true;
                zdbZoomedImageDisplay._Show();
                DrawZoomedImage(e.Location);
            }
            else
            {
                _ZoomLevel = Math.Round(_ZoomLevel * 0.9d, 2);
                if (_ZoomLevel <= 1)
                {
                    _ZoomRefreshLimiter.Reset();

                    _MagnifierShown = false;
                    _ZoomLevel = 1;

                    zdbZoomedImageDisplay._Hide();
                    ScaledImage?.Dispose();
                    ScaledImage = null;
                    //GC.Collect();
                }
                else
                {
                    DrawZoomedImage(e.Location);
                }
            }
        }

        private void MouseMove_Event(object sender, MouseEventArgs e)
        {
            if (_MagnifierShown)
            {
                if (_ZoomRefreshLimiter.ElapsedMilliseconds > Options.ZoomRefreshRate)
                {
                    DrawZoomedImage(e.Location);
                    _ZoomRefreshLimiter.Restart();
                }
            }
            else if (_IsResizable && !_IsMoving)
            {
                if (_IsResizing)
                {
                    Point mousepos = ScreenHelper.GetCursorPosition();
                    switch (_DragLocation)
                    {
                        case DragLoc.Top:
                            ResizeHeight(Height + Location.Y - mousepos.Y);
                            Location = new Point(Location.X, mousepos.Y);
                            break;
                        case DragLoc.Left:
                            ResizeWidth(Width + Location.X - mousepos.X);
                            Location = new Point(mousepos.X, Location.Y);
                            break;
                        case DragLoc.Right:
                            ResizeWidth(mousepos.X - Location.X);
                            break;
                        case DragLoc.Bottom:
                            ResizeHeight(mousepos.Y - Location.Y);
                            break;
                    }

                    Invalidate();

                    _ZoomControlSize = new Size(
                        (int)Math.Round(ClientSize.Width * SettingsManager.ClipSettings.Zoom_Size_From_Percent),
                        (int)Math.Round(ClientSize.Height * SettingsManager.ClipSettings.Zoom_Size_From_Percent));
                }
                else
                {
                    Point m = e.Location;

                    if (m.X >= Size.Width - Options.BorderThickness - Options.BorderGrabSize)
                    {
                        Cursor = Cursors.SizeWE;
                        if (_IsLeftClicking)
                        {
                            _DragLocation = DragLoc.Right;
                            _IsResizing = true;
                        }
                    }
                    else if (m.Y >= Size.Height - Options.BorderThickness - Options.BorderGrabSize)
                    {
                        Cursor = Cursors.SizeNS;
                        if (_IsLeftClicking)
                        {
                            _DragLocation = DragLoc.Bottom;
                            _IsResizing = true;
                        }
                    }
                    else if (m.X < Options.BorderThickness + Options.BorderGrabSize)
                    {
                        Cursor = Cursors.SizeWE;
                        if (_IsLeftClicking)
                        {
                            _DragLocation = DragLoc.Left;
                            _IsResizing = true;
                        }
                    }
                    else if (m.Y < Options.BorderThickness + Options.BorderGrabSize)
                    {
                        Cursor = Cursors.SizeNS;
                        if (_IsLeftClicking)
                        {
                            _DragLocation = DragLoc.Top;
                            _IsResizing = true;
                        }
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
            if (_IsLeftClicking && !_IsResizing)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - _LastClickLocation.X, p.Y - _LastClickLocation.Y);
                _IsMoving = true;
            }
        }

        private void MouseDown_Event(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    _IsLeftClicking = true;
                    _LastClickLocation = new Point(e.X, e.Y);
                    break;

                case MouseButtons.Right:
                    break;

                case MouseButtons.Middle:
                    break;
            }
        }

        private void MouseUp_Event(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    _IsLeftClicking = false;
                    _IsResizing = false;
                    _IsMoving = false;
                    break;

                case MouseButtons.Right:
                    break;

                case MouseButtons.Middle:
                    break;
            }
        }

        #endregion


        #region overrides

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.CompositingMode = CompositingMode.SourceOver;

            g.DrawImage(
                Image,
                new Rectangle(
                    new Point(Options.BorderThickness, Options.BorderThickness),
                    new Size(Width - Options.BorderThickness * 2, Height - Options.BorderThickness * 2)),
                new Rectangle(0, 0, Image.Width, Image.Height),
                GraphicsUnit.Pixel
                );

            base.OnPaint(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000; // <--- use 0x20000
                return cp;
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            _ChangeTracker?.Dispose();
            cmMain?.Dispose();
            ScaledImage?.Dispose();
            zdbZoomedImageDisplay?.Dispose();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion


        #region other

        /// <summary>
        /// 
        /// this is called 1000 ms after the clip is made, this is because
        /// if the clip is made on a monitor that has dpi scaling 
        /// it makes the clip bigger than it should be, so the max size
        /// is set 1 second after its made to prevent that
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateMaxSizeTimerTick_Event(object sender, EventArgs e)
        {
            ((Timer)sender)?.Stop();
            ((Timer)sender)?.Dispose();
            MaximumSize = InternalSettings.Max_Clip_Size;
            Height = StartWindowSize.Height;
            Invalidate();
        }

        /// <summary>
        /// Used to keep the aspect ratio when resizing.
        /// </summary>
        /// <param name="newWidth"></param>
        private void ResizeWidth(int newWidth)
        {
            Height = (int)(newWidth * (StartWindowSize.Height / (float)StartWindowSize.Width));
            Width = newWidth;
        }

        /// <summary>
        /// Used to keep the aspect ratio when resizing.
        /// </summary>
        /// <param name="newHeight"></param>
        private void ResizeHeight(int newHeight)
        {
            Width = (int)(newHeight * (StartWindowSize.Width / (float)StartWindowSize.Height));
            Height = newHeight;
        }

        #endregion
    }
}
