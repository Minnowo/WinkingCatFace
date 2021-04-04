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
    public partial class ZoomDrawingBoard : UserControl
    {
        /// <summary>
        /// this only affects where the image is drawn
        /// you have to take this value into account when passing the dest rect
        /// into the draw image function otherwise it will only have top and left border
        /// </summary>
        public int BorderThickness
        {
            get
            {
                return borderThickness;
            }
            set
            {
                if (value >= 0)
                {
                    borderColorPen.Width = value;
                    borderThickness = value;
                    drawBorder = true;
                }
                else
                {
                    borderColorPen.Width = 0;
                    borderThickness = 0;
                    drawBorder = false;
                }
            }
        }
        private int borderThickness = 1;

        public Color replaceTransparent
        {
            get
            {
                return this.BackColor;
            }
            set
            {
                this.BackColor = value;
            }
        }

        public Color borderColor
        {
            get
            {
                return borderColorPen.Color;
            }
            set
            {
                borderColorPen.Dispose();
                borderColorPen = new Pen(value, borderThickness) { Alignment = PenAlignment.Inset };
            }
        }
        private Pen borderColorPen = new Pen(Color.Black, 1) { Alignment = PenAlignment.Inset };

        public Bitmap image
        {
            get
            {
                return img;
            }
            set
            {
                if (img != null)
                    img.Dispose();

                img = value;
            }
        }
        private Bitmap img;

        private bool drawBorder = true;
        public ZoomDrawingBoard()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();

            this.Size = new Size(50, 50);
        }


        public void _Show(Bitmap img)
        {
            image = img;
            _Show();
        }

        public void _Show()
        {
            this.Show();
            Invalidate();
        }

        public void _Hide()
        {
            this.Hide();
            image = null;
        }

        public void DrawImage(Bitmap img, Rectangle dest, Rectangle source, GraphicsUnit gu = GraphicsUnit.Pixel)
        {
            this.image = new Bitmap(dest.Size.Width, dest.Size.Height);
            using (Graphics g = Graphics.FromImage(image))
            {
                g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.CompositingMode = CompositingMode.SourceOver;
                g.PixelOffsetMode = PixelOffsetMode.Half;

                g.DrawImage(img, dest, source, gu);
            }
            Invalidate();

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.CompositingMode = CompositingMode.SourceOver;

            if (image != null)
            {
                g.DrawImage(image, new Point(borderThickness, borderThickness));
            }

            if (drawBorder)
            {
                // remove antialiasing otherwise it looks really bad cause bleedthrough
                g.SmoothingMode = SmoothingMode.HighSpeed;
                g.DrawRectangle(borderColorPen, new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
            }

            base.OnPaint(e);
        }
    }
}
