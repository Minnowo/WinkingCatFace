using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using WinkingCat.HelperLibs;
using WinkingCat.Native;

namespace WinkingCat
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Hotkey
    {
        [Browsable(false)]
        [XmlElement("Keybind_Callback")]
        public int Function_As_Int
        {
            get { return (int)Callback; }
            set { Callback = (Function)value; }
        }

        [Browsable(false)]
        [XmlElement("Keys")]
        public int Keys_As_Int
        {
            get { return (int)Keys; }
            set { Keys = (Keys)value; }
        }

        [XmlIgnore]
        public Function Callback { get; set; }

        [XmlIgnore]
        public Keys Keys { get; set; }

        [XmlIgnore]
        public ushort ID { get; set; }

        [XmlIgnore]
        public HotkeyStatus Status { get; set; }

        [XmlIgnore]
        public Keys KeyCode => Keys & Keys.KeyCode;

        [XmlIgnore]
        public Keys ModifiersKeys => Keys & Keys.Modifiers;

        [XmlIgnore]
        public bool Control => Keys.HasFlag(Keys.Control);

        [XmlIgnore]
        public bool Shift => Keys.HasFlag(Keys.Shift);

        [XmlIgnore]
        public bool Alt => Keys.HasFlag(Keys.Alt);

        [XmlIgnore]
        public bool Win { get; set; }

        [XmlIgnore]
        public Modifiers ModifiersEnum
        {
            get
            {
                Modifiers modifiers = Modifiers.None;

                if (Alt) modifiers |= Modifiers.Alt;
                if (Control) modifiers |= Modifiers.Control;
                if (Shift) modifiers |= Modifiers.Shift;
                if (Win) modifiers |= Modifiers.Win;

                return modifiers;
            }
        }

        [XmlIgnore]
        public bool IsOnlyModifiers => KeyCode == Keys.ControlKey || KeyCode == Keys.ShiftKey || KeyCode == Keys.Menu || (KeyCode == Keys.None && Win);

        [XmlIgnore]
        public bool IsValidHotkey => KeyCode != Keys.None && !IsOnlyModifiers;

        public Hotkey()
        {
            Status = HotkeyStatus.NotSet;
            Callback = Function.None;
        }

        public Hotkey(Keys hotkey) : this()
        {
            Keys = hotkey;
        }

        public Hotkey(Keys hotkey, Function function) : this(hotkey)
        {
            Callback = function;
        }

        public override string ToString()
        {
            string text = "";

            if (Control)
            {
                text += "Ctrl + ";
            }

            if (Shift)
            {
                text += "Shift + ";
            }

            if (Alt)
            {
                text += "Alt + ";
            }

            if (Win)
            {
                text += "Win + ";
            }

            if (IsOnlyModifiers)
            {
                text += "...";
            }
            else if (KeyCode == Keys.Back)
            {
                text += "Backspace";
            }
            else if (KeyCode == Keys.Return)
            {
                text += "Enter";
            }
            else if (KeyCode == Keys.Capital)
            {
                text += "Caps Lock";
            }
            else if (KeyCode == Keys.Next)
            {
                text += "Page Down";
            }
            else if (KeyCode == Keys.Scroll)
            {
                text += "Scroll Lock";
            }
            else if (KeyCode >= Keys.D0 && KeyCode <= Keys.D9)
            {
                text += (KeyCode - Keys.D0).ToString();
            }
            else if (KeyCode >= Keys.NumPad0 && KeyCode <= Keys.NumPad9)
            {
                text += "Numpad " + (KeyCode - Keys.NumPad0).ToString();
            }
            else
            {
                text += ToStringWithSpaces(KeyCode);
            }

            return text;
        }

        private string ToStringWithSpaces(Keys key)
        {
            string name = key.ToString();

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < name.Length; i++)
            {
                if (i > 0 && char.IsUpper(name[i]))
                {
                    result.Append(" " + name[i]);
                }
                else
                {
                    result.Append(name[i]);
                }
            }

            return result.ToString();
        }
    }
}
