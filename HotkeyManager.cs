using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinkingCat.HelperLibs;
using System.Windows.Forms;

namespace WinkingCat.HotkeyLib
{
    public static class HotkeyManager
    {
        public static HotkeyForm hotkeyForm { get; private set; }
        public static List<HotkeySettings> hotKeys { get; private set; }
        public static bool ignoreHotkeyPress { get; set; } = false;

        public static void Init()
        {
            hotkeyForm = new HotkeyForm();
            hotkeyForm.HotkeyPress += Nyah;
        }

        public static void Nyah(ushort id, Keys key, Modifiers modifier)
        {
            HotkeySettings hotkeySetting = hotKeys.Find(x => x.HotkeyInfo.ID == id);

            if (hotkeySetting != null && !ignoreHotkeyPress)
            {
                TaskHandler.ExecuteTask(hotkeySetting.Task);
            }
        }

        public static void UpdateHotkeys(List<HotkeySettings> hotkeys, bool showFailedHotkeys)
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

        public static void RegisterHotkey(HotkeySettings hotkeySetting)
        {
            UnRegisterHotkey(hotkeySetting, false);

            if (hotkeySetting.HotkeyInfo.Status != HotkeyStatus.Registered && hotkeySetting.HotkeyInfo.IsValidHotkey)
            {
                hotkeyForm.RegisterHotkey(hotkeySetting.HotkeyInfo);

                if (hotkeySetting.HotkeyInfo.Status == HotkeyStatus.Registered)
                {
                    Logger.WriteLine("Hotkey registered: " + hotkeySetting);
                }
                else if (hotkeySetting.HotkeyInfo.Status == HotkeyStatus.Failed)
                {
                    Logger.WriteLine("Hotkey register failed: " + hotkeySetting);
                }
            }
            

            if (!hotKeys.Contains(hotkeySetting))
            {
                hotKeys.Add(hotkeySetting);
            }
        }

        public static void UnRegisterHotkey(HotkeySettings hotkeySetting, bool removeFromList)
        {
            if (hotkeySetting.HotkeyInfo.Status == HotkeyStatus.Registered)
            {
                hotkeyForm.UnRegisterHotkey(hotkeySetting.HotkeyInfo);

                if (hotkeySetting.HotkeyInfo.Status == HotkeyStatus.NotSet)
                {
                    Logger.WriteLine("Hotkey unregistered: " + hotkeySetting);
                }
                else if (hotkeySetting.HotkeyInfo.Status == HotkeyStatus.Failed)
                {
                    Logger.WriteLine("Hotkey unregister failed: " + hotkeySetting);
                }
            }

            if (removeFromList)
            {
                hotKeys.Remove(hotkeySetting);
            }
        }

        public static void RegisterAllHotkeys()
        {
            foreach (HotkeySettings hotkeySetting in hotKeys.ToArray())
            {
                RegisterHotkey(hotkeySetting);
            }
        }

        public static void UnRegisterAllHotkeys(bool removeFromList = true, bool temporary = false)
        {
            if (hotKeys != null)
            {
                foreach (HotkeySettings hotkeySetting in hotKeys.ToArray())
                {
                    UnRegisterHotkey(hotkeySetting, removeFromList);
                }
            }
        }

        public static void ShowFailedHotkeys()
        {
            List<HotkeySettings> failedHotkeysList = hotKeys.Where(x => x.HotkeyInfo.Status == HotkeyStatus.Failed).ToList();

            if (failedHotkeysList.Count > 0)
            {
                foreach(HotkeySettings hotkey in failedHotkeysList)
                {
                    MessageBox.Show(hotkey.ToString());
                }
            }
        }

        public static List<HotkeySettings> GetDefaultHotkeyList()
        {
            return new List<HotkeySettings>
            {
                new HotkeySettings(Tasks.RegionCapture, Keys.Control | Keys.PrintScreen),
                new HotkeySettings(Tasks.CaptureActiveMonitor, Keys.PrintScreen),
            };
        }
    }
}
