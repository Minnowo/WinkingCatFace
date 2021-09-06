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
        public HotkeyInputControl selectedHotkey { get; private set; }

        private bool isHandleCreated = false;
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

            preventUpdate = false;

            this.HandleCreated += HandleCreated_Event;
            this.FormClosing += new FormClosingEventHandler(OnFormClosing_Event);

            ApplicationStyles.UpdateStylesEvent += ApplicationStyles_UpdateStylesEvent;
        }

        
        public void UpdateTheme()
        {
            try
            {
                if (ApplicationStyles.currentStyle.mainFormStyle.useImersiveDarkMode && isHandleCreated)
                {
                    NativeMethods.UseImmersiveDarkMode(Handle, true);
                    this.Icon = ApplicationStyles.whiteIcon;
                }
                else
                {
                    NativeMethods.UseImmersiveDarkMode(Handle, false);
                    this.Icon = ApplicationStyles.blackIcon;
                }
                ApplicationStyles.ApplyCustomThemeToControl(this);
                Refresh();
            }
            catch
            {
            }
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
            SettingsManager.SaveClipSettings();
            SettingsManager.SaveMainFormSettings();
            SettingsManager.SaveRegionCaptureSettings();
            SettingsManager.SaveMiscSettings();
            SettingsManager.SaveHotkeySettings(HotkeyManager.hotKeys);
        }

        private void ApplicationStyles_UpdateStylesEvent(object sender, EventArgs e)
        {
            UpdateTheme();
        }

        #region MainForm events

        private void OnFormClosing_Event(object sender, EventArgs e)
        {
            SaveSettingsToDisk();
        }
        public void HandleCreated_Event(object sender, EventArgs e)
        {
            isHandleCreated = true;
            UpdateTheme();
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

        #endregion

        #region Region Capture Settings Tab

        private void ScreenWideCrosshair_CheckChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            if (cbDrawScreenWideCrosshair.Checked)
            {
                SettingsManager.RegionCaptureSettings.Draw_Screen_Wide_Crosshair= true;
                SettingsManager.RegionCaptureSettings.Draw_Marching_Ants= cbMarchingAnts.Checked;
                cbMarchingAnts.Enabled = true;
                return;
            }

            SettingsManager.RegionCaptureSettings.Draw_Screen_Wide_Crosshair= false;
            SettingsManager.RegionCaptureSettings.Draw_Marching_Ants= false;
            cbMarchingAnts.Enabled = false;
        }

        private void MarchingAnts_CheckChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            if (cbMarchingAnts.Checked)
            {
                cbDrawScreenWideCrosshair.Checked = true;
                SettingsManager.RegionCaptureSettings.Draw_Marching_Ants= true;
                return;
            }

            SettingsManager.RegionCaptureSettings.Draw_Marching_Ants= false;
        }

        private void DimBackground_CheckChanged(object sender, EventArgs e)
        {
            if (preventUpdate) return;
            SettingsManager.RegionCaptureSettings.Draw_Background_Overlay= cbDimBackground.Checked;
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
            SettingsManager.RegionCaptureSettings.Draw_Crosshair_In_Magnifier= cbDrawMagnifierCrosshair.Checked;
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
            SettingsManager.RegionCaptureSettings.Center_Magnifier_On_Mouse= cbCenterMagnifierOnMouse.Checked;
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

        
    }

}
