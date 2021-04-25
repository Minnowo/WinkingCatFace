using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using WinkingCat.HelperLibs;
using WinkingCat.ClipHelper;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using WinkingCat.Uploaders;
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
        private ToolStripMenuItem toolStripMenuItemRemoveFromList;
        private ToolStripSeparator toolStripSeparator3;

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
        public bool autoFillColumn { get; set; } = true;
        //public bool showingContextMenu { get; private set; } = false;
        public NoCheckboxListView()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.EnableNotifyMessage, true);
            InitializeComponent();

            FullRowSelect = true;
            PathHelper.UpdateRelativePaths();
            if (File.Exists(PathHelper.loadedItemsPath))
            {
                const Int32 BufferSize = 128;
                using (var fileStream = File.OpenRead(PathHelper.loadedItemsPath))
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String line;
                    FileInfo info;
                    string[] row;
                    Size dimensions;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (File.Exists(line))
                        {
                            info = new FileInfo(line);
                            dimensions = ImageHelper.GetImageDimensionsFile(info.FullName);

                            if (dimensions == Size.Empty)
                                row = new string[4]{ info.Extension, 
                                    "", 
                                    Helpers.SizeSuffix(info.Length), 
                                    File.GetLastWriteTime(info.FullName).ToString()};
                            else
                                row = new string[4] { info.Extension, 
                                    $"{dimensions.Width}, " +
                                    $"{dimensions.Height}", 
                                    Helpers.SizeSuffix(info.Length), File.GetLastWriteTime(info.FullName).ToString() };

                            ListViewItem item = new ListViewItem() { Text = info.Name, Tag = info.FullName };
                            item.SubItems.AddRange(row);

                            if(this.Items.Count <= 0)
                            {
                                this.Items.Add(item);
                            }
                            else
                            {
                                this.Items.Insert(0, item);
                            }
                        }
                    }
                }
            }
            else
            {
                StreamWriter w = File.AppendText(PathHelper.loadedItemsPath);
                w.Close();
                w.Dispose();
            }

            #region cmsMain Events
            toolStripMenuItemAlwaysOnTop.Checked = MainFormSettings.alwaysOnTop;

            cmsMain.Opening += ContextMenuStrip_Opening;
            //cmsMain.Closing += CmsMain_Closing;
            toolStripMenuItemOCR.Click += ToolStripMenuItemOCR_Click;
            toolStripMenuItemDelete.Click += ToolStripMenuItemDelete_Click;
            toolStripMenuItemAlwaysOnTop.Click += ToolStripMenuItemAlwaysOnTop_Click;
            toolStripMenuItemRemoveFromList.Click += ToolStripMenuItemRemoveFromList_Click;

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
            toolStripMenuItemPath,
            toolStripMenuItemRemoveFromList};
            ApplicationStyles.UpdateStylesEvent += ApplicationStyles_UpdateSylesEvent;
            UpdateTheme();
            

        }
        private bool supressIndexChangeEvent = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    supressIndexChangeEvent = false;
                    break;
                case MouseButtons.Right:
                    supressIndexChangeEvent = true;
                    break;
            }
                
            base.OnMouseDown(e);
        }

        protected override void OnItemSelectionChanged(ListViewItemSelectionChangedEventArgs e)
        {
            if (!supressIndexChangeEvent)
            {
                base.OnItemSelectionChanged(e);
            }
        }

        private void ApplicationStyles_UpdateSylesEvent(object sender, EventArgs e)
        {
            UpdateTheme();
        }

        public void UpdateTheme()
        {
            this.SupportCustomTheme();
            this.BackColor = ApplicationStyles.currentStyle.mainFormStyle.backgroundColor;
            this.ForeColor = ApplicationStyles.currentStyle.mainFormStyle.textColor;

            cmsMain.Renderer = new ToolStripCustomRenderer();
            cmsMain.Opacity = ApplicationStyles.currentStyle.mainFormStyle.contextMenuOpacity;

            cmsOpen.Renderer = new ToolStripCustomRenderer();
            cmsOpen.Opacity = ApplicationStyles.currentStyle.mainFormStyle.contextMenuOpacity;

            cmsCopy.Renderer = new ToolStripCustomRenderer();
            cmsCopy.Opacity = ApplicationStyles.currentStyle.mainFormStyle.contextMenuOpacity;
            Refresh();
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
                        using (Bitmap img = ImageHelper.LoadImage(Items[SelectedIndex].Tag.ToString()))
                        {
                            ClipOptions ops = new ClipOptions();
                            ops.location = Program.MainForm.Location;
                            ops.date = DateTime.Now;
                            ops.uuid = Guid.NewGuid().ToString();
                            ops.filePath = Items[SelectedIndex].Tag.ToString();

                            ClipManager.CreateClip(img, ops);
                        }
                        GC.Collect(); // free memory from the stream of LoadImage
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
                string path = Items[SelectedIndex].Tag.ToString();
                if (File.Exists(path))
                {
                    ClipboardHelper.CopyImageFromFile(path, false, false);
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
                    Size dims = ImageHelper.GetImageDimensionsFile(Items[SelectedIndex].Tag.ToString());
                    ClipboardHelper.CopyStringDefault(
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
                    ClipboardHelper.CopyFile(Items[SelectedIndex].Tag.ToString());
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
                    ClipboardHelper.CopyStringDefault(Items[SelectedIndex].Text.ToString());
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
                    ClipboardHelper.CopyStringDefault(Items[SelectedIndex].Tag.ToString());
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
                    ClipboardHelper.CopyStringDefault(Path.GetDirectoryName(Items[SelectedIndex].Tag.ToString()));
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
            OCRForm form = new OCRForm(Items[SelectedIndex].Tag.ToString());
            form.Owner = Program.MainForm;
            form.TopMost = MainFormSettings.alwaysOnTop;
            form.Show();
            
        }
        

        private async void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            if (SelectedIndex != -1)
            {
                foreach (ListViewItem item in SelectedItems)
                {
                    if (PathHelper.DeleteFile(item.Tag.ToString()))
                    {
                        Items.Remove(item);
                        SelectedIndex = -1;
                    }
                }
                await ListViewDumpAsync((ListViewItem[])this.Items.OfType<ListViewItem>().ToArray().Clone());
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

        private async void ToolStripMenuItemRemoveFromList_Click(object sender, EventArgs e)
        {
            if(SelectedItems.Count > 0)
            {
                foreach(ListViewItem item in SelectedItems)
                {
                    this.Items.Remove(item);
                }
                await ListViewDumpAsync((ListViewItem[])this.Items.OfType<ListViewItem>().ToArray().Clone());
            }
        }
        #endregion

        public async Task ListViewDumpAsync(ListViewItem[] items)
        {
            if (File.Exists(PathHelper.loadedItemsPath))
            {
                await Task.Run(() =>
                {
                    System.IO.File.WriteAllText(PathHelper.loadedItemsPath, "");
                    using (StreamWriter w = File.AppendText(PathHelper.loadedItemsPath))
                    {
                        foreach (ListViewItem item in items.Reverse())
                        {
                            w.WriteLine(item.Tag.ToString());
                        }
                    }
                });
            }
        }

        public void AddItem(ListViewItem item)
        {
            using(StreamWriter w = File.AppendText(PathHelper.loadedItemsPath))
            {
                w.WriteLine(item.Tag.ToString());
            }
            this.Items.Add(item);
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        public void InsertItem(int index, ListViewItem item)
        {
            using (StreamWriter w = File.AppendText(PathHelper.loadedItemsPath))
            {
                w.WriteLine(item.Tag.ToString());
            }
            this.Items.Insert(index, item);
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

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

        protected override void WndProc(ref Message m)
        {
            if (autoFillColumn && m.Msg == (int)WindowsMessages.PAINT && !DesignMode)
            {
                if (Columns.Count != 0) // sizes the columns to fill the rest of the list box
                { 
                    this.Columns[this.Columns.Count - 1].Width = -2;
                }
            }
            base.WndProc(ref m);
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
            this.toolStripMenuItemRemoveFromList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
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
            this.toolStripMenuItemRemoveFromList,
            this.toolStripSeparator3,
            this.toolStripMenuItemDelete,
            this.toolStripMenuItemAlwaysOnTop});
            this.cmsMain.Name = "cmsMain";
            this.cmsMain.Size = new System.Drawing.Size(197, 170);
            // 
            // toolStripMenuItemOpen
            // 
            this.toolStripMenuItemOpen.DropDown = this.cmsOpen;
            this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            this.toolStripMenuItemOpen.Size = new System.Drawing.Size(196, 22);
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
            this.toolStripMenuItemCopy.Size = new System.Drawing.Size(196, 22);
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
            this.toolStripMenuItemUpload.Size = new System.Drawing.Size(196, 22);
            this.toolStripMenuItemUpload.Text = "Upload";
            // 
            // toolStripMenuItemOCR
            // 
            this.toolStripMenuItemOCR.Name = "toolStripMenuItemOCR";
            this.toolStripMenuItemOCR.Size = new System.Drawing.Size(196, 22);
            this.toolStripMenuItemOCR.Text = "OCR";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // toolStripMenuItemRemoveFromList
            // 
            this.toolStripMenuItemRemoveFromList.Name = "toolStripMenuItemRemoveFromList";
            this.toolStripMenuItemRemoveFromList.Size = new System.Drawing.Size(196, 22);
            this.toolStripMenuItemRemoveFromList.Text = "Remove Selected Items";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(193, 6);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(196, 22);
            this.toolStripMenuItemDelete.Text = "Delete File Locally";
            // 
            // toolStripMenuItemAlwaysOnTop
            // 
            this.toolStripMenuItemAlwaysOnTop.CheckOnClick = true;
            this.toolStripMenuItemAlwaysOnTop.Name = "toolStripMenuItemAlwaysOnTop";
            this.toolStripMenuItemAlwaysOnTop.Size = new System.Drawing.Size(196, 22);
            this.toolStripMenuItemAlwaysOnTop.Text = "Always On Top";
            // 
            // NoCheckboxListView
            // 
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