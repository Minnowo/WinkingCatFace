using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using WinkingCat.HelperLibs;

namespace WinkingCat.Controls
{
    public partial class FolderView : UserControl
    {
        public int FileSortOrder
        {
            get { return _FolderWatcher.SortOrderFile; }
            set {_FolderWatcher.SortOrderFile = value; }
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
            set
            {
                if (this._CurrentDirectory == value)
                    return;

                if (!_Undo)
                {
                    _FolderUndoHistory.Push(this._CurrentDirectory);
                    _FolderRedoHistory.Clear();
                }

                this.DirectorySelectedIndexCache[_CurrentDirectory] = ListView_.SelectedIndex1;
                this._CurrentDirectory = value;
                this.LoadDirectory(value);
            }
        }
        private string _CurrentDirectory = "";

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

        public FolderView()
        {
            InitializeComponent();

            this.textBox1.ShortcutsEnabled = true;

            // SuggestAppend is nice, but if you hit ctrl + A while its suggesting it just empties the textbox
            this.textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.textBox1.AutoCompleteSource = AutoCompleteSource.FileSystemDirectories;

            ListView_.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView1_RetrieveVirtualItem);
            ListView_.CacheVirtualItems += new CacheVirtualItemsEventHandler(listView1_CacheVirtualItems);
            ListView_.ItemActivate += ListView1_ItemActivate;
            ListView_.KeyUp += OnKeyUp;

            ListView_.GridLines = false;
            ListView_.VirtualMode = true;
            ListView_.VirtualListSize = 0;
            ListView_.Sorting = SortOrder.None;
            ListView_.FullRowSelect = true;
            ListView_.AllowDrop = true;

            _FolderWatcher = new FolderWatcher(PathHelper.GetScreenshotFolder());
            _FolderWatcher.WatcherNotifyFilter = NotifyFilters.FileName;
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
            UpdateTheme();

            CurrentDirectory = "C:\\";// PathHelper.GetScreenshotFolder();

            ApplicationStyles.UpdateThemeEvent += UpdateTheme;
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

        /// <summary>
        /// Moves the current directory to its parent.
        /// </summary>
        public void UpDirectoryLevel()
        {
            if (!PathHelper.IsValidDirectoryPath(this.CurrentDirectory))
                return;

            if (string.IsNullOrEmpty(this.CurrentDirectory) || CurrentDirectory == InternalSettings.DRIVES_FOLDERNAME)
            {
                this.LoadDirectory(InternalSettings.DRIVES_FOLDERNAME);
                this.UpdateTextbox();
                return;
            }

            DirectoryInfo info = new DirectoryInfo(this.CurrentDirectory);
            if (info.Parent != null)
            {
                this.UpdateDirectory(info.Parent.FullName);
                this.SetLastDirectoryIndex();
            }
            else
            {
                this.LoadDirectory(InternalSettings.DRIVES_FOLDERNAME);
            }

            this.UpdateTextbox();
        }

        /// <summary>
        /// Takes the user back to the directory before the previous directory
        /// </summary>
        public void UndoPreviousDirectory()
        {
            if (_FolderRedoHistory.Count < 1)
                return;

            this._Undo = true;
            string newDir = this._FolderRedoHistory.Pop();

            if (newDir != "" && newDir != InternalSettings.DRIVES_FOLDERNAME && !Directory.Exists(newDir))
            {
                UndoPreviousDirectory();
                this._Undo = false;
                return;
            }
            this._FolderUndoHistory.Push(this._CurrentDirectory);
            this.LoadDirectory(newDir);
            this.SetLastDirectoryIndex();
            this.UpdateTextbox();

            this._Undo = false;
            UpdateTextbox();
        }

        /// <summary>
        /// Takes the user back to the previous directory
        /// </summary>
        public void PreviousDirectory()
        {
            if (_FolderUndoHistory.Count < 1)
                return;

            this._Undo = true;
            string newDir = this._FolderUndoHistory.Pop();

            if (newDir != "" && newDir != InternalSettings.DRIVES_FOLDERNAME && !Directory.Exists(newDir))
            {
                PreviousDirectory();
                this._Undo = false;
                return;
            }

            this._FolderRedoHistory.Push(this._CurrentDirectory);
            this.LoadDirectory(newDir);
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

        /// <summary>
        /// Changes the current directory to the given path and loads the new directory
        /// </summary>
        /// <param name="path">The new directory path.</param>
        /// <param name="updateTextbox">Should the textbox at the top be changed.</param>
        public void UpdateDirectory(string path, bool updateTextbox = false)
        {
            if (!Directory.Exists(path))
                return;

            this._PreventOverflow = true;
            path = new DirectoryInfo(path).FullName;

            if (!path.EndsWith("\\"))
                path += '\\';

            this.CurrentDirectory = path;

            if (updateTextbox)
            {
                UpdateTextbox();
            }
            this._PreventOverflow = false;
        }

        public void LoadDirectory(string path, bool update = false)
        {
            if (_CurrentDirectory != path && !update)
            {
                CurrentDirectory = path;
                return;
            }

            if (!string.IsNullOrEmpty(path) && path != InternalSettings.DRIVES_FOLDERNAME)
            {
                Directory.SetCurrentDirectory(path);
            }
            else
            {
                Directory.SetCurrentDirectory(PathHelper.GetScreenshotFolder());
            }
            
            _FolderWatcher.UpdateDirectory(path);
            
            this.ListView_.VirtualListSize = _FolderWatcher.GetTotalCount();
            UpdateTextbox();
            ForceListviewRedraw();

            GC.Collect();
        }

        private void ListView1_ItemActivate(object sender, EventArgs e)
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
                return;
            }
            else if (ListView_.Items[ListView_.SelectedIndex1].Tag is DirectoryInfo)
            {
                UpdateDirectory(((DirectoryInfo)ListView_.Items[ListView_.SelectedIndex1].Tag).FullName, true); 
            }
        }

        private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //Caching is not required but improves performance on large sets.
            //To leave out caching, don't connect the CacheVirtualItems event 
            //and make sure myCache is null.
            //check to see if the requested item is currently in the cache
            if (_ListViewItemCache != null && e.ItemIndex >= _CahceItem1 && e.ItemIndex < _CahceItem1 + _ListViewItemCache.Length)
            {
                //A cache hit, so get the ListViewItem from the cache instead of making a new one.
                e.Item = _ListViewItemCache[e.ItemIndex - _CahceItem1];
            }
            else
            {
                if (e.ItemIndex < _FolderWatcher.DirectoryCache.Count)
                {
                    _FolderWatcher.WaitThreadsFinished(false);
                    DirectoryInfo dinfo = new DirectoryInfo(_FolderWatcher.DirectoryCache[e.ItemIndex]);
                    ListViewItem ditem = new ListViewItem(dinfo.Name);
                    ditem.SubItems.Add("");
                    ditem.Tag = dinfo;

                    e.Item = ditem;
                    return;
                }
                _FolderWatcher.WaitThreadsFinished();

                int index = e.ItemIndex - _FolderWatcher.DirectoryCache.Count;

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
                        fitem.SubItems.Add(Helper.SizeSuffix(0, 2));
                    }

                    fitem.Tag = finfo;
                }
                else
                {
                    fitem = new ListViewItem();
                    fitem.SubItems.Add("");
                }

                e.Item = fitem;
            }
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
                _FolderWatcher.WaitThreadsFinished(false);
                for (int index = start; index <= end; index++)
                {
                    DirectoryInfo dinfo;
                    ListViewItem ditem;

                    if (index < _FolderWatcher.DirectoryCache.Count)
                    {
                        dinfo = new DirectoryInfo(_FolderWatcher.DirectoryCache[index]);
                        ditem = new ListViewItem(dinfo.Name);

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
                _FolderWatcher.WaitThreadsFinished(false);
                for (int index = start; index < _FolderWatcher.DirectoryCache.Count; index++)
                {
                    DirectoryInfo dinfo;
                    ListViewItem ditem;

                    if (index < _FolderWatcher.DirectoryCache.Count)
                    {
                        dinfo = new DirectoryInfo(_FolderWatcher.DirectoryCache[index]);
                        ditem = new ListViewItem(dinfo.Name);

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

                _FolderWatcher.WaitThreadsFinished(true);
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
            _FolderWatcher.WaitThreadsFinished(true);
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

        private void UpDirectoryLevel_Click(object sender, EventArgs e)
        {
            UpDirectoryLevel();
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true; // prevents tab from switching controls
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_PreventOverflow)
                return;

            string text = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(text) || text == InternalSettings.DRIVES_FOLDERNAME)
            {
                LoadDirectory(InternalSettings.DRIVES_FOLDERNAME, true);
            }

            if (PathHelper.IsValidDirectoryPath(text))
            {
                UpdateDirectory(text);
            }
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

            }
        }

        // really need to fix this cause its SUPER slow for a large amount of files 
        // probably gonna just use a timer to redraw the listview after 500ms or smth
        private void _FolderWatcher_DirectoryAdded(string name)
        {
            this.ListView_.InvokeSafe((Action)(() =>
            {
                this.ListView_.VirtualListSize++;
                this.ForceListviewRedraw();
            }));
        }

        private void _FolderWatcher_FileAdded(string name)
        {
            this.ListView_.InvokeSafe((Action)(() =>
            {
                this.ListView_.VirtualListSize++;
                this.ForceListviewRedraw();
            }));
        }

        private void _FolderWatcher_DirectoryRemoved(string name)
        {
            this.ListView_.InvokeSafe((Action)(() =>
            {
                this.ListView_.VirtualListSize--;
                this.ForceListviewRedraw();
            }));
        }

        private void _FolderWatcher_FileRemoved(string name)
        {
            this.ListView_.InvokeSafe((Action)(() =>
            {
                this.ListView_.VirtualListSize--;
                this.ForceListviewRedraw();
            }));
        }

        private void _FolderWatcher_FileRenamed(string newName, string oldName)
        {
            this.InvokeSafe((Action)(() =>
            {
                this.ForceListviewRedraw();
            }));
        }

        private void _FolderWatcher_DirectoryRenamed(string newName, string oldName)
        {
            this.InvokeSafe((Action)(() =>
            {
                this.ForceListviewRedraw();
            }));
        }

        private void _FolderWatcher_ItemChanged(string name)
        {
            this.InvokeSafe((Action)(() =>
            {
                this.ForceListviewRedraw();
            }));
        }

        private void _FolderWatcher_SortOrderChanged(int order)
        {
            this.InvokeSafe((Action)(() =>
            {
                this.ForceListviewRedraw();
            }));
        }

    }
}
