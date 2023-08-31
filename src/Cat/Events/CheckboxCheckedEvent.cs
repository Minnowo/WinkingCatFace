using System;
using System.Windows.Forms;

namespace WinkingCat
{
    public class CheckboxCheckedEvent : EventArgs
    {
        public bool isChecked { get; private set; }
        public CheckBox checkbox { get; private set; }
        public CheckboxCheckedEvent(CheckBox checkbox)
        {
            this.checkbox = checkbox;
            this.isChecked = checkbox.Checked;
        }
        public override string ToString()
        {
            return $"checkbox: \"{checkbox.Name}\", text: \"{checkbox.Text}\", checked: \"{isChecked}\"";
        }
    }
}
