using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{
    /// <summary>
    /// The different color formats.
    /// </summary>
    public enum ColorFormat
    {
        /// <summary>
        /// The RGB color format. 255, 255, 255
        /// Red, Green, Blue -> 0 - 255, 0 - 255, 0 - 255
        /// </summary>
        RGB,

        /// <summary>
        /// The ARGB color format. 255, 255, 255, 255
        /// Alpha, Red, Green, Blue -> 0 - 255, 0 - 255, 0 - 255, 0 - 255
        /// </summary>
        ARGB,

        /// <summary>
        /// The RGB or ARGB format in Hex form. #FFFFFF or #FFFFFFFF
        /// </summary>
        Hex,

        /// <summary>
        /// The RGB or ARGB format in Decimal form. 16777215
        /// </summary>
        Decminal,

        /// <summary>
        /// The CMYK color format. 0, 0, 0, 0
        /// Cyan, Magenta, Yellow, Key -> 0 - 100, 0 - 100, 0 - 100, 0 - 100
        /// </summary>
        CMYK,

        /// <summary>
        /// The HSB color format. 0, 0, 100
        /// Hue, Saturation, Brightness -> 0 - 360, 0 - 100, 0 - 100 
        /// </summary>
        HSB,

        /// <summary>
        /// The HSL color format. 0, 0, 100
        /// Hue, Saturation, Value -> 0 - 360, 0 - 100, 0 - 100
        /// </summary>
        HSV,

        /// <summary>
        /// The HSV color format. 0, 0, 100
        /// Hue, Saturation, Lightness -> 0 - 360, 0 - 100, 0 - 100
        /// </summary>
        HSL
    }
}
