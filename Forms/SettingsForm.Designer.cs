namespace WinkingCat
{
    partial class SettingsForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.bHotkeys = new System.Windows.Forms.Button();
            this.bClipboard = new System.Windows.Forms.Button();
            this.bUpload = new System.Windows.Forms.Button();
            this.bRegionCapture = new System.Windows.Forms.Button();
            this.bGeneral = new System.Windows.Forms.Button();
            this.FormDockPanel = new System.Windows.Forms.Panel();
            this.bPaths = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.bPaths);
            this.panel1.Controls.Add(this.bHotkeys);
            this.panel1.Controls.Add(this.bClipboard);
            this.panel1.Controls.Add(this.bUpload);
            this.panel1.Controls.Add(this.bRegionCapture);
            this.panel1.Controls.Add(this.bGeneral);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 396);
            this.panel1.TabIndex = 0;
            // 
            // bHotkeys
            // 
            this.bHotkeys.Dock = System.Windows.Forms.DockStyle.Top;
            this.bHotkeys.Location = new System.Drawing.Point(0, 160);
            this.bHotkeys.Name = "bHotkeys";
            this.bHotkeys.Size = new System.Drawing.Size(200, 40);
            this.bHotkeys.TabIndex = 4;
            this.bHotkeys.Text = "Hotkeys";
            this.bHotkeys.UseVisualStyleBackColor = true;
            // 
            // bClipboard
            // 
            this.bClipboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.bClipboard.Location = new System.Drawing.Point(0, 120);
            this.bClipboard.Name = "bClipboard";
            this.bClipboard.Size = new System.Drawing.Size(200, 40);
            this.bClipboard.TabIndex = 3;
            this.bClipboard.Text = "Clipboard";
            this.bClipboard.UseVisualStyleBackColor = true;
            // 
            // bUpload
            // 
            this.bUpload.Dock = System.Windows.Forms.DockStyle.Top;
            this.bUpload.Location = new System.Drawing.Point(0, 80);
            this.bUpload.Name = "bUpload";
            this.bUpload.Size = new System.Drawing.Size(200, 40);
            this.bUpload.TabIndex = 2;
            this.bUpload.Text = "Upload";
            this.bUpload.UseVisualStyleBackColor = true;
            // 
            // bRegionCapture
            // 
            this.bRegionCapture.Dock = System.Windows.Forms.DockStyle.Top;
            this.bRegionCapture.Location = new System.Drawing.Point(0, 40);
            this.bRegionCapture.Name = "bRegionCapture";
            this.bRegionCapture.Size = new System.Drawing.Size(200, 40);
            this.bRegionCapture.TabIndex = 1;
            this.bRegionCapture.Text = "RegionCapture";
            this.bRegionCapture.UseVisualStyleBackColor = true;
            // 
            // bGeneral
            // 
            this.bGeneral.Dock = System.Windows.Forms.DockStyle.Top;
            this.bGeneral.Location = new System.Drawing.Point(0, 0);
            this.bGeneral.Name = "bGeneral";
            this.bGeneral.Size = new System.Drawing.Size(200, 40);
            this.bGeneral.TabIndex = 0;
            this.bGeneral.Text = "General";
            this.bGeneral.UseVisualStyleBackColor = true;
            // 
            // FormDockPanel
            // 
            this.FormDockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormDockPanel.Location = new System.Drawing.Point(200, 0);
            this.FormDockPanel.Name = "FormDockPanel";
            this.FormDockPanel.Size = new System.Drawing.Size(524, 396);
            this.FormDockPanel.TabIndex = 1;
            // 
            // bPaths
            // 
            this.bPaths.Dock = System.Windows.Forms.DockStyle.Top;
            this.bPaths.Location = new System.Drawing.Point(0, 200);
            this.bPaths.Name = "bPaths";
            this.bPaths.Size = new System.Drawing.Size(200, 40);
            this.bPaths.TabIndex = 5;
            this.bPaths.Text = "Paths";
            this.bPaths.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 396);
            this.Controls.Add(this.FormDockPanel);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(690, 235);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bHotkeys;
        private System.Windows.Forms.Button bClipboard;
        private System.Windows.Forms.Button bUpload;
        private System.Windows.Forms.Button bRegionCapture;
        private System.Windows.Forms.Button bGeneral;
        private System.Windows.Forms.Panel FormDockPanel;
        private System.Windows.Forms.Button bPaths;
    }
}