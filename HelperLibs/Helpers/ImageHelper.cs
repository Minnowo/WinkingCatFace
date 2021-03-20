using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace WinkingCat.HelperLibs
{
    public static class ImageHelper
    {
        public static Bitmap FastLoadImage(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                try
                {
                    using (FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (Image image = Image.FromStream(fileStream, false, false))
                    {
                        return (Bitmap)image.CloneSafe();
                    }
                }
                catch (Exception e)
                {
                    Logger.WriteException(e);
                    return null;
                }
                finally
                {
                    // if you don't call collect lots of the memory used by loading the image is held
                    // when loading a 100mb image 9900 x 9900 without the collect it holds 100mb of memory
                    GC.Collect();
                }
            }
            else
                return null;
        }

        public static Bitmap LoadImage(string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path))
                {
                    if (File.Exists(path))
                    {
                        return (Bitmap)Image.FromStream(new MemoryStream(File.ReadAllBytes(path)));
                    }

                }
            }
            catch (Exception e)
            {
                Logger.WriteException(e);
            }
            return null;
        }

        public static Size GetImageDimensionsFile(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                try
                {
                    using (FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (Image image = Image.FromStream(fileStream, false, false))
                    {
                        return new Size(image.Width, image.Height);
                    }
                }
                catch (Exception e)
                {
                    Logger.WriteException(e);
                    return Size.Empty;
                }
                finally
                {
                    // if you don't call collect lots of the memory used by loading the image is held
                    // when loading a 100mb image 9900 x 9900 without the collect it holds 100mb of memory
                    GC.Collect();
                }
            }
            else
                return Size.Empty;
        }

        public static Bitmap ResizeImage(Bitmap image, Size newSize, InterpolationMode intr = InterpolationMode.NearestNeighbor)
        {
            Bitmap bmp = new Bitmap(newSize.Width, newSize.Height);
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.InterpolationMode = intr;
                gr.PixelOffsetMode = PixelOffsetMode.Half;

                gr.DrawImage(image, new Rectangle(0, 0, newSize.Width, newSize.Height),
                    new Rectangle(0, 0, image.Width, image.Height),
                    GraphicsUnit.Pixel);
            }
            return bmp;
        }
    }
}
