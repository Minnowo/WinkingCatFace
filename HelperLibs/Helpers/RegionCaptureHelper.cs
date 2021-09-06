using System;
using System.Drawing;


namespace WinkingCat.HelperLibs
{
    public static class RegionCaptureHelper
    {
        public static event EventHandler<RegionReturn> CaptureEvent;
        public static event EventHandler<ImageSavedEvent> ImageSaved;
        public static RegionReturn LastRegionResult { get; private set; }


        public static Image GetRegionResultImage()
        {
            using (RegionCaptureForm regionCapture = new RegionCaptureForm(ScreenHelper.GetScreenBounds()))
            {
                regionCapture.ShowDialog();
                LastRegionResult?.Dispose();
                LastRegionResult = regionCapture.GetResultImage();
                return LastRegionResult.Image.CloneSafe();
            }
        }

        public static void RegionCapture(RegionCaptureMode mode, bool creatClip = false)
        {
            SettingsManager.RegionCaptureSettings.Mode = mode;
            RegionCapture(creatClip);
        }

        public static void RegionCapture(bool creatClip = false)
        {
            using (RegionCaptureForm regionCapture = new RegionCaptureForm(ScreenHelper.GetScreenBounds()))
            {
                regionCapture.ShowDialog();

                LastRegionResult?.Dispose();
                LastRegionResult = regionCapture.GetResultImage();

                if (LastRegionResult.Result == RegionResult.Close)
                    return;

                if(LastRegionResult.Result == RegionResult.Color)
                {
                    if (SettingsManager.RegionCaptureSettings.Auto_Copy_Color)
                        ClipboardHelper.FormatCopyColor(SettingsManager.MiscSettings.Default_Color_Format, LastRegionResult.Color);
                    OnCaptureEvent(LastRegionResult);
                    return;
                }

                if (SettingsManager.RegionCaptureSettings.Auto_Copy_Image)
                    ClipboardHelper.CopyImage(LastRegionResult.Image);

                string path = string.Empty;

                if (InternalSettings.Save_Images_To_Disk)
                    path = PathHelper.GetNewImageFileName();

                if (creatClip)
                {
                    ClipOptions ops = new ClipOptions(ScreenHelper.GetRectangle0Based(LastRegionResult.Region).Location);
                    ops.FilePath = path;
                    ClipManager.CreateClip(LastRegionResult.Image, ops);
                }

                if (InternalSettings.Save_Images_To_Disk)
                    Save(path, LastRegionResult.Image);

                if (LastRegionResult.Image != null)
                    LastRegionResult.Image.Dispose();

                OnCaptureEvent(LastRegionResult);
            }
        }


        public static string Save(string imageName, Image img)
        {
            if (ImageHelper.SaveImage(img, imageName))
            {
                OnImageSaved(imageName, img.Size);
                return imageName;
            }

            return string.Empty;
        }


        private static void OnCaptureEvent(RegionReturn info)
        {
            if (CaptureEvent != null)
            {
                CaptureEvent(null, info);
            }
        }

        private static void OnImageSaved(string info, Size size)
        {
            OnImageSaved(new ImageSavedEvent(info, size));
        }

        private static void OnImageSaved(ImageSavedEvent info)
        {
            if (ImageSaved != null)
            {
                ImageSaved(null, info);
            }
        }
    }
}
