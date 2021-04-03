﻿using System;
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

        public static int imagesToHandle { get; private set; } = 10;
        public static string newImageName
        {
            get
            {
                ImageHelper.imageCounter++;
                return PathHelper.GetScreenshotFolder() + ImageHelper.imageCounter.ToString() + "." + ImageHelper.newImageName;
            }
        }

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

        public static void Update(int maxImg)
        {
            imagesToHandle = maxImg;
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


        public static string Save(string imageName, Image img, ImageFormat format = null)
        {

            if (ImageHelper.Save(imageName, img, format))
            {
                OnImageSaved(imageName);
                return imageName;
            }
            else
            {
                return string.Empty;
            }
        }

        public static void HandleRegionReturnColor(Color color)
        {
            if (RegionCaptureOptions.autoCopyColor)
                ClipboardHelper.FormatCopyColor(ClipboardHelper.copyFormat, color);
        }

        public static void HandleRegionReturnImage(Image img)
        {
            Save(newImageName, img, ImageHelper.defaultImageFormat);
        }
    }
}
