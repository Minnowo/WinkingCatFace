using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinkingCat.HelperLibs;
using WinkingCat.ClipHelper;

namespace WinkingCat.ScreenCaptureLib
{
    public static class ImageHandler
    {
        public static event EventHandler<LastRegionCaptureInfo> CaptureEvent;
        public static event EventHandler<ImageSavedEvent> ImageSaved;
        public static LastRegionCaptureInfo LastInfo { get; private set; }


        

        public static Image GetRegionResultImage()
        {
            using (ClippingWindowForm regionCapture = new ClippingWindowForm(ScreenHelper.GetScreenBounds()))
            {
                regionCapture.ShowDialog();
                LastInfo?.Dispose();
                LastInfo = regionCapture.GetResultImage();
                return LastInfo.Image.CloneSafe();
            }
        }

        public static void RegionCapture(RegionCaptureMode mode, bool creatClip = false)
        {
            RegionCaptureOptions.mode = mode;
            RegionCapture(creatClip);
        }

        public static void RegionCapture(bool creatClip = false)
        {
            using (ClippingWindowForm regionCapture = new ClippingWindowForm(ScreenHelper.GetScreenBounds()))
            {
                regionCapture.ShowDialog();

                LastInfo?.Dispose();
                LastInfo = regionCapture.GetResultImage();

                if (LastInfo.Result == RegionResult.Close)
                    return;

                if(LastInfo.Result == RegionResult.Color)
                {
                    HandleRegionReturnColor(LastInfo.Color);
                    OnCaptureEvent(LastInfo);
                    return;
                }

                string path = string.Empty;

                if (StaticSettings.Save_Images_To_Disk)
                    path = ImageHelper.newImagePath;

                if (RegionCaptureOptions.autoCopyImage)
                    ClipboardHelper.CopyImageDefault(LastInfo.Image);

                if (creatClip || RegionCaptureOptions.createClipAfterRegionCapture)
                {
                    ClipOptions ops = new ClipOptions();
                    ops.Location = ScreenHelper.GetRectangle0Based(LastInfo.Region).Location;
                    ops.DateCreated = DateTime.Now;
                    ops.Name = Guid.NewGuid().ToString();
                    ops.FilePath = path;

                    using (LastInfo.Image)
                    {
                        ClipManager.CreateClip(LastInfo.Image, ops);

                        if (StaticSettings.Save_Images_To_Disk)
                            Save(path, LastInfo.Image, ImageHelper.defaultImageFormat);
                    }
                }
                else
                {
                    if (StaticSettings.Save_Images_To_Disk)
                        Save(path, LastInfo.Image, ImageHelper.defaultImageFormat);
                }

                OnCaptureEvent(LastInfo);
            }
        }


        public static string Save(string imageName, Image img, ImageFormat format = null)
        {
            if (ImageHelper.SaveImage(imageName, img, format))
            {
                OnImageSaved(imageName);
                return imageName;
            }

            return string.Empty;
        }


        private static void HandleRegionReturnColor(Color color)
        {
            if (RegionCaptureOptions.autoCopyColor)
                ClipboardHelper.FormatCopyColor(ClipboardHelper.copyFormat, color);
        }

        private static void OnCaptureEvent(LastRegionCaptureInfo info)
        {
            if (CaptureEvent != null)
            {
                CaptureEvent(null, info);
            }
        }

        private static void OnImageSaved(string info)
        {
            OnImageSaved(new ImageSavedEvent(info));
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
