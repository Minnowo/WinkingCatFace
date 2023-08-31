using System;
using System.Windows.Forms;
using WinkingCat.HelperLibs;
using WinkingCat.Settings;

namespace WinkingCat
{
    public partial class BaseForm : Form
    {
        /// <summary>
        /// The forms handle has been created.
        /// </summary>
        public bool IsReady { get; protected set; }

        /// <summary>
        /// Prevents the form from being hidden when requested.
        /// </summary>
        public bool PreventHide { get; set; } = false;

        /// <summary>
        /// Prevents the form from being hidden next time its requested and then defaults to false.
        /// </summary>
        public bool PreventHideNext { get; set; } = false;

        /// <summary>
        /// If the form has been hidden. this prevents forms that are hidden before shown forms are hidden to not be shown.
        /// </summary>
        protected bool _hiddenFromRequest = false;

        /// <summary>
        /// Updates the custom theme of the form.
        /// </summary>
        public virtual void UpdateTheme()
        {
            SettingsManager.ApplyImmersiveDarkTheme(this, IsHandleCreated);
            ApplicationStyles.ApplyCustomThemeToControl(this);
            Refresh();
        }

        /// <summary>
        /// Updates any settings the form uses.
        /// </summary>
        public virtual void UpdateSettings()
        {
            this.TopMost = SettingsManager.MainFormSettings.Always_On_Top;
        }

        /// <summary>
        /// Registers default events used by the form.
        /// </summary>
        protected virtual void RegisterEvents()
        {
            RegionCaptureHelper.RequestShowForms += ShowHide;
            SettingsManager.SettingsUpdatedEvent += UpdateSettings;
            ApplicationStyles.UpdateThemeEvent += UpdateTheme;
        }

        protected void ShowHide(bool show)
        {
            if (!show)
            {
                if (this.WindowState == FormWindowState.Minimized || !this.Visible || this.Opacity == 0)
                {
                    return;
                }

                if (PreventHide)
                {
                    return;
                }

                if (PreventHideNext)
                {
                    PreventHideNext = false;
                    return;
                }

                _hiddenFromRequest = true;
                this.InvokeSafe(Hide);
            }
            else
            {
                if (_hiddenFromRequest)
                {
                    _hiddenFromRequest = false;
                    this.InvokeSafe(Show);
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.IsReady = true;
            this.UpdateTheme();
            this.UpdateSettings();
        }
    }
}
