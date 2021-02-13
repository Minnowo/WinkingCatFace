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
            this.ResizePanel = new System.Windows.Forms.Panel();
            this.ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allowResizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oCRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.destroyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.destroyAllClipsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clipDisplayPictureBox = new System.Windows.Forms.PictureBox();
            this.ContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clipDisplayPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ResizePanel
            // 
            this.ResizePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ResizePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ResizePanel.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.ResizePanel.Location = new System.Drawing.Point(614, 360);
            this.ResizePanel.Name = "ResizePanel";
            this.ResizePanel.Size = new System.Drawing.Size(200, 100);
            this.ResizePanel.TabIndex = 1;
            // 
            // ContextMenu
            // 
            this.ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.allowResizeToolStripMenuItem,
            this.oCRToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator2,
            this.destroyToolStripMenuItem,
            this.toolStripSeparator1,
            this.destroyAllClipsToolStripMenuItem});
            this.ContextMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.ContextMenu.Name = "contextMenuStrip1";
            this.ContextMenu.Size = new System.Drawing.Size(197, 266);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.Clipboard_2_icon;
            this.copyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // allowResizeToolStripMenuItem
            // 
            this.allowResizeToolStripMenuItem.CheckOnClick = true;
            this.allowResizeToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.layer_resize_icon;
            this.allowResizeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.allowResizeToolStripMenuItem.Name = "allowResizeToolStripMenuItem";
            this.allowResizeToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+R";
            this.allowResizeToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.allowResizeToolStripMenuItem.Text = "AllowResize";
            // 
            // oCRToolStripMenuItem
            // 
            this.oCRToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.layer_shape_text_icon;
            this.oCRToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.oCRToolStripMenuItem.Name = "oCRToolStripMenuItem";
            this.oCRToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+T";
            this.oCRToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.oCRToolStripMenuItem.Text = "OCR";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.save_download_icon;
            this.saveToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(193, 6);
            // 
            // destroyToolStripMenuItem
            // 
            this.destroyToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.Error_Symbol_icon;
            this.destroyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.destroyToolStripMenuItem.Name = "destroyToolStripMenuItem";
            this.destroyToolStripMenuItem.ShortcutKeyDisplayString = "Esc";
            this.destroyToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.destroyToolStripMenuItem.Text = "Destroy";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // destroyAllClipsToolStripMenuItem
            // 
            this.destroyAllClipsToolStripMenuItem.Image = global::WinkingCat.ClipHelper.Properties.Resources.fire_icon;
            this.destroyAllClipsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.destroyAllClipsToolStripMenuItem.Name = "destroyAllClipsToolStripMenuItem";
            this.destroyAllClipsToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.destroyAllClipsToolStripMenuItem.Text = "DestroyAllClips";
            // 
            // clipDisplayPictureBox
            // 
            this.clipDisplayPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clipDisplayPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.clipDisplayPictureBox.ContextMenuStrip = this.ContextMenu;
            this.clipDisplayPictureBox.Location = new System.Drawing.Point(1, 1);
            this.clipDisplayPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.clipDisplayPictureBox.Name = "clipDisplayPictureBox";
            this.clipDisplayPictureBox.Size = new System.Drawing.Size(100, 50);
            this.clipDisplayPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.clipDisplayPictureBox.TabIndex = 0;
            this.clipDisplayPictureBox.TabStop = false;
            // 
            // ClipForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ResizePanel);
            this.Controls.Add(this.clipDisplayPictureBox);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ClipForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.ContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clipDisplayPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox clipDisplayPictureBox;
        private System.Windows.Forms.Panel ResizePanel;
        private System.Windows.Forms.ContextMenuStrip ContextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allowResizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oCRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem destroyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem destroyAllClipsToolStripMenuItem;
    }
}