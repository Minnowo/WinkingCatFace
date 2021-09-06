using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MainFormSettings
    {
        public bool Show_In_Tray { get; set; } = true;
        public bool Start_In_Tray { get; set; } = false;
        public bool Hide_In_Tray_On_Close { get; set; } = true;
        public bool Hide_Form_On_Captrue { get; set; } = true;
        public bool Always_On_Top { get; set; } = true;

        public int Wait_Hide_Time { get; set; } = 300;
        public int Image_Counter { get; set; } = 0;

        [XmlIgnore]
        public Function On_Tray_Left_Click { get; set; } = Function.RegionCapture;
        [XmlIgnore]
        public Function On_Tray_Double_Click { get; set; } = Function.OpenMainForm;
        [XmlIgnore]
        public Function On_Tray_Middle_Click { get; set; } = Function.NewClipFromClipboard;

        // visual 
        public float contextMenuOpacity { get; set; } = 0.9f;
        public bool useImersiveDarkMode { get; set; } = true;

        [XmlIgnore]
        public Color backgroundColor { get; set; } = Color.FromArgb(42, 47, 56);
        [XmlIgnore]
        public Color lightBackgroundColor { get; set; } = Color.FromArgb(52, 57, 65);
        [XmlIgnore]
        public Color darkBackgroundColor { get; set; } = Color.FromArgb(28, 32, 38);
        [XmlIgnore]
        public Color textColor { get; set; } = Color.FromArgb(235, 235, 235);
        [XmlIgnore]
        public Color borderColor { get; set; } = Color.FromArgb(28, 32, 38);
        [XmlIgnore]
        public Color menuHighlightColor { get; set; } = Color.FromArgb(30, 34, 40);
        [XmlIgnore]
        public Color menuHighlightBorderColor { get; set; } = Color.FromArgb(116, 129, 152);
        [XmlIgnore]
        public Color menuBorderColor { get; set; } = Color.FromArgb(22, 26, 31);
        [XmlIgnore]
        public Color menuCheckBackgroundColor { get; set; } = Color.FromArgb(26, 64, 75);
        [XmlIgnore]
        public Color separatorDarkColor { get; set; } = Color.FromArgb(22, 26, 31);
        [XmlIgnore]
        public Color separatorLightColor { get; set; } = Color.FromArgb(56, 64, 75);
        [XmlIgnore]
        public Color contextMenuFontColor { get; set; } = Color.White;
        [XmlIgnore]
        public Color imageViewerBackColor { get; set; } = Color.Black;
        [XmlIgnore]
        public Color linkColor { get; set; } = Color.FromArgb(164, 98, 160);

        // xml helpers 

        public byte On_Tray_Left_Click_As_Byte
        {
            get { return (byte)On_Tray_Left_Click; }
            set { On_Tray_Left_Click = (Function)value; }
        }

        public byte On_Tray_Double_Click_As_Byte
        {
            get { return (byte)On_Tray_Double_Click; }
            set { On_Tray_Double_Click = (Function)value; }
        }

        public byte On_Tray_Middle_Click_As_Byte
        {
            get { return (byte)On_Tray_Middle_Click; }
            set { On_Tray_Middle_Click = (Function)value; }
        }

        public int backgroundColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(backgroundColor, ColorFormat.ARGB); }
            set { backgroundColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int lightBackgroundColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(lightBackgroundColor, ColorFormat.ARGB); }
            set { lightBackgroundColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int darkBackgroundColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(darkBackgroundColor, ColorFormat.ARGB); }
            set { darkBackgroundColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int textColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(textColor, ColorFormat.ARGB); }
            set { textColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int borderColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(borderColor, ColorFormat.ARGB); }
            set { borderColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int menuHighlightColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(menuHighlightColor, ColorFormat.ARGB); }
            set { menuHighlightColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int menuHighlightBorderColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(menuHighlightBorderColor, ColorFormat.ARGB); }
            set { menuHighlightBorderColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int menuBorderColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(menuBorderColor, ColorFormat.ARGB); }
            set { menuBorderColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int menuCheckBackgroundColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(menuCheckBackgroundColor, ColorFormat.ARGB); }
            set { menuCheckBackgroundColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int separatorDarkColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(separatorDarkColor, ColorFormat.ARGB); }
            set { separatorDarkColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int separatorLightColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(separatorLightColor, ColorFormat.ARGB); }
            set { separatorLightColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int contextMenuFontColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(contextMenuFontColor, ColorFormat.ARGB); }
            set { contextMenuFontColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int imageViewerBackColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(imageViewerBackColor, ColorFormat.ARGB); }
            set { imageViewerBackColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }

        public int linkColor_As_Int
        {
            get { return ColorHelper.ColorToDecimal(linkColor, ColorFormat.ARGB); }
            set { linkColor = ColorHelper.DecimalToColor(value, ColorFormat.ARGB); }
        }
    }
}
