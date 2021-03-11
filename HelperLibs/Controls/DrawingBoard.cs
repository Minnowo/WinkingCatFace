using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace WinkingCat.HelperLibs
{
    public partial class DrawingBoard : UserControl
    {
        public delegate void ScrollPositionChanged(object sender, EventArgs e);
        public event ScrollPositionChanged ScrollChanged;

        public Image Image
        {
            get
            {
                return originalImage;
            }
            set
            {
                if (originalImage != null)
                {
                    originalImage.Dispose();
                    origin = new Point(0, 0);
                    apparentImageSize = new Size(0, 0);
                    zoomFactor = 1f;
                    GC.Collect();
                }

                if (value == null)
                {
                    originalImage = null;
                    Invalidate();
                    return;
                }

                originalImage = new Bitmap(value.Width, value.Height, PixelFormat.Format24bppRgb);
                using (Graphics g = Graphics.FromImage(originalImage))
                {
                    g.DrawImage(value, new Point(0, 0));
                }
                Invalidate();
            }
        }


        public double ZoomFactor
        {
            get
            {
                return zoomFactor;
            }
            set
            {
                zoomFactor = value.Clamp(0.05, 15);

                if (originalImage != null)
                {
                    apparentImageSize.Height = (int)Math.Round(originalImage.Height * zoomFactor);
                    apparentImageSize.Width = (int)Math.Round(originalImage.Width * zoomFactor);
                    ComputeDrawingArea();
                    CheckBounds();
                }
                Invalidate();
            }
        }

        public Point Origin
        {
            get
            {
                return origin;
            }
            set
            {
                origin = value;
                Invalidate();
            }
        }

        public Size ApparentImageSize
        {
            get
            {
                return apparentImageSize;
            }
        }

        private Bitmap originalImage;

        private Rectangle srcRect;
        private Rectangle destRect;

        private Point startPoint;
        private Point origin = new Point(0, 0);
        private Point centerPoint;

        private Size apparentImageSize = new Size(0, 0);

        private int drawWidth;
        private int drawHeight;

        private double zoomFactor = 1.0d;

        //private bool zoomOnMouseWheel = true;
        private bool isLeftClicking = false;

        public DrawingBoard()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
            this.MouseDown += ImageViewer_MouseDown;
            this.MouseUp += ImageViewer_MouseUp;
            this.MouseWheel += ImageViewer_MouseWheel;
            this.MouseMove += ImageViewer_MouseMove;
            //this.Resize += DrawingBoard_Resize;
        }

        #region public properties

        public void ZoomIn()
        {
            ZoomImage(true);
        }

        public void ZoomOut()
        {
            ZoomImage(false);
        }


        public void FitToScreen()
        {
            Origin = new Point(0, 0);

            if (originalImage == null)
                return;
            else
                ZoomFactor = Math.Min(ClientSize.Width / originalImage.Width, ClientSize.Height / originalImage.Height);
        }
        #endregion

        #region private properites

        private void ZoomImage(bool zoomIn)
        {


            if (zoomIn)
            {
                zoomFactor = Math.Round(zoomFactor * 1.1d, 2);
            }
            else
            {
                zoomFactor = Math.Round(zoomFactor * 0.9d, 2);
            }

            centerPoint.X = origin.X + srcRect.Width / 2;
            centerPoint.Y = origin.Y + srcRect.Height / 2;
            origin = new Point(centerPoint.X - (int)Math.Round(ClientSize.Width / zoomFactor / 2),
                centerPoint.Y - (int)Math.Round(ClientSize.Height / zoomFactor / 2));

            CheckBounds();
            ComputeDrawingArea();
            Invalidate();
        }

        private void DrawImage(Graphics g)
        {
            if (originalImage == null)
                return;

            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;


            srcRect = new Rectangle(origin.X, origin.Y, drawWidth, drawHeight);

            g.DrawImage(originalImage, destRect, srcRect, GraphicsUnit.Pixel);

            OnScrollChanged();
        }

        /*        private void DrawingBoard_Resize(object sender, EventArgs e)
                {
                    ComputeDrawingArea();
                }*/

        private void ImageViewer_MouseWheel(object sender, MouseEventArgs e)
        {
            if (originalImage == null)
                return;

            if (e.Delta > 0)
            {
                ZoomImage(true);
            }
            else if (e.Delta < 0)
            {
                ZoomImage(false);
            }
        }

        private void ImageViewer_MouseUp(object sender, MouseEventArgs e)
        {
            if (originalImage == null)
                return;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    isLeftClicking = false;
                    break;
            }

            this.Focus();
        }

        private void ImageViewer_MouseDown(object sender, MouseEventArgs e)
        {
            if (originalImage == null)
                return;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    startPoint = e.Location;
                    isLeftClicking = true;
                    ComputeDrawingArea();
                    break;
            }

            this.Focus();
        }

        private void ImageViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (originalImage == null)
                return;

            if (isLeftClicking)
            {
                // need to fix the flicker when dragging left
                origin = new Point(origin.X + (int)Math.Round((startPoint.X - e.X) / zoomFactor), origin.Y + (int)Math.Round((startPoint.Y - e.Y) / zoomFactor));
                CheckBounds();
                Console.WriteLine(origin);
                startPoint = e.Location;
                Invalidate();
            }
        }

        private void CheckBounds()
        {
            if (originalImage == null)
                return;

            origin.X = origin.X.Clamp(0, originalImage.Width - (int)Math.Round(ClientSize.Width / zoomFactor));
            origin.Y = origin.Y.Clamp(0, originalImage.Height - (int)Math.Round(ClientSize.Height / zoomFactor));
        }

        private void ComputeDrawingArea()
        {
            drawHeight = (int)Math.Round(Height / zoomFactor);
            drawWidth = (int)Math.Round(Width / zoomFactor);
        }

        private void OnScrollChanged()
        {
            if (ScrollChanged != null)
            {
                ScrollChanged(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Overrides

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            Graphics g = e.Graphics;
            g.CompositingMode = CompositingMode.SourceOver;
            DrawImage(g);

            base.OnPaint(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            destRect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);
            ComputeDrawingArea();
            base.OnSizeChanged(e);
        }

        #endregion
    }
}
