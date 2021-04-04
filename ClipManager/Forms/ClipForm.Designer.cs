namespace WinkingCat.ClipHelper
{
    partial class ClipForm
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
            cmMain?.Dispose();
            image?.Dispose();
            zoomedImage?.Dispose();
            zdbZoomedImageDisplay?.Dispose();
            
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClipForm));
            this.cmMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyDefaultContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyZoomScaledContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAllowResizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOCRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tssToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDestroyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tssToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDestroyAllClipsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zdbZoomedImageDisplay = new WinkingCat.HelperLibs.ZoomDrawingBoard();
            this.tsmiCopyZoomedImage = new System.Windows.Forms.ToolStripMenuItem();
            this.cmMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmMain
            // 
            this.cmMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyToolStripMenuItem,
            this.tsmiAllowResizeToolStripMenuItem,
            this.tsmiOCRToolStripMenuItem,
            this.tsmiSaveToolStripMenuItem,
            this.tssToolStripSeparator2,
            this.tsmiDestroyToolStripMenuItem,
            this.tssToolStripSeparator1,
            this.tsmiDestroyAllClipsToolStripMenuItem});
            this.cmMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.cmMain.Name = "contextMenuStrip1";
            this.cmMain.Size = new System.Drawing.Size(227, 272);
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
            this.tsmiCopyToolStripMenuItem.Size = new System.Drawing.Size(226, 38);
            this.tsmiCopyToolStripMenuItem.Text = "Copy";
            // 
            // tsmiCopyDefaultContextMenuItem
            // 
            this.tsmiCopyDefaultContextMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.Clipboard_2_icon;
            this.tsmiCopyDefaultContextMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiCopyDefaultContextMenuItem.Name = "tsmiCopyDefaultContextMenuItem";
            this.tsmiCopyDefaultContextMenuItem.Size = new System.Drawing.Size(236, 38);
            this.tsmiCopyDefaultContextMenuItem.Text = "CopyDefault";
            // 
            // tsmiCopyZoomScaledContextMenuItem
            // 
            this.tsmiCopyZoomScaledContextMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCopyZoomScaledContextMenuItem.Image")));
            this.tsmiCopyZoomScaledContextMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiCopyZoomScaledContextMenuItem.Name = "tsmiCopyZoomScaledContextMenuItem";
            this.tsmiCopyZoomScaledContextMenuItem.Size = new System.Drawing.Size(236, 38);
            this.tsmiCopyZoomScaledContextMenuItem.Text = "CopyScaled";
            // 
            // tsmiAllowResizeToolStripMenuItem
            // 
            this.tsmiAllowResizeToolStripMenuItem.CheckOnClick = true;
            this.tsmiAllowResizeToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.layer_resize_icon;
            this.tsmiAllowResizeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiAllowResizeToolStripMenuItem.Name = "tsmiAllowResizeToolStripMenuItem";
            this.tsmiAllowResizeToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+R";
            this.tsmiAllowResizeToolStripMenuItem.Size = new System.Drawing.Size(226, 38);
            this.tsmiAllowResizeToolStripMenuItem.Text = "AllowResize";
            // 
            // tsmiOCRToolStripMenuItem
            // 
            this.tsmiOCRToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.layer_shape_text_icon;
            this.tsmiOCRToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiOCRToolStripMenuItem.Name = "tsmiOCRToolStripMenuItem";
            this.tsmiOCRToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+T";
            this.tsmiOCRToolStripMenuItem.Size = new System.Drawing.Size(226, 38);
            this.tsmiOCRToolStripMenuItem.Text = "OCR";
            // 
            // tsmiSaveToolStripMenuItem
            // 
            this.tsmiSaveToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.save_download_icon;
            this.tsmiSaveToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiSaveToolStripMenuItem.Name = "tsmiSaveToolStripMenuItem";
            this.tsmiSaveToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            this.tsmiSaveToolStripMenuItem.Size = new System.Drawing.Size(226, 38);
            this.tsmiSaveToolStripMenuItem.Text = "Save";
            // 
            // tssToolStripSeparator2
            // 
            this.tssToolStripSeparator2.Name = "tssToolStripSeparator2";
            this.tssToolStripSeparator2.Size = new System.Drawing.Size(223, 6);
            // 
            // tsmiDestroyToolStripMenuItem
            // 
            this.tsmiDestroyToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.Error_Symbol_icon;
            this.tsmiDestroyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiDestroyToolStripMenuItem.Name = "tsmiDestroyToolStripMenuItem";
            this.tsmiDestroyToolStripMenuItem.ShortcutKeyDisplayString = "Esc";
            this.tsmiDestroyToolStripMenuItem.Size = new System.Drawing.Size(226, 38);
            this.tsmiDestroyToolStripMenuItem.Text = "Destroy";
            // 
            // tssToolStripSeparator1
            // 
            this.tssToolStripSeparator1.Name = "tssToolStripSeparator1";
            this.tssToolStripSeparator1.Size = new System.Drawing.Size(223, 6);
            // 
            // tsmiDestroyAllClipsToolStripMenuItem
            // 
            this.tsmiDestroyAllClipsToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.fire_icon;
            this.tsmiDestroyAllClipsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiDestroyAllClipsToolStripMenuItem.Name = "tsmiDestroyAllClipsToolStripMenuItem";
            this.tsmiDestroyAllClipsToolStripMenuItem.Size = new System.Drawing.Size(226, 38);
            this.tsmiDestroyAllClipsToolStripMenuItem.Text = "DestroyAllClips";
            // 
            // zdbZoomedImageDisplay
            // 
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
            // tsmiCopyZoomedImage
            // 
            this.tsmiCopyZoomedImage.Image = global::WinkingCat.ClipHelper.Properties.Resources.layer_resize_icon;
            this.tsmiCopyZoomedImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiCopyZoomedImage.Name = "tsmiCopyZoomedImage";
            this.tsmiCopyZoomedImage.Size = new System.Drawing.Size(236, 38);
            this.tsmiCopyZoomedImage.Text = "CopyZoomed";
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
    }
}