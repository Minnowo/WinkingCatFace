namespace WinkingCat
{
    partial class PathSettingsForm
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
            this.tbScreenshotFolder = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.PathBrowseButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbScreenshotFolder
            // 
            this.tbScreenshotFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbScreenshotFolder.Location = new System.Drawing.Point(17, 50);
            this.tbScreenshotFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbScreenshotFolder.Name = "tbScreenshotFolder";
            this.tbScreenshotFolder.Size = new System.Drawing.Size(476, 22);
            this.tbScreenshotFolder.TabIndex = 0;
            this.tbScreenshotFolder.TextChanged += new System.EventHandler(this.tbScreenshotFolder_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(17, 22);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(237, 21);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Use Custom Screenshots Folder:";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // PathBrowseButton
            // 
            this.PathBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PathBrowseButton.Location = new System.Drawing.Point(503, 50);
            this.PathBrowseButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PathBrowseButton.Name = "PathBrowseButton";
            this.PathBrowseButton.Size = new System.Drawing.Size(121, 25);
            this.PathBrowseButton.TabIndex = 2;
            this.PathBrowseButton.Text = "Browse...";
            this.PathBrowseButton.UseVisualStyleBackColor = true;
            this.PathBrowseButton.Click += new System.EventHandler(this.PathBrowseButton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(17, 112);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(128, 24);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Default Image Type";
            // 
            // PathSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(647, 487);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.PathBrowseButton);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.tbScreenshotFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "PathSettingsForm";
            this.Padding = new System.Windows.Forms.Padding(13, 18, 13, 12);
            this.Text = "PathSettingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbScreenshotFolder;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button PathBrowseButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}