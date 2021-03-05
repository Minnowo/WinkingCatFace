namespace WinkingCat.ScreenCaptureLib
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
            this.SuspendLayout();
            // 
            // ClippingWindowForm
            // 
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            MaximizeBox = false;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None; // might remove
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 381);
            this.Name = "ClippingWindowForm";
            this.Text = "Region Capture";
            MouseDown += Click_Event;
            MouseUp += ClickRelease_Event;
            MouseMove += MouseMove_Event;
            MouseWheel += MouseWheel_Event;
            KeyDown += KeyDown_Event;
            this.ResumeLayout(false);

        }

        #endregion
    }
}