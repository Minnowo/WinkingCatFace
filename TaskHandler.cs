using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinkingCat.ScreenCaptureLib;
using WinkingCat.HelperLibs;
using System.Drawing.Imaging;
using System.Drawing;

namespace WinkingCat
{

    public static class TaskHandler
    {
        public static Image img;
        public static bool CaptureWindow(WindowInfo window)
        {
            if (ScreenHelper.IsValidCropArea(window.Rectangle))
            {
                img = ScreenShotManager.CaptureRectangle(window.Rectangle);
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
            switch (task)
            {
                case Tasks.RegionCapture:
                    RegionCaptureOptions.mode = RegionCaptureMode.Default;
                    ImageHandler.RegionCapture();
                    break;

                case Tasks.RegionCaptureLite:
                    return false;

                case Tasks.NewClipFromRegionCapture:
                    RegionCaptureOptions.mode = RegionCaptureMode.Default;
                    RegionCaptureOptions.createSingleClipAfterRegionCapture = true;
                    ImageHandler.RegionCapture();
                    break;

                case Tasks.NewClipFromFile:
                    return false;

                case Tasks.NewClipFromClipboard:
                    return false;

                case Tasks.ScreenColorPicker:
                    RegionCaptureOptions.mode = RegionCaptureMode.ColorPicker;
                    ImageHandler.RegionCapture();
                    break;

                case Tasks.CaptureLastRegion:
                    if (ImageHandler.LastInfo != null && ScreenHelper.IsValidCropArea(ImageHandler.LastInfo.Region))
                    {
                        img = ScreenShotManager.CaptureRectangle(ScreenHelper.GetRectangle0Based(ImageHandler.LastInfo.Region));
                        ImageHandler.Save(img: img);
                    }
                    else
                        return false;
                    break;

                case Tasks.CaptureFullScreen:
                    img = ScreenShotManager.CaptureFullscreen();
                    ImageHandler.Save(img: img);
                    break;

                case Tasks.CaptureActiveMonitor:
                    img = ScreenShotManager.CaptureActiveMonitor();
                    ImageHandler.Save(img: img);
                    break;

                case Tasks.CaptureActiveWindow:
                    img = ScreenShotManager.CaptureRectangle(ScreenHelper.GetWindowRectangle(NativeMethods.GetForegroundWindow()));
                    ImageHandler.Save(img: img);
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
                    break;
            }
            img?.Dispose();
            return true;
        }
    }
}
