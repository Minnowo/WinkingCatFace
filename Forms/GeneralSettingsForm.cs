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
            SuspendLayout();
            MinimizeToTrayOnCloseCheckBox.Checked = MainFormSettings.minimizeToTray;
            ShowTrayIconCheckBox.Checked = MainFormSettings.showInTray;
            AlwaysOnTopCheckbox.Checked = MainFormSettings.alwaysOnTop;
            MinimizeToTrayOnStartCheckBox.Checked = MainFormSettings.startInTray;

            foreach (Tasks task in Enum.GetValues(typeof(Tasks)))
            {
                comboBox1.Items.Add(task);
                comboBox2.Items.Add(task);
                comboBox3.Items.Add(task);
            }
            if (!MainFormSettings.showInTray)
                MinimizeToTrayOnCloseCheckBox.Enabled = false;
            ResumeLayout();
            UpdateComboBox();
            UpdateTheme();
        }
        public void UpdateTheme()
        {
            ApplicationStyles.ApplyCustomThemeToControl(this);
        }
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox3.SelectedItem != null)
            {
                MainFormSettings.onTrayLeftClick = (Tasks)comboBox1.SelectedItem;
                MainFormSettings.onTrayDoubleLeftClick = (Tasks)comboBox2.SelectedItem;
                MainFormSettings.onTrayMiddleClick = (Tasks)comboBox3.SelectedItem;
                
            }
        }

        private void UpdateComboBox()
        {
            comboBox1.SelectedItem = MainFormSettings.onTrayLeftClick;
            comboBox2.SelectedItem = MainFormSettings.onTrayDoubleLeftClick;
            comboBox3.SelectedItem = MainFormSettings.onTrayMiddleClick;
        }

        private void AlwaysOnTopCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            MainFormSettings.alwaysOnTop = AlwaysOnTopCheckbox.Checked;
            Program.MainForm.TopMost = AlwaysOnTopCheckbox.Checked;
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
            Program.MainForm.niTrayIcon.Visible = ShowTrayIconCheckBox.Checked;
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
