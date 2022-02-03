using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{
    /// <summary>
    /// A class used to get info about the screen and monitors.
    /// </summary>
    public static class ScreenHelper
    {
        /// <summary>
        /// Gets the virtual screen size.
        /// </summary>
        /// <returns>The virtual screen size.</returns>
        public static Rectangle GetScreenBounds()
        {
            return SystemInformation.VirtualScreen;
        }

        /// <summary>
        /// Gets the bounds of the active screen.
        /// </summary>
        /// <returns>The active screen bounds.</returns>
        public static Rectangle GetActiveScreenBounds()
        {
            return Screen.FromPoint(GetCursorPosition()).Bounds;
        }

        /// <summary>
        /// Gets the bounds of the primary screen.
        /// </summary>
        /// <returns>The bounds of the primary screen.</returns>
        public static Rectangle GetPrimaryScreenBounds()
        {
            return Screen.PrimaryScreen.Bounds;
        }

        /// <summary>
        /// Gets the bounds of a given window.
        /// </summary>
        /// <param name="handle">The handle of the window.</param>
        /// <returns>The bounds of the given window.</returns>
        public static Rectangle GetWindowRectangle(IntPtr handle)
        {
            if (NativeMethods.IsDWMEnabled())
                if (NativeMethods.GetExtendedFrameBounds(handle, out Rectangle tempRect))
                   return tempRect;
               
            return NativeMethods.GetWindowRect(handle);
        }

        /// <summary>
        /// Gets the cursor position.
        /// </summary>
        /// <returns>A <see cref="Point"/> with the cursor x y.</returns>
        public static Point GetCursorPosition()
        {
            POINT point;

            if (NativeMethods.GetCursorPos(out point))
                return (Point)point;

            return Cursor.Position;
        }


        public static Rectangle GetScreenBounds0Based()
        {
            return ScreenToClient(GetScreenBounds());
        }

        public static Rectangle GetActiveScreenBounds0Based()
        {
            return ScreenToClient(GetActiveScreenBounds());
        }

        public static Rectangle GetPrimaryScreenBounds0Based()
        {
            return ScreenToClient(GetPrimaryScreenBounds());
        }


        public static Rectangle GetRectangle0Based(Rectangle rect)
        {
            int screenX = NativeMethods.GetSystemMetrics(SystemMetric.SM_XVIRTUALSCREEN);
            int screenY = NativeMethods.GetSystemMetrics(SystemMetric.SM_YVIRTUALSCREEN);
            return new Rectangle(new Point(rect.X + screenX, rect.Y + screenY), rect.Size);
        }

        /// <summary>
        /// Converts the screen coordinates of a specified point on the screen to client-area coordinates.
        /// </summary>
        /// <param name="p">The <see cref="Point"/>.</param>
        /// <returns>The shifted <see cref="Point"/>.</returns>
        public static Point ScreenToClient(Point p)
        {
            int screenX = NativeMethods.GetSystemMetrics(SystemMetric.SM_XVIRTUALSCREEN);
            int screenY = NativeMethods.GetSystemMetrics(SystemMetric.SM_YVIRTUALSCREEN);
            return new Point(p.X - screenX, p.Y - screenY);
        }

        /// <summary>
        /// Converts the screen coordinates of the rect x y on the screen to client-area coordinates.
        /// </summary>
        /// <param name="rect">The <see cref="Rectangle"/>.</param>
        /// <returns>The shifted <see cref="Rectangle"/>.</returns>
        public static Rectangle ScreenToClient(Rectangle rect)
        {
            return new Rectangle(ScreenToClient(rect.Location), rect.Size);
        }

        /// <summary>
        /// Gets the pixel color at the cursor position.
        /// </summary>
        /// <returns>The <see cref="Color"/> at the cursor.</returns>
        public static Color GetPixelColor()
        {
            return GetPixelColor(GetCursorPosition());
        }

        /// <summary>
        /// Gets the pixel color at the given point.
        /// </summary>
        /// <param name="position">The <see cref="Point"/>.</param>
        /// <returns>The <see cref="Color"/> at the given point.</returns>
        public static Color GetPixelColor(Point position)
        {
            return GetPixelColor(position.X, position.Y);
        }

        /// <summary>
        /// Gets the pixel color at the given x y position.
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        /// <returns>A <see cref="Color"/> at the given x y position.</returns>
        public static Color GetPixelColor(int x, int y)
        {
            IntPtr hdc = NativeMethods.GetDC(IntPtr.Zero);
            uint pixel = NativeMethods.GetPixel(hdc, x, y);
            NativeMethods.ReleaseDC(IntPtr.Zero, hdc);
            return Color.FromArgb((int)(pixel & 0x000000FF), (int)(pixel & 0x0000FF00) >> 8, (int)(pixel & 0x00FF0000) >> 16);
        }
    }
}
