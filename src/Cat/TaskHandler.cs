using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinkingCat.HelperLibs;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;

namespace WinkingCat
{

    public static class TaskHandler
    {
        public static event EventHandler TaskExecuted;
        private static bool result = false;

        public static void OnTaskExecuted(Function t)
        {
            if (TaskExecuted != null)
            {
                TaskExecuted(null, new TaskExecutedEvent(t));
            }
        }

        public static bool CaptureWindow(WindowInfo window)
        {
            OnTaskExecuted(Function.CaptureWindow);
            if (!Helper.IsValidCropArea(window.Rectangle))
                return false;

            if(!SettingsManager.MainFormSettings.Never_Hide_Windows) 
                RegionCaptureHelper.RequestFormsHide(false, true);

            using (Image img = ScreenshotHelper.CaptureRectangle(window.Rectangle))
            {
                if (SettingsManager.RegionCaptureSettings.Auto_Copy_Image)
                {
                    ClipboardHelper.CopyImage(img);
                }

                if (img == null || string.IsNullOrEmpty(RegionCaptureHelper.Save(PathHelper.GetNewImageFileName(), img)))
                {
                    if (!SettingsManager.MainFormSettings.Never_Hide_Windows)
                        RegionCaptureHelper.RequestFormsHide(true, false);
                    return false;
                }
                if (!SettingsManager.MainFormSettings.Never_Hide_Windows)
                    RegionCaptureHelper.RequestFormsHide(true, false);

                return true;
            } 
        }

        public static bool ExecuteTask(Function task)
        {
            OnTaskExecuted(task);

            Image image;

            switch (task)
            {
                case Function.RegionCapture:
                    HotkeyManager.tempTgnoreHotkeyPress = true;
                    if (!SettingsManager.MainFormSettings.Never_Hide_Windows)
                        RegionCaptureHelper.RequestFormsHide(false, true);
                    RegionCaptureHelper.RegionCapture(RegionCaptureMode.Default, false);
                    if (!SettingsManager.MainFormSettings.Never_Hide_Windows)
                        RegionCaptureHelper.RequestFormsHide(true, false);
                    HotkeyManager.tempTgnoreHotkeyPress = false;
                    return true;

                case Function.RegionCaptureLite:
                    HotkeyManager.tempTgnoreHotkeyPress = true;

                    HotkeyManager.tempTgnoreHotkeyPress = false;
                    return false;

                case Function.NewClipFromRegionCapture:
                    HotkeyManager.tempTgnoreHotkeyPress = true;
                    if (!SettingsManager.MainFormSettings.Never_Hide_Windows)
                        RegionCaptureHelper.RequestFormsHide(false, true);
                    RegionCaptureHelper.RegionCapture(RegionCaptureMode.Default, true);
                    if (!SettingsManager.MainFormSettings.Never_Hide_Windows)
                        RegionCaptureHelper.RequestFormsHide(true, false);
                    HotkeyManager.tempTgnoreHotkeyPress = false;
                    return true;

                case Function.NewClipFromFile:
                    string[] path = ImageHelper.OpenImageFileDialog(false, Program.MainForm);

                    if (path == null || path.Length < 1)
                        return false;

                    image = ImageHelper.LoadImage(path[0]);
                    
                    if (image == null)
                        return false;

                    ClipManager.Clips[ClipManager.CreateClipAtCursor(image, false)].Options.FilePath = path[0];
                    return true;

                case Function.NewClipFromClipboard:
                    image = ClipboardHelper.GetImage();
                    
                    if (image == null)
                        return false;

                    ClipManager.CreateClipAtCursor(image, false);
                    return true;

                case Function.ScreenColorPicker:
                    RegionCaptureHelper.RegionCapture(RegionCaptureMode.ColorPicker);
                    return true;

                case Function.CaptureLastRegion:

                    if (RegionCaptureHelper.LastRegionResult == null || !Helper.IsValidCropArea(RegionCaptureHelper.LastRegionResult.Region))
                        return false;

                    if (!SettingsManager.MainFormSettings.Never_Hide_Windows)
                        RegionCaptureHelper.RequestFormsHide(false, true);

                    using (Image img = ScreenshotHelper.CaptureRectangle(ScreenHelper.GetRectangle0Based(RegionCaptureHelper.LastRegionResult.Region)))
                    {
                        if (img == null || string.IsNullOrEmpty(RegionCaptureHelper.Save(PathHelper.GetNewImageFileName(), img)))
                        {
                            if (!SettingsManager.MainFormSettings.Never_Hide_Windows)
                                RegionCaptureHelper.RequestFormsHide(true, false);
                            return false;
                        }

                        if (SettingsManager.RegionCaptureSettings.Auto_Copy_Image)
                            ClipboardHelper.CopyImage(img);
                    }

                    if (!SettingsManager.MainFormSettings.Never_Hide_Windows)
                        RegionCaptureHelper.RequestFormsHide(true, false);

                    return true;

                case Function.CaptureFullScreen:

                    if (!SettingsManager.MainFormSettings.Never_Hide_Windows)
                        RegionCaptureHelper.RequestFormsHide(false, true);

                    using (Image img = ScreenshotHelper.CaptureFullscreen())
                    {
                        if (img == null || string.IsNullOrEmpty(RegionCaptureHelper.Save(PathHelper.GetNewImageFileName(), img)))
                        {
                            if (!SettingsManager.MainFormSettings.Never_Hide_Windows)
                                RegionCaptureHelper.RequestFormsHide(true, false);
                            return false;
                        }

                        if (SettingsManager.RegionCaptureSettings.Auto_Copy_Image)
                            ClipboardHelper.CopyImage(img);
                    }

                    if (!SettingsManager.MainFormSettings.Never_Hide_Windows)
                        RegionCaptureHelper.RequestFormsHide(true, false);

                    return true;

                case Function.CaptureActiveMonitor:

                    if (!SettingsManager.MainFormSettings.Never_Hide_Windows)
                        RegionCaptureHelper.RequestFormsHide(false, true);

                    using (Image img = ScreenshotHelper.CaptureActiveMonitor())
                    {
                        if (img == null || string.IsNullOrEmpty(RegionCaptureHelper.Save(PathHelper.GetNewImageFileName(), img)))
                        {
                            if (!SettingsManager.MainFormSettings.Never_Hide_Windows)
                                RegionCaptureHelper.RequestFormsHide(true, false);
                            return false;
                        }

                        if (SettingsManager.RegionCaptureSettings.Auto_Copy_Image)
                            ClipboardHelper.CopyImage(img);
                    }

                    if (!SettingsManager.MainFormSettings.Never_Hide_Windows)
                        RegionCaptureHelper.RequestFormsHide(true, false);

                    return true;

                case Function.CaptureActiveWindow:

                    if (!SettingsManager.MainFormSettings.Never_Hide_Windows)
                        RegionCaptureHelper.RequestFormsHide(false, true);

                    using (Image img = ScreenshotHelper.CaptureRectangle(
                        ScreenHelper.GetWindowRectangle(NativeMethods.GetForegroundWindow())))
                    {
                        if (img == null || string.IsNullOrEmpty(RegionCaptureHelper.Save(PathHelper.GetNewImageFileName(), img)))
                        {
                            if (!SettingsManager.MainFormSettings.Never_Hide_Windows)
                                RegionCaptureHelper.RequestFormsHide(true, false);
                            return false;
                        }

                        if (SettingsManager.RegionCaptureSettings.Auto_Copy_Image)
                            ClipboardHelper.CopyImage(img);
                    }

                    if (!SettingsManager.MainFormSettings.Never_Hide_Windows)
                        RegionCaptureHelper.RequestFormsHide(true, false);

                    return true;

                case Function.CaptureGif:
                    return false;

                case Function.NewOCRCapture:
                    return false;

                case Function.ColorWheelPicker:
                    Program.MainForm.ColorPicker_Click(null, EventArgs.Empty);
                    return true;

                case Function.HashCheck:
                    Program.MainForm.HashCheck_Click(null, EventArgs.Empty);
                    return true;

                case Function.Regex:
                    Program.MainForm.Regex_Click(null, EventArgs.Empty);
                    return true;

                case Function.QRCode:
                    Program.MainForm.QrCode_Click(null, EventArgs.Empty);
                    return true;

                case Function.OpenMainForm:
                    Program.MainForm.ForceActivate();
                    return true;
            }
            return result;
        }
    }
}
