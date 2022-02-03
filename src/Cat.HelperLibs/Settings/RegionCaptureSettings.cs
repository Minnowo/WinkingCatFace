using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Drawing.Design;

namespace WinkingCat.HelperLibs
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RegionCaptureSettings
    {
        [Browsable(false)]
        [XmlIgnore]
        public InRegionTasks On_Mouse_Middle_Click { get; set; } = InRegionTasks.CaptureLastRegion;
        [Browsable(false)]
        [XmlIgnore]
        public InRegionTasks On_Mouse_Right_Click { get; set; } = InRegionTasks.RemoveSelectionOrCancel;
        [Browsable(false)]
        [XmlIgnore]
        public InRegionTasks On_XButton1_Click { get; set; } = InRegionTasks.CaptureActiveMonitor;
        [Browsable(false)]
        [XmlIgnore]
        public InRegionTasks On_XButton2_Click { get; set; } = InRegionTasks.CaptureFullScreen;
        [Browsable(false)]
        [XmlIgnore]
        public InRegionTasks On_Escape_Press { get; set; } = InRegionTasks.Cancel;
        [Browsable(false)]
        [XmlIgnore]
        public InRegionTasks On_Z_Press { get; set; } = InRegionTasks.SwapCenterMagnifier;

        // magnifier
        [Browsable(false)]
        public bool Capture_Cursor
        { 
            get { return ScreenshotHelper.CaptureCursor; } 
            set { ScreenshotHelper.CaptureCursor = value; } 
        }
        [Browsable(false)]
        public bool Draw_Magnifier { get; set; } = true;
        [Browsable(false)]
        public bool Draw_Crosshair_In_Magnifier { get; set; } = true;
        [Browsable(false)]
        public bool Draw_Pixel_Grid_In_Magnifier { get; set; } = true;
        [Browsable(false)]
        public bool Draw_Border_On_Magnifier { get; set; } = true;
        [Browsable(false)]
        public bool Center_Magnifier_On_Mouse { get; set; } = false;

        [Browsable(false)]
        public bool Draw_Background_Overlay { get; set; } = true;
        [Browsable(false)]
        public bool Update_On_Mouse_Move { get; set; } = true;
        [Browsable(false)]
        public bool Draw_Screen_Wide_Crosshair { get; set; } = true;
        [Browsable(false)]
        public bool Draw_Info_Text { get; set; } = true;
        [Browsable(false)]
        public bool Draw_Marching_Ants { get; set; } = true;

        [Browsable(false)]
        public bool Create_Clip_After_Region_Capture { get; set; } = false;
        [Browsable(false)]
        public bool Auto_Copy_Image { get; set; } = true;
        [Browsable(false)]
        public bool Auto_Copy_Color { get; set; } = true;

        [Browsable(false)]
        public float Magnifier_Zoom_Level { get; set; } = 1; // no more than 6 (Hard Coded Limit)
        [Browsable(false)]
        public float Magnifier_Zoom_Scale { get; set; } = 0.25f; // less = more scrolling, more = less scrolling
        [Browsable(false)]
        public int Magnifier_Pixel_Count { get; set; } = 25; // needs to be odd number
        [Browsable(false)]
        public int Magnifier_Pixel_Size { get; set; } = 6;  // needs to be odd number
        [Browsable(false)]
        public int Cursor_Info_Offset { get; set; } = 10;

        [XmlIgnore]
        [Browsable(false)]
        public RegionCaptureMode Mode { get; set; } = RegionCaptureMode.Default;

        // Visual

        [XmlIgnore]
        [DisplayName("Background Overlay Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color Background_Overlay_Color { get; set; } = Color.FromArgb(36, 0, 0, 0);

        [XmlIgnore]
        [DisplayName("Screenwide Crosshair Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color Screen_Wide_Crosshair_Color { get; set; } = Color.FromArgb(249, 0, 187);

        [XmlIgnore]
        [DisplayName("Magnifier Crosshair Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color Magnifier_Crosshair_Color { get; set; } = Color.FromArgb(128, 173, 216, 230);

        [XmlIgnore]
        [DisplayName("Magnifier Grid Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color Magnifier_Grid_Color { get; set; } = Color.Black;

        [XmlIgnore]
        [DisplayName("Magnifier Border Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color Magnifier_Border_Color { get; set; } = Color.White;

        [XmlIgnore]
        [DisplayName("Info Text Background Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color Info_Text_Back_Color { get; set; } = Color.FromArgb(39, 43, 50);

        [XmlIgnore]
        [DisplayName("Info Text Border Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color Info_Text_Border_Color { get; set; } = Color.Black;

        [XmlIgnore]
        [DisplayName("Info Text Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color Info_Text_Color { get; set; } = Color.FromArgb(255, 255, 255);


        // Serialize Helpers 
        [Browsable(false)]
        public byte On_Mouse_Middle_Click_As_Byte
        {
            get { return (byte)On_Mouse_Middle_Click; }
            set { On_Mouse_Middle_Click = (InRegionTasks)value; }
        }

        [Browsable(false)]
        public byte On_Mouse_Right_Click_As_Byte
        {
            get { return (byte)On_Mouse_Right_Click; }
            set { On_Mouse_Right_Click = (InRegionTasks)value; }
        }

        [Browsable(false)]
        public byte On_XButton1_Click_As_Byte
        {
            get { return (byte)On_XButton1_Click; }
            set { On_XButton1_Click = (InRegionTasks)value; }
        }

        [Browsable(false)]
        public byte On_XButton2_Click_As_Byte
        {
            get { return (byte)On_XButton2_Click; }
            set { On_XButton2_Click = (InRegionTasks)value; }
        }

        [Browsable(false)]
        public byte On_Escape_Press_As_Byte
        {
            get { return (byte)On_Escape_Press; }
            set { On_Escape_Press = (InRegionTasks)value; }
        }

        [Browsable(false)]
        public byte On_Z_Press_As_Byte
        {
            get { return (byte)On_Z_Press; }
            set { On_Z_Press = (InRegionTasks)value; }
        }


        [Browsable(false)]
        public byte Mode_As_Byte
        {
            get { return (byte)Mode; }
            set { Mode = (RegionCaptureMode)value; }
        }


        [Browsable(false)]
        public int Background_Overlay_Color_As_Decimal
        {
            get { return ColorHelper.ColorToDecimal(Background_Overlay_Color, ColorFormat.ARGB); }
            set { Background_Overlay_Color = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int Screen_Wide_Crosshair_Color_As_Decimal
        {
            get { return ColorHelper.ColorToDecimal(Screen_Wide_Crosshair_Color, ColorFormat.ARGB); }
            set { Screen_Wide_Crosshair_Color = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int Magnifier_Crosshair_Color_As_Decimal
        {
            get { return ColorHelper.ColorToDecimal(Magnifier_Crosshair_Color, ColorFormat.ARGB); }
            set { Magnifier_Crosshair_Color = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int Magnifier_Grid_Color_As_Decimal
        {
            get { return ColorHelper.ColorToDecimal(Magnifier_Grid_Color, ColorFormat.ARGB); }
            set { Magnifier_Grid_Color = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int Magnifier_Border_Color_As_Decimal
        {
            get { return ColorHelper.ColorToDecimal(Magnifier_Border_Color, ColorFormat.ARGB); }
            set { Magnifier_Border_Color = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int Info_Text_Back_Color_As_Decimal
        {
            get { return ColorHelper.ColorToDecimal(Info_Text_Back_Color, ColorFormat.ARGB); }
            set { Info_Text_Back_Color = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int Info_Text_Border_Color_As_Decimal
        {
            get { return ColorHelper.ColorToDecimal(Info_Text_Border_Color, ColorFormat.ARGB); }
            set { Info_Text_Border_Color = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int Info_Text_Color_As_Decimal
        {
            get { return ColorHelper.ColorToDecimal(Info_Text_Color, ColorFormat.ARGB); }
            set { Info_Text_Color = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }
    }
}
