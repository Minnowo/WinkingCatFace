using System;
using System.Drawing;
using System.Windows.Forms;
using WinkingCat.HelperLibs;

using WinkingCat.Settings;

namespace WinkingCat.Controls
{
    public partial class HotkeyInputControl : UserControl
    {
        public event EventHandler HotkeyChanged;
        public event EventHandler TaskChanged;
        public event EventHandler SelectionChanged;
        public Hotkey Hotkey { get; private set; }

        public bool editingHotkey { get; private set; } = false;
        private bool supressCheckboxEvent { get; set; } = false;
        public Function currentSelectedItem { get; private set; }

        public HotkeyInputControl(Hotkey hotkey)
        {
            InitializeComponent();
            Hotkey = hotkey;
            currentSelectedItem = hotkey.Callback;

            foreach (Function task in Enum.GetValues(typeof(Function)))
                HotkeyTask.Items.Add(task);

            UpdateDescription();
            UpdateHotkeyText();
            UpdateTheme();
            UpdateHotkeyStatus();
        }

        public void UpdateTheme()
        {
            ApplicationStyles.ApplyCustomThemeToControl(this);
        }
        public void Deselect(bool supressEvent = false)
        {
            supressCheckboxEvent = supressEvent;
            isSelectedCheckbox.Checked = false;
        }

        private void SelectedCheckbox_Checked(object sender, EventArgs e)
        {
            OnSelectionChanged();
        }

        private void HotkeyTask_MouseWheel(object sender, EventArgs e)
        {
            if (currentSelectedItem != (Function)HotkeyTask.SelectedItem)
            {
                Hotkey.Callback = (Function)HotkeyTask.SelectedItem;
                OnTaskChanged();
            }
        }

        protected void OnSelectionChanged()
        {
            if (SelectionChanged != null)
            {
                if (supressCheckboxEvent == false)
                    SelectionChanged(this, (EventArgs)new CheckboxCheckedEvent(isSelectedCheckbox));
                else
                    supressCheckboxEvent = false;
            }
        }

        protected void OnTaskChanged()
        {
            if (TaskChanged != null)
            {
                TaskChanged(this, EventArgs.Empty);
            }
        }

        protected void OnHotkeyChanged()
        {
            if (HotkeyChanged != null)
            {
                HotkeyChanged(this, EventArgs.Empty);
            }
        }

        private void UpdateDescription()
        {
            HotkeyTask.SelectedItem = Hotkey.Callback;
        }

        private void UpdateHotkeyText()
        {
            buttonHotkey.Text = Hotkey.ToString();
        }

        private void UpdateHotkeyStatus()
        {
            switch (Hotkey.Status)
            {
                default:
                case HotkeyStatus.NotSet:
                    labelHotkeySuccess.StaticBackColor = Color.LightGoldenrodYellow;
                    break;
                case HotkeyStatus.Failed:
                    labelHotkeySuccess.StaticBackColor = Color.Red;
                    break;
                case HotkeyStatus.Registered:
                    labelHotkeySuccess.StaticBackColor = Color.Green;
                    break;
            }
        }

        private void StartEditing()
        {
            editingHotkey = true;

            HotkeyManager.ignoreHotkeyPress = true;

            buttonHotkey.BackColor = ColorHelper.Invert(SettingsManager.MainFormSettings.textColor);
            buttonHotkey.Text = "Select a hotkey";

            Hotkey.Keys = Keys.None;
            Hotkey.Win = false;
            OnHotkeyChanged();
            UpdateHotkeyStatus();
        }

        private void StopEditing()
        {
            editingHotkey = false;

            HotkeyManager.ignoreHotkeyPress = false;

            if (Hotkey.IsOnlyModifiers)
            {
                Hotkey.Keys = Keys.None;
            }

            buttonHotkey.BackColor = SettingsManager.MainFormSettings.lightBackgroundColor;
            buttonHotkey.UseVisualStyleBackColor = true;

            OnHotkeyChanged();
            UpdateHotkeyStatus();
            UpdateHotkeyText();
        }

        private void buttonHotkey_Click(object sender, MouseEventArgs e)
        {
            if (editingHotkey)
                StopEditing();
            else
                StartEditing();

        }

        private void buttonHotkey_Leave(object sender, EventArgs e)
        {
            if (editingHotkey)
                StopEditing();
        }

        private void buttonTask_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;

            if (editingHotkey)
            {
                if (e.KeyData == Keys.Escape)
                {
                    Hotkey.Keys = Keys.None;
                    StopEditing();
                }
                else if (e.KeyCode == Keys.LWin || e.KeyCode == Keys.RWin)
                {
                    Hotkey.Win = !Hotkey.Win;
                    UpdateHotkeyText();
                }
                else if (new Hotkey(e.KeyData).IsValidHotkey)
                {
                    Hotkey.Keys = e.KeyData;
                    StopEditing();
                }
                else
                {
                    Hotkey.Keys = e.KeyData;
                    UpdateHotkeyText();
                }
            }
        }

        private void buttonTask_KeyUp(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;

            if (editingHotkey)
            {
                // PrintScreen not trigger KeyDown event
                if (e.KeyCode == Keys.PrintScreen)
                {
                    Hotkey.Keys = e.KeyData;
                    StopEditing();
                }
            }
        }
    }
}
