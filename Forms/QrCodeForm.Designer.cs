namespace WinkingCat
{
    partial class BarcodeForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.cmFormat = new System.Windows.Forms.ComboBox();
            this.pbQRDisplay = new System.Windows.Forms.PictureBox();
            this.tbTextInput = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbTextOutput = new System.Windows.Forms.TextBox();
            this.bFromClipboard = new System.Windows.Forms.Button();
            this.bFromFile = new System.Windows.Forms.Button();
            this.bFromScreen = new System.Windows.Forms.Button();
            this.btnCopyCode = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbQRDisplay)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(416, 495);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnCopyCode);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.cmFormat);
            this.tabPage1.Controls.Add(this.pbQRDisplay);
            this.tabPage1.Controls.Add(this.tbTextInput);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(408, 469);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Encode";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Format";
            // 
            // cmFormat
            // 
            this.cmFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmFormat.FormattingEnabled = true;
            this.cmFormat.Location = new System.Drawing.Point(53, 6);
            this.cmFormat.Name = "cmFormat";
            this.cmFormat.Size = new System.Drawing.Size(257, 21);
            this.cmFormat.TabIndex = 2;
            this.cmFormat.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // pbQRDisplay
            // 
            this.pbQRDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbQRDisplay.Location = new System.Drawing.Point(3, 107);
            this.pbQRDisplay.Name = "pbQRDisplay";
            this.pbQRDisplay.Size = new System.Drawing.Size(402, 359);
            this.pbQRDisplay.TabIndex = 1;
            this.pbQRDisplay.TabStop = false;
            // 
            // tbTextInput
            // 
            this.tbTextInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTextInput.Location = new System.Drawing.Point(3, 33);
            this.tbTextInput.Multiline = true;
            this.tbTextInput.Name = "tbTextInput";
            this.tbTextInput.Size = new System.Drawing.Size(402, 68);
            this.tbTextInput.TabIndex = 0;
            this.tbTextInput.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbTextOutput);
            this.tabPage2.Controls.Add(this.bFromClipboard);
            this.tabPage2.Controls.Add(this.bFromFile);
            this.tabPage2.Controls.Add(this.bFromScreen);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(408, 469);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Decode";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbTextOutput
            // 
            this.tbTextOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTextOutput.Location = new System.Drawing.Point(6, 35);
            this.tbTextOutput.Multiline = true;
            this.tbTextOutput.Name = "tbTextOutput";
            this.tbTextOutput.Size = new System.Drawing.Size(394, 426);
            this.tbTextOutput.TabIndex = 3;
            // 
            // bFromClipboard
            // 
            this.bFromClipboard.Location = new System.Drawing.Point(270, 6);
            this.bFromClipboard.Name = "bFromClipboard";
            this.bFromClipboard.Size = new System.Drawing.Size(132, 23);
            this.bFromClipboard.TabIndex = 2;
            this.bFromClipboard.Text = "From Clipboard";
            this.bFromClipboard.UseVisualStyleBackColor = true;
            this.bFromClipboard.Click += new System.EventHandler(this.bFromClipboard_Click);
            // 
            // bFromFile
            // 
            this.bFromFile.Location = new System.Drawing.Point(144, 6);
            this.bFromFile.Name = "bFromFile";
            this.bFromFile.Size = new System.Drawing.Size(120, 23);
            this.bFromFile.TabIndex = 1;
            this.bFromFile.Text = "From File";
            this.bFromFile.UseVisualStyleBackColor = true;
            this.bFromFile.Click += new System.EventHandler(this.bFromFile_Click);
            // 
            // bFromScreen
            // 
            this.bFromScreen.Location = new System.Drawing.Point(6, 6);
            this.bFromScreen.Name = "bFromScreen";
            this.bFromScreen.Size = new System.Drawing.Size(132, 23);
            this.bFromScreen.TabIndex = 0;
            this.bFromScreen.Text = "From Screen";
            this.bFromScreen.UseVisualStyleBackColor = true;
            this.bFromScreen.Click += new System.EventHandler(this.bFromScreen_Click);
            // 
            // btnCopyCode
            // 
            this.btnCopyCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyCode.Location = new System.Drawing.Point(316, 4);
            this.btnCopyCode.Name = "btnCopyCode";
            this.btnCopyCode.Size = new System.Drawing.Size(86, 23);
            this.btnCopyCode.TabIndex = 4;
            this.btnCopyCode.Text = "Copy";
            this.btnCopyCode.UseVisualStyleBackColor = true;
            this.btnCopyCode.Click += new System.EventHandler(this.btnCopyCode_Click);
            // 
            // BarcodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 495);
            this.Controls.Add(this.tabControl1);
            this.Name = "BarcodeForm";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbQRDisplay)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pbQRDisplay;
        private System.Windows.Forms.TextBox tbTextInput;
        private System.Windows.Forms.ComboBox cmFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bFromClipboard;
        private System.Windows.Forms.Button bFromFile;
        private System.Windows.Forms.Button bFromScreen;
        private System.Windows.Forms.TextBox tbTextOutput;
        private System.Windows.Forms.Button btnCopyCode;
    }
}