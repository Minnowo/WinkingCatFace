using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WinkingCat.HelperLibs
{
    public class FolderWatcher : IDisposable
    {
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

        public bool GetAbsolutePaths = true;

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

        public string[] FilterFileExtensions = null;

        public List<string> DirectoryCache;        // list of sorted directories for the current directory
        public List<string> FileCache;             // list of sorted files for the current directory

        private WorkerQueue _ProcessFileSystemChange = new WorkerQueue();

        private List<FileSystemWatcher> watchers = new List<FileSystemWatcher>();
        private string directory;

        private Task DirectorySortThread;
        private Task FileSortThread;
        public FolderWatcher()
        {
            _ProcessFileSystemChange.ProcessFile += Process;

            directory = "";
            CreateWatchers(Directory.GetCurrentDirectory(), false);
            FileCache = new List<string>();
            DirectoryCache = new List<string>();
            UpdateDirectory("");
        }


        public FolderWatcher(string path)
        {
            _ProcessFileSystemChange.ProcessFile += Process;

            directory = path;

            FileCache = new List<string>();
            DirectoryCache = new List<string>();

            if (!Directory.Exists(path))
            {
                CreateWatchers(Directory.GetCurrentDirectory(), false);
                return;
            }

            CreateWatchers(path, true);
            SetFiles(path);
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
            WaitThreadsFinished(true);
            return BinarySearchItemIndex(this.FileCache, filename);
        }

        public int GetFolderIndex(string foldername)
        {
            WaitThreadsFinished(false);
            return BinarySearchItemIndex(this.DirectoryCache, foldername);
        }

        private void Process(object item)
        {
            FileSystemEventArgs e = item as FileSystemEventArgs;

            if (!FilterFileExtensions.Contains(PathHelper.GetFilenameExtension(e.Name)))
                return;

            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Changed:
                    OnItemChanged(e.Name);
                    break;

                case WatcherChangeTypes.Created:
                    if (File.Exists(e.FullPath))
                    {
                        BinaryInsertFileCache(e.Name);
                    }
                    if (Directory.Exists(e.FullPath))
                    {
                        BinaryInsertDirectoryCache(e.Name);
                    }
                    break;

                case WatcherChangeTypes.Deleted:

                    if (FileCache.Remove(e.Name))
                    {
                        OnFileRemoved(e.Name);
                    }

                    if (DirectoryCache.Remove(e.Name))
                    {
                        OnDirectoryRemoved(e.Name);
                    }
                    break;

                case WatcherChangeTypes.Renamed:
                    Process_ItemRenamed(item as RenamedEventArgs);
                    break;
            }
        }

        private void Process_ItemRenamed(RenamedEventArgs e)
        {
            if (FileCache.Remove(e.OldName))
            {
                BinaryInsertFileCache(e.Name, false);
                OnFileRenamed(e.Name, e.OldName);
            }

            if (DirectoryCache.Remove(e.OldName))
            {
                BinaryInsertDirectoryCache(e.Name, false);
                OnDirectoryRenamed(e.Name, e.OldName);
            }
        }



        /// <summary>
        /// blocks the thread until the Sortthread is completed
        /// </summary>
        public void WaitThreadsFinished(bool isFileThread)
        {
            if (isFileThread)
            {
                if (FileSortThread == null)
                    return;

                if (!FileSortThread.IsCompleted)
                {
                    FileSortThread.Wait();
                }
            }
            else
            {
                if (DirectorySortThread == null)
                    return;
                if (!DirectorySortThread.IsCompleted)
                    DirectorySortThread.Wait();
            }
        }

        public void WaitThreadsFinished()
        {
            WaitThreadsFinished(true);
            WaitThreadsFinished(false);
        }

        /// <summary>
        /// Returns the index of the given filename.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private int BinarySearchItemIndex(List<string> arr, string name)
        {
            int L = 0;
            int R = arr.Count - 1;
            int mid;
            int com;

            while (L <= R)
            {
                mid = (L + R) / 2;

                com = Helper.StringCompareNatural(arr[mid], name);

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
        private int BinarySearchIndex(List<string> arr, string name)
        {
            int L = 0;
            int R = arr.Count;
            int mid;

            while (L < R)
            {
                mid = (L + R) / 2;

                if (Helper.StringCompareNatural(arr[mid], name) <= 0)
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

            int index = BinarySearchIndex(FileCache, name);
            FileCache.Insert(index, name);
            if (fireEvent)
                OnFileAdded(name);
        }


        private void BinaryInsertDirectoryCache(string name, bool fireEvent = true)
        {
            if (string.IsNullOrEmpty(name))
                return;

            int index = BinarySearchIndex(DirectoryCache, name);
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

        private void SetFiles(string path)
        {
            WaitThreadsFinished();

            FileSortThread = Task.Run(() =>
            {
                FileCache.Clear();
                if (FilterFileExtensions == null)
                {
                    foreach (string i in Directory.EnumerateFiles(path).OrderByNatural(e => e))
                    {
                        FileCache.Add(Path.GetFileName(i));
                    }
                }
                else
                {
                    foreach (string i in Directory.EnumerateFiles(path).OrderByNatural(e => e))
                    {
                        if (FilterFileExtensions.Contains(PathHelper.GetFilenameExtension(i)))
                        {
                            FileCache.Add(Path.GetFileName(i));
                        }
                    }
                }
            });

            DirectorySortThread = Task.Run(() =>
            {
                DirectoryCache.Clear();
                foreach (string i in Directory.EnumerateDirectories(path).OrderByNatural(e => e))
                {
                    DirectoryCache.Add(Path.GetFileName(i));
                }
            });
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
            WaitThreadsFinished();
            return FileCache.Count + DirectoryCache.Count;
        }

        public int GetFileCount()
        {
            WaitThreadsFinished();
            return FileCache.Count;
        }

        public int GetDirectoryCount()
        {
            WaitThreadsFinished();
            return DirectoryCache.Count();
        }

        public void UpdateDirectory(string path)
        {
            _AboveDrives = false;
            directory = path;

            if (!Directory.Exists(path))
            {
                WaitThreadsFinished();

                DirectoryCache.Clear();
                FileCache.Clear();
                if (!string.IsNullOrEmpty(path) && path != InternalSettings.DRIVES_FOLDERNAME)
                {
                    UpdateWatchers(path, false);
                }
                else
                {
                    foreach (DriveInfo di in DriveInfo.GetDrives())
                    {
                        DirectoryCache.Add(di.Name);
                    }
                    _AboveDrives = true;
                }
                return;
            }

            UpdateWatchers(path, true);
            SetFiles(directory);
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

            WaitThreadsFinished();
            this.watchers.Clear();

            this._ProcessFileSystemChange.ProcessFile -= Process;
            this._ProcessFileSystemChange?.Dispose();

            this.FileCache.Clear();
            this.FileSortThread?.Dispose();
            this.DirectoryCache.Clear();
            this.DirectorySortThread?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
