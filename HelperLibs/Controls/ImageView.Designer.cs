namespace WinkingCat.HelperLibs
{
    partial class ImageView
    {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.drawingBoard1 = new WinkingCat.HelperLibs.DrawingBoard();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // drawingBoard1
            // 
            this.drawingBoard1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawingBoard1.Image = null;
            this.drawingBoard1.Location = new System.Drawing.Point(0, 0);
            this.drawingBoard1.Name = "drawingBoard1";
            this.drawingBoard1.Origin = new System.Drawing.Point(0, 0);
            this.drawingBoard1.Size = new System.Drawing.Size(200, 137);
            this.drawingBoard1.TabIndex = 0;
            this.drawingBoard1.ZoomFactor = 1D;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar1.Enabled = false;
            this.hScrollBar1.LargeChange = 20;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 137);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(200, 17);
            this.hScrollBar1.TabIndex = 1;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar1.Enabled = false;
            this.vScrollBar1.LargeChange = 20;
            this.vScrollBar1.Location = new System.Drawing.Point(199, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 137);
            this.vScrollBar1.TabIndex = 2;
            // 
            // ImageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.drawingBoard1);
            this.Name = "ImageView";
            this.Size = new System.Drawing.Size(217, 155);
            this.ResumeLayout(false);

        }

        #endregion

        private DrawingBoard drawingBoard1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
    }
}
