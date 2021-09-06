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
        private bool isHandleCreated = false;

        public OCRForm(string path = "")
        {
            InitializeComponent();
            HandleCreated += OCRForm_HandleCreated;

            cbLanguage.Items.AddRange(Helper.GetEnumDescriptions<OCRSpaceLanguages>());
            cbLanguage.SelectedIndex = 8;

            tbFilePath.Text = path;
        }

        private void OCRForm_HandleCreated(object sender, EventArgs e)
        {
            isHandleCreated = true;
            UpdateTheme();
        }

        public void UpdateTheme()
        {
            if (ApplicationStyles.currentStyle.mainFormStyle.useImersiveDarkMode && isHandleCreated)
            {
                NativeMethods.UseImmersiveDarkMode(Handle, true);
                this.Icon = ApplicationStyles.whiteIcon; //Properties.Resources._3white;
            }
            else
            {
                NativeMethods.UseImmersiveDarkMode(Handle, false);
                this.Icon = ApplicationStyles.blackIcon; //Properties.Resources._3black;
            }
            ApplicationStyles.ApplyCustomThemeToControl(this);
        }

        private async void btnRunOCR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFilePath.Text))
                return;

            btnRunOCR.Enabled = false;

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
            btnRunOCR.Enabled = true;
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
                    clShowFailed.StaticBackColor = Color.Red;
                }
                else
                {
                    clShowFailed.StaticBackColor = Color.LightGreen;
                }
                lblFileSize.Text = Helper.SizeSuffix(PathHelper.GetFileSizeBytes(tbFilePath.Text));
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://translate.google.com/#auto/en/" + Uri.EscapeDataString(tbResult.Text));
        }
    }
}
