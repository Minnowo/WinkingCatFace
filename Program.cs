using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WinkingCat.HelperLibs;
using WinkingCat.HotkeyLib;
using System.IO;

namespace WinkingCat
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static ApplicationForm mainForm;

        [STAThread]
        static void Main()
        {
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

            if (SettingsLoader.LoadMainFormSettings())
            {
                Logger.WriteLine("MainForm settings loaded successfully");
            }

            if (SettingsLoader.LoadRegionCaptureSettings())
            {
                Logger.WriteLine("RegionCapture settings loaded successfully");
            }


            HotkeyManager.Init();
            mainForm = new ApplicationForm();
            
            Application.Run(mainForm);
        }
    }
}
