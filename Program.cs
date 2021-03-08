using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WinkingCat.HelperLibs;
using System.IO;
using System.Threading;

namespace WinkingCat
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static ApplicationForm mainForm;
        static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}"); // this sets a single instance probably use different uuid each app

        [STAThread]
        static void Main()
        {

            // this checks for an instance, if it finds it return
            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                NativeMethods.PostMessage(
                   (IntPtr)NativeMethods.HWND_BROADCAST,
                   NativeMethods.WM_SHOWME,
                   IntPtr.Zero,
                   IntPtr.Zero);
                return;
            }


            NativeMethods.SetProcessDpiAwarenessContext(-3);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            PathHelper.LoadLocalSettings();

            Logger.Init(PathHelper.logPath + "\\" + DateTime.Now.ToString("yyyy MM dd") + ".txt");
            Console.WriteLine("log path: " + PathHelper.logPath + "\\" + DateTime.Now.ToString("yyyy M dd") + ".txt");

            Logger.WriteLine(";3c starting...");
            Logger.WriteLine("path settings loaded");
            Logger.WriteLine(PathHelper.currentDirectory);
            Logger.WriteLine(PathHelper.configPath);
            Logger.WriteLine(PathHelper.resourcePath);
            Logger.WriteLine(PathHelper.screenshotPath);
            Logger.WriteLine(PathHelper.logPath);

            if (SettingsManager.LoadMainFormSettings())
            {
                Logger.WriteLine("MainForm settings loaded successfully");
            }

            if (SettingsManager.LoadRegionCaptureSettings())
            {
                Logger.WriteLine("RegionCapture settings loaded successfully");
            }

            if (SettingsManager.LoadClipboardSettings())
            {
                Logger.WriteLine("Clipboard settings loaded successfully");
            }            

            HotkeyManager.Init();

            if (SettingsManager.LoadHotkeySettings() != null)
            {
                HotkeyManager.UpdateHotkeys(SettingsManager.LoadHotkeySettings(), true);
                Logger.WriteLine("Hotkeys loaded successfully");
            }
            else
            {
                Logger.WriteLine("Hotkeys not loaded using default");
                HotkeyManager.UpdateHotkeys(HotkeyManager.GetDefaultHotkeyList(), true);
            }

            mainForm = new ApplicationForm();
            
            Application.Run(mainForm);
            mutex.ReleaseMutex();
        }
    }
}
