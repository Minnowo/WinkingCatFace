
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{
    public class HotkeySettings
    {
        public Hotkey HotkeyInfo { get; set; }
        public Function Task { get; set; }
        public HotkeySettings(Function task, Keys hotkey = Keys.None)
        {
            Task = task;
            HotkeyInfo = new Hotkey(hotkey);
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
