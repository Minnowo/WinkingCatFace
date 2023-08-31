using System.ComponentModel;

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
