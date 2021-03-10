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

namespace WinkingCat.HelperLibs
{
    public partial class DrawingBoard : UserControl
    {
        public Bitmap Image
        {
            get
            {
                return originalImage;
            }
            set
            {
                if(originalImage != null)
                {
                    originalImage.Dispose();
                    origin = new Point(0, 0);
                    apparentImageSize = new Size(0, 0);
                    zoomFactor = 1f;
                    GC.Collect();
                }

                if(value == null)
                {
                    originalImage = null;
                    Invalidate();
                    return;
                }

                originalImage = new Bitmap(value.Width, value.Height, PixelFormat.Format24bppRgb);
                using (Graphics g = Graphics.FromImage(value))
                {
                    g.DrawImage(originalImage, new Point(0, 0));
                }
            }
        }


        private Bitmap originalImage;

        private Point startPoint;
        private Point origin = new Point(0, 0);
        private Point centerPoint;

        private Size apparentImageSize = new Size(0, 0);

        private int drawWidth;
        private int drawHeight;

        private float zoomFactor = 1.0f;

        private bool zoomOnMouseWheel = true;

        public DrawingBoard()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
        }

        #region Overrides

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            
            // draw image functions

            base.OnPaint(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {

            base.OnSizeChanged(e);
        }

        #endregion
    }
}
