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
            this.SuspendLayout();
            // 
            // drawingBoard1
            // 
            this.drawingBoard1.BackgroundImage = null;
            this.drawingBoard1.Image = null;
            this.drawingBoard1.InitialImage = null;
            this.drawingBoard1.Location = new System.Drawing.Point(0, 0);
            this.drawingBoard1.Name = "drawingBoard1";
            this.drawingBoard1.Origin = new System.Drawing.Point(0, 0);
            this.drawingBoard1.Size = new System.Drawing.Size(150, 150);
            this.drawingBoard1.TabIndex = 0;
            this.drawingBoard1.ZoomFactor = 1D;
            // 
            // ImageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.drawingBoard1);
            this.Name = "ImageView";
            this.ResumeLayout(false);

        }

        #endregion

        private DrawingBoard drawingBoard1;
    }
}
