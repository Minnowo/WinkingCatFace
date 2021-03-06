using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WinkingCat.HelperLibs
{
    public partial class ColorPickerSlider : ColorPickerBase
    {
        public ColorPickerSlider()
        {
            InitializeComponent();
            Size = new Size(30, 258);
        }

        protected override void DrawCrosshair(Graphics g)
        {
            DrawCrosshair(g, Pens.Black, 3, 11);
            DrawCrosshair(g, Pens.White, 4, 9);
        }

        private void DrawCrosshair(Graphics g, Pen pen, int offset, int height)
        {
            g.DrawRectangle(pen, new Rectangle(offset, lastClicked.Y - (height / 2), clientWidth - (offset * 2), height));
        }

        protected override void DrawHSBHue()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                HSB color = new HSB(0, 100, 100, SelectedColor.argb.A);

                for (int y = 0; y < clientHeight; y++)
                {
                    color.Hue = (float)(1.0 - ((double)y / clientHeight));

                    using (Pen pen = new Pen(color))
                    {
                        g.DrawLine(pen, 0, y, clientWidth, y);
                    }
                }
            }
        }

        protected override void DrawHSBSaturation()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                HSB start = new HSB((int)SelectedColor.hsb.Hue360, 100, (int)SelectedColor.hsb.Brightness100, SelectedColor.argb.A);
                HSB end = new HSB((int)SelectedColor.hsb.Hue360, 0, (int)SelectedColor.hsb.Brightness100, SelectedColor.argb.A);

                using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, clientWidth, clientHeight), start, end, LinearGradientMode.Vertical))
                {
                    g.FillRectangle(brush, new Rectangle(0, 0, clientWidth, clientHeight));
                }
            }
        }

        protected override void DrawHSBBrightness()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                HSB start = new HSB((int)SelectedColor.hsb.Hue360, (int)SelectedColor.hsb.Saturation100, 100, SelectedColor.argb.A);
                HSB end = new HSB((int)SelectedColor.hsb.Hue360, (int)SelectedColor.hsb.Saturation100, 0, SelectedColor.argb.A);

                using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, clientWidth, clientHeight), start, end, LinearGradientMode.Vertical))
                {
                    g.FillRectangle(brush, new Rectangle(0, 0, clientWidth, clientHeight));
                }
            }
        }

        protected override void DrawRed()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                ARGB start = new ARGB(SelectedColor.argb.A, 255, SelectedColor.argb.G, SelectedColor.argb.B);
                ARGB end = new ARGB(SelectedColor.argb.A, 0, SelectedColor.argb.G, SelectedColor.argb.B);

                using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, clientWidth, clientHeight), start, end, LinearGradientMode.Vertical))
                {
                    g.FillRectangle(brush, new Rectangle(0, 0, clientWidth, clientHeight));
                }
            }
        }

        protected override void DrawGreen()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                ARGB start = new ARGB(SelectedColor.argb.A, SelectedColor.argb.R, 255, SelectedColor.argb.B);
                ARGB end = new ARGB(SelectedColor.argb.A, SelectedColor.argb.R, 0, SelectedColor.argb.B);

                using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, clientWidth, clientHeight), start, end, LinearGradientMode.Vertical))
                {
                    g.FillRectangle(brush, new Rectangle(0, 0, clientWidth, clientHeight));
                }
            }
        }

        protected override void DrawBlue()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                ARGB start = new ARGB(SelectedColor.argb.A, SelectedColor.argb.R, SelectedColor.argb.G, 255);
                ARGB end = new ARGB(SelectedColor.argb.A, SelectedColor.argb.R, SelectedColor.argb.G, 0);

                using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, clientWidth, clientHeight), start, end, LinearGradientMode.Vertical))
                {
                    g.FillRectangle(brush, new Rectangle(0, 0, clientWidth, clientHeight));
                }
            }
        }

        protected override void DrawHSLHue()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                HSB color = new HSB(0, 100, 100, SelectedColor.argb.A);

                for (int y = 0; y < clientHeight; y++)
                {
                    color.Hue = (float)(1.0 - ((double)y / clientHeight));

                    using (Pen pen = new Pen(color))
                    {
                        g.DrawLine(pen, 0, y, clientWidth, y);
                    }
                }
            }
        }

        protected override void DrawHSLSaturation()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                HSL start = new HSL((int)SelectedColor.hsl.Hue360, 100, (int)SelectedColor.hsl.Lightness100, SelectedColor.argb.A);
                HSL end = new HSL((int)SelectedColor.hsl.Hue360, 0, (int)SelectedColor.hsl.Lightness100, SelectedColor.argb.A);

                using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, clientWidth, clientHeight), start, end, LinearGradientMode.Vertical))
                {
                    g.FillRectangle(brush, new Rectangle(0, 0, clientWidth, clientHeight));
                }
            }
        }

        protected override void DrawHSLLightness()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                HSL start = new HSL((int)SelectedColor.hsl.Hue360, (int)SelectedColor.hsl.Saturation100, 100, SelectedColor.argb.A);
                HSL end = new HSL((int)SelectedColor.hsl.Hue360, (int)SelectedColor.hsl.Saturation100, 0, SelectedColor.argb.A);

                using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, clientWidth, clientHeight), start, end, LinearGradientMode.Vertical))
                {
                    g.FillRectangle(brush, new Rectangle(0, 0, clientWidth, clientHeight));
                }
            }
        }

        protected override void DrawXYZ()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                XYZ color = new XYZ(0f, 100f, 150f, SelectedColor.argb.A);

                for (int y = 0; y < clientHeight; y++)
                {
                    color.X = (float)(150 * (1.0 - ((double)y / clientHeight)));
                    //color.X = (float)(150 - (150.0 * ((double)y / clientHeight)));

                    using (Pen pen = new Pen(color))
                    {
                        g.DrawLine(pen, 0, y, clientWidth, y);
                    }
                }
            }
        }
    }
}
