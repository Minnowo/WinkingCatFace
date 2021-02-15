namespace WinkingCat
{
    partial class GeneralSettingsForm
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
            this.ShowTrayIconCheckBox = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.AlwaysOnTopCheckbox = new System.Windows.Forms.CheckBox();
            this.MinimizeToTrayOnCloseCheckBox = new System.Windows.Forms.CheckBox();
            this.MinimizeToTrayOnStartCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ShowTrayIconCheckBox
            // 
            this.ShowTrayIconCheckBox.AutoSize = true;
            this.ShowTrayIconCheckBox.Location = new System.Drawing.Point(13, 18);
            this.ShowTrayIconCheckBox.Name = "ShowTrayIconCheckBox";
            this.ShowTrayIconCheckBox.Size = new System.Drawing.Size(101, 17);
            this.ShowTrayIconCheckBox.TabIndex = 0;
            this.ShowTrayIconCheckBox.Text = "Show Tray Icon";
            this.ShowTrayIconCheckBox.UseVisualStyleBackColor = true;
            this.ShowTrayIconCheckBox.CheckedChanged += new System.EventHandler(this.ShowTrayIconCheckBox_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(209, 95);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(261, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(209, 122);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(261, 21);
            this.comboBox2.TabIndex = 2;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(209, 149);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(261, 21);
            this.comboBox3.TabIndex = 3;
            // 
            // AlwaysOnTopCheckbox
            // 
            this.AlwaysOnTopCheckbox.AutoSize = true;
            this.AlwaysOnTopCheckbox.Location = new System.Drawing.Point(209, 18);
            this.AlwaysOnTopCheckbox.Name = "AlwaysOnTopCheckbox";
            this.AlwaysOnTopCheckbox.Size = new System.Drawing.Size(98, 17);
            this.AlwaysOnTopCheckbox.TabIndex = 4;
            this.AlwaysOnTopCheckbox.Text = "Always On Top";
            this.AlwaysOnTopCheckbox.UseVisualStyleBackColor = true;
            this.AlwaysOnTopCheckbox.CheckedChanged += new System.EventHandler(this.AlwaysOnTopCheckbox_CheckedChanged);
            // 
            // MinimizeToTrayOnCloseCheckBox
            // 
            this.MinimizeToTrayOnCloseCheckBox.AutoSize = true;
            this.MinimizeToTrayOnCloseCheckBox.Location = new System.Drawing.Point(13, 41);
            this.MinimizeToTrayOnCloseCheckBox.Name = "MinimizeToTrayOnCloseCheckBox";
            this.MinimizeToTrayOnCloseCheckBox.Size = new System.Drawing.Size(152, 17);
            this.MinimizeToTrayOnCloseCheckBox.TabIndex = 5;
            this.MinimizeToTrayOnCloseCheckBox.Text = "Minimize To Tray On Close";
            this.MinimizeToTrayOnCloseCheckBox.UseVisualStyleBackColor = true;
            this.MinimizeToTrayOnCloseCheckBox.CheckedChanged += new System.EventHandler(this.MinimizeToTrayOnCloseCheckBox_CheckedChanged);
            // 
            // MinimizeToTrayOnStartCheckBox
            // 
            this.MinimizeToTrayOnStartCheckBox.AutoSize = true;
            this.MinimizeToTrayOnStartCheckBox.Location = new System.Drawing.Point(13, 64);
            this.MinimizeToTrayOnStartCheckBox.Name = "MinimizeToTrayOnStartCheckBox";
            this.MinimizeToTrayOnStartCheckBox.Size = new System.Drawing.Size(148, 17);
            this.MinimizeToTrayOnStartCheckBox.TabIndex = 6;
            this.MinimizeToTrayOnStartCheckBox.Text = "Minimize To Tray On Start";
            this.MinimizeToTrayOnStartCheckBox.UseVisualStyleBackColor = true;
            this.MinimizeToTrayOnStartCheckBox.CheckedChanged += new System.EventHandler(this.MinimizeToTrayOnStartCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "On Tray Icon Left Click";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "On Tray Icon Double Left CLick";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "On Tray Icon Middle Click";
            // 
            // GeneralSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 396);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MinimizeToTrayOnStartCheckBox);
            this.Controls.Add(this.MinimizeToTrayOnCloseCheckBox);
            this.Controls.Add(this.AlwaysOnTopCheckbox);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.ShowTrayIconCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GeneralSettingsForm";
            this.Padding = new System.Windows.Forms.Padding(10, 15, 10, 10);
            this.Text = "GeneralSettingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ShowTrayIconCheckBox;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.CheckBox AlwaysOnTopCheckbox;
        private System.Windows.Forms.CheckBox MinimizeToTrayOnCloseCheckBox;
        private System.Windows.Forms.CheckBox MinimizeToTrayOnStartCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}