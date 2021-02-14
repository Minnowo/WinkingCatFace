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
    public partial class SettingsForm : Form
    {
        public Form activeForm { get; private set; }
        public SettingsForm()
        {
            InitializeComponent();

            #region Buttons
            SideButton1.Click += SideButton1_Click;
            SideButton2.Click += SideButton2_Click;
            SideButton3.Click += SideButton3_Click;
            SideButton4.Click += SideButton4_Click;
            SideButton5.Click += SideButton5_Click;
            #endregion

            OpenChildForm(new GeneralSettingsForm());
        }

        #region MainForm events

        
        #endregion

        #region Button events
        private void SideButton1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GeneralSettingsForm());
        }

        private void SideButton2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new PathSettingsForm());
        }

        private void SideButton3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new UploadSettingsForm());
        }

        private void SideButton4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ClipboardSettingsForm());
        }

        private void SideButton5_Click(object sender, EventArgs e)
        {
            OpenChildForm(new HotkeySettingsForm());
        }
        #endregion

        private void OpenChildForm(Form childForm)
        {
            SuspendLayout();
            activeForm?.Close();
            activeForm = childForm;
            childForm.TopMost = false;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            //childForm.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            FormDockPanel.Controls.Add(childForm);
            FormDockPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            ResumeLayout();
        }
    }

}
