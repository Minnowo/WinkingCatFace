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

                initialDraw = true;
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

        public bool centerOnLoad { get; set; } = true;
        
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

        private bool isLeftClicking = false;
        private bool initialDraw = false;

        public DrawingBoard()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
            this.MouseDown += ImageViewer_MouseDown;
            this.MouseUp += ImageViewer_MouseUp;
            this.MouseWheel += ImageViewer_MouseWheel;
            this.MouseMove += ImageViewer_MouseMove;
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
            centerPoint.X = origin.X + srcRect.Width / 2;
            centerPoint.Y = origin.Y + srcRect.Height / 2;

            if (zoomIn)
            {
                zoomFactor = Math.Round(zoomFactor * 1.1d, 2);
            }
            else
            {
                zoomFactor = Math.Round(zoomFactor * 0.9d, 2);
            }


            origin = new Point( centerPoint.X - (int)Math.Round(ClientSize.Width / zoomFactor / 2),
                                centerPoint.Y - (int)Math.Round(ClientSize.Height / zoomFactor / 2));

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
            g.CompositingMode = CompositingMode.SourceOver;
            g.CompositingQuality = CompositingQuality.HighSpeed;

            srcRect = new Rectangle(origin.X, origin.Y, drawWidth, drawHeight);

            if (initialDraw && centerOnLoad)
            {
                if (Image.Width < drawWidth)
                {
                    destRect.X = ClientSize.Width / 2 - (Image.Width / 2);
                    origin.X = -(ClientSize.Width / 2 - (Image.Width / 2));
                }
                if (Image.Height < drawHeight)
                {
                    destRect.Y = ClientSize.Height / 2 - (Image.Height / 2);
                    origin.Y = -(ClientSize.Height / 2 - (Image.Height / 2));
                }


                g.DrawImage(originalImage, destRect, srcRect, GraphicsUnit.Pixel);
                destRect.X = 0;
                destRect.Y = 0;
                initialDraw = false;
                Console.WriteLine("init");
            }
            else
            {
                g.DrawImage(originalImage, destRect, srcRect, GraphicsUnit.Pixel);
            }
            
            OnScrollChanged();
        }


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

        private Point PointToImage(Point p)
        {
            return new Point((int)((p.X + origin.X) / zoomFactor), (int)((p.Y + origin.Y) / zoomFactor));
        }

        private void ImageViewer_MouseDown(object sender, MouseEventArgs e)
        {
            if (originalImage == null)
                return;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    startPoint = PointToImage(e.Location);
                    ComputeDrawingArea();
                    isLeftClicking = true;
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
                Point p = PointToImage(e.Location);
                origin.X = origin.X + (startPoint.X - p.X);
                origin.Y = origin.Y + (startPoint.Y - p.Y);
                startPoint = PointToImage(e.Location);
                //CheckBounds();
                Invalidate();
            }
        }

        /*private void CheckBounds()
        {
            if (originalImage == null)
                return;



            if (origin.X < 0)
            {
                origin.X = 0;
            }
            else if (origin.X > originalImage.Width - (int)(ClientSize.Width / zoomFactor))
            {
                origin.X = originalImage.Width - (int)(ClientSize.Width / zoomFactor);
                if (origin.X < 0)
                {
                    origin.X = 0;
                }
            }

            if (origin.Y < 0)
            {
                origin.Y = 0;
            }
            else if (origin.Y > originalImage.Height - (int)(ClientSize.Height / zoomFactor))
            {
                origin.Y = originalImage.Height - (int)(ClientSize.Height / zoomFactor);
                if (origin.Y < 0)
                {
                    origin.Y = 0;
                }
            }
    }*/

        private void ComputeDrawingArea()
        {
            drawHeight = (int)(Height / zoomFactor);
            drawWidth = (int)(Width / zoomFactor);
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
            e.Graphics.Clear(ApplicationStyles.currentStyle.mainFormStyle.imageViewerBackColor);
            Graphics g = e.Graphics;
            //g.CompositingMode = CompositingMode.SourceCopy;
            DrawImage(g);

            //base.OnPaint(e);
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
