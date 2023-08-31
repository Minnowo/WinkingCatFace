using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WinkingCat.HelperLibs.Enums;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.Rendering;

namespace WinkingCat.HelperLibs
{
    public static class Helper
    {
        public static readonly Version OSVersion = Environment.OSVersion.Version;
        public static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public const string Numbers = "0123456789"; // 48 ... 57
        public const string AlphabetCapital = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // 65 ... 90
        public const string Alphabet = "abcdefghijklmnopqrstuvwxyz"; // 97 ... 122
        public const string Alphanumeric = Numbers + AlphabetCapital + Alphabet;
        public const string AlphanumericInverse = Numbers + Alphabet + AlphabetCapital;
        public const string Hexadecimal = Numbers + "ABCDEF";
        public const string Base58 = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz"; // https://en.wikipedia.org/wiki/Base58
        public const string Base56 = "23456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz"; // A variant, Base56, excludes 1 (one) and o (lowercase o) compared to Base 58.


        /// <summary>
        /// Returns true if the given OS build number is windows 10 or greater.
        /// </summary>
        /// <param name="build">The build number.</param>
        /// <returns>true if 10 or greater, else false.</returns>
        public static bool IsWindows10OrGreater(int build = -1)
        {
            return OSVersion.Major >= 10 && OSVersion.Build >= build;
        }

        /// <summary>
        /// Returns true if the current OS is windows 7.
        /// </summary>
        /// <returns>true if the current OS is windows 7, else false.</returns>
        public static bool IsWindows7()
        {
            return OSVersion.Major == 6 && OSVersion.Minor == 1;
        }

        /// <summary>
        /// Returns true if the current OS is vista or greater.
        /// </summary>
        /// <returns>true if vista or greater, else false.</returns>
        public static bool IsWindowsVistaOrGreater()
        {
            return OSVersion.Major >= 6;
        }

        /// <summary>
        /// Gets a unique ID.
        /// </summary>
        /// <returns>A <see cref="string"/>.</returns>
        public static string GetUniqueID()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// Gets the descriptions of enums.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="skip">The skip.</param>
        /// <returns>A <see cref="string[]"/> of the enum descriptions.</returns>
        public static string[] GetEnumDescriptions<T>(int skip = 0)
        {
            return Enum.GetValues(typeof(T)).OfType<Enum>().Skip(skip).Select(x => x.GetDescription()).ToArray();
        }

        /// <summary>
        /// Checks if a rectangle has a valid width / height.
        /// </summary>
        /// <param name="rect">The <see cref="Rectangle"/>.</param>
        /// <returns>true if its valid, else false.</returns>
        public static bool IsValidCropArea(Rectangle rect)
        {
            return rect.Width > 0 && rect.Height > 0;
        }

        /// <summary>
        /// Checks if a rectangle has a valid width / height.
        /// </summary>
        /// <param name="a">Point 1 of the rectangle.</param>
        /// <param name="b">Point 2 of the rectangle.</param>
        /// <returns>true if its valid, else false.</returns>
        public static bool IsValidCropArea(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) > 0 && Math.Abs(a.Y - b.Y) > 0;
        }

        /// <summary>
        /// Creates a rectangle from the given points.
        /// </summary>
        /// <param name="a">Point 1.</param>
        /// <param name="b">Point 2.</param>
        /// <returns>A <see cref="Rectangle"/>.</returns>
        public static Rectangle CreateRect(Point a, Point b)
        {
            int x = Math.Min(a.X, b.X);
            int y = Math.Min(a.Y, b.Y);
            int width = Math.Abs(a.X - b.X);
            int height = Math.Abs(a.Y - b.Y);

            return new Rectangle(new Point(x, y), new Size(width, height));
        }

        public static int StringCompareNatural(string a, string b, StringComparison comparison = StringComparison.CurrentCulture)
        {
            Regex regex = new Regex(@"\d+", RegexOptions.Compiled);

            int max = new[] { a, b }.SelectMany(x => regex.Matches(x).Cast<Match>().Select(y => (int?)y.Value.Length)).Max() ?? 0;

            return string.Compare(
                regex.Replace(a, m => m.Value.PadLeft(max, '0')),
                regex.Replace(b, m => m.Value.PadLeft(max, '0')), comparison);
        }



        public static Image CreateBarCode(string text, int width, int height, BarcodeFormat format = BarcodeFormat.QR_CODE)
        {
            if (!CheckQRCodeContent(text))
                return null;

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
            catch
            {
            }

            return null;
        }

        public static Image CreateQRCode(string text, int size, BarcodeFormat format = BarcodeFormat.QR_CODE)
        {
            return CreateBarCode(text, size, size, format);
        }

        /// <summary>
        /// Reads the given barcade into a string[].
        /// </summary>
        /// <param name="bmp">The barcode.</param>
        /// <param name="scanQRCodeOnly">Should only scan QR codes.</param>
        /// <returns>The text from the given barcode.</returns>
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
            catch
            {
            }

            return null;
        }

        public static bool CheckQRCodeContent(string content)
        {
            return !string.IsNullOrEmpty(content) && Encoding.UTF8.GetByteCount(content) <= 2952;
        }


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
            }
        }

        public static string SizeSuffix(long value, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0)
                decimalPlaces = 0;

            if (value < 0)
                return "-" + SizeSuffix(-value, decimalPlaces);

            if (value == 0)
                return string.Format("{0:n" + decimalPlaces + "} bytes", 0);

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

            return string.Format("{0:n" + decimalPlaces + "} {1}", adjustedSize, SizeSuffixes[mag]);
        }

        public static string SizeSuffix(Int64 value, FileSizeUnit fsu, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0) { return "-" + SizeSuffix(-value, fsu, decimalPlaces); }
            if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} {1}", 0, SizeSuffixes[(int)fsu]); }

            int mag = (int)fsu;
            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            double adjustedSize = (double)value / (1L << (mag * 10));
            return string.Format("{0:n" + decimalPlaces + "} {1}", adjustedSize, SizeSuffixes[mag]);
        }
    }
}
