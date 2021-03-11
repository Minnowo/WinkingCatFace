using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WinkingCat.HelperLibs
{
    public partial class ImageViewerForm : Form
    {
        public Image image { get; private set; }
        public Size initialSize { get; private set; }
        public float zoomScale { get; set; } = 1.2f;

        private bool isLeftClicking = false;
        private Point lastLocation;
        public ImageViewerForm(Image img)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.image = img;
            initialSize = img.Size;
            InitializeComponent();
        }


        public static void ShowImage(Image img)
        {
            if (img != null)
            {
                using (Image tempImage = img.CloneSafe())
                {
                    if (tempImage != null)
                    {
                        using (ImageViewerForm viewer = new ImageViewerForm(tempImage))
                        {
                            viewer.ShowDialog();
                        }
                    }
                }
            }
        }


        private void PbMain_MouseWheel(object sender, MouseEventArgs e)
        {
            if(e.Delta > 0)
            {
                // scroll up
                pbMain.Image.Dispose();
                pbMain.Image = ImageHelper.ResizeImage((Bitmap)this.image, ResizeWidth((int)Math.Round(pbMain.Size.Width * zoomScale)));
                pbMain.Size = ResizeWidth((int)Math.Round(pbMain.Size.Width * zoomScale));
            }
            else if((int)Math.Round(pbMain.Size.Width / zoomScale) > 0)
            {
                // scroll down
                pbMain.Image.Dispose();
                pbMain.Image = ImageHelper.ResizeImage((Bitmap)this.image, ResizeWidth((int)Math.Round(pbMain.Size.Width / zoomScale)));
                pbMain.Size = ResizeWidth((int)Math.Round(pbMain.Size.Width / zoomScale));
            }
        }

        private void PbMain_MouseUp(object sender, MouseEventArgs e)
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

        private void PbMain_MouseDown(object sender, MouseEventArgs e)
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


        private void PbMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (isLeftClicking)
            {
                Point p = this.PointToClient(ScreenHelper.GetCursorPosition());
                pbMain.Location = new Point(p.X - lastLocation.X, p.Y - lastLocation.Y);
            }
        }

        private void ImageViewerForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private Size ResizeWidth(int newWidth)
        {
            int newHeight = (int)(newWidth * (initialSize.Height / (float)initialSize.Width));
            return new Size(newWidth, newHeight);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            if(this.image != null)
            {
                image.Dispose();
            }

            /*if(pbMain.Image != null)
            {
                pbMain.Image.Dispose();
            }*/

            base.Dispose(disposing);
        }


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.KeyPreview = true;
            this.Text = ";3 Image Viewer";
            this.StartPosition = FormStartPosition.Manual;
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Bounds = ScreenHelper.GetActiveScreenBounds();
            this.TopMost = true;
            this.BackColor = Color.Black;


            ivMain = new ImageView();
           
            ivMain.Dock = DockStyle.Fill;
            ivMain.Image = (Bitmap)this.image;
            this.Controls.Add(ivMain);

            this.KeyDown += ImageViewerForm_KeyDown;
            this.BringToFront();
            this.Activate();
            this.ResumeLayout();

        }

        private ImageView ivMain;
        private PictureBox pbMain;
        #endregion
    }
}
