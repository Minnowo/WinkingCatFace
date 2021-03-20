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
        public static List<String> images { get; private set; } = new List<string> { };
        public static ImageFormat defaultImageType { get; private set; } = ImageFormat.Png;
        public static LastRegionCaptureInfo LastInfo { get; private set; }

        public static int imagesToHandle { get; private set; } = 10;

        public static void OnCaptureEvent(LastRegionCaptureInfo info)
        {
            if (CaptureEvent != null)
            {
                CaptureEvent(null, info);
            }
        }
        public static void OnImageSaved(string info)
        {
            OnImageSaved(new ImageSavedEvent(info));
        }
        public static void OnImageSaved(ImageSavedEvent info)
        {
            if (ImageSaved != null)
            {
                ImageSaved(null, info);
            }
        }

        public static void Update(int maxImg, ImageFormat _default)
        {
            imagesToHandle = maxImg;
            defaultImageType = _default;
        }

        public static Image GetRegionResultImage()
        {
            using (ClippingWindowForm regionCapture = new ClippingWindowForm(ScreenHelper.GetScreenBounds()))
            {
                regionCapture.ShowDialog();
                LastInfo?.Destroy();
                LastInfo = regionCapture.GetResultImage();
                return LastInfo.Image.CloneSafe();
            }
        }

        public static void RegionCapture()
        {
            using (ClippingWindowForm regionCapture = new ClippingWindowForm(ScreenHelper.GetScreenBounds()))
            {
                regionCapture.ShowDialog();
                LastInfo?.Destroy();
                LastInfo = regionCapture.GetResultImage();

                if (LastInfo.Result != RegionResult.Close)
                {
                    if (LastInfo.Result != RegionResult.Color)
                    {
                        HandleRegionReturnImage(LastInfo.Image);

                        if (RegionCaptureOptions.autoCopyImage)
                            ClipboardHelper.CopyImageDefault(LastInfo.Image);

                        if (RegionCaptureOptions.createClipAfterRegionCapture || RegionCaptureOptions.createSingleClipAfterRegionCapture)
                        {
                            RegionCaptureOptions.createSingleClipAfterRegionCapture = false;
                            ClipOptions ops = new ClipOptions();
                            ops.location = ScreenHelper.GetRectangle0Based(LastInfo.Region).Location;
                            ClipManager.CreateClip(LastInfo.Image, ops);
                        }
                    }
                    else
                    {
                        HandleRegionReturnColor(LastInfo.color);
                    }
                    OnCaptureEvent(LastInfo);
                }
            }
        }


        public static string Save(string imageName = null, ImageFormat format = null, Image img = null)
        {
            if (img == null)
                return string.Empty;

            if (imageName == null)
                imageName = $"{PathHelper.CreateScreenshotSubFolder()}{DateTime.Now.ToString("yyyy-MM-dd h-m-ss-fffff")}.{defaultImageType.ToString().ToLower()}";

            if (format == null)
                format = ImageFormat.Png;

            try
            {
                img.Save(imageName, format);
                Logger.WriteLine(imageName);
                OnImageSaved(imageName);
            }
            catch (Exception e)
            {
                Logger.WriteException(e);
                return string.Empty;
            }
            return imageName;
        }

        public static void HandleRegionReturnColor(Color color)
        {
            if (RegionCaptureOptions.autoCopyColor)
                ClipboardHelper.FormatCopyColor(ClipboardHelper.copyFormat, color);
        }

        public static void HandleRegionReturnImage(Image img)
        {
            string fileName = $"{PathHelper.CreateScreenshotSubFolder()}{DateTime.Now.ToString("yyyy-MM-dd h-m-ss-fffff")}.{defaultImageType.ToString().ToLower()}";
            while (File.Exists(fileName)) fileName = $"{PathHelper.CreateScreenshotSubFolder()}{DateTime.Now.ToString("yyyy-MM-dd h-m-ss-fffff")}.{defaultImageType.ToString().ToLower()}";

            Save(fileName, defaultImageType, img);
            images.Add(fileName);

            if (images.Count > imagesToHandle)
            {
                images.RemoveAt(0);
            }
        }
    }
}
