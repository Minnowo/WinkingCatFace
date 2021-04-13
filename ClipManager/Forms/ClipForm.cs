﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using WinkingCat.HelperLibs;
using System.Windows.Forms.VisualStyles;
using WinkingCat.Uploaders;
using System.Diagnostics;

namespace WinkingCat.ClipHelper
{
    public enum DragLoc
    {
        Top,
        Left,
        Right,
        Bottom
    }
    public partial class ClipForm : Form
    {
        public Size imageSize { get; private set; }
        public Size imageDefaultSize { get; private set; }
        public Size startWindowSize { get; private set; }
        public Point lastLocation { get; private set; }

        public ClipOptions Options { get; private set; }
        public Bitmap image { get; private set; }
        public Bitmap zoomedImage { get; private set; } = null;

        public string ClipName { get; private set; }

        private Stopwatch zoomRefreshRate = new Stopwatch();

        private double zoomLevel = 1;

        private DragLoc drag;

        private Size zoomControlSize;

        private bool isLeftClicking = false;
        private bool isResizing = false;
        public bool isResizable = true;
        public bool isMoving = false;
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
            image = (Bitmap)displayImage;

            startWindowSize = new Size(
                imageSize.Width + Options.borderThickness * 2,
                imageSize.Height + Options.borderThickness * 2);

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
            FormClosing += ClipForm_FormClosing;
            ResizeEnd += ResizeEnded;
            KeyDown += FormKeyDown;
            MouseWheel += ClipForm_MouseWheel;

            ApplicationStyles.UpdateStylesEvent += UpdateTheme;

            #region context menu
            cmMain.Opening += CmMain_Opening;
            tsmiCopyToolStripMenuItem.Click += CopyClipImage;
            tsmiAllowResizeToolStripMenuItem.Click += AllowResize_Click;
            tsmiOCRToolStripMenuItem.Click += OCR_Click;
            tsmiSaveToolStripMenuItem.Click += Save_Click;
            tsmiDestroyToolStripMenuItem.Click += Destroy_Click;
            tsmiDestroyAllClipsToolStripMenuItem.Click += DestroyAllClips_Click;

            tsmiCopyDefaultContextMenuItem.Click += CopyClipImage;
            tsmiCopyZoomScaledContextMenuItem.Click += CopyScaledImage;
            tsmiCopyZoomedImage.Click += TsmiCopyZoomedImage_Click;

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
                    GC.Collect();
                }
                else
                {
                    DrawZoomedImage(e.Location);
                }
            }
        }

        private void DrawZoomedImage(Point mousePos)
        {

            zdbZoomedImageDisplay.Size = new Size(
                zoomControlSize.Width + zdbZoomedImageDisplay.BorderThickness * 2,
                zoomControlSize.Height + zdbZoomedImageDisplay.BorderThickness * 2);

            if (ApplicationStyles.currentStyle.clipStyle.zoomFollowMouse)
            {
                zdbZoomedImageDisplay.Location = new Point(
                    mousePos.X - zdbZoomedImageDisplay.ClientSize.Width / 2,
                    mousePos.Y - zdbZoomedImageDisplay.ClientSize.Height / 2);
            }

            // if the clip has been dragged larger then we need 
            // to resize the image before trying to zoom in on it
            if (ClientSize != startWindowSize)
            {
                Size scaledImageSize = new Size(
                    Width - Options.borderThickness * 2,
                    Height - Options.borderThickness * 2);

                // if the zoomed image is null or not the size its supposed to be
                // try and dispose of it, then remake it
                if (zoomedImage == null || zoomedImage.Size != scaledImageSize)
                {
                    zoomedImage?.Dispose();
                    zoomedImage = new Bitmap(
                        scaledImageSize.Width,
                        scaledImageSize.Height);

                    using (Graphics g = Graphics.FromImage(zoomedImage))
                    {
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;
                        g.PixelOffsetMode = PixelOffsetMode.Half;

                        g.DrawImage(
                            image,
                            new Rectangle(
                                new Point(Options.borderThickness, Options.borderThickness),
                                new Size(Width - Options.borderThickness * 2, Height - Options.borderThickness * 2)),
                            new Rectangle(0, 0, image.Width, image.Height),
                            GraphicsUnit.Pixel
                            );
                    }
                }

                zdbZoomedImageDisplay.DrawImage(
                    zoomedImage,
                    new Rectangle(0, 0, zoomControlSize.Width, zoomControlSize.Height),
                    new Rectangle(
                        mousePos.X - (int)Math.Round(zdbZoomedImageDisplay.Size.Width / zoomLevel / 2),
                        mousePos.Y - (int)Math.Round(zdbZoomedImageDisplay.Size.Height / zoomLevel / 2),
                        (int)Math.Round(zoomControlSize.Width / zoomLevel),
                        (int)Math.Round(zoomControlSize.Height / zoomLevel)));
            }
            else
            {
                zdbZoomedImageDisplay.DrawImage(
                    this.image,
                    new Rectangle(0, 0, zoomControlSize.Width, zoomControlSize.Height),
                    new Rectangle(
                        mousePos.X - (int)Math.Round(zdbZoomedImageDisplay.Size.Width / zoomLevel / 2),
                        mousePos.Y - (int)Math.Round(zdbZoomedImageDisplay.Size.Height / zoomLevel / 2),
                        (int)Math.Round(zoomControlSize.Width / zoomLevel),
                        (int)Math.Round(zoomControlSize.Height / zoomLevel)));
            }

        }

        public void UpdateTheme(object sender, EventArgs e)
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

        private void TsmiCopyZoomedImage_Click(object sender, EventArgs e)
        {
            if (zdbZoomedImageDisplay.image != null)
            {
                ClipboardHelper.CopyImageDefault(zdbZoomedImageDisplay.image);
            }
        }

        public void CopyScaledImage(object sender = null, EventArgs e = null)
        {
            using (Bitmap img = ImageHelper.ResizeImage(this.image, new Size(Width - Options.borderThickness, Height - Options.borderThickness)))
            {
                ClipboardHelper.CopyImageDefault(img);
            }
        }

        public void CopyClipImage(object sender = null, EventArgs e = null)
        {
            ClipboardHelper.CopyImageDefault(image);
            cmMain.Close();
        }

        public void AllowResize_Click(object sender = null, EventArgs e = null)
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

        public void OCR_Click(object sender = null, EventArgs e = null)
        {
            if (System.IO.File.Exists(Options.filePath))
            {
                OCRForm form = new OCRForm(Options.filePath);
                form.Owner = this;
                form.TopMost = true;
                form.Show();
            }
            else
            {
                string fileName = ImageHelper.newImagePath;

                if (ImageHelper.Save(fileName, this.image))
                {
                    OCRForm form = new OCRForm(fileName);
                    form.Owner = this;
                    form.TopMost = true;
                    form.Show();
                }
                else
                {
                    MessageBox.Show("The path to the image has does not exist, and the image failed to save");
                }
            }
        }

        public void Save_Click(object sender = null, EventArgs e = null)
        {
            PathHelper.AskSaveImage((Image)image.Clone());
        }

        public void Destroy_Click(object sender = null, EventArgs e = null)
        {
            ClipManager.DestroyClip(ClipName);
        }

        public void DestroyAllClips_Click(object sender = null, EventArgs e = null)
        {
            ClipManager.DestroyAllClips();
        }

        #endregion

        #region form events

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

        private void ClipForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            switch (e.KeyData)
            {
                case (Keys.C | Keys.Control):
                    CopyClipImage();
                    break;

                case (Keys.T | Keys.Control):
                    OCR_Click();
                    break;

                case (Keys.R | Keys.Control):
                    AllowResize_Click();
                    break;

                case (Keys.S | Keys.Control):
                    Save_Click();
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
            Refresh();
        }

        private void ResizeWidth(int newWidth)
        {
            Height = (int)(newWidth * (startWindowSize.Height / (float)startWindowSize.Width));
            Width = newWidth;
        }

        private void ResizeHeight(int newHeight)
        {
            Width = (int)(newHeight * (startWindowSize.Width / (float)startWindowSize.Height));
            Height = newHeight;
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
        #endregion



    }
}
