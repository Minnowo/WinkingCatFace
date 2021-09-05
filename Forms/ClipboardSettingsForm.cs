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
    public partial class ClipboardSettingsForm : Form
    {
        public ClipboardSettingsForm()
        {
            InitializeComponent();

            foreach (ColorFormat colorformat in Enum.GetValues(typeof(ColorFormat)))
                comboBox3.Items.Add(colorformat);

            checkBox1.Checked = RegionCaptureOptions.AutoCopyImage;
            checkBox2.Checked = RegionCaptureOptions.AutoCopyColor;

            UpdateComboBox();
            UpdateTheme();
        }

        public void UpdateTheme()
        {
            ApplicationStyles.ApplyCustomThemeToControl(this);
        }

        public void UpdateComboBox()
        {
            comboBox3.SelectedItem = SettingsManager.MiscSettings.Default_Color_Format;
        }

        // autocopy image checkbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.AutoCopyImage = checkBox1.Checked;
        }

        // autocopy color checkbox
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.AutoCopyColor = checkBox2.Checked;
        }

        // color format combobox
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SettingsManager.MiscSettings.Default_Color_Format = (ColorFormat)comboBox3.SelectedItem;
        }
    }
}
