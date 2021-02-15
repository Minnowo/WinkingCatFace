
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{
    public class HotkeySettings
    {
        public HotkeyInfo HotkeyInfo { get; set; }
        public Tasks Task { get; set; }
        public HotkeySettings(Tasks task, Keys hotkey = Keys.None)
        {
            Task = task;
            HotkeyInfo = new HotkeyInfo(hotkey);
        }

        public override string ToString()
        {
            if (HotkeyInfo != null)
            {
                return string.Format("Hotkey: {0}, Task: {1}", HotkeyInfo, Task);
            }

            return "";
        }
    }
}
