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
        /// Returns the image format based off the extension of the filepath.
        /// </summary>
        /// <param name="filePath">The filepath to check the extension of.</param>
        /// <returns></returns>
        public static ImgFormat GetImageFormat(string filePath)
        {
            string ext = PathHelper.GetFilenameExtension(filePath);

            if (string.IsNullOrEmpty(ext))
                return InternalSettings.Default_Image_Format;

            switch (ext)
            {
                case "png":
                    return ImgFormat.png;
                case "jpg":
                case "jpeg":
                case "jpe":
                case "jfif":
                    return ImgFormat.jpg;
                case "gif":
                    return ImgFormat.gif;
                case "bmp":
                    return ImgFormat.bmp;
                case "tif":
                case "tiff":
                    return ImgFormat.tif;
                case "webp":
                    return ImgFormat.webp;
            }
            return ImgFormat.nil;
        }


        /// <summary>
        /// Saves an image.
        /// </summary>
        /// <param name="img"> The image to save. </param>
        /// <param name="filePath"> The path to save the image. </param>
        /// <param name="collectGarbage"> A bool indicating if GC.Collect should be called after saving. </param>
        /// <returns> true if the image was saved successfully, else false </returns>
        public static bool SaveImage(Image img, string filePath, bool collectGarbage = true)
        {
            if (img == null || string.IsNullOrEmpty(filePath))
                return false;

            PathHelper.CreateDirectoryFromFilePath(filePath);

            try
            {
                switch (GetImageFormat(filePath))
                {
                    default:
                    case ImgFormat.png:
                        img.Save(filePath, ImageFormat.Png);
                        return true;
                    case ImgFormat.jpg:
                        img.Save(filePath, ImageFormat.Jpeg);
                        return true;
                    case ImgFormat.bmp:
                        img.Save(filePath, ImageFormat.Bmp);
                        return true;
                    case ImgFormat.gif:
                        img.Save(filePath, ImageFormat.Gif);
                        return true;
                    case ImgFormat.tif:
                        img.Save(filePath, ImageFormat.Tiff);
                        return true;
                    case ImgFormat.webp:
                        return SaveWebp(img, filePath, InternalSettings.WebpQuality_Default);
                }
            }
            catch (Exception e)
            {
                Logger.WriteException(e);
                e.ShowError();
                return false;
            }
            finally
            {
                if (collectGarbage)
                {
                    GC.Collect();
                }
            }
        }

        /// <summary>
        /// Saves an image.
        /// </summary>
        /// <param name="img"> The image to save. </param>
        /// <param name="filePath"> The path to save the image. </param>
        /// <param name="collectGarbage"> A bool indicating if GC.Collect should be called after saving. </param>
        /// <returns> true if the image was saved successfully, else false </returns>
        public static bool SaveImage(Bitmap img, string filePath, bool collectGarbage = true)
        {
            return SaveImage((Image)img, filePath, collectGarbage);
        }

        /// <summary>
        /// Save a bitmap as a webp file. (Requires the libwebp_x64.dll or libwebp_x86.dll)
        /// </summary>
        /// <param name="img"> The bitmap to encode. </param>
        /// <param name="filePath"> The path to save the bitmap. </param>
        /// <param name="q"> The webp quality args. </param>
        /// <param name="collectGarbage"> A bool indicating if GC.Collect should be called after saving. </param>
        /// <returns> true if the bitmap was saved successfully, else false </returns>
        public static bool SaveWebp(Bitmap img, string filePath, WebPQuality q, bool collectGarbage = true)
        {
            if (!InternalSettings.WebP_Plugin_Exists || string.IsNullOrEmpty(filePath) || img == null)
                return false;

            if (q == WebPQuality.empty)
                q = InternalSettings.WebpQuality_Default;

            try
            {
                using (WebP webp = new WebP())
                {
                    byte[] rawWebP;

                    switch (q.Format)
                    {
                        default:
                        case WebpEncodingFormat.EncodeLossless:
                            rawWebP = webp.EncodeLossless(img, q.Speed);
                            break;
                        case WebpEncodingFormat.EncodeNearLossless:
                            rawWebP = webp.EncodeNearLossless(img, q.Quality, q.Speed);
                            break;
                        case WebpEncodingFormat.EncodeLossy:
                            rawWebP = webp.EncodeLossy(img, q.Quality, q.Speed);
                            break;
                    }

                    File.WriteAllBytes(filePath, rawWebP);
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.WriteException(e);
                e.ShowError();
                return false;
            }
            finally
            {
                if (collectGarbage)
                {
                    GC.Collect();
                }
            }
        }

        /// <summary>
        /// Save an image as a webp file. (Requires the libwebp_x64.dll or libwebp_x86.dll)
        /// </summary>
        /// <param name="img"> The image to encode. </param>
        /// <param name="filePath"> The path to save the image. </param>
        /// <param name="q"> The webp quality args. </param>
        /// <param name="collectGarbage"> A bool indicating if GC.Collect should be called after saving. </param>
        /// <returns> true if the image was saved successfully, else false </returns>
        public static bool SaveWebp(Image img, string filePath, WebPQuality q, bool collectGarbage = true)
        {
            return SaveWebp((Bitmap)img, filePath, q, collectGarbage);
        }

        /// <summary>
        /// Opens a save file dialog asking where to save an image.
        /// </summary>
        /// <param name="img"> The image to save. </param>
        /// <param name="filePath"> The path to open. </param>
        /// <param name="collectGarbage"> A bool indicating if GC.Collect should be called after saving. </param>
        /// <returns> The filename of the saved image, null if failed to save or canceled. </returns>
        public static string SaveImageFileDialog(Image img, string filePath = "", bool collectGarbage = true)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = InternalSettings.Image_Dialog_Filter;
                sfd.DefaultExt = "png";

                if (!string.IsNullOrEmpty(filePath))
                {
                    sfd.FileName = Path.GetFileName(filePath);

                    ImgFormat fmt = GetImageFormat(filePath);

                    if (fmt != ImgFormat.nil)
                    {
                        switch (fmt)
                        {
                            case ImgFormat.png:
                                sfd.FilterIndex = 1;
                                break;
                            case ImgFormat.jpg:
                                sfd.FilterIndex = 2;
                                break;
                            case ImgFormat.gif:
                                sfd.FilterIndex = 3;
                                break;
                            case ImgFormat.bmp:
                                sfd.FilterIndex = 4;
                                break;
                            case ImgFormat.tif:
                                sfd.FilterIndex = 5;
                                break;
                            case ImgFormat.webp:
                                if (InternalSettings.WebP_Plugin_Exists)
                                {
                                    sfd.FilterIndex = 6;
                                    break;
                                }
                                sfd.FilterIndex = 2;
                                break;
                        }
                    }
                }

                if (sfd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(sfd.FileName))
                {
                    SaveImage(img, sfd.FileName, collectGarbage);
                    return sfd.FileName;
                }
            }

            return null;
        }


        /// <summary>
        /// Opens a select file dialog for readable image types.
        /// </summary>
        /// <param name="form">The parent form of the dialog.</param>
        /// <param name="initialDirectory">The initial directory to open the dialog in.</param>
        /// <returns>The path to the selected file.</returns>
        public static string OpenImageFileDialog(Form form = null, string initialDirectory = null)
        {
            string[] result = OpenImageFileDialog(false, form, initialDirectory);
            if (result == null || result.Length < 1)
                return string.Empty;
            return result[0];
        }

        /// <summary>
        /// Opens a select file dialog for readable image types.
        /// </summary>
        /// <param name="multiselect">Allow the user to select multiple files.</param>
        /// <param name="form">The parent form of the dialog.</param>
        /// <param name="initialDirectory">The initial directory to open the dialog in.</param>
        /// <returns>A string[] of image paths to the selected files.</returns>
        public static string[] OpenImageFileDialog(bool multiselect, Form form = null, string initialDirectory = null)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = InternalSettings.Image_Select_Dialog_Title + " (" +
                    string.Join(", ", InternalSettings.Readable_Image_Formats_Dialog_Options) + ")|" +
                    string.Join(";", InternalSettings.Readable_Image_Formats_Dialog_Options);

                ofd.Multiselect = multiselect;

                if (!string.IsNullOrEmpty(initialDirectory))
                {
                    ofd.InitialDirectory = initialDirectory;
                }

                if (ofd.ShowDialog(form) == DialogResult.OK)
                {
                    return ofd.FileNames;
                }
            }

            return null;
        }

        /// <summary>
        /// Load a wemp image. (Requires the libwebp_x64.dll or libwebp_x86.dll)
        /// </summary>
        /// <param name="path"> The path to the image. </param>
        /// <returns> A bitmap object if the image is loaded, otherwise null. </returns>
        public static Bitmap LoadWebP(string path, bool supressError = false)
        {
            if (!InternalSettings.WebP_Plugin_Exists || string.IsNullOrEmpty(path) || !File.Exists(path))
                return null;

            try
            {
                using (WebP webp = new WebP())
                    return webp.Load(path);
            }
            catch (Exception e)
            {
                if (supressError)
                    return null;

                Logger.WriteException(e);
                e.ShowError();
            }
            return null;
        }

        /// <summary>
        /// Loads an image.
        /// </summary>
        /// <param name="path"> The path to the image. </param>
        /// <returns> A bitmap object if the image is loaded, otherwise null. </returns>
        public static Bitmap LoadImage(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return null;

            try
            {
                if (InternalSettings.WebP_Plugin_Exists)
                    if (GetImageFormat(path) == ImgFormat.webp)
                    {
                        return LoadWebP(path);
                    }
                return (Bitmap)Image.FromStream(new MemoryStream(File.ReadAllBytes(path)));
            }
            catch (Exception e)
            {
                // in case the file doesn't have proper extension there is no harm in trying to load as webp
                Bitmap tryLoadWebP;
                if ((tryLoadWebP = LoadWebP(path, true)) != null)
                    return tryLoadWebP;

                Logger.WriteException(e);
                e.ShowError();
            }
            return null;
        }



        /// <summary>
        /// Gets the size of an image from a file.
        /// </summary>
        /// <param name="imagePath"> Path to the image. </param>
        /// <returns> The Size of the image, or Size.Empty if failed to load / not valid image. </returns>
        public static Size GetImageDimensionsFromFile(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return Size.Empty;

            Size s = ImageDimensionReader.GetDimensions(path);
            if (s != Size.Empty)
                return s;

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
        public static Bitmap GetResizedBitmap(Bitmap image, Size newSize, InterpolationMode intr = InterpolationMode.NearestNeighbor)
        {
            Bitmap bmp = new Bitmap(newSize.Width, newSize.Height, image.PixelFormat);
            bmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

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
        public static void CropBitmapByRef(ref Bitmap srcImage, Rectangle crop)
        {
            Bitmap newImage = new Bitmap(crop.Width, crop.Height, srcImage.PixelFormat);
            newImage.SetResolution(srcImage.HorizontalResolution, srcImage.VerticalResolution);

            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawImage(srcImage, new Rectangle(0, 0, crop.Width, crop.Height), crop, GraphicsUnit.Pixel);
            }

            srcImage.Dispose();
            srcImage = newImage;
        }

        /// <summary>
        /// Gets a cropped out area from point A to Point B from the given bitmap as the given pixelformat.
        /// </summary>
        /// <param name="a">Point A.</param>
        /// <param name="b">Point B.</param>
        /// <param name="srcImage">The image to crop from.</param>
        /// <param name="pf">The pixel format of the resultant image.</param>
        /// <returns>A new image cropped from the srcImage of the given pixelformat.</returns>
        public static Bitmap GetCroppedBitmap(Point a, Point b, Image srcImage, PixelFormat pf)
        {
            return GetCroppedBitmap(
                new Rectangle(
                    new Point(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y)),
                    new Size(Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y))), srcImage, pf);
        }

        /// <summary>
        /// Gets a cropped out area from the given rectangle from the given bitmap as the given pixelformat.
        /// </summary>
        /// <param name="cropArea">The rectangle to crop.</param>
        /// <param name="srcImage">The image to crop from.</param>
        /// <param name="pf">The pixel format of the resultant image.</param>
        /// <returns>A new image cropped from the srcImage of the given pixelformat.</returns>
        public static Bitmap GetCroppedBitmap(Rectangle cropArea, Image srcImage, PixelFormat pf)
        {
            Bitmap newImage = new Bitmap(cropArea.Width, cropArea.Height, pf);
            newImage.SetResolution(srcImage.HorizontalResolution, srcImage.VerticalResolution);

            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawImage(srcImage, new Rectangle(0, 0, cropArea.Width, cropArea.Height), cropArea, GraphicsUnit.Pixel);
            }
            return newImage;
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
        public static unsafe void UpdateBitmap(Bitmap toUpdate, Image dataBitmap)
        {
            UpdateBitmap(toUpdate, (Bitmap)dataBitmap);
        }

        /// <summary>
        /// Updates a bitmap pixel data using pointers.
        /// </summary>
        /// <param name="toUpdate"> The bitmap that is going to be written on. </param>
        /// <param name="dataBitmap"> The bitmap that the data comes from. </param>
        public static unsafe void UpdateBitmap(Image toUpdate, Image dataBitmap)
        {
            UpdateBitmap((Bitmap)toUpdate, (Bitmap)dataBitmap);
        }

        /// <summary>
        /// Updates a bitmap pixel data using pointers.
        /// </summary>
        /// <param name="toUpdate"> The bitmap that is going to be written on. </param>
        /// <param name="dataBitmap"> The bitmap that the data comes from. </param>
        public static unsafe void UpdateBitmap(Image toUpdate, Bitmap dataBitmap)
        {
            UpdateBitmap((Bitmap)toUpdate, dataBitmap);
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


        /// <summary>
        /// A class used for reading the width and height of an image file using their headers.
        /// </summary>
        // Mod of https://stackoverflow.com/a/60667939
        public static class ImageDimensionReader
        {
            const byte MAX_MAGIC_BYTE_LENGTH = 8;

            readonly static Dictionary<byte[], Func<BinaryReader, Size>> imageFormatDecoders = new Dictionary<byte[], Func<BinaryReader, Size>>()
        {
            { new byte[] { 0x42, 0x4D }, DecodeBitmap },
            { new byte[] { 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 }, DecodeGif },
            { new byte[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 }, DecodeGif },
            { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }, DecodePng },
            { new byte[] { 0xff, 0xd8 }, DecodeJfif },
            { new byte[] { 0x52, 0x49, 0x46, 0x46 }, DecodeWebP },
            { new byte[] { 0x49, 0x49, 0x2A },  DecodeTiffLE }, // little endian
            { new byte[] { 0x4D, 0x4D, 0x00, 0x2A },  DecodeTiffBE }  // big endian
        };

            /// <summary>        
            /// Gets the dimensions of an image.        
            /// </summary>        
            /// <param name="path">The path of the image to get the dimensions of.</param>        
            /// <returns>The dimensions of the specified image.</returns>        
            /// <exception cref="ArgumentException">The image was of an unrecognised format.</exception>            
            public static Size GetDimensions(BinaryReader binaryReader)
            {
                byte[] magicBytes = new byte[MAX_MAGIC_BYTE_LENGTH];

                for (int i = 0; i < MAX_MAGIC_BYTE_LENGTH; i += 1)
                {
                    magicBytes[i] = binaryReader.ReadByte();

                    foreach (KeyValuePair<byte[], Func<BinaryReader, Size>> kvPair in imageFormatDecoders)
                    {
                        if (StartsWith(magicBytes, kvPair.Key))
                        {
                            return kvPair.Value(binaryReader);
                        }
                    }
                }

                return Size.Empty;
            }

            /// <summary>
            /// Gets the dimensions of an image.
            /// </summary>
            /// <param name="path">The path of the image to get the dimensions of.</param>
            /// <returns>The dimensions of the specified image.</returns>
            /// <exception cref="ArgumentException">The image was of an unrecognized format.</exception>
            public static Size GetDimensions(string path)
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    try
                    {
                        return GetDimensions(binaryReader);
                    }
                    catch
                    {
                        return Size.Empty;
                    }
                }
            }


            private static Size DecodeTiffLE(BinaryReader binaryReader)
            {
                if (binaryReader.ReadByte() != 0)
                    return Size.Empty;

                int idfStart = ReadInt32LE(binaryReader);

                binaryReader.BaseStream.Seek(idfStart, SeekOrigin.Begin);

                int numberOfIDF = ReadInt16LE(binaryReader);

                int width = -1;
                int height = -1;
                for (int i = 0; i < numberOfIDF; i++)
                {
                    short field = ReadInt16LE(binaryReader);

                    switch (field)
                    {
                        // https://www.awaresystems.be/imaging/tiff/tifftags/baseline.html
                        default:
                            binaryReader.ReadBytes(10);
                            break;
                        case 256: // image width
                            binaryReader.ReadBytes(6);
                            width = ReadInt32LE(binaryReader);
                            break;
                        case 257: // image length
                            binaryReader.ReadBytes(6);
                            height = ReadInt32LE(binaryReader);
                            break;
                    }
                    if (width != -1 && height != -1)
                        return new Size(width, height);
                }
                return Size.Empty;
            }

            private static Size DecodeTiffBE(BinaryReader binaryReader)
            {
                int idfStart = ReadInt32BE(binaryReader);

                binaryReader.BaseStream.Seek(idfStart, SeekOrigin.Begin);

                int numberOfIDF = ReadInt16BE(binaryReader);

                int width = -1;
                int height = -1;
                for (int i = 0; i < numberOfIDF; i++)
                {
                    short field = ReadInt16BE(binaryReader);

                    switch (field)
                    {
                        // https://www.awaresystems.be/imaging/tiff/tifftags/baseline.html
                        default:
                            binaryReader.ReadBytes(10);
                            break;
                        case 256: // image width
                            binaryReader.ReadBytes(6);
                            width = ReadInt32BE(binaryReader);
                            break;
                        case 257: // image length
                            binaryReader.ReadBytes(6);
                            height = ReadInt32BE(binaryReader);
                            break;
                    }
                    if (width != -1 && height != -1)
                        return new Size(width, height);
                }
                return Size.Empty;
            }

            private static Size DecodeBitmap(BinaryReader binaryReader)
            {
                binaryReader.ReadBytes(16);
                int width = binaryReader.ReadInt32();
                int height = binaryReader.ReadInt32();
                return new Size(width, height);
            }

            private static Size DecodeGif(BinaryReader binaryReader)
            {
                int width = binaryReader.ReadInt16();
                int height = binaryReader.ReadInt16();
                return new Size(width, height);
            }

            private static Size DecodePng(BinaryReader binaryReader)
            {
                binaryReader.ReadBytes(8);
                int width = ReadInt32BE(binaryReader);
                int height = ReadInt32BE(binaryReader);
                return new Size(width, height);
            }

            private static Size DecodeJfif(BinaryReader binaryReader)
            {
                while (binaryReader.ReadByte() == 0xff)
                {
                    byte marker = binaryReader.ReadByte();
                    short chunkLength = ReadInt16BE(binaryReader);
                    if (marker == 0xc0 || marker == 0xc2) // c2: progressive
                    {
                        binaryReader.ReadByte();
                        int height = ReadInt16BE(binaryReader);
                        int width = ReadInt16BE(binaryReader);
                        return new Size(width, height);
                    }

                    if (chunkLength < 0)
                    {
                        ushort uchunkLength = (ushort)chunkLength;
                        binaryReader.ReadBytes(uchunkLength - 2);
                    }
                    else
                    {
                        binaryReader.ReadBytes(chunkLength - 2);
                    }
                }

                return Size.Empty;
            }

            private static Size DecodeWebP(BinaryReader binaryReader)
            {
                // 'RIFF' already read   

                binaryReader.ReadBytes(4);

                if (ReadInt32LE(binaryReader) != 1346520407)// 1346520407 : 'WEBP'
                    return Size.Empty;

                switch (ReadInt32LE(binaryReader))
                {
                    case 540561494: // 'VP8 ' : lossy
                                    // skip stuff we don't need
                        binaryReader.ReadBytes(7);

                        if (ReadInt24LE(binaryReader) != 2752925) // invalid webp file
                            return Size.Empty;

                        return new Size(ReadInt16LE(binaryReader), ReadInt16LE(binaryReader));

                    case 1278758998:// 'VP8L' : lossless
                                    // skip stuff we don't need
                        binaryReader.ReadBytes(4);

                        if (binaryReader.ReadByte() != 47)// 0x2f : 47 1 byte signature
                            return Size.Empty;

                        byte[] b = binaryReader.ReadBytes(4);

                        return new Size(
                            1 + (((b[1] & 0x3F) << 8) | b[0]),
                            1 + ((b[3] << 10) | (b[2] << 2) | ((b[1] & 0xC0) >> 6)));
                    // if something breaks put in the '& 0xF' but & oxf should do nothing in theory
                    // because inclusive & with 1111 will leave the binary untouched
                    //  1 + (((wh[3] & 0xF) << 10) | (wh[2] << 2) | ((wh[1] & 0xC0) >> 6))

                    case 1480085590:// 'VP8X' : extended
                                    // skip stuff we don't need
                        binaryReader.ReadBytes(8);
                        return new Size(1 + ReadInt24LE(binaryReader), 1 + ReadInt24LE(binaryReader));
                }

                return Size.Empty;
            }

            private static bool StartsWith(byte[] thisBytes, byte[] thatBytes)
            {
                for (int i = 0; i < thatBytes.Length; i += 1)
                    if (thisBytes[i] != thatBytes[i])
                        return false;

                return true;
            }

            #region Endians

            /// <summary>
            /// Reads a 16 bit int from the stream in the Little Endian format.
            /// </summary>
            /// <param name="binaryReader">The binary reader to read</param>
            /// <returns></returns>
            private static short ReadInt16LE(BinaryReader binaryReader)
            {
                byte[] bytes = binaryReader.ReadBytes(2);
                return (short)((bytes[0]) | (bytes[1] << 8));
            }

            /// <summary>
            /// Reads a 24 bit int from the stream in the Little Endian format.
            /// </summary>
            /// <param name="binaryReader">The binary reader to read</param>
            /// <returns></returns>
            private static int ReadInt24LE(BinaryReader binaryReader)
            {
                byte[] bytes = binaryReader.ReadBytes(3);
                return ((bytes[0]) | (bytes[1] << 8) | (bytes[2] << 16));
            }

            /// <summary>
            /// Reads a 32 bit int from the stream in the Little Endian format.
            /// </summary>
            /// <param name="binaryReader">The binary reader to read</param>
            /// <returns></returns>
            private static int ReadInt32LE(BinaryReader binaryReader)
            {
                byte[] bytes = binaryReader.ReadBytes(4);
                return ((bytes[0]) | (bytes[1] << 8) | (bytes[2] << 16) | (bytes[3] << 24));
            }



            /// <summary>
            /// Reads a 32 bit int from the stream in the Big Endian format.
            /// </summary>
            /// <param name="binaryReader">The binary reader to read</param>
            /// <returns></returns>
            private static int ReadInt32BE(BinaryReader binaryReader)
            {
                byte[] bytes = binaryReader.ReadBytes(4);
                return ((bytes[3]) | (bytes[2] << 8) | (bytes[1] << 16) | (bytes[0] << 24));
            }

            /// <summary>
            /// Reads a 24 bit int from the stream in the Big Endian format.
            /// </summary>
            /// <param name="binaryReader">The binary reader to read</param>
            /// <returns></returns>
            private static int ReadInt24BE(BinaryReader binaryReader)
            {
                byte[] bytes = binaryReader.ReadBytes(3);
                return ((bytes[2]) | (bytes[1] << 8) | (bytes[0] << 16));
            }

            /// <summary>
            /// Reads a 16 bit int from the stream in the Big Endian format.
            /// </summary>
            /// <param name="binaryReader">The binary reader to read</param>
            /// <returns></returns>
            private static short ReadInt16BE(BinaryReader binaryReader)
            {
                byte[] bytes = binaryReader.ReadBytes(2);
                return (short)((bytes[1]) | (bytes[0] << 8));
            }

            #endregion
        }
    }
}
