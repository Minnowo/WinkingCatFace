using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WinkingCat.HelperLibs;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace WinkingCat
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static ApplicationForm MainForm;


        [STAThread]
        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);
            using (InstanceManager instanceManager = new InstanceManager(true, args, SingleInstanceCallback))
            {
                Run();
            }
        }

        private static void SingleInstanceCallback(object sender, InstanceCallbackEventArgs args)
        {
            if (WaitFormLoad(3000))
            {
                Action d = () =>
                {
                    if (args.CommandLineArgs == null || args.CommandLineArgs.Length < 1)
                    {
                        if (MainForm.niTrayIcon != null && MainForm.niTrayIcon.Visible)
                        {
                            // Workaround for Windows startup tray icon bug
                            MainForm.niTrayIcon.Visible = false;
                            MainForm.niTrayIcon.Visible = true;
                        }

                        MainForm.ForceActivate();
                    }
                    else if (MainForm.Visible)
                    {
                        MainForm.ForceActivate();
                    }
                };

                MainForm.InvokeSafe(d);
            }
        }

        private static bool WaitFormLoad(int wait)
        {
            Stopwatch timer = Stopwatch.StartNew();

            while (timer.ElapsedMilliseconds < wait)
            {
                if (MainForm != null && MainForm.IsReady) return true;

                Thread.Sleep(10);
            }

            return false;
        }

        private static void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SettingsManager.LoadMiscSettings();

            PathHelper.CreateAllPaths();

            InternalSettings.EnableWebPIfPossible();

            SettingsManager.LoadClipSettings();
            SettingsManager.LoadMainFormSettings();
            SettingsManager.LoadRegionCaptureSettings();

            HotkeyManager.Init();

            List<Hotkey> hk;
            if ((hk = SettingsManager.LoadHotkeySettings()) != null)
            {
                HotkeyManager.UpdateHotkeys(hk, false);
            }
            else
            {
                HotkeyManager.UpdateHotkeys(HotkeyManager.GetDefaultHotkeyList(), false);
            }

            MainForm = new ApplicationForm();

            Application.Run(MainForm);


            SettingsManager.SaveAllSettings(HotkeyManager.hotKeys);
        }
    }
}
