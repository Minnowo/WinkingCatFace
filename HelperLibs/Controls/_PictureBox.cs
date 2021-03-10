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
                return isImageValid;
            }
            private set
            {
                if(this.Image != null)
                {

                }
            }
        }
        private bool isImageValid;

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

        public void ImageSizeMode()
        {

        }

        public void Reset()
        {

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
            pbMain.InitialImage = Properties.Resources.loading;
            pbMain.ErrorImage = Properties.Resources.failed_to_load;

            this.Controls.Add(pbMain);
        }

        PictureBox pbMain;
        #endregion
    }
}
