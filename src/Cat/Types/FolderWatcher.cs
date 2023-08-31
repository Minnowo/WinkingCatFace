using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WinkingCat.HelperLibs;
using WinkingCat.Settings;

namespace WinkingCat
{
    public class FolderWatcher : IDisposable
    {
        public delegate void SortOrderChangedEvent(int order);
        public event SortOrderChangedEvent SortOrderChanged;

        public delegate void FileAddedEvent(string name);
        public event FileAddedEvent FileAdded;

        public delegate void FileRemovedEvent(string name);
        public event FileRemovedEvent FileRemoved;

        public delegate void DirectoryAddedEvent(string name);
        public event DirectoryAddedEvent DirectoryAdded;

        public delegate void DirectoryRemovedEvent(string name);
        public event DirectoryRemovedEvent DirectoryRemoved;

        public delegate void FileRenamedEvent(string newName, string oldName);
        public event FileRenamedEvent FileRenamed;

        public delegate void DirectoryRenamedEvent(string newName, string oldName);
        public event DirectoryRenamedEvent DirectoryRenamed;

        public delegate void ItemChangedEvent(string name);
        public event ItemChangedEvent ItemChanged;

        public bool AboveDrives { get { return _AboveDrives; } }
        private bool _AboveDrives = false;

        /// <summary>
        /// The directory used when above drives or when the folder watchers are disabled
        /// </summary>
        public string IDLE_DIRECTORY = "C:\\";

        /// <summary>
        /// The path that determines if drives should be counted as directories 
        /// </summary>
        public string DRIVES_FOLDERNAME = InternalSettings.DRIVES_FOLDERNAME;

        /// <summary>
        /// 1 for ascending
        /// -1 for descending
        /// </summary>
        public int SortOrderFolder
        {
            get { return _sortOrderFolder; }
            set
            {
                if (value < 0)
                {
                    if (_sortOrderFolder == -1)
                        return;

                    _sortOrderFolder = -1;
                }
                if (value >= 0)
                {
                    if (_sortOrderFolder == 1)
                        return;

                    _sortOrderFolder = 1;
                }

                DirectoryCache.Reverse();

                if (SortOrderChanged != null)
                    SortOrderChanged.Invoke(_sortOrderFolder);
            }
        }
        private int _sortOrderFolder = 1;

        /// <summary>
        /// 1 for ascending
        /// -1 for descending
        /// </summary>
        public int SortOrderFile
        {
            get { return _sortOrderFile; }
            set
            {
                if (value < 0)
                {
                    if (_sortOrderFile == -1)
                        return;

                    _sortOrderFile = -1;
                }
                if (value >= 0)
                {
                    if (_sortOrderFile == 1)
                        return;

                    _sortOrderFile = 1;
                }

                FileCache.Reverse();

                if (SortOrderChanged != null)
                    SortOrderChanged.Invoke(_sortOrderFile);
            }
        }
        public int _sortOrderFile = -1;

        public string CurrentDirectory
        {
            get
            {
                return directory;
            }
        }

        public string this[int index]
        {
            get
            {
                int c = DirectoryCache.Count;
                if (index < c)
                    return DirectoryCache[index];
                return FileCache[index - c];
            }
            set
            {
                int c = DirectoryCache.Count;
                if (index < c)
                    DirectoryCache[index] = value;
                FileCache[index - c] = value;
            }
        }

        public NotifyFilters WatcherNotifyFilter
        {
            get
            {
                return _watcherNotifyFilter;
            }
            set
            {
                _watcherNotifyFilter = value;

                foreach (FileSystemWatcher fsw in watchers)
                {
                    fsw.NotifyFilter = WatcherNotifyFilter;
                }
            }
        }
        private NotifyFilters _watcherNotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;

        public string[] FilterFileExtensions = new string[0] { };

        public List<string> DirectoryCache;        // list of sorted directories for the current directory
        public List<string> FileCache;             // list of sorted files for the current directory

        private readonly object _folderLock = new object();
        private readonly object _fileLock = new object();


        private WorkerQueue _ProcessFileSystemChange = new WorkerQueue();

        private List<FileSystemWatcher> watchers = new List<FileSystemWatcher>();
        private string directory;

        Task _sortThread;

        EventWaitHandle _EventWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);


        public FolderWatcher()
        {
            _ProcessFileSystemChange.ProcessFile += Process;

            directory = "";
            CreateWatchers(IDLE_DIRECTORY, false);
            FileCache = new List<string>();
            DirectoryCache = new List<string>();
            _sortThread = UpdateDirectory("");
        }


        public FolderWatcher(string path)
        {
            _ProcessFileSystemChange.ProcessFile += Process;

            directory = path;

            FileCache = new List<string>();
            DirectoryCache = new List<string>();

            if (!Directory.Exists(path))
            {
                CreateWatchers(IDLE_DIRECTORY, false);
                return;
            }

            CreateWatchers(path, true);

            _sortThread = SetFiles(path);
            /*_sortThread.Wait();
            _sortThread.Dispose();*/
        }

        public string GetFile(int index)
        {
            if (index < 0)
                return string.Empty;

            if (index >= FileCache.Count)
                return string.Empty;

            return FileCache[index];
        }

        public string GetDirectory(int index)
        {
            if (index < 0)
                return string.Empty;

            if (index >= DirectoryCache.Count)
                return string.Empty;

            return DirectoryCache[index];
        }

        public int GetFileIndex(string filename)
        {
            return BinarySearchItemIndex(this.FileCache, filename);
        }

        public int GetFolderIndex(string foldername)
        {
            return BinarySearchItemIndex(this.DirectoryCache, foldername);
        }

        private void Process(object item)
        {
            _EventWaitHandle.WaitOne();

            FileSystemEventArgs e = item as FileSystemEventArgs;

            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Changed:

                    if (!FilterFileExtensions.Contains(PathHelper.GetFilenameExtension(e.Name)))
                        return;

                    OnItemChanged(e.Name);
                    break;

                case WatcherChangeTypes.Created:

                    if (Directory.Exists(e.FullPath))
                    {
                        BinaryInsertDirectoryCache(e.Name);
                        return;
                    }

                    if (!FilterFileExtensions.Contains(PathHelper.GetFilenameExtension(e.Name)))
                        return;

                    if (File.Exists(e.FullPath))
                    {
                        BinaryInsertFileCache(e.Name);
                    }

                    break;

                case WatcherChangeTypes.Deleted:

                    if (DirectoryCache.Remove(e.Name))
                    {
                        OnDirectoryRemoved(e.Name);
                        return;
                    }

                    if (!FilterFileExtensions.Contains(PathHelper.GetFilenameExtension(e.Name)))
                        return;

                    if (FileCache.Remove(e.Name))
                    {
                        OnFileRemoved(e.Name);
                    }

                    break;

                case WatcherChangeTypes.Renamed:
                    Process_ItemRenamed(item as RenamedEventArgs);
                    break;
            }
        }

        private void Process_ItemRenamed(RenamedEventArgs e)
        {
            if (DirectoryCache.Remove(e.OldName))
            {
                BinaryInsertDirectoryCache(e.Name, false);
                OnDirectoryRenamed(e.Name, e.OldName);
                return;
            }

            if (FilterFileExtensions != null)
            {
                // issue #4 -> files that are renamed are not added/removed from listview with extension filter
                string extOld = PathHelper.GetFilenameExtension(e.OldName);
                string extNew = PathHelper.GetFilenameExtension(e.Name);

                if (FilterFileExtensions.Contains(extOld) && !FilterFileExtensions.Contains(extNew))
                {
                    if (FileCache.Remove(e.Name))
                    {
                        OnFileRemoved(e.Name);
                    }
                    return;
                }
                else if (!FilterFileExtensions.Contains(extOld) && FilterFileExtensions.Contains(extNew))
                {
                    if (File.Exists(e.FullPath))
                    {
                        BinaryInsertFileCache(e.Name);
                    }
                    return;
                }
            }

            if (FileCache.Remove(e.OldName))
            {
                BinaryInsertFileCache(e.Name, false);
                OnFileRenamed(e.Name, e.OldName);
            }
        }


        /// <summary>
        /// Returns the index of the given filename.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private int BinarySearchItemIndex(List<string> arr, string name, int sortorder = 1)
        {
            int L = 0;
            int R = arr.Count - 1;
            int mid;
            int com;

            while (L <= R)
            {
                mid = (L + R) / 2;

                com = Helper.StringCompareNatural(arr[mid], name) * sortorder;

                if (com == 0)
                {
                    return mid;
                }
                else if (com < 0)
                {
                    L = mid + 1;
                }
                else
                {
                    R = mid - 1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Finds where to insert a string in either of the cache lists to avoid re-sorting whenever a file is created.
        /// </summary>
        /// <param name="arr">The array to search.</param>
        /// <param name="name">The filename to search.</param>
        /// <returns></returns>
        private int BinarySearchIndex(List<string> arr, string name, int sortorder = 1)
        {
            int L = 0;
            int R = arr.Count;
            int mid;

            while (L < R)
            {
                mid = (L + R) / 2;

                if (sortorder * Helper.StringCompareNatural(arr[mid], name) <= 0)
                {
                    L = mid + 1;
                }
                else
                {
                    R = mid;
                }
            }

            return L;
        }

        private void BinaryInsertFileCache(string name, bool fireEvent = true)
        {
            if (string.IsNullOrEmpty(name))
                return;

            int index = BinarySearchIndex(FileCache, name, SortOrderFile);
            FileCache.Insert(index, name);
            if (fireEvent)
                OnFileAdded(name);
        }


        private void BinaryInsertDirectoryCache(string name, bool fireEvent = true)
        {
            if (string.IsNullOrEmpty(name))
                return;

            int index = BinarySearchIndex(DirectoryCache, name, SortOrderFolder);
            DirectoryCache.Insert(index, name);
            if (fireEvent)
                OnDirectoryAdded(name);
        }


        private void ItemRenamed(object sender, RenamedEventArgs e)
        {
            _ProcessFileSystemChange.EnqueueItem(e);
        }

        private void ItemCreated(object sender, FileSystemEventArgs e)
        {
            _ProcessFileSystemChange.EnqueueItem(e);
        }

        private void FileItemChanged(object sender, FileSystemEventArgs e)
        {
            _ProcessFileSystemChange.EnqueueItem(e);
        }

        private void ItemDeleted(object sender, FileSystemEventArgs e)
        {
            _ProcessFileSystemChange.EnqueueItem(e);
        }

        private async Task SetFiles(string path)
        {
            _EventWaitHandle.Reset();

            Task files = Task.Run(() =>
            {
                lock (_fileLock)
                {
                    FileCache.Clear();
                    if (FilterFileExtensions == null)
                    {
                        foreach (string i in Directory.EnumerateFiles(path).OrderByNatural(e => e, StringComparer.CurrentCulture, SortOrderFile != -1))
                        {
                            FileCache.Add(Path.GetFileName(i));
                        }
                    }
                    else
                    {
                        foreach (string i in Directory.EnumerateFiles(path).OrderByNatural(e => e, StringComparer.CurrentCulture, SortOrderFile != -1))
                        {
                            if (FilterFileExtensions.Contains(PathHelper.GetFilenameExtension(i)))
                            {
                                FileCache.Add(Path.GetFileName(i));
                            }
                        }
                    }
                }
            });

            Task folders = Task.Run(() =>
            {
                lock (_folderLock)
                {

                    DirectoryCache.Clear();

                    foreach (string i in Directory.EnumerateDirectories(path).OrderByNatural(e =>
                        e, StringComparer.CurrentCulture, SortOrderFolder != -1))
                    {
                        DirectoryCache.Add(Path.GetFileName(i));
                    }
                }
            });

            await files;
            await folders;

            files.Dispose();
            folders.Dispose();

            _EventWaitHandle.Set();
        }


        private void CreateWatchers(string path, bool enabled = true)
        {
            FileSystemWatcher w = new FileSystemWatcher();

            w.Path = path;
            w.IncludeSubdirectories = false;
            w.NotifyFilter = WatcherNotifyFilter;
            w.Changed += FileItemChanged;
            w.Created += ItemCreated;
            w.Renamed += ItemRenamed;
            w.Deleted += ItemDeleted;
            w.EnableRaisingEvents = enabled;
            watchers.Add(w);
        }


        private void UpdateWatchers(string path, bool enable = true)
        {
            foreach (FileSystemWatcher fsw in watchers)
            {
                fsw.Path = path;
                fsw.EnableRaisingEvents = enable;
                fsw.NotifyFilter = WatcherNotifyFilter;
            }
        }

        public int GetTotalCount()
        {
            return FileCache.Count + DirectoryCache.Count;
        }

        public int GetFileCount()
        {
            return FileCache.Count;
        }

        public int GetDirectoryCount()
        {
            return DirectoryCache.Count();
        }

        public async Task UpdateDirectory(string path)
        {
            _AboveDrives = false;
            directory = path;

            if (Directory.Exists(path))
            {
                UpdateWatchers(path, true);
                await SetFiles(directory);
                return;
            }

            DirectoryCache.Clear();
            FileCache.Clear();

            UpdateWatchers(IDLE_DIRECTORY, false);

            if (string.IsNullOrEmpty(path) || path == this.DRIVES_FOLDERNAME)
            {
                foreach (DriveInfo di in DriveInfo.GetDrives())
                {
                    DirectoryCache.Add(di.Name);
                }
                _AboveDrives = true;
            }
        }

        private void OnFileAdded(string name)
        {
            if (FileAdded != null)
                FileAdded.Invoke(name);
        }

        private void OnFileRemoved(string name)
        {
            if (FileRemoved != null)
                FileRemoved.Invoke(name);
        }

        private void OnDirectoryAdded(string name)
        {
            if (DirectoryAdded != null)
                DirectoryAdded.Invoke(name);
        }

        private void OnDirectoryRemoved(string name)
        {
            if (DirectoryRemoved != null)
                DirectoryRemoved.Invoke(name);
        }

        private void OnFileRenamed(string newName, string oldName)
        {
            if (FileRenamed != null)
                FileRenamed.Invoke(newName, oldName);
        }

        private void OnDirectoryRenamed(string newName, string oldName)
        {
            if (DirectoryRenamed != null)
                DirectoryRenamed.Invoke(newName, oldName);
        }

        private void OnItemChanged(string name)
        {
            if (ItemChanged != null)
                ItemChanged.Invoke(name);
        }

        public void Dispose()
        {
            foreach (FileSystemWatcher fsw in watchers)
            {
                fsw.Created -= ItemCreated;
                fsw.Renamed -= ItemRenamed;
                fsw.Deleted -= ItemDeleted;
                fsw.Dispose();
            }

            this.watchers.Clear();

            this._ProcessFileSystemChange.ProcessFile -= Process;
            this._ProcessFileSystemChange?.Dispose();

            this.FileCache.Clear();
            this.DirectoryCache.Clear();

            GC.SuppressFinalize(this);
        }
    }
}
