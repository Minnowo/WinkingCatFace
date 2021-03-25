namespace WinkingCat
{
    partial class ColorPickerForm
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
            this.pnlRGBColor = new System.Windows.Forms.Panel();
            this.ccbRGBColor = new WinkingCat.HelperLibs.ColorComboBox();
            this.rbBlue = new System.Windows.Forms.RadioButton();
            this.rbGreen = new System.Windows.Forms.RadioButton();
            this.rbRed = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlHSBColor = new System.Windows.Forms.Panel();
            this.rbHSBBrightness = new System.Windows.Forms.RadioButton();
            this.rbHSBSaturation = new System.Windows.Forms.RadioButton();
            this.rbHSBHue = new System.Windows.Forms.RadioButton();
            this.ccbHSBColor = new WinkingCat.HelperLibs.ColorComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlHSLColor = new System.Windows.Forms.Panel();
            this.rbHSLLightness = new System.Windows.Forms.RadioButton();
            this.rbHSLSaturation = new System.Windows.Forms.RadioButton();
            this.rbHSLHue = new System.Windows.Forms.RadioButton();
            this.ccbHSLColor = new WinkingCat.HelperLibs.ColorComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlAdobeRGBColor = new System.Windows.Forms.Panel();
            this.rbAdobeRGBBlue = new System.Windows.Forms.RadioButton();
            this.rbAdobeRGBGreen = new System.Windows.Forms.RadioButton();
            this.rbAdobeRGBRed = new System.Windows.Forms.RadioButton();
            this.ccbAdobeRGBColor = new WinkingCat.HelperLibs.ColorComboBox();
            this.pnlXYZColor = new System.Windows.Forms.Panel();
            this.rbX = new System.Windows.Forms.RadioButton();
            this.ccbXYZColor = new WinkingCat.HelperLibs.ColorComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.displayColorLabel = new System.Windows.Forms.Label();
            this.cbCopyFormat = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ccbCMYKColor = new WinkingCat.HelperLibs.ColorComboBox();
            this.ccbYXYColor = new WinkingCat.HelperLibs.ColorComboBox();
            this.colorPicker = new WinkingCat.HelperLibs.ColorPicker();
            this.pnlRGBColor.SuspendLayout();
            this.pnlHSBColor.SuspendLayout();
            this.pnlHSLColor.SuspendLayout();
            this.pnlAdobeRGBColor.SuspendLayout();
            this.pnlXYZColor.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRGBColor
            // 
            this.pnlRGBColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRGBColor.Controls.Add(this.ccbRGBColor);
            this.pnlRGBColor.Controls.Add(this.rbBlue);
            this.pnlRGBColor.Controls.Add(this.rbGreen);
            this.pnlRGBColor.Controls.Add(this.rbRed);
            this.pnlRGBColor.Location = new System.Drawing.Point(411, 34);
            this.pnlRGBColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlRGBColor.Name = "pnlRGBColor";
            this.pnlRGBColor.Size = new System.Drawing.Size(322, 67);
            this.pnlRGBColor.TabIndex = 1;
            // 
            // ccbRGBColor
            // 
            this.ccbRGBColor.ColorFormat = WinkingCat.HelperLibs.ColorFormat.RGB;
            this.ccbRGBColor.DecimalPlaces = ((byte)(1));
            this.ccbRGBColor.Location = new System.Drawing.Point(4, 4);
            this.ccbRGBColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ccbRGBColor.MaxValues = new decimal[] {
        new decimal(new int[] {
                    255,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    255,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    255,
                    0,
                    0,
                    0})};
            this.ccbRGBColor.MinValues = new decimal[] {
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0})};
            this.ccbRGBColor.Name = "ccbRGBColor";
            this.ccbRGBColor.Size = new System.Drawing.Size(317, 25);
            this.ccbRGBColor.TabIndex = 3;
            this.ccbRGBColor.Values = new decimal[] {
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0})};
            // 
            // rbBlue
            // 
            this.rbBlue.AutoSize = true;
            this.rbBlue.Location = new System.Drawing.Point(201, 36);
            this.rbBlue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbBlue.Name = "rbBlue";
            this.rbBlue.Size = new System.Drawing.Size(57, 21);
            this.rbBlue.TabIndex = 2;
            this.rbBlue.TabStop = true;
            this.rbBlue.Text = "Blue";
            this.rbBlue.UseVisualStyleBackColor = true;
            // 
            // rbGreen
            // 
            this.rbGreen.AutoSize = true;
            this.rbGreen.Location = new System.Drawing.Point(104, 36);
            this.rbGreen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbGreen.Name = "rbGreen";
            this.rbGreen.Size = new System.Drawing.Size(69, 21);
            this.rbGreen.TabIndex = 1;
            this.rbGreen.TabStop = true;
            this.rbGreen.Text = "Green";
            this.rbGreen.UseVisualStyleBackColor = true;
            // 
            // rbRed
            // 
            this.rbRed.AutoSize = true;
            this.rbRed.Location = new System.Drawing.Point(4, 36);
            this.rbRed.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbRed.Name = "rbRed";
            this.rbRed.Size = new System.Drawing.Size(55, 21);
            this.rbRed.TabIndex = 0;
            this.rbRed.TabStop = true;
            this.rbRed.Text = "Red";
            this.rbRed.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(411, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "RGB Color";
            // 
            // pnlHSBColor
            // 
            this.pnlHSBColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHSBColor.Controls.Add(this.rbHSBBrightness);
            this.pnlHSBColor.Controls.Add(this.rbHSBSaturation);
            this.pnlHSBColor.Controls.Add(this.rbHSBHue);
            this.pnlHSBColor.Controls.Add(this.ccbHSBColor);
            this.pnlHSBColor.Location = new System.Drawing.Point(411, 150);
            this.pnlHSBColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlHSBColor.Name = "pnlHSBColor";
            this.pnlHSBColor.Size = new System.Drawing.Size(322, 67);
            this.pnlHSBColor.TabIndex = 3;
            // 
            // rbHSBBrightness
            // 
            this.rbHSBBrightness.AutoSize = true;
            this.rbHSBBrightness.Location = new System.Drawing.Point(211, 39);
            this.rbHSBBrightness.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbHSBBrightness.Name = "rbHSBBrightness";
            this.rbHSBBrightness.Size = new System.Drawing.Size(96, 21);
            this.rbHSBBrightness.TabIndex = 3;
            this.rbHSBBrightness.TabStop = true;
            this.rbHSBBrightness.Text = "Brightness";
            this.rbHSBBrightness.UseVisualStyleBackColor = true;
            // 
            // rbHSBSaturation
            // 
            this.rbHSBSaturation.AutoSize = true;
            this.rbHSBSaturation.Location = new System.Drawing.Point(105, 39);
            this.rbHSBSaturation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbHSBSaturation.Name = "rbHSBSaturation";
            this.rbHSBSaturation.Size = new System.Drawing.Size(94, 21);
            this.rbHSBSaturation.TabIndex = 2;
            this.rbHSBSaturation.TabStop = true;
            this.rbHSBSaturation.Text = "Saturation";
            this.rbHSBSaturation.UseVisualStyleBackColor = true;
            // 
            // rbHSBHue
            // 
            this.rbHSBHue.AutoSize = true;
            this.rbHSBHue.Location = new System.Drawing.Point(5, 39);
            this.rbHSBHue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbHSBHue.Name = "rbHSBHue";
            this.rbHSBHue.Size = new System.Drawing.Size(55, 21);
            this.rbHSBHue.TabIndex = 1;
            this.rbHSBHue.TabStop = true;
            this.rbHSBHue.Text = "Hue";
            this.rbHSBHue.UseVisualStyleBackColor = true;
            // 
            // ccbHSBColor
            // 
            this.ccbHSBColor.ColorFormat = WinkingCat.HelperLibs.ColorFormat.RGB;
            this.ccbHSBColor.DecimalPlaces = ((byte)(1));
            this.ccbHSBColor.Location = new System.Drawing.Point(5, 4);
            this.ccbHSBColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ccbHSBColor.MaxValues = new decimal[] {
        new decimal(new int[] {
                    255,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    255,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    255,
                    0,
                    0,
                    0})};
            this.ccbHSBColor.MinValues = new decimal[] {
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0})};
            this.ccbHSBColor.Name = "ccbHSBColor";
            this.ccbHSBColor.Size = new System.Drawing.Size(316, 28);
            this.ccbHSBColor.TabIndex = 0;
            this.ccbHSBColor.Values = new decimal[] {
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0})};
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(411, 130);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "HSB Color";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(411, 246);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "HSL Color";
            // 
            // pnlHSLColor
            // 
            this.pnlHSLColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHSLColor.Controls.Add(this.rbHSLLightness);
            this.pnlHSLColor.Controls.Add(this.rbHSLSaturation);
            this.pnlHSLColor.Controls.Add(this.rbHSLHue);
            this.pnlHSLColor.Controls.Add(this.ccbHSLColor);
            this.pnlHSLColor.Location = new System.Drawing.Point(411, 266);
            this.pnlHSLColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlHSLColor.Name = "pnlHSLColor";
            this.pnlHSLColor.Size = new System.Drawing.Size(322, 66);
            this.pnlHSLColor.TabIndex = 6;
            // 
            // rbHSLLightness
            // 
            this.rbHSLLightness.AutoSize = true;
            this.rbHSLLightness.Location = new System.Drawing.Point(211, 39);
            this.rbHSLLightness.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbHSLLightness.Name = "rbHSLLightness";
            this.rbHSLLightness.Size = new System.Drawing.Size(90, 21);
            this.rbHSLLightness.TabIndex = 3;
            this.rbHSLLightness.TabStop = true;
            this.rbHSLLightness.Text = "Lightness";
            this.rbHSLLightness.UseVisualStyleBackColor = true;
            // 
            // rbHSLSaturation
            // 
            this.rbHSLSaturation.AutoSize = true;
            this.rbHSLSaturation.Location = new System.Drawing.Point(105, 39);
            this.rbHSLSaturation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbHSLSaturation.Name = "rbHSLSaturation";
            this.rbHSLSaturation.Size = new System.Drawing.Size(94, 21);
            this.rbHSLSaturation.TabIndex = 2;
            this.rbHSLSaturation.TabStop = true;
            this.rbHSLSaturation.Text = "Saturation";
            this.rbHSLSaturation.UseVisualStyleBackColor = true;
            // 
            // rbHSLHue
            // 
            this.rbHSLHue.AutoSize = true;
            this.rbHSLHue.Location = new System.Drawing.Point(5, 39);
            this.rbHSLHue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbHSLHue.Name = "rbHSLHue";
            this.rbHSLHue.Size = new System.Drawing.Size(55, 21);
            this.rbHSLHue.TabIndex = 1;
            this.rbHSLHue.TabStop = true;
            this.rbHSLHue.Text = "Hue";
            this.rbHSLHue.UseVisualStyleBackColor = true;
            // 
            // ccbHSLColor
            // 
            this.ccbHSLColor.ColorFormat = WinkingCat.HelperLibs.ColorFormat.RGB;
            this.ccbHSLColor.DecimalPlaces = ((byte)(1));
            this.ccbHSLColor.Location = new System.Drawing.Point(5, 4);
            this.ccbHSLColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ccbHSLColor.MaxValues = new decimal[] {
        new decimal(new int[] {
                    255,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    255,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    255,
                    0,
                    0,
                    0})};
            this.ccbHSLColor.MinValues = new decimal[] {
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0})};
            this.ccbHSLColor.Name = "ccbHSLColor";
            this.ccbHSLColor.Size = new System.Drawing.Size(316, 28);
            this.ccbHSLColor.TabIndex = 0;
            this.ccbHSLColor.Values = new decimal[] {
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0})};
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(768, 15);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Adobe RGB";
            // 
            // pnlAdobeRGBColor
            // 
            this.pnlAdobeRGBColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAdobeRGBColor.Controls.Add(this.rbAdobeRGBBlue);
            this.pnlAdobeRGBColor.Controls.Add(this.rbAdobeRGBGreen);
            this.pnlAdobeRGBColor.Controls.Add(this.rbAdobeRGBRed);
            this.pnlAdobeRGBColor.Controls.Add(this.ccbAdobeRGBColor);
            this.pnlAdobeRGBColor.Location = new System.Drawing.Point(768, 34);
            this.pnlAdobeRGBColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlAdobeRGBColor.Name = "pnlAdobeRGBColor";
            this.pnlAdobeRGBColor.Size = new System.Drawing.Size(322, 67);
            this.pnlAdobeRGBColor.TabIndex = 8;
            // 
            // rbAdobeRGBBlue
            // 
            this.rbAdobeRGBBlue.AutoSize = true;
            this.rbAdobeRGBBlue.Location = new System.Drawing.Point(201, 36);
            this.rbAdobeRGBBlue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbAdobeRGBBlue.Name = "rbAdobeRGBBlue";
            this.rbAdobeRGBBlue.Size = new System.Drawing.Size(57, 21);
            this.rbAdobeRGBBlue.TabIndex = 3;
            this.rbAdobeRGBBlue.TabStop = true;
            this.rbAdobeRGBBlue.Text = "Blue";
            this.rbAdobeRGBBlue.UseVisualStyleBackColor = true;
            // 
            // rbAdobeRGBGreen
            // 
            this.rbAdobeRGBGreen.AutoSize = true;
            this.rbAdobeRGBGreen.Location = new System.Drawing.Point(104, 36);
            this.rbAdobeRGBGreen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbAdobeRGBGreen.Name = "rbAdobeRGBGreen";
            this.rbAdobeRGBGreen.Size = new System.Drawing.Size(69, 21);
            this.rbAdobeRGBGreen.TabIndex = 2;
            this.rbAdobeRGBGreen.TabStop = true;
            this.rbAdobeRGBGreen.Text = "Green";
            this.rbAdobeRGBGreen.UseVisualStyleBackColor = true;
            // 
            // rbAdobeRGBRed
            // 
            this.rbAdobeRGBRed.AutoSize = true;
            this.rbAdobeRGBRed.Location = new System.Drawing.Point(4, 36);
            this.rbAdobeRGBRed.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbAdobeRGBRed.Name = "rbAdobeRGBRed";
            this.rbAdobeRGBRed.Size = new System.Drawing.Size(55, 21);
            this.rbAdobeRGBRed.TabIndex = 1;
            this.rbAdobeRGBRed.TabStop = true;
            this.rbAdobeRGBRed.Text = "Red";
            this.rbAdobeRGBRed.UseVisualStyleBackColor = true;
            // 
            // ccbAdobeRGBColor
            // 
            this.ccbAdobeRGBColor.ColorFormat = WinkingCat.HelperLibs.ColorFormat.RGB;
            this.ccbAdobeRGBColor.DecimalPlaces = ((byte)(1));
            this.ccbAdobeRGBColor.Location = new System.Drawing.Point(4, 5);
            this.ccbAdobeRGBColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ccbAdobeRGBColor.MaxValues = new decimal[] {
        new decimal(new int[] {
                    255,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    255,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    255,
                    0,
                    0,
                    0})};
            this.ccbAdobeRGBColor.MinValues = new decimal[] {
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0})};
            this.ccbAdobeRGBColor.Name = "ccbAdobeRGBColor";
            this.ccbAdobeRGBColor.Size = new System.Drawing.Size(317, 28);
            this.ccbAdobeRGBColor.TabIndex = 0;
            this.ccbAdobeRGBColor.Values = new decimal[] {
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0})};
            // 
            // pnlXYZColor
            // 
            this.pnlXYZColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlXYZColor.Controls.Add(this.rbX);
            this.pnlXYZColor.Controls.Add(this.ccbXYZColor);
            this.pnlXYZColor.Location = new System.Drawing.Point(768, 150);
            this.pnlXYZColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlXYZColor.Name = "pnlXYZColor";
            this.pnlXYZColor.Size = new System.Drawing.Size(322, 67);
            this.pnlXYZColor.TabIndex = 10;
            // 
            // rbX
            // 
            this.rbX.AutoSize = true;
            this.rbX.Location = new System.Drawing.Point(4, 39);
            this.rbX.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbX.Name = "rbX";
            this.rbX.Size = new System.Drawing.Size(38, 21);
            this.rbX.TabIndex = 1;
            this.rbX.TabStop = true;
            this.rbX.Text = "X";
            this.rbX.UseVisualStyleBackColor = true;
            // 
            // ccbXYZColor
            // 
            this.ccbXYZColor.ColorFormat = WinkingCat.HelperLibs.ColorFormat.RGB;
            this.ccbXYZColor.DecimalPlaces = ((byte)(1));
            this.ccbXYZColor.Location = new System.Drawing.Point(4, 5);
            this.ccbXYZColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ccbXYZColor.MaxValues = new decimal[] {
        new decimal(new int[] {
                    255,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    255,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    255,
                    0,
                    0,
                    0})};
            this.ccbXYZColor.MinValues = new decimal[] {
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0})};
            this.ccbXYZColor.Name = "ccbXYZColor";
            this.ccbXYZColor.Size = new System.Drawing.Size(317, 28);
            this.ccbXYZColor.TabIndex = 0;
            this.ccbXYZColor.Values = new decimal[] {
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0})};
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(768, 130);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "XYZ Color";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(768, 233);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "YXY Color";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(769, 288);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "CMYK Color";
            // 
            // button1
            // 
            this.button1.Image = global::WinkingCat.Properties.Resources.color_picker_icon;
            this.button1.Location = new System.Drawing.Point(85, 364);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 49);
            this.button1.TabIndex = 13;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Image = global::WinkingCat.Properties.Resources.Clipboard_2_icon;
            this.button2.Location = new System.Drawing.Point(147, 364);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(53, 49);
            this.button2.TabIndex = 14;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(660, 364);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(195, 49);
            this.button3.TabIndex = 15;
            this.button3.Text = "Copy";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(957, 357);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.MaxLength = 8;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 24);
            this.textBox1.TabIndex = 18;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(957, 389);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(132, 22);
            this.textBox2.TabIndex = 19;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(881, 361);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 17);
            this.label8.TabIndex = 20;
            this.label8.Text = "Hex:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(881, 393);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 17);
            this.label9.TabIndex = 21;
            this.label9.Text = "Decimal:";
            // 
            // displayColorLabel
            // 
            this.displayColorLabel.BackColor = System.Drawing.Color.Red;
            this.displayColorLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.displayColorLabel.Location = new System.Drawing.Point(16, 357);
            this.displayColorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.displayColorLabel.Name = "displayColorLabel";
            this.displayColorLabel.Size = new System.Drawing.Size(61, 56);
            this.displayColorLabel.TabIndex = 22;
            // 
            // cbCopyFormat
            // 
            this.cbCopyFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCopyFormat.FormattingEnabled = true;
            this.cbCopyFormat.Location = new System.Drawing.Point(491, 388);
            this.cbCopyFormat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbCopyFormat.Name = "cbCopyFormat";
            this.cbCopyFormat.Size = new System.Drawing.Size(160, 24);
            this.cbCopyFormat.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(487, 368);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 17);
            this.label10.TabIndex = 24;
            this.label10.Text = "Copy Format";
            // 
            // ccbCMYKColor
            // 
            this.ccbCMYKColor.ColorFormat = WinkingCat.HelperLibs.ColorFormat.CMYK;
            this.ccbCMYKColor.DecimalPlaces = ((byte)(1));
            this.ccbCMYKColor.Location = new System.Drawing.Point(772, 308);
            this.ccbCMYKColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ccbCMYKColor.MaxValues = new decimal[] {
        new decimal(new int[] {
                    100,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    100,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    100,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    100,
                    0,
                    0,
                    0})};
            this.ccbCMYKColor.MinValues = new decimal[] {
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0})};
            this.ccbCMYKColor.Name = "ccbCMYKColor";
            this.ccbCMYKColor.Size = new System.Drawing.Size(316, 28);
            this.ccbCMYKColor.TabIndex = 17;
            this.ccbCMYKColor.Values = new decimal[] {
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0})};
            // 
            // ccbYXYColor
            // 
            this.ccbYXYColor.ColorFormat = WinkingCat.HelperLibs.ColorFormat.RGB;
            this.ccbYXYColor.DecimalPlaces = ((byte)(1));
            this.ccbYXYColor.Location = new System.Drawing.Point(772, 252);
            this.ccbYXYColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ccbYXYColor.MaxValues = new decimal[] {
        new decimal(new int[] {
                    255,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    255,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    255,
                    0,
                    0,
                    0})};
            this.ccbYXYColor.MinValues = new decimal[] {
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    0,
                    0,
                    0,
                    0})};
            this.ccbYXYColor.Name = "ccbYXYColor";
            this.ccbYXYColor.Size = new System.Drawing.Size(318, 28);
            this.ccbYXYColor.TabIndex = 16;
            this.ccbYXYColor.Values = new decimal[] {
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0}),
        new decimal(new int[] {
                    1,
                    0,
                    0,
                    0})};
            // 
            // colorPicker
            // 
            this.colorPicker.AutoSize = true;
            this.colorPicker.DrawStyle = WinkingCat.HelperLibs.DrawStyles.HSBHue;
            this.colorPicker.Location = new System.Drawing.Point(16, 15);
            this.colorPicker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.colorPicker.Name = "colorPicker";
            this.colorPicker.Size = new System.Drawing.Size(387, 321);
            this.colorPicker.TabIndex = 0;
            // 
            // ColorPickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 437);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbCopyFormat);
            this.Controls.Add(this.displayColorLabel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ccbCMYKColor);
            this.Controls.Add(this.ccbYXYColor);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pnlXYZColor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pnlAdobeRGBColor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pnlHSLColor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlHSBColor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlRGBColor);
            this.Controls.Add(this.colorPicker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ColorPickerForm";
            this.Text = "ColorPickerForm";
            this.pnlRGBColor.ResumeLayout(false);
            this.pnlRGBColor.PerformLayout();
            this.pnlHSBColor.ResumeLayout(false);
            this.pnlHSBColor.PerformLayout();
            this.pnlHSLColor.ResumeLayout(false);
            this.pnlHSLColor.PerformLayout();
            this.pnlAdobeRGBColor.ResumeLayout(false);
            this.pnlAdobeRGBColor.PerformLayout();
            this.pnlXYZColor.ResumeLayout(false);
            this.pnlXYZColor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HelperLibs.ColorPicker colorPicker;
        private System.Windows.Forms.Panel pnlRGBColor;
        private System.Windows.Forms.RadioButton rbBlue;
        private System.Windows.Forms.RadioButton rbGreen;
        private System.Windows.Forms.RadioButton rbRed;
        private HelperLibs.ColorComboBox ccbRGBColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlHSBColor;
        private System.Windows.Forms.RadioButton rbHSBBrightness;
        private System.Windows.Forms.RadioButton rbHSBSaturation;
        private System.Windows.Forms.RadioButton rbHSBHue;
        private HelperLibs.ColorComboBox ccbHSBColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlHSLColor;
        private System.Windows.Forms.RadioButton rbHSLLightness;
        private System.Windows.Forms.RadioButton rbHSLSaturation;
        private System.Windows.Forms.RadioButton rbHSLHue;
        private HelperLibs.ColorComboBox ccbHSLColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlAdobeRGBColor;
        private System.Windows.Forms.Panel pnlXYZColor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private HelperLibs.ColorComboBox ccbAdobeRGBColor;
        private HelperLibs.ColorComboBox ccbXYZColor;
        private System.Windows.Forms.RadioButton rbAdobeRGBBlue;
        private System.Windows.Forms.RadioButton rbAdobeRGBGreen;
        private System.Windows.Forms.RadioButton rbAdobeRGBRed;
        private System.Windows.Forms.RadioButton rbX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private HelperLibs.ColorComboBox ccbYXYColor;
        private HelperLibs.ColorComboBox ccbCMYKColor;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label displayColorLabel;
        private System.Windows.Forms.ComboBox cbCopyFormat;
        private System.Windows.Forms.Label label10;
    }
}