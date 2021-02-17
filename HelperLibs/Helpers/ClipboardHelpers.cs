﻿using System;
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
            return FormatCopyColor(format, new _Color(color));
        }

        public static bool FormatCopyColor(ColorFormat format, _Color color)
        {
            string formatedColor = "";
            switch (format)
            {
                case ColorFormat.ARGB:
                    formatedColor += string.Format("{0}, {1}, {2}, {3}", color.alpha, color.r, color.g, color.b);
                    Logger.WriteLine("ARGB Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.RGB:
                    formatedColor += string.Format("{0}, {1}, {2}", color.r, color.g, color.b);
                    Logger.WriteLine("RGB Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.Hex:
                    formatedColor += color.hex;
                    Logger.WriteLine("Hex Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.Decminal:
                    formatedColor += color.Decimal.ToString();
                    Logger.WriteLine("Decminal Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.CMYK:
                    formatedColor += color.cmyk.ToString();
                    Logger.WriteLine("CMYK Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.HSL:
                    formatedColor += color.hsl.ToString();
                    Logger.WriteLine("HSL Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.HSV:
                    formatedColor += color.hsb.ToString();
                    Logger.WriteLine("HSV Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.HSB:
                    formatedColor += color.hsb.ToString();
                    Logger.WriteLine("HSB Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.XYZ:
                    formatedColor += color.ToXYZ().ToString();
                    Logger.WriteLine("XYZ Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.Yxy:
                    formatedColor += color.ToYxy().ToString();
                    Logger.WriteLine("Yxy Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.AdobeRGB:
                    formatedColor += color.ToAdobeRGB().ToString();
                    Logger.WriteLine("AdobeRGB Format Color Copied: " + formatedColor);
                    break;

                case ColorFormat.All:
                    formatedColor += string.Format("{0}, {1}, {2}", color.r, color.g, color.b) + "\n"; // rgb
                    formatedColor += string.Format("{0}, {1}, {2}, {3}", color.alpha, color.r, color.g, color.b) + "\n"; // argb
                    formatedColor += color.hex + "\n"; // hex
                    formatedColor += color.Decimal.ToString() + "\n"; // decimal
                    formatedColor += color.cmyk.ToString() + "\n"; // cmyk
                    formatedColor += color.hsb.ToString() + "\n"; // hsb
                    formatedColor += color.hsb.ToString() + "\n"; // hsv
                    formatedColor += color.hsl.ToString(); // hsl
                    Logger.WriteLine("All Formats Color Copied: " + formatedColor);
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
