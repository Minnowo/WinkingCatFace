namespace WinkingCat
{
    partial class ClippingWindowForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.clipWinPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.clipWinPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // clipWinPictureBox
            // 
            this.clipWinPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clipWinPictureBox.Location = new System.Drawing.Point(0, 0);
            this.clipWinPictureBox.Name = "clipWinPictureBox";
            this.clipWinPictureBox.Size = new System.Drawing.Size(605, 381);
            this.clipWinPictureBox.TabIndex = 0;
            this.clipWinPictureBox.TabStop = false;
            // 
            // ClippingWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 381);
            this.Controls.Add(this.clipWinPictureBox);
            this.Name = "ClippingWindowForm";
            this.Text = "Region Capture";
            ((System.ComponentModel.ISupportInitialize)(this.clipWinPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox clipWinPictureBox;
    }
}