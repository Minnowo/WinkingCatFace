using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.Rendering;
using System.Diagnostics;

namespace WinkingCat.HelperLibs
{
    public class Helpers
    {
        public static readonly Version OSVersion = Environment.OSVersion.Version;
        public static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        #region just stolen from sharex changed 1 function a little bit
        public static Image CreateBarCode(string text, int width, int height, BarcodeFormat format = BarcodeFormat.QR_CODE)
        {
            if (CheckQRCodeContent(text))
            {
                try
                {
                    BarcodeWriter writer = new BarcodeWriter
                    {
                        Format = format,

                        Options = new QrCodeEncodingOptions
                        {
                            Width = width,
                            Height = height,
                            CharacterSet = "UTF-8"
                        },
                        Renderer = new BitmapRenderer()
                    };

                    return writer.Write(text);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return null;
        }

        public static Size StringToSize(string s, string split = ",")
        {
            try
            {
                string[] sizeParts = s.Split(new string[] { split }, StringSplitOptions.None);
                return new Size(int.Parse(sizeParts[0]), int.Parse(sizeParts[1]));
            }
            catch
            {
                return Size.Empty;
            }
        }

        public static Image CreateQRCode(string text, int size, BarcodeFormat format = BarcodeFormat.QR_CODE)
        {
            return CreateBarCode(text, size, size, format);
        }

        public static string[] BarcodeScan(Bitmap bmp, bool scanQRCodeOnly = false)
        {
            try
            {
                BarcodeReader barcodeReader = new BarcodeReader
                {
                    AutoRotate = true,
                    TryInverted = true,
                    Options = new DecodingOptions
                    {
                        TryHarder = true
                    }
                };

                if (scanQRCodeOnly)
                {
                    barcodeReader.Options.PossibleFormats = new List<BarcodeFormat>() { BarcodeFormat.QR_CODE };
                }

                Result[] results = barcodeReader.DecodeMultiple(bmp);

                if (results != null)
                {
                    return results.Where(x => x != null && !string.IsNullOrEmpty(x.Text)).Select(x => x.Text).ToArray();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        public static bool CheckQRCodeContent(string content)
        {
            return !string.IsNullOrEmpty(content) && Encoding.UTF8.GetByteCount(content) <= 2952;
        }
        #endregion

        public static void LaunchProcess(string path, string args, bool asAdmin = false)
        {
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = true;
            startInfo.Verb = "runas";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = path;
            startInfo.Arguments = args;

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error.
            }
        }

        public static string SizeSuffix(Int64 value, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0) { return "-" + SizeSuffix(-value, decimalPlaces); }
            if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        }

        public static Keys ModifierAsKey(Modifiers mod)
        {
            // this is really dumb but i can't think of a better way of doing this tbh so
            switch (mod)
            {
                case Modifiers.Shift:
                    return Keys.Shift;
                case Modifiers.Shift | Modifiers.Control | Modifiers.Alt | Modifiers.Win:
                    return Keys.Shift | Keys.Control | Keys.Alt | Keys.LWin;
                case Modifiers.Shift | Modifiers.Control | Modifiers.Alt:
                    return Keys.Shift | Keys.Control | Keys.Alt;
                case Modifiers.Shift | Modifiers.Control:
                    return Keys.Shift | Keys.Control;
                case Modifiers.Shift | Modifiers.Alt:
                    return Keys.Shift | Keys.Alt;
                case Modifiers.Shift | Modifiers.Win:
                    return Keys.Shift | Keys.LWin;
                case Modifiers.Control:
                    return Keys.Control;
                case Modifiers.Control | Modifiers.Alt:
                    return Keys.Control | Keys.Alt;
                case Modifiers.Control | Modifiers.Win:
                    return Keys.Control | Keys.LWin;
                case Modifiers.Alt:
                    return Keys.Alt;
                case Modifiers.Alt | Modifiers.Win:
                    return Keys.Alt | Keys.LWin;
                case Modifiers.Win:
                    return Keys.LWin;
            }
            return Keys.None;
        }

        public static bool IsWindows10OrGreater(int build = -1)
        {
            return OSVersion.Major >= 10 && OSVersion.Build >= build;
        }
        

        public static string[] GetEnumDescriptions<T>(int skip = 0)
        {
            return Enum.GetValues(typeof(T)).OfType<Enum>().Skip(skip).Select(x => x.GetDescription()).ToArray();
        }

        public static string GetUniqueID()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static bool IsWindowsVistaOrGreater()
        {
            return OSVersion.Major >= 6;
        }
        public static IntPtr CreateLParam(int x, int y)
        {
            return (IntPtr)(x | (y << 16));
        }

        public static IntPtr CreateLParam(Point p)
        {
            return CreateLParam(p.X, p.Y);
        }

        
    }
}
