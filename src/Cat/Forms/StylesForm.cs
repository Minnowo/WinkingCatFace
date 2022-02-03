using System;
using System.Windows.Forms;
using WinkingCat.HelperLibs;
using System.IO;

namespace WinkingCat
{
    public partial class StylesForm : BaseForm
    {
        public StylesForm()
        {
            InitializeComponent();
            this.FormClosing += Form_Closing;

            propertyGrid1.SelectedObject = SettingsManager.MainFormSettings;
            propertyGrid2.SelectedObject = SettingsManager.RegionCaptureSettings;
            propertyGrid3.SelectedObject = SettingsManager.ClipSettings;

            base.RegisterEvents();
        }

        private void Form_Closing(object sender, EventArgs e)
        {
            string dir = PathHelper.CurrentDirectory;

            Directory.SetCurrentDirectory(PathHelper.BaseDirectory);
            SettingsManager.SaveClipSettings();
            SettingsManager.SaveMainFormSettings();
            SettingsManager.SaveRegionCaptureSettings();
            SettingsManager.SaveMiscSettings();
            SettingsManager.SaveHotkeySettings(HotkeyManager.hotKeys);
            Directory.SetCurrentDirectory(dir);
        }
    }
}
