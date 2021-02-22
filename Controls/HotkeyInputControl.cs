using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinkingCat.HelperLibs;
 

namespace WinkingCat
{
    public partial class HotkeyInputControl : UserControl
    {
        public event EventHandler HotkeyChanged;
        public event EventHandler TaskChanged;
        public event EventHandler SelectionChanged;
        public HotkeySettings setting { get; private set; }

        public bool editingHotkey { get; private set; } = false;
        private bool supressCheckboxEvent { get; set; } = false;
        public Tasks currentSelectedItem { get; private set; }
        
        public HotkeyInputControl(HotkeySettings hotkey)
        {
            InitializeComponent();
            setting = hotkey;
            currentSelectedItem = hotkey.Task;

            foreach (Tasks task in Enum.GetValues(typeof(Tasks)))
                HotkeyTask.Items.Add(task);

            UpdateDescription();
            UpdateHotkeyText();
            UpdateHotkeyStatus();
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
            if (currentSelectedItem != (Tasks)HotkeyTask.SelectedItem)
            {
                setting.Task = (Tasks)HotkeyTask.SelectedItem;
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
            HotkeyTask.SelectedItem = setting.Task;
        }

        private void UpdateHotkeyText()
        {
            buttonHotkey.Text = setting.HotkeyInfo.ToString();
        }

        private void UpdateHotkeyStatus()
        {
            switch (setting.HotkeyInfo.Status)
            {
                default:
                case HotkeyStatus.NotSet:
                    labelHotkeySuccess.BackColor = Color.LightGoldenrodYellow;
                    break;
                case HotkeyStatus.Failed:
                    labelHotkeySuccess.BackColor = Color.Red;
                    break;
                case HotkeyStatus.Registered:
                    labelHotkeySuccess.BackColor = Color.Green;
                    break;
            }
        }

        private void StartEditing()
        {
            editingHotkey = true;

            HotkeyManager.ignoreHotkeyPress = true;

            buttonHotkey.BackColor = Color.FromArgb(225, 255, 225);
            buttonHotkey.Text = "Select a hotkey";

            setting.HotkeyInfo.Hotkey = Keys.None;
            setting.HotkeyInfo.Win = false;
            OnHotkeyChanged();
            UpdateHotkeyStatus();
        }

        private void StopEditing()
        {
            editingHotkey = false;

            HotkeyManager.ignoreHotkeyPress = false;

            if (setting.HotkeyInfo.IsOnlyModifiers)
            {
                setting.HotkeyInfo.Hotkey = Keys.None;
            }

            buttonHotkey.BackColor = SystemColors.Control;
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
                    setting.HotkeyInfo.Hotkey = Keys.None;
                    StopEditing();
                }
                else if (e.KeyCode == Keys.LWin || e.KeyCode == Keys.RWin)
                {
                    setting.HotkeyInfo.Win = !setting.HotkeyInfo.Win;
                    UpdateHotkeyText();
                }
                else if (new HotkeyInfo(e.KeyData).IsValidHotkey)
                {
                    setting.HotkeyInfo.Hotkey = e.KeyData;
                    StopEditing();
                }
                else
                {
                    setting.HotkeyInfo.Hotkey = e.KeyData;
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
                    setting.HotkeyInfo.Hotkey = e.KeyData;
                    StopEditing();
                }
            }
        }
    }
}
