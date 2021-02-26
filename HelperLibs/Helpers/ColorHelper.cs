using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WinkingCat.HelperLibs
{

    public struct AdobeRGB
    {
        public float r { get; set; }
        public float g { get; set; }
        public float b { get; set; }
        public ushort alpha { get; set; }

        public AdobeRGB(ushort r, ushort g, ushort b, ushort a = 255) : this(Color.FromArgb(a, r, g, b))
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

            alpha = xyz.Alpha;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", r, g, b);
        }
    }
    public struct Yxy
    {
        private float yy { get; set; }
        private float x { get; set; }
        private float y { get; set; }
        private ushort alpha { get; set; }

        public float YY
        {
            get
            {
                return yy;
            }
            set
            {
                yy = (float)ColorHelper.ValidColor(value);
            }
        }

        public float YY100
        {
            get
            {
                return yy * 100f;
            }
            set
            {
                yy = (float)ColorHelper.ValidColor(value / 100f);
            }
        }

        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = (float)ColorHelper.ValidColor(value);
            }
        }

        public float X100
        {
            get
            {
                return x * 100f;
            }
            set
            {
                x = (float)ColorHelper.ValidColor(value / 100f);
            }
        }

        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = (float)ColorHelper.ValidColor(value);
            }
        }

        public float Y100
        {
            get
            {
                return y * 100f;
            }
            set
            {
                y = (float)ColorHelper.ValidColor(value / 100f);
            }
        }

        public ushort Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = (ushort)ColorHelper.ValidColor(value);
            }
        }

        public Yxy(ushort r, ushort g, ushort b, ushort a = 255) : this(Color.FromArgb(a, r, g, b))
        {

        }

        public Yxy(Color color) : this(new XYZ(color))
        {

        }

        public Yxy(XYZ xyz)
        {
            yy = (float)Math.Round(xyz.Y, ColorHelper.decimalPlaces);
            x = (float)Math.Round(xyz.X / (xyz.X + xyz.Y + xyz.Z), ColorHelper.decimalPlaces);
            y = (float)Math.Round(xyz.Y / (xyz.X + xyz.Y + xyz.Z), ColorHelper.decimalPlaces);
            alpha = xyz.Alpha;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}",
                (float)Math.Round(YY100, ColorHelper.decimalPlaces),
                (float)Math.Round(X100, ColorHelper.decimalPlaces),
                (float)Math.Round(Y100, ColorHelper.decimalPlaces));
        }
    }
    public struct XYZ
    {
        private float x { get; set; }
        private float y { get; set; }
        private float z { get; set; }
        private ushort alpha { get; set; }

        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = (float)ColorHelper.ValidColor(value);
            }
        }

        public float X100
        {
            get
            {
                return x * 100f;
            }
            set
            {
                x = (float)ColorHelper.ValidColor(value / 100f);
            }
        }

        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                Y = (float)ColorHelper.ValidColor(value);
            }
        }

        public float Y100
        {
            get
            {
                return y * 100f;
            }
            set
            {
                Y = (float)ColorHelper.ValidColor(value / 100f);
            }
        }

        public float Z
        {
            get
            {
                return z;
            }
            set
            {
                z = (float)ColorHelper.ValidColor(value);
            }
        }

        public float Z100
        {
            get
            {
                return z * 100f;
            }
            set
            {
                z = (float)ColorHelper.ValidColor(value / 100f);
            }
        }

        public ushort Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = (ushort)ColorHelper.ValidColor(value);
            }
        }

        public XYZ(int x, int y, int z, int a) :this()
        {
            X100 = x;
            Y100 = y;
            Z100 = z;
            Alpha = (ushort)a;
        }

        public XYZ(Color color) : this(color.R, color.G, color.B, color.A)
        {
        }

        public XYZ(ushort r, ushort g, ushort b, ushort a = 255)
        {
            double newR = r / 255f;
            double newG = g / 255f;
            double newB = b / 255f;

            if (newR > 0.04045)
                newR = Math.Pow((newR + 0.055) / 1.055, 2.4);
            else
                newR = newR / 12.92f;

            if (newG > 0.04045)
                newG = Math.Pow((newG + 0.055) / 1.055, 2.4);
            else
                newG = newG / 12.92f;

            if (newB > 0.04045)
                newB = Math.Pow((newB + 0.055) / 1.055, 2.4);
            else
                newB = newB / 12.92f;

            newR = newR * 100f;
            newG = newG * 100f;
            newB = newB * 100f;

            x = (float)Math.Round((newR * 0.4124f + newG * 0.3576f + newB * 0.1805f) / 100, ColorHelper.decimalPlaces);
            y = (float)Math.Round((newR * 0.2126f + newG * 0.7152f + newB * 0.0722f) / 100, ColorHelper.decimalPlaces);
            z = (float)Math.Round((newR * 0.0193f + newG * 0.1192f + newB * 0.9505f) / 100, ColorHelper.decimalPlaces);
            alpha = a;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}",
                (float)Math.Round(X100, ColorHelper.decimalPlaces),
                (float)Math.Round(Y100, ColorHelper.decimalPlaces),
                (float)Math.Round(Z100, ColorHelper.decimalPlaces));
        }

        public static implicit operator XYZ(Color color)
        {
            return new XYZ(color);
        }

        public static implicit operator Color(XYZ color)
        {
            return color.ToColor();
        }
        public static implicit operator HSB(XYZ color)
        {
            return color.ToHSB();
        }

        public static implicit operator HSL(XYZ color)
        {
            return color.ToHSL();
        }

        public static implicit operator CMYK(XYZ color)
        {
            return color.ToCMYK();
        }


        public static implicit operator Yxy(XYZ color)
        {
            return color.ToYxy();
        }

        public static bool operator ==(XYZ left, XYZ right)
        {
            return (left.X == right.X) && (left.Y == right.Y) && (left.Z == right.Z) && (left.Alpha == right.Alpha);
        }

        public static bool operator !=(XYZ left, XYZ right)
        {
            return !(left == right);
        }
        public Color ToColor()
        {
            double r, g, b;
            r = (X * 3.2406 + Y * -1.5372 + Z * -0.4986);
            g = (X * -0.9689 + Y * 1.8758 + Z * 0.0415);
            b = (X * 0.0557 + Y * -0.2040 + Z * 1.0570);

            if (r > 0.0031308)
                r = (1.055 * Math.Pow(r, 1 / 2.4)) - 0.055;
            else
                r = 12.92 * r;

            if (g > 0.0031308)
                g = (1.055 * Math.Pow(g, 1 / 2.4)) - 0.055;
            else
                r = 12.92 * r;

            if (b > 0.0031308)
                b = (1.055 * Math.Pow(b, 1 / 2.4)) - 0.055;
            else
                b = 12.92 * b;

            return Color.FromArgb(Alpha, 
                (int)Math.Round(r * 255), 
                (int)Math.Round(g * 255), 
                (int)Math.Round(b * 255));
        }
        public HSB ToHSB()
        {
            return new HSB(this.ToColor());
        }
        public HSL ToHSL()
        {
            return new HSL(this.ToColor());
        }
        public CMYK ToCMYK()
        {
            return new CMYK(this.ToColor());
        }
        public Yxy ToYxy()
        {
            return new Yxy(this);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
    public struct CMYK
    {
        private float c { get; set; }
        private float m { get; set; }
        private float y { get; set; }
        private float k { get; set; }
        private ushort alpha { get; set; }

        public float C
        {
            get
            {
                return c;
            }
            set
            {
                c = (float)ColorHelper.ValidColor(value);
            }
        }

        public float C100
        {
            get
            {
                return c * 100f;
            }
            set
            {
                c = (float)ColorHelper.ValidColor(value / 100f);
            }
        }

        public float M
        {
            get
            {
                return m;
            }
            set
            {
                m = (float)ColorHelper.ValidColor(value);
            }
        }

        public float M100
        {
            get
            {
                return m * 100f;
            }
            set
            {
                m = (float)ColorHelper.ValidColor(value / 100f);
            }
        }

        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = (float)ColorHelper.ValidColor(value);
            }
        }

        public float Y100
        {
            get
            {
                return y * 100f;
            }
            set
            {
                y = (float)ColorHelper.ValidColor(value / 100f);
            }
        }

        public float K
        {
            get
            {
                return k;
            }
            set
            {
                k = (float)ColorHelper.ValidColor(value);
            }
        }

        public float K100
        {
            get
            {
                return k * 100f;
            }
            set
            {
                k = (float)ColorHelper.ValidColor(value / 100f);
            }
        }

        public ushort Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = (ushort)ColorHelper.ValidColor(value);
            }
        }

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
            }
            alpha = color.A;
        }

        public CMYK(ushort r, ushort g, ushort b, ushort a = 255) : this(Color.FromArgb(a, r, g, b))
        {
        }

        public CMYK(int c, int m, int y, int k, int a) : this()
        {
            C100 = c;
            M100 = m;
            Y100 = y;
            K100 = k;
            Alpha = (ushort)a;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}",
                (float)Math.Round(C100, ColorHelper.decimalPlaces),
                (float)Math.Round(M100, ColorHelper.decimalPlaces),
                (float)Math.Round(Y100, ColorHelper.decimalPlaces),
                (float)Math.Round(K100, ColorHelper.decimalPlaces));
        }

        public static implicit operator CMYK(Color color)
        {
            return new CMYK(color);
        }

        public static implicit operator Color(CMYK color)
        {
            return color.ToColor();
        }

        public static implicit operator XYZ(CMYK color)
        {
            return color.ToXYZ();
        }

        public static implicit operator Yxy(CMYK color)
        {
            return color.ToYxy();
        }

        public static bool operator ==(CMYK left, CMYK right)
        {
            return (left.C == right.C) && (left.M == right.M) && (left.Y == right.Y) && (left.K == right.K) && (left.Alpha == right.Alpha);
        }

        public static bool operator !=(CMYK left, CMYK right)
        {
            return !(left == right);
        }
        public Color ToColor()
        {
            double r, g, b;
            r = Math.Round(255 * (1 - C) * (1 - k));
            g = Math.Round(255 * (1 - M) * (1 - K));
            b = Math.Round(255 * (1 - Y) * (1 - K));
            return Color.FromArgb(Alpha, (int)r, (int)g, (int)b);
        }
        public HSB ToHSL()
        {
            return new HSB(this.ToColor());
        }
        public HSL ToCMYK()
        {
            return new HSL(this.ToColor());
        }
        public XYZ ToXYZ()
        {
            return new XYZ(this.ToColor());
        }
        public Yxy ToYxy()
        {
            return new Yxy(this.ToColor());
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
    public struct HSL
    {
        private float hue { get; set; }
        private float saturation { get; set; }
        private float lightness { get; set; }
        private ushort alpha { get; set; }

        public float Hue
        {
            get
            {
                return hue;
            }
            set
            {
                hue = (float)ColorHelper.ValidColor(value);
            }
        }

        public float Hue360
        {
            get
            {
                return hue * 360f;
            }
            set
            {
                hue = (float)ColorHelper.ValidColor(value / 360f);
            }
        }

        public float Saturation
        {
            get
            {
                return saturation;
            }
            set
            {
                saturation = (float)ColorHelper.ValidColor(value);
            }
        }

        public float Saturation100
        {
            get
            {
                return saturation * 100f;
            }
            set
            {
                saturation = (float)ColorHelper.ValidColor(value / 100f);
            }
        }

        public float Lightness
        {
            get
            {
                return lightness;
            }
            set
            {
                lightness = (float)ColorHelper.ValidColor(value);
            }
        }

        public float Lightness100
        {
            get
            {
                return lightness * 100f;
            }
            set
            {
                lightness = (float)ColorHelper.ValidColor(value / 100f);
            }
        }

        public ushort Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = (ushort)ColorHelper.ValidColor(value);
            }
        }

        public HSL(Color color) : this()
        {
            Hue360 = color.GetHue();
            Saturation = color.GetSaturation();
            Lightness = color.GetBrightness();
            alpha = color.A;
        }

        public HSL(ushort r, ushort g, ushort b, ushort a = 255) : this(Color.FromArgb(a, r, g, b))
        {
        }

        public HSL(int h, int s, int l, int a) : this()
        {
            Hue360 = h;
            Saturation100 = s;
            Lightness100 = l;
            alpha = (ushort)a;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}",
                (float)Math.Round(Hue360, ColorHelper.decimalPlaces),
                (float)Math.Round(Saturation100, ColorHelper.decimalPlaces),
                (float)Math.Round(Lightness100, ColorHelper.decimalPlaces));
        }

        public static implicit operator HSL(Color color)
        {
            return new HSL(color);
        }

        public static implicit operator Color(HSL color)
        {
            return color.ToColor();
        }

        public static implicit operator CMYK(HSL color)
        {
            return color.ToCMYK();
        }

        public static implicit operator XYZ(HSL color)
        {
            return color.ToXYZ();
        }

        public static implicit operator Yxy(HSL color)
        {
            return color.ToYxy();
        }

        public static bool operator ==(HSL left, HSL right)
        {
            return (left.Hue == right.Hue) && (left.Saturation == right.Saturation) && (left.Lightness == right.Lightness) && (left.Alpha == right.Alpha);
        }

        public static bool operator !=(HSL left, HSL right)
        {
            return !(left == right);
        }
        public Color ToColor()
        {
            float c, x, m, r = 0, g = 0, b = 0;
            c = (1 - Math.Abs(2 * lightness - 1)) * saturation;
            x = c * (1 - Math.Abs((Hue360 / 60) % 2 - 1));
            m = lightness - c/2;

            if (Hue360 < 60)
            {
                r = c;
                g = x;
                b = 0;
            }
            else if (Hue360 < 120)
            {
                r = x;
                g = c;
                b = 0;
            }
            else if (Hue360 < 180)
            {
                r = 0;
                g = c;
                b = x;
            }
            else if (Hue360 < 240)
            {
                r = 0;
                g = x;
                b = c;
            }
            else if (Hue360 < 300)
            {
                r = x;
                g = 0;
                b = c;
            }
            else if (Hue360 < 360)
            {
                r = c;
                g = 0;
                b = x;
            }

            return Color.FromArgb(Alpha, 
                (int)Math.Round((r + m) * 255), 
                (int)Math.Round((g + m) * 255), 
                (int)Math.Round((b + m) * 255));
        }
        public HSB ToHSL()
        {
            return new HSB(this.ToColor());
        }
        public CMYK ToCMYK()
        {
            return new CMYK(this.ToColor());
        }
        public XYZ ToXYZ()
        {
            return new XYZ(this.ToColor());
        }
        public Yxy ToYxy()
        {
            return new Yxy(this.ToColor());
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
    public struct HSB
    {
        private float hue { get; set; }
        private float saturation { get; set; }
        private float brightness { get; set; }
        private ushort alpha { get; set; }

        public float Brightness
        {
            get
            {
                return brightness;
            }
            set
            {
                brightness = (float)ColorHelper.ValidColor(value);
            }
        }

        public float Brightness100
        {
            get
            {
                return brightness * 100f;
            }
            set
            {
                brightness = (float)ColorHelper.ValidColor(value / 100f);
            }
        }

        public float Saturation
        {
            get
            {
                return saturation;
            }
            set
            {
                saturation = (float)ColorHelper.ValidColor(value);
            }
        }

        public float Saturation100
        {
            get
            {
                return saturation * 100f;
            }
            set
            {
                saturation = (float)ColorHelper.ValidColor(value / 100f);
            }
        }

        public float Hue
        {
            get
            {
                return hue;
            }
            set
            {
                hue = (float)ColorHelper.ValidColor(value);
            }
        }

        public float Hue360
        {
            get
            {
                return hue * 360f;
            }
            set
            {
                hue = (float)ColorHelper.ValidColor(value / 360f);
            }
        }

        public ushort Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = (ushort)ColorHelper.ValidColor(value);
            }
        }

        public HSB(int h, int s, int b, int a) : this()
        {
            Hue360 = h;
            Saturation100 = s;
            Brightness100 = b;
            Alpha = (ushort)a;
        }

        public HSB(Color color) : this(color.R, color.G, color.B)
        {
        }

        public HSB(ushort r, ushort g, ushort b, ushort a = 255)
        {
            float newR = r / 255f;
            float newG = g / 255f;
            float newB = b / 255f;
            float min = new List<float> { newR, newB, newG }.Min();

            if (newR >= newB && newR >= newG) // newR > than both
            {
                if ((newR - min) != 0) // cannot divide by 0 
                {
                    hue = (((newG - newB) / (newR - min)) % 6) / 6;
                    if (hue < 0)
                        hue += 1;
                }
                else
                    hue = 0;

                if (newR == 0)
                    saturation = 0f;
                else
                    saturation = (newR - min) / newR;
                brightness = newR;
            }
            else if (newB > newG) // newB > both
            {
                hue = (4.0f + (newR - newG) / (newB - min)) / 6;
                if (newB == 0)
                    saturation = 0f;
                else
                    saturation = (newB - min) / newB;
                brightness = newB;
            }
            else // newG > both
            {
                hue = (2.0f + (newB - newR) / (newG - min)) / 6;
                if (newG == 0)
                    saturation = 0f;
                else
                    saturation = (newG - min) / newG;
                brightness = newG;
            }
            alpha = a;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}",
                (float)Math.Round(Hue360, ColorHelper.decimalPlaces),
                (float)Math.Round(Saturation100, ColorHelper.decimalPlaces),
                (float)Math.Round(Brightness100, ColorHelper.decimalPlaces));
        }

        public static implicit operator HSB(Color color)
        {
            return new HSB(color);
        }

        public static implicit operator Color(HSB color)
        {
            return color.ToColor();
        }
        
        public static implicit operator HSL(HSB color)
        {
            return color.ToHSL();
        }

        public static implicit operator CMYK(HSB color)
        {
            return color.ToCMYK();
        }

        public static implicit operator XYZ(HSB color)
        {
            return color.ToXYZ();
        }

        public static implicit operator Yxy(HSB color)
        {
            return color.ToYxy();
        }

        public static bool operator ==(HSB left, HSB right)
        {
            return (left.Hue == right.Hue) && (left.Saturation == right.Saturation) && (left.Brightness == right.Brightness) && (left.Alpha == right.Alpha);
        }

        public static bool operator !=(HSB left, HSB right)
        {
            return !(left == right);
        }
        public Color ToColor()
        {
            float c, x, m, r = 0, g = 0, b = 0;
            c = brightness * saturation;
            x = c * (1 - Math.Abs((Hue360 / 60) % 2 - 1));
            m = brightness - c;

            if (Hue360 < 60)
            {
                r = c;
                g = x;
                b = 0;
            }
            else if (Hue360 < 120)
            {
                r = x;
                g = c;
                b = 0;
            }
            else if (Hue360 < 180)
            {
                r = 0;
                g = c;
                b = x;
            }
            else if (Hue360 < 240)
            {
                r = 0;
                g = x;
                b = c;
            }
            else if (Hue360 < 300)
            {
                r = x;
                g = 0;
                b = c;
            }
            else if (Hue360 < 360)
            {
                r = c;
                g = 0;
                b = x;
            }

            return Color.FromArgb(Alpha, 
                (int)Math.Round((r + m) * 255), 
                (int)Math.Round((g + m) * 255), 
                (int)Math.Round((b + m) * 255));
        }
        public HSL ToHSL()
        {
            return new HSL(this.ToColor());
        }
        public CMYK ToCMYK()
        {
            return new CMYK(this.ToColor());
        }
        public XYZ ToXYZ()
        {
            return new XYZ(this.ToColor());
        }
        public Yxy ToYxy()
        {
            return new Yxy(this.ToColor());
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
    public struct ARGB
    {
        private ushort r { get; set; }
        private ushort g { get; set; }
        private ushort b { get; set; }
        private ushort alpha { get; set; }

        public int R
        {
            get
            {
                return r;
            }
            set
            {
                r = (ushort)ColorHelper.ValidColor(value);
            }
        }

        public int G
        {
            get
            {
                return g;
            }
            set
            {
                g = (ushort)ColorHelper.ValidColor(value);
            }
        }

        public int B
        {
            get
            {
                return b;
            }
            set
            {
                b = (ushort)ColorHelper.ValidColor(value);
            }
        }

        public int A
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = (ushort)ColorHelper.ValidColor(value);
            }
        }

        public ARGB(int al, int red, int green, int blue)
        {
            alpha = (ushort)al;
            r = (ushort)red;
            g = (ushort)green;
            b = (ushort)blue;
        }

        public ARGB(Color argb) : this(argb.A, argb.R, argb.G, argb.B)
        {
        }

        public static implicit operator ARGB(Color color)
        {
            return new ARGB(color);
        }

        public static implicit operator Color(ARGB color)
        {
            return color.ToColor();
        }
        public static implicit operator HSB(ARGB color)
        {
            return color.ToHSB();
        }

        public static implicit operator HSL(ARGB color)
        {
            return color.ToHSL();
        }

        public static implicit operator CMYK(ARGB color)
        {
            return color.ToCMYK();
        }

        public static implicit operator XYZ(ARGB color)
        {
            return color.ToXYZ();
        }

        public static implicit operator Yxy(ARGB color)
        {
            return color.ToYxy();
        }

        public static bool operator ==(ARGB left, ARGB right)
        {
            return (left.R == right.R) && (left.G == right.G) && (left.B == right.B) && (left.A == right.A);
        }

        public static bool operator !=(ARGB left, ARGB right)
        {
            return !(left == right);
        }
        public Color ToColor()
        {
            return Color.FromArgb(A, R, G, B);
        }
        public HSB ToHSB()
        {
            return new HSB(r, g, b, alpha);
        }
        public HSL ToHSL()
        {
            return new HSL(r, g, b, alpha);
        }
        public CMYK ToCMYK()
        {
            return new CMYK(r, g, b, alpha);
        }
        public XYZ ToXYZ()
        {
            return new XYZ(r, g, b, alpha);
        }
        public Yxy ToYxy()
        {
            return new Yxy(r, g, b, alpha);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }

    public struct _Color
    {
        public ARGB argb { get; set; }
        public HSB hsb { get; set; }
        public HSL hsl { get; set; }
        public CMYK cmyk { get; set; }
        public ushort r { get; set; }
        public ushort g { get; set; }
        public ushort b { get; set; }
        public string hex { get; set; }
        public string alphaHex { get; set; }
        public int Decimal { get; set; }
        public int alphaDecimal { get; set; }
        public ushort alpha { get; set; }
        public _Color(Color color)
        {
            argb = color;
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

        public static implicit operator _Color(Color color)
        {
            return new _Color(color);
        }

        public static implicit operator Color(_Color color)
        {
            return Color.FromArgb(color.alpha, color.r, color.g, color.b);
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

        public static double ValidColor(double number)
        {
            return number.Clamp(0, 1);
        }

        public static float ValidColor(float number)
        {
            return (float)number.Clamp(0, 1);
        }

        public static int ValidColor(int number)
        {
            return number.Clamp(0, 255);
        }

        public static byte ValidColor(byte number)
        {
            return number.Clamp<byte>(0, 255);
        }
    }


}
