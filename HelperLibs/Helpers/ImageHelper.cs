using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Forms;

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


        /// <summary>
		/// A value from 0-1 which is used to convert a color to grayscale.
		/// <para>Default: 0.3</para>
		/// <para>Gray = (Red * GrayscaleRedMultiplier) + (Green * GrayscaleGreenMultiplier) + (Blue * GrayscaleBlueMultiplier)</para> 
		/// </summary>
		public static double GrayscaleRedMultiplier
        {
            get { return gsrm; }
            set { gsrm = value.Clamp(0, 1); }
        }
        private static double gsrm = 0.3; // 0.21

        /// <summary>
        /// A value from 0-1 which is used to convert a color to grayscale.
        /// <para>Default: 0.59</para>
        /// <para>Gray = (Red * GrayscaleRedMultiplier) + (Green * GrayscaleGreenMultiplier) + (Blue * GrayscaleBlueMultiplier)</para> 
        /// </summary>
        public static double GrayscaleGreenMultiplier
        {
            get { return gsgm; }
            set { gsgm = value.Clamp(0, 1); }
        }
        private static double gsgm = 0.59; // 0.71

        /// <summary>
        /// A value from 0-1 which is used to convert a color to grayscale.
        /// <para>Default: 0.11</para>
        /// <para>Gray = (Red * GrayscaleRedMultiplier) + (Green * GrayscaleGreenMultiplier) + (Blue * GrayscaleBlueMultiplier)</para> 
        /// </summary>
        public static double GrayscaleBlueMultiplier
        {
            get { return gsbm; }
            set { gsbm = value.Clamp(0, 1); }
        }
        private static double gsbm = 0.11; // 0.071


        public static bool SaveImage(string imageName, Image img, ImageFormat format = null)
        {
            if (img == null || string.IsNullOrEmpty(imageName))
                return false;

            if (format == null)
                format = defaultImageFormat;

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

        public static bool SaveImage(Image img, string filePath)
        {
            PathHelper.CreateDirectoryFromFilePath(filePath);
            ImageFormat imageFormat = GetImageFormat(filePath);

            if (img == null)
                return false;

            try
            {
                img.Save(filePath, imageFormat);
                return true;
            }
            catch (Exception e)
            {
                Logger.WriteException(e);
                //e.ShowError();
            }

            return false;
        }

        public static string SaveImageFileDialog(Image img, string filePath = "")
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG (*.png)|*.png|JPEG (*.jpg, *.jpeg, *.jpe, *.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|GIF (*.gif)|*.gif|BMP (*.bmp)|*.bmp|TIFF (*.tif, *.tiff)|*.tif;*.tiff";
                sfd.DefaultExt = "png";

                if (!string.IsNullOrEmpty(filePath))
                {
                    sfd.FileName = Path.GetFileName(filePath);

                    string ext = PathHelper.GetFilenameExtension(filePath);

                    if (!string.IsNullOrEmpty(ext))
                    {
                        ext = ext.ToLowerInvariant();

                        switch (ext)
                        {
                            case "png":
                                sfd.FilterIndex = 1;
                                break;
                            case "jpg":
                            case "jpeg":
                            case "jpe":
                            case "jfif":
                                sfd.FilterIndex = 2;
                                break;
                            case "gif":
                                sfd.FilterIndex = 3;
                                break;
                            case "bmp":
                                sfd.FilterIndex = 4;
                                break;
                            case "tif":
                            case "tiff":
                                sfd.FilterIndex = 5;
                                break;
                        }
                    }
                }

                if (sfd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(sfd.FileName))
                {
                    SaveImage(img, sfd.FileName);
                    return sfd.FileName;
                }
            }

            return null;
        }

        public static ImageFormat GetImageFormat(string filePath)
        {
            ImageFormat imageFormat = ImageFormat.Png;
            string ext = PathHelper.GetFilenameExtension(filePath);

            if (!string.IsNullOrEmpty(ext))
            {
                switch (ext.Trim().ToLower())
                {
                    case "png":
                        imageFormat = ImageFormat.Png;
                        break;
                    case "jpg":
                    case "jpeg":
                    case "jpe":
                    case "jfif":
                        imageFormat = ImageFormat.Jpeg;
                        break;
                    case "gif":
                        imageFormat = ImageFormat.Gif;
                        break;
                    case "bmp":
                        imageFormat = ImageFormat.Bmp;
                        break;
                    case "tif":
                    case "tiff":
                        imageFormat = ImageFormat.Tiff;
                        break;
                }
            }

            return imageFormat;
        }

        public static Bitmap LoadImage(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return null;

            try
            {
                return (Bitmap)Image.FromStream(new MemoryStream(File.ReadAllBytes(path)));
            }
            catch (Exception e)
            {
                Logger.WriteException(e);
            }
            return null;
        }

        public static Size GetImageDimensionsFile(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return Size.Empty;
            
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
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
        }

        /// <summary>
        /// Resize the given image.
        /// <para>The caller is responsible for disposing of the given image.</para>
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="newSize">The new size of the image.</param>
        /// <param name="intr">The interpolationmode to use when resizing.</param>
        /// <returns></returns>
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


        /// <summary>
        /// Crop the given image to the given rectange.
        /// <para>The given bitmap will be disposed and replaced with the cropped bitmap.</para>
        /// </summary>
        /// <param name="srcImage">The image to crop.</param>
        /// <param name="crop">The crop area.</param>
        public static void CropBitmap(ref Bitmap srcImage, Rectangle crop)
        {
            Bitmap newImage = new Bitmap(crop.Width, crop.Height, PixelFormat.Format32bppArgb);
            newImage.SetResolution(srcImage.HorizontalResolution, srcImage.VerticalResolution);

            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawImage(srcImage, new Rectangle(0, 0, crop.Width, crop.Height), crop, GraphicsUnit.Pixel);
            }

            srcImage.Dispose();
            srcImage = newImage;
        }


        /// <summary>
        /// Rotate the given image to the right by 90.
        /// </summary>
        /// <param name="srcImg">The image to rotate.</param>
        public static void RotateRight(Bitmap srcImg)
        {
            srcImg.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }

        /// <summary>
        /// Rotate the given image to the right by 90.
        /// </summary>
        /// <param name="srcImg">The image to rotate.</param>
        public static void RotateRight(Image srcImg)
        {
            srcImg.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }



        /// <summary>
        /// Rotate the given image to the left by 90.
        /// </summary>
        /// <param name="srcImg">The image to rotate.</param>
        public static void RotateLeft(Bitmap srcImg)
        {
            srcImg.RotateFlip(RotateFlipType.Rotate270FlipNone);
        }

        /// <summary>
        /// Rotate the given image to the left by 90.
        /// </summary>
        /// <param name="srcImg">The image to rotate.</param>
        public static void RotateLeft(Image srcImg)
        {
            srcImg.RotateFlip(RotateFlipType.Rotate270FlipNone);
        }


        /// <summary>
        /// Flip the given image in the x axis.
        /// </summary>
        /// <param name="srcImg">The image to flip.</param>
        public static void FlipHorizontal(Bitmap srcImg)
        {
            srcImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
        }

        /// <summary>
        /// Flip the given image in the x axis.
        /// </summary>
        /// <param name="srcImg">The image to flip.</param>
        public static void FlipHorizontal(Image srcImg)
        {
            srcImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
        }



        /// <summary>
        /// Flip the given image in the y axis.
        /// </summary>
        /// <param name="srcImg">The image to flip.</param>
        public static void FlipVertical(Bitmap srcImg)
        {
            srcImg.RotateFlip(RotateFlipType.RotateNoneFlipY);
        }

        /// <summary>
        /// Flip the given image in the y axis.
        /// </summary>
        /// <param name="srcImg">The image to flip.</param>
        public static void FlipVertical(Image srcImg)
        {
            srcImg.RotateFlip(RotateFlipType.RotateNoneFlipY);
        }



        /// <summary>
		/// Gets a array of ARGB colors from the given image.
		/// </summary>
		/// <param name="srcImg">The image.</param>
		/// <returns>An array of color.</returns>
		public static Color[] GetBitmapColors(Image srcImg)
        {
            return GetBitmapColors((Bitmap)srcImg);
        }

        /// <summary>
        /// Gets a array of ARGB colors from the given image.
        /// </summary>
        /// <param name="srcImg">The image.</param>
        /// <returns>An array of color.</returns>
        public static unsafe Color[] GetBitmapColors(Bitmap srcImg)
        {
            BitmapData dstBD = Get32bppArgbBitmapData(srcImg);

            byte* pDst = (byte*)(void*)dstBD.Scan0;

            Color[] result = new Color[srcImg.Width * srcImg.Height];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Color.FromArgb(*(pDst + 3), *(pDst + 2), *(pDst + 1), *pDst);
                pDst += 4;
            }
            srcImg.UnlockBits(dstBD);

            return result;
        }



        /// <summary>
		/// Convert an array of ARGB color to a bitmap of the given size.
		/// </summary>
		/// <param name="srcAry">The array of color.</param>
		/// <param name="size">The dimensions of the bitmap.</param>
		/// <returns>A bitmap of the given size, filled with colors from the given array. If the array is empty return null.</returns>
		public static unsafe Bitmap GetBitmapFromArray(Color[] srcAry, Size size)
        {
            if (srcAry.Length < 1)
                return null;

            Bitmap resultBmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            BitmapData dstBD = Get32bppArgbBitmapData(resultBmp);

            byte* pDst = (byte*)(void*)dstBD.Scan0;

            for (int i = 0; i < srcAry.Length; i++)
            {
                *(pDst++) = srcAry[i].B; // B
                *(pDst++) = srcAry[i].G; // G
                *(pDst++) = srcAry[i].R; // R
                *(pDst++) = srcAry[i].A; // A		 
            }
            resultBmp.UnlockBits(dstBD);

            return resultBmp;
        }



        /// <summary>
        /// Updates a bitmap's pixel data using pointers.
        /// </summary>
        /// <param name="toUpdate"> The bitmap that is going to be written on. </param>
        /// <param name="dataBitmap"> The bitmap that the data comes from. </param>
        /// <returns></returns>
        public static bool UpdateBitmapSafe(Bitmap toUpdate, Bitmap dataBitmap)
        {
            if (toUpdate.Width != dataBitmap.Width || toUpdate.Height != dataBitmap.Height)
                return false;

            try
            {
                UpdateBitmap(toUpdate, dataBitmap);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Updates a bitmap pixel data using pointers.
        /// </summary>
        /// <param name="toUpdate"> The bitmap that is going to be written on. </param>
        /// <param name="dataBitmap"> The bitmap that the data comes from. </param>
        public static unsafe void UpdateBitmap(Bitmap toUpdate, Bitmap dataBitmap)
        {
            Color[] data = GetBitmapColors(dataBitmap);

            BitmapData dstBD = Get32bppArgbBitmapData(toUpdate);

            byte* pDst = (byte*)(void*)dstBD.Scan0;

            for (int i = 0; i < data.Length; i++)
            {
                *(pDst++) = data[i].B; // B
                *(pDst++) = data[i].G; // G
                *(pDst++) = data[i].R; // R
                *(pDst++) = data[i].A; // A		 
            }
            toUpdate.UnlockBits(dstBD);
        }



        /// <summary>
		/// Locks the given bitmap and return bitmap data with a pixel format of 32bppArgb.
		/// </summary>
		/// <param name="srcImg">The image to lock.</param>
		/// <returns>32bppArgb bitmap data.</returns>
		public static BitmapData Get32bppArgbBitmapData(Bitmap srcImg)
        {
            return srcImg.LockBits(
                new Rectangle(0, 0, srcImg.Width, srcImg.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
        }

        /// <summary>
        /// Inverts the colors of a bitmap.
        /// </summary>
        /// <param name="image"> The bitmap to invert </param>
        /// <returns> true if the bitmap was inverted, else false </returns>
        public static bool InvertBitmapSafe(Bitmap image)
        {
            if (image == null)
                return false;

            try
            {
                InvertBitmap(image);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
		/// Invert the color of the given image.
		/// </summary>
		/// <param name="srcImg">The image to invert.</param>
		public static void InvertBitmap(Image srcImg)
        {
            InvertBitmap((Bitmap)srcImg);
        }

        /// <summary>
        /// Invert the color of the given image.
        /// </summary>
        /// <param name="srcImg">The image to invert.</param>
        public static unsafe void InvertBitmap(Bitmap srcImg)
        {
            BitmapData dstBD = Get32bppArgbBitmapData(srcImg);

            byte* pDst = (byte*)(void*)dstBD.Scan0;

            for (int i = 0; i < dstBD.Stride * dstBD.Height; i += 4)
            {
                *pDst = (byte)(255 - *pDst); // invert B
                pDst++;
                *pDst = (byte)(255 - *pDst); // invert G
                pDst++;
                *pDst = (byte)(255 - *pDst); // invert R
                pDst += 2; // skip alpha

                //*pDst = (byte)(255 - *pDst); // invert A
                //pDst++;						 
            }
            srcImg.UnlockBits(dstBD);
        }



        /// <summary>
        /// Convert a bitmap to greyscale.
        /// </summary>
        /// <param name="image"> The bitmap to convert </param>
        /// <returns> true if the bitmap was converted to greyscale, else false </returns>
        public static bool GrayscaleBitmapSafe(Bitmap image)
        {
            if (image == null)
                return false;

            try
            {
                GrayscaleBitmap(image);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
		/// Convert the given image to grayscale.
		/// </summary>
		/// <param name="srcImg">The image to convert.</param>
		public static void GrayscaleBitmap(Image srcImg)
        {
            GrayscaleBitmap((Bitmap)srcImg);
        }

        /// <summary>
        /// Convert the given image to grayscale.
        /// </summary>
        /// <param name="srcImg">The image to convert.</param>
        public static unsafe void GrayscaleBitmap(Bitmap srcImg)
        {
            BitmapData dstBD = Get32bppArgbBitmapData(srcImg);

            byte* pDst = (byte*)(void*)dstBD.Scan0;

            for (int i = 0; i < dstBD.Stride * dstBD.Height; i += 4)
            {
                byte gray = (byte)(
                    (gsbm * *(pDst)) +      // B
                    (gsgm * *(pDst + 1)) +  // G
                    (gsrm * *(pDst + 2)));  // R

                *pDst = gray; // B
                pDst++;
                *pDst = gray; // G
                pDst++;
                *pDst = gray; // R
                pDst += 2;    // Skip alpha

                //*pDst = grey; // A
                //pDst++;
            }
            srcImg.UnlockBits(dstBD);
        }
    }
}
