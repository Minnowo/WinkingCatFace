﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{
    public delegate void ColorEventHandler(object sender, ColorEventArgs e);

    public class ColorEventArgs : EventArgs
    {
        public COLOR Color;
        public ColorFormat ColorType;
        public ColorSpaceDrawStyle DrawStyle;

        public ColorEventArgs(COLOR color, ColorFormat format)
        {
            Color = color;
            ColorType = format;
        }

        public ColorEventArgs(COLOR color, ColorSpaceDrawStyle drawStyle)
        {
            Color = color;
            DrawStyle = drawStyle;
        }   
    }
}
