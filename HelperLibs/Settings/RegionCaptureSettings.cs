using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RegionCaptureSettings
    {
        public static InRegionTasks On_Mouse_Middle_Click { get; set; } = InRegionTasks.CaptureLastRegion;
        public static InRegionTasks On_Mouse_Right_Click { get; set; } = InRegionTasks.RemoveSelectionOrCancel;
        public static InRegionTasks On_XButton1_Click { get; set; } = InRegionTasks.CaptureActiveMonitor;
        public static InRegionTasks On_XButton2_Click { get; set; } = InRegionTasks.CaptureFullScreen;
        public static InRegionTasks On_Escape_Press { get; set; } = InRegionTasks.Cancel;
        public static InRegionTasks On_Z_Press { get; set; } = InRegionTasks.SwapCenterMagnifier;

        // magnifier
        public static bool Draw_Magnifier { get; set; } = true;
        public static bool Draw_Crosshair_In_Magnifier { get; set; } = true;
        public static bool Draw_Pixel_Grid_In_Magnifier { get; set; } = true;
        public static bool Draw_Border_On_Magnifier { get; set; } = true;
        public static bool Center_Magnifier_On_Mouse { get; set; } = false;

        public static bool Draw_Background_Overlay { get; set; } = true;
        public static bool Update_On_Mouse_Move { get; set; } = true;
        public static bool Draw_Screen_Wide_Crosshair { get; set; } = true;
        public static bool Draw_Info_Text { get; set; } = true;
        public static bool Draw_Marching_Ants { get; set; } = true;

        public static bool Create_Clip_After_Region_Capture { get; set; } = false;
        public static bool Auto_Copy_Image { get; set; } = true;
        public static bool Auto_Copy_Color { get; set; } = true;

        public static float Magnifier_Zoom_Level { get; set; } = 1; // no more than 6 (Hard Coded Limit)
        public static float Magnifier_Zoom_Scale { get; set; } = 0.25f; // less = more scrolling, more = less scrolling
        public static int Magnifier_Pixel_Count { get; set; } = 25; // needs to be odd number
        public static int Magnifier_Pixel_Size { get; set; } = 6;  // needs to be odd number
        public static int Cursor_Info_Offset { get; set; } = 10;

        public static RegionCaptureMode Mode { get; set; } = RegionCaptureMode.Default;
    }
}
