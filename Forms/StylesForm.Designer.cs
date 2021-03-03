namespace WinkingCat
{
    partial class StylesForm
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
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.btnMainForm = new System.Windows.Forms.Button();
            this.btnRegionCapture = new System.Windows.Forms.Button();
            this.btnClips = new System.Windows.Forms.Button();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnClips);
            this.pnlButtons.Controls.Add(this.btnRegionCapture);
            this.pnlButtons.Controls.Add(this.btnMainForm);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlButtons.Location = new System.Drawing.Point(0, 0);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(200, 450);
            this.pnlButtons.TabIndex = 0;
            // 
            // pnlForm
            // 
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForm.Location = new System.Drawing.Point(200, 0);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(600, 450);
            this.pnlForm.TabIndex = 1;
            // 
            // btnMainForm
            // 
            this.btnMainForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMainForm.Location = new System.Drawing.Point(0, 0);
            this.btnMainForm.Name = "btnMainForm";
            this.btnMainForm.Size = new System.Drawing.Size(200, 40);
            this.btnMainForm.TabIndex = 0;
            this.btnMainForm.Text = "Main Window / Children";
            this.btnMainForm.UseVisualStyleBackColor = true;
            this.btnMainForm.Click += new System.EventHandler(this.btnMainForm_Click);
            // 
            // btnRegionCapture
            // 
            this.btnRegionCapture.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRegionCapture.Location = new System.Drawing.Point(0, 40);
            this.btnRegionCapture.Name = "btnRegionCapture";
            this.btnRegionCapture.Size = new System.Drawing.Size(200, 40);
            this.btnRegionCapture.TabIndex = 1;
            this.btnRegionCapture.Text = "Region Capture";
            this.btnRegionCapture.UseVisualStyleBackColor = true;
            this.btnRegionCapture.Click += new System.EventHandler(this.btnRegionCapture_Click);
            // 
            // btnClips
            // 
            this.btnClips.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClips.Location = new System.Drawing.Point(0, 80);
            this.btnClips.Name = "btnClips";
            this.btnClips.Size = new System.Drawing.Size(200, 40);
            this.btnClips.TabIndex = 2;
            this.btnClips.Text = "Clips";
            this.btnClips.UseVisualStyleBackColor = true;
            this.btnClips.Click += new System.EventHandler(this.btnClips_Click);
            // 
            // StylesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.pnlButtons);
            this.Name = "StylesForm";
            this.Text = "StylesForm";
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Button btnMainForm;
        private System.Windows.Forms.Button btnClips;
        private System.Windows.Forms.Button btnRegionCapture;
    }
}