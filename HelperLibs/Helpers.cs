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
        public static Size GetImageDimensionsFile(string imagePath)
        {
            using (FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (Image image = Image.FromStream(fileStream, false, false))
                {
                    return new Size(image.Width, image.Height);
                }
            }
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
            return Environment.OSVersion.Version.Major >= 6;
        }
        public static IntPtr CreateLParam(int x, int y)
        {
            return (IntPtr)(x | (y << 16));
        }

        public static IntPtr CreateLParam(Point p)
        {
            return CreateLParam(p.X, p.Y);
        }

        public static void AskSaveImage(Image img)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "All Graphics Types | *.bmp; *.jpg; *.jpeg; *.png; *.tiff|JPeg Image|*.jpg|Png Image|*.png|Bitmap Image|*.bmp|Gif Image|*.gif|Tiff Image|*.tiff";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                FileStream fs = (FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.

                Dictionary<string, int> formatIndex = new Dictionary<string, int>
                {
                    { "jpg", 1 },
                    { "jpeg", 1 },
                    { "png", 2 },
                    { "bmp", 3 },
                    { "gif", 4 },
                    { "tiff", 5 }
                };

                string fileType = saveFileDialog1.FileName.Split('.').Last().ToLower();

                if (formatIndex.ContainsKey(fileType))
                    saveFileDialog1.FilterIndex = formatIndex[fileType];
                try
                {
                    switch (saveFileDialog1.FilterIndex)
                    {
                        case 1:
                            img.Save(fs, ImageFormat.Jpeg);
                            break;

                        case 2:
                            img.Save(fs, ImageFormat.Png);
                            break;

                        case 3:
                            img.Save(fs, ImageFormat.Bmp);
                            break;

                        case 4:
                            img.Save(fs, ImageFormat.Gif);
                            break;

                        case 5:
                            img.Save(fs, ImageFormat.Tiff);
                            break;
                    }
                }catch(Exception e)
                {
                    Logger.WriteException(e, "failed to save image");
                    MessageBox.Show("could not save file");
                }
                

                fs.Close();
                fs.Dispose();
                img.Dispose();
            }
        }
    }
}
