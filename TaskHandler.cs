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

namespace WinkingCat
{

    public static class TaskHandler
    {
        public static event EventHandler TaskExecuted;
        public static Image img;
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
            if (ScreenHelper.IsValidCropArea(window.Rectangle))
            {
                img = ScreenShotManager.CaptureRectangle(window.Rectangle);
                if (RegionCaptureOptions.autoCopyImage)
                {
                    ClipboardHelpers.CopyImageDefault(img);
                }
                if (string.IsNullOrEmpty(ImageHandler.Save(img: img)))
                {
                    img.Dispose();
                    return false;
                }
                else
                {
                    img.Dispose();
                    return true;
                }
            }
            else
                return false; 
        }

        public static bool ExecuteTask(Tasks task)
        {
            OnTaskExecuted(task);
            switch (task)
            {
                case Tasks.RegionCapture:
                    HotkeyManager.tempTgnoreHotkeyPress = true;
                    RegionCaptureOptions.mode = RegionCaptureMode.Default;
                    ImageHandler.RegionCapture();
                    result = true;
                    HotkeyManager.tempTgnoreHotkeyPress = false;
                    break;

                case Tasks.RegionCaptureLite:
                    HotkeyManager.tempTgnoreHotkeyPress = true;

                    HotkeyManager.tempTgnoreHotkeyPress = false;
                    return false;

                case Tasks.NewClipFromRegionCapture:
                    HotkeyManager.tempTgnoreHotkeyPress = true;
                    RegionCaptureOptions.mode = RegionCaptureMode.Default;
                    RegionCaptureOptions.createSingleClipAfterRegionCapture = true;
                    ImageHandler.RegionCapture();
                    result = true;
                    HotkeyManager.tempTgnoreHotkeyPress = false;
                    break;

                case Tasks.NewClipFromFile:
                    string path = PathHelper.AskChooseImageFile();
                    if (!string.IsNullOrEmpty(path))
                    {
                        img = Bitmap.FromFile(path);
                        Point p = ScreenHelper.GetCursorPosition();
                        ClipOptions ops = new ClipOptions() { location = new Point(p.X - img.Width / 2, p.Y - img.Height / 2) };

                        result = true;
                        if (string.IsNullOrEmpty(ClipManager.CreateClip(img, ops)))
                            result = false;
                    }
                    else
                        result = false;
                    break;

                case Tasks.NewClipFromClipboard:
                    img = ClipboardHelpers.GetImage();
                    if (img != null)
                    {
                        Point p = ScreenHelper.GetCursorPosition();
                        ClipOptions ops = new ClipOptions() { location = new Point(p.X - img.Width / 2, p.Y - img.Height / 2) };

                        result = true;
                        if (string.IsNullOrEmpty(ClipManager.CreateClip(img, ops)))
                            result = false;
                    }
                    else
                        result = false;
                    break;

                case Tasks.ScreenColorPicker:
                    
                    RegionCaptureOptions.mode = RegionCaptureMode.ColorPicker;
                    ImageHandler.RegionCapture();
                    result = true;
                    break;

                case Tasks.CaptureLastRegion:
                    if (ImageHandler.LastInfo != null && ScreenHelper.IsValidCropArea(ImageHandler.LastInfo.Region))
                    {
                        img = ScreenShotManager.CaptureRectangle(ScreenHelper.GetRectangle0Based(ImageHandler.LastInfo.Region));
                        result = true;
                        if (string.IsNullOrEmpty(ImageHandler.Save(img: img)))
                            result = false;
                        if(RegionCaptureOptions.autoCopyImage)
                        {
                            ClipboardHelpers.CopyImageDefault(img);
                        }
                    }
                    else
                        result = false;
                    break;

                case Tasks.CaptureFullScreen:
                    img = ScreenShotManager.CaptureFullscreen();
                    result = true;
                    if (string.IsNullOrEmpty(ImageHandler.Save(img: img)))
                        result = false;
                    if (RegionCaptureOptions.autoCopyImage)
                    {
                        ClipboardHelpers.CopyImageDefault(img);
                    }
                    break;

                case Tasks.CaptureActiveMonitor:
                    img = ScreenShotManager.CaptureActiveMonitor();
                    result = true;
                    if (string.IsNullOrEmpty(ImageHandler.Save(img: img)))
                        result = false;
                    if (RegionCaptureOptions.autoCopyImage)
                    {
                        ClipboardHelpers.CopyImageDefault(img);
                    }
                    break;

                case Tasks.CaptureActiveWindow:
                    img = ScreenShotManager.CaptureRectangle(ScreenHelper.GetWindowRectangle(NativeMethods.GetForegroundWindow()));
                    result = true;
                    if (string.IsNullOrEmpty(ImageHandler.Save(img: img)))
                        result = false;
                    if (RegionCaptureOptions.autoCopyImage)
                    {
                        ClipboardHelpers.CopyImageDefault(img);
                    }
                    break;

                case Tasks.CaptureGif:
                    return false;

                case Tasks.NewOCRCapture:
                    return false;

                case Tasks.ColorWheelPicker:
                    return false;

                case Tasks.HashCheck:
                    return false;

                case Tasks.OpenMainForm:
                    Helpers.ForceActivate(Program.mainForm);
                    result = true;
                    break;
            }
            img?.Dispose();
            return result;
        }
    }
}
