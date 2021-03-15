using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using WinkingCat.HelperLibs;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace WinkingCat
{
    public partial class HashCheckForm : Form
    {
        private HashCheck hashCheck;
        private bool isHandleCreated = false;
        public HashCheckForm()
        {
            InitializeComponent();
            cbHashType.Items.AddRange(Helpers.GetEnumDescriptions<HashType>());
            cbHashType.SelectedIndex = 0;
            hashCheck = new HashCheck();
            hashCheck.FileCheckProgressChanged += HashCheck_FileCheckProgressChanged;
            HandleCreated += HashCheckForm_HandleCreated;
            ApplicationStyles.UpdateStylesEvent += ApplicationStyles_UpdateStylesEvent;
        }

        private void ApplicationStyles_UpdateStylesEvent(object sender, EventArgs e)
        {
            UpdateTheme();
        }

        private void HashCheckForm_HandleCreated(object sender, EventArgs e)
        {
            isHandleCreated = true;
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
            Refresh();
        }

        private void HashCheck_FileCheckProgressChanged(float progress)
        {
            pbProgressDone.Value = (int)(progress);
            lProgress.Text = ((int)(progress)).ToString() + "%";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string path = PathHelper.AskChooseFile();
            if (!string.IsNullOrEmpty(path))
            {
                tbFilePathInput.Text = path;
            }
        }

        private void btnBrowse2_Click(object sender, EventArgs e)
        {
            string path = PathHelper.AskChooseFile();
            if (!string.IsNullOrEmpty(path))
            {
                tbFilePathInput2.Text = path;
            }
        }


        private void btnCopyFileHash_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbFileHash.Text))
            {
                ClipboardHelpers.CopyStringDefault(tbFileHash.Text);
            }
        }

        private void btnCopyInput_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbHashInput.Text))
            {
                ClipboardHelpers.CopyStringDefault(tbHashInput.Text);
            }
        }

        private void btnCopyTarget_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbHashTarget.Text))
            {
                ClipboardHelpers.CopyStringDefault(tbHashTarget.Text);
            }
        }

        private void btnCopyFileHash2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbHashTarget.Text))
            {
                ClipboardHelpers.CopyStringDefault(tbFileHash2.Text);
            }
        }

        private void btnPasteFileHash_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                tbFileHash.Text = Clipboard.GetText();
            }
        }

        private void btnPasteInput_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                tbHashInput.Text = Clipboard.GetText();
            }
        }

        private void btnPasteTarget_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                tbHashTarget.Text = Clipboard.GetText();
            }
        }

        private void btnPasteFileHash2_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                tbFileHash2.Text = Clipboard.GetText();
            }
        }


        private void btnClearFileHash_Click(object sender, EventArgs e)
        {
            tbFileHash.Text = "";
        }

        private void btnClearHashInput_Click(object sender, EventArgs e)
        {
            tbHashInput.Text = "";
        }

        private void btnClearHashTarget_Click(object sender, EventArgs e)
        {
            tbHashTarget.Text = "";
        }

        private void btnClearFileHash2_Click(object sender, EventArgs e)
        {
            tbFileHash2.Text = "";
        }

        private void ResetTextBoxColor()
        {
            tbFileHash.BackColor = ApplicationStyles.currentStyle.mainFormStyle.lightBackgroundColor;
            tbFileHash2.BackColor = ApplicationStyles.currentStyle.mainFormStyle.lightBackgroundColor;
            tbHashInput.BackColor = ApplicationStyles.currentStyle.mainFormStyle.lightBackgroundColor;
            tbHashTarget.BackColor = ApplicationStyles.currentStyle.mainFormStyle.lightBackgroundColor;
        }

        private void UpdateTextBoxColor()
        {
            ResetTextBoxColor();
            if (tbFileHash.Text.Equals(tbFileHash2.Text, StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(tbFileHash.Text) && !string.IsNullOrEmpty(tbFileHash2.Text))
            {
                tbFileHash.BackColor = Color.FromArgb(200, 255, 200);
                tbFileHash2.BackColor = Color.FromArgb(200, 255, 200);
            }

            if (tbFileHash.Text.Equals(tbHashTarget.Text, StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(tbFileHash.Text) && !string.IsNullOrEmpty(tbHashTarget.Text))
            {
                tbFileHash.BackColor = Color.FromArgb(200, 255, 200);
                tbHashTarget.BackColor = Color.FromArgb(200, 255, 200);
            }

            if (tbFileHash2.Text.Equals(tbHashTarget.Text, StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(tbFileHash2.Text) && !string.IsNullOrEmpty(tbHashTarget.Text))
            {
                tbFileHash2.BackColor = Color.FromArgb(200, 255, 200);
                tbHashTarget.BackColor = Color.FromArgb(200, 255, 200);
            }

            if (tbHashInput.Text.Equals(tbHashTarget.Text, StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(tbHashInput.Text) && !string.IsNullOrEmpty(tbHashTarget.Text))
            {
                tbHashInput.BackColor = Color.FromArgb(200, 255, 200);
                tbHashTarget.BackColor = Color.FromArgb(200, 255, 200);
            }

            if (tbHashInput.Text.Equals(tbFileHash.Text, StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(tbHashInput.Text) && !string.IsNullOrEmpty(tbFileHash.Text))
            {
                tbHashInput.BackColor = Color.FromArgb(200, 255, 200);
                tbFileHash.BackColor = Color.FromArgb(200, 255, 200);
            }

            if (tbHashInput.Text.Equals(tbFileHash2.Text, StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(tbHashInput.Text) && !string.IsNullOrEmpty(tbFileHash2.Text))
            {
                tbHashInput.BackColor = Color.FromArgb(200, 255, 200);
                tbFileHash2.BackColor = Color.FromArgb(200, 255, 200);
            }
        }

        private async void btnCheckHash_Click(object sender, EventArgs e)
        {
            UpdateTextBoxColor();
            if (hashCheck.isRunning)
            {
                hashCheck.Stop();
            }
            else
            {
                if (string.IsNullOrEmpty(tbFilePathInput.Text) && string.IsNullOrEmpty(tbFilePathInput2.Text))
                    return;

                btnCheckHash.Text = "Stop";
                lProgress.Text = "0%";
                pbProgressDone.Value = 0;

                HashType hash = (HashType)cbHashType.SelectedIndex;
                string result;

                if (!string.IsNullOrEmpty(tbFilePathInput.Text))
                {
                    tbFileHash.Text = "";
                    result = await hashCheck.Start(tbFilePathInput.Text, hash);

                    if (!string.IsNullOrEmpty(result))
                    {
                        tbFileHash.Text = result.ToUpperInvariant();
                    }
                }

                
                if (!string.IsNullOrEmpty(tbFilePathInput2.Text))
                {
                    pbProgressDone.Value = 0;
                    tbFileHash2.Text = "";
                    result = await hashCheck.Start(tbFilePathInput2.Text, hash);

                    if (!string.IsNullOrEmpty(result))
                    {
                        tbFileHash2.Text = result.ToUpperInvariant();
                    }
                }

                btnCheckHash.Text = "Check";
                UpdateTextBoxColor();
            }
        }

        public class HashCheck
        {
            public bool isRunning { get; private set; } = false;
            public delegate void ProgressChanged(float progress);
            public event ProgressChanged FileCheckProgressChanged;

            private CancellationTokenSource cts;

            private void OnProgressChanged(float percentage)
            {
                FileCheckProgressChanged?.Invoke(percentage);
            }

            public async Task<string> Start(string filePath, HashType hashType)
            {
                string result = null;

                if (!isRunning && !string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    isRunning = true;

                    Progress<float> progress = new Progress<float>(OnProgressChanged);
                    cts = new CancellationTokenSource();
                    result = await Task.Run(() =>
                    {
                        try
                        {
                            return HashCheckThread(filePath, hashType, progress, cts.Token);
                        }
                        catch (OperationCanceledException)
                        {
                        }

                        return null;
                    }, cts.Token);

                    isRunning = false;
                }

                return result;
            }

            public void Stop()
            {
                if (cts != null)
                {
                    cts.Cancel();
                }
            }

            private string HashCheckThread(string filePath, HashType hashType, IProgress<float> progress, CancellationToken ct)
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (HashAlgorithm hash = GetHashType(hashType))
                using (CryptoStream cs = new CryptoStream(stream, hash, CryptoStreamMode.Read))
                {
                    long bytesRead, totalRead = 0;
                    byte[] buffer = new byte[8192];
                    Stopwatch timer = Stopwatch.StartNew();

                    while ((bytesRead = cs.Read(buffer, 0, buffer.Length)) > 0 && !ct.IsCancellationRequested)
                    {
                        totalRead += bytesRead;

                        if (timer.ElapsedMilliseconds > 200)
                        {
                            float percentage = (float)totalRead / stream.Length * 100;
                            progress.Report(percentage);

                            timer.Reset();
                            timer.Start();
                        }
                    }

                    if (ct.IsCancellationRequested)
                    {
                        progress.Report(0);

                        ct.ThrowIfCancellationRequested();
                    }
                    else
                    {
                        progress.Report(100);

                        string[] hex = TranslatorHelper.BytesToHexadecimal(hash.Hash);
                        return string.Concat(hex);
                    }
                }

                return null;
            }

            public HashAlgorithm GetHashType(HashType type)
            {
                switch (type)
                {
                    case HashType.CRC32:
                        return new Crc32();
                    case HashType.CRC64:
                        return new Crc64(0x42F0E1EBA9EA3693);
                    case HashType.MD5:
                        return new MD5CryptoServiceProvider();
                    case HashType.SHA1:
                        return new SHA1CryptoServiceProvider();
                    case HashType.SHA256:
                        return new SHA256CryptoServiceProvider();
                    case HashType.SHA384:
                        return new SHA384CryptoServiceProvider();
                    case HashType.SHA512:
                        return new SHA512CryptoServiceProvider();
                }
                return null;
            }
        }


    }
}
