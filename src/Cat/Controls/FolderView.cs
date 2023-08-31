using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinkingCat.HelperLibs;
using WinkingCat.Settings;

namespace WinkingCat.Controls
{
    public partial class FolderView : UserControl
    {
        public bool AskConfirmationOnDelete { get; set; } = true;
        public int FileSortOrder
        {
            get { return _FolderWatcher.SortOrderFile; }
            set { _FolderWatcher.SortOrderFile = value; }
        }

        public int FolderSortOrder
        {
            get { return _FolderWatcher.SortOrderFolder; }
            set { _FolderWatcher.SortOrderFolder = value; }
        }

        public int SelectedIndex
        {
            get
            {
                if (ListView_.SelectedIndex1 == -1)
                    return -1;

                if (ListView_.SelectedIndex1 >= ListView_.Items.Count)
                    return -1;

                return ListView_.SelectedIndex1;
            }
        }

        /// <summary>
        /// The current worker directory / displayed directory
        /// </summary>
        public string CurrentDirectory
        {
            get { return _CurrentDirectory; }
        }
        private string _CurrentDirectory = "";

        private TIMER _ListviewRefreshTimer = new TIMER();

        /// <summary>
        /// Is the textbox active
        /// </summary>
        private bool _isUsingTextbox = false;

        /// <summary>
        /// Keeps track of the first index of the first item in the item cache
        /// </summary>
        private int _CahceItem1;

        /// <summary>
        /// Items currently displayed in the listview
        /// </summary>
        private ListViewItem[] _ListViewItemCache;

        /// <summary>
        /// Keeps track of the current directory, watches for system changes and contains FileCache and DirectoryCache
        /// </summary>
        private FolderWatcher _FolderWatcher;

        /// <summary>
        /// The previously visited directories 
        /// </summary>
        private Stack<string> _FolderUndoHistory = new Stack<string>();

        /// <summary>
        /// The previously visited directories after they've been undone
        /// </summary>
        private Stack<string> _FolderRedoHistory = new Stack<string>();

        /// <summary>
        /// Holds the last selected index before changing a directory.
        /// </summary>
        private Dictionary<string, int> DirectorySelectedIndexCache = new Dictionary<string, int>();

        /// <summary>
        /// Is an undo / redo occuring
        /// </summary>
        private bool _Undo = false;

        /// <summary>
        /// Prevents overflow errors
        /// </summary>
        private bool _PreventOverflow = false;

        private int _lastRetrieveVirtualItemIndex = -1;

        private ListViewItem _lastRetrieveVirtualItem = null;

        private ImageList _iconList16 = new ImageList();

        public FolderView()
        {
            InitializeComponent();

            this.textBox1.ShortcutsEnabled = true;
            _ListViewItemCache = new ListViewItem[0];

            // SuggestAppend is nice, but if you hit ctrl + A while its suggesting it just empties the textbox
            this.textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.textBox1.AutoCompleteSource = AutoCompleteSource.FileSystemDirectories;

            _iconList16.ImageSize = new Size(16, 16);
            _iconList16.ColorDepth = ColorDepth.Depth24Bit;

            _iconList16.Images.Add(ApplicationStyles.Resources.Folder16);

            ListView_.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView1_RetrieveVirtualItem);
            ListView_.CacheVirtualItems += new CacheVirtualItemsEventHandler(listView1_CacheVirtualItems);
            ListView_.ItemActivate += ListView1_ItemActivate;
            ListView_.KeyUp += OnKeyUp;

            ListView_.SmallImageList = _iconList16;
            ListView_.GridLines = false;
            ListView_.VirtualMode = true;
            ListView_.VirtualListSize = 0;
            ListView_.Sorting = SortOrder.None;
            ListView_.FullRowSelect = true;
            ListView_.AllowDrop = true;

            _FolderWatcher = new FolderWatcher(PathHelper.GetScreenshotFolder());
            _FolderWatcher.WatcherNotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
            _FolderWatcher.FilterFileExtensions = InternalSettings.Readable_Image_Formats.ToArray();
            _FolderWatcher.FileRemoved += _FolderWatcher_FileRemoved;
            _FolderWatcher.DirectoryRemoved += _FolderWatcher_DirectoryRemoved;
            _FolderWatcher.FileAdded += _FolderWatcher_FileAdded;
            _FolderWatcher.DirectoryAdded += _FolderWatcher_DirectoryAdded;
            _FolderWatcher.DirectoryRenamed += _FolderWatcher_DirectoryRenamed;
            _FolderWatcher.FileRenamed += _FolderWatcher_FileRenamed;
            _FolderWatcher.ItemChanged += _FolderWatcher_ItemChanged;
            _FolderWatcher.SortOrderChanged += _FolderWatcher_SortOrderChanged;

            ListView_.UpdateTheme();
            ListView_.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            ListView_.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            _ListviewRefreshTimer.Tick += _ListviewRefreshTimer_Tick;
            _ListviewRefreshTimer.SetInterval(500);

            UpdateTheme();
            ApplicationStyles.UpdateThemeEvent += UpdateTheme;
        }









        public async Task SetCurrentDirectory(string path)
        {
            if (this._CurrentDirectory == path)
                return;

            if (!_Undo)
            {
                _FolderUndoHistory.Push(this._CurrentDirectory);
                _FolderRedoHistory.Clear();
                this.DirectorySelectedIndexCache[_CurrentDirectory] = ListView_.SelectedIndex1;
            }

            this._CurrentDirectory = path;
            await this.LoadDirectoryAsync(path);
        }

        public async Task LoadDirectoryAsync(string path)
        {
            if (!string.IsNullOrEmpty(path) && path != InternalSettings.DRIVES_FOLDERNAME)
            {
                Directory.SetCurrentDirectory(path);
            }
            else
            {
                Directory.SetCurrentDirectory(PathHelper.GetScreenshotFolder());
            }

            await _FolderWatcher.UpdateDirectory(path);

            this.ListView_.VirtualListSize = _FolderWatcher.GetTotalCount();
            UpdateTextbox();
            ForceListviewRedraw();

            GC.Collect();
        }



















        private void _ListviewRefreshTimer_Tick(object sender, EventArgs e)
        {
            ListView_.VirtualListSize += changeVirtualSizeBy;
            changeVirtualSizeBy = 0;
            ForceListviewRedraw();
            _ListviewRefreshTimer.Stop();
        }

        public void UpdateTheme()
        {
            ApplicationStyles.ApplyCustomThemeToControl(this);
            Invalidate();
        }

        public void ForceListviewRedraw()
        {
            this._ListViewItemCache = new ListViewItem[] { };
            this.ListView_.Invalidate();
        }

        /// <summary>
        /// Updates the text of the textbox to the current directory
        /// </summary>
        public void UpdateTextbox()
        {
            this._PreventOverflow = true;
            this.textBox1.Text = $"{_CurrentDirectory}";
            this._PreventOverflow = false;
        }

        public void DeleteSelectedItems()
        {
            if (ListView_.SelectedIndices.Count < 2)
            {
                if (ListView_.FocusedItem != null)
                {
                    if (ListView_.FocusedItem.Tag is DirectoryInfo)
                    {
                        PathHelper.DeleteFileOrPath(((DirectoryInfo)ListView_.FocusedItem.Tag).FullName);
                    }
                    else if (ListView_.FocusedItem.Tag is FileInfo)
                    {
                        PathHelper.DeleteFileOrPath(((FileInfo)ListView_.FocusedItem.Tag).FullName);
                    }
                }
                return;
            }

            if (AskConfirmationOnDelete)
            {
                if (MessageBox.Show(this,
                    $"Are you sure you want to delete {ListView_.SelectedIndices.Count} items?\n",
                    "Delete Files?",
                    MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }

            string[] del = new string[ListView_.SelectedIndices.Count];
            int c = 0;
            foreach (int i in ListView_.SelectedIndices)
            {
                if (ListView_.Items[i].Tag is DirectoryInfo)
                {
                    del[c++] = ((DirectoryInfo)ListView_.Items[i].Tag).FullName;
                }
                else if (ListView_.Items[i].Tag is FileInfo)
                {
                    del[c++] = ((FileInfo)ListView_.Items[i].Tag).FullName;
                }
            }

            ListView_.DeselectAll();

            Task.Run(() =>
            {
                foreach (string i in del)
                {
                    PathHelper.DeleteFileOrPath(i);
                }
            });
        }

        /// <summary>
        /// Moves the current directory to its parent.
        /// </summary>
        public async Task UpDirectoryLevel()
        {
            if (!PathHelper.IsValidDirectoryPath(this.CurrentDirectory))
                return;

            if (string.IsNullOrEmpty(this.CurrentDirectory) || CurrentDirectory == InternalSettings.DRIVES_FOLDERNAME)
            {
                await this.SetCurrentDirectory(InternalSettings.DRIVES_FOLDERNAME);
                return;
            }

            DirectoryInfo info = new DirectoryInfo(this.CurrentDirectory);
            if (info.Parent != null)
            {
                await this.SetCurrentDirectory(info.Parent.FullName);
                this.SetLastDirectoryIndex();
            }
            else
            {
                await this.SetCurrentDirectory(InternalSettings.DRIVES_FOLDERNAME);
            }

            this.UpdateTextbox();
        }

        /// <summary>
        /// Takes the user back to the directory before the previous directory
        /// </summary>
        public async Task UndoPreviousDirectory()
        {
            if (_FolderRedoHistory.Count < 1)
                return;

            this._Undo = true;
            string newDir = this._FolderRedoHistory.Pop();

            if (newDir != "" && newDir != InternalSettings.DRIVES_FOLDERNAME && !Directory.Exists(newDir))
            {
                await UndoPreviousDirectory();
                this._Undo = false;
                return;
            }
            this._FolderUndoHistory.Push(this._CurrentDirectory);
            await this.SetCurrentDirectory(newDir);
            this.SetLastDirectoryIndex();
            this.UpdateTextbox();

            this._Undo = false;
            UpdateTextbox();
        }

        /// <summary>
        /// Takes the user back to the previous directory
        /// </summary>
        public async Task PreviousDirectory()
        {
            if (_FolderUndoHistory.Count < 1)
                return;

            this._Undo = true;
            string newDir = this._FolderUndoHistory.Pop();

            if (newDir != "" && newDir != InternalSettings.DRIVES_FOLDERNAME && !Directory.Exists(newDir))
            {
                await PreviousDirectory();
                this._Undo = false;
                return;
            }

            this._FolderRedoHistory.Push(this._CurrentDirectory);
            await this.SetCurrentDirectory(newDir);
            this.SetLastDirectoryIndex();
            this.UpdateTextbox();

            this._Undo = false;
            UpdateTextbox();
        }

        public void SetLastDirectoryIndex()
        {
            if (!this.DirectorySelectedIndexCache.ContainsKey(_CurrentDirectory))
                return;

            int index = this.DirectorySelectedIndexCache[_CurrentDirectory];

            if (this.ListView_.Items.Count <= index || index < 0)
                return;

            this.ListView_.DeselectAll();
            this.ListView_.SelectedIndex1 = index;
            this.ListView_.SelectedIndex2 = index;
            this.ListView_.SelectedItemsCount = 1;
            this.ListView_.SelectedIndices.Clear();
            this.ListView_.SelectedIndices.Add(index);
            this.ListView_.Items[index].Focused = true;
            this.ListView_.Invalidate();
        }




        private async void ListView1_ItemActivate(object sender, EventArgs e)
        {
            if (_PreventOverflow || ListView_.SelectedIndex1 == -1)
                return;

            if (ListView_.Items[ListView_.SelectedIndex1].Tag == null)
                return;

            if (ListView_.Items[ListView_.SelectedIndex1].Tag is FileInfo)
            {
                if (SettingsManager.MainFormSettings.Open_Files_On_Double_Click)
                {
                    PathHelper.OpenWithDefaultProgram(((FileInfo)ListView_.Items[ListView_.SelectedIndex1].Tag).FullName);
                }
            }
            else if (ListView_.Items[ListView_.SelectedIndex1].Tag is DirectoryInfo)
            {
                await SetCurrentDirectory(((DirectoryInfo)ListView_.Items[ListView_.SelectedIndex1].Tag).FullName);
            }
        }

        private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            int iIndex = e.ItemIndex;

            if (_lastRetrieveVirtualItemIndex == iIndex)
            {
                e.Item = _lastRetrieveVirtualItem;
                return;
            }

            _lastRetrieveVirtualItemIndex = iIndex;

            if (iIndex >= _CahceItem1 && iIndex < _CahceItem1 + _ListViewItemCache.Length)
            {
                _lastRetrieveVirtualItem = _ListViewItemCache[iIndex - _CahceItem1];

                e.Item = _lastRetrieveVirtualItem;
                return;
            }

            int dirCount = _FolderWatcher.DirectoryCache.Count;
            int fileCount = _FolderWatcher.FileCache.Count;

            if (iIndex < dirCount)
            {
                DirectoryInfo dinfo = new DirectoryInfo(_FolderWatcher.DirectoryCache[e.ItemIndex]);
                ListViewItem ditem = new ListViewItem(dinfo.Name, 0);
                ditem.SubItems.Add("");
                ditem.Tag = dinfo;

                _lastRetrieveVirtualItem = ditem;
                e.Item = ditem;
                return;
            }

            int index = iIndex - dirCount;

            FileInfo finfo;
            ListViewItem fitem;

            if (index < fileCount)
            {
                finfo = new FileInfo(_FolderWatcher.FileCache[index]);
                fitem = new ListViewItem(finfo.Name);

                if (finfo.Exists)
                {
                    fitem.SubItems.Add(Helper.SizeSuffix(finfo.Length, 2));
                }
                else
                {
                    fitem.SubItems.Add(Helper.SizeSuffix(0, 2));
                }

                fitem.Tag = finfo;
            }
            else
            {
                fitem = new ListViewItem();
                fitem.SubItems.Add("");
            }

            _lastRetrieveVirtualItem = fitem;
            e.Item = fitem;
        }


        private void listView1_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            //We've gotten a request to refresh the cache.
            //First check if it's really neccesary.
            if (_ListViewItemCache != null && e.StartIndex >= _CahceItem1 && e.EndIndex <= _CahceItem1 + _ListViewItemCache.Length)
            {
                //If the newly requested cache is a subset of the old cache, 
                //no need to rebuild everything, so do nothing.
                return;
            }
            //Now we need to rebuild the cache.
            _CahceItem1 = e.StartIndex;

            int length = e.EndIndex - e.StartIndex + 1; //indexes are inclusive
            int start = e.StartIndex;
            int end = e.EndIndex;
            int count = 0;

            _ListViewItemCache = new ListViewItem[length];

            int dirCount = _FolderWatcher.GetDirectoryCount();

            // start and ends in directory cache
            if (end < dirCount)
            {
                for (int index = start; index <= end; index++)
                {
                    DirectoryInfo dinfo;
                    ListViewItem ditem;

                    if (index < _FolderWatcher.DirectoryCache.Count)
                    {
                        dinfo = new DirectoryInfo(_FolderWatcher.DirectoryCache[index]);
                        ditem = new ListViewItem(dinfo.Name, 0);

                        ditem.SubItems.Add("");
                        ditem.Tag = dinfo;
                    }
                    else
                    {
                        ditem = new ListViewItem();
                        ditem.SubItems.Add("");
                    }
                    _ListViewItemCache[count] = ditem;
                    count++;
                }
                return;
            }

            // starts in directory cache, ends in file cache
            if (start < dirCount)
            {
                for (int index = start; index < _FolderWatcher.DirectoryCache.Count; index++)
                {
                    DirectoryInfo dinfo;
                    ListViewItem ditem;

                    if (index < _FolderWatcher.DirectoryCache.Count)
                    {
                        dinfo = new DirectoryInfo(_FolderWatcher.DirectoryCache[index]);
                        ditem = new ListViewItem(dinfo.Name, 0);

                        ditem.SubItems.Add("");
                        ditem.Tag = dinfo;
                    }
                    else
                    {
                        ditem = new ListViewItem();
                        ditem.SubItems.Add("");
                    }

                    _ListViewItemCache[count] = ditem;
                    count++;
                }

                for (int index = 0; count < length; index++)
                {
                    FileInfo finfo;
                    ListViewItem fitem;

                    if (index < _FolderWatcher.FileCache.Count)
                    {
                        finfo = new FileInfo(_FolderWatcher.FileCache[index]);
                        fitem = new ListViewItem(finfo.Name);

                        if (finfo.Exists)
                        {
                            fitem.SubItems.Add(Helper.SizeSuffix(finfo.Length, 2));
                        }
                        else
                        {
                            fitem.SubItems.Add("DELETED");
                        }

                        fitem.Tag = finfo;
                    }
                    else
                    {
                        fitem = new ListViewItem();
                        fitem.SubItems.Add("");
                    }

                    _ListViewItemCache[count] = fitem;
                    count++;
                }
                return;
            }

            // starts and ends in file cache
            for (int index = start - dirCount; count < length; index++)
            {
                FileInfo finfo;
                ListViewItem fitem;

                if (index < _FolderWatcher.FileCache.Count)
                {
                    finfo = new FileInfo(_FolderWatcher.FileCache[index]);

                    fitem = new ListViewItem(finfo.Name);

                    if (finfo.Exists)
                    {
                        fitem.SubItems.Add(Helper.SizeSuffix(finfo.Length, 2));
                    }
                    else
                    {
                        fitem.SubItems.Add("DELETED");
                    }

                    fitem.Tag = finfo;
                }
                else
                {
                    fitem = new ListViewItem();
                    fitem.SubItems.Add("");
                }

                _ListViewItemCache[count] = fitem;
                count++;
            }
        }

        private async void UpDirectoryLevel_Click(object sender, EventArgs e)
        {
            await UpDirectoryLevel();
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true; // prevents tab from switching controls
            }
        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_PreventOverflow)
                return;

            this._PreventOverflow = true;

            string text = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(text) || text == InternalSettings.DRIVES_FOLDERNAME)
            {
                await SetCurrentDirectory(InternalSettings.DRIVES_FOLDERNAME);
                this.textBox1.Text = "";
            }
            else if (PathHelper.IsValidDirectoryPath(text))
            {
                if (Directory.Exists(text))
                {
                    await SetCurrentDirectory(text);
                }
            }

            this._PreventOverflow = false;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            _isUsingTextbox = false;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            _isUsingTextbox = true;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (_isUsingTextbox)
                return;

            switch (e.KeyData)
            {
                case Keys.Back:
                    UpDirectoryLevel();
                    break;

                case Keys.Delete:
                    DeleteSelectedItems();
                    break;

                case Keys.Control | Keys.Z:
                    PreviousDirectory();
                    break;

                case Keys.Control | Keys.Y:
                    UndoPreviousDirectory();
                    break;
            }
        }

        int changeVirtualSizeBy = 0;

        // really need to fix this cause its SUPER slow for a large amount of files 
        // probably gonna just use a timer to redraw the listview after 500ms or smth
        private void _FolderWatcher_DirectoryAdded(string name)
        {
            changeVirtualSizeBy++;
            _ListviewRefreshTimer.Start();
        }

        private void _FolderWatcher_FileAdded(string name)
        {
            changeVirtualSizeBy++;
            _ListviewRefreshTimer.Start();
        }

        private void _FolderWatcher_DirectoryRemoved(string name)
        {
            changeVirtualSizeBy++;
            _ListviewRefreshTimer.Start();
        }

        private void _FolderWatcher_FileRemoved(string name)
        {
            changeVirtualSizeBy--;
            _ListviewRefreshTimer.Start();
        }

        private void _FolderWatcher_FileRenamed(string newName, string oldName)
        {
            _ListviewRefreshTimer.Start();
        }

        private void _FolderWatcher_DirectoryRenamed(string newName, string oldName)
        {
            _ListviewRefreshTimer.Start();
        }

        private void _FolderWatcher_ItemChanged(string name)
        {
            _ListviewRefreshTimer.Start();
        }

        private void _FolderWatcher_SortOrderChanged(int order)
        {
            _ListviewRefreshTimer.Start();
        }

    }
}
