namespace WinkingCat
{
    partial class HashCheckForm
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
            this.cbHashType = new System.Windows.Forms.ComboBox();
            this.tbHashTarget = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFilePathInput = new System.Windows.Forms.TextBox();
            this.tbFileHash = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbHashInput = new System.Windows.Forms.TextBox();
            this.btnCheckHash = new System.Windows.Forms.Button();
            this.btnCopyFileHash = new System.Windows.Forms.Button();
            this.btnCopyInput = new System.Windows.Forms.Button();
            this.btnCopyTarget = new System.Windows.Forms.Button();
            this.btnPasteFileHash = new System.Windows.Forms.Button();
            this.btnPasteInput = new System.Windows.Forms.Button();
            this.btnPasteTarget = new System.Windows.Forms.Button();
            this.pbProgressDone = new System.Windows.Forms.ProgressBar();
            this.lProgress = new System.Windows.Forms.Label();
            this.btnClearHashInput = new System.Windows.Forms.Button();
            this.btnClearHashTarget = new System.Windows.Forms.Button();
            this.btnClearFileHash = new System.Windows.Forms.Button();
            this.btnBrowse2 = new System.Windows.Forms.Button();
            this.tbFilePathInput2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnClearFileHash2 = new System.Windows.Forms.Button();
            this.btnPasteFileHash2 = new System.Windows.Forms.Button();
            this.btnCopyFileHash2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tbFileHash2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbHashType
            // 
            this.cbHashType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHashType.FormattingEnabled = true;
            this.cbHashType.Location = new System.Drawing.Point(12, 25);
            this.cbHashType.Name = "cbHashType";
            this.cbHashType.Size = new System.Drawing.Size(143, 21);
            this.cbHashType.TabIndex = 0;
            // 
            // tbHashTarget
            // 
            this.tbHashTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHashTarget.Location = new System.Drawing.Point(12, 520);
            this.tbHashTarget.Multiline = true;
            this.tbHashTarget.Name = "tbHashTarget";
            this.tbHashTarget.Size = new System.Drawing.Size(463, 38);
            this.tbHashTarget.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Hash Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 504);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Target:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "File Path:";
            // 
            // tbFilePathInput
            // 
            this.tbFilePathInput.AllowDrop = true;
            this.tbFilePathInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilePathInput.Location = new System.Drawing.Point(12, 85);
            this.tbFilePathInput.Name = "tbFilePathInput";
            this.tbFilePathInput.Size = new System.Drawing.Size(463, 20);
            this.tbFilePathInput.TabIndex = 5;
            // 
            // tbFileHash
            // 
            this.tbFileHash.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileHash.Location = new System.Drawing.Point(12, 271);
            this.tbFileHash.Multiline = true;
            this.tbFileHash.Name = "tbFileHash";
            this.tbFileHash.Size = new System.Drawing.Size(463, 38);
            this.tbFileHash.TabIndex = 7;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(319, 59);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(156, 23);
            this.btnBrowse.TabIndex = 8;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 255);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "File Hash:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 421);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Input:";
            // 
            // tbHashInput
            // 
            this.tbHashInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHashInput.Location = new System.Drawing.Point(12, 437);
            this.tbHashInput.Multiline = true;
            this.tbHashInput.Name = "tbHashInput";
            this.tbHashInput.Size = new System.Drawing.Size(463, 38);
            this.tbHashInput.TabIndex = 10;
            // 
            // btnCheckHash
            // 
            this.btnCheckHash.Location = new System.Drawing.Point(12, 191);
            this.btnCheckHash.Name = "btnCheckHash";
            this.btnCheckHash.Size = new System.Drawing.Size(143, 23);
            this.btnCheckHash.TabIndex = 12;
            this.btnCheckHash.Text = "Check";
            this.btnCheckHash.UseVisualStyleBackColor = true;
            this.btnCheckHash.Click += new System.EventHandler(this.btnCheckHash_Click);
            // 
            // btnCopyFileHash
            // 
            this.btnCopyFileHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyFileHash.Location = new System.Drawing.Point(400, 245);
            this.btnCopyFileHash.Name = "btnCopyFileHash";
            this.btnCopyFileHash.Size = new System.Drawing.Size(75, 23);
            this.btnCopyFileHash.TabIndex = 13;
            this.btnCopyFileHash.Text = "Copy";
            this.btnCopyFileHash.UseVisualStyleBackColor = true;
            this.btnCopyFileHash.Click += new System.EventHandler(this.btnCopyFileHash_Click);
            // 
            // btnCopyInput
            // 
            this.btnCopyInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyInput.Location = new System.Drawing.Point(400, 408);
            this.btnCopyInput.Name = "btnCopyInput";
            this.btnCopyInput.Size = new System.Drawing.Size(75, 23);
            this.btnCopyInput.TabIndex = 14;
            this.btnCopyInput.Text = "Copy";
            this.btnCopyInput.UseVisualStyleBackColor = true;
            this.btnCopyInput.Click += new System.EventHandler(this.btnCopyInput_Click);
            // 
            // btnCopyTarget
            // 
            this.btnCopyTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyTarget.Location = new System.Drawing.Point(400, 491);
            this.btnCopyTarget.Name = "btnCopyTarget";
            this.btnCopyTarget.Size = new System.Drawing.Size(75, 23);
            this.btnCopyTarget.TabIndex = 15;
            this.btnCopyTarget.Text = "Copy";
            this.btnCopyTarget.UseVisualStyleBackColor = true;
            this.btnCopyTarget.Click += new System.EventHandler(this.btnCopyTarget_Click);
            // 
            // btnPasteFileHash
            // 
            this.btnPasteFileHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPasteFileHash.Location = new System.Drawing.Point(319, 245);
            this.btnPasteFileHash.Name = "btnPasteFileHash";
            this.btnPasteFileHash.Size = new System.Drawing.Size(75, 23);
            this.btnPasteFileHash.TabIndex = 16;
            this.btnPasteFileHash.Text = "Paste";
            this.btnPasteFileHash.UseVisualStyleBackColor = true;
            this.btnPasteFileHash.Click += new System.EventHandler(this.btnPasteFileHash_Click);
            // 
            // btnPasteInput
            // 
            this.btnPasteInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPasteInput.Location = new System.Drawing.Point(319, 408);
            this.btnPasteInput.Name = "btnPasteInput";
            this.btnPasteInput.Size = new System.Drawing.Size(75, 23);
            this.btnPasteInput.TabIndex = 17;
            this.btnPasteInput.Text = "Paste";
            this.btnPasteInput.UseVisualStyleBackColor = true;
            this.btnPasteInput.Click += new System.EventHandler(this.btnPasteInput_Click);
            // 
            // btnPasteTarget
            // 
            this.btnPasteTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPasteTarget.Location = new System.Drawing.Point(319, 491);
            this.btnPasteTarget.Name = "btnPasteTarget";
            this.btnPasteTarget.Size = new System.Drawing.Size(75, 23);
            this.btnPasteTarget.TabIndex = 18;
            this.btnPasteTarget.Text = "Paste";
            this.btnPasteTarget.UseVisualStyleBackColor = true;
            this.btnPasteTarget.Click += new System.EventHandler(this.btnPasteTarget_Click);
            // 
            // pbProgressDone
            // 
            this.pbProgressDone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgressDone.Location = new System.Drawing.Point(161, 191);
            this.pbProgressDone.Name = "pbProgressDone";
            this.pbProgressDone.Size = new System.Drawing.Size(314, 23);
            this.pbProgressDone.TabIndex = 19;
            // 
            // lProgress
            // 
            this.lProgress.AutoSize = true;
            this.lProgress.Location = new System.Drawing.Point(158, 217);
            this.lProgress.Name = "lProgress";
            this.lProgress.Size = new System.Drawing.Size(21, 13);
            this.lProgress.TabIndex = 20;
            this.lProgress.Text = "0%";
            // 
            // btnClearHashInput
            // 
            this.btnClearHashInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearHashInput.Location = new System.Drawing.Point(238, 408);
            this.btnClearHashInput.Name = "btnClearHashInput";
            this.btnClearHashInput.Size = new System.Drawing.Size(75, 23);
            this.btnClearHashInput.TabIndex = 21;
            this.btnClearHashInput.Text = "Clear";
            this.btnClearHashInput.UseVisualStyleBackColor = true;
            this.btnClearHashInput.Click += new System.EventHandler(this.btnClearHashInput_Click);
            // 
            // btnClearHashTarget
            // 
            this.btnClearHashTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearHashTarget.Location = new System.Drawing.Point(238, 491);
            this.btnClearHashTarget.Name = "btnClearHashTarget";
            this.btnClearHashTarget.Size = new System.Drawing.Size(75, 23);
            this.btnClearHashTarget.TabIndex = 22;
            this.btnClearHashTarget.Text = "Clear";
            this.btnClearHashTarget.UseVisualStyleBackColor = true;
            this.btnClearHashTarget.Click += new System.EventHandler(this.btnClearHashTarget_Click);
            // 
            // btnClearFileHash
            // 
            this.btnClearFileHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearFileHash.Location = new System.Drawing.Point(238, 245);
            this.btnClearFileHash.Name = "btnClearFileHash";
            this.btnClearFileHash.Size = new System.Drawing.Size(75, 23);
            this.btnClearFileHash.TabIndex = 23;
            this.btnClearFileHash.Text = "Clear";
            this.btnClearFileHash.UseVisualStyleBackColor = true;
            this.btnClearFileHash.Click += new System.EventHandler(this.btnClearFileHash_Click);
            // 
            // btnBrowse2
            // 
            this.btnBrowse2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse2.Location = new System.Drawing.Point(319, 111);
            this.btnBrowse2.Name = "btnBrowse2";
            this.btnBrowse2.Size = new System.Drawing.Size(156, 23);
            this.btnBrowse2.TabIndex = 26;
            this.btnBrowse2.Text = "Browse...";
            this.btnBrowse2.UseVisualStyleBackColor = true;
            this.btnBrowse2.Click += new System.EventHandler(this.btnBrowse2_Click);
            // 
            // tbFilePathInput2
            // 
            this.tbFilePathInput2.AllowDrop = true;
            this.tbFilePathInput2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilePathInput2.Location = new System.Drawing.Point(12, 137);
            this.tbFilePathInput2.Name = "tbFilePathInput2";
            this.tbFilePathInput2.Size = new System.Drawing.Size(463, 20);
            this.tbFilePathInput2.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "File Path:";
            // 
            // btnClearFileHash2
            // 
            this.btnClearFileHash2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearFileHash2.Location = new System.Drawing.Point(238, 325);
            this.btnClearFileHash2.Name = "btnClearFileHash2";
            this.btnClearFileHash2.Size = new System.Drawing.Size(75, 23);
            this.btnClearFileHash2.TabIndex = 31;
            this.btnClearFileHash2.Text = "Clear";
            this.btnClearFileHash2.UseVisualStyleBackColor = true;
            this.btnClearFileHash2.Click += new System.EventHandler(this.btnClearFileHash2_Click);
            // 
            // btnPasteFileHash2
            // 
            this.btnPasteFileHash2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPasteFileHash2.Location = new System.Drawing.Point(319, 325);
            this.btnPasteFileHash2.Name = "btnPasteFileHash2";
            this.btnPasteFileHash2.Size = new System.Drawing.Size(75, 23);
            this.btnPasteFileHash2.TabIndex = 30;
            this.btnPasteFileHash2.Text = "Paste";
            this.btnPasteFileHash2.UseVisualStyleBackColor = true;
            this.btnPasteFileHash2.Click += new System.EventHandler(this.btnPasteFileHash2_Click);
            // 
            // btnCopyFileHash2
            // 
            this.btnCopyFileHash2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyFileHash2.Location = new System.Drawing.Point(400, 325);
            this.btnCopyFileHash2.Name = "btnCopyFileHash2";
            this.btnCopyFileHash2.Size = new System.Drawing.Size(75, 23);
            this.btnCopyFileHash2.TabIndex = 29;
            this.btnCopyFileHash2.Text = "Copy";
            this.btnCopyFileHash2.UseVisualStyleBackColor = true;
            this.btnCopyFileHash2.Click += new System.EventHandler(this.btnCopyFileHash2_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 335);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "File Hash:";
            // 
            // tbFileHash2
            // 
            this.tbFileHash2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileHash2.Location = new System.Drawing.Point(12, 354);
            this.tbFileHash2.Multiline = true;
            this.tbFileHash2.Name = "tbFileHash2";
            this.tbFileHash2.Size = new System.Drawing.Size(463, 38);
            this.tbFileHash2.TabIndex = 27;
            // 
            // HashCheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 568);
            this.Controls.Add(this.btnClearFileHash2);
            this.Controls.Add(this.btnPasteFileHash2);
            this.Controls.Add(this.btnCopyFileHash2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbFileHash2);
            this.Controls.Add(this.btnBrowse2);
            this.Controls.Add(this.tbFilePathInput2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnClearFileHash);
            this.Controls.Add(this.btnClearHashTarget);
            this.Controls.Add(this.btnClearHashInput);
            this.Controls.Add(this.lProgress);
            this.Controls.Add(this.pbProgressDone);
            this.Controls.Add(this.btnPasteTarget);
            this.Controls.Add(this.btnPasteInput);
            this.Controls.Add(this.btnPasteFileHash);
            this.Controls.Add(this.btnCopyTarget);
            this.Controls.Add(this.btnCopyInput);
            this.Controls.Add(this.btnCopyFileHash);
            this.Controls.Add(this.btnCheckHash);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbHashInput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbFileHash);
            this.Controls.Add(this.tbFilePathInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbHashTarget);
            this.Controls.Add(this.cbHashType);
            this.MinimumSize = new System.Drawing.Size(348, 525);
            this.Name = "HashCheckForm";
            this.Text = "HashCheck";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbHashType;
        private System.Windows.Forms.TextBox tbHashTarget;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFilePathInput;
        private System.Windows.Forms.TextBox tbFileHash;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbHashInput;
        private System.Windows.Forms.Button btnCheckHash;
        private System.Windows.Forms.Button btnCopyFileHash;
        private System.Windows.Forms.Button btnCopyInput;
        private System.Windows.Forms.Button btnCopyTarget;
        private System.Windows.Forms.Button btnPasteFileHash;
        private System.Windows.Forms.Button btnPasteInput;
        private System.Windows.Forms.Button btnPasteTarget;
        private System.Windows.Forms.ProgressBar pbProgressDone;
        private System.Windows.Forms.Label lProgress;
        private System.Windows.Forms.Button btnClearHashInput;
        private System.Windows.Forms.Button btnClearHashTarget;
        private System.Windows.Forms.Button btnClearFileHash;
        private System.Windows.Forms.Button btnBrowse2;
        private System.Windows.Forms.TextBox tbFilePathInput2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClearFileHash2;
        private System.Windows.Forms.Button btnPasteFileHash2;
        private System.Windows.Forms.Button btnCopyFileHash2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbFileHash2;
    }
}