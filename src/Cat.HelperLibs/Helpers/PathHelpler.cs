using System.IO;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;

namespace WinkingCat.HelperLibs
{
    public static class PathHelper
    {
        public static string BaseDirectory = "";
        public static string CurrentDirectory { get { return Directory.GetCurrentDirectory(); } }

        /// <summary>
        /// Creates all the paths used by the application.
        /// </summary>
        /// <param name="dir">The parent directory.</param>
        public static void CreateAllPaths(string dir = "")
        {
            string curDir;

            if (string.IsNullOrEmpty(dir)) 
            {
                curDir = BaseDirectory; 
            }
            else
            {
                curDir = dir;
            }

            CreateDirectory(Path.Combine(curDir, InternalSettings.Settings_IO_Path));
            CreateDirectory(Path.Combine(curDir, InternalSettings.Default_Screenshot_IO_Path));
            CreateDirectory(Path.Combine(curDir, InternalSettings.Plugin_IO_Path));

            if (!string.IsNullOrEmpty(SettingsManager.MiscSettings.Screenshot_Folder_Path))
                CreateDirectory(Path.Combine(curDir, InternalSettings.Plugin_IO_Path));
        }

        /// <summary>
        /// Creates a directory.
        /// </summary>
        /// <param name="directoryPath"></param>
        public static void CreateDirectory(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath) || Directory.Exists(directoryPath))
                return;
            
            try
            {
                Directory.CreateDirectory(directoryPath);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Gets the screenshot folder path.
        /// </summary>
        /// <returns>The screenshot folder path.</returns>
        public static string GetScreenshotFolder()
        {
            string curDir = BaseDirectory;
            CreateAllPaths(curDir);

            if (!SettingsManager.MiscSettings.Use_Custom_Screenshot_Folder || string.IsNullOrEmpty(SettingsManager.MiscSettings.Screenshot_Folder_Path))
                return Path.Combine(curDir, InternalSettings.Default_Screenshot_IO_Path);

            return Path.Combine(curDir, SettingsManager.MiscSettings.Screenshot_Folder_Path);
        }

        /// <summary>
        /// Checks if the given path is valid.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>true if its valid, else false.</returns>
        public static bool ValidPath(string path)
        {
            try
            {
                Path.GetFullPath(path);
                return true;
            }
            catch { }
            return false;
        }

        public static string GetNewImageFileName(string ext)
        {
            if (string.IsNullOrEmpty(ext) || ext.Length < 1)
            {
                ext = "";
            }
            else if(ext[0] != '.')
            {
                ext = '.' + ext;
            }

            string path;
            string folder = GetScreenshotFolder();
            
            do
            {
                path = Path.Combine(
                    folder,
                    (++InternalSettings.Image_Counter).ToString().PadLeft(20, '0') + ext);
            }
            while (File.Exists(path));

            return path;
        }

        public static string GetNewImageFileName()
        {
            string path;
            string folder = GetScreenshotFolder();
            string ext;

            if (InternalSettings.Default_Image_Format == ImgFormat.wrm && InternalSettings.Save_WORM_As_DWORM)
            {
                ext = ".dwrm";
            }
            else
            {
                ext = '.' + InternalSettings.Default_Image_Format.ToString();
            }

            do
            {
                path = Path.Combine(
                    folder,
                    (++InternalSettings.Image_Counter).ToString().PadLeft(20, '0') + ext);
            }
            while (File.Exists(path));

            return path;
        }

        /// <summary>
        /// Creates a directory from the given file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public static void CreateDirectoryFromFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;
            
            string directoryPath = Path.GetDirectoryName(filePath);
            CreateDirectory(directoryPath);
        }

        /// <summary>
        /// Gets the extension from a string.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="includeDot">Should the returned extension include a dot.</param>
        /// <returns>The file extension of the given string.</returns>
        public static string GetFilenameExtension(string filePath, bool includeDot = false)
        {
            if (string.IsNullOrEmpty(filePath))
                return string.Empty;
            
            int pos = filePath.LastIndexOf('.');

            if (pos < 0)
                return string.Empty;
            
            if (includeDot)
                return "." + filePath.Substring(pos + 1).ToLower();

            return filePath.Substring(pos + 1).ToLower();
        }

        

        public static string AskChooseDirectory(string dir = "")
        {
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                dialog.EnsurePathExists = true;
                dialog.IsFolderPicker = true;

                if (!string.IsNullOrEmpty(dir))
                    dialog.InitialDirectory = dir;
                
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    return dialog.FileName + "\\";
                }
                else
                {
                    return string.Empty;
                }
            } 
        }

        public static string AskChooseFile(Form form = null)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = InternalSettings.All_Files_File_Dialog;

                ofd.Multiselect = false;

                if (ofd.ShowDialog(form) == DialogResult.OK)
                {
                    if (ofd.FileNames == null || ofd.FileNames.Length < 1)
                        return string.Empty;
                    return ofd.FileNames[0];
                }
            }

            return string.Empty; 
        }

        public static bool OpenWithDefaultProgram(string path)
        {
            if (!File.Exists(path))
                return false;
            
            Process fileopener = new Process();
            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = "\"" + path + "\"";
            fileopener.Start();
            return true;
        }

        public static bool OpenExplorerAtLocation(string path)
        {
            if (File.Exists(path))
            {
                Process fileopener = new Process();
                fileopener.StartInfo.FileName = "explorer";
                fileopener.StartInfo.Arguments = string.Format("/select,\"{0}\"", path);
                fileopener.Start();
                return true;
            }
            else if (Directory.Exists(path))
            {
                Process fileopener = new Process();
                fileopener.StartInfo.FileName = "explorer";
                fileopener.StartInfo.Arguments = path;
                fileopener.Start();
                return true;
            }
            return false;
        }

        public static bool DeleteFile(string path)
        {
            if (!File.Exists(path))
                return false;

            try
            {
                File.Delete(path);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public static long GetFileSizeBytes(string path)
        {
            if (File.Exists(path))
            {
                return new FileInfo(path).Length;
            }
            return 0;
        }

        public static bool DeleteFileOrPath(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                else if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        /// <summary>
        /// Tries to create a <see cref="DirectoryInfo"/> for the given path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>true if the <see cref="DirectoryInfo"/> does not throw an error, else false</returns>
        public static bool IsValidDirectoryPath(string path)
        {
            try
            {
                new DirectoryInfo(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidDirectoryPath(string path, out DirectoryInfo info)
        {
            try
            {
                info = new DirectoryInfo(path);
                return true;
            }
            catch
            {
                info = null;
                return false;
            }
        }
    }
}
