using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinkingCat.ScreenCaptureLib;
using WinkingCat.HelperLibs;
using System.Drawing.Imaging;
using System.Drawing;

namespace WinkingCat.HotkeyLib
{

    public static class TaskHandler
    {

        public static bool CaptureWindow(WindowInfo window)
        {
            Console.WriteLine(window.Rectangle);
            if (ScreenHelper.IsValidCropArea(window.Rectangle))              
                if (string.IsNullOrEmpty(ImageHandler.Save(img: ScreenShotManager.CaptureRectangle(window.Rectangle))))
                    return false;
                else
                    return true;
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
                    return false;

                case Tasks.NewClipFromFile:
                    return false;

                case Tasks.NewClipFromClipboard:
                    return false;

                case Tasks.ScreenColorPicker:
                    RegionCaptureOptions.mode = RegionCaptureMode.ColorPicker;
                    ImageHandler.RegionCapture();
                    return false;

                case Tasks.CaptureLastRegion:
                    if (ImageHandler.LastInfo != null && ScreenHelper.IsValidCropArea(ImageHandler.LastInfo.Region))
                    {
                        ImageHandler.Save(img: ScreenShotManager.CaptureRectangle(ScreenHelper.GetRectangle0Based(ImageHandler.LastInfo.Region)));
                    }
                    else
                        return false;
                    break;

                case Tasks.CaptureFullScreen:
                    ImageHandler.Save(img: ScreenShotManager.CaptureFullscreen());                    
                    break;

                case Tasks.CaptureActiveMonitor:
                    ImageHandler.Save(img: ScreenShotManager.CaptureActiveMonitor());
                    break;

                case Tasks.CaptureActiveWindow:
                    return false;

                case Tasks.CaptureGif:
                    return false;

                case Tasks.NewOCRCapture:
                    return false;

                case Tasks.ColorWheelPicker:
                    return false;

                case Tasks.HashCheck:
                    return false;
            }
            return true;
        }
    }
}
