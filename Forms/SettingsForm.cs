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
    public partial class SettingsForm : Form
    {
        public Form activeForm { get; private set; }
        private bool isHandleCreated = false;
        public SettingsForm()
        {
            InitializeComponent();
            this.HandleCreated += HandleCreated_Event;
            ApplicationStyles.UpdateStylesEvent += ApplicationStyles_UpdateStylesEvent;
            this.Text = "Settings";
            #region Buttons
            bGeneral.Click += GeneralButtonClick_Event;
            bRegionCapture.Click += RegionCaptureButtonClick_Event;
            bUpload.Click += UploadButtonClick_Event;
            bClipboard.Click += ClipboardButtonClick_Event;
            bHotkeys.Click += HotkeysButtonClick_Event;
            bPaths.Click += PathsButtonClick_Event;
            #endregion
            this.FormClosing += new FormClosingEventHandler(OnFormClosing_Event);
            OpenChildForm(new GeneralSettingsForm());
        }

        private void ApplicationStyles_UpdateStylesEvent(object sender, EventArgs e)
        {
            UpdateTheme();
        }

        public void UpdateTheme()
        {
            try
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
                //this.BackColor = ApplicationStyles.currentStyle.mainFormStyle.backgroundColor;
                ApplicationStyles.ApplyCustomThemeToControl(this);
                Refresh();
            }
            catch (Exception e)
            {
                Logger.WriteException(e);
            }
        }

        public void HandleCreated_Event(object sender, EventArgs e)
        {
            isHandleCreated = true;
            UpdateTheme();
        }

        #region MainForm events
        private void OnFormClosing_Event(object sender, EventArgs e)
        {
            SaveSettingsToDisk();
        }

        private void SaveSettingsToDisk()
        {
            if (SettingsManager.SavePathSettings())
                Logger.WriteLine("PathSettings Settings Saved Successfully");
            if (SettingsManager.SaveMainFormSettings())
                Logger.WriteLine("MainForm Settings Saved Successfully");
            if(SettingsManager.SaveRegionCaptureSettings())
                Logger.WriteLine("RegionCapture Settings Saved Successfully");
            if(SettingsManager.SaveClipboardSettings())
                Logger.WriteLine("Clipboard Settings Saved Successfully");
            if(SettingsManager.SaveMiscSettings())
                Logger.WriteLine("Misc Settings Saved Successfully");
            if (SettingsManager.SaveHotkeySettings(HotkeyManager.hotKeys))
                Logger.WriteLine("Hotkeys Saved Successfully");
        }
        #endregion

        #region Button events

        private void btnIntegration_Click(object sender, EventArgs e)
        {
            OpenChildForm(new IntegrationForm());
        }

        private void GeneralButtonClick_Event(object sender, EventArgs e)
        {
            OpenChildForm(new GeneralSettingsForm());
        }

        private void RegionCaptureButtonClick_Event(object sender, EventArgs e)
        {
            OpenChildForm(new RegionCaptureSettingsForm());
        }

        private void UploadButtonClick_Event(object sender, EventArgs e)
        {
            OpenChildForm(new UploadSettingsForm());
        }

        private void ClipboardButtonClick_Event(object sender, EventArgs e)
        {
            OpenChildForm(new ClipboardSettingsForm());
        }

        private void HotkeysButtonClick_Event(object sender, EventArgs e)
        {
            OpenChildForm(new HotkeySettingsForm());
        }

        private void PathsButtonClick_Event(object sender, EventArgs e)
        {
            OpenChildForm(new PathSettingsForm());
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
            FormDockPanel.Controls.Add((Form)childForm);
            FormDockPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            ResumeLayout();
        }

    }

}
