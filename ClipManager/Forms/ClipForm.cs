using System;
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
        private const int RESIZE_HANDLE_SIZE = 7;

        public Size imageSize { get; private set; }
        public Size imageDefaultSize { get; private set; }
        public Size startWindowSize { get; private set; }
        public Point lastLocation { get; private set; }

        public ClipOptions Options { get; private set; }
        public string ClipName { get; private set; }
        public Bitmap image { get; private set; }
        public Bitmap zoomImage { get; private set; }
        private TextureBrush backgroundBrush { get; set; }

        private DragLoc drag { get; set; }
        private bool isLeftClicking { get; set; } = false;
        private bool isResizing { get; set; } = false;
        public bool isResizable { get; set; } = true;
        public bool isMoving { get; set; } = false;

        public ClipForm(ClipOptions options, Image displayImage, string clipName)
        {
            NativeMethods.SetProcessDpiAwarenessContext(-3);
            InitializeComponent();
            SuspendLayout();

            Options = options;
            Options.borderThickness = 2;
            imageSize = displayImage.Size;
            imageDefaultSize = displayImage.Size;
            ClipName = clipName;
            image = (Bitmap)displayImage;
            startWindowSize = new Size(imageSize.Width + Options.borderThickness, imageSize.Height + Options.borderThickness);
            Console.WriteLine(startWindowSize);

            MinimumSize = startWindowSize;
            MaximumSize = startWindowSize;

            // why tf can't you make the width / height of a windows form bigger than the screen width + 12 its bs
            Bounds = new Rectangle(options.location, startWindowSize); 
            BackColor = Options.borderColor;


            MouseDown += MouseDown_Event;
            MouseUp += MouseUp_Event;
            MouseMove += MouseMove_Event;

            #region context menu
            copyToolStripMenuItem.Click += CopyClipImage;
            allowResizeToolStripMenuItem.Click += AllowResize_Click;
            oCRToolStripMenuItem.Click += OCR_Click;
            saveToolStripMenuItem.Click += Save_Click;
            destroyToolStripMenuItem.Click += Destroy_Click;
            destroyAllClipsToolStripMenuItem.Click += DestroyAllClips_Click;

            if (isResizable) allowResizeToolStripMenuItem.Checked = true;
            #endregion

            #region form
            ResizeEnd += ResizeEnded;
            KeyDown += FormKeyDown;

            TopMost = true;
            ResumeLayout(true);
            Show();
            #endregion

            backgroundBrush = new TextureBrush(image) { WrapMode = WrapMode.Clamp };


            //for some reason if the clip is on a display that is scaled it makes it bigger than its supposed to be
            //so only allow it to be resized after 1000 ms
            Timer updateMaxSizeTimer = new Timer() { Interval = 1000 };
            updateMaxSizeTimer.Tick += UpdateMaxSizeTimerTick_Event;
            updateMaxSizeTimer.Start();

        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality; // for some reason highspeed crashes the window
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.CompositingMode = CompositingMode.SourceOver;

            g.FillRectangle(backgroundBrush, new Rectangle(
                new Point(Options.borderThickness, Options.borderThickness), 
                new Size(Width, Height)));
            

            base.OnPaint(e);
        }

        #region context menu functions
        public void CopyClipImage(object sender = null, EventArgs e = null)
        {
            //ClipboardHelpers.CopyImageDefault(clipDisplayPictureBox.Image);
        }

        public void AllowResize_Click(object sender = null, EventArgs e = null)
        {
            if (isResizable)
            {
                isResizable = false;
                allowResizeToolStripMenuItem.Checked = false;
            }
            else
            {
                isResizable = true;
                allowResizeToolStripMenuItem.Checked = true;
            }
            Invalidate();
        }

        public void OCR_Click(object sender = null, EventArgs e = null)
        {

        }

        public void Save_Click(object sender = null, EventArgs e = null)
        {
            //Helpers.AskSaveImage((Image)clipDisplayPictureBox.Image.Clone());
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
        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            switch (e.KeyData)
            {
                case (Keys.C | Keys.Control):
                    CopyClipImage();
                    break;

                case (Keys.T | Keys.Control):

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
        #endregion

        private void NewBrush()
        {
            backgroundBrush?.Dispose();
            Bitmap bmp = new Bitmap(Width - Options.borderThickness, Height - Options.borderThickness);
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.InterpolationMode = InterpolationMode.NearestNeighbor;
                gr.PixelOffsetMode = PixelOffsetMode.Half;

                gr.DrawImage(image, new Rectangle(0, 0, Width - Options.borderThickness, Height - Options.borderThickness),
                    new Rectangle(0, 0, image.Width, image.Height),
                    GraphicsUnit.Pixel);

                gr.PixelOffsetMode = PixelOffsetMode.None;
            }
            backgroundBrush = new TextureBrush(bmp) { WrapMode = WrapMode.Clamp };
            bmp.Dispose();
        }

        #region Mouse Events
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
                            if (mousepos.Y < Location.Y)
                            {
                                ResizeHeight(Location.Y - mousepos.Y);
                                Location = new Point(Location.X, mousepos.Y);
                            }
                            break;
                        case DragLoc.Left:
                            if (mousepos.X < Location.X)
                            {
                                ResizeHeight(Location.X - mousepos.X);
                                Location = new Point(mousepos.X, Location.Y);
                            }
                            break;
                        case DragLoc.Right:
                            if (mousepos.X > Location.X)
                            {
                                ResizeWidth(mousepos.X - Location.X);
                            }
                            break;
                        case DragLoc.Bottom:
                            if (mousepos.Y > Location.Y)
                            {
                                ResizeHeight(mousepos.Y - Location.Y);
                            }
                            break;
                    }
                    
                    NewBrush();
                    Invalidate();
                }
                else
                {
                    if (!isMoving)
                    {
                        Point m = e.Location;

                        if ((m.X >= Size.Width - Options.borderThickness - 1))
                        {
                            Cursor = Cursors.Hand;
                            if (isLeftClicking)
                            {
                                drag = DragLoc.Right;
                                isResizing = true;
                            }
                        }
                        else if ((m.Y >= Size.Height - Options.borderThickness - 1))
                        {
                            Cursor = Cursors.Hand;
                            if (isLeftClicking)
                            {
                                drag = DragLoc.Bottom;
                                isResizing = true;
                            }
                        }
                        else if (m.X < Options.borderThickness)
                        {
                            Cursor = Cursors.Hand;
                            if (isLeftClicking)
                            {
                                drag = DragLoc.Left;
                                isResizing = true;
                            }
                        }
                        else if (m.Y < Options.borderThickness)
                        {
                            Cursor = Cursors.Hand;
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
        private void UpdateMaxSizeTimerTick_Event(object sender, EventArgs e)
        {
            ((Timer)sender)?.Stop();
            ((Timer)sender)?.Dispose();
            MaximumSize = Options.maxClipSize;
            Height = startWindowSize.Height;
            //Size = startWindowSize;
            Console.WriteLine(Size);
            Refresh();
        }

        private void ResizeWidth(int newWidth)
        {
            float aspectRatio = (startWindowSize.Height / (float)startWindowSize.Width);
            Height = (int)(newWidth * aspectRatio);
            Width = newWidth;
        }

        private void ResizeHeight(int newHeight)
        {
            float aspectRatio = (startWindowSize.Width / (float)startWindowSize.Height);
            Width = (int)(newHeight * aspectRatio);
            Height = newHeight;
        }

        public void BringFront()
        {
            NativeMethods.SetForegroundWindow(Handle);
            NativeMethods.ShowWindow(Handle, (int)WindowShowStyle.Restore);
        }

        public void Destroy()
        {
            ContextMenu?.Dispose();
            image?.Dispose();
            zoomImage?.Dispose();
            backgroundBrush?.Dispose();
            base.Dispose(true);
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
