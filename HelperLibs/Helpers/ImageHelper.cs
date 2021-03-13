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
