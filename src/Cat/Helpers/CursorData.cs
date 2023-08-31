using System;
using System.Drawing;
using System.Runtime.InteropServices;
using WinkingCat.Native;

namespace WinkingCat.HelperLibs
{
    public class CursorData
    {
        public IntPtr Handle { get; private set; }
        public Point Position { get; private set; }
        public bool IsVisible { get; private set; }

        public CursorData()
        {
            UpdateCursorData();
        }

        public void UpdateCursorData()
        {
            Handle = IntPtr.Zero;
            Position = Point.Empty;
            IsVisible = false;

            CursorInfo cursorInfo = new CursorInfo();
            cursorInfo.cbSize = Marshal.SizeOf(cursorInfo);

            if (NativeMethods.GetCursorInfo(out cursorInfo))
            {
                Handle = cursorInfo.hCursor;
                Position = cursorInfo.ptScreenPos;
                IsVisible = cursorInfo.flags == NativeConstants.CURSOR_SHOWING;

                if (IsVisible)
                {
                    IntPtr iconHandle = NativeMethods.CopyIcon(Handle);

                    if (iconHandle != IntPtr.Zero)
                    {
                        IconInfo iconInfo;

                        if (NativeMethods.GetIconInfo(iconHandle, out iconInfo))
                        {
                            Position = new Point(Position.X - iconInfo.xHotspot, Position.Y - iconInfo.yHotspot);

                            if (iconInfo.hbmMask != IntPtr.Zero)
                            {
                                NativeMethods.DeleteObject(iconInfo.hbmMask);
                            }

                            if (iconInfo.hbmColor != IntPtr.Zero)
                            {
                                NativeMethods.DeleteObject(iconInfo.hbmColor);
                            }
                        }

                        NativeMethods.DestroyIcon(iconHandle);
                    }
                }
            }
        }

        public void DrawCursor(Image img)
        {
            DrawCursor(img, Point.Empty);
        }

        public void DrawCursor(Image img, Point offset)
        {
            if (IsVisible)
            {
                Point drawPosition = new Point(Position.X - offset.X, Position.Y - offset.Y);
                drawPosition = ScreenHelper.ScreenToClient(drawPosition);

                using (Graphics g = Graphics.FromImage(img))
                using (Icon icon = Icon.FromHandle(Handle))
                {
                    g.DrawIcon(icon, drawPosition.X, drawPosition.Y);
                }
            }
        }

        public void DrawCursor(IntPtr hdcDest)
        {
            DrawCursor(hdcDest, Point.Empty);
        }

        public void DrawCursor(IntPtr hdcDest, Point offset)
        {
            if (IsVisible)
            {
                Point drawPosition = new Point(Position.X - offset.X, Position.Y - offset.Y);
                drawPosition = ScreenHelper.ScreenToClient(drawPosition);

                NativeMethods.DrawIconEx(hdcDest, drawPosition.X, drawPosition.Y, Handle, 0, 0, 0, IntPtr.Zero, NativeConstants.DI_NORMAL);
            }
        }
    }
}
