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
    public partial class StylesForm : Form
    {
        public Form activeForm;
        private bool isHandleCreated = false;
        public StylesForm()
        {
            InitializeComponent();
            this.HandleCreated += StylesForm_HandleCreated;
            ApplicationStyles.UpdateStylesEvent += ApplicationStyles_UpdateStylesEvent;
        }

        private void StylesForm_HandleCreated(object sender, EventArgs e)
        {
            isHandleCreated = true;
            UpdateTheme();
        }

        private void ApplicationStyles_UpdateStylesEvent(object sender, EventArgs e)
        {
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
            //this.BackColor = ApplicationStyles.currentStyle.mainFormStyle.backgroundColor;
            ApplicationStyles.ApplyCustomThemeToControl(this);
            Refresh();
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
            OpenChildForm(new MainWindowStylesForm());
        }

        private void btnRegionCapture_Click(object sender, EventArgs e)
        {
            OpenChildForm(new RegionCaptureStylesForm());
        }

        private void btnClips_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ClipStylesForm());
        }
    }
}
