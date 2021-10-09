using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinkingCat.HelperLibs.Properties;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;

namespace WinkingCat.HelperLibs
{
    public static class PathHelper
    {
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
                curDir = CurrentDirectory; 
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
            string curDir = CurrentDirectory;
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

        public static string GetNewImageFileName(ImgFormat fmt)
        {
            while (true)
            {
                InternalSettings.Image_Counter++;
                string pathh = Path.Combine(
                    GetScreenshotFolder(),
                    InternalSettings.Image_Counter.ToString().PadLeft(20, '0') +
                    "." + fmt.ToString());

                if (!File.Exists(pathh))
                    return pathh;
            }
        }

        public static string GetNewImageFileName()
        {
            while (true)
            {
                InternalSettings.Image_Counter++;
                string pathh = Path.Combine(
                    GetScreenshotFolder(), 
                    InternalSettings.Image_Counter.ToString().PadLeft(20, '0') + "." + InternalSettings.Default_Image_Format.ToString());

                if (!File.Exists(pathh))
                    return pathh;
            }
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
            
            File.Delete(path);

            if (File.Exists(path))
                return false;
            
            return true;
        }

        public static long GetFileSizeBytes(string path)
        {
            if (File.Exists(path))
            {
                return new FileInfo(path).Length;
            }
            return 0;
        }
    }
}
