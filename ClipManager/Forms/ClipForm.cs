using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.IO;

using WinkingCat.Uploaders;
using WinkingCat.HelperLibs;

namespace WinkingCat.ClipHelper
{
    public enum DragLoc
    {
        Top,
        Left,
        Right,
        Bottom
    }
    public partial class ClipForm : Form, IUndoable
    {
        public Size imageSize { get; private set; }
        public Size imageDefaultSize { get; private set; }
        public Size startWindowSize { get; private set; }
        public Point lastLocation { get; private set; }

        public ClipOptions Options { get; private set; }
        public Bitmap image { get; private set; }
        public Bitmap zoomedImage { get; private set; }

        public string ClipName { get; private set; }

        private BitmapUndo changeTracker;

        private Stopwatch zoomRefreshRate = new Stopwatch();

        private double zoomLevel = 1;
        private double aspectRatio = 0;

        private DragLoc drag;

        private Size zoomControlSize;

        private bool isLeftClicking = false;
        private bool isResizing = false;
        private bool isResizable = true;
        private bool isMoving = false;
        private bool isRotated = false;
        private bool showingZoomed = false;

        public ClipForm(ClipOptions options, Image displayImage)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
            SuspendLayout();

            this.Text = options.uuid;
            ClipName = options.uuid;

            Options = options;

            imageSize = displayImage.Size;
            imageDefaultSize = displayImage.Size;
            aspectRatio = imageSize.Width / (double)imageSize.Height;
            image = (Bitmap)displayImage;

            changeTracker = new BitmapUndo(image);

            startWindowSize = new Size(
                imageSize.Width + (Options.borderThickness << 1),
                imageSize.Height + (Options.borderThickness << 1));

            zoomControlSize = new Size(
                (int)Math.Round(startWindowSize.Width * ApplicationStyles.currentStyle.clipStyle.ZoomSizePercent),
                (int)Math.Round(startWindowSize.Height * ApplicationStyles.currentStyle.clipStyle.ZoomSizePercent));

            MinimumSize = startWindowSize;
            MaximumSize = startWindowSize;

            // why tf can't you make the width / height of a windows form bigger than the screen width + 12 its bs
            Bounds = new Rectangle(options.location, startWindowSize);
            BackColor = Options.borderColor;

            zdbZoomedImageDisplay.Enabled = false;
            zdbZoomedImageDisplay.borderColor = ApplicationStyles.currentStyle.clipStyle.zoomBorderColor;
            zdbZoomedImageDisplay.replaceTransparent = ApplicationStyles.currentStyle.clipStyle.zoomReplaceTransparentColor;
            if (ApplicationStyles.currentStyle.clipStyle.ZoomSizePercent == 1)
            {
                zdbZoomedImageDisplay.BorderThickness = 0;
            }
            else
            {
                zdbZoomedImageDisplay.BorderThickness = ApplicationStyles.currentStyle.clipStyle.zoomBorderThickness;
            }


            MouseDown += MouseDown_Event;
            MouseUp += MouseUp_Event;
            MouseMove += MouseMove_Event;
            ResizeEnd += ResizeEnded;
            KeyDown += FormKeyDown;
            MouseWheel += ClipForm_MouseWheel;

            changeTracker.BitmapChanged += ChangeTracker_BitmapChanged;

            ApplicationStyles.UpdateStylesEvent += UpdateTheme;

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

            if (isResizable) tsmiAllowResizeToolStripMenuItem.Checked = true;
            #endregion

            TopMost = true;
            ResumeLayout(true);
            Show();

            UpdateTheme(null, EventArgs.Empty);

            //for some reason if the clip is on a display that is scaled it makes it bigger than its supposed to be
            //so only allow it to be resized after 1000 ms
            Timer updateMaxSizeTimer = new Timer() { Interval = 1000 };
            updateMaxSizeTimer.Tick += UpdateMaxSizeTimerTick_Event;
            updateMaxSizeTimer.Start();

        }

        

        /// <summary>
        /// Checks if the window should adjust its size to match a newly rotated image.
        /// </summary>
        public void UpdateWindow()
        {
            double aspectR = changeTracker.CurrentBitmap.Width / (double)changeTracker.CurrentBitmap.Height;

            if(aspectR.CompareTo(aspectRatio) == 0)
                return;

            // if the aspect ratio changed it means that the image has been rotated left or right
            // so swap the width and height to rotate
            aspectRatio = aspectR;

            Size oldWindowSize = new Size(Width, Height);

            MinimumSize =   new Size(MinimumSize.Height, MinimumSize.Width);
            Size =          new Size(oldWindowSize.Height, oldWindowSize.Width);
        }

        /// <summary>
        /// Updates the theme of the form.
        /// </summary>
        public void UpdateTheme()
        {
            if (ApplicationStyles.currentStyle.mainFormStyle.useImersiveDarkMode)
            {
                this.Icon = Properties.Resources._3white;
            }
            else
            {
                this.Icon = Properties.Resources._3black;
            }

            cmMain.Renderer = new ToolStripCustomRenderer();
            cmMain.Opacity = ApplicationStyles.currentStyle.mainFormStyle.contextMenuOpacity;

            this.BackColor = ApplicationStyles.currentStyle.clipStyle.borderColor;

            zdbZoomedImageDisplay.borderColor = ApplicationStyles.currentStyle.clipStyle.zoomBorderColor;
            zdbZoomedImageDisplay.replaceTransparent = ApplicationStyles.currentStyle.clipStyle.zoomReplaceTransparentColor;

            zoomControlSize = new Size(
                (int)Math.Round(ClientSize.Width * ApplicationStyles.currentStyle.clipStyle.ZoomSizePercent),
                (int)Math.Round(ClientSize.Height * ApplicationStyles.currentStyle.clipStyle.ZoomSizePercent));

            Refresh();
        }

        /// <summary>
        /// Opens a save file dialog for the current image.
        /// </summary>
        public void AskSaveImage()
        {
            using (Image img = (Image)image.CloneSafe())
            {
                ImageHelper.SaveImageFileDialog(img);
            }
        }

        /// <summary>
        /// Opens an OCR form for the image.
        /// </summary>
        public void OCR_Image()
        {
            if (File.Exists(Options.filePath))
            {
                OCRForm form = new OCRForm(Options.filePath);
                form.Owner = this;
                form.TopMost = true;
                form.Show();
                return;
            }
            
            string fileName = ImageHelper.newImagePath;

            if (ImageHelper.SaveImage(fileName, this.image))
            {
                OCRForm form = new OCRForm(fileName);
                form.Owner = this;
                form.TopMost = true;
                form.Show();
                return;
            }
            
            MessageBox.Show("The path to the image has does not exist, and the image failed to save");
        }

        /// <summary>
        /// Toggles the ability to resize the form.
        /// </summary>
        public void ToggleResize()
        {
            if (isResizable)
            {
                isResizable = false;
                tsmiAllowResizeToolStripMenuItem.Checked = false;
            }
            else
            {
                isResizable = true;
                tsmiAllowResizeToolStripMenuItem.Checked = true;
            }
            Invalidate();
        }

        /// <summary>
        /// Copies the original image.
        /// </summary>
        public void CopyImage()
        {
            ClipboardHelper.CopyImageDefault(image);
            cmMain.Close();
        }

        /// <summary>
        /// Copies the zoomed image shown when the mouse wheel is zoomed in.
        /// </summary>
        public void CopyZoomedImage()
        {
            if (zdbZoomedImageDisplay.image != null)
            {
                ClipboardHelper.CopyImageDefault(zdbZoomedImageDisplay.image);
            }
        }

        /// <summary>
        /// Copies the image scaled to fill the form when its resized.
        /// </summary>
        public void CopyScaledImage()
        {
            using (Bitmap img = ImageHelper.ResizeImage(this.image, new Size(Width - Options.borderThickness, Height - Options.borderThickness)))
            {
                ClipboardHelper.CopyImageDefault(img);
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
            changeTracker.InvertBitmap();
            //ImageHelper.InvertBitmap(this.image);
            Invalidate();
        }

        /// <summary>
        /// Converts the image to grayscale.
        /// </summary>
        public void ConvertImageToGray()
        {
            changeTracker.ConvertToGray();
            //ImageHelper.GrayscaleBitmap(this.image);
            Invalidate();
        }

        /// <summary>
        /// Rotate the image 90 to the left.
        /// </summary>
        public void RotateLeft()
        {
            changeTracker.RotateLeft();
            Invalidate();
        }

        /// <summary>
        /// Rotate the image 90 to the right.
        /// </summary>
        public void RotateRight()
        {
            changeTracker.RotateRight();
            Invalidate();
        }

        /// <summary>
        /// Flip the image horizontally.
        /// </summary>
        public void FlipHorizontal()
        {
            changeTracker.FlipHorizontal();
            Invalidate();
        }

        /// <summary>
        /// Flip the image vertically.
        /// </summary>
        public void FlipVertical()
        {
            changeTracker.FlipVertical();
            Invalidate();
        }

        /// <summary>
        /// Redo the last change to the image.
        /// </summary>
        public void Redo()
        {
            if (changeTracker.RedoCount == 0)
                return;

            changeTracker.Redo();
            Invalidate();
        }

        /// <summary>
        /// Undo the last change to the image.
        /// </summary>
        public void Undo()
        {
            if (changeTracker.UndoCount == 0)
                return;

            changeTracker.Undo();
            Invalidate();
        }

        private void DrawZoomedImage(Point mousePos)
        {

            zdbZoomedImageDisplay.Size = new Size(
                zoomControlSize.Width + (zdbZoomedImageDisplay.BorderThickness << 1),
                zoomControlSize.Height + (zdbZoomedImageDisplay.BorderThickness << 1));

            if (ApplicationStyles.currentStyle.clipStyle.zoomFollowMouse)
            {
                zdbZoomedImageDisplay.Location = new Point(
                    mousePos.X - (zdbZoomedImageDisplay.ClientSize.Width >> 1),
                    mousePos.Y - (zdbZoomedImageDisplay.ClientSize.Height >> 1));
            }

            // if the clip has been dragged larger then we need 
            // to resize the image before trying to zoom in on it
            if (ClientSize != startWindowSize)
            {
                Size scaledImageSize = new Size(
                    Width - (Options.borderThickness << 1),
                    Height - (Options.borderThickness << 1));

                // if the zoomed image is null or not the size its supposed to be
                // try and dispose of it, then remake it
                if (zoomedImage == null || zoomedImage.Size != scaledImageSize)
                {
                    zoomedImage?.Dispose();
                    zoomedImage = new Bitmap(scaledImageSize.Width, scaledImageSize.Height);

                    using (Graphics g = Graphics.FromImage(zoomedImage))
                    {
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;
                        g.PixelOffsetMode = PixelOffsetMode.Half;

                        g.DrawImage(
                            image,
                            new Rectangle(new Point(Options.borderThickness, Options.borderThickness), scaledImageSize),
                            new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                    }
                }

                zdbZoomedImageDisplay.DrawImage(zoomedImage,
                    new Rectangle(0, 0, zoomControlSize.Width, zoomControlSize.Height),
                    new Rectangle(
                        mousePos.X - (int)Math.Round(zdbZoomedImageDisplay.Size.Width / zoomLevel / 2),
                        mousePos.Y - (int)Math.Round(zdbZoomedImageDisplay.Size.Height / zoomLevel / 2),
                        (int)Math.Round(zoomControlSize.Width / zoomLevel),
                        (int)Math.Round(zoomControlSize.Height / zoomLevel)));
            }
            else
            {
                zdbZoomedImageDisplay.DrawImage(this.image,
                    new Rectangle(0, 0, zoomControlSize.Width, zoomControlSize.Height),
                    new Rectangle(
                        mousePos.X - (int)Math.Round(zdbZoomedImageDisplay.Size.Width / zoomLevel / 2),
                        mousePos.Y - (int)Math.Round(zdbZoomedImageDisplay.Size.Height / zoomLevel / 2),
                        (int)Math.Round(zoomControlSize.Width / zoomLevel),
                        (int)Math.Round(zoomControlSize.Height / zoomLevel)));
            }

        }


        #region context menu functions

        private void ChangeTracker_BitmapChanged(BitmapChanges change)
        {
            UpdateWindow();
        }

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
                case (Keys.Z | Keys.Shift | Keys.LControlKey):
                case (Keys.Z | Keys.Shift | Keys.Control):
                case (Keys.Y | Keys.LControlKey):
                case (Keys.Y | Keys.Control):
                    Redo();
                    break;

                case (Keys.Z | Keys.LControlKey):
                case (Keys.Z | Keys.Control):
                    Undo();
                    break;

                case (Keys.C | Keys.LControlKey):
                case (Keys.C | Keys.Control):
                    CopyImage();
                    break;

                case (Keys.T | Keys.LControlKey):
                case (Keys.T | Keys.Control):
                    OCR_Image();
                    break;

                case (Keys.R | Keys.LControlKey):
                case (Keys.R | Keys.Control):
                    ToggleResize();
                    break;

                case (Keys.S | Keys.LControlKey):
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
                zoomRefreshRate.Restart();
                zoomLevel = Math.Round(zoomLevel * 1.1d, 2);
                showingZoomed = true;
                zdbZoomedImageDisplay._Show();
                DrawZoomedImage(e.Location);
            }
            else
            {
                zoomLevel = Math.Round(zoomLevel * 0.9d, 2);
                if (zoomLevel <= 1)
                {
                    zoomRefreshRate.Reset();

                    showingZoomed = false;
                    zoomLevel = 1;

                    zdbZoomedImageDisplay._Hide();
                    zoomedImage?.Dispose();
                    zoomedImage = null;
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
            if (showingZoomed)
            {
                if (zoomRefreshRate.ElapsedMilliseconds > ClipOptions.ZoomRefreshRate)
                {
                    DrawZoomedImage(e.Location);
                    zoomRefreshRate.Restart();
                }
            }
            else if (isResizable && !isMoving)
            {
                if (isResizing)
                {
                    Point mousepos = ScreenHelper.GetCursorPosition();
                    switch (drag)
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

                    zoomControlSize = new Size(
                        (int)Math.Round(ClientSize.Width * ApplicationStyles.currentStyle.clipStyle.ZoomSizePercent),
                        (int)Math.Round(ClientSize.Height * ApplicationStyles.currentStyle.clipStyle.ZoomSizePercent));
                }
                else
                {
                    Point m = e.Location;

                    if (m.X >= Size.Width - Options.borderThickness - ClipOptions.extendBorderGrabRange)
                    {
                        Cursor = Cursors.SizeWE;
                        if (isLeftClicking)
                        {
                            drag = DragLoc.Right;
                            isResizing = true;
                        }
                    }
                    else if (m.Y >= Size.Height - Options.borderThickness - ClipOptions.extendBorderGrabRange)
                    {
                        Cursor = Cursors.SizeNS;
                        if (isLeftClicking)
                        {
                            drag = DragLoc.Bottom;
                            isResizing = true;
                        }
                    }
                    else if (m.X < Options.borderThickness + ClipOptions.extendBorderGrabRange)
                    {
                        Cursor = Cursors.SizeWE;
                        if (isLeftClicking)
                        {
                            drag = DragLoc.Left;
                            isResizing = true;
                        }
                    }
                    else if (m.Y < Options.borderThickness + ClipOptions.extendBorderGrabRange)
                    {
                        Cursor = Cursors.SizeNS;
                        if (isLeftClicking)
                        {
                            drag = DragLoc.Top;
                            isResizing = true;
                        }
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
            if (isLeftClicking && !isResizing)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - lastLocation.X, p.Y - lastLocation.Y);
                isMoving = true;
            }
        }

        private void MouseDown_Event(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    isLeftClicking = true;
                    lastLocation = new Point(e.X, e.Y);
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
                    isLeftClicking = false;
                    isResizing = false;
                    isMoving = false;
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
                image,
                new Rectangle(
                    new Point(Options.borderThickness, Options.borderThickness),
                    new Size(Width - Options.borderThickness * 2, Height - Options.borderThickness * 2)),
                new Rectangle(0, 0, image.Width, image.Height),
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
            changeTracker?.Dispose();
            cmMain?.Dispose();
            image?.Dispose();
            zoomedImage?.Dispose();
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
            MaximumSize = Options.maxClipSize;
            Height = startWindowSize.Height;
            Invalidate();
        }

        /// <summary>
        /// Used to keep the aspect ratio when resizing.
        /// </summary>
        /// <param name="newWidth"></param>
        private void ResizeWidth(int newWidth)
        {
            Height = (int)(newWidth * (startWindowSize.Height / (float)startWindowSize.Width));
            Width = newWidth;
        }

        /// <summary>
        /// Used to keep the aspect ratio when resizing.
        /// </summary>
        /// <param name="newHeight"></param>
        private void ResizeHeight(int newHeight)
        {
            Width = (int)(newHeight * (startWindowSize.Width / (float)startWindowSize.Height));
            Height = newHeight;
        }

        #endregion
    }
}
