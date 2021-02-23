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
            this.SubFolderPatternLabel = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.OpenButton = new System.Windows.Forms.Button();
            this.SubFolderPathLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbScreenshotFolder
            // 
            this.tbScreenshotFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbScreenshotFolder.Location = new System.Drawing.Point(13, 41);
            this.tbScreenshotFolder.Name = "tbScreenshotFolder";
            this.tbScreenshotFolder.Size = new System.Drawing.Size(358, 20);
            this.tbScreenshotFolder.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 18);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(180, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Use Custom Screenshots Folder:";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // PathBrowseButton
            // 
            this.PathBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PathBrowseButton.Location = new System.Drawing.Point(377, 41);
            this.PathBrowseButton.Name = "PathBrowseButton";
            this.PathBrowseButton.Size = new System.Drawing.Size(91, 20);
            this.PathBrowseButton.TabIndex = 2;
            this.PathBrowseButton.Text = "Browse...";
            this.PathBrowseButton.UseVisualStyleBackColor = true;
            this.PathBrowseButton.Click += new System.EventHandler(this.PathBrowseButton_Click);
            // 
            // SubFolderPatternLabel
            // 
            this.SubFolderPatternLabel.AutoSize = true;
            this.SubFolderPatternLabel.Location = new System.Drawing.Point(13, 81);
            this.SubFolderPatternLabel.Name = "SubFolderPatternLabel";
            this.SubFolderPatternLabel.Size = new System.Drawing.Size(98, 13);
            this.SubFolderPatternLabel.TabIndex = 3;
            this.SubFolderPatternLabel.Text = "Sub Folder Pattern:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(13, 97);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(358, 20);
            this.textBox2.TabIndex = 4;
            // 
            // OpenButton
            // 
            this.OpenButton.Location = new System.Drawing.Point(13, 123);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(75, 20);
            this.OpenButton.TabIndex = 5;
            this.OpenButton.Text = "Open...";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // SubFolderPathLabel
            // 
            this.SubFolderPathLabel.AutoSize = true;
            this.SubFolderPathLabel.Location = new System.Drawing.Point(94, 127);
            this.SubFolderPathLabel.Name = "SubFolderPathLabel";
            this.SubFolderPathLabel.Size = new System.Drawing.Size(0, 13);
            this.SubFolderPathLabel.TabIndex = 6;
            // 
            // PathSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(485, 396);
            this.Controls.Add(this.SubFolderPathLabel);
            this.Controls.Add(this.OpenButton);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.SubFolderPatternLabel);
            this.Controls.Add(this.PathBrowseButton);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.tbScreenshotFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PathSettingsForm";
            this.Padding = new System.Windows.Forms.Padding(10, 15, 10, 10);
            this.Text = "PathSettingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbScreenshotFolder;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button PathBrowseButton;
        private System.Windows.Forms.Label SubFolderPatternLabel;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.Label SubFolderPathLabel;
    }
}