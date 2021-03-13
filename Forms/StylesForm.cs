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
        private IntPtr hwnd;
        public StylesForm()
        {
            InitializeComponent();
            this.Text = "Styles";
            this.HandleCreated += StylesForm_HandleCreated;
            ApplicationStyles.UpdateStylesEvent += ApplicationStyles_UpdateStylesEvent;
            this.FormClosing += new FormClosingEventHandler(OnFormClosing_Event);
            OpenChildForm(new MainWindowStylesForm());
        }

        private void StylesForm_HandleCreated(object sender, EventArgs e)
        {
            isHandleCreated = true;
            // handle gets disposed when changing property grid value or something idk, 
            // need this otherwise closing and re opening the form, 
            // then trying to change a value throws an exception
            hwnd = Handle; 
            UpdateTheme();
        }

        private void ApplicationStyles_UpdateStylesEvent(object sender, EventArgs e)
        {
            UpdateTheme();
        }

        private void OnFormClosing_Event(object sender, EventArgs e)
        {
            SaveSettingsToDisk();
        }

        private void SaveSettingsToDisk()
        {
            if (SettingsManager.SaveMainFormStyles())
                Logger.WriteLine("MainForm Styles Saved Successfully");
            if (SettingsManager.SaveRegionCaptureStyles())
                Logger.WriteLine("RegionCapture Styles Saved Successfully");
            if (SettingsManager.SaveClipStyles())
                Logger.WriteLine("Clip Styles Saved Successfully");
        }

        public void UpdateTheme()
        {
            Console.WriteLine(hwnd);
            try
            {
                if (ApplicationStyles.currentStyle.mainFormStyle.useImersiveDarkMode && isHandleCreated)
                {

                    NativeMethods.UseImmersiveDarkMode(this.hwnd, true);
                    this.Icon = Properties.Resources._3white;
                }
                else
                {
                    NativeMethods.UseImmersiveDarkMode(this.hwnd, false);
                    this.Icon = Properties.Resources._3black;
                }
                //this.BackColor = ApplicationStyles.currentStyle.mainFormStyle.backgroundColor;
                ApplicationStyles.ApplyCustomThemeToControl(this);
                Refresh();
            }
            catch(Exception e)
            {
                Logger.WriteException(e);
            }
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
