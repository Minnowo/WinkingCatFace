using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{
    public static class InternalSettings
    {
        public static string Folder_Select_Dialog_Title = "Select a folder";
        public static string Image_Select_Dialog_Title = "Select a folder";

        public const string All_Files_File_Dialog = "All Files (*.*)|*.";

        public static string Image_Dialog_Default = "PNG (*.png)|*.png|JPEG (*.jpg, *.jpeg, *.jpe, *.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|GIF (*.gif)|*.gif|BMP (*.bmp)|*.bmp|TIFF (*.tif, *.tiff)|*.tif;*.tiff";

        public static string WebP_File_Dialog = "WebP (*.webp)|*.webp";


        public static List<string> Readable_Image_Formats_Dialog_Options = new List<string>
        { "*.png", "*.jpg", "*.jpeg", "*.jpe", "*.jfif", "*.gif", "*.bmp", "*.tif", "*.tiff" };

        public static List<string> Readable_Image_Formats = new List<string>()
        { "png", "jpg", "jpeg", "jpe", "jfif", "gif", "bmp", "tif", "tiff" };


        #region paths

        public static string Temp_Image_Folder = string.Format("{0}\\{1}", Directory.GetCurrentDirectory(), "tmp");

        #endregion

        #region plugins

        // webp support
        public const string libwebP_x64 = "plugins\\libwebp_x64.dll";
        public const string libwebP_x86 = "plugins\\libwebp_x86.dll";

        public static bool WebP_Plugin_Exists = false;
        #endregion

        #region rate limits

        #endregion

        public static int Image_Counter = -1;


        public static bool Write_Logs = true;

        public static bool Save_Images_To_Disk = true;

        public static bool Garbage_Collect_After_Clip_Destroyed = false;
        public static bool Garbage_Collect_After_All_Clips_Destroyed = true;

        public static ImgFormat Default_Image_Format = ImgFormat.jpg;

        public static bool CPU_Type_x64 = IntPtr.Size == 8;

        public static void EnableWebPIfPossible()
        {
            if (CPU_Type_x64)
            {
                if (File.Exists(libwebP_x64))
                {
                    WebP_Plugin_Exists = true;
                    Readable_Image_Formats_Dialog_Options.Add("*.webp");
                    Readable_Image_Formats.Add("webp");
                }
            }
            else
            {
                if (File.Exists(libwebP_x86))
                {
                    WebP_Plugin_Exists = true;
                    Readable_Image_Formats_Dialog_Options.Add("*.webp");
                    Readable_Image_Formats.Add("webp");
                }
            }
        }
    }

    public static class MainFormSettings
    {
        public static event EventHandler SettingsChangedEvent;
        public static bool hideMainFormOnCapture { get; set; } = true;
        public static bool showInTray { get; set; } = true;
        public static bool minimizeToTray { get; set; } = true;
        public static bool startInTray { get; set; } = false;
        public static bool alwaysOnTop { get; set; } = true;
        public static int waitHideTime { get; set; } = 300;

        public static Tasks onTrayLeftClick { get; set; } = Tasks.RegionCapture;
        public static Tasks onTrayDoubleLeftClick { get; set; } = Tasks.OpenMainForm;
        public static Tasks onTrayMiddleClick { get; set; } = Tasks.NewClipFromClipboard;

        public static void OnSettingsChangedEvent()
        {
            if (SettingsChangedEvent != null)
            {
                SettingsChangedEvent(null, EventArgs.Empty);
            }
        }
    }
}
