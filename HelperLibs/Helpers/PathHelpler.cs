using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using WinkingCat.HelperLibs.Properties;

namespace WinkingCat.HelperLibs
{
    public static class ResourceManager
    {
        public static string regionCaptureCursor { get; private set; } = ResourcePath.Default.regionCapturCrosshair;
    }
    public static class DirectoryManager
    {
        public static string currentDirectory { get; set; } = Directory.GetCurrentDirectory();
        public static string configPath { get; set; } = Settings.Default.configFolderPath;
        public static string resourcePath { get; set; } = Settings.Default.resourceFolderPath;
        public static string screenshotPath { get; set; }
        public static string logPath { get; set; } = Settings.Default.logFolderPath;

        public static string settingsPath { get; set; } = Settings.Default.settingsFolderPath;

        public static void LoadLocalSettings()
        {
            UpdateRelativePaths();

            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = currentDirectory + ResourcePath.Default.pathSettingsFile;
            Configuration conf = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            if (File.Exists(currentDirectory + ResourcePath.Default.pathSettingsFile))
            {
                screenshotPath = conf.AppSettings.Settings["screenshotFolderPathCustom"].Value;

                conf.AppSettings.Settings["currentDirectory"].Value = currentDirectory;

                if (string.IsNullOrEmpty(screenshotPath) || !Directory.Exists(screenshotPath))
                {
                    screenshotPath = currentDirectory + Settings.Default.screenshotFolderPathDefault;
                    conf.AppSettings.Settings["screenshotFolderPathCustom"].Value = screenshotPath;
                }
                conf.Save();
            }
            else
            {
                conf.AppSettings.Settings.Add("currentDirectory", currentDirectory);
                conf.AppSettings.Settings.Add("screenshotFolderPathCustom", currentDirectory + Settings.Default.screenshotFolderPathDefault);
                conf.Save();
                screenshotPath = currentDirectory + Settings.Default.screenshotFolderPathDefault;
            }
            UpdateRelativePaths();
        }

        public static void SavePaths()
        {
            var fileMap = new ConfigurationFileMap("");
            Settings.Default.Save();

        }

        public static void UpdateRelativePaths()
        {
            configPath = currentDirectory + Settings.Default.configFolderPath;

            resourcePath = currentDirectory + Settings.Default.resourceFolderPath;

            logPath = currentDirectory + Settings.Default.logFolderPath;

            settingsPath = currentDirectory + Settings.Default.settingsFolderPath;
        }

        public static void CreateAllPaths()
        {
            UpdateRelativePaths();
            if (!Directory.Exists(configPath))
            {
                CreateDirectory(configPath);

                if (!Directory.Exists(logPath))
                    CreateDirectory(logPath);

                if (!Directory.Exists(settingsPath))
                    CreateDirectory(settingsPath);

                if (!Directory.Exists(currentDirectory + Settings.Default.screenshotFolderPathDefault))
                    CreateDirectory(currentDirectory + Settings.Default.screenshotFolderPathDefault);
            }
            else
            {
                if (!Directory.Exists(logPath))
                    CreateDirectory(logPath);

                if (!Directory.Exists(settingsPath))
                    CreateDirectory(settingsPath);

                if (!Directory.Exists(currentDirectory + Settings.Default.screenshotFolderPathDefault))
                    CreateDirectory(currentDirectory + Settings.Default.screenshotFolderPathDefault);
            }
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

        public static string CreateScreenshotSubFolder()
        {
            String directoryName = screenshotPath + "\\" + DateTime.Today.ToString("yyy MM") + "\\";
            if (!Directory.Exists(screenshotPath))
            {
                CreateDirectory(screenshotPath);
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
    }
}
