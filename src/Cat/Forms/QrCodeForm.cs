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
using ZXing;
using System.Threading;

namespace WinkingCat
{
    public partial class BarcodeForm : BaseForm
    {
        public BarcodeForm()
        {
            InitializeComponent();
            this.Text = "Qr Code";
            foreach (BarcodeFormat format in new BarcodeFormat[] { BarcodeFormat.AZTEC, BarcodeFormat.CODABAR, BarcodeFormat.CODE_39, BarcodeFormat.CODE_128, BarcodeFormat.DATA_MATRIX, BarcodeFormat.PDF_417, BarcodeFormat.QR_CODE })
            {
                cmFormat.Items.Add(format);
            }
            cmFormat.SelectedItem = BarcodeFormat.QR_CODE;
            pbQRDisplay.SizeMode = PictureBoxSizeMode.CenterImage;

            base.RegisterEvents();
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
            if (!IsReady)
                return;
            
            ClearQRCode();

            int size = Math.Min(pbQRDisplay.Width, pbQRDisplay.Height);
            pbQRDisplay.Image = Helper.CreateQRCode(text, size, (BarcodeFormat)cmFormat.SelectedItem);
            pbQRDisplay.BackColor = Color.White;
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
            if (!IsReady)
                return;

            RegionCaptureHelper.RequestFormsHide(false, true);

            if (RegionCaptureHelper.GetRegionResultImage(out Image i))
            {
                using (Bitmap img = (Bitmap)i)
                {
                    if (img != null)
                    {
                        Decode(img);
                    }
                }
                this.ForceActivate();
            }

            RegionCaptureHelper.RequestFormsHide(true, false);
        }

        private void bFromFile_Click(object sender, EventArgs e)
        {
            if (!IsReady)
                return;
            
            string[] res = ImageHelper.OpenImageFileDialog(false, Program.MainForm);
            if (res == null || res.Length < 1)
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
            if (!IsReady)
                return;
            
            if (Clipboard.ContainsImage())
            {
                using (Bitmap img = ClipboardHelper.GetImage())
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
