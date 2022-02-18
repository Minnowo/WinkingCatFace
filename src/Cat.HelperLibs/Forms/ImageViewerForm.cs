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

using WinkingCat.HelperLibs.Controls;

namespace WinkingCat.HelperLibs
{
    public partial class ImageViewerForm : Form
    {

        public ImageViewerForm(IMAGE img)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();

            ivMain.Image = img;
            ivMain.DrawMode = ImageDrawMode.Resizeable;
            ivMain.CenterCurrentImage();
        }

        public static void ShowDisposeImage(IMAGE img)
        {
            if (img == null)
                return;

           /* using (Image tempImage = img)
            {
                if (tempImage == null)
                    return;*/

                using (ImageViewerForm viewer = new ImageViewerForm(img))
                {
                    viewer.ShowDialog();
                }
           /* }*/
        }

        public static void ShowImage(IMAGE img)
        {
            if (img == null)
                return;
/*
            using (Image tempImage = img.CloneSafe())
            {
                if (tempImage == null)
                    return;
*/
                using (ImageViewerForm viewer = new ImageViewerForm(img))
                {
                    viewer.ShowDialog();
                }
            //}
        }

        public static void ShowImage(string path)
        {
            using (IMAGE tempImage = ImageHelper.LoadImage(path))
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


            ivMain = new ImageDisplay();
            ivMain.Dock = DockStyle.Fill;

            ivMain.DisposeImageOnReplace = false;

            this.Controls.Add(ivMain);

            this.KeyDown += ImageViewerForm_KeyDown;
            this.BringToFront();
            this.Activate();
            this.ResumeLayout();

        }

        

        private ImageDisplay ivMain;
        //private PictureBox pbMain;
        #endregion
    }
}
