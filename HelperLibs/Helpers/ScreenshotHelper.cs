using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using WinkingCat.HelperLibs;

namespace WinkingCat.HelperLibs
{
    /// <summary>
    /// A class used for capturing screenshots.
    /// </summary>
    public static class ScreenshotHelper
    {
        /// <summary>
        /// Should screenshots capture the cursor.
        /// </summary>
        public static bool CaptureCursor = false;

        /// <summary>
        /// Should Native methods be used for screen capture.
        /// </summary>
        public static bool UseNativeCapture = true;


        /// <summary>
        /// Captures the given rectangle on the screen.
        /// </summary>
        /// <param name="rect">The <see cref="Rectangle"/> to capture.</param>
        /// <returns>A <see cref="Bitmap"/> of the screen region.</returns>
        public static Bitmap CaptureRectangle(Rectangle rect)
        {
            Rectangle bounds = ScreenHelper.GetScreenBounds();
            rect = Rectangle.Intersect(bounds, rect);

            if(UseNativeCapture)
                return CaptureRectangleNative(IntPtr.Zero, rect, CaptureCursor); 
            
            return ManagedRectAsImage(rect, CaptureCursor);
        }

        /// <summary>
        /// Captures the given rectangle on the screen.
        /// </summary>
        /// <param name="x">The top left X of the capture.</param>
        /// <param name="y">The top left Y of the capture.</param>
        /// <param name="width">The width of the capture.</param>
        /// <param name="height">The height of the capture.</param>
        /// <returns>A <see cref="Bitmap"/> of the screen region.</returns>
        public static Bitmap CaptureRectangle(int x, int y, int width, int height)
        {
            return CaptureRectangle(new Rectangle(x, y, width, height));
        }


        /// <summary>
        /// Captures the entire screen.
        /// </summary>
        /// <returns>A <see cref="Bitmap"/> of the screen.</returns>
        public static Bitmap CaptureFullscreen()
        {
            Rectangle bounds = ScreenHelper.GetScreenBounds();

            return CaptureRectangle(bounds);
        }


        /// <summary>
        /// Captures the active monitor.
        /// </summary>
        /// <returns>A <see cref="Bitmap"/> of the active monitor.</returns>
        public static Bitmap CaptureActiveMonitor()
        {
            Rectangle bounds = ScreenHelper.GetActiveScreenBounds();

            return CaptureRectangle(bounds);
        }


        private static Bitmap CaptureRectangleNative(IntPtr handle, Rectangle rect, bool captureCursor = false)
        {
            if (rect.Width == 0 || rect.Height == 0)
                return null;

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

        private static Bitmap ManagedRectAsImage(Rectangle rect, bool captureCursor = false)
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

                if (captureCursor)
                {
                    try
                    {
                        CursorData cursorData = new CursorData();
                        cursorData.DrawCursor(bmp, rect.Location);
                    }
                    catch
                    {
                    }
                }
            }

            return bmp;
        }
    }
}
