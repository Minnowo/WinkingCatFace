using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using WinkingCat.HelperLibs;
using WinkingCat.ClipHelper;

namespace WinkingCat
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
        private ToolStripMenuItem toolStripMenuItemAlwaysOnTop;

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

            #region cmsMain Events
            toolStripMenuItemAlwaysOnTop.Checked = MainFormSettings.alwaysOnTop;

            cmsMain.Opening += ContextMenuStrip_Opening;
            toolStripMenuItemOCR.Click += ToolStripMenuItemOCR_Click;
            toolStripMenuItemDelete.Click += ToolStripMenuItemDelete_Click;
            toolStripMenuItemAlwaysOnTop.Click += ToolStripMenuItemAlwaysOnTop_Click;

            #region cmsOpen Events
            toolStripMenuItemOpenFile.Click += ToolStripMenuItemOpenFile_Click;
            toolStripMenuItemOpenFolder.Click += ToolStripMenuItemOpenFolder_Click;
            toolStripMenuItemOpenAsClip.Click += ToolStripMenuItemOpenAsClip_Click;
            #endregion

            #region cmsCopy Events
            toolStripMenuItemCopyImage.Click += ToolStripMenuItemCopyImage_Click;
            toolStripMenuItemCopyDimensions.Click += ToolStripMenuItemCopyDimensions_Click;
            toolStripMenuItemFile.Click += ToolStripMenuItemCopyFile_Click;
            toolStripMenuItemFileName.Click += ToolStripMenuItemCopyFileName_Click;
            toolStripMenuItemPath.Click += ToolStripMenuItemCopyPath_Click;
            toolStripMenuItemDirectory.Click += ToolStripMenuItemCopyDirectory_Click;
            #endregion

            #endregion

            MouseDoubleClick += MouseDoubleClick_Event;

        }
        #region cmsMain

        #region cmsOpen
        private void ToolStripMenuItemOpenFile_Click(object sender, EventArgs e)
        {
            if (SelectedIndex != -1)
            {
                if (File.Exists(Items[SelectedIndex].Tag.ToString()))
                {
                    PathHelper.OpenWithDefaultProgram(Items[SelectedIndex].Tag.ToString());
                }
                else
                {
                    MessageBox.Show("The file path has changed or the file has been deleted");
                    Items.Remove(Items[SelectedIndex]);
                    SelectedIndex = -1;
                }
            }
        }

        private void ToolStripMenuItemOpenFolder_Click(object sender, EventArgs e)
        {
            if (SelectedIndex != -1)
            {
                if (File.Exists(Items[SelectedIndex].Tag.ToString()))
                {
                    PathHelper.OpenExplorerAtLocation(Items[SelectedIndex].Tag.ToString());
                }
                else if (Directory.Exists(Path.GetDirectoryName(Items[SelectedIndex].Tag.ToString())))
                {
                    PathHelper.OpenExplorerAtLocation(Path.GetDirectoryName(Items[SelectedIndex].Tag.ToString()));
                    MessageBox.Show("The file path has changed or the file has been deleted");
                    Items.Remove(Items[SelectedIndex]);
                    SelectedIndex = -1;
                }
                else 
                {
                    MessageBox.Show("The file path has changed or the file has been deleted");
                    Items.Remove(Items[SelectedIndex]);
                    SelectedIndex = -1;
                }
            }
        }

        private void ToolStripMenuItemOpenAsClip_Click(object sender, EventArgs e)
        {
            if (SelectedIndex != -1)
            {
                if (File.Exists(Items[SelectedIndex].Tag.ToString()))
                {
                    try
                    {
                        Image img = Bitmap.FromFile(Items[SelectedIndex].Tag.ToString());
                        Point p = ScreenHelper.GetCursorPosition();
                        ClipOptions ops = new ClipOptions() { location = new Point(p.X - img.Width / 2, p.Y - img.Height / 2) };

                        ClipManager.CreateClip(img, ops);
                        img.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteException(ex);
                        MessageBox.Show("The file is either not an image file or is corrupt");
                    }
                }
                else
                {
                    MessageBox.Show("The file path has changed or the file has been deleted");
                    Items.Remove(Items[SelectedIndex]);
                    SelectedIndex = -1;
                }
            }
        }
        #endregion

        #region cmsCopy
        private void ToolStripMenuItemCopyImage_Click(object sender, EventArgs e)
        {
            if (SelectedIndex != -1)
            {
                if (File.Exists(Items[SelectedIndex].Tag.ToString()))
                {
                    try
                    {
                        Image img = Bitmap.FromFile(Items[SelectedIndex].Tag.ToString());
                        ClipboardHelpers.CopyImageDefault(img);
                        img.Dispose();
                    }
                    catch(Exception ex)
                    {
                        Logger.WriteException(ex);
                        MessageBox.Show("The file is either not an image file or is corrupt");
                    }
                    
                }
                else
                {
                    MessageBox.Show("The file path has changed or the file has been deleted");
                    Items.Remove(Items[SelectedIndex]);
                    SelectedIndex = -1;
                }
            }
        }

        private void ToolStripMenuItemCopyDimensions_Click(object sender, EventArgs e)
        {
            if (SelectedIndex != -1)
            {
                if (File.Exists(Items[SelectedIndex].Tag.ToString()))
                {
                    Size dims = Helpers.GetImageDimensionsFile(Items[SelectedIndex].Tag.ToString());
                    ClipboardHelpers.CopyStringDefault(
                        string.Format("{0} x {1}", dims.Width, dims.Height));
                }
                else
                {
                    MessageBox.Show("The file path has changed or the file has been deleted");
                    Items.Remove(Items[SelectedIndex]);
                    SelectedIndex = -1;
                }
            }
        }

        private void ToolStripMenuItemCopyFile_Click(object sender, EventArgs e)
        {
            if (SelectedIndex != -1)
            {
                if (File.Exists(Items[SelectedIndex].Tag.ToString()))
                {
                    ClipboardHelpers.CopyFile(Items[SelectedIndex].Tag.ToString());
                }
                else
                {
                    MessageBox.Show("The file path has changed or the file has been deleted");
                    Items.Remove(Items[SelectedIndex]);
                    SelectedIndex = -1;
                }
            }
        }

        private void ToolStripMenuItemCopyFileName_Click(object sender, EventArgs e)
        {
            if (SelectedIndex != -1)
            {
                if (File.Exists(Items[SelectedIndex].Tag.ToString()))
                {
                    ClipboardHelpers.CopyStringDefault(Items[SelectedIndex].Text.ToString());
                }
                else
                {
                    MessageBox.Show("The file path has changed or the file has been deleted");
                    Items.Remove(Items[SelectedIndex]);
                    SelectedIndex = -1;
                }
            }
        }

        private void ToolStripMenuItemCopyPath_Click(object sender, EventArgs e)
        {
            if (SelectedIndex != -1)
            {
                if (File.Exists(Items[SelectedIndex].Tag.ToString()))
                {
                    ClipboardHelpers.CopyStringDefault(Items[SelectedIndex].Tag.ToString());
                }
                else
                {
                    MessageBox.Show("The file path has changed or the file has been deleted");
                    Items.Remove(Items[SelectedIndex]);
                    SelectedIndex = -1;
                }
            }
        }

        private void ToolStripMenuItemCopyDirectory_Click(object sender, EventArgs e)
        {
            if (SelectedIndex != -1)
            {
                if (Directory.Exists(Path.GetDirectoryName(Items[SelectedIndex].Tag.ToString())))
                {
                    ClipboardHelpers.CopyStringDefault(Path.GetDirectoryName(Items[SelectedIndex].Tag.ToString()));
                }
                else
                {
                    MessageBox.Show("The file path has changed or the file has been deleted");
                    Items.Remove(Items[SelectedIndex]);
                    SelectedIndex = -1;
                }
            }
        }
        #endregion

        private void ToolStripMenuItemOCR_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            if (SelectedIndex != -1)
            {
                if (PathHelper.DeleteFile(Items[SelectedIndex].Tag.ToString()))
                {
                    Items.Remove(Items[SelectedIndex]);
                    SelectedIndex = -1;
                }
            }
        }

        private void ToolStripMenuItemAlwaysOnTop_Click(object sender, EventArgs e)
        {
            if (toolStripMenuItemAlwaysOnTop.Checked)
            {
                MainFormSettings.alwaysOnTop = true;
                MainFormSettings.OnSettingsChangedEvent();
            }
            else
            {
                MainFormSettings.alwaysOnTop = false;
                MainFormSettings.OnSettingsChangedEvent();
            }
        }

        #endregion
        private void MouseDoubleClick_Event(object sender, MouseEventArgs e)
        {
            if (SelectedIndex != -1)
            {
                if (File.Exists(Items[SelectedIndex].Tag.ToString()))
                {
                    PathHelper.OpenWithDefaultProgram(Items[SelectedIndex].Tag.ToString());
                }
                else 
                {
                    MessageBox.Show("The file path has changed or the file has been deleted");
                    Items.Remove(Items[SelectedIndex]);
                    SelectedIndex = -1;
                }                    
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
            this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsOpen = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpenAsClip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsCopy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCopyImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCopyDimensions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemFileName = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPath = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOCR = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripMenuItemDelete,
            this.toolStripMenuItemAlwaysOnTop});
            this.cmsMain.Name = "cmsMain";
            this.cmsMain.Size = new System.Drawing.Size(153, 142);
            // 
            // toolStripMenuItemOpen
            // 
            this.toolStripMenuItemOpen.DropDown = this.cmsOpen;
            this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            this.toolStripMenuItemOpen.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemOpen.Text = "Open";
            // 
            // cmsOpen
            // 
            this.cmsOpen.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpenFile,
            this.toolStripMenuItemOpenFolder,
            this.toolStripMenuItemOpenAsClip});
            this.cmsOpen.Name = "cmsOpen";
            this.cmsOpen.OwnerItem = this.toolStripMenuItemOpen;
            this.cmsOpen.Size = new System.Drawing.Size(144, 70);
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
            // toolStripMenuItemCopy
            // 
            this.toolStripMenuItemCopy.DropDown = this.cmsCopy;
            this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
            this.toolStripMenuItemCopy.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemCopy.Text = "Copy";
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
            this.cmsCopy.OwnerItem = this.toolStripMenuItemCopy;
            this.cmsCopy.Size = new System.Drawing.Size(137, 142);
            this.cmsCopy.Text = "Copy";
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
            // toolStripMenuItemPath
            // 
            this.toolStripMenuItemPath.Name = "toolStripMenuItemPath";
            this.toolStripMenuItemPath.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItemPath.Text = "File Path";
            // 
            // toolStripMenuItemDirectory
            // 
            this.toolStripMenuItemDirectory.Name = "toolStripMenuItemDirectory";
            this.toolStripMenuItemDirectory.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItemDirectory.Text = "Directory";
            // 
            // toolStripMenuItemUpload
            // 
            this.toolStripMenuItemUpload.Name = "toolStripMenuItemUpload";
            this.toolStripMenuItemUpload.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemUpload.Text = "Upload";
            // 
            // toolStripMenuItemOCR
            // 
            this.toolStripMenuItemOCR.Name = "toolStripMenuItemOCR";
            this.toolStripMenuItemOCR.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemOCR.Text = "OCR";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemDelete.Text = "Delete File Locally";
            // 
            // toolStripMenuItemAlwaysOnTop
            // 
            this.toolStripMenuItemAlwaysOnTop.CheckOnClick = true;
            this.toolStripMenuItemAlwaysOnTop.Name = "toolStripMenuItemAlwaysOnTop";
            this.toolStripMenuItemAlwaysOnTop.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemAlwaysOnTop.Text = "Always On Top";
            // 
            // NoCheckboxListView
            // 
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
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.cmsMain;
            this.FullRowSelect = true;
            this.View = System.Windows.Forms.View.Details;
            this.cmsMain.ResumeLayout(false);
            this.cmsOpen.ResumeLayout(false);
            this.cmsCopy.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}