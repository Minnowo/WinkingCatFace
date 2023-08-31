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
using System.Drawing;
using System.IO;
using WinkingCat.HelperLibs;

namespace WinkingCat.Settings
{
    public static class InternalSettings
    {
        public const string DRIVES_FOLDERNAME = "|Drives|";
        public const string Plugin_IO_Path = "plugins\\";
        public const string Default_Screenshot_IO_Path = "screenshots\\";
        public const string Settings_IO_Path = "settings\\";

        public const string Clip_Settings_IO_Path = "settings\\clip.conf";
        public const string Main_Form_Settings_IO_Path = "settings\\main.conf";
        public const string Region_Capture_Settings_IO_Path = "settings\\capture.conf";
        public const string Misc_Settings_IO_Path = "settings\\misc.conf";
        public const string Hotkey_Settings_IO_Path = "settings\\hotkeys.conf";
        public const string List_View_Items_IO_Path = "settings\\items.txt";

        public const string Folder_Select_Dialog_Title = "Select a folder";
        public const string Image_Select_Dialog_Title = "Select an image";
        public const string Save_File_Dialog_Title = "Save Item";
        public const string Move_File_Dialog_Title = "Move Item";

        public const string All_Files_File_Dialog = "All Files (*.*)|*.*";

        public const string PNG_File_Dialog = "PNG (*.png)|*.png";
        public const string BMP_File_Dialog = "BMP (*.bmp)|*.bmp";
        public const string GIF_File_Dialog = "GIF (*.gif)|*.gif";
        public const string WEBP_File_Dialog = "WebP (*.webp)|*.webp";
        public const string TIFF_File_Dialog = "TIFF (*.tif, *.tiff)|*.tif;*.tiff";
        public const string JPEG_File_Dialog = "JPEG (*.jpg, *.jpeg, *.jpe, *.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif";
        public const string WRM_File_Dialog = "WRM (*.wrm, *.dwrm)|*.wrm;*.dwrm";

        public static string Image_Dialog_Filters = string.Join("|",
            new string[]
            {
                PNG_File_Dialog,
                JPEG_File_Dialog,
                BMP_File_Dialog,
                TIFF_File_Dialog,
                GIF_File_Dialog,
                WRM_File_Dialog
            });


        public static List<string> Readable_Image_Formats_Dialog_Options = new List<string>
        { "*.png", "*.jpg", "*.jpeg", "*.jpe", "*.jfif", "*.gif", "*.bmp", "*.tif", "*.tiff", "*.wrm", "*.dwrm" };

        public static List<string> Readable_Image_Formats = new List<string>()
        { "png", "jpg", "jpeg", "jpe", "jfif", "gif", "bmp", "tif", "tiff", "wrm", "dwrm" };

        public static string All_Image_Files_File_Dialog = string.Format(
            "Graphic Types ({0})|{1}",
            string.Join(", ", Readable_Image_Formats_Dialog_Options),
            string.Join(";", Readable_Image_Formats_Dialog_Options));

        #region paths

        public static string Temp_Image_Folder = string.Format("{0}\\{1}", Directory.GetCurrentDirectory(), "tmp");

        #endregion


        public static bool WebP_Plugin_Exists = false;


        #region rate limits

        #endregion

        public static Size Max_Clip_Size
        {
            get
            {
                //https://social.msdn.microsoft.com/Forums/windows/en-US/a1843fa0-64f3-41f7-a117-a28b8d83dd12/windows-form-larger-than-screen-size?forum=winformsdesigner
                Rectangle r = ScreenHelper.GetScreenBounds();
                return new Size(r.Width + 12, r.Height + 12);
            }
        }

        public static ColorFormat Clipboard_Color_Format = ColorFormat.RGB;

        public static long Jpeg_Quality = 75;

        public static bool Save_WORM_As_DWORM = true;

        public static int Image_Counter = -1;

        public static bool Write_Logs = true;

        public static bool Save_Images_To_Disk = true;

        public static bool Garbage_Collect_After_Image_Display_Unload = true;
        public static bool Garbage_Collect_After_Clip_Destroyed = false;
        public static bool Garbage_Collect_After_All_Clips_Destroyed = true;
        public static bool Garbage_Collect_After_Gif_Encode = true;

        public static bool Use_Alternate_Copy_Method = true;
        public static bool Replace_Transparency_On_Copy = false;
        public static Color Fill_Transparency_On_Copy_Color = Color.White;

        public static ImgFormat Default_Image_Format
        {
            get { return _Default_Image_Format; }
            set
            {
                if (value == ImgFormat.webp && !WebP_Plugin_Exists)
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

        public static Size TSMI_Generated_Icon_Size = new Size(16, 16);

        public static void UpdateDialogFilters()
        {
            if (WebP_Plugin_Exists)
            {
                if (!Readable_Image_Formats.Contains("webp"))
                {
                    Readable_Image_Formats_Dialog_Options.Add("*.webp");
                    Readable_Image_Formats.Add("webp");
                }
            }

            All_Image_Files_File_Dialog = string.Format(
                "Graphic Types ({0})|{1}",
                string.Join(", ", Readable_Image_Formats_Dialog_Options),
                string.Join(";", Readable_Image_Formats_Dialog_Options));

            Image_Dialog_Filters = string.Join("|",
                new string[]
                {
                    PNG_File_Dialog,
                    JPEG_File_Dialog,
                    BMP_File_Dialog,
                    TIFF_File_Dialog,
                    GIF_File_Dialog,
                    WRM_File_Dialog
                });

            if (WebP_Plugin_Exists)
            {
                Image_Dialog_Filters += "|" + WEBP_File_Dialog;
            }
        }

        public static bool EnableWebPIfPossible()
        {
            if (CPU_Type_x64)
            {
                if (File.Exists(Path.Combine(PathHelper.CurrentDirectory, Webp.libwebP_x64)))
                {
                    WebP_Plugin_Exists = true;
                    UpdateDialogFilters();
                    return true;
                }
            }
            else
            {
                if (File.Exists(Path.Combine(PathHelper.CurrentDirectory, Webp.libwebP_x86)))
                {
                    WebP_Plugin_Exists = true;
                    UpdateDialogFilters();
                    return true;
                }
            }
            return false;
        }
    }
}
