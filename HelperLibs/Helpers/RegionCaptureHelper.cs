using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace WinkingCat.HelperLibs
{
    public static class RegionCaptureHelper
    {
        public delegate void RequestShowFormsEvent(bool show);
        public static event RequestShowFormsEvent RequestShowForms;

        public static RegionReturn LastRegionResult { get; private set; }

        /// <summary>
        /// Sends a global event notifying all forms to hide or show themselves.
        /// </summary>
        /// <param name="show">False if the forms should hide, True if they should be shown.</param>
        /// <param name="sleepThread">Should Thread.Sleep be called after hiding / showing.</param>
        public static void RequestFormsHide(bool show, bool sleepThread = true)
        {
            if (!SettingsManager.MainFormSettings.Hide_Form_On_Captrue)
                return;

            if (RequestShowForms != null)
            {
                RequestShowForms.Invoke(show);
            }

            // when forms are being hidden it may take a bit to fully hide them
            if (sleepThread) 
            {
                Thread.Sleep(SettingsManager.MainFormSettings.Wait_Hide_Time);
            }
        }


        public static bool GetRegionResultImage(out Image image)
        {
            using (RegionCaptureForm regionCapture = new RegionCaptureForm(ScreenHelper.GetScreenBounds(), RegionCaptureMode.Default))
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

        public static bool GetRegionResultColor(out COLOR color)
        {
            using (RegionCaptureForm regionCapture = new RegionCaptureForm(ScreenHelper.GetScreenBounds(), RegionCaptureMode.ColorPicker))
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

        public static void RegionCapture(bool creatClip = false)
        {
            RegionCapture(SettingsManager.RegionCaptureSettings.Mode, creatClip);
        }

        public static void RegionCapture(RegionCaptureMode mode, bool creatClip = false)
        {
            using (RegionCaptureForm regionCapture = new RegionCaptureForm(ScreenHelper.GetScreenBounds(), mode))
            {
                regionCapture.ShowDialog();

                LastRegionResult?.Dispose();
                LastRegionResult = regionCapture.GetRsult();


                if (LastRegionResult.Result == RegionResult.Close)
                    return;

                if(LastRegionResult.Result == RegionResult.Color)
                {
                    if (SettingsManager.RegionCaptureSettings.Auto_Copy_Color)
                    {
                        ClipboardHelper.FormatCopyColor(SettingsManager.MiscSettings.Default_Color_Format, LastRegionResult.Color);
                    }
                    return;
                }

                if (SettingsManager.RegionCaptureSettings.Auto_Copy_Image)
                    ClipboardHelper.CopyImage(LastRegionResult.Image);

                string path = string.Empty;

                if (InternalSettings.Save_Images_To_Disk)
                {
                    path = PathHelper.GetNewImageFileName();
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
            }
        }


        public static string Save(string imageName, Image img)
        {
            if (ImageHelper.SaveImage(img, imageName))
            {
                return imageName;
            }

            return string.Empty;
        }
    }
}
