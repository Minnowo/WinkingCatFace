using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{
    /// <summary>
    /// Specifies which piece of the color space should be drawn in the color box / slider.
    /// </summary>
    public enum ColorSpaceDrawStyle
    {
        /// <summary>
        /// The RGB -> Red.
        /// 
        /// Slider controls red
        /// X = Blue 0 -> 255
        /// Y = Green 255 -> 0
        /// </summary>
        Red,

        /// <summary>
        /// The RGB -> Green.
        /// 
        /// Slider controls green
        /// X = Red 255 -> 0
        /// Y = Blue 0 -> 255
        /// </summary>
        Green,

        /// <summary>
        /// The RGB -> Blue.
        /// 
        /// Slider controls blue
        /// X = Red 0 -> 255
        /// Y = Green 255 -> 0
        /// </summary>
        Blue,

        /// <summary>
        /// The HSB -> Hue.
        /// 
        /// Slider controls hue
        /// X = Saturation 0 -> 100
        /// Y = Brightness 100 -> 0
        /// </summary>
        HSBHue,

        /// <summary>
        /// The HSB -> Saturation.
        /// 
        /// Slider controls saturation
        /// X = Hue 0 -> 360
        /// Y = Brightness 100 -> 0
        /// </summary>
        HSBSaturation,

        /// <summary>
        /// The HSB -> Brightness.
        /// 
        /// Slider controls brightness
        /// X = Hue 0 -> 360
        /// Y = Saturation 100 -> 0
        /// </summary>
        HSBBrightness,

        /// <summary>
        /// The HSL -> Hue.
        /// 
        /// Slider controls hue
        /// X = Saturation 0 -> 100
        /// Y = Lightness 100 -> 0
        /// </summary>
        HSLHue,

        /// <summary>
        /// The HSL -> Saturation.
        /// 
        /// Slider controls saturation
        /// X = Hue 0 -> 360
        /// Y = Lightness 100 -> 0
        /// </summary>
        HSLSaturation,

        /// <summary>
        /// The HSL -> Lightness.
        /// 
        /// Slider controls lightness
        /// X = Hue 0 -> 360
        /// Y = Saturation 100 -> 0
        /// </summary>
        HSLLightness,

    }
}
