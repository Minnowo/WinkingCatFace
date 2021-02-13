
using System;
using System.Diagnostics;
using System.Drawing;

namespace WinkingCat.HelperLibs
{
    public class WindowInfo
    {
        public IntPtr Handle { get; }

        public bool IsHandleCreated => Handle != IntPtr.Zero;

        public string Text => NativeMethods.GetWindowText(Handle);

        public string ClassName => NativeMethods.GetClassName(Handle);

        public Process Process => NativeMethods.GetProcessByWindowHandle(Handle);

        public string ProcessName
        {
            get
            {
                using (Process process = Process)
                {
                    return process?.ProcessName;
                }
            }
        }

        public Rectangle Rectangle => ScreenHelper.GetWindowRectangle(Handle);

        public Icon Icon => NativeMethods.GetApplicationIcon(Handle);

        public bool IsMinimized => NativeMethods.IsIconic(Handle);

        public bool IsVisible => NativeMethods.IsWindowVisible(Handle) && !IsCloaked;

        public bool IsCloaked => NativeMethods.IsWindowCloaked(Handle);

        public WindowInfo(IntPtr handle)
        {
            Handle = handle;
        }

        public void Activate()
        {
            if (IsHandleCreated)
            {
                NativeMethods.SetForegroundWindow(Handle);
            }
        }

        public void Restore()
        {
            if (IsHandleCreated)
            {
                NativeMethods.ShowWindow(Handle, (int)WindowShowStyle.Restore);
            }
        }

        public override string ToString()
        {
            return Text;
        }
    }
}