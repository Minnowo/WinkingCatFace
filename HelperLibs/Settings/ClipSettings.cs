using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WinkingCat.HelperLibs
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ClipSettings
    {
        [DisplayName("Border Grab Size (px)")]
        public int Border_Grab_Size { get; set; } = 1;

        [DisplayName("Border Thickness (px)")]
        public int Border_Thickness { get; set; } = 2;

        [DisplayName("Zoom Border Thickness (px)")]
        public int Zoom_Border_Thickness { get; set; } = 2;

        [DisplayName("Zoom Refresh Rate (ms)")]
        public int Zoom_Refresh_Rate
        {
            get { return _Zoom_Refresh_Rate; }
            set { _Zoom_Refresh_Rate = value.Clamp(1, 5000); }
        }
        private int _Zoom_Refresh_Rate = 10;

        [DisplayName("Zoom Size, (% of clip size 0 - 1)")]
        public float Zoom_Size_From_Percent 
        { 
            get { return _Zoom_Size_From_Percent; }
            set { _Zoom_Size_From_Percent = value.Clamp(0.01f, 1f); } 
        }
        private float _Zoom_Size_From_Percent = 1f;

        [DisplayName("Zoom Follows Mouse")]
        public bool Zoom_Follow_Mouse { get; set; } = false;

        [DisplayName("Force Aspect Ratio")]
        public bool Force_Aspect_Ratio { get; set; } = true;

        [DisplayName("Border Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        [XmlIgnore]
        public Color Border_Color { get; set; } = Color.FromArgb(249, 0, 187);

        [DisplayName("Zoom Back Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        [XmlIgnore]
        public Color Zoom_Color { get; set; } = Color.Black;

        // serialize helpers 

        [Browsable(false)]
        public int Border_Color_As_Decimal
        {
            get { return ColorHelper.ColorToDecimal(Border_Color, ColorFormat.RGB); }
            set { Border_Color = ColorHelper.DecimalToColor(value, ColorFormat.RGB); }
        }

        [Browsable(false)]
        public int Zoom_Color_As_Decimal
        {
            get { return ColorHelper.ColorToDecimal(Zoom_Color, ColorFormat.RGB); }
            set { Zoom_Color = ColorHelper.DecimalToColor(value, ColorFormat.RGB); }
        }
    }
}
