﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MiscSettings
    {
        public string Screenshot_Folder_Path { get; set; } = "";
        public bool Use_Custom_Screenshot_Folder { get; set; } = false;
        public ImgFormat Default_Image_Format { get; set; } = ImgFormat.png;
        public ColorFormat Default_Color_Format { get; set; } = ColorFormat.RGB;
    }
}