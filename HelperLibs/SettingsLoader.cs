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
            KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

            if (File.Exists(DirectoryManager.currentDirectory + Settings.Default.mainFormSettings))
            {
                try
                {
                    if (keys.AllKeys.Length != 6)
                        throw new Exception("Keys have been modified MainForm.config will be reset with default values");
                    foreach (string key in keys.AllKeys)
                        switch (key)
                        {
                            case "hideMainFormOnCapture":
                                MainFormSettings.hideMainFormOnCapture = bool.Parse(keys["hideMainFormOnCapture"].Value);
                                break;
                            case "showInTray":
                                MainFormSettings.showInTray = bool.Parse(keys["showInTray"].Value);
                                break;
                            case "minimizeToTray":
                                MainFormSettings.minimizeToTray = bool.Parse(keys["minimizeToTray"].Value);
                                break;
                            case "startInTray":
                                MainFormSettings.startInTray = bool.Parse(keys["startInTray"].Value);
                                break;
                            case "alwaysOnTop":
                                MainFormSettings.alwaysOnTop = bool.Parse(keys["alwaysOnTop"].Value);
                                break;
                            case "waitHideTime":
                                MainFormSettings.waitHideTime = int.Parse(keys["waitHideTime"].Value);
                                break;
                            default:
                                throw new Exception("Keys have been modified MainForm.config will be reset with default values");
                        }
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

            foreach (string key in keys.AllKeys)
                keys.Remove(key);
            keys.Add("hideMainFormOnCapture", MainFormSettings.hideMainFormOnCapture.ToString());
            keys.Add("showInTray", MainFormSettings.showInTray.ToString());
            keys.Add("minimizeToTray", MainFormSettings.minimizeToTray.ToString());
            keys.Add("startInTray", MainFormSettings.startInTray.ToString());
            keys.Add("alwaysOnTop", MainFormSettings.alwaysOnTop.ToString());
            keys.Add("waitHideTime", MainFormSettings.waitHideTime.ToString());
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
            KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

            if (File.Exists(DirectoryManager.currentDirectory + Settings.Default.regionCaptureSettings))
            {
                try
                {
                    if (keys.AllKeys.Length != 11)
                        throw new Exception("Keys have been modified RegionCapture.config will be reset with default values");
                    foreach (string key in keys.AllKeys)
                        switch (key)
                        {
                            case "drawMagnifier":
                                RegionCaptureOptions.drawMagnifier = bool.Parse(keys["drawMagnifier"].Value);
                                break;
                            case "drawCrossHair":
                                RegionCaptureOptions.drawCrossHair = bool.Parse(keys["drawCrossHair"].Value);
                                break;
                            case "drawInfoText":
                                RegionCaptureOptions.drawInfoText = bool.Parse(keys["drawInfoText"].Value);
                                break;
                            case "marchingAnts":
                                RegionCaptureOptions.marchingAnts = bool.Parse(keys["marchingAnts"].Value);
                                break;
                            case "createClipAfterRegionCapture":
                                RegionCaptureOptions.createClipAfterRegionCapture = bool.Parse(keys["createClipAfterRegionCapture"].Value);
                                break;
                            case "autoCopyImage":
                                RegionCaptureOptions.autoCopyImage = bool.Parse(keys["autoCopyImage"].Value);
                                break;
                            case "autoCopyColor":
                                RegionCaptureOptions.autoCopyColor = bool.Parse(keys["autoCopyColor"].Value);
                                break;
                            case "cursorInfoOffset":
                                RegionCaptureOptions.cursorInfoOffset = int.Parse(keys["cursorInfoOffset"].Value);
                                break;
                            case "MagnifierPixelCount":
                                RegionCaptureOptions.MagnifierPixelCount = int.Parse(keys["MagnifierPixelCount"].Value);
                                break;
                            case "MagnifierPixelSize":
                                RegionCaptureOptions.MagnifierPixelSize = int.Parse(keys["MagnifierPixelSize"].Value);
                                break;
                            case "mode":
                                RegionCaptureOptions.mode = (RegionCaptureMode)int.Parse(keys["mode"].Value);
                                break;
                            default:
                                throw new Exception("Keys have been modified RegionCapture.config will be reset with default values");
                        }
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

            foreach (string key in keys.AllKeys)
                keys.Remove(key);
            keys.Add("drawMagnifier", RegionCaptureOptions.drawMagnifier.ToString());  // bool
            keys.Add("drawCrossHair", RegionCaptureOptions.drawCrossHair.ToString());  // bool
            keys.Add("drawInfoText", RegionCaptureOptions.drawInfoText.ToString());    // bool
            keys.Add("marchingAnts", RegionCaptureOptions.marchingAnts.ToString());    // bool
            keys.Add("createClipAfterRegionCapture", RegionCaptureOptions.createClipAfterRegionCapture.ToString());// bool
            keys.Add("autoCopyImage", RegionCaptureOptions.autoCopyImage.ToString());  // bool
            keys.Add("autoCopyColor", RegionCaptureOptions.autoCopyColor.ToString());  // bool
            keys.Add("cursorInfoOffset", RegionCaptureOptions.cursorInfoOffset.ToString()); // int
            keys.Add("MagnifierPixelCount", RegionCaptureOptions.MagnifierPixelCount.ToString());   // int
            keys.Add("MagnifierPixelSize", RegionCaptureOptions.MagnifierPixelSize.ToString());     // int
            keys.Add("mode", RegionCaptureMode.Default.ToString("D"));
            conf.Save();
            return false;
        }

        public static bool LoadHotkeySettings()
        {
            
            return true;
        }
    }
}
