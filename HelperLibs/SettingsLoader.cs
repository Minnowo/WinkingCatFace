using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using WinkingCat.HelperLibs.Properties;

namespace WinkingCat.HelperLibs
{
    public static class SettingsLoader
    {
        public static Configuration ConfigLoader(string path)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = path;
            Configuration conf = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            return conf;
        }
        public static bool LoadMainFormSettings()
        {
            DirectoryManager.UpdateRelativePaths();
            Configuration conf = ConfigLoader(DirectoryManager.currentDirectory + Settings.Default.mainFormSettings);

            if (File.Exists(DirectoryManager.currentDirectory + Settings.Default.mainFormSettings))
            {
                try
                {
                    MainFormSettings.hideMainFormOnCapture = bool.Parse(conf.AppSettings.Settings["hideMainFormOnCapture"].Value);
                    MainFormSettings.showInTray = bool.Parse(conf.AppSettings.Settings["showInTray"].Value);
                    MainFormSettings.waitHideTime = int.Parse(conf.AppSettings.Settings["waitHideTime"].Value);
                    return true;
                }
                catch (Exception e)
                {
                    // if it fails to load settings from the file 
                    // the static class will just use the default settings that are hard coded
                    // then just reset the file
                    Logger.WriteException(e);
                }
            }

            foreach (string key in conf.AppSettings.Settings.AllKeys)
                conf.AppSettings.Settings.Remove(key);
            conf.AppSettings.Settings.Add("hideMainFormOnCapture", "true");
            conf.AppSettings.Settings.Add("showInTray", "true");
            conf.AppSettings.Settings.Add("waitHideTime", "500");
            conf.Save();
            return false;
        }

        public static bool LoadMainFormStyles()
        {
            return true;
        }

        public static bool LoadRegionCaptureSettings()
        {
            DirectoryManager.UpdateRelativePaths();
            Configuration conf = ConfigLoader(DirectoryManager.currentDirectory + Settings.Default.regionCaptureSettings);

            if (File.Exists(DirectoryManager.currentDirectory + Settings.Default.regionCaptureSettings))
            {
                try
                {
                    RegionCaptureOptions.drawMagnifier =    bool.Parse(conf.AppSettings.Settings["drawMagnifier"].Value);
                    RegionCaptureOptions.drawCrossHair =    bool.Parse(conf.AppSettings.Settings["drawCrossHair"].Value);
                    RegionCaptureOptions.drawInfoText =     bool.Parse(conf.AppSettings.Settings["drawInfoText"].Value);
                    RegionCaptureOptions.marchingAnts =     bool.Parse(conf.AppSettings.Settings["marchingAnts"].Value);
                    RegionCaptureOptions.createClipAfterRegionCapture = bool.Parse(conf.AppSettings.Settings["createClipAfterRegionCapture"].Value);
                    RegionCaptureOptions.cursorInfoOffset =         int.Parse(conf.AppSettings.Settings["cursorInfoOffset"].Value);
                    RegionCaptureOptions.MagnifierPixelCount =      int.Parse(conf.AppSettings.Settings["MagnifierPixelCount"].Value);
                    RegionCaptureOptions.MagnifierPixelSize =       int.Parse(conf.AppSettings.Settings["MagnifierPixelSize"].Value);
                    RegionCaptureOptions.mode = (RegionCaptureMode) int.Parse(conf.AppSettings.Settings["mode"].Value);
                    return true;
                }
                catch (Exception e)
                {
                    // if it fails to load settings from the file 
                    // the static class will just use the default settings that are hard coded
                    // then just reset the file
                    Logger.WriteException(e);
                }
            }

            foreach (string key in conf.AppSettings.Settings.AllKeys)
                conf.AppSettings.Settings.Remove(key);
            conf.AppSettings.Settings.Add("drawMagnifier", "false");
            conf.AppSettings.Settings.Add("drawCrossHair", "true");
            conf.AppSettings.Settings.Add("drawInfoText", "true");
            conf.AppSettings.Settings.Add("marchingAnts", "true");
            conf.AppSettings.Settings.Add("createClipAfterRegionCapture", "false");
            conf.AppSettings.Settings.Add("cursorInfoOffset", "10");
            conf.AppSettings.Settings.Add("MagnifierPixelCount", "15");
            conf.AppSettings.Settings.Add("MagnifierPixelSize", "10");
            conf.AppSettings.Settings.Add("mode", RegionCaptureMode.Default.ToString("D"));
            conf.Save();
            return false;
        }

        public static bool LoadHotkeySettings()
        {
            
            return true;
        }
    }
}
