using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{
    public static class StaticSettings
    {
        public static bool Write_Logs = true;

        public static bool Save_Images_To_Disk = true;

        public static bool Garbage_Collect_After_Clip_Destroyed = false;
        public static bool Garbage_Collect_After_All_Clips_Destroyed = true;
    }

    public static class MainFormSettings
    {
        public static event EventHandler SettingsChangedEvent;
        public static bool hideMainFormOnCapture { get; set; } = true;
        public static bool showInTray { get; set; } = true;
        public static bool minimizeToTray { get; set; } = true;
        public static bool startInTray { get; set; } = false;
        public static bool alwaysOnTop { get; set; } = true;
        public static int waitHideTime { get; set; } = 300;

        public static Tasks onTrayLeftClick { get; set; } = Tasks.RegionCapture;
        public static Tasks onTrayDoubleLeftClick { get; set; } = Tasks.OpenMainForm;
        public static Tasks onTrayMiddleClick { get; set; } = Tasks.NewClipFromClipboard;

        public static void OnSettingsChangedEvent()
        {
            if (SettingsChangedEvent != null)
            {
                SettingsChangedEvent(null, EventArgs.Empty);
            }
        }
    }
}
