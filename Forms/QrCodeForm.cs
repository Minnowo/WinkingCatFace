﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinkingCat.HelperLibs;
using ZXing;
using System.Threading;

namespace WinkingCat
{
    public partial class BarcodeForm : Form
    {
        private bool isReady;
        private bool isHandleCreated = false;
        public BarcodeForm()
        {
            InitializeComponent();
            this.HandleCreated += BarcodeForm_HandleCreated;
            StyleChanged += BarcodeForm_StyleChanged;
            this.Text = "Qr Code";
            foreach (BarcodeFormat format in new BarcodeFormat[] { BarcodeFormat.AZTEC, BarcodeFormat.CODABAR, BarcodeFormat.CODE_39, BarcodeFormat.CODE_128, BarcodeFormat.DATA_MATRIX, BarcodeFormat.PDF_417, BarcodeFormat.QR_CODE })
            {
                cmFormat.Items.Add(format);
            }
            cmFormat.SelectedItem = BarcodeFormat.QR_CODE;
            pbQRDisplay.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void BarcodeForm_HandleCreated(object sender, EventArgs e)
        {
            isHandleCreated = true;
            UpdateTheme();
        }

        private void BarcodeForm_StyleChanged(object sender, EventArgs e)
        {
            UpdateTheme();
        }

        private void UpdateTheme()
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

            this.BackColor = ApplicationStyles.currentStyle.mainFormStyle.backgroundColor;
            ApplicationStyles.ApplyCustomThemeToControl(this);
            Refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            EncodeText(tbTextInput.Text);
        }

        private void ClearQRCode()
        {
            if (pbQRDisplay.Image != null)
            {
                Image temp = pbQRDisplay.Image;
                pbQRDisplay.Image = null;
                temp.Dispose();
            }
        }

        private void EncodeText(string text)
        {
            if (!isReady)
                return;
            
            ClearQRCode();

            int size = Math.Min(pbQRDisplay.Width, pbQRDisplay.Height);
            pbQRDisplay.Image = Helper.CreateQRCode(text, size, (BarcodeFormat)cmFormat.SelectedItem);
            pbQRDisplay.BackColor = Color.White;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            isReady = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            EncodeText(tbTextInput.Text);
        }

        private void Decode(Bitmap bmp)
        {
            string output = "";

            string[] results = Helper.BarcodeScan(bmp);

            if (results != null)
            {
                output = string.Join(Environment.NewLine + Environment.NewLine, results);
            }

            tbTextOutput.Text = output;
        }

        private void bFromScreen_Click(object sender, EventArgs e)
        {
            if (!isReady)
                return;
            
            if (Visible)
            {
                Hide();
                Thread.Sleep(SettingsManager.MainFormSettings.Wait_Hide_Time);
            }

            using(Bitmap img = (Bitmap)RegionCaptureHelper.GetRegionResultImage())
            {
                if(img != null)
                {
                    Decode(img);
                }
            }
            this.ForceActivate();
        }

        private void bFromFile_Click(object sender, EventArgs e)
        {
            if (!isReady)
                return;
            
            string[] res = ImageHelper.OpenImageFileDialog(false, Program.MainForm);
            if (res == null | res.Length < 1)
                return;

            using (Bitmap img = ImageHelper.LoadImageAsBitmap(res[0]))
            {
                if (img != null)
                {
                    Decode(img);
                }
            }
            GC.Collect();
        }

        private void bFromClipboard_Click(object sender, EventArgs e)
        {
            if (!isReady)
                return;
            
            if (Clipboard.ContainsImage())
            {
                using (Bitmap img = (Bitmap)Clipboard.GetImage())
                {
                    if (img != null)
                    {
                        Decode(img);
                    }
                }
            }
        }

        private void btnCopyCode_Click(object sender, EventArgs e)
        {
            if (pbQRDisplay.Image != null)
            {
                ClipboardHelper.CopyImage(pbQRDisplay.Image);
            }
        }
    }
}
