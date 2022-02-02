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
    public class MainFormSettings
    {
        [Browsable(false)]
        public bool Open_Files_On_Double_Click { get; set; } = true;

        [Browsable(false)]
        public bool Show_Maximize_Box { get; set; } = true;

        [Browsable(false)]
        public bool Show_In_Tray { get; set; } = true;
        [Browsable(false)]
        public bool Start_In_Tray { get; set; } = false;
        [Browsable(false)]
        public bool Hide_In_Tray_On_Close { get; set; } = true;
        [Browsable(false)]
        public bool Hide_Form_On_Captrue { get; set; } = true;
        [Browsable(false)]
        public bool Always_On_Top { get; set; } = true;

        [Browsable(false)]
        public int Wait_Hide_Time { get; set; } = 300;
        [Browsable(false)]
        public int Image_Counter { get; set; } = 0;

        [XmlIgnore]
        [Browsable(false)]
        public Function On_Tray_Left_Click { get; set; } = Function.RegionCapture;
        [XmlIgnore]
        [Browsable(false)]
        public Function On_Tray_Double_Click { get; set; } = Function.OpenMainForm;
        [XmlIgnore]
        [Browsable(false)]
        public Function On_Tray_Middle_Click { get; set; } = Function.NewClipFromClipboard;



        // visual 
        [DisplayName("Context Menu Opacity")]
        public float contextMenuOpacity { get; set; } = 0.9f;
        [DisplayName("Use Imersive Dark Mode")]
        public bool useImersiveDarkMode { get; set; } = true;

        [DisplayName("Force Listview Column Fill")]
        public bool forceColumnFill { get; set; } = true;

        [XmlIgnore]
        [DisplayName("Background Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color backgroundColor { get; set; } = Color.FromArgb(42, 47, 56);

        [XmlIgnore]
        [DisplayName("Light Background Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color lightBackgroundColor { get; set; } = Color.FromArgb(52, 57, 65);

        [XmlIgnore]
        [DisplayName("Dark Background Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color darkBackgroundColor { get; set; } = Color.FromArgb(28, 32, 38);

        [XmlIgnore]
        [DisplayName("Text Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color textColor { get; set; } = Color.FromArgb(235, 235, 235);

        [XmlIgnore]
        [DisplayName("Border Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color borderColor { get; set; } = Color.FromArgb(28, 32, 38);

        [XmlIgnore]
        [DisplayName("Menu Highlight Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color menuHighlightColor { get; set; } = Color.FromArgb(30, 34, 40);

        [XmlIgnore]
        [DisplayName("Menu Highlight Border Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color menuHighlightBorderColor { get; set; } = Color.FromArgb(116, 129, 152);

        [XmlIgnore]
        [DisplayName("Menu Border Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color menuBorderColor { get; set; } = Color.FromArgb(22, 26, 31);

        [XmlIgnore]
        [DisplayName("Menu Check Background Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color menuCheckBackgroundColor { get; set; } = Color.FromArgb(26, 64, 75);

        [XmlIgnore]
        [DisplayName("Separator Dark Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color separatorDarkColor { get; set; } = Color.FromArgb(22, 26, 31);

        [XmlIgnore]
        [DisplayName("Separator Light Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color separatorLightColor { get; set; } = Color.FromArgb(56, 64, 75);

        [XmlIgnore]
        [DisplayName("Context Menu Font Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color contextMenuFontColor { get; set; } = Color.White;

        [XmlIgnore]
        [DisplayName("Image Viewer Background Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color imageViewerBackColor { get; set; } = Color.Black;

        [XmlIgnore]
        [DisplayName("Link Color")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorConverter))]
        public Color linkColor { get; set; } = Color.FromArgb(164, 98, 160);



        // xml helpers 

        [Browsable(false)]
        public byte On_Tray_Left_Click_As_Byte
        {
            get { return (byte)On_Tray_Left_Click; }
            set { On_Tray_Left_Click = (Function)value; }
        }

        [Browsable(false)]
        public byte On_Tray_Double_Click_As_Byte
        {
            get { return (byte)On_Tray_Double_Click; }
            set { On_Tray_Double_Click = (Function)value; }
        }

        [Browsable(false)]
        public byte On_Tray_Middle_Click_As_Byte
        {
            get { return (byte)On_Tray_Middle_Click; }
            set { On_Tray_Middle_Click = (Function)value; }
        }

        [Browsable(false)]
        public int backgroundColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(backgroundColor, ColorFormat.ARGB); }
            set { backgroundColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int lightBackgroundColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(lightBackgroundColor, ColorFormat.ARGB); }
            set { lightBackgroundColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int darkBackgroundColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(darkBackgroundColor, ColorFormat.ARGB); }
            set { darkBackgroundColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int textColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(textColor, ColorFormat.ARGB); }
            set { textColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int borderColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(borderColor, ColorFormat.ARGB); }
            set { borderColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int menuHighlightColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(menuHighlightColor, ColorFormat.ARGB); }
            set { menuHighlightColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int menuHighlightBorderColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(menuHighlightBorderColor, ColorFormat.ARGB); }
            set { menuHighlightBorderColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int menuBorderColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(menuBorderColor, ColorFormat.ARGB); }
            set { menuBorderColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int menuCheckBackgroundColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(menuCheckBackgroundColor, ColorFormat.ARGB); }
            set { menuCheckBackgroundColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int separatorDarkColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(separatorDarkColor, ColorFormat.ARGB); }
            set { separatorDarkColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int separatorLightColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(separatorLightColor, ColorFormat.ARGB); }
            set { separatorLightColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int contextMenuFontColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(contextMenuFontColor, ColorFormat.ARGB); }
            set { contextMenuFontColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int imageViewerBackColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(imageViewerBackColor, ColorFormat.ARGB); }
            set { imageViewerBackColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        [Browsable(false)]
        public int linkColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(linkColor, ColorFormat.ARGB); }
            set { linkColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }
    }
}
