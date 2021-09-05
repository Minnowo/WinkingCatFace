using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using WinkingCat.HelperLibs;

namespace WinkingCat.ScreenCaptureLib
{
    public static class ScreenShotManager
    {
        public static bool captureCursor { get; set; } = false;
        public static Bitmap CaptureRectangle(Rectangle rect)
        {

            Rectangle bounds = ScreenHelper.GetScreenBounds();
            rect = Rectangle.Intersect(bounds, rect);

            //return ManagedRectAsImage(rect);
            return CaptureRectangleNative(IntPtr.Zero, rect, captureCursor); // (IntPtr)0x0000000000010010
            
            
        }
        public static Bitmap CaptureRectangle(int x, int y, int x1, int y1)
        {
            return CaptureRectangle(new Rectangle(x, y, x1, y1));
        }


        public static Bitmap CaptureFullscreen()
        {
            Rectangle bounds = ScreenHelper.GetScreenBounds();

            return CaptureRectangle(bounds);
        }

        public static Bitmap CaptureActiveMonitor()
        {
            Rectangle bounds = ScreenHelper.GetActiveScreenBounds();

            return CaptureRectangle(bounds);
        }


        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.PixelOffsetMode = PixelOffsetMode.Half;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private static Bitmap CaptureRectangleNative(IntPtr handle, Rectangle rect, bool captureCursor = false)
        {
            if (rect.Width == 0 || rect.Height == 0)
            {
                return null;
            }

            IntPtr hdcSrc = NativeMethods.GetWindowDC(handle);
            IntPtr hdcDest = NativeMethods.CreateCompatibleDC(hdcSrc);
            IntPtr hBitmap = NativeMethods.CreateCompatibleBitmap(hdcSrc, rect.Width, rect.Height);
            IntPtr hOld = NativeMethods.SelectObject(hdcDest, hBitmap);
            NativeMethods.BitBlt(hdcDest, 0, 0, rect.Width, rect.Height, hdcSrc, rect.X, rect.Y, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);

            if (captureCursor)
            {
                try
                {
                    CursorData cursorData = new CursorData();
                    cursorData.DrawCursor(hdcDest, rect.Location);
                }
                catch
                {
                }
            }

            NativeMethods.SelectObject(hdcDest, hOld);
            NativeMethods.DeleteDC(hdcDest);
            NativeMethods.ReleaseDC(handle, hdcSrc);
            Bitmap bmp = Image.FromHbitmap(hBitmap);
            NativeMethods.DeleteObject(hBitmap);

            return bmp;
        }

        public static Bitmap ManagedRectAsImage(Rectangle rect)
        {
            if (rect.Width == 0 || rect.Height == 0)
            {
                return null;
            }

            Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format24bppRgb);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Managed can't use SourceCopy | CaptureBlt because of .NET bug
                g.CopyFromScreen(rect.Location, Point.Empty, rect.Size, CopyPixelOperation.SourceCopy);
            }

            return bmp;
        }
    }
}
