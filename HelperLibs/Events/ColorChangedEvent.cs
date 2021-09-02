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
        public ColorEventArgs(COLOR color, ColorFormat format)
        {
            Color = color;
            AbsoluteColor = color;
            ColorType = format;
        }
        public ColorEventArgs(COLOR color, COLOR absoluteColor, ColorFormat format)
        {
            Color = color;
            AbsoluteColor = absoluteColor;
            ColorType = format;
        }

        public ColorEventArgs(COLOR color, COLOR absoluteColor, DrawStyles drawStyle)
        {
            Color = color;
            AbsoluteColor = absoluteColor;
            DrawStyle = drawStyle;
        }

        public COLOR Color;
        public COLOR AbsoluteColor;
        public ColorFormat ColorType;
        public DrawStyles DrawStyle;
    }
}
