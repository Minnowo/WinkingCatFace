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
    public partial class StylesForm : Form
    {
        public StylesForm()
        {
            InitializeComponent();
            this.HandleCreated += HandleCreated_Event;
            this.FormClosing += Form_Closing;
            ApplicationStyles.UpdateStylesEvent += HandleCreated_Event;

            propertyGrid1.SelectedObject = SettingsManager.MainFormSettings;
            propertyGrid2.SelectedObject = SettingsManager.RegionCaptureSettings;
        }

        public void UpdateTheme()
        {
            SettingsManager.ApplyImmersiveDarkTheme(this, IsHandleCreated);
            ApplicationStyles.ApplyCustomThemeToControl(this);
            Refresh();
        }

        private void SaveSettingsToDisk()
        {
            SettingsManager.SaveClipSettings();
            SettingsManager.SaveMainFormSettings();
            SettingsManager.SaveRegionCaptureSettings();
            SettingsManager.SaveMiscSettings();
            SettingsManager.SaveHotkeySettings(HotkeyManager.hotKeys);
        }

        private void HandleCreated_Event(object sender ,EventArgs e)
        {
            UpdateTheme();
        }

        private void Form_Closing(object sender, EventArgs e)
        {
            SaveSettingsToDisk();
        }
    }
}
