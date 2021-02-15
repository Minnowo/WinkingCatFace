using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{
    public static class ClipboardHelpers
    {
        private const int RETRYTIMES = 20;
        private const int RETRYDELAY = 100;
        private const string FORMAT_PNG = "PNG";
        private const string FORMAT_17 = "Format17";

        private static readonly object ClipboardLock = new object();
        public static ColorFormat copyFormat { get; set; } = ColorFormat.ARGB;

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
            string formatedColor = "";
            switch (format)
            {
                case ColorFormat.ARGB:
                    formatedColor += string.Format("{0}, {1}, {2}", color.R, color.G, color.B);
                    Logger.WriteLine("RGB Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.Hex:
                    formatedColor += string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
                    Logger.WriteLine("Hex Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.Decminal:
                    formatedColor += string.Format("{0}", (color.R * 65536) + (color.G * 256) + color.B);
                    Logger.WriteLine("Decminal Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.CMYK:
                    double modifiedR, modifiedG, modifiedB, c, m, y, k;

                    modifiedR = color.R / 255.0;
                    modifiedG = color.G / 255.0;
                    modifiedB = color.B / 255.0;

                    k = 1 - new List<double>() { modifiedR, modifiedG, modifiedB }.Max();
                    c = (1 - modifiedR - k) / (1 - k);
                    m = (1 - modifiedG - k) / (1 - k);
                    y = (1 - modifiedB - k) / (1 - k);

                    formatedColor += string.Format("{0}, {1}, {2}, {3}", Math.Round(c * 100), Math.Round(m * 100), Math.Round(y * 100), Math.Round(k * 100));
                    Logger.WriteLine("CMYK Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.HSB:
                    formatedColor += string.Format("{0}, {1}, {2}", color.GetHue(), color.GetSaturation(), color.GetBrightness());
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
