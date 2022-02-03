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

        public ImageViewerForm(Image img)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.image = img;
            initialSize = img.Size;
            InitializeComponent();
        }

        public static void ShowDisposeImage(Image img)
        {
            if (img == null)
                return;
            using (Image tempImage = img)
            {
                if (tempImage == null)
                    return;

                using (ImageViewerForm viewer = new ImageViewerForm(tempImage))
                {
                    viewer.ShowDialog();
                }
            }
        }

        public static void ShowImage(Image img)
        {
            if (img == null)
                return;

            using (Image tempImage = img.CloneSafe())
            {
                if (tempImage == null)
                    return;

                using (ImageViewerForm viewer = new ImageViewerForm(tempImage))
                {
                    viewer.ShowDialog();
                }
            }
        }

        public static void ShowImage(string path)
        {
            using (Image tempImage = ImageHelper.LoadImage(path))
            {
                if (tempImage == null)
                    return;

                using (ImageViewerForm viewer = new ImageViewerForm(tempImage))
                {
                    viewer.ShowDialog();
                }
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

        private void RightClicked()
        {
            Close();
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
            ivMain.ScrollbarsVisible = false;
            ivMain.Dock = DockStyle.Fill;
            ivMain.Image = (Bitmap)this.image;
            
            this.Controls.Add(ivMain);

            this.KeyDown += ImageViewerForm_KeyDown;
            ivMain.db.RightClicked+= RightClicked;
            this.BringToFront();
            this.Activate();
            this.ResumeLayout();

        }

        

        private ImageView ivMain;
        //private PictureBox pbMain;
        #endregion
    }
}
