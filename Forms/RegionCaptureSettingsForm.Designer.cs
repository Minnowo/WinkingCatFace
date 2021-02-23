namespace WinkingCat
{
    partial class RegionCaptureSettingsForm
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
            this.pDrawMagnifier = new System.Windows.Forms.Panel();
            this.nudMagnifierZoomLevel = new System.Windows.Forms.NumericUpDown();
            this.nudMagnifierPixelCount = new System.Windows.Forms.NumericUpDown();
            this.nudMagnifierPixelSize = new System.Windows.Forms.NumericUpDown();
            this.nudMagnifierZoomScale = new System.Windows.Forms.NumericUpDown();
            this.lMagnifierZoomLevel = new System.Windows.Forms.Label();
            this.lMagnifierZoomScale = new System.Windows.Forms.Label();
            this.lMagnifierPixelSize = new System.Windows.Forms.Label();
            this.lMagnifierPixelCount = new System.Windows.Forms.Label();
            this.cbDrawMagnifierBorder = new System.Windows.Forms.CheckBox();
            this.cbCenterMagnifierOnMouse = new System.Windows.Forms.CheckBox();
            this.cbDrawMagnifierGrid = new System.Windows.Forms.CheckBox();
            this.cbDrawMagnifierCrosshair = new System.Windows.Forms.CheckBox();
            this.cbDrawMagnifier = new System.Windows.Forms.CheckBox();
            this.cbDrawScreenWideCrosshair = new System.Windows.Forms.CheckBox();
            this.pMouseButtons = new System.Windows.Forms.Panel();
            this.combobXButton2ClickAction = new System.Windows.Forms.ComboBox();
            this.lXbutton2ClickAction = new System.Windows.Forms.Label();
            this.combobXButton1ClickAction = new System.Windows.Forms.ComboBox();
            this.lXbutton1ClickAction = new System.Windows.Forms.Label();
            this.combobMouseMiddleClickAction = new System.Windows.Forms.ComboBox();
            this.lMouseMiddleClickAction = new System.Windows.Forms.Label();
            this.combobMouseRightClickAction = new System.Windows.Forms.ComboBox();
            this.lMouseRightClickAction = new System.Windows.Forms.Label();
            this.cbDimBackground = new System.Windows.Forms.CheckBox();
            this.cbDrawInfoText = new System.Windows.Forms.CheckBox();
            this.lMouseButtons = new System.Windows.Forms.Label();
            this.cbUpdateOnMouseMove = new System.Windows.Forms.CheckBox();
            this.cbDrawMarchingAnts = new System.Windows.Forms.CheckBox();
            this.pDrawMagnifier.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagnifierZoomLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagnifierPixelCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagnifierPixelSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagnifierZoomScale)).BeginInit();
            this.pMouseButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pDrawMagnifier
            // 
            this.pDrawMagnifier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pDrawMagnifier.Controls.Add(this.nudMagnifierZoomLevel);
            this.pDrawMagnifier.Controls.Add(this.nudMagnifierPixelCount);
            this.pDrawMagnifier.Controls.Add(this.nudMagnifierPixelSize);
            this.pDrawMagnifier.Controls.Add(this.nudMagnifierZoomScale);
            this.pDrawMagnifier.Controls.Add(this.lMagnifierZoomLevel);
            this.pDrawMagnifier.Controls.Add(this.lMagnifierZoomScale);
            this.pDrawMagnifier.Controls.Add(this.lMagnifierPixelSize);
            this.pDrawMagnifier.Controls.Add(this.lMagnifierPixelCount);
            this.pDrawMagnifier.Controls.Add(this.cbDrawMagnifierBorder);
            this.pDrawMagnifier.Controls.Add(this.cbCenterMagnifierOnMouse);
            this.pDrawMagnifier.Controls.Add(this.cbDrawMagnifierGrid);
            this.pDrawMagnifier.Controls.Add(this.cbDrawMagnifierCrosshair);
            this.pDrawMagnifier.Location = new System.Drawing.Point(13, 297);
            this.pDrawMagnifier.Name = "pDrawMagnifier";
            this.pDrawMagnifier.Padding = new System.Windows.Forms.Padding(10);
            this.pDrawMagnifier.Size = new System.Drawing.Size(456, 193);
            this.pDrawMagnifier.TabIndex = 20;
            // 
            // nudMagnifierZoomLevel
            // 
            this.nudMagnifierZoomLevel.DecimalPlaces = 2;
            this.nudMagnifierZoomLevel.Location = new System.Drawing.Point(195, 152);
            this.nudMagnifierZoomLevel.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudMagnifierZoomLevel.Name = "nudMagnifierZoomLevel";
            this.nudMagnifierZoomLevel.Size = new System.Drawing.Size(248, 20);
            this.nudMagnifierZoomLevel.TabIndex = 16;
            this.nudMagnifierZoomLevel.ValueChanged += new System.EventHandler(this.NudMagnifierZoomLevel_ValueChanged);
            // 
            // nudMagnifierPixelCount
            // 
            this.nudMagnifierPixelCount.Location = new System.Drawing.Point(195, 71);
            this.nudMagnifierPixelCount.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudMagnifierPixelCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMagnifierPixelCount.Name = "nudMagnifierPixelCount";
            this.nudMagnifierPixelCount.Size = new System.Drawing.Size(248, 20);
            this.nudMagnifierPixelCount.TabIndex = 15;
            this.nudMagnifierPixelCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMagnifierPixelCount.ValueChanged += new System.EventHandler(this.NudMagnifierPixelCount_ValueChanged);
            // 
            // nudMagnifierPixelSize
            // 
            this.nudMagnifierPixelSize.Location = new System.Drawing.Point(195, 98);
            this.nudMagnifierPixelSize.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMagnifierPixelSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMagnifierPixelSize.Name = "nudMagnifierPixelSize";
            this.nudMagnifierPixelSize.Size = new System.Drawing.Size(248, 20);
            this.nudMagnifierPixelSize.TabIndex = 14;
            this.nudMagnifierPixelSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMagnifierPixelSize.ValueChanged += new System.EventHandler(this.NudMagnifierPixelSize_ValueChanged);
            // 
            // nudMagnifierZoomScale
            // 
            this.nudMagnifierZoomScale.DecimalPlaces = 2;
            this.nudMagnifierZoomScale.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.nudMagnifierZoomScale.Location = new System.Drawing.Point(195, 125);
            this.nudMagnifierZoomScale.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudMagnifierZoomScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudMagnifierZoomScale.Name = "nudMagnifierZoomScale";
            this.nudMagnifierZoomScale.Size = new System.Drawing.Size(248, 20);
            this.nudMagnifierZoomScale.TabIndex = 13;
            this.nudMagnifierZoomScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudMagnifierZoomScale.ValueChanged += new System.EventHandler(this.NudMagnifierZoomScale_ValueChanged);
            // 
            // lMagnifierZoomLevel
            // 
            this.lMagnifierZoomLevel.AutoSize = true;
            this.lMagnifierZoomLevel.Location = new System.Drawing.Point(10, 154);
            this.lMagnifierZoomLevel.Name = "lMagnifierZoomLevel";
            this.lMagnifierZoomLevel.Size = new System.Drawing.Size(109, 13);
            this.lMagnifierZoomLevel.TabIndex = 11;
            this.lMagnifierZoomLevel.Text = "Magnifier Zoom Level";
            // 
            // lMagnifierZoomScale
            // 
            this.lMagnifierZoomScale.AutoSize = true;
            this.lMagnifierZoomScale.Location = new System.Drawing.Point(10, 127);
            this.lMagnifierZoomScale.Name = "lMagnifierZoomScale";
            this.lMagnifierZoomScale.Size = new System.Drawing.Size(110, 13);
            this.lMagnifierZoomScale.TabIndex = 9;
            this.lMagnifierZoomScale.Text = "Magnifier Zoom Scale";
            // 
            // lMagnifierPixelSize
            // 
            this.lMagnifierPixelSize.AutoSize = true;
            this.lMagnifierPixelSize.Location = new System.Drawing.Point(10, 100);
            this.lMagnifierPixelSize.Name = "lMagnifierPixelSize";
            this.lMagnifierPixelSize.Size = new System.Drawing.Size(98, 13);
            this.lMagnifierPixelSize.TabIndex = 7;
            this.lMagnifierPixelSize.Text = "Magnifier Pixel Size";
            // 
            // lMagnifierPixelCount
            // 
            this.lMagnifierPixelCount.AutoSize = true;
            this.lMagnifierPixelCount.Location = new System.Drawing.Point(10, 73);
            this.lMagnifierPixelCount.Name = "lMagnifierPixelCount";
            this.lMagnifierPixelCount.Size = new System.Drawing.Size(106, 13);
            this.lMagnifierPixelCount.TabIndex = 5;
            this.lMagnifierPixelCount.Text = "Magnifier Pixel Count";
            // 
            // cbDrawMagnifierBorder
            // 
            this.cbDrawMagnifierBorder.AutoSize = true;
            this.cbDrawMagnifierBorder.Location = new System.Drawing.Point(195, 13);
            this.cbDrawMagnifierBorder.Name = "cbDrawMagnifierBorder";
            this.cbDrawMagnifierBorder.Size = new System.Drawing.Size(131, 17);
            this.cbDrawMagnifierBorder.TabIndex = 4;
            this.cbDrawMagnifierBorder.Text = "Draw Magnifier Border";
            this.cbDrawMagnifierBorder.UseVisualStyleBackColor = true;
            this.cbDrawMagnifierBorder.CheckedChanged += new System.EventHandler(this.CbDrawMagnifierBorder_CheckedChanged);
            // 
            // cbCenterMagnifierOnMouse
            // 
            this.cbCenterMagnifierOnMouse.AutoSize = true;
            this.cbCenterMagnifierOnMouse.Location = new System.Drawing.Point(195, 36);
            this.cbCenterMagnifierOnMouse.Name = "cbCenterMagnifierOnMouse";
            this.cbCenterMagnifierOnMouse.Size = new System.Drawing.Size(155, 17);
            this.cbCenterMagnifierOnMouse.TabIndex = 3;
            this.cbCenterMagnifierOnMouse.Text = "Center Magnifier On Mouse";
            this.cbCenterMagnifierOnMouse.UseVisualStyleBackColor = true;
            this.cbCenterMagnifierOnMouse.CheckedChanged += new System.EventHandler(this.CbCenterMagnifierOnMouse_CheckedChanged);
            // 
            // cbDrawMagnifierGrid
            // 
            this.cbDrawMagnifierGrid.AutoSize = true;
            this.cbDrawMagnifierGrid.Location = new System.Drawing.Point(13, 37);
            this.cbDrawMagnifierGrid.Name = "cbDrawMagnifierGrid";
            this.cbDrawMagnifierGrid.Size = new System.Drawing.Size(119, 17);
            this.cbDrawMagnifierGrid.TabIndex = 2;
            this.cbDrawMagnifierGrid.Text = "Draw Magnifier Grid";
            this.cbDrawMagnifierGrid.UseVisualStyleBackColor = true;
            this.cbDrawMagnifierGrid.CheckedChanged += new System.EventHandler(this.CbDrawMagnifierGrid_CheckedChanged);
            // 
            // cbDrawMagnifierCrosshair
            // 
            this.cbDrawMagnifierCrosshair.AutoSize = true;
            this.cbDrawMagnifierCrosshair.Location = new System.Drawing.Point(13, 13);
            this.cbDrawMagnifierCrosshair.Name = "cbDrawMagnifierCrosshair";
            this.cbDrawMagnifierCrosshair.Size = new System.Drawing.Size(143, 17);
            this.cbDrawMagnifierCrosshair.TabIndex = 1;
            this.cbDrawMagnifierCrosshair.Text = "Draw Magnifier Crosshair";
            this.cbDrawMagnifierCrosshair.UseVisualStyleBackColor = true;
            this.cbDrawMagnifierCrosshair.CheckedChanged += new System.EventHandler(this.CbDrawMagnifierCrosshair_CheckedChanged);
            // 
            // cbDrawMagnifier
            // 
            this.cbDrawMagnifier.AutoSize = true;
            this.cbDrawMagnifier.Location = new System.Drawing.Point(13, 274);
            this.cbDrawMagnifier.Name = "cbDrawMagnifier";
            this.cbDrawMagnifier.Size = new System.Drawing.Size(97, 17);
            this.cbDrawMagnifier.TabIndex = 0;
            this.cbDrawMagnifier.Text = "Draw Magnifier";
            this.cbDrawMagnifier.UseVisualStyleBackColor = true;
            this.cbDrawMagnifier.CheckedChanged += new System.EventHandler(this.CbDrawMagnifier_CheckedChanged);
            // 
            // cbDrawScreenWideCrosshair
            // 
            this.cbDrawScreenWideCrosshair.AutoSize = true;
            this.cbDrawScreenWideCrosshair.Location = new System.Drawing.Point(13, 18);
            this.cbDrawScreenWideCrosshair.Name = "cbDrawScreenWideCrosshair";
            this.cbDrawScreenWideCrosshair.Size = new System.Drawing.Size(162, 17);
            this.cbDrawScreenWideCrosshair.TabIndex = 21;
            this.cbDrawScreenWideCrosshair.Text = "Draw Screen Wide Crosshair";
            this.cbDrawScreenWideCrosshair.UseVisualStyleBackColor = true;
            this.cbDrawScreenWideCrosshair.CheckedChanged += new System.EventHandler(this.CbDrawScreenWideCrosshair_CheckedChanged);
            // 
            // pMouseButtons
            // 
            this.pMouseButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pMouseButtons.Controls.Add(this.combobXButton2ClickAction);
            this.pMouseButtons.Controls.Add(this.lXbutton2ClickAction);
            this.pMouseButtons.Controls.Add(this.combobXButton1ClickAction);
            this.pMouseButtons.Controls.Add(this.lXbutton1ClickAction);
            this.pMouseButtons.Controls.Add(this.combobMouseMiddleClickAction);
            this.pMouseButtons.Controls.Add(this.lMouseMiddleClickAction);
            this.pMouseButtons.Controls.Add(this.combobMouseRightClickAction);
            this.pMouseButtons.Controls.Add(this.lMouseRightClickAction);
            this.pMouseButtons.Location = new System.Drawing.Point(13, 125);
            this.pMouseButtons.Name = "pMouseButtons";
            this.pMouseButtons.Padding = new System.Windows.Forms.Padding(10);
            this.pMouseButtons.Size = new System.Drawing.Size(456, 128);
            this.pMouseButtons.TabIndex = 21;
            // 
            // combobXButton2ClickAction
            // 
            this.combobXButton2ClickAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobXButton2ClickAction.FormattingEnabled = true;
            this.combobXButton2ClickAction.Location = new System.Drawing.Point(195, 94);
            this.combobXButton2ClickAction.Name = "combobXButton2ClickAction";
            this.combobXButton2ClickAction.Size = new System.Drawing.Size(248, 21);
            this.combobXButton2ClickAction.TabIndex = 12;
            this.combobXButton2ClickAction.SelectedIndexChanged += new System.EventHandler(this.CombobXButton2ClickAction_SelectionChanged);
            // 
            // lXbutton2ClickAction
            // 
            this.lXbutton2ClickAction.AutoSize = true;
            this.lXbutton2ClickAction.Location = new System.Drawing.Point(10, 97);
            this.lXbutton2ClickAction.Name = "lXbutton2ClickAction";
            this.lXbutton2ClickAction.Size = new System.Drawing.Size(113, 13);
            this.lXbutton2ClickAction.TabIndex = 11;
            this.lXbutton2ClickAction.Text = "XButton2 Click Action:";
            // 
            // combobXButton1ClickAction
            // 
            this.combobXButton1ClickAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobXButton1ClickAction.FormattingEnabled = true;
            this.combobXButton1ClickAction.Location = new System.Drawing.Point(195, 67);
            this.combobXButton1ClickAction.Name = "combobXButton1ClickAction";
            this.combobXButton1ClickAction.Size = new System.Drawing.Size(248, 21);
            this.combobXButton1ClickAction.TabIndex = 10;
            this.combobXButton1ClickAction.SelectedIndexChanged += new System.EventHandler(this.CombobXButton1ClickAction_SelectionChanged);
            // 
            // lXbutton1ClickAction
            // 
            this.lXbutton1ClickAction.AutoSize = true;
            this.lXbutton1ClickAction.Location = new System.Drawing.Point(10, 70);
            this.lXbutton1ClickAction.Name = "lXbutton1ClickAction";
            this.lXbutton1ClickAction.Size = new System.Drawing.Size(113, 13);
            this.lXbutton1ClickAction.TabIndex = 9;
            this.lXbutton1ClickAction.Text = "XButton1 Click Action:";
            // 
            // combobMouseMiddleClickAction
            // 
            this.combobMouseMiddleClickAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobMouseMiddleClickAction.FormattingEnabled = true;
            this.combobMouseMiddleClickAction.Location = new System.Drawing.Point(195, 40);
            this.combobMouseMiddleClickAction.Name = "combobMouseMiddleClickAction";
            this.combobMouseMiddleClickAction.Size = new System.Drawing.Size(248, 21);
            this.combobMouseMiddleClickAction.TabIndex = 8;
            this.combobMouseMiddleClickAction.SelectedIndexChanged += new System.EventHandler(this.CombobMouseMiddleClickAction_SelectionChanged);
            // 
            // lMouseMiddleClickAction
            // 
            this.lMouseMiddleClickAction.AutoSize = true;
            this.lMouseMiddleClickAction.Location = new System.Drawing.Point(10, 43);
            this.lMouseMiddleClickAction.Name = "lMouseMiddleClickAction";
            this.lMouseMiddleClickAction.Size = new System.Drawing.Size(135, 13);
            this.lMouseMiddleClickAction.TabIndex = 7;
            this.lMouseMiddleClickAction.Text = "Mouse Middle Click Action:";
            // 
            // combobMouseRightClickAction
            // 
            this.combobMouseRightClickAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobMouseRightClickAction.FormattingEnabled = true;
            this.combobMouseRightClickAction.Location = new System.Drawing.Point(195, 13);
            this.combobMouseRightClickAction.Name = "combobMouseRightClickAction";
            this.combobMouseRightClickAction.Size = new System.Drawing.Size(248, 21);
            this.combobMouseRightClickAction.TabIndex = 6;
            this.combobMouseRightClickAction.SelectedIndexChanged += new System.EventHandler(this.CombobMouseRightClickAction_SelectionChanged);
            // 
            // lMouseRightClickAction
            // 
            this.lMouseRightClickAction.AutoSize = true;
            this.lMouseRightClickAction.Location = new System.Drawing.Point(10, 16);
            this.lMouseRightClickAction.Name = "lMouseRightClickAction";
            this.lMouseRightClickAction.Size = new System.Drawing.Size(129, 13);
            this.lMouseRightClickAction.TabIndex = 5;
            this.lMouseRightClickAction.Text = "Mouse Right Click Action:";
            // 
            // cbDimBackground
            // 
            this.cbDimBackground.AutoSize = true;
            this.cbDimBackground.Location = new System.Drawing.Point(13, 41);
            this.cbDimBackground.Name = "cbDimBackground";
            this.cbDimBackground.Size = new System.Drawing.Size(108, 17);
            this.cbDimBackground.TabIndex = 22;
            this.cbDimBackground.Text = "Dim Background ";
            this.cbDimBackground.UseVisualStyleBackColor = true;
            this.cbDimBackground.CheckedChanged += new System.EventHandler(this.CbDimBackground_CheckedChanged);
            // 
            // cbDrawInfoText
            // 
            this.cbDrawInfoText.AutoSize = true;
            this.cbDrawInfoText.Location = new System.Drawing.Point(208, 18);
            this.cbDrawInfoText.Name = "cbDrawInfoText";
            this.cbDrawInfoText.Size = new System.Drawing.Size(96, 17);
            this.cbDrawInfoText.TabIndex = 23;
            this.cbDrawInfoText.Text = "Draw Info Text";
            this.cbDrawInfoText.UseVisualStyleBackColor = true;
            this.cbDrawInfoText.CheckedChanged += new System.EventHandler(this.CbDrawInfoText_CheckedChanged);
            // 
            // lMouseButtons
            // 
            this.lMouseButtons.AutoSize = true;
            this.lMouseButtons.Location = new System.Drawing.Point(10, 109);
            this.lMouseButtons.Name = "lMouseButtons";
            this.lMouseButtons.Size = new System.Drawing.Size(78, 13);
            this.lMouseButtons.TabIndex = 24;
            this.lMouseButtons.Text = "Mouse Buttons";
            // 
            // cbUpdateOnMouseMove
            // 
            this.cbUpdateOnMouseMove.AutoSize = true;
            this.cbUpdateOnMouseMove.Location = new System.Drawing.Point(208, 41);
            this.cbUpdateOnMouseMove.Name = "cbUpdateOnMouseMove";
            this.cbUpdateOnMouseMove.Size = new System.Drawing.Size(255, 17);
            this.cbUpdateOnMouseMove.TabIndex = 25;
            this.cbUpdateOnMouseMove.Text = "Update On MouseMove Event (0 cpu when idle)";
            this.cbUpdateOnMouseMove.UseVisualStyleBackColor = true;
            this.cbUpdateOnMouseMove.CheckedChanged += new System.EventHandler(this.CbUpdateOnMouseMove_CheckedChanged);
            // 
            // cbDrawMarchingAnts
            // 
            this.cbDrawMarchingAnts.AutoSize = true;
            this.cbDrawMarchingAnts.Location = new System.Drawing.Point(13, 64);
            this.cbDrawMarchingAnts.Name = "cbDrawMarchingAnts";
            this.cbDrawMarchingAnts.Size = new System.Drawing.Size(122, 17);
            this.cbDrawMarchingAnts.TabIndex = 26;
            this.cbDrawMarchingAnts.Text = "Draw Marching Ants";
            this.cbDrawMarchingAnts.UseVisualStyleBackColor = true;
            this.cbDrawMarchingAnts.CheckedChanged += new System.EventHandler(this.CbDrawMarchingAnts_CheckedChanged);
            // 
            // RegionCaptureSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(483, 504);
            this.Controls.Add(this.cbDrawMarchingAnts);
            this.Controls.Add(this.cbUpdateOnMouseMove);
            this.Controls.Add(this.lMouseButtons);
            this.Controls.Add(this.cbDrawInfoText);
            this.Controls.Add(this.cbDimBackground);
            this.Controls.Add(this.pMouseButtons);
            this.Controls.Add(this.cbDrawScreenWideCrosshair);
            this.Controls.Add(this.pDrawMagnifier);
            this.Controls.Add(this.cbDrawMagnifier);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegionCaptureSettingsForm";
            this.Padding = new System.Windows.Forms.Padding(10, 15, 10, 10);
            this.Text = "RegionCaptureSettingsForm";
            this.pDrawMagnifier.ResumeLayout(false);
            this.pDrawMagnifier.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagnifierZoomLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagnifierPixelCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagnifierPixelSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagnifierZoomScale)).EndInit();
            this.pMouseButtons.ResumeLayout(false);
            this.pMouseButtons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pDrawMagnifier;
        private System.Windows.Forms.CheckBox cbCenterMagnifierOnMouse;
        private System.Windows.Forms.CheckBox cbDrawMagnifierGrid;
        private System.Windows.Forms.CheckBox cbDrawMagnifierCrosshair;
        private System.Windows.Forms.CheckBox cbDrawMagnifier;
        private System.Windows.Forms.Label lMagnifierZoomLevel;
        private System.Windows.Forms.Label lMagnifierZoomScale;
        private System.Windows.Forms.Label lMagnifierPixelSize;
        private System.Windows.Forms.Label lMagnifierPixelCount;
        private System.Windows.Forms.CheckBox cbDrawMagnifierBorder;
        private System.Windows.Forms.CheckBox cbDrawScreenWideCrosshair;
        private System.Windows.Forms.Panel pMouseButtons;
        private System.Windows.Forms.ComboBox combobXButton2ClickAction;
        private System.Windows.Forms.Label lXbutton2ClickAction;
        private System.Windows.Forms.ComboBox combobXButton1ClickAction;
        private System.Windows.Forms.Label lXbutton1ClickAction;
        private System.Windows.Forms.ComboBox combobMouseMiddleClickAction;
        private System.Windows.Forms.Label lMouseMiddleClickAction;
        private System.Windows.Forms.ComboBox combobMouseRightClickAction;
        private System.Windows.Forms.Label lMouseRightClickAction;
        private System.Windows.Forms.CheckBox cbDimBackground;
        private System.Windows.Forms.CheckBox cbDrawInfoText;
        private System.Windows.Forms.Label lMouseButtons;
        private System.Windows.Forms.CheckBox cbUpdateOnMouseMove;
        private System.Windows.Forms.CheckBox cbDrawMarchingAnts;
        private System.Windows.Forms.NumericUpDown nudMagnifierZoomLevel;
        private System.Windows.Forms.NumericUpDown nudMagnifierPixelCount;
        private System.Windows.Forms.NumericUpDown nudMagnifierPixelSize;
        private System.Windows.Forms.NumericUpDown nudMagnifierZoomScale;
    }
}