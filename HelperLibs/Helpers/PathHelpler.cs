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
        public static bool UseCustomScreenshotPath
        {
            get
            {
                return useCustomScreenshotPath;
            }
            set
            {
                useCustomScreenshotPath = value;

                if (value)
                {
                    CreateDirectory(screenshotCustomPath);
                }
            }
        }
        private static bool useCustomScreenshotPath = false;
        public static string regionCaptureCursor { get; private set; } = ResourcePath.Default.regionCapturCrosshair;
        public static string pathHelperFile { get; private set; } = ResourcePath.Default.pathSettingsFile;
        public static string currentDirectory { get; set; } = Directory.GetCurrentDirectory();
        public static string configPath { get; set; } = Settings.Default.configFolderPath;
        public static string resourcePath { get; set; } = Settings.Default.resourceFolderPath;
        public static string screenshotDefaultPath { get; set; } = Settings.Default.screenshotFolderPathDefault;
        public static string screenshotCustomPath { get; set; } = "";
        public static string logPath { get; set; } = Settings.Default.logFolderPath;
        public static string loadedItemsPath { get; set; } = Settings.Default.LoadedItems;

        public static string settingsPath { get; set; } = Settings.Default.settingsFolderPath;

        public static void UpdateRelativePaths()
        {
            currentDirectory = Directory.GetCurrentDirectory();

            configPath = currentDirectory + Settings.Default.configFolderPath;

            resourcePath = currentDirectory + Settings.Default.resourceFolderPath;

            logPath = currentDirectory + Settings.Default.logFolderPath;

            settingsPath = currentDirectory + Settings.Default.settingsFolderPath;

            loadedItemsPath = currentDirectory + Settings.Default.LoadedItems;

            screenshotDefaultPath = currentDirectory + Settings.Default.screenshotFolderPathDefault;
        }

        public static void CreateAllPaths()
        {
            UpdateRelativePaths();

            CreateDirectory(configPath);

            CreateDirectory(logPath);
            CreateDirectory(settingsPath);
            CreateDirectory(screenshotDefaultPath);

            if (!string.IsNullOrEmpty(screenshotCustomPath))
                CreateDirectory(screenshotCustomPath);
        }

        public static void CreateDirectory(string directoryPath)
        {
            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                try
                {
                    Directory.CreateDirectory(directoryPath);
                }
                catch (Exception e)
                {
                    Logger.WriteException(e);
                }
            }
        }

        public static string GetScreenshotFolder()
        {
            UpdateRelativePaths();
            if (UseCustomScreenshotPath && !string.IsNullOrEmpty(screenshotCustomPath))
            {
                CreateDirectory(screenshotCustomPath);

                if(Directory.Exists(screenshotCustomPath))
                    return screenshotCustomPath;
            }

            return CreateScreenshotSubFolder();
        }

        public static string CreateScreenshotSubFolder()
        {
            String directoryName = screenshotDefaultPath + "\\" + DateTime.Today.ToString("yyy MM") + "\\";
            if (!Directory.Exists(screenshotDefaultPath))
            {
                CreateDirectory(screenshotDefaultPath);
            }

            if (!Directory.Exists(directoryName))
            {
                CreateDirectory(directoryName);
            }

            return directoryName;
        }

        public static void CreateDirectoryFromFilePath(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                string directoryPath = Path.GetDirectoryName(filePath);
                CreateDirectory(directoryPath);
            }
        }

        public static string GetFilenameExtension(string filePath, bool includeDot = false)
        {
            string extension = "";

            if (!string.IsNullOrEmpty(filePath))
            {
                int pos = filePath.LastIndexOf('.');

                if (pos >= 0)
                {
                    extension = filePath.Substring(pos + 1);

                    if (includeDot)
                    {
                        extension = "." + extension;
                    }
                }
            }

            return extension;
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

        public static string AskChooseImageFile(string dir = "")
        {
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                dialog.EnsurePathExists = true;
                dialog.Multiselect = false;
                dialog.EnsureValidNames = true;

                CommonFileDialogFilter filter = new CommonFileDialogFilter() { DisplayName = "All Grphic Types", ShowExtensions = true };
                filter.Extensions.Add("*.bmp; *.jpg; *.jpeg; *.png; *.tiff; *.gif;");
                dialog.Filters.Add(filter);

                if (!string.IsNullOrEmpty(dir))
                    dialog.InitialDirectory = dir;
                
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    return dialog.FileName;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public static string AskChooseFile(string dir = "")
        {
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                dialog.EnsurePathExists = true;
                dialog.Multiselect = false;
                dialog.EnsureValidNames = true;

                if (!string.IsNullOrEmpty(dir))
                    dialog.InitialDirectory = dir;

                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    return dialog.FileName;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public static bool OpenWithDefaultProgram(string path)
        {
            if (File.Exists(path))
            {
                Process fileopener = new Process();
                fileopener.StartInfo.FileName = "explorer";
                fileopener.StartInfo.Arguments = "\"" + path + "\"";
                fileopener.Start();
                return true;
            }
            return false;
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
            if (File.Exists(path))
            {
                File.Delete(path);
                if (File.Exists(path))
                    return false;
                else
                    return true;
            }
            else
                return false;
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
