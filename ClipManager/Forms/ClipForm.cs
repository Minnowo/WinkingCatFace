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
        public string ClipName { get; private set; }
        public Bitmap image { get; private set; }
        //public Bitmap zoomImage { get; private set; }

        private DragLoc drag { get; set; }
        private bool isLeftClicking { get; set; } = false;
        private bool isResizing { get; set; } = false;
        public bool isResizable { get; set; } = true;
        public bool isMoving { get; set; } = false;
        
        public ClipForm(ClipOptions options, Image displayImage)
        {
            InitializeComponent();
            SuspendLayout();

            this.Text = options.uuid;
            ClipName = options.uuid;

            Options = options;

            imageSize = displayImage.Size;
            imageDefaultSize = displayImage.Size;
            image = (Bitmap)displayImage;

            startWindowSize = new Size(imageSize.Width + Options.borderThickness, imageSize.Height + Options.borderThickness);

            MinimumSize = startWindowSize;
            MaximumSize = startWindowSize;

            // why tf can't you make the width / height of a windows form bigger than the screen width + 12 its bs
            Bounds = new Rectangle(options.location, startWindowSize); 
            BackColor = Options.borderColor;


            MouseDown += MouseDown_Event;
            MouseUp += MouseUp_Event;
            MouseMove += MouseMove_Event;
            FormClosing += ClipForm_FormClosing;
            ResizeEnd += ResizeEnded;
            KeyDown += FormKeyDown;

            ApplicationStyles.UpdateStylesEvent += UpdateTheme;

            #region context menu
            tsmiCopyToolStripMenuItem.Click += CopyClipImage;
            tsmiAllowResizeToolStripMenuItem.Click += AllowResize_Click;
            tsmiOCRToolStripMenuItem.Click += OCR_Click;
            tsmiSaveToolStripMenuItem.Click += Save_Click;
            tsmiDestroyToolStripMenuItem.Click += Destroy_Click;
            tsmiDestroyAllClipsToolStripMenuItem.Click += DestroyAllClips_Click;

            tsmiCopyDefaultContextMenuItem.Click += CopyClipImage;
            tsmiCopyZoomScaledContextMenuItem.Click += CopyScaledImage;

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
            
            Refresh();
        }

        #region context menu functions
        public void CopyScaledImage(object sender = null, EventArgs e = null)
        {
            using(Bitmap img = ImageHelper.ResizeImage(this.image, new Size(Width - Options.borderThickness, Height - Options.borderThickness)))
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

                if(ImageHelper.Save(fileName, this.image))
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
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
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
            if (isResizable)
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
                }
                else
                {
                    if (!isMoving)
                    {
                        Point m = e.Location;

                        if ((m.X >= Size.Width - Options.borderThickness - 1))
                        {
                            Cursor = Cursors.SizeWE;
                            if (isLeftClicking)
                            {
                                drag = DragLoc.Right;
                                isResizing = true;
                            }
                        }
                        else if ((m.Y >= Size.Height - Options.borderThickness - 1))
                        {
                            Cursor = Cursors.SizeNS;
                            if (isLeftClicking)
                            {
                                drag = DragLoc.Bottom;
                                isResizing = true;
                            }
                        }
                        else if (m.X < Options.borderThickness)
                        {
                            Cursor = Cursors.SizeWE;
                            if (isLeftClicking)
                            {
                                drag = DragLoc.Left;
                                isResizing = true;
                            }
                        }
                        else if (m.Y < Options.borderThickness)
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
