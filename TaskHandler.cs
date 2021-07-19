using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinkingCat.ScreenCaptureLib;
using WinkingCat.HelperLibs;
using System.Drawing.Imaging;
using WinkingCat.ClipHelper;
using System.Drawing;
using System.Windows.Forms;

namespace WinkingCat
{

    public static class TaskHandler
    {
        public static event EventHandler TaskExecuted;
        private static bool result = false;

        public static void OnTaskExecuted(Tasks t)
        {
            if (TaskExecuted != null)
            {
                TaskExecuted(null, new TaskExecutedEvent(t));
            }
        }

        public static bool CaptureWindow(WindowInfo window)
        {
            OnTaskExecuted(Tasks.CaptureWindow);
            if (!ScreenHelper.IsValidCropArea(window.Rectangle))
                return false;
            
            using (Image img = ScreenShotManager.CaptureRectangle(window.Rectangle))
            {
                if (RegionCaptureOptions.AutoCopyImage)
                {
                    ClipboardHelper.CopyImageDefault(img);
                }

                if (img == null || string.IsNullOrEmpty(ImageHandler.Save(PathHelper.GetNewImageFileName(), img)))
                {
                    return false;
                }
                 
                return true;
            } 
        }

        public static bool ExecuteTask(Tasks task)
        {
            OnTaskExecuted(task);

            Image image;
            switch (task)
            {
                case Tasks.RegionCapture:
                    HotkeyManager.tempTgnoreHotkeyPress = true;
                    ImageHandler.RegionCapture(RegionCaptureMode.Default);
                    HotkeyManager.tempTgnoreHotkeyPress = false;
                    return true;

                case Tasks.RegionCaptureLite:
                    HotkeyManager.tempTgnoreHotkeyPress = true;

                    HotkeyManager.tempTgnoreHotkeyPress = false;
                    return false;

                case Tasks.NewClipFromRegionCapture:
                    HotkeyManager.tempTgnoreHotkeyPress = true;
                    ImageHandler.RegionCapture(RegionCaptureMode.Default, true);
                    HotkeyManager.tempTgnoreHotkeyPress = false;
                    return true;

                case Tasks.NewClipFromFile:
                    string path = ImageHelper.OpenImageFileDialog(Program.MainForm);

                    if (string.IsNullOrEmpty(path))
                        return false;

                    image = ImageHelper.LoadImage(path);
                    
                    if (image == null)
                        return false;

                    ClipManager.Clips[ClipManager.CreateClipAtCursor(image, false)].Options.FilePath = path;
                    return true;

                case Tasks.NewClipFromClipboard:
                    image = ClipboardHelper.GetImage(true);
                    
                    if (image == null)
                        return false;

                    ClipManager.CreateClipAtCursor(image, false);
                    return true;

                case Tasks.ScreenColorPicker:
                    ImageHandler.RegionCapture(RegionCaptureMode.ColorPicker);
                    return true;

                case Tasks.CaptureLastRegion:

                    if (ImageHandler.LastInfo == null || !ScreenHelper.IsValidCropArea(ImageHandler.LastInfo.Region))
                        return false;
                    
                    using (Image img = ScreenShotManager.CaptureRectangle(ScreenHelper.GetRectangle0Based(ImageHandler.LastInfo.Region)))
                    {
                        if (img == null || string.IsNullOrEmpty(ImageHandler.Save(PathHelper.GetNewImageFileName(), img)))
                            return false;

                        if (RegionCaptureOptions.AutoCopyImage)
                            ClipboardHelper.CopyImageDefault(img);
                    }
                    return true;

                case Tasks.CaptureFullScreen:

                    using (Image img = ScreenShotManager.CaptureFullscreen())
                    {
                        if (img == null || string.IsNullOrEmpty(ImageHandler.Save(PathHelper.GetNewImageFileName(), img)))
                            return false;

                        if (RegionCaptureOptions.AutoCopyImage)
                            ClipboardHelper.CopyImageDefault(img);
                    }

                    return true;

                case Tasks.CaptureActiveMonitor:

                    using (Image img = ScreenShotManager.CaptureActiveMonitor())
                    {
                        if (img == null || string.IsNullOrEmpty(ImageHandler.Save(PathHelper.GetNewImageFileName(), img)))
                            return false;

                        if (RegionCaptureOptions.AutoCopyImage)
                            ClipboardHelper.CopyImageDefault(img);
                    }

                    return true;

                case Tasks.CaptureActiveWindow:

                    using (Image img = ScreenShotManager.CaptureRectangle(ScreenHelper.GetWindowRectangle(NativeMethods.GetForegroundWindow())))
                    {
                        if (img == null || string.IsNullOrEmpty(ImageHandler.Save(PathHelper.GetNewImageFileName(), img)))
                            return false;

                        if (RegionCaptureOptions.AutoCopyImage)
                            ClipboardHelper.CopyImageDefault(img);
                    }

                    return true;

                case Tasks.CaptureGif:
                    return false;

                case Tasks.NewOCRCapture:
                    return false;

                case Tasks.ColorWheelPicker:
                    Program.MainForm.ColorPicker_Click(null, EventArgs.Empty);
                    return true;

                case Tasks.HashCheck:
                    Program.MainForm.HashCheck_Click(null, EventArgs.Empty);
                    return true;

                case Tasks.Regex:
                    Program.MainForm.Regex_Click(null, EventArgs.Empty);
                    return true;

                case Tasks.QRCode:
                    Program.MainForm.QrCode_Click(null, EventArgs.Empty);
                    return true;

                case Tasks.OpenMainForm:
                    Program.MainForm.ForceActivate();
                    return true;
            }
            return result;
        }
    }
}
