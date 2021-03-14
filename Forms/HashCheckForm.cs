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
    public partial class HashCheckForm : Form
    {
        //public bool isWorking { get; private set; } = false;
        public HashCheckForm()
        {
            InitializeComponent();
            cbHashType.Items.AddRange(Helpers.GetEnumDescriptions<HashType>());
            cbHashType.SelectedIndex = 0;
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

        private async void btnCheckHash_Click(object sender, EventArgs e)
        {
            /*if(!isWorking && string.IsNullOrEmpty(tbFilePathInput2.Text) && string.IsNullOrEmpty(tbFilePathInput.Text))
            {

            }*/
        }


    }
}
