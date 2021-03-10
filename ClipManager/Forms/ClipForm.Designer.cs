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
            this.cmMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContextMenu
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
            this.cmMain.Size = new System.Drawing.Size(197, 266);
            // 
            // copyToolStripMenuItem
            // 
            this.tsmiCopyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyDefaultContextMenuItem,
            this.tsmiCopyZoomScaledContextMenuItem});
            this.tsmiCopyToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.Clipboard_2_icon;
            this.tsmiCopyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiCopyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.tsmiCopyToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
            this.tsmiCopyToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiCopyToolStripMenuItem.Text = "Copy";
            // 
            // copyDefaultContextMenuItem
            // 
            this.tsmiCopyDefaultContextMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.Clipboard_2_icon;
            this.tsmiCopyDefaultContextMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiCopyDefaultContextMenuItem.Name = "copyDefaultContextMenuItem";
            this.tsmiCopyDefaultContextMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiCopyDefaultContextMenuItem.Text = "CopyDefault";
            // 
            // copyZoomScaledContextMenuItem
            // 
            this.tsmiCopyZoomScaledContextMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyZoomScaledContextMenuItem.Image")));
            this.tsmiCopyZoomScaledContextMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiCopyZoomScaledContextMenuItem.Name = "copyZoomScaledContextMenuItem";
            this.tsmiCopyZoomScaledContextMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiCopyZoomScaledContextMenuItem.Text = "CopyScaled";
            // 
            // allowResizeToolStripMenuItem
            // 
            this.tsmiAllowResizeToolStripMenuItem.CheckOnClick = true;
            this.tsmiAllowResizeToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.layer_resize_icon;
            this.tsmiAllowResizeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiAllowResizeToolStripMenuItem.Name = "allowResizeToolStripMenuItem";
            this.tsmiAllowResizeToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+R";
            this.tsmiAllowResizeToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiAllowResizeToolStripMenuItem.Text = "AllowResize";
            // 
            // oCRToolStripMenuItem
            // 
            this.tsmiOCRToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.layer_shape_text_icon;
            this.tsmiOCRToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiOCRToolStripMenuItem.Name = "oCRToolStripMenuItem";
            this.tsmiOCRToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+T";
            this.tsmiOCRToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiOCRToolStripMenuItem.Text = "OCR";
            // 
            // saveToolStripMenuItem
            // 
            this.tsmiSaveToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.save_download_icon;
            this.tsmiSaveToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiSaveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.tsmiSaveToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            this.tsmiSaveToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiSaveToolStripMenuItem.Text = "Save";
            // 
            // toolStripSeparator2
            // 
            this.tssToolStripSeparator2.Name = "toolStripSeparator2";
            this.tssToolStripSeparator2.Size = new System.Drawing.Size(193, 6);
            // 
            // destroyToolStripMenuItem
            // 
            this.tsmiDestroyToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.Error_Symbol_icon;
            this.tsmiDestroyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiDestroyToolStripMenuItem.Name = "destroyToolStripMenuItem";
            this.tsmiDestroyToolStripMenuItem.ShortcutKeyDisplayString = "Esc";
            this.tsmiDestroyToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiDestroyToolStripMenuItem.Text = "Destroy";
            // 
            // toolStripSeparator1
            // 
            this.tssToolStripSeparator1.Name = "toolStripSeparator1";
            this.tssToolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // destroyAllClipsToolStripMenuItem
            // 
            this.tsmiDestroyAllClipsToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.fire_icon;
            this.tsmiDestroyAllClipsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiDestroyAllClipsToolStripMenuItem.Name = "destroyAllClipsToolStripMenuItem";
            this.tsmiDestroyAllClipsToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.tsmiDestroyAllClipsToolStripMenuItem.Text = "DestroyAllClips";
            // 
            // ClipForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ContextMenuStrip = this.cmMain;
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
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
    }
}