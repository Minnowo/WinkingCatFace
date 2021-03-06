using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{
    public delegate void ColorEventHandler(object sender, ColorEventArgs e);

    public class ColorEventArgs : EventArgs
    {
        public ColorEventArgs(_Color color, _Color absoluteColor, ColorFormat format)
        {
            Color = color;
            AbsoluteColor = absoluteColor;
            ColorType = format;
        }

        public ColorEventArgs(_Color color, _Color absoluteColor, DrawStyles drawStyle)
        {
            Color = color;
            AbsoluteColor = absoluteColor;
            DrawStyle = drawStyle;
        }

        public _Color Color;
        public _Color AbsoluteColor;
        public ColorFormat ColorType;
        public DrawStyles DrawStyle;
    }
}
