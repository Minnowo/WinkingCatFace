using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WinkingCat.HelperLibs;
using WinkingCat.Uploaders;

namespace WinkingCat
{

    public partial class OCRForm : BaseForm
    {
        public OCRForm(string path = "")
        {
            InitializeComponent();

            cbLanguage.Items.AddRange(Helper.GetEnumDescriptions<OCRSpaceLanguages>());
            cbLanguage.SelectedIndex = 8;

            tbFilePath.Text = path;

            base.RegisterEvents();
        }


        private async void btnRunOCR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFilePath.Text))
                return;

            btnRunOCR.Enabled = false;

            string result = "";
            lblState.Text = "Waiting...";
            if (Path.GetExtension(tbFilePath.Text).ToLower() == ".pdf")
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
                if (fileSize > OCRManager.maxUploadSizeBytes)
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
