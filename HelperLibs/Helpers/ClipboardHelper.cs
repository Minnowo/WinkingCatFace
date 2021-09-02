using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.IO;

namespace WinkingCat.HelperLibs
{
    public static class ClipboardHelper
    {
        private const int RETRYTIMES = 20;
        private const int RETRYDELAY = 100;
        private const string FORMAT_PNG = "PNG";
        private const string FORMAT_17 = "Format17";

        private static readonly object ClipboardLock = new object();
        public static ColorFormat copyFormat { get; set; } = ColorFormat.RGB;

        public static bool CopyData(IDataObject data, bool copy = true)
        {
            if (data != null)
            {
                lock (ClipboardLock)
                {
                    Clipboard.SetDataObject(data, copy, RETRYTIMES, RETRYDELAY);
                }

                return true;
            }

            return false;
        }

        public static bool Clear()
        {
            try
            {
                IDataObject data = new DataObject();
                CopyData(data, false);
            }
            catch (Exception e)
            {
                Logger.WriteException(e, "Clipboard clear failed.");
            }

            return false;
        }
        public static bool FormatCopyColor(ColorFormat format, Color color)
        {
            return FormatCopyColor(format, new COLOR(color));
        }

        public static bool FormatCopyColor(ColorFormat format, COLOR color)
        {
            string formatedColor = "";

            switch (format)
            {
                case ColorFormat.ARGB:
                    formatedColor += color.ARGB.ToString(ColorFormat.ARGB);
                    Logger.WriteLine("ARGB Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.RGB:
                    formatedColor += color.ARGB.ToString();
                    Logger.WriteLine("RGB Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.Hex:
                    formatedColor += color.ToHex();
                    Logger.WriteLine("Hex Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.Decminal:
                    formatedColor += color.ToDecimal().ToString();
                    Logger.WriteLine("Decminal Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.CMYK:
                    formatedColor += color.CMYK.ToString();
                    Logger.WriteLine("CMYK Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.HSL:
                    formatedColor += color.HSL.ToString();
                    Logger.WriteLine("HSL Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.HSV:
                    formatedColor += color.HSB.ToString();
                    Logger.WriteLine("HSV Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.HSB:
                    formatedColor += color.HSB.ToString();
                    Logger.WriteLine("HSB Format Color Copied: " + formatedColor);
                    break;
            }
            return CopyStringDefault(formatedColor);
        }

        public static Bitmap GetImage(bool checkContainsImage = false)
        {
            try
            {
                lock (ClipboardLock)
                {
                    if (!checkContainsImage || Clipboard.ContainsImage())
                    {
                        return (Bitmap)Clipboard.GetImage();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteException(e, "Clipboard get image failed.");
            }

            return null;
        }

        public static bool CopyFile(string path)
        {
            if (File.Exists(path))
            {
                StringCollection paths = new StringCollection();
                paths.Add(path);
                Clipboard.SetFileDropList(paths);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CopyImageFromFile(string path)
        {
            try
            {
                using (Image image = ImageHelper.LoadImage(path))
                {
                    if (image != null)
                        return CopyImageDefault(image);
                }
                return false;
            }
            finally
            {
                GC.Collect();
            }
        }

        public static bool CopyStringDefault(string str)
        {
            IDataObject dataObject = new DataObject();
            dataObject.SetData(DataFormats.StringFormat, true, str);

            return CopyData(dataObject);
        }

        public static bool CopyImageDefault(Image img)
        {
            IDataObject dataObject = new DataObject();
            dataObject.SetData(DataFormats.Bitmap, true, img);

            return CopyData(dataObject);
        }
    }
}
