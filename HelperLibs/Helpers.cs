using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace WinkingCat.HelperLibs
{
    public class Helpers
    {
        public static readonly Version OSVersion = Environment.OSVersion.Version;
        public static readonly string[] SizeSuffixes =
                   { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
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
        public static Size GetImageDimensionsFile(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                using (FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    try
                    {
                        using (Image image = Image.FromStream(fileStream, false, false))
                        {
                            return new Size(image.Width, image.Height);
                        }
                    }
                    catch(Exception e)
                    {
                        Logger.WriteException(e);
                        return Size.Empty;
                    }
                    
                }
            }
            else
                return Size.Empty;
        }
        public static void ForceActivate(Form form)
        {
            if (!form.IsDisposed)
            {
                if (!form.Visible)
                {
                    form.Show();
                }

                if (form.WindowState == FormWindowState.Minimized)
                {
                    form.WindowState = FormWindowState.Normal;
                }

                form.BringToFront();
                form.Activate();
            }
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
