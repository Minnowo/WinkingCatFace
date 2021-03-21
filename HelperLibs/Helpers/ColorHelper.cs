using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Text.RegularExpressions;

namespace WinkingCat.HelperLibs
{
    public struct AdobeRGB
    {
        public double r { get; set; }
        public double g { get; set; }
        public double b { get; set; }
        public ushort alpha { get; set; }
        public static readonly AdobeRGB Empty;
        public double R
        {
            get
            {
                return r;
            }
            set
            {
                r = ColorHelper.ValidRGBColor(value);
            }
        }
        public double G
        {
            get
            {
                return g;
            }
            set
            {
                g = ColorHelper.ValidRGBColor(value);
            }
        }
        public double B
        {
            get
            {
                return b;
            }
            set
            {
                b = ColorHelper.ValidRGBColor(value);
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

        public AdobeRGB(ushort r, ushort g, ushort b, ushort a = 255) : this()
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.alpha = a;
        }

        public AdobeRGB(Color color) : this(new XYZ(color))
        {

        }

        public AdobeRGB(XYZ xyz)
        {
            double newX = xyz.X / 100d;
            double newY = xyz.Y / 100d;
            double newZ = xyz.Z / 100d;

            r = (newX * 2.04137) + (newY * -0.56495) + (newZ * -0.34469);
            g = (newX * -0.96927) + (newY * 1.87601) + (newZ * 0.04156);
            b = (newX * 0.01345) + (newY * -0.11839) + (newZ * 1.01541);

            if(r <= 0.0)
            {
                r = 0.0;
            }
            else
            {
                r = MathHelper.checkSquareRoot(r, 0.45470692717);
            }

            if (g <= 0.0)
            {
                g = 0.0;
            }
            else
            {
                g = MathHelper.checkSquareRoot(g, 0.45470692717);
            }

            if (b <= 0.0)
            {
                b = 0.0;
            }
            else
            {
                b = MathHelper.checkSquareRoot(b, 0.45470692717);
            }
            
            r = (r * 255d).Clamp(0, 255);
            g = (g * 255d).Clamp(0, 255);
            b = (b * 255d).Clamp(0, 255);

            alpha = xyz.Alpha;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}",
                Math.Round(r, ColorHelper.decimalPlaces),
                Math.Round(g, ColorHelper.decimalPlaces),
                Math.Round(b, ColorHelper.decimalPlaces));
        }
        public static implicit operator AdobeRGB(Color color)
        {
            return new AdobeRGB(color);
        }

        public static implicit operator Color(AdobeRGB color)
        {
            return color.ToColor();
        }

        public static implicit operator HSL(AdobeRGB color)
        {
            return color.ToHSL();
        }

        public static implicit operator CMYK(AdobeRGB color)
        {
            return color.ToCMYK();
        }

        public static implicit operator XYZ(AdobeRGB color)
        {
            return color.ToXYZ();
        }

        public static implicit operator Yxy(AdobeRGB color)
        {
            return color.ToYxy();
        }

        public static bool operator ==(AdobeRGB left, AdobeRGB right)
        {
            return (left.r == right.r) && (left.g == right.g) && (left.b == right.b) && (left.Alpha == right.Alpha);
        }

        public static bool operator !=(AdobeRGB left, AdobeRGB right)
        {
            return !(left == right);
        }
        public Color ToColor()
        {
            return this.ToXYZ().ToColor();
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
            double r, g, b;
            r = R / 255;
            g = G / 255;
            b = B / 255;

            r = Math.Pow(r, 2.19921875);
            g = Math.Pow(g, 2.19921875);
            b = Math.Pow(b, 2.19921875);

            r = r * 100;
            g = g * 100;
            b = b * 100;

            return new XYZ(
                (float)(r * 0.57667 + g * 0.18555 + b * 0.18819), // X
                (float)(r * 0.29738 + g * 0.62735 + b * 0.07527), // Y
                (float)(r * 0.02703 + g * 0.07069 + b * 0.99110), // Z
                Alpha);
        }
        public Yxy ToYxy()
        {
            return new Yxy(this.ToXYZ());
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
    public struct Yxy
    {
        private float yy { get; set; }
        private float x { get; set; }
        private float y { get; set; }
        private ushort alpha { get; set; }
        public static readonly Yxy Empty;
        public float YY
        {
            get
            {
                return yy;
            }
            set
            {
                yy = (float)ColorHelper.ValidY(value);
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

        public Yxy(ushort r, ushort g, ushort b, ushort a = 255) : this(new XYZ(r, g, b, a))
        {
        }
        public Yxy(float yy, float x, float y, ushort a = 255) : this()
        {
            YY = yy;
            X = x;
            Y = y;
        }
        public Yxy(Color color) : this(new XYZ(color))
        {
        }

        public Yxy(XYZ xyz)
        {
            alpha = xyz.Alpha;
            yy = xyz.Y;

            if ((xyz.X + xyz.Y + xyz.Z) != 0)
            {
                x = xyz.X / (xyz.X + xyz.Y + xyz.Z);
                y = xyz.Y / (xyz.X + xyz.Y + xyz.Z);
            }
            else
            {
                x = 0;
                y = 0;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}",
                Math.Round(YY, ColorHelper.decimalPlaces),
                Math.Round(X, ColorHelper.decimalPlaces),
                Math.Round(Y, ColorHelper.decimalPlaces));
        }
        public static implicit operator Yxy(Color color)
        {
            return new Yxy(color);
        }

        public static implicit operator Color(Yxy color)
        {
            return color.ToColor();
        }

        public static implicit operator ARGB(Yxy color)
        {
            return color.ToColor();
        }

        public static implicit operator HSB(Yxy color)
        {
            return color.ToHSB();
        }

        public static implicit operator HSL(Yxy color)
        {
            return color.ToHSL();
        }

        public static implicit operator CMYK(Yxy color)
        {
            return color.ToCMYK();
        }


        public static implicit operator XYZ(Yxy color)
        {
            return color.ToXYZ();
        }

        public static bool operator ==(Yxy left, Yxy right)
        {
            return (left.YY == right.YY) && (left.X == right.X) && (left.Y == right.Y) && (left.Alpha == right.Alpha);
        }

        public static bool operator !=(Yxy left, Yxy right)
        {
            return !(left == right);
        }
        public Color ToColor() // fix this
        {
            return ToXYZ().ToColor();
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
        public XYZ ToXYZ()
        {
            double _x, _y, _z;

            if (Y != 0)
            {
                _x = X * (YY / Y);
                _z = (1 - _x - Y) * (YY / Y);
            }
            else
            {
                _x = 0;
                _z = 0;
            }

            _y = YY;

            return new XYZ((float)_x, (float)_y, (float)_z, alpha);
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
    public struct XYZ // D65/2
    {
        private float x { get; set; }
        private float y { get; set; }
        private float z { get; set; }
        private ushort alpha { get; set; }
        public static readonly XYZ Empty;
        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = (float)ColorHelper.ValidXZ(value);
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
                y = (float)ColorHelper.ValidY(value);
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
                z = (float)ColorHelper.ValidXZ(value);
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

        public XYZ(float x, float y, float z, int a = 255) : this()
        {
            X = x;
            Y = y;
            Z = z;
            Alpha = (ushort)a;
        }

        public XYZ(Color color) : this(color.R, color.G, color.B, color.A)
        {
        }

        public XYZ(ushort r, ushort g, ushort b, ushort a = 255)
        {
            double newR = r / 255d;
            double newG = g / 255d;
            double newB = b / 255d;

            if (newR > 0.04045)
                newR = Math.Pow((newR + 0.055) / 1.055, 2.4);
            else
                newR = newR / 12.92;

            if (newG > 0.04045)
                newG = Math.Pow((newG + 0.055) / 1.055, 2.4);
            else
                newG = newG / 12.92;

            if (newB > 0.04045)
                newB = Math.Pow((newB + 0.055) / 1.055, 2.4);
            else
                newB = newB / 12.92;

            newR = newR * 100;
            newG = newG * 100;
            newB = newB * 100;

            x = (float)(newR * 0.4124 + newG * 0.3576 + newB * 0.1805);
            y = (float)(newR * 0.2126 + newG * 0.7152 + newB * 0.0722);
            z = (float)(newR * 0.0193 + newG * 0.1192 + newB * 0.9505);

            alpha = a;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}",
                Math.Round(X, ColorHelper.decimalPlaces),
                Math.Round(Y, ColorHelper.decimalPlaces),
                Math.Round(Z, ColorHelper.decimalPlaces));
        }

        public static implicit operator XYZ(Color color)
        {
            return new XYZ(color);
        }

        public static implicit operator Color(XYZ color)
        {
            return color.ToColor();
        }

        public static implicit operator ARGB(XYZ color)
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
            double r, g, b, x, y, z;
            x = X / 100.0;
            y = Y / 100.0;
            z = Z / 100.0;

            r = (x * 3.2406 + y * -1.5372 + z * -0.4986);
            g = (x * -0.9689 + y * 1.8758 + z * 0.0415);
            b = (x * 0.0557 + y * -0.2040 + z * 1.0570);

            if (r > 0.0031308)
                r = (1.055 * Math.Pow(r, 1 / 2.4)) - 0.055;
            else
                r = 12.92 * r;

            if (g > 0.0031308)
                g = (1.055 * Math.Pow(g, 1 / 2.4)) - 0.055;
            else
                g = 12.92 * g;

            if (b > 0.0031308)
                b = (1.055 * Math.Pow(b, 1 / 2.4)) - 0.055;
            else
                b = 12.92 * b;

            return Color.FromArgb(Alpha,
                (int)Math.Round(r * 255).Clamp(0, 255),
                (int)Math.Round(g * 255).Clamp(0, 255),
                (int)Math.Round(b * 255).Clamp(0, 255));
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
        public static readonly CMYK Empty;
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

        public CMYK(Color color) : this(color.R, color.G, color.B, color.A)
        {

        }

        public CMYK(ushort r, ushort g, ushort b, ushort a = 255)
        {
            if (r == 0 && g == 0 && b == 0)
            {
                c = 0;
                m = 0;
                y = 0;
                k = 1;
            }
            else
            {
                float modifiedR, modifiedG, modifiedB;

                modifiedR = r / 255f;
                modifiedG = g / 255f;
                modifiedB = b / 255f;

                k = 1f - new List<float>() { modifiedR, modifiedG, modifiedB }.Max();
                c = (1f - modifiedR - k) / (1f - k);
                m = (1f - modifiedG - k) / (1f - k);
                y = (1f - modifiedB - k) / (1f - k);
            }
            alpha = a;
        }

        public CMYK(int c, int m, int y, int k, int a = 255) : this()
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
                Math.Round(C100, ColorHelper.decimalPlaces),
                Math.Round(M100, ColorHelper.decimalPlaces),
                Math.Round(Y100, ColorHelper.decimalPlaces),
                Math.Round(K100, ColorHelper.decimalPlaces));
        }

        public static implicit operator CMYK(Color color)
        {
            return new CMYK(color);
        }

        public static implicit operator Color(CMYK color)
        {
            return color.ToColor();
        }

        public static implicit operator ARGB(CMYK color)
        {
            return color.ToColor();
        }

        public static implicit operator XYZ(CMYK color)
        {
            return color.ToXYZ();
        }

        public static implicit operator HSB(CMYK color)
        {
            return color.ToHSB();
        }

        public static implicit operator HSL(CMYK color)
        {
            return color.ToHSL();
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
        public HSB ToHSB()
        {
            return new HSB(this.ToColor());
        }
        public HSL ToHSL()
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
        public static readonly HSL Empty;
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

        public HSL(int h, int s, int l, int a = 255) : this()
        {
            Hue360 = h;
            Saturation100 = s;
            Lightness100 = l;
            alpha = (ushort)a;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}",
                Math.Round(Hue360, ColorHelper.decimalPlaces),
                Math.Round(Saturation100, ColorHelper.decimalPlaces),
                Math.Round(Lightness100, ColorHelper.decimalPlaces));
        }

        public static implicit operator HSL(Color color)
        {
            return new HSL(color);
        }

        public static implicit operator Color(HSL color)
        {
            return color.ToColor();
        }

        public static implicit operator ARGB(HSL color)
        {
            return color.ToColor();
        }

        public static implicit operator CMYK(HSL color)
        {
            return color.ToCMYK();
        }

        public static implicit operator HSB(HSL color)
        {
            return color.ToHSB();
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
            double c, x, m, r = 0, g = 0, b = 0;
            c = (1.0 - Math.Abs(2 * lightness - 1.0)) * saturation;
            x = c * (1.0 - Math.Abs(Hue360 / 60 % 2 - 1.0));
            m = lightness - c / 2.0;

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
            else if (Hue360 <= 360)
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
        public HSB ToHSB()
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

        public static readonly HSB Empty;
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

        public HSB(float h, float s, float b, int a = 255) : this()
        {
            Hue360 = h;
            Saturation100 = s;
            Brightness100 = b;
            Alpha = (ushort)a;
        }

        public HSB(int h, int s, int b, int a = 255) : this()
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
                    // divide by 6 because if you don't hue * 60 = 0-360, but we want hue * 360 = 0-360
                    hue = (((newG - newB) / (newR - min)) % 6) / 6;
                    if (hue < 0) // if its negative add 360. 360/360 = 1
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
                // don't have to worry about dividing by 0 because if max == min the if statement above is true
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
                Math.Round(Hue360, ColorHelper.decimalPlaces),
                Math.Round(Saturation100, ColorHelper.decimalPlaces),
                Math.Round(Brightness100, ColorHelper.decimalPlaces));
        }

        public static implicit operator HSB(Color color)
        {
            return new HSB(color);
        }

        public static implicit operator Color(HSB color)
        {
            return color.ToColor();
        }

        public static implicit operator ARGB(HSB color)
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
            //Console.WriteLine(Hue360);
            float c, x, m, r = 0, g = 0, b = 0;
            c = brightness * saturation;
            x = c * (1 - Math.Abs(Hue360 / 60 % 2 - 1));
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
            else if (Hue360 <= 360)
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
        public static readonly ARGB Empty;
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

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", R, G, B);
        }

        public string ToString(ColorFormat format = ColorFormat.RGB)
        {
            switch (format)
            {
                case ColorFormat.RGB:
                    return string.Format("{0}, {1}, {2}", R, G, B);
                case ColorFormat.ARGB:
                    return string.Format("{0}, {1}, {2}, {3}", A, R, G, B);
            }
            return string.Format("{0}, {1}, {2}", R, G, B);
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
        public static readonly _Color Empty;
        private ushort alpha { get; set; }
        public ARGB argb;
        public HSB hsb;
        public HSL hsl;
        public CMYK cmyk;
        public XYZ xyz;
        public bool isTransparent
        {
            get
            {
                return Alpha < 255;
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
        public _Color(Color color)
        {
            argb = color;
            hsb = color;
            hsl = color;
            cmyk = color;
            alpha = color.A;
            xyz = color;
        }

        public _Color(int r, int g, int b, int a = 255) : this(Color.FromArgb(a, r, g, b))
        {
        }

        public static bool operator ==(_Color left, _Color right)
        {
            return (left.argb.R == right.argb.R) && (left.argb.G == right.argb.G) && (left.argb.B == right.argb.B) && (left.Alpha == right.Alpha);
        }

        public static bool operator !=(_Color left, _Color right)
        {
            return !(left == right);
        }

        public static implicit operator _Color(Color color)
        {
            return new _Color(color);
        }

        public static implicit operator Color(_Color color)
        {
            return Color.FromArgb(color.alpha, color.argb.R, color.argb.G, color.argb.B);
        }

        public string ToHex(ColorFormat format = ColorFormat.RGB)
        {

            return ColorHelper.ColorToHex(argb.R, argb.G, argb.B, argb.A, format);
        }

        public int ToDecimal(ColorFormat format = ColorFormat.RGB)
        {
            return ColorHelper.ColorToDecimal(argb.R, argb.G, argb.B, argb.A, format);
        }

        public XYZ ToXYZ()
        {
            // important to cast to prevent converting r - x, g - y, b - z
            return new XYZ((ushort)argb.R, (ushort)argb.G, (ushort)argb.B, alpha);
        }

        public Yxy ToYxy()
        {
            // important to cast to prevent converting r - Y, g - x, b - y
            return new Yxy((ushort)argb.R, (ushort)argb.G, (ushort)argb.B, alpha);
        }

        public AdobeRGB ToAdobeRGB()
        {
            return new AdobeRGB(new XYZ((ushort)argb.R, (ushort)argb.G, (ushort)argb.B, alpha));
        }

        public void UpdateHSB()
        {
            this.argb = hsb.ToColor();
            this.hsl = hsb;
            this.cmyk = hsb;
            this.xyz = hsb;
        }

        public void UpdateHSL()
        {
            this.argb = hsl.ToColor();
            this.hsb = hsl;
            this.cmyk = hsl;
            this.xyz = hsl;
        }

        public void UpdateCMYK()
        {
            this.argb = cmyk.ToColor();
            this.hsl = cmyk;
            this.hsb = cmyk;
            this.xyz = cmyk;
        }

        public void UpdateARGB()
        {
            this.hsb = argb;
            this.cmyk = argb;
            this.hsb = argb;
            this.xyz = argb;
        }

        public void UpdateXYZ()
        {
            this.hsb = xyz;
            this.cmyk = xyz;
            this.hsb = xyz;
            this.argb = xyz;
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
    public static class ColorHelper
    {
        public static int decimalPlaces { get; set; } = 2;
        public static Regex regHex = new Regex(@"^(?:#|0x)?((?:[0-9A-F]{2}){3})$");
        public static Regex regRGB = new Regex(@"^([1]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])(?:\s|,)+([1]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])(?:\s|,)+([1]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])(?:\s)?$");
        //public static Regex regARGB = new Regex(@"^([1]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])(?:\s|,)+([1]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])(?:\s|,)+([1]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])(?:\s|,)+([1]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])(?:\s)?$");
        public static Regex regHSB = new Regex(@"^([1-2]?[0-9]?[0-9](?:[.][0-9]?[0-9]?[0-9]|)|3[0-5][0-9](?:[.][0-9]?[0-9]?[0-9]|)|360)(?:\s|,)+([0-9]?[0-9](?:[.][0-9]?[0-9]?[0-9]|)|100)(?:\s|,)+([0-9]?[0-9](?:[.][0-9]?[0-9]?[0-9]|)|100)(?:\s)?$");
        public static Regex regCMYK = new Regex(@"^([0-9]?[0-9](?:[.][0-9]?[0-9]?[0-9]|)|100)(?:\s|,)+([0-9]?[0-9](?:[.][0-9]?[0-9]?[0-9]|)|100)(?:\s|,)+([0-9]?[0-9](?:[.][0-9]?[0-9]?[0-9]|)|100)(?:\s|,)+([0-9]?[0-9](?:[.][0-9]?[0-9]?[0-9]|)|100)(?:\s)?$");
        public static string ColorToHex(Color color, ColorFormat format = ColorFormat.RGB)
        {
            return ColorToHex(color.R, color.G, color.B, color.A, format);
        }

        public static string ColorToHex(int r, int g, int b, int a = 255, ColorFormat format = ColorFormat.RGB)
        {
            switch (format)
            {
                default:
                case ColorFormat.RGB:
                    return string.Format("{0:X2}{1:X2}{2:X2}", r, g, b);
                case ColorFormat.ARGB:
                    return string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", a, r, g, b);
            }
        }

        public static int ColorToDecimal(Color color, ColorFormat format = ColorFormat.RGB)
        {
            return ColorToDecimal(color.R, color.G, color.B, color.A, format);
        }

        public static int ColorToDecimal(int r, int g, int b, int a = 255, ColorFormat format = ColorFormat.RGB)
        {
            switch (format)
            {
                default:
                case ColorFormat.RGB:
                    return r << 16 | g << 8 | b;
                case ColorFormat.ARGB:
                    return a << 24 | r << 16 | g << 8 | b;
            }
        }

        public static bool ParseRGB(string input, out Color color)
        {
            Match matchRGB = Regex.Match(input, @"^([1]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])(?:\s|,)+([1]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])(?:\s|,)+([1]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])(?:\s)?$");
            if (matchRGB.Success)
            {
                color = Color.FromArgb(int.Parse(matchRGB.Groups[1].Value), int.Parse(matchRGB.Groups[2].Value), int.Parse(matchRGB.Groups[3].Value));
                return true;
            }
            color = Color.Empty;
            return false;
        }

        public static bool ParseHex(string input, out Color color)
        {
            Match matchHex = Regex.Match(input, @"^(?:#|0x)?((?:[0-9A-Fa-f]{2}){3})$");
            if (matchHex.Success)
            {
                color = HexToColor(matchHex.Groups[1].Value);
                return true;
            }
            color = Color.Empty;
            return false;
        }

        public static bool ParseHSB(string input, out HSB color)
        {
            Match matchHSB = Regex.Match(input, @"^([1-2]?[0-9]?[0-9](?:[.][0-9]?[0-9]?[0-9]|)|3[0-5][0-9](?:[.][0-9]?[0-9]?[0-9]|)|360)(?:\s|,)+([0-9]?[0-9](?:[.][0-9]?[0-9]?[0-9]|)|100)(?:\s|,)+([0-9]?[0-9](?:[.][0-9]?[0-9]?[0-9]|)|100)(?:\s)?$");
            if (matchHSB.Success)
            {
                color = new HSB(float.Parse(matchHSB.Groups[1].Value), float.Parse(matchHSB.Groups[2].Value), float.Parse(matchHSB.Groups[3].Value));
                return true;
            }
            color = HSB.Empty;
            return false;
        }


        public static Color HexToColor(string hex)
        {
            if (string.IsNullOrEmpty(hex))
            {
                return Color.Empty;
            }

            if (hex[0] == '#')
            {
                hex = hex.Remove(0, 1);
            }
            else if (hex.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase))
            {
                hex = hex.Remove(0, 2);
            }
            try
            {
                return ColorTranslator.FromHtml("#" + hex);
            }
            catch
            {
                return Color.Empty;
            }
        }

        public static Color Subtract(Color a, Color b)
        {
            return Color.FromArgb(Math.Abs(a.R - b.R), Math.Abs(a.G - b.G), Math.Abs(a.B - b.B));
        }

        public static Color Invert(Color a)
        {
            if(a.A < 255)
            {
                return Color.FromArgb(Math.Abs(a.A - a.R), Math.Abs(a.A - a.G), Math.Abs(a.A - a.B));
            }
            else
            {
                return Color.FromArgb(255 - a.R, 255 - a.G, 255 - a.B);
            }
        }

        public static Color BackgroundColorBasedOffTextColor(Color text)
        {
            if ((text.R * 0.299 + text.G * 0.587 + text.B * 0.114) > 150 )
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        public static double ValidXZ(double number)
        {
            return number.Clamp(0, 150);
        }

        public static double ValidY(double number)
        {
            return number.Clamp(0, 100);
        }

        public static double ValidRGBColor(double number)
        {
            return number.Clamp(0, 255);
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
