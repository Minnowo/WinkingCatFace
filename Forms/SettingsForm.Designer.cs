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
            this.SideButton5 = new System.Windows.Forms.Button();
            this.SideButton4 = new System.Windows.Forms.Button();
            this.SideButton3 = new System.Windows.Forms.Button();
            this.SideButton2 = new System.Windows.Forms.Button();
            this.SideButton1 = new System.Windows.Forms.Button();
            this.FormDockPanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SideButton5);
            this.panel1.Controls.Add(this.SideButton4);
            this.panel1.Controls.Add(this.SideButton3);
            this.panel1.Controls.Add(this.SideButton2);
            this.panel1.Controls.Add(this.SideButton1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 396);
            this.panel1.TabIndex = 0;
            // 
            // SideButton5
            // 
            this.SideButton5.Dock = System.Windows.Forms.DockStyle.Top;
            this.SideButton5.Location = new System.Drawing.Point(0, 160);
            this.SideButton5.Name = "SideButton5";
            this.SideButton5.Size = new System.Drawing.Size(200, 40);
            this.SideButton5.TabIndex = 4;
            this.SideButton5.Text = "Hotkeys";
            this.SideButton5.UseVisualStyleBackColor = true;
            // 
            // SideButton4
            // 
            this.SideButton4.Dock = System.Windows.Forms.DockStyle.Top;
            this.SideButton4.Location = new System.Drawing.Point(0, 120);
            this.SideButton4.Name = "SideButton4";
            this.SideButton4.Size = new System.Drawing.Size(200, 40);
            this.SideButton4.TabIndex = 3;
            this.SideButton4.Text = "Clipboard";
            this.SideButton4.UseVisualStyleBackColor = true;
            // 
            // SideButton3
            // 
            this.SideButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.SideButton3.Location = new System.Drawing.Point(0, 80);
            this.SideButton3.Name = "SideButton3";
            this.SideButton3.Size = new System.Drawing.Size(200, 40);
            this.SideButton3.TabIndex = 2;
            this.SideButton3.Text = "Upload";
            this.SideButton3.UseVisualStyleBackColor = true;
            // 
            // SideButton2
            // 
            this.SideButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.SideButton2.Location = new System.Drawing.Point(0, 40);
            this.SideButton2.Name = "SideButton2";
            this.SideButton2.Size = new System.Drawing.Size(200, 40);
            this.SideButton2.TabIndex = 1;
            this.SideButton2.Text = "Paths";
            this.SideButton2.UseVisualStyleBackColor = true;
            // 
            // SideButton1
            // 
            this.SideButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.SideButton1.Location = new System.Drawing.Point(0, 0);
            this.SideButton1.Name = "SideButton1";
            this.SideButton1.Size = new System.Drawing.Size(200, 40);
            this.SideButton1.TabIndex = 0;
            this.SideButton1.Text = "General";
            this.SideButton1.UseVisualStyleBackColor = true;
            // 
            // FormDockPanel
            // 
            this.FormDockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormDockPanel.Location = new System.Drawing.Point(200, 0);
            this.FormDockPanel.Name = "FormDockPanel";
            this.FormDockPanel.Size = new System.Drawing.Size(524, 396);
            this.FormDockPanel.TabIndex = 1;
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
        private System.Windows.Forms.Button SideButton5;
        private System.Windows.Forms.Button SideButton4;
        private System.Windows.Forms.Button SideButton3;
        private System.Windows.Forms.Button SideButton2;
        private System.Windows.Forms.Button SideButton1;
        private System.Windows.Forms.Panel FormDockPanel;
    }
}