using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinkingCat.HotkeyLib;

namespace WinkingCat
{
    public partial class HotkeySettingsForm : Form
    {
        public HotkeySettingsForm()
        {
            InitializeComponent();
            UpdateHotkeyControls();
        }

        public void UpdateHotkeyControls()
        {
            if (HotkeyManager.hotKeys != null)
                foreach(HotkeySettings hotkey in HotkeyManager.hotKeys)
                {
                    HotkeyInputControl control = new HotkeyInputControl(hotkey);
                    control.Margin = new Padding(0, 0, 0, 2);
                    control.Dock = DockStyle.Top;
                    //control.SelectedChanged += control_SelectedChanged;
                    //control.HotkeyChanged += control_HotkeyChanged;
                    //control.EditRequested += control_EditRequested;
                    this.Controls.Add(control);
                }
        }
    }
}
