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
    public partial class GeneralSettingsForm : Form
    {
        public GeneralSettingsForm()
        {
            InitializeComponent();

            MinimizeToTrayOnCloseCheckBox.Checked = MainFormSettings.minimizeToTray;
            ShowTrayIconCheckBox.Checked = MainFormSettings.showInTray;
            AlwaysOnTopCheckbox.Checked = MainFormSettings.alwaysOnTop;
            MinimizeToTrayOnStartCheckBox.Checked = MainFormSettings.startInTray;

            if (!MainFormSettings.showInTray)
                MinimizeToTrayOnCloseCheckBox.Enabled = false;
        }

        private void AlwaysOnTopCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            MainFormSettings.alwaysOnTop = AlwaysOnTopCheckbox.Checked;
            Program.mainForm.TopMost = AlwaysOnTopCheckbox.Checked;
        }

        private void ShowTrayIconCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!ShowTrayIconCheckBox.Checked)
            {
                MainFormSettings.minimizeToTray = false;
                MinimizeToTrayOnCloseCheckBox.Enabled = false;
            }
            else
            {
                MinimizeToTrayOnCloseCheckBox.Enabled = true;
                MainFormSettings.minimizeToTray = MinimizeToTrayOnCloseCheckBox.Checked;
            }

            MainFormSettings.showInTray = ShowTrayIconCheckBox.Checked;
            Program.mainForm.niTrayIcon.Visible = ShowTrayIconCheckBox.Checked;
        }

        private void MinimizeToTrayOnCloseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainFormSettings.minimizeToTray = MinimizeToTrayOnCloseCheckBox.Checked;
        }

        private void MinimizeToTrayOnStartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainFormSettings.startInTray = MinimizeToTrayOnStartCheckBox.Checked;
        }
    }
}
