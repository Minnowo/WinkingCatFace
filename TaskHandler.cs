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
            if (!ScreenHelper.IsValidCropArea(window.Rectangle))
                return false;
            
            using (Image img = ScreenShotManager.CaptureRectangle(window.Rectangle))
            {
                if (RegionCaptureOptions.AutoCopyImage)
                {
                    ClipboardHelper.CopyImage(img);
                }

                if (img == null || string.IsNullOrEmpty(ImageHandler.Save(PathHelper.GetNewImageFileName(), img)))
                {
                    return false;
                }
                 
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
                    ImageHandler.RegionCapture(RegionCaptureMode.Default);
                    HotkeyManager.tempTgnoreHotkeyPress = false;
                    return true;

                case Function.RegionCaptureLite:
                    HotkeyManager.tempTgnoreHotkeyPress = true;

                    HotkeyManager.tempTgnoreHotkeyPress = false;
                    return false;

                case Function.NewClipFromRegionCapture:
                    HotkeyManager.tempTgnoreHotkeyPress = true;
                    ImageHandler.RegionCapture(RegionCaptureMode.Default, true);
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
                    ImageHandler.RegionCapture(RegionCaptureMode.ColorPicker);
                    return true;

                case Function.CaptureLastRegion:

                    if (ImageHandler.LastInfo == null || !ScreenHelper.IsValidCropArea(ImageHandler.LastInfo.Region))
                        return false;
                    
                    using (Image img = ScreenShotManager.CaptureRectangle(ScreenHelper.GetRectangle0Based(ImageHandler.LastInfo.Region)))
                    {
                        if (img == null || string.IsNullOrEmpty(ImageHandler.Save(PathHelper.GetNewImageFileName(), img)))
                            return false;

                        if (RegionCaptureOptions.AutoCopyImage)
                            ClipboardHelper.CopyImage(img);
                    }
                    return true;

                case Function.CaptureFullScreen:

                    using (Image img = ScreenShotManager.CaptureFullscreen())
                    {
                        if (img == null || string.IsNullOrEmpty(ImageHandler.Save(PathHelper.GetNewImageFileName(), img)))
                            return false;

                        if (RegionCaptureOptions.AutoCopyImage)
                            ClipboardHelper.CopyImage(img);
                    }

                    return true;

                case Function.CaptureActiveMonitor:

                    using (Image img = ScreenShotManager.CaptureActiveMonitor())
                    {
                        if (img == null || string.IsNullOrEmpty(ImageHandler.Save(PathHelper.GetNewImageFileName(), img)))
                            return false;

                        if (RegionCaptureOptions.AutoCopyImage)
                            ClipboardHelper.CopyImage(img);
                    }

                    return true;

                case Function.CaptureActiveWindow:

                    using (Image img = ScreenShotManager.CaptureRectangle(ScreenHelper.GetWindowRectangle(NativeMethods.GetForegroundWindow())))
                    {
                        if (img == null || string.IsNullOrEmpty(ImageHandler.Save(PathHelper.GetNewImageFileName(), img)))
                            return false;

                        if (RegionCaptureOptions.AutoCopyImage)
                            ClipboardHelper.CopyImage(img);
                    }

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
