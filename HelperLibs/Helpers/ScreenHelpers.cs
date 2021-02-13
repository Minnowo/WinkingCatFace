using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{
    public static class ScreenHelper
    {
        public struct Monitor
        {
            public string name;
            public Rectangle bounds;
            public Rectangle workArea;
            public int hash;
        }
        public static Rectangle GetScreenBounds()
        {
            return SystemInformation.VirtualScreen;
        }

        public static Rectangle GetActiveScreenBounds()
        {
            return Screen.FromPoint(GetCursorPosition()).Bounds;
        }

        public static Rectangle GetPrimaryScreenBounds()
        {
            return Screen.PrimaryScreen.Bounds;
        }

        public static Monitor MonitorFromPoint(Point position)
        {
            Screen scr = Screen.FromPoint(position);
            return new Monitor
            {
                name = scr.DeviceName,
                bounds = scr.Bounds,
                workArea = scr.WorkingArea,
                hash = scr.GetHashCode()
            };
        }
        public static Monitor MonitorFromPoint(int x, int y)
        {
            return MonitorFromPoint(new Point(x, y));
        }

        public static Rectangle GetWindowRectangle(IntPtr handle)
        {
            Rectangle rect = Rectangle.Empty;

            if (NativeMethods.IsDWMEnabled())
            {
                Rectangle tempRect;

                if (NativeMethods.GetExtendedFrameBounds(handle, out tempRect))
                {
                    rect = tempRect;
                }
            }

            if (rect.IsEmpty)
            {
                rect = NativeMethods.GetWindowRect(handle);
            }

            return rect;
        }

        public static bool IsValidCropArea(Rectangle rect)
        {
            if (rect.Width > 0 && rect.Height > 0)
                return true;
            else
                return false;
        }
        public static bool IsValidCropArea(Point a, Point b)
        {
            int width = Math.Abs(a.X - b.X);
            int height = Math.Abs(a.Y - b.Y);
            if (width > 0 || height > 0)
                return true;
            else
                return false;
        }

        public static Rectangle CreateValidCropArea(Point a, Point b)
        {
            int x = Math.Min(a.X, b.X);
            int y = Math.Min(a.Y, b.Y);
            int width = Math.Abs(a.X - b.X);
            int height = Math.Abs(a.Y - b.Y);

            return new Rectangle(new Point(x, y), new Size(width, height));
        }


        public static Point GetCursorPosition()
        {
            POINT point;

            if (NativeMethods.GetCursorPos(out point))
                return (Point)point;

            return Point.Empty;
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
            // have to use + here because the point is assuming the top left of the top left monitor is 0, 0
            // but when taking a picture it find the intersection rectange between GetScreenBounds()
        }

        public static Point ScreenToClient(Point p)
        {
            int screenX = NativeMethods.GetSystemMetrics(SystemMetric.SM_XVIRTUALSCREEN);
            int screenY = NativeMethods.GetSystemMetrics(SystemMetric.SM_YVIRTUALSCREEN);
            return new Point(p.X - screenX, p.Y - screenY);
        }

        public static Rectangle ScreenToClient(Rectangle r)
        {
            return new Rectangle(ScreenToClient(r.Location), r.Size);
        }


        public static Color GetPixelColor()
        {
            return GetPixelColor(GetCursorPosition());
        }

        public static Color GetPixelColor(int x, int y)
        {
            IntPtr hdc = NativeMethods.GetDC(IntPtr.Zero);
            uint pixel = NativeMethods.GetPixel(hdc, x, y);
            NativeMethods.ReleaseDC(IntPtr.Zero, hdc);
            return Color.FromArgb((int)(pixel & 0x000000FF), (int)(pixel & 0x0000FF00) >> 8, (int)(pixel & 0x00FF0000) >> 16);
        }

        public static Color GetPixelColor(Point position)
        {
            return GetPixelColor(position.X, position.Y);
        }

    }
}
