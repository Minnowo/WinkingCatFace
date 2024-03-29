﻿using System;
using System.IO;
using System.Windows.Forms;
using WinkingCat.Controls;
using WinkingCat.HelperLibs;
using WinkingCat.Settings;

namespace WinkingCat
{
    public partial class SettingsForm : BaseForm
    {
        public HotkeyInputControl selectedHotkey { get; private set; }

        private bool preventUpdate = false;
        public SettingsForm()
        {
            InitializeComponent();
            this.Text = "Settings";

            preventUpdate = true;

            // general settings
            cbShowInTray.Checked = SettingsManager.MainFormSettings.Show_In_Tray;
            cbMinimizeToTrayOnClose.Checked = SettingsManager.MainFormSettings.Hide_In_Tray_On_Close;
            cbMinimizeToTrayOnStart.Checked = SettingsManager.MainFormSettings.Start_In_Tray;
            cbAlwaysOnTop.Checked = SettingsManager.MainFormSettings.Always_On_Top;
            cbSaveImageToDisk.Checked = InternalSettings.Save_Images_To_Disk;
            cbUseDWRMOverWRM.Checked = InternalSettings.Save_WORM_As_DWORM;
            cbHideClipsOnCapture.Checked = SettingsManager.ClipSettings.Never_Hide_Clips;
            folderSortAscendingCheckbox.Checked = SettingsManager.MainFormSettings.FolderSortOrder >= 0;
            fileSortAscendingCheckbox.Checked = SettingsManager.MainFormSettings.FileSortOrder >= 0;
            cbMaximizeBox.Checked = SettingsManager.MainFormSettings.Show_Maximize_Box;
            neverHideWindowsCheckbox.Checked = SettingsManager.MainFormSettings.Never_Hide_Windows;

            foreach (Function task in Enum.GetValues(typeof(Function)))
            {
                cbOnTrayLeftClick.Items.Add(task);
                cbOnTrayMiddleClick.Items.Add(task);
                cbOnTrayDoubleClick.Items.Add(task);
            }
            cbOnTrayLeftClick.SelectedItem = SettingsManager.MainFormSettings.On_Tray_Left_Click;
            cbOnTrayMiddleClick.SelectedItem = SettingsManager.MainFormSettings.On_Tray_Middle_Click;
            cbOnTrayDoubleClick.SelectedItem = SettingsManager.MainFormSettings.On_Tray_Double_Click;

            foreach (ImgFormat fmt in Enum.GetValues(typeof(ImgFormat)))
            {
                if (fmt == ImgFormat.nil) continue;
                if (fmt == ImgFormat.webp && !InternalSettings.WebP_Plugin_Exists) continue;
                cbDefaultImageFormat.Items.Add(fmt);
            }
            cbDefaultImageFormat.SelectedItem = InternalSettings.Default_Image_Format;

            // region capture settings
            cbDrawScreenWideCrosshair.Checked = SettingsManager.RegionCaptureSettings.Draw_Screen_Wide_Crosshair;
            cbMarchingAnts.Checked = SettingsManager.RegionCaptureSettings.Draw_Marching_Ants;
            cbDimBackground.Checked = SettingsManager.RegionCaptureSettings.Draw_Background_Overlay;
            cbShowInfoText.Checked = SettingsManager.RegionCaptureSettings.Draw_Info_Text;
            cbMarchingAnts.Enabled = SettingsManager.RegionCaptureSettings.Draw_Screen_Wide_Crosshair;

            cbShowMagnifier.Checked = SettingsManager.RegionCaptureSettings.Draw_Magnifier;
            cbDrawMagnifierCrosshair.Checked = SettingsManager.RegionCaptureSettings.Draw_Crosshair_In_Magnifier;
            cbDrawMagnifierGrid.Checked = SettingsManager.RegionCaptureSettings.Draw_Pixel_Grid_In_Magnifier;
            cbDrawMagnifierBorder.Checked = SettingsManager.RegionCaptureSettings.Draw_Border_On_Magnifier;
            cbCenterMagnifierOnMouse.Checked = SettingsManager.RegionCaptureSettings.Center_Magnifier_On_Mouse;

            nudMagnifierPixelCount.Value = SettingsManager.RegionCaptureSettings.Magnifier_Pixel_Count;
            nudMagnifierPixelSize.Value = SettingsManager.RegionCaptureSettings.Magnifier_Pixel_Size;
            nudMagnifierZoomLevel.Value = (decimal)SettingsManager.RegionCaptureSettings.Magnifier_Zoom_Level;
            nudMagnifierZoomScale.Value = (decimal)SettingsManager.RegionCaptureSettings.Magnifier_Zoom_Scale;

            foreach (InRegionTasks task in Enum.GetValues(typeof(InRegionTasks)))
            {
                cbMiddleClickAction.Items.Add(task);
                cbRightClickAction.Items.Add(task);
                cbXButton1Action.Items.Add(task);
                cbXButton2Action.Items.Add(task);
            }
            cbMiddleClickAction.SelectedItem = SettingsManager.RegionCaptureSettings.On_Mouse_Middle_Click;
            cbRightClickAction.SelectedItem = SettingsManager.RegionCaptureSettings.On_Mouse_Right_Click;
            cbXButton1Action.SelectedItem = SettingsManager.RegionCaptureSettings.On_XButton1_Click;
            cbXButton2Action.SelectedItem = SettingsManager.RegionCaptureSettings.On_XButton2_Click;

            // clipboard settings
            foreach (ColorFormat colorformat in Enum.GetValues(typeof(ColorFormat)))
            {
                cbDefaultColorFormat.Items.Add(colorformat);
            }
            cbDefaultColorFormat.SelectedItem = SettingsManager.MiscSettings.Default_Color_Format;
            cbAutoCopyImage.Checked = SettingsManager.RegionCaptureSettings.Auto_Copy_Image;
            cbAutoCopyColor.Checked = SettingsManager.RegionCaptureSettings.Auto_Copy_Color;

            // path settings
            cbUseCustomScreenshotPath.Checked = SettingsManager.MiscSettings.Use_Custom_Screenshot_Folder;
            tbCustomScreenshotPath.Text = SettingsManager.MiscSettings.Screenshot_Folder_Path;

            // hotkey settings
            UpdateHotkeyControls();

            // delay settings
            numericUpDown1.Value = SettingsManager.MainFormSettings.Load_Image_Delay.Clamp(0, (int)numericUpDown1.Maximum);
            numericUpDown2.Value = SettingsManager.MainFormSettings.Tray_Double_Click_Time.Clamp(0, (int)numericUpDown2.Maximum);
            numericUpDown3.Value = SettingsManager.MainFormSettings.Wait_Hide_Time.Clamp(0, (int)numericUpDown3.Maximum);
            numericUpDown4.Value = SettingsManager.MainFormSettings.Image_Failed_To_Load_Message_Time.Clamp(0, (int)numericUpDown4.Maximum);

            preventUpdate = false;

            this.FormClosing += new FormClosingEventHandler(OnFormClosing_Event);

            base.RegisterEvents();
        }

        public void UpdateHotkeyControls()
        {
            if (HotkeyManager.hotKeys != null)
                foreach (Hotkey hotkey in HotkeyManager.hotKeys)
                {
                    AddHotkeyControl(new HotkeyInputControl(hotkey));
                }
        }

        public void AddHotkeyControl(HotkeyInputControl control)
        {
            control.Margin = new Padding(0, 0, 0, 2);
            control.Dock = DockStyle.Top;
            control.TaskChanged += control_SelectedChanged;
            control.HotkeyChanged += control_HotkeyChanged;
            control.SelectionChanged += control_CheckboxChanged;
            flpHotkeyDisplayPanel.Controls.Add(control);
        }

        private void SaveSettingsToDisk()
        {
            string dir = PathHelper.CurrentDirectory;

            Directory.SetCurrentDirectory(PathHelper.BaseDirectory);
            SettingsManager.SaveClipSettings();
            SettingsManager.SaveMainFormSettings();
            SettingsManager.SaveRegionCaptureSettings();
            SettingsManager.SaveMiscSettings();
            SettingsManager.SaveHotkeySettings(HotkeyManager.hotKeys);
            Directory.SetCurrentDirectory(dir);
        }

        #region MainForm events

        private void OnFormClosing_Event(object sender, EventArgs e)
        {
            SaveSettingsToDisk();
            SettingsManager.CallUpdateSettings();
        }

        #endregion

        #region General Settings Tab

        private void ShowTrayIcon_CheckChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            if (cbShowInTray.Checked)
            {
                SettingsManager.MainFormSettings.Show_In_Tray = true;
                SettingsManager.MainFormSettings.Hide_In_Tray_On_Close = cbMinimizeToTrayOnClose.Checked;
                cbMinimizeToTrayOnClose.Enabled = true;

                return;
            }

            cbMinimizeToTrayOnClose.Enabled = false;

            SettingsManager.MainFormSettings.Show_In_Tray = false;
            SettingsManager.MainFormSettings.Hide_In_Tray_On_Close = false;
        }

        private void MinimizeToTrayOnClose_CheckChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.MainFormSettings.Hide_In_Tray_On_Close = cbMinimizeToTrayOnClose.Checked;
        }

        private void MinimizeToTrayOnStart_CheckChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.MainFormSettings.Start_In_Tray = cbMinimizeToTrayOnStart.Checked;
        }

        private void AlwaysOnTop_CheckChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.MainFormSettings.Always_On_Top = cbAlwaysOnTop.Checked;
        }

        private void OnTrayIconLeftClick_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.MainFormSettings.On_Tray_Left_Click = (Function)cbOnTrayLeftClick.SelectedItem;
        }

        private void OnTrayIconDoubleClick_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.MainFormSettings.On_Tray_Double_Click = (Function)cbOnTrayDoubleClick.SelectedItem;
        }

        private void OnTrayIconMiddleClick_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.MainFormSettings.On_Tray_Middle_Click = (Function)cbOnTrayMiddleClick.SelectedItem;
        }

        private void DefaultImageFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            InternalSettings.Default_Image_Format = (ImgFormat)cbDefaultImageFormat.SelectedItem;
        }

        private void SaveImageToDisk_CheckedChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            InternalSettings.Save_Images_To_Disk = cbSaveImageToDisk.Checked;
        }

        private void UseDWRMOverWRM_CheckedChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            InternalSettings.Save_WORM_As_DWORM = cbUseDWRMOverWRM.Checked;
        }

        private void ToggleMaximizeBox_CheckedChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.MainFormSettings.Show_Maximize_Box = cbMaximizeBox.Checked;
        }

        private void HideClipsOnCapture_CheckedChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.ClipSettings.Never_Hide_Clips = !cbHideClipsOnCapture.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            if (fileSortAscendingCheckbox.Checked)
            {
                SettingsManager.MainFormSettings.FileSortOrder = 1;
            }
            else
            {
                SettingsManager.MainFormSettings.FileSortOrder = -1;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            if (folderSortAscendingCheckbox.Checked)
            {
                SettingsManager.MainFormSettings.FolderSortOrder = 1;
            }
            else
            {
                SettingsManager.MainFormSettings.FolderSortOrder = -1;
            }
        }

        #endregion

        #region Region Capture Settings Tab

        private void ScreenWideCrosshair_CheckChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            if (cbDrawScreenWideCrosshair.Checked)
            {
                SettingsManager.RegionCaptureSettings.Draw_Screen_Wide_Crosshair = true;
                SettingsManager.RegionCaptureSettings.Draw_Marching_Ants = cbMarchingAnts.Checked;
                cbMarchingAnts.Enabled = true;
                return;
            }

            SettingsManager.RegionCaptureSettings.Draw_Screen_Wide_Crosshair = false;
            SettingsManager.RegionCaptureSettings.Draw_Marching_Ants = false;
            cbMarchingAnts.Enabled = false;
        }

        private void MarchingAnts_CheckChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            if (cbMarchingAnts.Checked)
            {
                cbDrawScreenWideCrosshair.Checked = true;
                SettingsManager.RegionCaptureSettings.Draw_Marching_Ants = true;
                return;
            }

            SettingsManager.RegionCaptureSettings.Draw_Marching_Ants = false;
        }

        private void DimBackground_CheckChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.RegionCaptureSettings.Draw_Background_Overlay = cbDimBackground.Checked;
        }

        private void ShowInfoText_CheckChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.RegionCaptureSettings.Draw_Info_Text = cbShowInfoText.Checked;
        }

        private void SolidScreenWideCrosshair_CheckChanged(object sender, EventArgs e)
        {

        }

        private void ClickAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.RegionCaptureSettings.On_Mouse_Middle_Click = (InRegionTasks)cbMiddleClickAction.SelectedItem;
            SettingsManager.RegionCaptureSettings.On_Mouse_Right_Click = (InRegionTasks)cbRightClickAction.SelectedItem;
            SettingsManager.RegionCaptureSettings.On_XButton1_Click = (InRegionTasks)cbXButton1Action.SelectedItem;
            SettingsManager.RegionCaptureSettings.On_XButton2_Click = (InRegionTasks)cbXButton2Action.SelectedItem;
        }

        private void ShowMagnifier_CheckChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            pnlMagnifierSettings.Enabled = cbShowMagnifier.Checked;
        }

        private void DrawMagnifierCrosshair_CheckChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.RegionCaptureSettings.Draw_Crosshair_In_Magnifier = cbDrawMagnifierCrosshair.Checked;
        }

        private void DrawMagnifierGrid_CheckChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.RegionCaptureSettings.Draw_Pixel_Grid_In_Magnifier = cbDrawMagnifierGrid.Checked;
        }

        private void DrawMagnifierBorder_CheckChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.RegionCaptureSettings.Draw_Border_On_Magnifier = cbDrawMagnifierBorder.Checked;
        }

        private void CenterMagnifierOnMouse_CheckChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.RegionCaptureSettings.Center_Magnifier_On_Mouse = cbCenterMagnifierOnMouse.Checked;
        }

        private void NUD_ValueChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.RegionCaptureSettings.Magnifier_Pixel_Count = (int)nudMagnifierPixelCount.Value;
            SettingsManager.RegionCaptureSettings.Magnifier_Pixel_Size = (int)nudMagnifierPixelSize.Value;
            SettingsManager.RegionCaptureSettings.Magnifier_Zoom_Level = (float)nudMagnifierZoomLevel.Value;
            SettingsManager.RegionCaptureSettings.Magnifier_Zoom_Scale = (float)nudMagnifierZoomScale.Value;
        }

        #endregion

        #region Clipboard Settings Tab

        private void DefaultColorFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.MiscSettings.Default_Color_Format = (ColorFormat)cbDefaultColorFormat.SelectedItem;
        }

        private void AutoCopyImage_CheckChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.RegionCaptureSettings.Auto_Copy_Image = cbAutoCopyImage.Checked;
        }

        private void AutoCopyColor_CheckChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.RegionCaptureSettings.Auto_Copy_Color = cbAutoCopyColor.Checked;
        }

        #endregion

        #region Hotkey Settings Tab

        #region Hotkey Controls Callbacks

        private void control_HotkeyChanged(object sender, EventArgs e)
        {
            HotkeyManager.RegisterHotkey(((HotkeyInputControl)sender).Hotkey);
        }

        private void control_SelectedChanged(object sender, EventArgs e)
        {
            HotkeyManager.RegisterHotkey(((HotkeyInputControl)sender).Hotkey);
        }

        private void control_CheckboxChanged(object sender, EventArgs e)
        {
            if ((HotkeyInputControl)sender != selectedHotkey)
                selectedHotkey?.Deselect();
            selectedHotkey = (HotkeyInputControl)sender;
        }

        #endregion

        private void AddHotkey_Click(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            Hotkey newHotkey = new Hotkey(Keys.None, Function.None);
            HotkeyManager.RegisterHotkey(newHotkey);

            AddHotkeyControl(new HotkeyInputControl(newHotkey));
        }

        private void RemoveHotkey_Click(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            if (selectedHotkey != null)
            {
                HotkeyManager.UnRegisterHotkey(selectedHotkey.Hotkey, true);
                flpHotkeyDisplayPanel.Controls.Remove(selectedHotkey);
                selectedHotkey.Dispose();
                selectedHotkey = null;
            }
        }

        private void RestoreDefaultHotkeys_Click(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            foreach (HotkeyInputControl control in flpHotkeyDisplayPanel.Controls)
                HotkeyManager.UnRegisterHotkey(control.Hotkey, true);

            flpHotkeyDisplayPanel.Controls.Clear();
            HotkeyManager.UpdateHotkeys(HotkeyManager.GetDefaultHotkeyList(), true);
            UpdateHotkeyControls();
        }

        #endregion

        private void BrowseFolders_Click(object sender, EventArgs e)
        {
            string folderPath = PathHelper.AskChooseDirectory();

            if (string.IsNullOrEmpty(folderPath) || !PathHelper.ValidPath(folderPath))
                return;

            tbCustomScreenshotPath.Text = folderPath;
            SettingsManager.MiscSettings.Screenshot_Folder_Path = folderPath;
        }

        private void UseCustomScreenshotFolder_CheckChanged(object sender, EventArgs e)
        {
            SettingsManager.MiscSettings.Use_Custom_Screenshot_Folder = cbUseCustomScreenshotPath.Checked;
        }

        private void LoadImageDelayNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            SettingsManager.MainFormSettings.Load_Image_Delay = (int)numericUpDown1.Value;
        }

        private void TrayDoubleClickDelayNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            SettingsManager.MainFormSettings.Tray_Double_Click_Time = (int)numericUpDown2.Value;
        }

        private void HideFormsDelayNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            SettingsManager.MainFormSettings.Wait_Hide_Time = (int)numericUpDown3.Value;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            SettingsManager.MainFormSettings.Image_Failed_To_Load_Message_Time = (int)numericUpDown4.Value;

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            SettingsManager.MainFormSettings.Never_Hide_Windows = neverHideWindowsCheckbox.Checked;
        }
    }

}
