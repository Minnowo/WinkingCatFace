#region License Information (GPL v3)

/*
    Nyan.Imaging - A library with a bunch of helpers and other stuff
    Copyright (c) 2007-2020 ShareX Team

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

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
        public static string Image_Select_Dialog_Title = "Select an image";

        public const string All_Files_File_Dialog = "All Files (*.*)|*.";

        public static string Png_File_Dialog =  "PNG (*.png)|*.png";
        public static string Bmp_File_Dialog =  "BMP (*.bmp)|*.bmp";
        public static string Gif_File_Dialog =  "GIF (*.gif)|*.gif";
        public static string WebP_File_Dialog = "WebP (*.webp)|*.webp";
        public static string Tiff_File_Dialog = "TIFF (*.tif, *.tiff)|*.tif;*.tiff";
        public static string Jpeg_File_Dialog = "JPEG (*.jpg, *.jpeg, *.jpe, *.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif";

        public static string Image_Dialog_Filter = string.Join("|",
            new string[]
            {
                Png_File_Dialog,
                Jpeg_File_Dialog,
                Bmp_File_Dialog,
                Tiff_File_Dialog,
                Gif_File_Dialog
            });

        public static List<string> Readable_Image_Formats_Dialog_Options = new List<string>
        { "*.png", "*.jpg", "*.jpeg", "*.jpe", "*.jfif", "*.gif", "*.bmp", "*.tif", "*.tiff" };

        public static List<string> Readable_Image_Formats = new List<string>()
        { "png", "jpg", "jpeg", "jpe", "jfif", "gif", "bmp", "tif", "tiff" };


        #region paths

        public static string Temp_Image_Folder = string.Format("{0}\\{1}", Directory.GetCurrentDirectory(), "tmp");

        #endregion

        #region plugins

        // webp support
        public const string libwebP_x64 = "AppConfig\\plugins\\libwebp_x64.dll";
        public const string libwebP_x86 = "AppConfig\\plugins\\libwebp_x86.dll";

        public static bool WebP_Plugin_Exists = false;
        #endregion

        #region rate limits

        #endregion

        public static int Image_Counter = -1;

        public static bool Write_Logs = true;

        public static bool Save_Images_To_Disk = true;

        public static bool Garbage_Collect_After_Clip_Destroyed = false;
        public static bool Garbage_Collect_After_All_Clips_Destroyed = true;

        public static ImgFormat Default_Image_Format 
        {
            get { return _Default_Image_Format; }
            set 
            { 
                if(value == ImgFormat.webp && !WebP_Plugin_Exists)
                {
                    _Default_Image_Format = ImgFormat.jpg;
                    return;
                }
                _Default_Image_Format = value; 
            } 
        }
        private static ImgFormat _Default_Image_Format = ImgFormat.jpg;

        public static WebPQuality WebpQuality_Default = new WebPQuality(WebpEncodingFormat.EncodeLossy, 74, 6);

        public static bool CPU_Type_x64 = IntPtr.Size == 8;

        public static bool EnableWebPIfPossible()
        {
            if (CPU_Type_x64)
            {
                if (File.Exists(libwebP_x64))
                {
                    WebP_Plugin_Exists = true;
                    Readable_Image_Formats_Dialog_Options.Add("*.webp");
                    Readable_Image_Formats.Add("webp");
                    Image_Dialog_Filter += "|" + WebP_File_Dialog;
                    return true;
                }
            }
            else
            {
                if (File.Exists(libwebP_x86))
                {
                    WebP_Plugin_Exists = true;
                    Readable_Image_Formats_Dialog_Options.Add("*.webp");
                    Readable_Image_Formats.Add("webp");
                    Image_Dialog_Filter += "|" + WebP_File_Dialog;
                    return true;
                }
            }
            return false;
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
