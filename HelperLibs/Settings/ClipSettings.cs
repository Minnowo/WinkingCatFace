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
    public class ClipSettings
    {
        public int Border_Grab_Size { get; set; } = 1;
        public int Border_Thickness { get; set; } = 2;
        public int Zoom_Refresh_Rate
        { 
            get { return _Zoom_Refresh_Rate; } 
            set { _Zoom_Refresh_Rate = value.Clamp(1, 5000); } 
        }
        private int _Zoom_Refresh_Rate = 10;

        public bool Force_Aspect_Ratio { get; set; } = true;

        public Color Border_Color { get; set; } = Color.FromArgb(249, 0, 187);
    }
}
