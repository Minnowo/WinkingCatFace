using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{
    public partial class ImageView : UserControl
    {

        public Image Image
        {
            get
            {
                return this.drawingBoard1.Image;
            }
            set
            {
                this.drawingBoard1.Image = value;
                if (value == null)
                {
                    hScrollBar1.Enabled = false;
                    vScrollBar1.Enabled = false;
                }
            }
        }
        public Double ZoomFactor
        {
            get
            {
                return drawingBoard1.ZoomFactor;
            }
            set
            {
                drawingBoard1.ZoomFactor = value;
            }
        }
        public Point Origin
        {
            get
            {
                return drawingBoard1.Origin;
            }
            set
            {
                drawingBoard1.Origin = value;
            }
        }
        public Size ApparentImageSize
        {
            get
            {
                return drawingBoard1.ApparentImageSize;
            }
        }
        public bool ScrollbarsVisible
        {
            get
            {
                return scrollVisible;
            }
            set
            {
                scrollVisible = value;
                this.hScrollBar1.Visible = value;
                this.vScrollBar1.Visible = value;

                if (value)
                {
                    this.drawingBoard1.Dock = DockStyle.None;
                    this.drawingBoard1.Location = new Point(0, 0);
                    this.drawingBoard1.Width = ClientSize.Width - vScrollBar1.Width;
                    this.drawingBoard1.Height = ClientSize.Height - hScrollBar1.Height;
                }
                else
                {
                    this.drawingBoard1.Dock = DockStyle.Fill;
                }
            }
        }

        private bool scrollVisible = true;
        private bool preventUpdate = false;
        public ImageView()
        {
            InitializeComponent();
            drawingBoard1.ScrollChanged += DrawingBoard_SetScrollPosition;
            hScrollBar1.ValueChanged += ScrollbarValue_Changed;
            vScrollBar1.ValueChanged += ScrollbarValue_Changed;
        }

        #region public properties

        public void FitToScreen()
        {
            drawingBoard1.FitToScreen();
        }

        public void ZoomIn()
        {
            drawingBoard1.ZoomIn();
        }

        public void ZoomOut()
        {
            drawingBoard1.ZoomOut();
        }



        #endregion

        private void DrawingBoard_SetScrollPosition(object sender, EventArgs e)
        {
            preventUpdate = true;
            int factoredWidth = (int)Math.Round(drawingBoard1.Width / drawingBoard1.ZoomFactor);
            int factoredHeight = (int)Math.Round(drawingBoard1.Height / drawingBoard1.ZoomFactor);

            hScrollBar1.Maximum = this.drawingBoard1.Image.Width;
            vScrollBar1.Maximum = this.drawingBoard1.Image.Height;

            if (factoredWidth >= drawingBoard1.Image.Width)
            {
                hScrollBar1.Enabled = false;
                hScrollBar1.Value = 0;
            }
            else if (drawingBoard1.Origin.X > 0 && drawingBoard1.Origin.X < hScrollBar1.Maximum)
            {
                hScrollBar1.LargeChange = factoredWidth;
                hScrollBar1.Enabled = true;
                hScrollBar1.Value = drawingBoard1.Origin.X;
            }

            if (factoredHeight >= drawingBoard1.Image.Height)
            {
                vScrollBar1.Enabled = false;
                vScrollBar1.Value = 0;
            }
            else if (drawingBoard1.Origin.Y > 0 && drawingBoard1.Origin.Y < vScrollBar1.Maximum)
            {
                vScrollBar1.Enabled = true;
                vScrollBar1.LargeChange = factoredHeight;
                vScrollBar1.Value = drawingBoard1.Origin.Y;
            }
            preventUpdate = false;
        }

        private void ScrollbarValue_Changed(object sender, EventArgs e)
        {
            if (preventUpdate)
                return;
            this.drawingBoard1.Origin = new Point(hScrollBar1.Value, vScrollBar1.Value);
        }
    }
}
