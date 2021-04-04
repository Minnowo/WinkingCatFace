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
            if (ScreenHelper.IsValidCropArea(window.Rectangle))
            {
                using (Image img = ScreenShotManager.CaptureRectangle(window.Rectangle))
                {
                    if (RegionCaptureOptions.autoCopyImage)
                    {
                        ClipboardHelper.CopyImageDefault(img);
                    }

                    if (img == null || string.IsNullOrEmpty(ImageHandler.Save(ImageHelper.newImagePath, img)))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
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

                    result = false;
                    string path = PathHelper.AskChooseImageFile();

                    if (!string.IsNullOrEmpty(path))
                    {
                        result = true;
                        using (Image img = ImageHelper.FastLoadImage(path))
                        {
                            if (img == null)
                            {
                                result = false;
                            }
                            else
                            {
                                Point p = ScreenHelper.GetCursorPosition();
                                ClipOptions ops = new ClipOptions()
                                {
                                    location = new Point(p.X - img.Width / 2, p.Y - img.Height / 2)
                                };
                            }
                        }
                    }

                    break;

                case Tasks.NewClipFromClipboard:

                    result = true;
                    using (Image img = ClipboardHelper.GetImage(true))
                    {
                        if (img == null)
                        {
                            result = false;
                        }
                        else
                        {
                            Point p = ScreenHelper.GetCursorPosition();
                            ClipOptions ops = new ClipOptions() 
                            { 
                                location = new Point(p.X - img.Width / 2, p.Y - img.Height / 2) 
                            };
                        }   
                    }
                        
                    break;

                case Tasks.ScreenColorPicker:
                    
                    RegionCaptureOptions.mode = RegionCaptureMode.ColorPicker;
                    ImageHandler.RegionCapture();
                    result = true;
                    break;

                case Tasks.CaptureLastRegion:

                    result = false;
                    if (ImageHandler.LastInfo != null && ScreenHelper.IsValidCropArea(ImageHandler.LastInfo.Region))
                    {
                        result = true;
                        using (Image img = ScreenShotManager.CaptureRectangle(
                            ScreenHelper.GetRectangle0Based(ImageHandler.LastInfo.Region)))
                        {
                            if (img == null || string.IsNullOrEmpty(ImageHandler.Save(ImageHelper.newImagePath, img)))
                                result = false;

                            else if (RegionCaptureOptions.autoCopyImage)
                                ClipboardHelper.CopyImageDefault(img);
                        }
                    }

                    break;

                case Tasks.CaptureFullScreen:

                    result = true;
                    using (Image img = ScreenShotManager.CaptureFullscreen())
                    {
                        if (img == null || string.IsNullOrEmpty(ImageHandler.Save(ImageHelper.newImagePath, img)))
                            result = false;

                        else if (RegionCaptureOptions.autoCopyImage)
                            ClipboardHelper.CopyImageDefault(img);
                    }
                    
                    break;

                case Tasks.CaptureActiveMonitor:

                    result = true;
                    using (Image img = ScreenShotManager.CaptureActiveMonitor())
                    {
                        if (img == null || string.IsNullOrEmpty(ImageHandler.Save(ImageHelper.newImagePath, img)))
                            result = false;

                        else if (RegionCaptureOptions.autoCopyImage)
                            ClipboardHelper.CopyImageDefault(img);
                    }

                    break;

                case Tasks.CaptureActiveWindow:

                    result = true;
                    using (Image img = ScreenShotManager.CaptureRectangle(ScreenHelper.GetWindowRectangle(NativeMethods.GetForegroundWindow())))
                    {
                        if (img == null || string.IsNullOrEmpty(ImageHandler.Save(ImageHelper.newImagePath, img)))
                            result = false;

                        else if (RegionCaptureOptions.autoCopyImage)
                            ClipboardHelper.CopyImageDefault(img);
                    }

                    break;

                case Tasks.CaptureGif:
                    return false;

                case Tasks.NewOCRCapture:
                    return false;

                case Tasks.ColorWheelPicker:
                    Program.mainForm.ColorPicker_Click(null, EventArgs.Empty);
                    return true;

                case Tasks.HashCheck:
                    Program.mainForm.HashCheck_Click(null, EventArgs.Empty);
                    return true;

                case Tasks.Regex:
                    Program.mainForm.Regex_Click(null, EventArgs.Empty);
                    return true;

                case Tasks.QRCode:
                    Program.mainForm.QrCode_Click(null, EventArgs.Empty);
                    return true;

                case Tasks.OpenMainForm:
                    Program.mainForm.ForceActivate();
                    result = true;
                    break;
            }
            return result;
        }
    }
}
