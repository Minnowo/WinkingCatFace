﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WinkingCat.HelperLibs;

namespace WinkingCat.Controls
{
    public partial class _PictureBox : UserControl
    {

        public Image Image
        {
            get
            {
                return this.pbMain.Image;
            }
            private set
            {
                this.pbMain.Image = value;
            }
        }

        public bool IsImageValid
        {
            get
            {
                return this.Image != null && this.Image != this.pbMain.ErrorImage && this.Image != this.pbMain.InitialImage;
            }
        }

        public bool previewOnClick = false;

        private readonly object imageLock = new object();
        private bool isImageLoading = false;
        public _PictureBox()
        {
            InitializeComponent();
        }

        public void SetImage(Image img)
        {
            lock (imageLock)
            {
                if (!isImageLoading)
                {
                    this.Reset();
                    isImageLoading = true;
                    this.Image = (Image)img.Clone();
                    this.isImageLoading = false;
                    ImageSizeMode();
                }
            }
        }

        public void SetImage(string path)
        {
            lock (imageLock)
            {
                if (!isImageLoading)
                    if (File.Exists(path))
                    {
                        this.Reset();
                        isImageLoading = true;

                        this.Image = ImageHelper.LoadImage(path);

                        this.isImageLoading = false;
                        ImageSizeMode();
                    }
            }
        }

        public void ImageSizeMode()
        {
            if (IsImageValid)
            {
                if (this.Image.Width > this.ClientSize.Width || this.Image.Height > this.ClientSize.Height)
                {
                    this.pbMain.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    this.pbMain.SizeMode = PictureBoxSizeMode.CenterImage;
                }
            }
        }

        public void Reset()
        {
            if (!isImageLoading && IsImageValid)
            {
                lock (imageLock)
                {
                    Image tmp = this.Image;
                    this.Image = null;
                    tmp.Dispose();
                }
                GC.Collect(); // this is to garbage collect the memory stream used by the LoadImage function
            }
        }

        private void _PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (previewOnClick)
                    {
                        this.pbMain.Enabled = false;
                        //ImageViewerForm.ShowImage(this.Image);

                        this.pbMain.Enabled = true;
                    }
                    break;
            }

        }

        #region Component Designer generated code
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
            components = new System.ComponentModel.Container();
            this.DoubleBuffered = true;
            pbMain = new PictureBox();
            pbMain.Name = "pbMain";
            pbMain.Dock = DockStyle.Fill;
            pbMain.BackColor = Color.Transparent;
            //pbMain.InitialImage = Properties.Resources.loading;
            //pbMain.ErrorImage = Properties.Resources.failed_to_load;

            pbMain.MouseClick += _PictureBox_MouseClick;

            this.Controls.Add(pbMain);
        }

        public PictureBox pbMain;
        #endregion
    }
}
