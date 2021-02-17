using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WinkingCat.HelperLibs
{
    public class AdobeRGB
    {
        public float r { get; private set; }
        public float g { get; private set; }
        public float b { get; private set; }
        public ushort alpha { get; private set; }

        public AdobeRGB(ushort r, ushort g, ushort b, ushort a = 255) : this (Color.FromArgb(a, r, g, b))
        {

        }

        public AdobeRGB(Color color) : this(new XYZ(color))
        {

        }

        public AdobeRGB(XYZ xyz)
        {
            float newX = xyz.X / 100;
            float newY = xyz.Y / 100;
            float newZ = xyz.Z / 100;

            r = newX * 2.04137f + newY * -0.56495f + newZ * -0.34469f;
            g = newX * -0.96927f + newY * 1.87601f + newZ * 0.04156f;
            b = newX * 0.01345f + newY * -0.11839f + newZ * 1.01541f;
            
            r = (float)Math.Pow(r, (1 / 2.19921875));
            g = (float)Math.Pow(g, (1 / 2.19921875));
            b = (float)Math.Pow(b, (1 / 2.19921875));

            r = (float)Math.Round(r * 255, ColorHelper.decimalPlaces);
            g = (float)Math.Round(g * 255, ColorHelper.decimalPlaces);
            b = (float)Math.Round(b * 255, ColorHelper.decimalPlaces);
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", r, g, b);
        }
    }
    public class Yxy
    {
        public float Y { get; private set; }
        public float x { get; private set; }
        public float y { get; private set; }
        public ushort alpha { get; private set; }

        public Yxy(ushort r, ushort g, ushort b, ushort a = 255) : this(Color.FromArgb(a, r, g, b))
        {

        }

        public Yxy(Color color) : this(new XYZ(color))
        {

        }

        public Yxy(XYZ xyz)
        {
            Y = (float)Math.Round(xyz.Y, ColorHelper.decimalPlaces);
            x = (float)Math.Round(xyz.X / (xyz.X + xyz.Y + xyz.Z), ColorHelper.decimalPlaces);
            y = (float)Math.Round(xyz.Y / (xyz.X + xyz.Y + xyz.Z), ColorHelper.decimalPlaces);
            alpha = xyz.alpha;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", Y, x, y);
        }
    }
    public class XYZ
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Z { get; private set; }
        public ushort alpha { get; private set; }

        public XYZ(Color color) : this(color.R, color.G, color.B, color.A)
        {

        }

        public XYZ(ushort r, ushort g, ushort b, ushort a = 255)
        {
            float newR = r / 255f;
            float newG = g / 255f;
            float newB = b / 255f;

            if (newR > 0.04045)
                newR = (float)Math.Pow((newR + 0.055) / 1.055, 2.4);
            else
                newR = newR / 12.92f;

            if (newG > 0.04045)
                newG = (float)Math.Pow((newG + 0.055) / 1.055, 2.4);
            else
                newG = newG / 12.92f;

            if (newB > 0.04045)
                newB = (float)Math.Pow((newB + 0.055) / 1.055, 2.4);
            else
                newB = newB / 12.92f;

            newR = newR * 100f;
            newG = newG * 100f;
            newB = newB * 100f;

            X = (float)Math.Round(newR * 0.4124f + newG * 0.3576f + newB * 0.1805f, ColorHelper.decimalPlaces);
            Y = (float)Math.Round(newR * 0.2126f + newG * 0.7152f + newB * 0.0722f, ColorHelper.decimalPlaces);
            Z = (float)Math.Round(newR * 0.0193f + newG * 0.1192f + newB * 0.9505f, ColorHelper.decimalPlaces);
            alpha = a;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", X, Y, Z);
        }
    }
    public class CMYK
    {
        public float c { get; private set; }
        public float m { get; private set; }
        public float y { get; private set; }
        public float k { get; private set; }
        public float cPercent { get; private set; }
        public float mPercent { get; private set; }
        public float yPercent { get; private set; }
        public float kPercent { get; private set; }
        public ushort alpha { get; private set; }
        public CMYK(Color color)
        {
            if (color.R == 0 && color.G == 0 && color.B == 0)
            {
                c = 0;
                m = 0;
                y = 0;
                k = 1;
            }
            else
            {
                float modifiedR, modifiedG, modifiedB;

                modifiedR = color.R / 255f;
                modifiedG = color.G / 255f;
                modifiedB = color.B / 255f;

                k = 1 - new List<float>() { modifiedR, modifiedG, modifiedB }.Max();
                c = (1 - modifiedR - k) / (1 - k);
                m = (1 - modifiedG - k) / (1 - k);
                y = (1 - modifiedB - k) / (1 - k);

                cPercent = (float)Math.Round(c * 100, ColorHelper.decimalPlaces);
                mPercent = (float)Math.Round(m * 100, ColorHelper.decimalPlaces);
                yPercent = (float)Math.Round(y * 100, ColorHelper.decimalPlaces);
                
            }
            kPercent = (float)Math.Round(k * 100, ColorHelper.decimalPlaces);
            alpha = color.A;
        }

        public CMYK(ushort r, ushort g, ushort b, ushort a = 255) : this(Color.FromArgb(a, r, g, b))
        {

        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}", cPercent, mPercent, yPercent, kPercent);
        }
    }
    public class HSL
    {
        public float hue { get; private set; }
        public float saturation { get; private set; }
        public float lightness { get; private set; }
        public float hueDegree { get; private set; }
        public float saturationPercent { get; private set; }
        public float lightnessPercent { get; private set; }
        public ushort alpha { get; private set; }

        public HSL(Color color)
        {
            hue = color.GetHue();
            saturation = color.GetSaturation();
            lightness = color.GetBrightness();
            hueDegree = hue;
            saturationPercent = (float)Math.Round(saturation * 100f, ColorHelper.decimalPlaces);
            lightnessPercent = (float)Math.Round(lightness * 100f, ColorHelper.decimalPlaces);
            alpha = color.A;
        }

        public HSL(ushort r, ushort g, ushort b, ushort a = 255) : this(Color.FromArgb(a, r, g, b))
        {

        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", hueDegree, saturationPercent, lightnessPercent);
        }
    }
    public class HSB
    {
        public float hue { get; private set; }
        public float saturation { get; private set; }
        public float brightness { get; private set; }
        public float hueDegree { get; private set; }
        public float saturationPercent { get; private set; }
        public float brightnessPercent { get; private set; }
        public ushort alpha { get; private set; }

        public HSB(Color color) : this(color.R, color.G, color.B)
        {
            alpha = color.A;
        }

        public HSB(ushort r, ushort g, ushort b, ushort a = 255)
        {
            float newR = r / 255f;
            float newG = g / 255f;
            float newB = b / 255f;
            float min = new List<float> { newR, newB, newG }.Min();

            if (newR >= newB && newR >= newG) // newR > than both
            {
                hue = (newG - newB) / (newR - min);
                if (newR == 0)
                    saturation = 0f;
                else
                    saturation = (newR - min) / newR;
                brightness = newR;
            }
            else if (newB > newG) // newB > both
            {
                hue = 4.0f + (newR - newG) / (newB - min);
                if (newB == 0)
                    saturation = 0f;
                else
                    saturation = (newB - min) / newB;
                brightness = newB;
            }
            else // newG > both
            {
                hue = 2.0f + (newB - newR) / (newG - min);
                if (newG == 0)
                    saturation = 0f;
                else
                    saturation = (newG - min) / newG;
                brightness = newG;
            }
            hueDegree = hue * 60f;
            saturationPercent = (float)Math.Round(saturation * 100f, ColorHelper.decimalPlaces);
            brightnessPercent = (float)Math.Round(brightness * 100f, ColorHelper.decimalPlaces);
            alpha = a;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", hueDegree, saturationPercent, brightnessPercent);
        }
    }
    public class _Color
    {
        public HSB hsb { get; private set; }
        public HSL hsl { get; private set; }
        public CMYK cmyk { get; private set; }
        public ushort r { get; private set; }
        public ushort g { get; private set; }
        public ushort b { get; private set; }
        public string hex { get; private set; }
        public string alphaHex { get; private set; }
        public int Decimal { get; private set; }
        public int alphaDecimal { get; private set; }
        public ushort alpha { get; private set; }
        public _Color(Color color)
        {
            hsb = new HSB(color);
            hsl = new HSL(color);
            cmyk = new CMYK(color);
            r = color.R;
            g = color.G;
            b = color.B;
            alpha = color.A;
            hex = ColorHelper.ColorToHex(color);
            alphaHex = ColorHelper.ColorToHex(color, ColorFormat.ARGB);
            Decimal = ColorHelper.ColorToDecimal(color);
            alphaDecimal = ColorHelper.ColorToDecimal(color, ColorFormat.ARGB);
        }

        public _Color(int r, int g, int b, int a = 255) : this(Color.FromArgb(a, r, g, b))
        {

        }

        public XYZ ToXYZ()
        {
            return new XYZ(r, g, b, alpha);
        }

        public Yxy ToYxy()
        {
            return new Yxy(r, g, b, alpha);
        }

        public AdobeRGB ToAdobeRGB()
        {
            return new AdobeRGB(r, g, b, alpha);
        }
    }

    public static class ColorHelper
    {
        public static int decimalPlaces { get; set; } = 2;


        public static string ColorToHex(Color color, ColorFormat format = ColorFormat.RGB)
        {
            switch (format)
            {
                default:
                case ColorFormat.RGB:
                    return string.Format("{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
                case ColorFormat.ARGB:
                    return string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", color.A, color.R, color.G, color.B);
            }
        }

        public static int ColorToDecimal(Color color, ColorFormat format = ColorFormat.RGB)
        {
            switch (format)
            {
                default:
                case ColorFormat.RGB:
                    return color.R << 16 | color.G << 8 | color.B;
                case ColorFormat.ARGB:
                    return color.A << 24 | color.R << 16 | color.G << 8 | color.B;
            }
        }
    }
}
