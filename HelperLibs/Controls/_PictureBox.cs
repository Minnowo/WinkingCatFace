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
                return image;
            }
            set
            {
                this.pbMain.Image = value;
            }
        }
        private Image image;
        public _PictureBox()
        {
            InitializeComponent();
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
            pbMain = new PictureBox();
            pbMain.Name = "pbMain";
            pbMain.Dock = DockStyle.Fill;
            pbMain.BackColor = Color.Transparent;

            this.Controls.Add(pbMain);
        }

        PictureBox pbMain;
        #endregion
    }
}
