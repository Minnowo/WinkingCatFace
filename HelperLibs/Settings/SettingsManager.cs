using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using WinkingCat.HelperLibs.Properties;
using System.Windows.Forms;
using System.Drawing;
using System.Xml.Serialization;

namespace WinkingCat.HelperLibs
{
    public static class SettingsManager
    {
        private static XmlSerializer MainFormSettings_Serializer = new XmlSerializer(typeof(MainFormSettings));
        private static XmlSerializer RegionCaptureSettings_Serializer = new XmlSerializer(typeof(RegionCaptureSettings));
        private static XmlSerializer ClipSettings_Serializer = new XmlSerializer(typeof(ClipSettings));
        private static XmlSerializer MiscSettings_Serializer = new XmlSerializer(typeof(MiscSettings));
        private static XmlSerializer Hotkey_Serializer = new XmlSerializer(typeof(List<Hotkey>));

        public static MainFormSettings MainFormSettings = new MainFormSettings();
        public static RegionCaptureSettings RegionCaptureSettings = new RegionCaptureSettings();
        public static ClipSettings ClipSettings = new ClipSettings();
        public static MiscSettings MiscSettings = new MiscSettings();

        public static void SaveAllSettings(List<Hotkey> hotkeys)
        {
            SettingsManager.SaveClipSettings();
            SettingsManager.SaveMainFormSettings();
            SettingsManager.SaveRegionCaptureSettings();
            SettingsManager.SaveMiscSettings();
            SettingsManager.SaveHotkeySettings(hotkeys);
        }

        public static Configuration ConfigLoader(string path)
        {
            try
            {
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = path;
                Configuration conf = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                return conf;
            }
            catch
            {
                return null;
            }
        }

        #region save styles

        public static bool SaveMainFormStyles(Configuration conf = null)
        {
            string curDir = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(curDir, Settings.Default.mainFormStyles);

            PathHelper.CreateAllPaths(curDir);
            
            if(conf == null)
                conf = ConfigLoader(filePath);

            if (conf == null)
                return false;

            try
            {
                KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

                keys.Clear();
                keys.Add("useImersiveDarkMode", ApplicationStyles.currentStyle.mainFormStyle.useImersiveDarkMode.ToString()); // bool
                keys.Add("contextMenuOpacity", ApplicationStyles.currentStyle.mainFormStyle.contextMenuOpacity.ToString()); // float

                keys.Add("backgroundColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.mainFormStyle.backgroundColor)); // color
                keys.Add("lightBackgroundColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.mainFormStyle.lightBackgroundColor)); // color
                keys.Add("darkBackgroundColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.mainFormStyle.darkBackgroundColor)); // color
                keys.Add("textColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.mainFormStyle.textColor)); // color
                keys.Add("borderColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.mainFormStyle.borderColor)); // color
                keys.Add("menuHighlightColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.mainFormStyle.menuHighlightColor)); // color
                keys.Add("menuHighlightBorderColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.mainFormStyle.menuHighlightBorderColor)); // color
                keys.Add("menuBorderColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.mainFormStyle.menuBorderColor)); // color
                keys.Add("menuCheckBackgroundColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.mainFormStyle.menuCheckBackgroundColor)); // color
                keys.Add("separatorDarkColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.mainFormStyle.separatorDarkColor)); // color
                keys.Add("separatorLightColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.mainFormStyle.separatorLightColor)); // color
                keys.Add("contextMenuFontColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.mainFormStyle.contextMenuFontColor)); // color
                keys.Add("imageViewerBackColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.mainFormStyle.imageViewerBackColor)); // color

                conf.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SaveRegionCaptureStyles(Configuration conf = null)
        {
            string curDir = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(curDir, Settings.Default.regionCaptureStyles);

            PathHelper.CreateAllPaths(curDir);

            if (conf == null)
                conf = ConfigLoader(filePath);

            if (conf == null)
                return false;

            try
            {
                KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

                keys.Clear();
                keys.Add("BackgroundOverlayColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.regionCaptureStyle.BackgroundOverlayColor)); // color
                keys.Add("ScreenWideCrosshairColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.regionCaptureStyle.ScreenWideCrosshairColor)); // color
                keys.Add("MagnifierCrosshairColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.regionCaptureStyle.MagnifierCrosshairColor)); // color
                keys.Add("MagnifierGridColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.regionCaptureStyle.MagnifierGridColor)); // color
                keys.Add("MagnifierBorderColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.regionCaptureStyle.MagnifierBorderColor)); // color
                keys.Add("infoTextBackgroundColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.regionCaptureStyle.infoTextBackgroundColor)); // color
                keys.Add("infoTextBorderColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.regionCaptureStyle.infoTextBorderColor)); // color
                keys.Add("infoTextTextColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.regionCaptureStyle.infoTextTextColor)); // color

                keys.Add("BackgroundOverlayOpacity", ApplicationStyles.currentStyle.regionCaptureStyle.BackgroundOverlayOpacity.ToString()); // ushort
                keys.Add("ScreenWideCrosshairOpacity", ApplicationStyles.currentStyle.regionCaptureStyle.ScreenWideCrosshairOpacity.ToString()); // ushort
                keys.Add("MagnifierCrosshairOpacity", ApplicationStyles.currentStyle.regionCaptureStyle.MagnifierCrosshairOpacity.ToString()); // ushort
                keys.Add("MagnifierGridOpacity", ApplicationStyles.currentStyle.regionCaptureStyle.MagnifierGridOpacity.ToString()); // ushort
                keys.Add("MagnifierBorderOpacity", ApplicationStyles.currentStyle.regionCaptureStyle.MagnifierBorderOpacity.ToString()); // ushort

                conf.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SaveClipStyles(Configuration conf = null)
        {
            string curDir = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(curDir, Settings.Default.clipStyles);

            PathHelper.CreateAllPaths(curDir);

            if (conf == null)
                conf = ConfigLoader(filePath);

            if (conf == null)
                return false;

            try
            {
                KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

                keys.Clear();
                keys.Add("clipBorderColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.clipStyle.borderColor)); // color
                keys.Add("zoomBorderColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.clipStyle.zoomBorderColor)); // color
                keys.Add("zoomReplaceTransparentColor", ColorTranslator.ToHtml(ApplicationStyles.currentStyle.clipStyle.zoomReplaceTransparentColor)); // color

                keys.Add("clipBorderThickness", ApplicationStyles.currentStyle.clipStyle.borderThickness.ToString());       // ushort
                keys.Add("zoomBorderThickness", ApplicationStyles.currentStyle.clipStyle.zoomBorderThickness.ToString());   // ushort

                keys.Add("ZoomSizePercent", ApplicationStyles.currentStyle.clipStyle.ZoomSizePercent.ToString());           // float

                keys.Add("zoomFollowMouse", ApplicationStyles.currentStyle.clipStyle.zoomFollowMouse.ToString()); // bool;

                conf.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region load styles

        public static bool LoadMainFormStyles()
        {
            string curDir = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(curDir, Settings.Default.mainFormStyles);

            PathHelper.CreateAllPaths(curDir);
            Configuration conf = ConfigLoader(filePath);

            if (conf == null)
                return false;
            KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

            if (File.Exists(filePath))
            {
                try
                {
                    foreach (string key in keys.AllKeys)
                        switch (key)
                        {
                            case "useImersiveDarkMode":
                                ApplicationStyles.currentStyle.mainFormStyle.useImersiveDarkMode = bool.Parse(keys["useImersiveDarkMode"].Value);
                                break;
                            case "contextMenuOpacity":
                                ApplicationStyles.currentStyle.mainFormStyle.contextMenuOpacity = float.Parse(keys["contextMenuOpacity"].Value);
                                break;
                            case "backgroundColor":
                                ApplicationStyles.currentStyle.mainFormStyle.backgroundColor = ColorTranslator.FromHtml(keys["backgroundColor"].Value);
                                break;
                            case "lightBackgroundColor":
                                ApplicationStyles.currentStyle.mainFormStyle.lightBackgroundColor = ColorTranslator.FromHtml(keys["lightBackgroundColor"].Value);
                                break;
                            case "darkBackgroundColor":
                                ApplicationStyles.currentStyle.mainFormStyle.darkBackgroundColor = ColorTranslator.FromHtml(keys["darkBackgroundColor"].Value);
                                break;
                            case "textColor":
                                ApplicationStyles.currentStyle.mainFormStyle.textColor = ColorTranslator.FromHtml(keys["textColor"].Value);
                                break;
                            case "borderColor":
                                ApplicationStyles.currentStyle.mainFormStyle.borderColor = ColorTranslator.FromHtml(keys["borderColor"].Value);
                                break;
                            case "menuHighlightColor":
                                ApplicationStyles.currentStyle.mainFormStyle.menuHighlightColor = ColorTranslator.FromHtml(keys["menuHighlightColor"].Value);
                                break;
                            case "menuHighlightBorderColor":
                                ApplicationStyles.currentStyle.mainFormStyle.menuHighlightBorderColor = ColorTranslator.FromHtml(keys["menuHighlightBorderColor"].Value);
                                break;
                            case "menuBorderColor":
                                ApplicationStyles.currentStyle.mainFormStyle.menuBorderColor = ColorTranslator.FromHtml(keys["menuBorderColor"].Value);
                                break;
                            case "menuCheckBackgroundColor":
                                ApplicationStyles.currentStyle.mainFormStyle.menuCheckBackgroundColor = ColorTranslator.FromHtml(keys["menuCheckBackgroundColor"].Value);
                                break;
                            case "separatorDarkColor":
                                ApplicationStyles.currentStyle.mainFormStyle.separatorDarkColor = ColorTranslator.FromHtml(keys["separatorDarkColor"].Value);
                                break;
                            case "separatorLightColor":
                                ApplicationStyles.currentStyle.mainFormStyle.separatorLightColor = ColorTranslator.FromHtml(keys["separatorLightColor"].Value);
                                break;
                            case "contextMenuFontColor":
                                ApplicationStyles.currentStyle.mainFormStyle.contextMenuFontColor = ColorTranslator.FromHtml(keys["contextMenuFontColor"].Value);
                                break;
                            case "imageViewerBackColor":
                                ApplicationStyles.currentStyle.mainFormStyle.imageViewerBackColor = ColorTranslator.FromHtml(keys["imageViewerBackColor"].Value);
                                break;
                            default:
                                throw new Exception("Keys have been modified MainFormStyles.config will be reset with default values");
                        }
                    // if the number of settings don't match they have been moded 
                    // so re-save the settings just loaded and re-write the file
                    if (keys.AllKeys.Length != 15)
                        throw new Exception("Keys have been modified MainFormStyles.config will be re-saved with recent values");
                    return true;
                }
                catch
                {
                    // if it fails to load settings from the file 
                    // the static class will just use the default settings that are hard coded
                    // then just reset the file
                }
            }
            // save the current values in memory / reset them
            SaveMainFormStyles(conf);
            return false;
        }

        public static bool LoadRegionCaptureStyles()
        {
            string curDir = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(curDir, Settings.Default.regionCaptureStyles);

            PathHelper.CreateAllPaths(curDir);
            Configuration conf = ConfigLoader(filePath);

            if (conf == null)
                return false;

            KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

            if (File.Exists(filePath))
            {
                try
                {
                    foreach (string key in keys.AllKeys)
                        switch (key)
                        {
                            case "BackgroundOverlayOpacity":
                                ApplicationStyles.currentStyle.regionCaptureStyle.BackgroundOverlayOpacity = ushort.Parse(keys["BackgroundOverlayOpacity"].Value);
                                break;
                            case "ScreenWideCrosshairOpacity":
                                ApplicationStyles.currentStyle.regionCaptureStyle.ScreenWideCrosshairOpacity = ushort.Parse(keys["ScreenWideCrosshairOpacity"].Value);
                                break;
                            case "MagnifierCrosshairOpacity":
                                ApplicationStyles.currentStyle.regionCaptureStyle.MagnifierCrosshairOpacity = ushort.Parse(keys["MagnifierCrosshairOpacity"].Value);
                                break;
                            case "MagnifierGridOpacity":
                                ApplicationStyles.currentStyle.regionCaptureStyle.MagnifierGridOpacity = ushort.Parse(keys["MagnifierGridOpacity"].Value);
                                break;
                            case "MagnifierBorderOpacity":
                                ApplicationStyles.currentStyle.regionCaptureStyle.MagnifierBorderOpacity = ushort.Parse(keys["MagnifierBorderOpacity"].Value);
                                break;
                            case "BackgroundOverlayColor":
                                ApplicationStyles.currentStyle.regionCaptureStyle.BackgroundOverlayColor = ColorTranslator.FromHtml(keys["BackgroundOverlayColor"].Value);
                                break;
                            case "ScreenWideCrosshairColor":
                                ApplicationStyles.currentStyle.regionCaptureStyle.ScreenWideCrosshairColor = ColorTranslator.FromHtml(keys["ScreenWideCrosshairColor"].Value);
                                break;
                            case "MagnifierCrosshairColor":
                                ApplicationStyles.currentStyle.regionCaptureStyle.MagnifierCrosshairColor = ColorTranslator.FromHtml(keys["MagnifierCrosshairColor"].Value);
                                break;
                            case "MagnifierGridColor":
                                ApplicationStyles.currentStyle.regionCaptureStyle.MagnifierGridColor = ColorTranslator.FromHtml(keys["MagnifierGridColor"].Value);
                                break;
                            case "MagnifierBorderColor":
                                ApplicationStyles.currentStyle.regionCaptureStyle.MagnifierBorderColor = ColorTranslator.FromHtml(keys["MagnifierBorderColor"].Value);
                                break;
                            case "infoTextBackgroundColor":
                                ApplicationStyles.currentStyle.regionCaptureStyle.infoTextBackgroundColor = ColorTranslator.FromHtml(keys["infoTextBackgroundColor"].Value);
                                break;
                            case "infoTextBorderColor":
                                ApplicationStyles.currentStyle.regionCaptureStyle.infoTextBorderColor = ColorTranslator.FromHtml(keys["infoTextBorderColor"].Value);
                                break;
                            case "infoTextTextColor":
                                ApplicationStyles.currentStyle.regionCaptureStyle.infoTextTextColor = ColorTranslator.FromHtml(keys["infoTextTextColor"].Value);
                                break;
                            default:
                                throw new Exception("Keys have been modified RegionCaptureStyles.config will be re-saved with recent values");
                        }
                    // if the number of settings don't match they have been moded 
                    // so re-save the settings just loaded and re-write the file
                    if (keys.AllKeys.Length != 13)
                        throw new Exception("Keys have been modified RegionCaptureStyles.config will be re-saved with recent values");
                    return true;
                }
                catch
                {
                    // if it fails to load settings from the file 
                    // the static class will just use the default settings that are hard coded
                    // then just reset the file
                }
            }
            // save the current values in memory / reset them
            SaveRegionCaptureStyles(conf);
            return false;
        }

        public static bool LoadClipStyles()
        {
            string curDir = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(curDir, Settings.Default.clipStyles);

            PathHelper.CreateAllPaths(curDir);
            Configuration conf = ConfigLoader(filePath);

            if (conf == null)
                return false;

            KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

            if (File.Exists(filePath))
            {
                try
                {
                    foreach (string key in keys.AllKeys)
                        switch (key)
                        {
                            case "zoomFollowMouse":
                                ApplicationStyles.currentStyle.clipStyle.zoomFollowMouse = bool.Parse(keys["zoomFollowMouse"].Value);
                                break;
                            case "clipBorderThickness":
                                ApplicationStyles.currentStyle.clipStyle.borderThickness = ushort.Parse(keys["clipBorderThickness"].Value);
                                break;
                            case "zoomBorderThickness":
                                ApplicationStyles.currentStyle.clipStyle.zoomBorderThickness = ushort.Parse(keys["zoomBorderThickness"].Value);
                                break;
                            case "ZoomSizePercent":
                                ApplicationStyles.currentStyle.clipStyle.ZoomSizePercent = float.Parse(keys["ZoomSizePercent"].Value);
                                break;
                            case "zoomReplaceTransparentColor":
                                ApplicationStyles.currentStyle.clipStyle.zoomReplaceTransparentColor = ColorTranslator.FromHtml(keys["zoomReplaceTransparentColor"].Value);
                                break;
                            case "zoomBorderColor":
                                ApplicationStyles.currentStyle.clipStyle.zoomBorderColor = ColorTranslator.FromHtml(keys["zoomBorderColor"].Value);
                                break;
                            case "clipBorderColor":
                                ApplicationStyles.currentStyle.clipStyle.borderColor = ColorTranslator.FromHtml(keys["clipBorderColor"].Value);
                                break;
                            default:
                                throw new Exception("Keys have been modified ClipStyles.config will be re-saved with recent values");
                        }
                    // if the number of settings don't match they have been moded 
                    // so re-save the settings just loaded and re-write the file
                    if (keys.AllKeys.Length != 7)
                        throw new Exception("Keys have been modified ClipStyles.config will be re-saved with recent values");
                    return true;
                }
                catch
                {
                    // if it fails to load settings from the file 
                    // the static class will just use the default settings that are hard coded
                    // then just reset the file
                }
            }
            // save the current values in memory / reset them
            SaveClipStyles(conf);
            return false;
        }

        #endregion

        #region save settings

        public static bool SaveClipSettings(Configuration conf = null)
        {
            try
            {
                string curDir = Directory.GetCurrentDirectory();
                PathHelper.CreateAllPaths(curDir);

                using (TextWriter writer = new StreamWriter(Path.Combine(curDir, InternalSettings.Clip_Settings_IO_Path)))
                {
                    ClipSettings_Serializer.Serialize(writer, ClipSettings);
                }
                return true;
            }
            catch
            {

            }
            return false;
        }

        public static bool SaveMainFormSettings()
        {
            try
            {
                string curDir = Directory.GetCurrentDirectory();
                PathHelper.CreateAllPaths(curDir);

                using (TextWriter writer = new StreamWriter(Path.Combine(curDir, InternalSettings.Main_Form_Settings_IO_Path)))
                {
                    MainFormSettings_Serializer.Serialize(writer, MainFormSettings);
                }
                return true;
            }
            catch
            {

            }
            return false;
        }

        public static bool SaveRegionCaptureSettings()
        {
            try
            {
                string curDir = Directory.GetCurrentDirectory();
                PathHelper.CreateAllPaths(curDir);

                using (TextWriter writer = new StreamWriter(Path.Combine(curDir, InternalSettings.Region_Capture_Settings_IO_Path)))
                {
                    RegionCaptureSettings_Serializer.Serialize(writer, RegionCaptureSettings);
                }
                return true;
            }
            catch
            {

            }
            return false;
        }


        public static bool SaveMiscSettings(Configuration conf = null)
        {
            try
            {
                string curDir = Directory.GetCurrentDirectory();
                PathHelper.CreateAllPaths(curDir);

                using (TextWriter writer = new StreamWriter(Path.Combine(curDir, InternalSettings.Misc_Settings_IO_Path)))
                {
                    MiscSettings_Serializer.Serialize(writer, MiscSettings);
                }
                return true;
            }
            catch
            {

            }
            return false;
        }

        public static bool SaveHotkeySettings(List<Hotkey> hotkeys)
        {
            try
            {
                string curDir = Directory.GetCurrentDirectory();
                PathHelper.CreateAllPaths(curDir);

                using (TextWriter writer = new StreamWriter(Path.Combine(curDir, InternalSettings.Hotkey_Settings_IO_Path)))
                {
                    Hotkey_Serializer.Serialize(writer, hotkeys);
                }
                return true;
            }
            catch
            {
            }
            return false;
        }

        #endregion

        #region load settings

        public static bool LoadClipSettings()
        {
            string curDir = Directory.GetCurrentDirectory();
            string path = Path.Combine(curDir, InternalSettings.Clip_Settings_IO_Path);

            PathHelper.CreateAllPaths(curDir);

            if (!File.Exists(path))
                return false;

            try
            {
                using (TextReader reader = new StreamReader(path))
                {
                    ClipSettings = (ClipSettings)ClipSettings_Serializer.Deserialize(reader);
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        public static bool LoadMainFormSettings()
        {
            string curDir = Directory.GetCurrentDirectory();
            string path = Path.Combine(curDir, InternalSettings.Main_Form_Settings_IO_Path);

            PathHelper.CreateAllPaths(curDir);

            if (!File.Exists(path))
                return false;

            try
            {
                using (TextReader reader = new StreamReader(path))
                {
                    MainFormSettings = (MainFormSettings)MainFormSettings_Serializer.Deserialize(reader);
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        public static bool LoadRegionCaptureSettings()
        {
            string curDir = Directory.GetCurrentDirectory();
            string path = Path.Combine(curDir, InternalSettings.Region_Capture_Settings_IO_Path);

            PathHelper.CreateAllPaths(curDir);

            if (!File.Exists(path))
                return false;

            try
            {
                using (TextReader reader = new StreamReader(path))
                {
                    RegionCaptureSettings = (RegionCaptureSettings)RegionCaptureSettings_Serializer.Deserialize(reader);
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        public static bool LoadMiscSettings()
        {
            string curDir = Directory.GetCurrentDirectory();
            string path = Path.Combine(curDir, InternalSettings.Misc_Settings_IO_Path);

            PathHelper.CreateAllPaths(curDir);

            if (!File.Exists(path))
                return false;

            try
            {
                using (TextReader reader = new StreamReader(path))
                {
                    MiscSettings = (MiscSettings)MiscSettings_Serializer.Deserialize(reader);
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        public static List<Hotkey> LoadHotkeySettings()
        {
            string curDir = Directory.GetCurrentDirectory();
            string path = Path.Combine(curDir, InternalSettings.Hotkey_Settings_IO_Path);

            PathHelper.CreateAllPaths(curDir);

            if (!File.Exists(path))
                return null;

            try
            {
                using (TextReader reader = new StreamReader(path))
                {
                    List<Hotkey> hotkeys = (List<Hotkey>)Hotkey_Serializer.Deserialize(reader);
                    return hotkeys;
                }
            }
            catch
            {
            }
            return null;
        }

        #endregion
    }
}
