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

namespace WinkingCat.HelperLibs
{
    public static class SettingsManager
    {

        public static void SaveAllSettings(List<HotkeySettings> hotkeys)
        {
            if (SettingsManager.SavePathSettings())
                Logger.WriteLine("PathSettings Settings Saved Successfully");
            if (SettingsManager.SaveMainFormSettings())
                Logger.WriteLine("MainForm Settings Saved Successfully");
            if (SettingsManager.SaveRegionCaptureSettings())
                Logger.WriteLine("RegionCapture Settings Saved Successfully");
            if (SettingsManager.SaveClipboardSettings())
                Logger.WriteLine("Clipboard Settings Saved Successfully");
            if (SettingsManager.SaveMiscSettings())
                Logger.WriteLine("Misc Settings Saved Successfully");
            if (SettingsManager.SaveHotkeySettings(hotkeys))
                Logger.WriteLine("Hotkeys Saved Successfully");
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
            catch (Exception e)
            {
                Logger.WriteException(e, "Error loading config");
                return null;
            }
        }

        #region save styles

        public static bool SaveMainFormStyles(Configuration conf = null)
        {
            PathHelper.CreateAllPaths();
            try
            {
                if (conf == null)
                    conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.mainFormStyles);
                if (conf == null)
                    return false;
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
            catch (Exception e)
            {
                Logger.WriteException(e, "Exception saving MainForm styles");
                return false;
            }
        }

        public static bool SaveRegionCaptureStyles(Configuration conf = null)
        {
            PathHelper.CreateAllPaths();
            try
            {
                if (conf == null)
                    conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.regionCaptureStyles);
                if (conf == null)
                    return false;
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
            catch (Exception e)
            {
                Logger.WriteException(e, "Exception saving MainForm styles");
                return false;
            }
        }

        public static bool SaveClipStyles(Configuration conf = null)
        {
            PathHelper.CreateAllPaths();
            try
            {
                if (conf == null)
                    conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.clipStyles);
                if (conf == null)
                    return false;
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
            catch (Exception e)
            {
                Logger.WriteException(e, "Exception saving MainForm styles");
                return false;
            }
        }

        #endregion

        #region load styles

        public static bool LoadMainFormStyles()
        {
            PathHelper.CreateAllPaths();

            Configuration conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.mainFormStyles);
            if (conf == null)
                return false;
            KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

            if (File.Exists(PathHelper.currentDirectory + Settings.Default.mainFormStyles))
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
                catch (Exception e)
                {
                    // if it fails to load settings from the file 
                    // the static class will just use the default settings that are hard coded
                    // then just reset the file
                    Logger.WriteException(e);
                }
            }
            // save the current values in memory / reset them
            SaveMainFormStyles(conf);
            return false;
        }

        public static bool LoadRegionCaptureStyles()
        {
            PathHelper.CreateAllPaths();

            Configuration conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.regionCaptureStyles);
            if (conf == null)
                return false;
            KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

            if (File.Exists(PathHelper.currentDirectory + Settings.Default.regionCaptureStyles))
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
                catch (Exception e)
                {
                    // if it fails to load settings from the file 
                    // the static class will just use the default settings that are hard coded
                    // then just reset the file
                    Logger.WriteException(e);
                }
            }
            // save the current values in memory / reset them
            SaveRegionCaptureStyles(conf);
            return false;
        }

        public static bool LoadClipStyles()
        {
            PathHelper.CreateAllPaths();

            Configuration conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.clipStyles);
            if (conf == null)
                return false;
            KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

            if (File.Exists(PathHelper.currentDirectory + Settings.Default.clipStyles))
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
                catch (Exception e)
                {
                    // if it fails to load settings from the file 
                    // the static class will just use the default settings that are hard coded
                    // then just reset the file
                    Logger.WriteException(e);
                }
            }
            // save the current values in memory / reset them
            SaveClipStyles(conf);
            return false;
        }

        #endregion

        #region save settings

        public static bool SavePathSettings(Configuration conf = null)
        {
            PathHelper.CreateAllPaths();
            try
            {
                if (conf == null)
                    conf = ConfigLoader(PathHelper.currentDirectory + PathHelper.pathHelperFile);
                if (conf == null)
                    return false;
                KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

                keys.Clear();
                keys.Add("screenshotFolderPathCustom", PathHelper.screenshotCustomPath);
                keys.Add("UseCustomScreenshotPath", PathHelper.UseCustomScreenshotPath.ToString());
                conf.Save();
                return true;
            }
            catch (Exception e)
            {
                Logger.WriteException(e, "Exception saving Path settings");
                return false;
            }
        }

        public static bool SaveMainFormSettings(Configuration conf = null)
        {
            PathHelper.CreateAllPaths();
            try
            {
                if (conf == null)
                    conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.mainFormSettings);
                if (conf == null)
                    return false;
                KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

                keys.Clear();
                keys.Add("hideMainFormOnCapture", MainFormSettings.hideMainFormOnCapture.ToString());
                keys.Add("showInTray", MainFormSettings.showInTray.ToString());
                keys.Add("minimizeToTray", MainFormSettings.minimizeToTray.ToString());
                keys.Add("startInTray", MainFormSettings.startInTray.ToString());
                keys.Add("alwaysOnTop", MainFormSettings.alwaysOnTop.ToString());
                keys.Add("waitHideTime", MainFormSettings.waitHideTime.ToString());

                keys.Add("onTrayLeftClick", MainFormSettings.onTrayLeftClick.ToString("D"));
                keys.Add("onTrayDoubleLeftClick", MainFormSettings.onTrayDoubleLeftClick.ToString("D"));
                keys.Add("onTrayMiddleClick", MainFormSettings.onTrayMiddleClick.ToString("D"));
                conf.Save();
                return true;
            }
            catch (Exception e)
            {
                Logger.WriteException(e, "Exception saving MainForm settings");
                return false;
            }
        }

        public static bool SaveRegionCaptureSettings(Configuration conf = null)
        {
            try
            {
                PathHelper.CreateAllPaths();
                if (conf == null)
                    conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.regionCaptureSettings);
                if (conf == null)
                    return false;
                KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

                keys.Clear();
                keys.Add("drawMagnifier", RegionCaptureOptions.DrawMagnifier.ToString());  // bool
                keys.Add("drawCrossHair", RegionCaptureOptions.DrawScreenWideCrosshair.ToString());  // bool
                keys.Add("drawInfoText", RegionCaptureOptions.DrawInfoText.ToString());    // bool
                keys.Add("marchingAnts", RegionCaptureOptions.DrawMarchingAnts.ToString());    // bool
                keys.Add("createClipAfterRegionCapture", RegionCaptureOptions.CreateClipAfterRegionCapture.ToString());// bool
                keys.Add("autoCopyImage", RegionCaptureOptions.AutoCopyImage.ToString());  // bool
                keys.Add("autoCopyColor", RegionCaptureOptions.AutoCopyColor.ToString());  // bool
                keys.Add("tryCenterMagnifier", RegionCaptureOptions.CenterMagnifierOnMouse.ToString());         // bool
                keys.Add("drawMagnifierCrosshair", RegionCaptureOptions.DrawCrosshairInMagnifier.ToString()); // bool
                keys.Add("drawMagnifierGrid", RegionCaptureOptions.DrawPixelGridInMagnifier.ToString());       // bool
                keys.Add("dimBackground", RegionCaptureOptions.DrawBackgroundOverlay.ToString());               // bool
                keys.Add("drawMagnifierBorder", RegionCaptureOptions.DrawBorderOnMagnifier.ToString());   // bool
                keys.Add("updateOnMouseMove", RegionCaptureOptions.updateOnMouseMove.ToString());       // bool
                //keys.Add("useSolidCrossHair", RegionCaptureOptions.useSolidCrossHair.ToString());       // bool
                keys.Add("magnifierZoomLevel", RegionCaptureOptions.MagnifierZoomLevel.ToString());     // float
                keys.Add("magnifierZoomScale", RegionCaptureOptions.MagnifierZoomScale.ToString());     // float
                keys.Add("cursorInfoOffset", RegionCaptureOptions.CursorInfoOffset.ToString());         // int
                keys.Add("MagnifierPixelCount", RegionCaptureOptions.MagnifierPixelCount.ToString());   // int
                keys.Add("MagnifierPixelSize", RegionCaptureOptions.MagnifierPixelSize.ToString());     // int
                keys.Add("mode", RegionCaptureOptions.Mode.ToString("D"));                              // enum
                keys.Add("onMouseMiddleClick", RegionCaptureOptions.OnMouseMiddleClick.ToString("D"));  // enum
                keys.Add("onMouseRightClick", RegionCaptureOptions.OnMouseRightClick.ToString("D"));    // enum
                keys.Add("onXButton1Click", RegionCaptureOptions.OnXButton1Click.ToString("D"));        // enum
                keys.Add("onXButton2Click", RegionCaptureOptions.OnXButton2Click.ToString("D"));        // enum
                keys.Add("onEscapePress", RegionCaptureOptions.OnEscapePress.ToString("D"));            // enum
                keys.Add("onZPress", RegionCaptureOptions.OnZPress.ToString("D"));                      // enum
                conf.Save();
                return true;
            }
            catch (Exception e)
            {
                Logger.WriteException(e, "Exception saving RegionCapture settings");
                return false;
            }
        }

        public static bool SaveClipboardSettings(Configuration conf = null)
        {
            PathHelper.CreateAllPaths();
            try
            {
                if (conf == null)
                    conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.clipboardSettings);
                if (conf == null)
                    return false;
                KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

                keys.Clear();
                keys.Add("copyFormat", ClipboardHelper.copyFormat.ToString("D"));
                conf.Save();
                return true;
            }
            catch (Exception e)
            {
                Logger.WriteException(e, "Exception saving Clipboard settings");
                return false;
            }
        }

        public static bool SaveMiscSettings(Configuration conf = null)
        {
            PathHelper.CreateAllPaths();
            try
            {
                if (conf == null)
                    conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.MiscSettings);
                if (conf == null)
                    return false;
                KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

                keys.Clear();
                keys.Add("imageCount", InternalSettings.Image_Counter.ToString());
                keys.Add("defaultImageType", ((int)InternalSettings.Default_Image_Format).ToString());
                keys.Add("extendBorderGrabRange", ClipOptions.ExtendBorderGrabRangePixels.ToString());
                keys.Add("ZoomRefreshRate", ClipOptions.ZoomRefreshRate.ToString());
                conf.Save();
                return true;
            }
            catch (Exception e)
            {
                Logger.WriteException(e, "Exception saving Path settings");
                return false;
            }
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

                foreach (var hotkey in hotkeys)

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

        #endregion

        #region load settings

        public static bool LoadPathSettings()
        {
            PathHelper.CreateAllPaths();
            Configuration conf = ConfigLoader(PathHelper.currentDirectory + PathHelper.pathHelperFile);
            if (conf == null)
                return false;
            KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

            if (File.Exists(PathHelper.currentDirectory + PathHelper.pathHelperFile))
            {
                try
                {
                    foreach (string key in keys.AllKeys)
                        switch (key)
                        {
                            case "screenshotFolderPathCustom":
                                PathHelper.screenshotCustomPath = keys["screenshotFolderPathCustom"].Value;
                                break;
                            case "UseCustomScreenshotPath":
                                PathHelper.UseCustomScreenshotPath = bool.Parse(keys["UseCustomScreenshotPath"].Value);
                                break;
                            default:
                                throw new Exception("Keys have been modified PathSettings.config will be reset with default values");
                        }
                    if (keys.AllKeys.Length != 2)
                        throw new Exception("Keys have been modified PathSettings.config will be re-saved with recent values");
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
            // rewrite the file
            SavePathSettings(conf);
            return false;
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
                            case "onTrayLeftClick":
                                MainFormSettings.onTrayLeftClick = (Tasks)int.Parse(keys["onTrayLeftClick"].Value);
                                break;
                            case "onTrayDoubleLeftClick":
                                MainFormSettings.onTrayDoubleLeftClick = (Tasks)int.Parse(keys["onTrayDoubleLeftClick"].Value);
                                break;
                            case "onTrayMiddleClick":
                                MainFormSettings.onTrayMiddleClick = (Tasks)int.Parse(keys["onTrayMiddleClick"].Value);
                                break;
                            default:
                                throw new Exception("Keys have been modified MainForm.config will be reset with default values");
                        }
                    if (keys.AllKeys.Length != 9)
                        throw new Exception("Keys have been modified MainForm.config will be re-saved with recent values");
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
            // save the current values in memory / reset them
            SaveMainFormSettings(conf);
            return false;
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
                    foreach (string key in keys.AllKeys)
                        switch (key)
                        {
                            case "drawMagnifier":
                                RegionCaptureOptions.DrawMagnifier = bool.Parse(keys["drawMagnifier"].Value);
                                break;
                            case "drawCrossHair":
                                RegionCaptureOptions.DrawScreenWideCrosshair = bool.Parse(keys["drawCrossHair"].Value);
                                break;
                            case "drawInfoText":
                                RegionCaptureOptions.DrawInfoText = bool.Parse(keys["drawInfoText"].Value);
                                break;
                            case "marchingAnts":
                                RegionCaptureOptions.DrawMarchingAnts = bool.Parse(keys["marchingAnts"].Value);
                                break;
                            case "createClipAfterRegionCapture":
                                RegionCaptureOptions.CreateClipAfterRegionCapture = bool.Parse(keys["createClipAfterRegionCapture"].Value);
                                break;
                            case "autoCopyImage":
                                RegionCaptureOptions.AutoCopyImage = bool.Parse(keys["autoCopyImage"].Value);
                                break;
                            case "autoCopyColor":
                                RegionCaptureOptions.AutoCopyColor = bool.Parse(keys["autoCopyColor"].Value);
                                break;
                            case "tryCenterMagnifier":
                                RegionCaptureOptions.CenterMagnifierOnMouse = bool.Parse(keys["tryCenterMagnifier"].Value);
                                break;
                            case "drawMagnifierCrosshair":
                                RegionCaptureOptions.DrawCrosshairInMagnifier = bool.Parse(keys["drawMagnifierCrosshair"].Value);
                                break;
                            case "drawMagnifierGrid":
                                RegionCaptureOptions.DrawPixelGridInMagnifier = bool.Parse(keys["drawMagnifierGrid"].Value);
                                break;
                            case "dimBackground":
                                RegionCaptureOptions.DrawBackgroundOverlay = bool.Parse(keys["dimBackground"].Value);
                                break;
                            case "drawMagnifierBorder":
                                RegionCaptureOptions.DrawBorderOnMagnifier = bool.Parse(keys["drawMagnifierBorder"].Value);
                                break;
                            case "updateOnMouseMove":
                                RegionCaptureOptions.updateOnMouseMove = bool.Parse(keys["updateOnMouseMove"].Value);
                                break;
                            /*case "useSolidCrossHair":
                                RegionCaptureOptions.useSolidCrossHair = bool.Parse(keys["useSolidCrossHair"].Value);
                                break;*/
                            case "magnifierZoomLevel":
                                RegionCaptureOptions.MagnifierZoomLevel = float.Parse(keys["magnifierZoomLevel"].Value);
                                break;
                            case "magnifierZoomScale":
                                RegionCaptureOptions.MagnifierZoomScale = float.Parse(keys["magnifierZoomScale"].Value);
                                break;
                            case "cursorInfoOffset":
                                RegionCaptureOptions.CursorInfoOffset = int.Parse(keys["cursorInfoOffset"].Value);
                                break;
                            case "MagnifierPixelCount":
                                RegionCaptureOptions.MagnifierPixelCount = int.Parse(keys["MagnifierPixelCount"].Value);
                                break;
                            case "MagnifierPixelSize":
                                RegionCaptureOptions.MagnifierPixelSize = int.Parse(keys["MagnifierPixelSize"].Value);
                                break;
                            case "mode":
                                RegionCaptureOptions.Mode = (RegionCaptureMode)int.Parse(keys["mode"].Value);
                                break;
                            case "onMouseMiddleClick":
                                RegionCaptureOptions.OnMouseMiddleClick = (InRegionTasks)int.Parse(keys["onMouseMiddleClick"].Value);
                                break;
                            case "onMouseRightClick":
                                RegionCaptureOptions.OnMouseRightClick = (InRegionTasks)int.Parse(keys["onMouseRightClick"].Value);
                                break;
                            case "onXButton1Click":
                                RegionCaptureOptions.OnXButton1Click = (InRegionTasks)int.Parse(keys["onXButton1Click"].Value);
                                break;
                            case "onXButton2Click":
                                RegionCaptureOptions.OnXButton2Click = (InRegionTasks)int.Parse(keys["onXButton2Click"].Value);
                                break;
                            case "onEscapePress":
                                RegionCaptureOptions.OnEscapePress = (InRegionTasks)int.Parse(keys["onEscapePress"].Value);
                                break;
                            case "onZPress":
                                RegionCaptureOptions.OnZPress = (InRegionTasks)int.Parse(keys["onZPress"].Value);
                                break;
                            default:
                                throw new Exception("Keys have been modified RegionCapture.config will be reset with default values");
                        }
                    if (keys.AllKeys.Length != 25)
                        throw new Exception("Keys have been modified RegionCapture.config will be re-saved with recent values");
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
            // rewrite the settings file 
            SaveRegionCaptureSettings(conf);
            return false;
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
                    foreach (string key in keys.AllKeys)
                        switch (key)
                        {
                            case "copyFormat":
                                ClipboardHelper.copyFormat = (ColorFormat)int.Parse(keys["copyFormat"].Value);
                                break;
                            default:
                                throw new Exception("Keys have been modified Clipboard.config will be reset with default values");
                        }
                    if (keys.AllKeys.Length != 1)
                        throw new Exception("Keys have been modified Clipboard.config will be re-saved with recent values");
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
            // rewrite the file
            SaveClipboardSettings(conf);
            return false;
        }

        public static bool LoadMiscSettings()
        {
            PathHelper.CreateAllPaths();
            Configuration conf = ConfigLoader(PathHelper.currentDirectory + Settings.Default.MiscSettings);
            if (conf == null)
                return false;
            KeyValueConfigurationCollection keys = conf.AppSettings.Settings;

            try
            {
                foreach (string key in keys.AllKeys)
                    switch (key)
                    {
                        case "imageCount":
                            InternalSettings.Image_Counter = int.Parse(keys["imageCount"].Value);
                            break;
                        case "defaultImageType":
                            InternalSettings.Default_Image_Format = (ImgFormat)int.Parse(keys["defaultImageType"].Value);
                            break;
                        case "extendBorderGrabRange":
                            ClipOptions.ExtendBorderGrabRangePixels = int.Parse(keys["extendBorderGrabRange"].Value);
                            break;
                        case "ZoomRefreshRate":
                            ClipOptions.ZoomRefreshRate = int.Parse(keys["ZoomRefreshRate"].Value);
                            break;
                        default:
                            throw new Exception("Keys have been modified MiscSettings.config will be reset with default values");
                    }
                if (keys.AllKeys.Length != 4)
                    throw new Exception("Keys have been modified MiscSettings.config will be re-saved with recent values");
                return true;
            }
            catch (Exception e)
            {
                // if it fails to load settings from the file 
                // the static class will just use the default settings that are hard coded
                // then just reset the file
                Logger.WriteException(e);
            }
            
            // rewrite the file
            SaveMiscSettings(conf);
            return false;
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

        #endregion
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
