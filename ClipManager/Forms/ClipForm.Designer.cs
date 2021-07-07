namespace WinkingCat.ClipHelper
{
    partial class ClipForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClipForm));
            this.cmMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyDefaultContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyZoomScaledContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyZoomedImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenInEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInvertColor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiConvertToGray = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAllowResizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOCRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tssToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDestroyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tssToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDestroyAllClipsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zdbZoomedImageDisplay = new WinkingCat.HelperLibs.ZoomDrawingBoard();
            this.tsmiRotateLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRotateRight = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFlipHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFlipVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.cmMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmMain
            // 
            this.cmMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyToolStripMenuItem,
            this.tsmiEdit,
            this.tsmiAllowResizeToolStripMenuItem,
            this.tsmiOCRToolStripMenuItem,
            this.tsmiSaveToolStripMenuItem,
            this.tssToolStripSeparator2,
            this.tsmiDestroyToolStripMenuItem,
            this.tssToolStripSeparator1,
            this.tsmiDestroyAllClipsToolStripMenuItem});
            this.cmMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.cmMain.Name = "contextMenuStrip1";
            this.cmMain.Size = new System.Drawing.Size(197, 304);
            // 
            // tsmiCopyToolStripMenuItem
            // 
            this.tsmiCopyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyDefaultContextMenuItem,
            this.tsmiCopyZoomScaledContextMenuItem,
            this.tsmiCopyZoomedImage});
            this.tsmiCopyToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.Clipboard_2_icon;
            this.tsmiCopyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiCopyToolStripMenuItem.Name = "tsmiCopyToolStripMenuItem";
            this.tsmiCopyToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
            this.tsmiCopyToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiCopyToolStripMenuItem.Text = "Copy";
            // 
            // tsmiCopyDefaultContextMenuItem
            // 
            this.tsmiCopyDefaultContextMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.Clipboard_2_icon;
            this.tsmiCopyDefaultContextMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiCopyDefaultContextMenuItem.Name = "tsmiCopyDefaultContextMenuItem";
            this.tsmiCopyDefaultContextMenuItem.Size = new System.Drawing.Size(163, 38);
            this.tsmiCopyDefaultContextMenuItem.Text = "CopyDefault";
            // 
            // tsmiCopyZoomScaledContextMenuItem
            // 
            this.tsmiCopyZoomScaledContextMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCopyZoomScaledContextMenuItem.Image")));
            this.tsmiCopyZoomScaledContextMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiCopyZoomScaledContextMenuItem.Name = "tsmiCopyZoomScaledContextMenuItem";
            this.tsmiCopyZoomScaledContextMenuItem.Size = new System.Drawing.Size(163, 38);
            this.tsmiCopyZoomScaledContextMenuItem.Text = "CopyScaled";
            // 
            // tsmiCopyZoomedImage
            // 
            this.tsmiCopyZoomedImage.Image = global::WinkingCat.ClipHelper.Properties.Resources.layer_resize_icon;
            this.tsmiCopyZoomedImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiCopyZoomedImage.Name = "tsmiCopyZoomedImage";
            this.tsmiCopyZoomedImage.Size = new System.Drawing.Size(163, 38);
            this.tsmiCopyZoomedImage.Text = "CopyZoomed";
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenInEditor,
            this.tsmiInvertColor,
            this.tsmiConvertToGray,
            this.tsmiRotateLeft,
            this.tsmiRotateRight,
            this.tsmiFlipHorizontal,
            this.tsmiFlipVertical});
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(196, 38);
            this.tsmiEdit.Text = "Edit";
            // 
            // tsmiOpenInEditor
            // 
            this.tsmiOpenInEditor.Image = global::WinkingCat.ClipHelper.Properties.Resources.layer_stack_arrange_back_icon;
            this.tsmiOpenInEditor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiOpenInEditor.Name = "tsmiOpenInEditor";
            this.tsmiOpenInEditor.Size = new System.Drawing.Size(196, 38);
            this.tsmiOpenInEditor.Text = "Open In Editor";
            // 
            // tsmiInvertColor
            // 
            this.tsmiInvertColor.Name = "tsmiInvertColor";
            this.tsmiInvertColor.Size = new System.Drawing.Size(196, 38);
            this.tsmiInvertColor.Text = "Invert Color";
            // 
            // tsmiConvertToGray
            // 
            this.tsmiConvertToGray.Name = "tsmiConvertToGray";
            this.tsmiConvertToGray.Size = new System.Drawing.Size(196, 38);
            this.tsmiConvertToGray.Text = "Convert To Gray";
            // 
            // tsmiAllowResizeToolStripMenuItem
            // 
            this.tsmiAllowResizeToolStripMenuItem.CheckOnClick = true;
            this.tsmiAllowResizeToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.layer_resize_icon;
            this.tsmiAllowResizeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiAllowResizeToolStripMenuItem.Name = "tsmiAllowResizeToolStripMenuItem";
            this.tsmiAllowResizeToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+R";
            this.tsmiAllowResizeToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiAllowResizeToolStripMenuItem.Text = "AllowResize";
            // 
            // tsmiOCRToolStripMenuItem
            // 
            this.tsmiOCRToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.layer_shape_text_icon;
            this.tsmiOCRToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiOCRToolStripMenuItem.Name = "tsmiOCRToolStripMenuItem";
            this.tsmiOCRToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+T";
            this.tsmiOCRToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiOCRToolStripMenuItem.Text = "OCR";
            // 
            // tsmiSaveToolStripMenuItem
            // 
            this.tsmiSaveToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.save_download_icon;
            this.tsmiSaveToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiSaveToolStripMenuItem.Name = "tsmiSaveToolStripMenuItem";
            this.tsmiSaveToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            this.tsmiSaveToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiSaveToolStripMenuItem.Text = "Save";
            // 
            // tssToolStripSeparator2
            // 
            this.tssToolStripSeparator2.Name = "tssToolStripSeparator2";
            this.tssToolStripSeparator2.Size = new System.Drawing.Size(193, 6);
            // 
            // tsmiDestroyToolStripMenuItem
            // 
            this.tsmiDestroyToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.Error_Symbol_icon;
            this.tsmiDestroyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiDestroyToolStripMenuItem.Name = "tsmiDestroyToolStripMenuItem";
            this.tsmiDestroyToolStripMenuItem.ShortcutKeyDisplayString = "Esc";
            this.tsmiDestroyToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiDestroyToolStripMenuItem.Text = "Destroy";
            // 
            // tssToolStripSeparator1
            // 
            this.tssToolStripSeparator1.Name = "tssToolStripSeparator1";
            this.tssToolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // tsmiDestroyAllClipsToolStripMenuItem
            // 
            this.tsmiDestroyAllClipsToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.fire_icon;
            this.tsmiDestroyAllClipsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiDestroyAllClipsToolStripMenuItem.Name = "tsmiDestroyAllClipsToolStripMenuItem";
            this.tsmiDestroyAllClipsToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiDestroyAllClipsToolStripMenuItem.Text = "DestroyAllClips";
            // 
            // zdbZoomedImageDisplay
            // 
            this.zdbZoomedImageDisplay.BackColor = System.Drawing.Color.White;
            this.zdbZoomedImageDisplay.borderColor = System.Drawing.Color.Black;
            this.zdbZoomedImageDisplay.BorderThickness = 1;
            this.zdbZoomedImageDisplay.Enabled = false;
            this.zdbZoomedImageDisplay.image = null;
            this.zdbZoomedImageDisplay.Location = new System.Drawing.Point(0, 0);
            this.zdbZoomedImageDisplay.Name = "zdbZoomedImageDisplay";
            this.zdbZoomedImageDisplay.replaceTransparent = System.Drawing.Color.White;
            this.zdbZoomedImageDisplay.Size = new System.Drawing.Size(50, 50);
            this.zdbZoomedImageDisplay.TabIndex = 1;
            this.zdbZoomedImageDisplay.Visible = false;
            // 
            // tsmiRotateLeft
            // 
            this.tsmiRotateLeft.Name = "tsmiRotateLeft";
            this.tsmiRotateLeft.Size = new System.Drawing.Size(196, 38);
            this.tsmiRotateLeft.Text = "Rotate Left 90";
            // 
            // tsmiRotateRight
            // 
            this.tsmiRotateRight.Name = "tsmiRotateRight";
            this.tsmiRotateRight.Size = new System.Drawing.Size(196, 38);
            this.tsmiRotateRight.Text = "Rotate Right 90";
            // 
            // tsmiFlipHorizontal
            // 
            this.tsmiFlipHorizontal.Name = "tsmiFlipHorizontal";
            this.tsmiFlipHorizontal.Size = new System.Drawing.Size(196, 38);
            this.tsmiFlipHorizontal.Text = "Flip Horizontal";
            // 
            // tsmiFlipVertical
            // 
            this.tsmiFlipVertical.Name = "tsmiFlipVertical";
            this.tsmiFlipVertical.Size = new System.Drawing.Size(196, 38);
            this.tsmiFlipVertical.Text = "Flip Vertical";
            // 
            // ClipForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ContextMenuStrip = this.cmMain;
            this.Controls.Add(this.zdbZoomedImageDisplay);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ClipForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.cmMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cmMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiAllowResizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiOCRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator tssToolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiDestroyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator tssToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiDestroyAllClipsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyDefaultContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyZoomScaledContextMenuItem;
        private WinkingCat.HelperLibs.ZoomDrawingBoard zdbZoomedImageDisplay;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyZoomedImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenInEditor;
        private System.Windows.Forms.ToolStripMenuItem tsmiInvertColor;
        private System.Windows.Forms.ToolStripMenuItem tsmiConvertToGray;
        private System.Windows.Forms.ToolStripMenuItem tsmiRotateLeft;
        private System.Windows.Forms.ToolStripMenuItem tsmiRotateRight;
        private System.Windows.Forms.ToolStripMenuItem tsmiFlipHorizontal;
        private System.Windows.Forms.ToolStripMenuItem tsmiFlipVertical;
    }
}