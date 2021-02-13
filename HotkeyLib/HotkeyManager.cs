using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinkingCat.HelperLibs;
using System.Windows.Forms;

namespace WinkingCat.HotkeyLib
{
    public class HotkeyManager
    {
        public HotkeyForm hotkeyForm { get; private set; }
        public List<HotkeySettings> hotKeys { get; private set; }

        public HotkeyManager()
        {
            hotkeyForm = new HotkeyForm();
            hotkeyForm.HotkeyPress += Nyah;
        }

        public void Nyah(ushort id, Keys key, Modifiers modifier)
        {
            HotkeySettings hotkeySetting = hotKeys.Find(x => x.HotkeyInfo.ID == id);

            if (hotkeySetting != null)
            {
                TaskHandler.ExecuteTask(hotkeySetting.Task);
            }
        }

        public void UpdateHotkeys(List<HotkeySettings> hotkeys, bool showFailedHotkeys)
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

        public void RegisterHotkey(HotkeySettings hotkeySetting)
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

        public void UnRegisterHotkey(HotkeySettings hotkeySetting, bool removeFromList)
        {
            if (hotkeySetting.HotkeyInfo.Status == HotkeyStatus.Registered)
            {
                hotkeyForm.UnRegisterHotkey(hotkeySetting.HotkeyInfo);

                if (hotkeySetting.HotkeyInfo.Status == HotkeyStatus.NotSet)
                {
                    Console.WriteLine("Hotkey unregistered: " + hotkeySetting);
                }
                else if (hotkeySetting.HotkeyInfo.Status == HotkeyStatus.Failed)
                {
                    Console.WriteLine("Hotkey unregister failed: " + hotkeySetting);
                }
            }

            if (removeFromList)
            {
                hotKeys.Remove(hotkeySetting);
            }
        }

        public void RegisterAllHotkeys()
        {
            foreach (HotkeySettings hotkeySetting in hotKeys.ToArray())
            {
                RegisterHotkey(hotkeySetting);
            }
        }

        public void UnRegisterAllHotkeys(bool removeFromList = true, bool temporary = false)
        {
            if (hotKeys != null)
            {
                foreach (HotkeySettings hotkeySetting in hotKeys.ToArray())
                {
                    UnRegisterHotkey(hotkeySetting, removeFromList);
                }
            }
        }

        public void ShowFailedHotkeys()
        {
            List<HotkeySettings> failedHotkeysList = hotKeys.Where(x => x.HotkeyInfo.Status == HotkeyStatus.Failed).ToList();

            if (failedHotkeysList.Count > 0)
            {
                foreach(HotkeySettings hotkey in failedHotkeysList)
                {
                    Console.WriteLine(hotkey.ToString());
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
