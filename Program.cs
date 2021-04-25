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
        public static string CurrentEXEPath
        {
            get
            {
                string p = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
                return p.Substring(8, p.Length - 8);
            }
        }

        [STAThread]
        static void Main(string[] args)
        {

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

                    //CLIManager cli = new CLIManager(args.CommandLineArgs);
                    //cli.ParseCommands();

                    //CLI.UseCommandLineArgs(cli.Commands);
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
            NativeMethods.SetProcessDpiAwarenessContext(-3);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SettingsManager.LoadPathSettings();
            PathHelper.UpdateRelativePaths();

            Logger.Init(PathHelper.logPath + "\\" + DateTime.Now.ToString("yyyy MM dd") + ".txt");
            Console.WriteLine("log path: " + PathHelper.logPath + "\\" + DateTime.Now.ToString("yyyy M dd") + ".txt");

            Logger.WriteLine(";3c starting...");
            Logger.WriteLine("path settings loaded");
            Logger.WriteLine(PathHelper.currentDirectory);
            Logger.WriteLine(PathHelper.configPath);
            Logger.WriteLine(PathHelper.resourcePath);
            Logger.WriteLine(PathHelper.screenshotDefaultPath);
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

            if (SettingsManager.LoadMiscSettings())
            {
                Logger.WriteLine("Misc settings loaded successfully");
            }

            if (SettingsManager.LoadMainFormStyles())
            {
                Logger.WriteLine("MainForm styles loaded successfully");
            }

            if (SettingsManager.LoadRegionCaptureStyles())
            {
                Logger.WriteLine("RegionCapture styles loaded successfully");
            }

            if (SettingsManager.LoadClipStyles())
            {
                Logger.WriteLine("Clip styles loaded successfully");
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

            MainForm = new ApplicationForm();

            Application.Run(MainForm);


            SettingsManager.SaveAllSettings(HotkeyManager.hotKeys);
        }
    }
}
