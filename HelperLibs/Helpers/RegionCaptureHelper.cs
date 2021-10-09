using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{
    public static class RegionCaptureHelper
    {
        public static event EventHandler<RegionReturn> CaptureEvent;
        public static event EventHandler<ImageSavedEvent> ImageSaved;
        public static RegionReturn LastRegionResult { get; private set; }

        public static bool GetRegionResultImage(Form form, out Image image)
        {
            Helper.WaitHideForm(form, out bool reshow);

            bool result = GetRegionResultImage(out image);

            if (reshow)
                form.Show();

            return result;
        }

        public static bool GetRegionResultImage(out Image image)
        {
            SettingsManager.RegionCaptureSettings.Mode = RegionCaptureMode.Default;
            using (RegionCaptureForm regionCapture = new RegionCaptureForm(ScreenHelper.GetScreenBounds()))
            {
                regionCapture.ShowDialog();
                LastRegionResult?.Dispose();
                LastRegionResult = regionCapture.GetRsult();

                if (LastRegionResult.Result == RegionResult.Close || LastRegionResult.Image == null)
                {
                    image = null;
                    return false;
                }

                image = LastRegionResult.Image.CloneSafe();
                return true;
            }
        }

        public static bool GetRegionResultColor(Form form, out COLOR color)
        {
            Helper.WaitHideForm(form, out bool reshow);

            bool result =  GetRegionResultColor(out color);
            
            if (reshow)
                form.Show();

            return result;        
        }

        public static bool GetRegionResultColor(out COLOR color)
        {
            SettingsManager.RegionCaptureSettings.Mode = RegionCaptureMode.ColorPicker;
            using (RegionCaptureForm regionCapture = new RegionCaptureForm(ScreenHelper.GetScreenBounds()))
            {
                regionCapture.ShowDialog();
                LastRegionResult?.Dispose();
                LastRegionResult = regionCapture.GetRsult();

                if (LastRegionResult.Result == RegionResult.Close)
                {
                    color = Color.Empty;
                    return false;
                }

                color = LastRegionResult.Color;
                return true;
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
                LastRegionResult = regionCapture.GetRsult();

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
                {
                    path = PathHelper.GetNewImageFileName();
                    if (InternalSettings.Default_Image_Format == ImgFormat.wrm && InternalSettings.Save_WORM_As_DWORM)
                    {
                        path = path.Split('.')[0] + ".dwrm";
                    }
                }

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
