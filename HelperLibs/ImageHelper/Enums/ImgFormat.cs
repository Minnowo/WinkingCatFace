using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{
    public enum ImgFormat
    {
        png,
        jpg,
        tif,
        bmp,
        gif,
        wrm,
        webp,

        [Browsable(false)]
        nil = -1
    }
}
