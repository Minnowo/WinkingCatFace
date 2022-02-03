using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WinkingCat.HelperLibs
{
    public static class SettingsManager
    {
        public delegate void SettingsUpdated();
        public static event SettingsUpdated SettingsUpdatedEvent;

        private static XmlSerializer MainFormSettings_Serializer = new XmlSerializer(typeof(MainFormSettings));
        private static XmlSerializer RegionCaptureSettings_Serializer = new XmlSerializer(typeof(RegionCaptureSettings));
        private static XmlSerializer ClipSettings_Serializer = new XmlSerializer(typeof(ClipSettings));
        private static XmlSerializer MiscSettings_Serializer = new XmlSerializer(typeof(MiscSettings));
        private static XmlSerializer Hotkey_Serializer = new XmlSerializer(typeof(List<Hotkey>));

        public static MainFormSettings MainFormSettings = new MainFormSettings();
        public static RegionCaptureSettings RegionCaptureSettings = new RegionCaptureSettings();
        public static ClipSettings ClipSettings = new ClipSettings();
        public static MiscSettings MiscSettings = new MiscSettings();

        public static void ApplyImmersiveDarkTheme(Form form, bool enable)
        {
            if (form == null || form.IsDisposed)
                return;

            if (enable && MainFormSettings.useImersiveDarkMode)
            {
                NativeMethods.UseImmersiveDarkMode(form.Handle, true);
                form.Icon = ApplicationStyles.whiteIcon;
            }
            else
            {
                NativeMethods.UseImmersiveDarkMode(form.Handle, false);
                form.Icon = ApplicationStyles.blackIcon;
            }
        }

        public static void CallUpdateSettings()
        {
            if (SettingsUpdatedEvent != null)
                SettingsUpdatedEvent();
        }

        public static void SaveAllSettings(List<Hotkey> hotkeys)
        {
            string dir = Directory.GetCurrentDirectory();

            Directory.SetCurrentDirectory(AppContext.BaseDirectory);
            SettingsManager.SaveClipSettings();
            SettingsManager.SaveMainFormSettings();
            SettingsManager.SaveRegionCaptureSettings();
            SettingsManager.SaveMiscSettings();
            SettingsManager.SaveHotkeySettings(hotkeys);
            Directory.SetCurrentDirectory(dir);
        }


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
