using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinkingCat
{
    public partial class StylesForm : Form
    {
        public Form activeForm;
        public StylesForm()
        {
            InitializeComponent();
        }
        private void OpenChildForm(Form childForm)
        {
            SuspendLayout();
            activeForm?.Close();
            activeForm = childForm;
            childForm.TopMost = false;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlForm.Controls.Add((Form)childForm);
            pnlForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            ResumeLayout();
        }

        private void btnMainForm_Click(object sender, EventArgs e)
        {
            OpenChildForm(new RegionCaptureStylesForm());
        }

        private void btnRegionCapture_Click(object sender, EventArgs e)
        {

        }

        private void btnClips_Click(object sender, EventArgs e)
        {

        }
    }
}
