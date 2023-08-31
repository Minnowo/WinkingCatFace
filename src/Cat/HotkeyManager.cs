using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinkingCat.HelperLibs;
using WinkingCat.Native;
using WinkingCat.Settings;

namespace WinkingCat
{
    public static class HotkeyManager
    {
        public static HotkeyForm hotkeyForm { get; private set; }
        public static List<Hotkey> hotKeys { get; private set; }
        public static bool ignoreHotkeyPress { get; set; } = false;
        public static bool tempTgnoreHotkeyPress { get; set; } = false;

        public static void Init()
        {
            hotkeyForm = new HotkeyForm();
            hotkeyForm.HotkeyPress += Nyah;
        }

        public static void Nyah(ushort id, Keys key, Modifiers modifier)
        {
            Hotkey hotkey = hotKeys.Find(x => x.ID == id);

            if (hotkey != null && !ignoreHotkeyPress && !tempTgnoreHotkeyPress)
            {
                int i = SettingsManager.MainFormSettings.Wait_Hide_Time;
                SettingsManager.MainFormSettings.Wait_Hide_Time = 0;

                TaskHandler.ExecuteTask(hotkey.Callback);

                SettingsManager.MainFormSettings.Wait_Hide_Time = i;
            }
        }


        public static void UpdateHotkeys(List<Hotkey> hotkeys, bool showFailedHotkeys)
        {
            if (hotKeys != null)
            {
                UnRegisterAllHotkeys();
            }

            hotKeys = hotkeys;

            RegisterAllHotkeys();

            if (showFailedHotkeys)
            {
                ShowFailedHotkeys();
            }
        }

        public static void RegisterHotkey(Hotkey hotkey)
        {
            UnRegisterHotkey(hotkey, false);

            if (hotkey.Status != HotkeyStatus.Registered && hotkey.IsValidHotkey)
            {
                hotkeyForm.RegisterHotkey(hotkey);
            }

            if (!hotKeys.Contains(hotkey))
            {
                hotKeys.Add(hotkey);
            }
        }

        public static void UnRegisterHotkey(Hotkey hotkeySetting, bool removeFromList)
        {
            if (hotkeySetting.Status == HotkeyStatus.Registered)
            {
                hotkeyForm.UnRegisterHotkey(hotkeySetting);
            }

            if (removeFromList)
            {
                hotKeys.Remove(hotkeySetting);
            }
        }

        public static void RegisterAllHotkeys()
        {
            foreach (Hotkey hotkey in hotKeys.ToArray())
            {
                RegisterHotkey(hotkey);
            }
        }

        public static void UnRegisterAllHotkeys(bool removeFromList = true, bool temporary = false)
        {
            if (hotKeys == null)
                return;

            foreach (Hotkey hotkey in hotKeys.ToArray())
            {
                UnRegisterHotkey(hotkey, removeFromList);
            }
        }

        public static void ShowFailedHotkeys()
        {
            List<Hotkey> failedHotkeysList = hotKeys.Where(x => x.Status == HotkeyStatus.Failed).ToList();

            if (failedHotkeysList.Count > 0)
            {
                foreach (Hotkey hotkey in failedHotkeysList)
                {
                    MessageBox.Show(hotkey.ToString());
                }
            }
        }

        public static List<Hotkey> GetDefaultHotkeyList()
        {
            return new List<Hotkey>
            {
                new Hotkey(Keys.Control | Keys.PrintScreen, Function.CaptureActiveMonitor),
                new Hotkey(Keys.Alt | Keys.PrintScreen, Function.CaptureActiveWindow),
                new Hotkey(Keys.PrintScreen, Function.CaptureFullScreen),
                new Hotkey(Keys.Alt | Keys.Z, Function.NewClipFromRegionCapture),
            };
        }
    }
}
