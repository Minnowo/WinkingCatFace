using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinkingCat.HelperLibs;

namespace WinkingCat
{
    public partial class HotkeySettingsForm : Form
    {
        public HotkeyInputControl selectedHotkey { get; private set; }
        public HotkeySettingsForm()
        {
            InitializeComponent();
            UpdateTheme();
            UpdateHotkeyControls();
        }
        public void UpdateTheme()
        {
            ApplicationStyles.ApplyCustomThemeToControl(this);
        }
        public void UpdateHotkeyControls()
        {
            if (HotkeyManager.hotKeys != null)
                foreach(HotkeySettings hotkey in HotkeyManager.hotKeys)
                {
                    AddControl(new HotkeyInputControl(hotkey));
                }
        }

        public void AddControl(HotkeyInputControl control)
        {
            control.Margin = new Padding(0, 0, 0, 2);
            control.Dock = DockStyle.Top;
            control.TaskChanged += control_SelectedChanged;
            control.HotkeyChanged += control_HotkeyChanged;
            control.SelectionChanged += control_CheckboxChanged;
            flowLayoutPanel1.Controls.Add(control);
        }

        private void control_HotkeyChanged(object sender, EventArgs e)
        {
            HotkeyManager.RegisterHotkey(((HotkeyInputControl)sender).setting);
            Logger.WriteLine(string.Format("Hotkey changed: {0}", ((HotkeyInputControl)sender).setting));
        }

        private void control_SelectedChanged(object sender, EventArgs e)
        {
            HotkeyManager.RegisterHotkey(((HotkeyInputControl)sender).setting);
            Logger.WriteLine(string.Format("Hotkey changed: {0}", ((HotkeyInputControl)sender).setting));
        }

        private void control_CheckboxChanged(object sender, EventArgs e)
        {
            if((HotkeyInputControl)sender != selectedHotkey)
                selectedHotkey?.Deselect();
            selectedHotkey = (HotkeyInputControl)sender;
        }

        // add button clicked
        private void button1_Click(object sender, EventArgs e)
        {
            HotkeySettings newHotkey = new HotkeySettings(Tasks.RegionCapture, Keys.None);
            HotkeyManager.RegisterHotkey(newHotkey);

            AddControl(new HotkeyInputControl(newHotkey));
        }

        // remove button clicked
        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedHotkey != null)
            {
                HotkeyManager.UnRegisterHotkey(selectedHotkey.setting, true);
                flowLayoutPanel1.Controls.Remove(selectedHotkey);
                selectedHotkey.Dispose();
                selectedHotkey = null;
            }
        }

        // up button pressed
        private void button4_Click(object sender, EventArgs e)
        {
            if (selectedHotkey != null && flowLayoutPanel1.Controls.Count > 1)
            {
                int index = flowLayoutPanel1.Controls.GetChildIndex(selectedHotkey);

                int newIndex;

                if (index == 0)
                {
                    newIndex = flowLayoutPanel1.Controls.Count - 1;
                }
                else
                {
                    newIndex = index - 1;
                }

                flowLayoutPanel1.Controls.SetChildIndex(selectedHotkey, newIndex);
                HotkeyManager.hotKeys.Swap(index, newIndex);
            }
        }

        // down button pressed
        private void button5_Click(object sender, EventArgs e)
        {
            if (selectedHotkey != null && flowLayoutPanel1.Controls.Count > 1)
            {
                int index = flowLayoutPanel1.Controls.GetChildIndex(selectedHotkey);

                int newIndex;

                if (index == flowLayoutPanel1.Controls.Count - 1)
                {
                    newIndex = 0;
                }
                else
                {
                    newIndex = index + 1;
                }

                flowLayoutPanel1.Controls.SetChildIndex(selectedHotkey, newIndex);
                HotkeyManager.hotKeys.Swap(index, newIndex);
            }
        }

        // restore default button click
        private void button6_Click(object sender, EventArgs e)
        {
            foreach(HotkeyInputControl control in flowLayoutPanel1.Controls)
                HotkeyManager.UnRegisterHotkey(control.setting, true);
            
            flowLayoutPanel1.Controls.Clear();
            HotkeyManager.UpdateHotkeys(HotkeyManager.GetDefaultHotkeyList(), true);
            UpdateHotkeyControls();
        }
    }
}
