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
    public partial class ClipForm : Form
    {
        private const int RESIZE_HANDLE_SIZE = 20;


        public Size imageSize { get; private set; }
        public Size windowSize { get; private set; }
        public Size minSize { get; private set; }
        public Size maxSize { get; private set; }
        public Point lastLocation { get; private set; }
        
        public ClipOptions Options { get; private set; }
        public string ClipName { get; private set; }

        private bool isLeftClicking { get; set; } = false;
        public bool isResizable { get; set; } = true;
        private bool minimizedFlag { get; set; } = false;

        public ClipForm(ClipOptions options, Image displayImage,string clipName)
        {
            InitializeComponent();
            SuspendLayout();
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            Options = options;
            imageSize = displayImage.Size;
            ClipName = clipName;
            windowSize = new Size(imageSize.Width + Options.borderThickness * 2, imageSize.Height + Options.borderThickness * 2);

            Location = options.location;

            Size = windowSize;

            MinimumSize = windowSize;
            MaximumSize = windowSize;
            
            BackColor = Options.borderColor;

            #region picturebox
            clipDisplayPictureBox.Location = new Point(Options.borderThickness, Options.borderThickness);
            clipDisplayPictureBox.Size = imageSize;
            //clipDisplayPictureBox.Image = displayImage;

            clipDisplayPictureBox.MouseDown += PictureBox_MouseDown;
            clipDisplayPictureBox.MouseUp += PictureBox_MouseUp;
            clipDisplayPictureBox.MouseMove += PictureBox_MouseMove;
            #endregion

            #region resizePanel
            ResizePanel.Size = new Size(25, 25);
            ResizePanel.Location = new Point(windowSize.Width - 25, windowSize.Height - 25);
            ResizePanel.MouseDown += FakeResize;
            #endregion

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
            KeyUp += FormKeyUp;
            KeyDown += FormKeyDown;

            TopMost = true;
            ResumeLayout();
            Show();
            #endregion
        }

        #region context menu functions
        public void CopyClipImage(object sender = null, EventArgs e = null)
        {
            ClipboardHelpers.CopyImageDefault(clipDisplayPictureBox.Image);
        }

        public void AllowResize_Click(object sender = null, EventArgs e = null)
        {
            if(isResizable)
            {
                isResizable = false;
                allowResizeToolStripMenuItem.Checked = false;
                ResizePanel.Visible = false;
            }
            else
            {
                isResizable = true;
                allowResizeToolStripMenuItem.Checked = true;
                ResizePanel.Visible = true;
            }
            Invalidate();
        }

        public void OCR_Click(object sender = null, EventArgs e = null)
        {

        }

        public void Save_Click(object sender = null, EventArgs e = null)
        {
            Helpers.AskSaveImage((Image)clipDisplayPictureBox.Image.Clone());
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
            }
        }

        private void FormKeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            switch (e.KeyData)
            {
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
            Console.WriteLine("resize end");
            if (Size != windowSize)
            {
                Size a = MathHelper.PictureBoxZoomSize(clipDisplayPictureBox, imageSize);
                a.Width += Options.borderThickness * 2;
                a.Height += Options.borderThickness * 2;
                Size = new Size(a.Width, a.Height);
                windowSize = Size;
            }
            Invalidate();
        }
        #endregion

        #region picturebox events
        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            Point m = e.Location;
            if (isLeftClicking)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - lastLocation.X, p.Y - lastLocation.Y);
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    isLeftClicking = true;
                    lastLocation = new Point(e.X, e.Y);

                    if (MaximumSize != Options.maxClipSize) // for some reason if the clip is on a display that is scaled it makes it bigger than its supposed to be so only allow it to be resized after clicking it once
                        MaximumSize = Options.maxClipSize;
                    break;

                case MouseButtons.Right:
                    break;

                case MouseButtons.Middle:
                    break;
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    isLeftClicking = false;
                    break;

                case MouseButtons.Right:
                    break;

                case MouseButtons.Middle:
                    break;
            }
        }
        #endregion

        #region other
        private void FakeResize(object sender = null, EventArgs e = null)
        {
            Point m = ScreenHelper.GetCursorPosition();
            NativeMethods.ReleaseCapture();
            NativeMethods.SendMessage(Handle, 0xA1, 17, Helpers.CreateLParam(m).ToInt32());
            isLeftClicking = false;

        }

        public void BringFront()
        {
            NativeMethods.SetForegroundWindow(Handle);
            NativeMethods.ShowWindow(Handle, (int)WindowShowStyle.Restore);
        }
        #endregion



        public void Destroy()
        {
            clipDisplayPictureBox.Image?.Dispose();
            clipDisplayPictureBox?.Dispose();

            ContextMenu?.Dispose();

            ResizePanel?.Dispose();

            base.Dispose(true);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = clipDisplayPictureBox.PointToClient(screenPoint);
                        if (isResizable)
                        {
                            if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                            {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13; //*HTTOPLEFT*//* ;
                                else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12; //*HTTOP*//* ;
                                else
                                m.Result = (IntPtr)14; //*HTTOPRIGHT*//* ;
                            }
                            else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                            {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10; //*HTLEFT*//* ;
                                else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2;//*HTCAPTION*//* ;
                            else
                                m.Result = (IntPtr)11; //*HTRIGHT*//* ;
                            }
                            else
                            {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16; //*HTBOTTOMLEFT*//;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15; //*HTBOTTOM*//* ;
                                else
                                m.Result = (IntPtr)17; //*HTBOTTOMRIGHT*//* ;
                            }
                        }
                    }
                    return;
            }
            base.WndProc(ref m);
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

    }
}
