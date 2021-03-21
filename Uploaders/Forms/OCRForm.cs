using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WinkingCat.HelperLibs;

namespace WinkingCat.Uploaders
{
    
    public partial class OCRForm : Form
    {
        private bool isHangleCreated = false;
        public OCRForm(string path = "")
        {
            InitializeComponent();
            HandleCreated += OCRForm_HandleCreated;

            cbLanguage.Items.AddRange(Helpers.GetEnumDescriptions<OCRSpaceLanguages>());
            cbLanguage.SelectedIndex = 6;

            tbFilePath.Text = path;
        }

        private void OCRForm_HandleCreated(object sender, EventArgs e)
        {
            isHangleCreated = true;
            UpdateTheme();
        }

        public void UpdateTheme()
        {
            if (ApplicationStyles.currentStyle.mainFormStyle.useImersiveDarkMode && isHandleCreated)
            {
                NativeMethods.UseImmersiveDarkMode(Handle, true);
                this.Icon = Properties.Resources._3white;
            }
            else
            {
                NativeMethods.UseImmersiveDarkMode(Handle, false);
                this.Icon = Properties.Resources._3black;
            }
            ApplicationStyles.ApplyCustomThemeToControl(this);
        }

        private async void btnRunOCR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFilePath.Text))
                return;

            string result = "";
            lblState.Text = "Waiting...";
            if(Path.GetExtension(tbFilePath.Text).ToLower() == ".pdf")
            {
                result = await OCRManager.UploadPDF(tbFilePath.Text, cbLanguage.SelectedIndex);
                if (!string.IsNullOrEmpty(result))
                    tbResult.Text = result;
                else
                    tbResult.Text = "the ocr result is empty, either the file contains no text, or the file size exceeds 1MB";
            }
            else
            {
                result = await OCRManager.UploadImage(tbFilePath.Text, cbLanguage.SelectedIndex);
                if (!string.IsNullOrEmpty(result))
                    tbResult.Text = result;
                else
                    tbResult.Text = "the ocr result is empty, either the file contains no text, or the file size exceeds 1MB";
            }
            lblState.Text = "Idle";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            tbFilePath.Text = PathHelper.AskChooseFile();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbResult.Text))
            {
                ClipboardHelper.CopyStringDefault(tbResult.Text);
            }
        }

        private void tbFilePath_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(tbFilePath.Text))
            {
                long fileSize = PathHelper.GetFileSizeBytes(tbFilePath.Text);
                if(fileSize > OCRManager.maxUploadSizeBytes)
                {
                    lblFileSize.BackColor = Color.Red;
                }
                else
                {
                    lblFileSize.BackColor = Color.LightGreen;
                }
                lblFileSize.Text = Helpers.SizeSuffix(PathHelper.GetFileSizeBytes(tbFilePath.Text));
            }
        }
    }
}
