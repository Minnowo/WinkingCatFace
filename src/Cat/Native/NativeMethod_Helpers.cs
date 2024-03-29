﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using WinkingCat.HelperLibs;

namespace WinkingCat.Native
{
    public static partial class NativeMethods
    {
        public static bool UseImmersiveDarkMode(IntPtr handle, bool enabled)
        {
            if (Helper.IsWindows10OrGreater(17763))
            {
                DwmWindowAttribute attribute;
                attribute = DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                if (Helper.IsWindows10OrGreater(18985))
                {
                    attribute = DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE;
                }

                int useImmersiveDarkMode = enabled ? 1 : 0;
                return DwmSetWindowAttribute(handle, (int)attribute, ref useImmersiveDarkMode, sizeof(int)) == 0;
            }

            return false;
        }

        public static Process GetProcessByWindowHandle(IntPtr hwnd)
        {
            if (hwnd.ToInt32() > 0)
            {
                try
                {
                    uint processID;
                    GetWindowThreadProcessId(hwnd, out processID);
                    return Process.GetProcessById((int)processID);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return null;
        }

        public static string GetClassName(IntPtr handle)
        {
            if (handle.ToInt32() > 0)
            {
                StringBuilder sb = new StringBuilder(256);

                if (GetClassName(handle, sb, sb.Capacity) > 0)
                {
                    return sb.ToString();
                }
            }

            return null;
        }

        public static string GetWindowText(IntPtr handle)
        {
            if (handle.ToInt32() > 0)
            {
                try
                {
                    int length = GetWindowTextLength(handle);

                    if (length > 0)
                    {
                        StringBuilder sb = new StringBuilder(length + 1);

                        if (GetWindowText(handle, sb, sb.Capacity) > 0)
                        {
                            return sb.ToString();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return null;
        }
        public static Rectangle GetWindowRect(IntPtr handle)
        {
            RECT rect;
            GetWindowRect(handle, out rect);
            return rect;
        }

        public static bool IsWindowCloaked(IntPtr handle)
        {
            if (IsDWMEnabled())
            {
                int cloaked;
                int result = DwmGetWindowAttribute(handle, (int)DwmWindowAttribute.DWMWA_CLOAKED, out cloaked, sizeof(int));
                return result == 0 && cloaked != 0;
            }

            return false;
        }

        public static bool GetExtendedFrameBounds(IntPtr handle, out Rectangle rectangle)
        {
            RECT rect;
            int result = DwmGetWindowAttribute(handle, (int)DwmWindowAttribute.DWMWA_EXTENDED_FRAME_BOUNDS, out rect, Marshal.SizeOf(typeof(RECT)));
            rectangle = rect;
            return result == 0;
        }

        public static bool IsDWMEnabled()
        {
            return Helper.IsWindowsVistaOrGreater() && DwmIsCompositionEnabled();
        }

        public static Icon GetApplicationIcon(IntPtr handle)
        {
            return GetSmallApplicationIcon(handle) ?? GetBigApplicationIcon(handle);
        }

        public static IntPtr GetClassLongPtrSafe(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size > 4)
            {
                return GetClassLongPtr(hWnd, nIndex);
            }
            return new IntPtr(GetClassLong(hWnd, nIndex));
        }

        private static Icon GetSmallApplicationIcon(IntPtr handle)
        {
            IntPtr iconHandle;

            SendMessageTimeout(handle, (int)WindowsMessages.GETICON, NativeConstants.ICON_SMALL2, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 1000, out iconHandle);

            if (iconHandle == IntPtr.Zero)
            {
                SendMessageTimeout(handle, (int)WindowsMessages.GETICON, NativeConstants.ICON_SMALL, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 1000, out iconHandle);

                if (iconHandle == IntPtr.Zero)
                {
                    iconHandle = GetClassLongPtrSafe(handle, NativeConstants.GCL_HICONSM);

                    if (iconHandle == IntPtr.Zero)
                    {
                        SendMessageTimeout(handle, (int)WindowsMessages.QUERYDRAGICON, 0, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 1000, out iconHandle);
                    }
                }
            }

            if (iconHandle != IntPtr.Zero)
            {
                return Icon.FromHandle(iconHandle);
            }

            return null;
        }

        private static Icon GetBigApplicationIcon(IntPtr handle)
        {
            IntPtr iconHandle;

            SendMessageTimeout(handle, (int)WindowsMessages.GETICON, NativeConstants.ICON_BIG, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 1000, out iconHandle);

            if (iconHandle == IntPtr.Zero)
            {
                iconHandle = GetClassLongPtrSafe(handle, NativeConstants.GCL_HICON);
            }

            if (iconHandle != IntPtr.Zero)
            {
                return Icon.FromHandle(iconHandle);
            }

            return null;
        }
    }
}
