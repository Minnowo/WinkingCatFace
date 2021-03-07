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
    public partial class PathSettingsForm : Form
    {
        public PathSettingsForm()
        {
            InitializeComponent();
            tbScreenshotFolder.Text = PathHelper.screenshotPath;
            UpdateTheme();
        }
        public void UpdateTheme()
        {
            ApplicationStyles.ApplyCustomThemeToControl(this);
        }
        private void OpenButton_Click(object sender, EventArgs e)
        {
            
        }

        private void PathBrowseButton_Click(object sender, EventArgs e)
        {
            string dir = PathHelper.AskChooseDirectory();
            if (!string.IsNullOrEmpty(dir))
            {
                tbScreenshotFolder.Text = dir;
            }
        }
    }
}
