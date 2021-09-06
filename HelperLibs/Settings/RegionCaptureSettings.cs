using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WinkingCat.HelperLibs
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RegionCaptureSettings
    {
        [XmlIgnore]
        public InRegionTasks On_Mouse_Middle_Click { get; set; } = InRegionTasks.CaptureLastRegion;
        [XmlIgnore]
        public InRegionTasks On_Mouse_Right_Click { get; set; } = InRegionTasks.RemoveSelectionOrCancel;
        [XmlIgnore]
        public InRegionTasks On_XButton1_Click { get; set; } = InRegionTasks.CaptureActiveMonitor;
        [XmlIgnore]
        public InRegionTasks On_XButton2_Click { get; set; } = InRegionTasks.CaptureFullScreen;
        [XmlIgnore]
        public InRegionTasks On_Escape_Press { get; set; } = InRegionTasks.Cancel;
        [XmlIgnore]
        public InRegionTasks On_Z_Press { get; set; } = InRegionTasks.SwapCenterMagnifier;

        // magnifier
        public bool Draw_Magnifier { get; set; } = true;
        public bool Draw_Crosshair_In_Magnifier { get; set; } = true;
        public bool Draw_Pixel_Grid_In_Magnifier { get; set; } = true;
        public bool Draw_Border_On_Magnifier { get; set; } = true;
        public bool Center_Magnifier_On_Mouse { get; set; } = false;

        public bool Draw_Background_Overlay { get; set; } = true;
        public bool Update_On_Mouse_Move { get; set; } = true;
        public bool Draw_Screen_Wide_Crosshair { get; set; } = true;
        public bool Draw_Info_Text { get; set; } = true;
        public bool Draw_Marching_Ants { get; set; } = true;

        public bool Create_Clip_After_Region_Capture { get; set; } = false;
        public bool Auto_Copy_Image { get; set; } = true;
        public bool Auto_Copy_Color { get; set; } = true;

        public float Magnifier_Zoom_Level { get; set; } = 1; // no more than 6 (Hard Coded Limit)
        public float Magnifier_Zoom_Scale { get; set; } = 0.25f; // less = more scrolling, more = less scrolling
        public int Magnifier_Pixel_Count { get; set; } = 25; // needs to be odd number
        public int Magnifier_Pixel_Size { get; set; } = 6;  // needs to be odd number
        public int Cursor_Info_Offset { get; set; } = 10;

        [XmlIgnore]
        public RegionCaptureMode Mode { get; set; } = RegionCaptureMode.Default;

        // Visual

        [XmlIgnore]
        public Color Background_Overlay_Color { get; set; } = Color.FromArgb(36, 0, 0, 0);

        [XmlIgnore]
        public Color Screen_Wide_Crosshair_Color { get; set; } = Color.FromArgb(249, 0, 187);

        [XmlIgnore]
        public Color Magnifier_Crosshair_Color { get; set; } = Color.FromArgb(128, 173, 216, 230);

        [XmlIgnore]
        public Color Magnifier_Grid_Color { get; set; } = Color.Black;

        [XmlIgnore]
        public Color Magnifier_Border_Color { get; set; } = Color.White;

        [XmlIgnore]
        public Color Info_Text_Back_Color { get; set; } = Color.FromArgb(39, 43, 50);

        [XmlIgnore]
        public Color Info_Text_Border_Color { get; set; } = Color.Black;

        [XmlIgnore]
        public Color Info_Text_Color { get; set; } = Color.FromArgb(255, 255, 255);


        // Serialize Helpers 
        public byte On_Mouse_Middle_Click_As_Byte 
        {
            get { return (byte)On_Mouse_Middle_Click; }
            set { On_Mouse_Middle_Click = (InRegionTasks)value; }
        }

        public byte On_Mouse_Right_Click_As_Byte
        {
            get { return (byte)On_Mouse_Right_Click; }
            set { On_Mouse_Right_Click = (InRegionTasks)value; }
        }

        public byte On_XButton1_Click_As_Byte
        {
            get { return (byte)On_XButton1_Click; }
            set { On_XButton1_Click = (InRegionTasks)value; }
        }

        public byte On_XButton2_Click_As_Byte
        {
            get { return (byte)On_XButton2_Click; }
            set { On_XButton2_Click = (InRegionTasks)value; }
        }

        public byte On_Escape_Press_As_Byte
        {
            get { return (byte)On_Escape_Press; }
            set { On_Escape_Press = (InRegionTasks)value; }
        }

        public byte On_Z_Press_As_Byte
        {
            get { return (byte)On_Z_Press; }
            set { On_Z_Press = (InRegionTasks)value; }
        }


        public byte Mode_As_Byte
        {
            get { return (byte)Mode; }
            set { Mode = (RegionCaptureMode)value; }
        }


        public int Background_Overlay_Color_As_Decimal
        {
            get { return ColorHelper.ColorToDecimal(Background_Overlay_Color, ColorFormat.ARGB); }
            set { Background_Overlay_Color = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int Screen_Wide_Crosshair_Color_As_Decimal
        {
            get { return ColorHelper.ColorToDecimal(Screen_Wide_Crosshair_Color, ColorFormat.ARGB); }
            set { Screen_Wide_Crosshair_Color = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int Magnifier_Crosshair_Color_As_Decimal
        {
            get { return ColorHelper.ColorToDecimal(Magnifier_Crosshair_Color, ColorFormat.ARGB); }
            set { Magnifier_Crosshair_Color = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int Magnifier_Grid_Color_As_Decimal
        {
            get { return ColorHelper.ColorToDecimal(Magnifier_Grid_Color, ColorFormat.ARGB); }
            set { Magnifier_Grid_Color = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int Magnifier_Border_Color_As_Decimal
        {
            get { return ColorHelper.ColorToDecimal(Magnifier_Border_Color, ColorFormat.ARGB); }
            set { Magnifier_Border_Color = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int Info_Text_Back_Color_As_Decimal
        {
            get { return ColorHelper.ColorToDecimal(Info_Text_Back_Color, ColorFormat.ARGB); }
            set { Info_Text_Back_Color = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int Info_Text_Border_Color_As_Decimal
        {
            get { return ColorHelper.ColorToDecimal(Info_Text_Border_Color, ColorFormat.ARGB); }
            set { Info_Text_Border_Color = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int Info_Text_Color_As_Decimal
        {
            get { return ColorHelper.ColorToDecimal(Info_Text_Color, ColorFormat.ARGB); }
            set { Info_Text_Color = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }
    }
}
