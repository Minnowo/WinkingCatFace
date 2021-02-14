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
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>


        [STAThread]
        static void Main()
        {
            NativeMethods.SetProcessDpiAwarenessContext(-3);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DirectoryManager.LoadLocalSettings();

            Logger.Init(DirectoryManager.logPath + "\\" + DateTime.Now.ToString("yyyy MM dd") + ".txt");
            Console.WriteLine("log path: " + DirectoryManager.logPath + "\\" + DateTime.Now.ToString("yyyy M dd") + ".txt");
             
            Logger.WriteLine(";3c starting...");
            Logger.WriteLine("path settings loaded");
            Logger.WriteLine(DirectoryManager.currentDirectory);
            Logger.WriteLine(DirectoryManager.configPath);
            Logger.WriteLine(DirectoryManager.resourcePath);
            Logger.WriteLine(DirectoryManager.screenshotPath);
            Logger.WriteLine(DirectoryManager.logPath);

            if (SettingsLoader.LoadMainFormSettings())
            {
                Logger.WriteLine("MainForm settings loaded successfully");
            }

            if (SettingsLoader.LoadRegionCaptureSettings())
            {
                Logger.WriteLine("RegionCapture settings loaded successfully");
            }

            HotkeyManager.Init();
            Application.Run(new ApplicationForm());
        }
    }
}
