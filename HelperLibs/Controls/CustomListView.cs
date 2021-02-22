using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{
    public class NoCheckboxListView : ListView
    {
        private ContextMenuStrip cmsMain;
        private IContainer components;
        private ToolStripMenuItem toolStripMenuItemCopy;
        private ToolStripMenuItem toolStripMenuItemOpen;
        private ToolStripMenuItem toolStripMenuItemUpload;
        private ToolStripMenuItem toolStripMenuItemOCR;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem toolStripMenuItemDelete;
        private ContextMenuStrip cmsOpen;
        private ContextMenuStrip cmsCopy;
        private ToolStripMenuItem toolStripMenuItemOpenFile;
        private ToolStripMenuItem toolStripMenuItemOpenFolder;
        private ToolStripMenuItem toolStripMenuItemOpenAsClip;
        private ToolStripMenuItem toolStripMenuItemCopyImage;
        private ToolStripMenuItem toolStripMenuItemCopyDimensions;
        private ToolStripMenuItem toolStripMenuItemFile;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem toolStripMenuItemFileName;
        private ToolStripMenuItem toolStripMenuItemPath;
        private ToolStripMenuItem toolStripMenuItemDirectory;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get
            {
                if (SelectedIndices.Count > 0)
                {
                    return SelectedIndices[0];
                }

                return -1;
            }
            set
            {
                UnselectAll();

                if (value > -1)
                {
                    ListViewItem lvi = Items[value];
                    lvi.EnsureVisible();
                    lvi.Selected = true;
                }
            }
        }

        ToolStripMenuItem[] buttonOnlyItems;

        public NoCheckboxListView()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.EnableNotifyMessage, true);
            InitializeComponent();
            

            cmsMain.Opening += ContextMenuStrip_Opening;
            MouseDoubleClick += MouseDoubleClick_Event;

        }
        #region cmsMain

        #region cmsOpen

        #endregion

        #region cmsCopy

        #endregion

        #endregion
        private void MouseDoubleClick_Event(object sender, MouseEventArgs e)
        {
            if (SelectedIndex != -1)
            {
                PathHelper.OpenWithDefaultProgram(Items[SelectedIndex].Tag.ToString());
            }
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (SelectedIndex != -1) 
            {
                foreach (ToolStripMenuItem a in buttonOnlyItems)
                {
                    a.Enabled = true;
                }
            } 
            else
            {
                foreach (ToolStripMenuItem a in buttonOnlyItems)
                {
                    a.Enabled = false;
                }
            }
        }

        public void UnselectAll()
        {
            if (MultiSelect)
            {
                SelectedItems.Clear();
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOCR = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsOpen = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsCopy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpenAsClip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCopyImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCopyDimensions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemFileName = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPath = new System.Windows.Forms.ToolStripMenuItem();
            buttonOnlyItems = new ToolStripMenuItem[] { 
            toolStripMenuItemDelete,
            toolStripMenuItemOCR,
            toolStripMenuItemUpload,
            toolStripMenuItemOpenFile,
            toolStripMenuItemOpenFolder,
            toolStripMenuItemOpenAsClip,
            toolStripMenuItemCopyImage,
            toolStripMenuItemCopyDimensions,
            toolStripMenuItemFile,
            toolStripMenuItemFileName,
            toolStripMenuItemDirectory,
            toolStripMenuItemPath};
            this.cmsMain.SuspendLayout();
            this.cmsOpen.SuspendLayout();
            this.cmsCopy.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsMain
            // 
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpen,
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemUpload,
            this.toolStripMenuItemOCR,
            this.toolStripSeparator1,
            this.toolStripMenuItemDelete});
            this.cmsMain.Name = "cmsMain";
            this.cmsMain.Size = new System.Drawing.Size(213, 120);
            // 
            // toolStripMenuItemCopy
            // 
            this.toolStripMenuItemCopy.DropDown = this.cmsCopy;
            this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
            this.toolStripMenuItemCopy.Size = new System.Drawing.Size(212, 22);
            this.toolStripMenuItemCopy.Text = "Copy";
            // 
            // toolStripMenuItemOpen
            // 
            this.toolStripMenuItemOpen.DropDown = this.cmsOpen;
            this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            this.toolStripMenuItemOpen.Size = new System.Drawing.Size(212, 22);
            this.toolStripMenuItemOpen.Text = "Open";
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(212, 22);
            this.toolStripMenuItemDelete.Text = "Delete";
            // 
            // toolStripMenuItemOCR
            // 
            this.toolStripMenuItemOCR.Name = "toolStripMenuItemOCR";
            this.toolStripMenuItemOCR.Size = new System.Drawing.Size(212, 22);
            this.toolStripMenuItemOCR.Text = "OCR";
            // 
            // toolStripMenuItemUpload
            // 
            this.toolStripMenuItemUpload.Name = "toolStripMenuItemUpload";
            this.toolStripMenuItemUpload.Size = new System.Drawing.Size(212, 22);
            this.toolStripMenuItemUpload.Text = "Upload";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(209, 6);
            // 
            // cmsOpen
            // 
            this.cmsOpen.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpenFile,
            this.toolStripMenuItemOpenFolder,
            this.toolStripMenuItemOpenAsClip});
            this.cmsOpen.Name = "cmsOpen";
            this.cmsOpen.Size = new System.Drawing.Size(144, 70);
            // 
            // cmsCopy
            // 
            this.cmsCopy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopyImage,
            this.toolStripMenuItemCopyDimensions,
            this.toolStripMenuItemFile,
            this.toolStripSeparator2,
            this.toolStripMenuItemFileName,
            this.toolStripMenuItemPath,
            this.toolStripMenuItemDirectory});
            this.cmsCopy.Name = "cmsCopy";
            this.cmsCopy.Size = new System.Drawing.Size(137, 142);
            this.cmsCopy.Text = "Copy";
            // 
            // toolStripMenuItemOpenFile
            // 
            this.toolStripMenuItemOpenFile.Name = "toolStripMenuItemOpenFile";
            this.toolStripMenuItemOpenFile.Size = new System.Drawing.Size(143, 22);
            this.toolStripMenuItemOpenFile.Text = "Open File";
            // 
            // toolStripMenuItemOpenFolder
            // 
            this.toolStripMenuItemOpenFolder.Name = "toolStripMenuItemOpenFolder";
            this.toolStripMenuItemOpenFolder.Size = new System.Drawing.Size(143, 22);
            this.toolStripMenuItemOpenFolder.Text = "Open Folder";
            // 
            // toolStripMenuItemOpenAsClip
            // 
            this.toolStripMenuItemOpenAsClip.Name = "toolStripMenuItemOpenAsClip";
            this.toolStripMenuItemOpenAsClip.Size = new System.Drawing.Size(143, 22);
            this.toolStripMenuItemOpenAsClip.Text = "Open As Clip";
            // 
            // toolStripMenuItemCopyImage
            // 
            this.toolStripMenuItemCopyImage.Name = "toolStripMenuItemCopyImage";
            this.toolStripMenuItemCopyImage.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItemCopyImage.Text = "Image";
            // 
            // toolStripMenuItemCopyDimensions
            // 
            this.toolStripMenuItemCopyDimensions.Name = "toolStripMenuItemCopyDimensions";
            this.toolStripMenuItemCopyDimensions.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItemCopyDimensions.Text = "Dimensions";
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItemFile.Text = "File";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(133, 6);
            // 
            // toolStripMenuItemFileName
            // 
            this.toolStripMenuItemFileName.Name = "toolStripMenuItemFileName";
            this.toolStripMenuItemFileName.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItemFileName.Text = "File Name";
            // 
            // toolStripMenuItemDirectory
            // 
            this.toolStripMenuItemDirectory.Name = "toolStripMenuItemDirectory";
            this.toolStripMenuItemDirectory.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItemDirectory.Text = "Directory";
            // 
            // toolStripMenuItemPath
            // 
            this.toolStripMenuItemPath.Name = "toolStripMenuItemPath";
            this.toolStripMenuItemPath.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItemPath.Text = "File Path";
            // 
            // NoCheckboxListView
            // 
            this.View = View.Details;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.FullRowSelect = true;
            this.CheckBoxes = false;
            this.ContextMenuStrip = this.cmsMain;
            this.cmsMain.ResumeLayout(false);
            this.cmsOpen.ResumeLayout(false);
            this.cmsCopy.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}