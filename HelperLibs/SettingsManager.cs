using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using WinkingCat.HelperLibs.Properties;
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{
    public static class SettingsManager
    {
        public static Configuration ConfigLoader(string path)
        {
            try
            {
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = path;
                Configuration conf = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                return conf;
            }
            catch (Exception e)
            {
                Logger.WriteException(e, "Error loading config");
                return null;
            }
        }

        public static bool SaveMainFormSettings()
        {
            PathHelper.CreateAllPaths();
            try
            {
                Configuration conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.mainFormSettings);
                if (conf == null)
                    return false;
                KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

                foreach (string key in keys.AllKeys)
                    keys.Remove(key);
                keys.Add("hideMainFormOnCapture", MainFormSettings.hideMainFormOnCapture.ToString());
                keys.Add("showInTray", MainFormSettings.showInTray.ToString());
                keys.Add("minimizeToTray", MainFormSettings.minimizeToTray.ToString());
                keys.Add("startInTray", MainFormSettings.startInTray.ToString());
                keys.Add("alwaysOnTop", MainFormSettings.alwaysOnTop.ToString());
                keys.Add("waitHideTime", MainFormSettings.waitHideTime.ToString());
                conf.Save();
                return true;
            }
            catch (Exception e)
            {
                Logger.WriteException(e, "Exception saving MainForm settings");
                return false;
            }
        }

        public static bool LoadMainFormSettings()
        {
            PathHelper.CreateAllPaths();
            Configuration conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.mainFormSettings);
            if (conf == null)
                return false;
            KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

            if (File.Exists(PathHelper.currentDirectory + Settings.Default.mainFormSettings))
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

        public static bool SaveRegionCaptureSettings()
        {
            try
            {
                PathHelper.CreateAllPaths();
                Configuration conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.regionCaptureSettings);
                if (conf == null)
                    return false;
                KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

                foreach (string key in keys.AllKeys)
                    keys.Remove(key);
                keys.Add("drawMagnifier", RegionCaptureOptions.drawMagnifier.ToString());  // bool
                keys.Add("drawCrossHair", RegionCaptureOptions.drawCrossHair.ToString());  // bool
                keys.Add("drawInfoText", RegionCaptureOptions.drawInfoText.ToString());    // bool
                keys.Add("marchingAnts", RegionCaptureOptions.marchingAnts.ToString());    // bool
                keys.Add("createClipAfterRegionCapture", RegionCaptureOptions.createClipAfterRegionCapture.ToString());// bool
                keys.Add("autoCopyImage", RegionCaptureOptions.autoCopyImage.ToString());  // bool
                keys.Add("autoCopyColor", RegionCaptureOptions.autoCopyColor.ToString());  // bool
                keys.Add("tryCenterMagnifier", RegionCaptureOptions.tryCenterMagnifier.ToString());  // bool
                keys.Add("drawMagnifierCrosshair", RegionCaptureOptions.drawMagnifierCrosshair.ToString());  // bool
                keys.Add("drawMagnifierGrid", RegionCaptureOptions.drawMagnifierGrid.ToString());  // bool
                keys.Add("dimBackground", RegionCaptureOptions.dimBackground.ToString());  // bool
                keys.Add("drawMagnifierBorder", RegionCaptureOptions.drawMagnifierBorder.ToString());  // bool
                keys.Add("updateOnMouseMove", RegionCaptureOptions.updateOnMouseMove.ToString());  // bool
                keys.Add("magnifierZoomLevel", RegionCaptureOptions.magnifierZoomLevel.ToString());  // float
                keys.Add("magnifierZoomScale", RegionCaptureOptions.magnifierZoomScale.ToString()); // float
                keys.Add("cursorInfoOffset", RegionCaptureOptions.cursorInfoOffset.ToString()); // int
                keys.Add("MagnifierPixelCount", RegionCaptureOptions.magnifierPixelCount.ToString());   // int
                keys.Add("MagnifierPixelSize", RegionCaptureOptions.magnifierPixelSize.ToString());     // int
                keys.Add("mode", RegionCaptureOptions.mode.ToString("D"));
                keys.Add("onMouseMiddleClick", RegionCaptureOptions.onMouseMiddleClick.ToString("D"));
                keys.Add("onMouseRightClick", RegionCaptureOptions.onMouseRightClick.ToString("D"));
                keys.Add("onXButton1Click", RegionCaptureOptions.onXButton1Click.ToString("D"));
                keys.Add("onXButton2Click", RegionCaptureOptions.onXButton2Click.ToString("D"));
                keys.Add("onEscapePress", RegionCaptureOptions.onEscapePress.ToString("D"));
                keys.Add("onZPress", RegionCaptureOptions.onZPress.ToString("D"));
                conf.Save();
                return true;
            }
            catch (Exception e)
            {
                Logger.WriteException(e, "Exception saving RegionCapture settings");
                return false;
            }
        }

        public static bool LoadRegionCaptureSettings()
        {
            PathHelper.CreateAllPaths();
            Configuration conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.regionCaptureSettings);
            if (conf == null)
                return false;
            KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

            if (File.Exists(PathHelper.currentDirectory + Settings.Default.regionCaptureSettings))
            {
                try
                {
                    if (keys.AllKeys.Length != 25)
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
                            case "tryCenterMagnifier":
                                RegionCaptureOptions.tryCenterMagnifier = bool.Parse(keys["tryCenterMagnifier"].Value);
                                break;
                            case "drawMagnifierCrosshair":
                                RegionCaptureOptions.drawMagnifierCrosshair = bool.Parse(keys["drawMagnifierCrosshair"].Value);
                                break;
                            case "drawMagnifierGrid":
                                RegionCaptureOptions.drawMagnifierGrid = bool.Parse(keys["drawMagnifierGrid"].Value);
                                break;
                            case "dimBackground":
                                RegionCaptureOptions.dimBackground = bool.Parse(keys["dimBackground"].Value);
                                break;
                            case "drawMagnifierBorder":
                                RegionCaptureOptions.drawMagnifierBorder = bool.Parse(keys["drawMagnifierBorder"].Value);
                                break;
                            case "updateOnMouseMove":
                                RegionCaptureOptions.updateOnMouseMove = bool.Parse(keys["updateOnMouseMove"].Value);
                                break;
                            case "magnifierZoomLevel":
                                RegionCaptureOptions.magnifierZoomLevel = float.Parse(keys["magnifierZoomLevel"].Value);
                                break;
                            case "magnifierZoomScale":
                                RegionCaptureOptions.magnifierZoomScale = float.Parse(keys["magnifierZoomScale"].Value);
                                break;
                            case "cursorInfoOffset":
                                RegionCaptureOptions.cursorInfoOffset = int.Parse(keys["cursorInfoOffset"].Value);
                                break;
                            case "MagnifierPixelCount":
                                RegionCaptureOptions.magnifierPixelCount = int.Parse(keys["MagnifierPixelCount"].Value);
                                break;
                            case "MagnifierPixelSize":
                                RegionCaptureOptions.magnifierPixelSize = int.Parse(keys["MagnifierPixelSize"].Value);
                                break;
                            case "mode":
                                RegionCaptureOptions.mode = (RegionCaptureMode)int.Parse(keys["mode"].Value);
                                break;
                            case "onMouseMiddleClick":
                                RegionCaptureOptions.onMouseMiddleClick = (InRegionTasks)int.Parse(keys["onMouseMiddleClick"].Value);
                                break;
                            case "onMouseRightClick":
                                RegionCaptureOptions.onMouseRightClick = (InRegionTasks)int.Parse(keys["onMouseRightClick"].Value);
                                break;
                            case "onXButton1Click":
                                RegionCaptureOptions.onXButton1Click = (InRegionTasks)int.Parse(keys["onXButton1Click"].Value);
                                break;
                            case "onXButton2Click":
                                RegionCaptureOptions.onXButton2Click = (InRegionTasks)int.Parse(keys["onXButton2Click"].Value);
                                break;
                            case "onEscapePress":
                                RegionCaptureOptions.onEscapePress = (InRegionTasks)int.Parse(keys["onEscapePress"].Value);
                                break;
                            case "onZPress":
                                RegionCaptureOptions.onZPress = (InRegionTasks)int.Parse(keys["onZPress"].Value);
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
            keys.Add("tryCenterMagnifier", RegionCaptureOptions.tryCenterMagnifier.ToString());  // bool
            keys.Add("drawMagnifierCrosshair", RegionCaptureOptions.drawMagnifierCrosshair.ToString());  // bool
            keys.Add("drawMagnifierGrid", RegionCaptureOptions.drawMagnifierGrid.ToString());  // bool
            keys.Add("dimBackground", RegionCaptureOptions.dimBackground.ToString());  // bool
            keys.Add("drawMagnifierBorder", RegionCaptureOptions.drawMagnifierBorder.ToString());  // bool
            keys.Add("updateOnMouseMove", RegionCaptureOptions.updateOnMouseMove.ToString());  // bool
            keys.Add("magnifierZoomLevel", RegionCaptureOptions.magnifierZoomLevel.ToString());  // float
            keys.Add("magnifierZoomScale", RegionCaptureOptions.magnifierZoomScale.ToString()); // float
            keys.Add("cursorInfoOffset", RegionCaptureOptions.cursorInfoOffset.ToString()); // int
            keys.Add("MagnifierPixelCount", RegionCaptureOptions.magnifierPixelCount.ToString());   // int
            keys.Add("MagnifierPixelSize", RegionCaptureOptions.magnifierPixelSize.ToString());     // int
            keys.Add("mode", RegionCaptureOptions.mode.ToString("D"));
            keys.Add("onMouseMiddleClick", RegionCaptureOptions.onMouseMiddleClick.ToString("D"));
            keys.Add("onMouseRightClick", RegionCaptureOptions.onMouseRightClick.ToString("D"));
            keys.Add("onXButton1Click", RegionCaptureOptions.onXButton1Click.ToString("D"));
            keys.Add("onXButton2Click", RegionCaptureOptions.onXButton2Click.ToString("D"));
            keys.Add("onEscapePress", RegionCaptureOptions.onEscapePress.ToString("D"));
            keys.Add("onZPress", RegionCaptureOptions.onZPress.ToString("D"));
            conf.Save();
            return false;
        }

        public static bool SaveClipboardSettings()
        {
            PathHelper.CreateAllPaths();
            try
            {
                Configuration conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.clipboardSettings);
                if (conf == null)
                    return false;
                KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

                foreach (string key in keys.AllKeys)
                    keys.Remove(key);
                keys.Add("copyFormat", ClipboardHelpers.copyFormat.ToString("D"));
                conf.Save();
                return true;
            }
            catch (Exception e)
            {
                Logger.WriteException(e, "Exception saving Clipboard settings");
                return false;
            }
        }

        public static bool LoadClipboardSettings()
        {
            PathHelper.CreateAllPaths();
            Configuration conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.clipboardSettings);
            if (conf == null)
                return false;
            KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

            if (File.Exists(PathHelper.currentDirectory + Settings.Default.clipboardSettings))
            {
                try
                {
                    if (keys.AllKeys.Length != 1)
                        throw new Exception("Keys have been modified Clipboard.config will be reset with default values");
                    foreach (string key in keys.AllKeys)
                        switch (key)
                        {
                            case "copyFormat":
                                ClipboardHelpers.copyFormat = (ColorFormat)int.Parse(keys["copyFormat"].Value);
                                break;
                            default:
                                throw new Exception("Keys have been modified Clipboard.config will be reset with default values");
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
            keys.Add("copyFormat", ClipboardHelpers.copyFormat.ToString("D"));

            conf.Save();
            return false;
        }

        public static bool SaveHotkeySettings(List<HotkeySettings> hotkeys)
        {
            PathHelper.CreateAllPaths();
            Configuration conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.hotkeySettings);
            if (conf == null)
                return false;
            try
            {
                ConfigurationSectionGroup hotkeySectionGroup = new ConfigurationSectionGroup();
                HotkeySectionHandler HotkeySection;
                int i = -1;

                conf.SectionGroups.Remove("Hotkeys");
                conf.SectionGroups.Add("Hotkeys", hotkeySectionGroup);

                foreach(var hotkey in hotkeys)

                {
                    i++;

                    HotkeySection = new HotkeySectionHandler();
                    HotkeySection.SectionName = "hotkey" + i.ToString();
                    HotkeySection.Keys = ((uint)hotkey.HotkeyInfo.KeyCode).ToString();
                    HotkeySection.Modifiers = ((uint)hotkey.HotkeyInfo.ModifiersEnum).ToString();
                    HotkeySection.task = ((uint)hotkey.Task).ToString();
                    HotkeySection.SectionInformation.ForceSave = true;

                    hotkeySectionGroup.Sections.Add(HotkeySection.SectionName, HotkeySection);

                }
                conf.Save(ConfigurationSaveMode.Minimal);
                return true;
            }
            catch (Exception e)
            {
                Logger.WriteException(e, "Exception saving Hotkeys");
                return false;
            }
        }

        public static List<HotkeySettings> LoadHotkeySettings()
        {
            PathHelper.CreateAllPaths();
            Configuration conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.hotkeySettings);
            if (conf == null)
                return null;
            try
            {
                ConfigurationSectionGroup hotkeySectionGroup = new ConfigurationSectionGroup();
                List<HotkeySettings> hotKeys = new List<HotkeySettings> { };

                ConfigurationSectionGroup group = conf.GetSectionGroup("Hotkeys") as ConfigurationSectionGroup;

                if (group == null)
                    return null;

                foreach(HotkeySectionHandler section in group.Sections)
                {
                    hotKeys.Add(new HotkeySettings((Tasks)int.Parse(section.task), 
                        Helpers.ModifierAsKey((Modifiers)uint.Parse(section.Modifiers)) | ((Keys)uint.Parse(section.Keys))));
                }

                return hotKeys;
            }
            catch (Exception e)
            {
                Logger.WriteException(e, "Exception saving Hotkeys");
                return null;
            }
        }
    }

    public class HotkeySectionHandler : ConfigurationSection

    {
        private string _SectionName;
        public string SectionName
        {
            get { return _SectionName; }
            set { _SectionName = value; }
        }

        public HotkeySectionHandler() { }

        public HotkeySectionHandler(string SectionName, string modifiers, string Key, string task)
        {
            this.SectionName = SectionName;
            this.Modifiers = modifiers;
            this.Keys = Key; 
            this.task = task;
        }

        [ConfigurationProperty("Modifiers", DefaultValue = "null", IsRequired = true)]
        [StringValidator(InvalidCharacters = "", MinLength = 0)]
        public string Modifiers
        {
            get { return (string)this["Modifiers"]; }
            set { this["Modifiers"] = value; }
        }


        [ConfigurationProperty("keys", DefaultValue = "null", IsRequired = true)]
        [StringValidator(InvalidCharacters = "", MinLength = 0)]
        public string Keys
        {
            get { return (string)this["keys"]; }
            set { this["keys"] = value; }
        }


        [ConfigurationProperty("task", DefaultValue = "null", IsRequired = true)]
        [StringValidator(InvalidCharacters = "", MinLength = 1)]
        public string task
        {
            get { return this["task"] as string; }
            set { this["task"] = value; }
        }
    }
}
