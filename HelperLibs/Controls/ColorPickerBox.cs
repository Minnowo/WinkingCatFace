﻿using System;
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
    public partial class ColorPickerBox : ColorPickerBase
    {
        public bool nyah = true;
        public ColorPickerBox()
        {
            InitializeComponent();
        }

        protected override void DrawCrosshair(Graphics g)
        {
            DrawCrosshair(g, Pens.Black, 6);
            DrawCrosshair(g, Pens.White, 5);
        }

        private void DrawCrosshair(Graphics g, Pen pen, int size)
        {
            g.DrawEllipse(pen, new Rectangle(new Point(lastClicked.X - size, lastClicked.Y - size), new Size(size * 2, size * 2)));
        }

        // slider controls hue
        // x = saturation 0 -> 100
        // y = brightness 100 -> 0
        protected override void DrawHSBHue()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                HSB start = new HSB((int)selectedColor.hsb.Hue360, 0, 0, selectedColor.argb.A);
                HSB end = new HSB((int)selectedColor.hsb.Hue360, 100, 0, selectedColor.argb.A);

                for (int y = 0; y < clientHeight; y++)
                {
                    start.Brightness = end.Brightness = (float)(1.0 - ((double)y / (clientHeight)));

                    using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, clientWidth, 1), start, end, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(brush, new Rectangle(0, y, clientWidth, 1));
                    }
                }
            }
        }

        // slider controls saturation
        // x = hue 0 -> 360
        // y = brightness 100 -> 0
        protected override void DrawHSBSaturation()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                HSB start = new HSB(0, (int)selectedColor.hsb.Saturation100, 100, selectedColor.argb.A);
                HSB end = new HSB(0, (int)selectedColor.hsb.Saturation100, 0, selectedColor.argb.A);

                for (int x = 0; x < clientWidth; x++)
                {
                    start.Hue = end.Hue = (float)((double)x / (clientHeight));

                    using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 1, clientHeight), start, end, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(brush, new Rectangle(x, 0, 1, clientHeight));
                    }
                }
            }
        }

        // slider controls brightness
        // x = hue 0 -> 360
        // y = saturation 100 -> 0
        protected override void DrawHSBBrightness()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                HSB start = new HSB(0, 100, (int)selectedColor.hsb.Brightness100, selectedColor.argb.A);
                HSB end = new HSB(0, 0, (int)selectedColor.hsb.Brightness100, selectedColor.argb.A);

                for (int x = 0; x < clientWidth; x++)
                {
                    start.Hue = end.Hue = (float)((double)x / (clientHeight));

                    using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 1, clientHeight), start, end, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(brush, new Rectangle(x, 0, 1, clientHeight));
                    }
                }
            }
        }

        // slider controls red
        // x = blue 0 -> 255
        // y = green 255 -> 0
        protected override void DrawRed()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                ARGB start = new ARGB(selectedColor.argb.A, (int)selectedColor.argb.R, 0, 0);
                ARGB end = new ARGB(selectedColor.argb.A, (int)selectedColor.argb.R, 0, 255);

                for (int y = 0; y < clientHeight; y++)
                {
                    start.G = end.G = (int)Math.Round(255 - ((double)y / (clientHeight)) * 255);

                    using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, clientWidth, 1), start, end, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(brush, new Rectangle(0, y, clientWidth, 1));
                    }
                }
            }
        }

        // slider controls green
        // x = red 255 -> 0
        // y = blue 0 -> 255
        protected override void DrawGreen()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                ARGB start = new ARGB(selectedColor.argb.A, 0, (int)selectedColor.argb.G, 0);
                ARGB end = new ARGB(selectedColor.argb.A, 0, (int)selectedColor.argb.G, 255);

                for (int y = 0; y < clientHeight; y++)
                {
                    start.R = end.R = (int)Math.Round(255 - ((double)y / (clientHeight)) * 255);

                    using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, clientWidth, 1), start, end, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(brush, new Rectangle(0, y, clientWidth, 1));
                    }
                }
            }
        }

        // slider controls blue
        // x = red 0 -> 255
        // y = green 255 -> 0
        protected override void DrawBlue()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                ARGB start = new ARGB(selectedColor.argb.A, 0, 0, (int)selectedColor.argb.B);
                ARGB end = new ARGB(selectedColor.argb.A, 255, 0, (int)selectedColor.argb.B);

                for (int y = 0; y < clientHeight; y++)
                {
                    start.G = end.G = (int)Math.Round(255 - ((double)y / (clientHeight)) * 255);

                    using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, clientWidth, 1), start, end, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(brush, new Rectangle(0, y, clientWidth, 1));
                    }
                }
            }
        }

        // slider controls hue
        // x = saturation 0 -> 100
        // y = Lightness 100 -> 0
        protected override void DrawHSLHue()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                HSL start = new HSL((int)selectedColor.hsl.Hue360, 0, 0, selectedColor.argb.A);
                HSL end = new HSL((int)selectedColor.hsl.Hue360, 100, 0, selectedColor.argb.A);

                for (int y = 0; y < clientHeight; y++)
                {
                    start.Lightness = end.Lightness = (float)(1.0 - ((double)y / (clientHeight)));

                    using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, clientWidth, 1), start, end, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(brush, new Rectangle(0, y, clientWidth, 1));
                    }
                }
            }
        }

        // slider controls saturation
        // x = hue 0 -> 360
        // y = Lightness 100 -> 0
        protected override void DrawHSLSaturation()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                HSL start = new HSL(0, (int)selectedColor.hsl.Saturation100, 50, selectedColor.argb.A);
                HSL end = new HSL(360, (int)selectedColor.hsl.Saturation100, 50, selectedColor.argb.A);

                for (int x = 0; x < clientWidth; x++)
                {
                    start.Hue = end.Hue = (float)((double)x / (clientHeight));

                    using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 1, clientHeight), start, end, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(brush, new Rectangle(x, 0, 1, clientHeight));
                    }
                }

                // in order to have the top lighter and the bottom darker
                ColorBlend fadeBlend = new ColorBlend();
                fadeBlend.Colors = new Color[] { Color.White, Color.Transparent, Color.Black };
                fadeBlend.Positions = new float[] { 0, 0.5f, 1 };

                using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 1, clientHeight), Color.White, Color.White, LinearGradientMode.Vertical))
                {
                    brush.InterpolationColors = fadeBlend;
                    g.FillRectangle(brush, new Rectangle(0, 0, clientWidth, clientHeight));
                }
            }
        }

        // slider controls lightness
        // x = hue 0 -> 360
        // y = saturation 100 -> 0
        protected override void DrawHSLLightness()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                HSL start = new HSL(0, 100, (int)selectedColor.hsl.Lightness100, selectedColor.argb.A);
                HSL end = new HSL(0, 0, (int)selectedColor.hsl.Lightness100, selectedColor.argb.A);

                for (int x = 0; x < clientWidth; x++)
                {
                    start.Hue = end.Hue = (float)((double)x / (clientHeight));

                    using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 1, clientHeight), start, end, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(brush, new Rectangle(x, 0, 1, clientHeight));
                    }
                }
            }
        }

        protected override void DrawXYZ()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                XYZ start = new XYZ(selectedColor.xyz.X, 0f, 0f, selectedColor.argb.A);
                XYZ end = new XYZ(selectedColor.xyz.X, 100f, 0f, selectedColor.argb.A);

                for (int y = 0; y < clientHeight; y++)
                {
                    //start.Z = end.Z = (float)(150 * (1.0 - ((double)y / clientHeight)));
                    start.Z = end.Z = (float)(150 * (1.0 - ((double)y / clientHeight)));
                    //start.Z = end.Z = (float)(150 - (150.0 * ((double)y / clientHeight)));

                    using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, clientWidth, 1), start, end, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(brush, new Rectangle(0, y, clientWidth, 1));
                    }
                }
            }
        }
    }
}