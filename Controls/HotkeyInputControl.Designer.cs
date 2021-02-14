﻿namespace WinkingCat
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
            this.buttonHotkey = new System.Windows.Forms.Button();
            this.labelHotkeySuccess = new System.Windows.Forms.Label();
            this.HotkeyTask = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonHotkey
            // 
            this.buttonHotkey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHotkey.Location = new System.Drawing.Point(227, 0);
            this.buttonHotkey.Name = "buttonHotkey";
            this.buttonHotkey.Size = new System.Drawing.Size(190, 23);
            this.buttonHotkey.TabIndex = 1;
            this.buttonHotkey.Text = "Hotkey";
            this.buttonHotkey.UseVisualStyleBackColor = true;
            this.buttonHotkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonTask_KeyDown);
            this.buttonHotkey.KeyUp += new System.Windows.Forms.KeyEventHandler(this.buttonTask_KeyUp);
            this.buttonHotkey.Leave += new System.EventHandler(this.buttonHotkey_Leave);
            this.buttonHotkey.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonHotkey_Click);
            // 
            // labelHotkeySuccess
            // 
            this.labelHotkeySuccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHotkeySuccess.BackColor = System.Drawing.Color.Red;
            this.labelHotkeySuccess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelHotkeySuccess.Location = new System.Drawing.Point(420, 1);
            this.labelHotkeySuccess.Name = "labelHotkeySuccess";
            this.labelHotkeySuccess.Size = new System.Drawing.Size(24, 21);
            this.labelHotkeySuccess.TabIndex = 2;
            // 
            // HotkeyTask
            // 
            this.HotkeyTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HotkeyTask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HotkeyTask.FormattingEnabled = true;
            this.HotkeyTask.Location = new System.Drawing.Point(0, 1);
            this.HotkeyTask.Name = "HotkeyTask";
            this.HotkeyTask.Size = new System.Drawing.Size(227, 21);
            this.HotkeyTask.TabIndex = 3;
            this.HotkeyTask.SelectedIndexChanged += HotkeyTask_MouseWheel;
            // 
            // HotkeyInputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.HotkeyTask);
            this.Controls.Add(this.labelHotkeySuccess);
            this.Controls.Add(this.buttonHotkey);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "HotkeyInputControl";
            this.Size = new System.Drawing.Size(446, 23);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonHotkey;
        private System.Windows.Forms.Label labelHotkeySuccess;
        private System.Windows.Forms.ComboBox HotkeyTask;
    }
}
