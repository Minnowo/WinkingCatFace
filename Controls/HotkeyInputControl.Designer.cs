namespace WinkingCat
{
    partial class HotkeyInputControl
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
            this.labelDescription = new System.Windows.Forms.Label();
            this.buttonHotkey = new System.Windows.Forms.Button();
            this.labelHotkeySuccess = new System.Windows.Forms.Label();
            this.buttonTask = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelDescription
            // 
            this.labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDescription.Location = new System.Drawing.Point(29, 1);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(229, 21);
            this.labelDescription.TabIndex = 0;
            this.labelDescription.Text = "Description";
            this.labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelDescription.UseMnemonic = false;
            // 
            // buttonHotkey
            // 
            this.buttonHotkey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHotkey.Location = new System.Drawing.Point(263, 0);
            this.buttonHotkey.Name = "buttonHotkey";
            this.buttonHotkey.Size = new System.Drawing.Size(190, 23);
            this.buttonHotkey.TabIndex = 1;
            this.buttonHotkey.Text = "Hotkey";
            this.buttonHotkey.UseVisualStyleBackColor = true;
            this.buttonHotkey.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonHotkey_Click);
            this.buttonHotkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonTask_KeyDown);
            this.buttonHotkey.KeyUp += new System.Windows.Forms.KeyEventHandler(this.buttonTask_KeyUp);
            this.buttonHotkey.Leave += new System.EventHandler(this.buttonHotkey_Leave);
            // 
            // labelHotkeySuccess
            // 
            this.labelHotkeySuccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHotkeySuccess.BackColor = System.Drawing.Color.Red;
            this.labelHotkeySuccess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelHotkeySuccess.Location = new System.Drawing.Point(456, 1);
            this.labelHotkeySuccess.Name = "labelHotkeySuccess";
            this.labelHotkeySuccess.Size = new System.Drawing.Size(24, 21);
            this.labelHotkeySuccess.TabIndex = 2;
            // 
            // buttonTask
            // 
            this.buttonTask.Location = new System.Drawing.Point(0, 0);
            this.buttonTask.Name = "buttonTask";
            this.buttonTask.Size = new System.Drawing.Size(24, 23);
            this.buttonTask.TabIndex = 3;
            this.buttonTask.UseVisualStyleBackColor = true;
            this.buttonTask.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonTask_Click);
            // 
            // HotkeyInputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonTask);
            this.Controls.Add(this.labelHotkeySuccess);
            this.Controls.Add(this.buttonHotkey);
            this.Controls.Add(this.labelDescription);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "HotkeyInputControl";
            this.Size = new System.Drawing.Size(482, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Button buttonHotkey;
        private System.Windows.Forms.Label labelHotkeySuccess;
        private System.Windows.Forms.Button buttonTask;
    }
}
