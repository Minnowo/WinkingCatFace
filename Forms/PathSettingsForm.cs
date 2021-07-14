using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using WinkingCat.HelperLibs;

namespace WinkingCat
{
    public partial class PathSettingsForm : Form
    {
        public PathSettingsForm()
        {
            InitializeComponent();

            comboBox1.Items.AddRange(new ImageFormat[5] { ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Bmp, ImageFormat.Gif, ImageFormat.Tiff });
            comboBox1.SelectedItem = InternalSettings.Default_Image_Format;

            tbScreenshotFolder.Text = PathHelper.GetScreenshotFolder();
            checkBox1.Checked = PathHelper.UseCustomScreenshotPath;
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            PathHelper.UseCustomScreenshotPath = checkBox1.Checked;
        }

        private void tbScreenshotFolder_TextChanged(object sender, EventArgs e)
        {
            PathHelper.screenshotCustomPath = tbScreenshotFolder.Text;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ImageHelper.defaultImageFormat = (System.Drawing.Imaging.ImageFormat)comboBox1.SelectedItem;
        }
    }
}
