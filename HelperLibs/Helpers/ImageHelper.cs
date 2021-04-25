using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

namespace WinkingCat.HelperLibs
{
    public static class ImageHelper
    {
        public static int imageCounter { get; set; } = -1;
        public static string newImageName
        {
            get
            {
                // i just hate hoe jpeg looks in the file system cause i'm used to jpg
                string fileFormat = defaultImageFormat.ToString().ToLower();
                if (fileFormat == "jpeg") fileFormat = "jpg";

                for (int x = 0; x < 10; x++)
                {
                    string fileName = DateTime.Now.Ticks.GetHashCode().ToString("x").ToUpper() + "." + fileFormat;

                    if (!File.Exists(fileName))
                        return fileName;
                }

                while (true)
                {
                    string fileName = string.Format(@"{0}", Guid.NewGuid()) + "." + fileFormat;

                    if (!File.Exists(fileName))
                        return fileName;
                }
            }
        }
        public static string newImagePath
        {
            get
            {
                imageCounter++;
                return PathHelper.GetScreenshotFolder() + ImageHelper.imageCounter.ToString() + "." + ImageHelper.newImageName;
            }
        }
        public static ImageFormat defaultImageFormat = ImageFormat.Jpeg;

        public static bool Save(string imageName, Image img, ImageFormat format = null)
        {
            if (img == null || string.IsNullOrEmpty(imageName))
                return false;

            if (format == null)
                format = defaultImageFormat;

            Console.WriteLine(imageName);
            try
            {
                img.Save(imageName, format);
                return true;
            }
            catch (Exception e)
            {
                Logger.WriteException(e);
                return false;
            }
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
