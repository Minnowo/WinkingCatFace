using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using WinkingCat.HelperLibs;

namespace WinkingCat.Controls
{
    public abstract partial class ColorPickerBase : UserControl
    {
        public event ColorEventHandler ColorChanged;

        /// <summary>
        /// Is the crosshair visible.
        /// </summary>
        public bool CrosshairVisible
        {
            get
            {
                return crosshairVisible;
            }
            set
            {
                crosshairVisible = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The current color.
        /// </summary>
        public COLOR SelectedColor
        {
            get
            {
                return selectedColor;
            }

            set
            {
                selectedColor = value;

                if (this is ColorPickerBox)
                {
                    SetBoxMarker();
                }
                else
                {
                    SetSliderMarker();
                }

                Invalidate();
            }
        }

        /// <summary>
        /// The color space being drawn.
        /// </summary>
        public ColorSpaceDrawStyle DrawStyle
        {
            get
            {
                return drawStyle;
            }

            set
            {

                drawStyle = value;

                if (this is ColorPickerBox)
                {
                    SetBoxMarker();
                }
                else
                {
                    SetSliderMarker();
                }

                Invalidate();
            }
        }

        protected ColorSpaceDrawStyle drawStyle;
        protected COLOR selectedColor;
        protected COLOR absoluteColor = Color.White;
        protected Point lastClicked;
        protected int clientWidth, clientHeight;
        protected bool isLeftClicking;
        protected bool crosshairVisible = false;
        protected Timer mouseMoveTimer;
        protected Bitmap bmp;

        protected void Init()
        {
            SuspendLayout();
            this.DoubleBuffered = true;

            this.clientWidth = ClientRectangle.Width;
            this.clientHeight = ClientRectangle.Height;

            this.selectedColor = Color.Red;
            this.drawStyle = ColorSpaceDrawStyle.HSBHue;

            bmp = new Bitmap(clientWidth, clientHeight, PixelFormat.Format32bppArgb);

            mouseMoveTimer = new Timer();
            mouseMoveTimer.Interval = 10;
            mouseMoveTimer.Tick += new EventHandler(MouseMoveTimer_Tick);

            ClientSizeChanged += new EventHandler(ClientSizeChanged_Event);
            MouseDown += new MouseEventHandler(MouseDown_Event);
            MouseEnter += new EventHandler(MouseEnter_Event);
            MouseUp += new MouseEventHandler(MouseUp_Event);
            Paint += new PaintEventHandler(Paint_Event);

            ResumeLayout(false);
        }

        public ColorPickerBase()
        {
            InitializeComponent();
            Init();
        }

        private void MouseMoveTimer_Tick(object sender, EventArgs e)
        {
            if (!isLeftClicking)
                return;

            Point mousePosition = PointToClient(MousePosition);

            if (lastClicked == mousePosition)
                return;

            lastClicked = GetPoint(mousePosition);

            if (this is ColorPickerBox)
            {
                GetBoxColor();
            }
            else
            {
                GetSliderColor();
            }

            OnColorChanged();
            Refresh();
        }

        private void ClientSizeChanged_Event(object sender, EventArgs e)
        {
            clientWidth = ClientRectangle.Width;
            clientHeight = ClientRectangle.Height;
            bmp.Dispose();
            bmp = new Bitmap(clientWidth, clientHeight, PixelFormat.Format32bppArgb);
            DrawColors();
        }

        private void MouseDown_Event(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                crosshairVisible = true;
                isLeftClicking = true;
                mouseMoveTimer.Start();
            }
        }

        private void MouseUp_Event(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isLeftClicking = false;
                mouseMoveTimer.Stop();
            }
        }

        private void MouseEnter_Event(object sender, EventArgs e)
        {

        }

        public static Bitmap CreateCheckerPattern(int width, int height, Color checkerColor1, Color checkerColor2)
        {
            Bitmap bmp = new Bitmap(width * 2, height * 2);

            using (Graphics g = Graphics.FromImage(bmp))
            using (Brush brush1 = new SolidBrush(checkerColor1))
            using (Brush brush2 = new SolidBrush(checkerColor2))
            {
                g.FillRectangle(brush1, 0, 0, width, height);
                g.FillRectangle(brush1, width, height, width, height);
                g.FillRectangle(brush2, width, 0, width, height);
                g.FillRectangle(brush2, 0, height, width, height);
            }

            return bmp;
        }

        private void Paint_Event(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (!isLeftClicking)
            {
                if (selectedColor.isTransparent)
                {
                    if (bmp != null)
                        bmp.Dispose();

                    bmp = new Bitmap(clientWidth, clientHeight, PixelFormat.Format24bppRgb);

                    ImageProcessor.DrawCheckers(bmp, 32,
                        SystemColors.ControlLight,
                        SystemColors.ControlLightLight);

                }
                DrawColors();
            }

            g.DrawImage(bmp, ClientRectangle);

            if (CrosshairVisible)
            {
                DrawCrosshair(g);
            }
        }

        protected void DrawColors()
        {
            switch (drawStyle)
            {
                // HSB Color Space
                case ColorSpaceDrawStyle.HSBHue:
                    DrawHSBHue();
                    break;
                case ColorSpaceDrawStyle.HSBSaturation:
                    DrawHSBSaturation();
                    break;
                case ColorSpaceDrawStyle.HSBBrightness:
                    DrawHSBBrightness();
                    break;

                // HSL Color Space
                case ColorSpaceDrawStyle.HSLHue:
                    DrawHSLHue();
                    break;
                case ColorSpaceDrawStyle.HSLSaturation:
                    DrawHSLSaturation();
                    break;
                case ColorSpaceDrawStyle.HSLLightness:
                    DrawHSLLightness();
                    break;

                // RGB Color Space
                case ColorSpaceDrawStyle.Red:
                    DrawRed();
                    break;
                case ColorSpaceDrawStyle.Green:
                    DrawGreen();
                    break;
                case ColorSpaceDrawStyle.Blue:
                    DrawBlue();
                    break;
            }
        }
        protected abstract void DrawCrosshair(Graphics g);

        protected abstract void DrawHSBHue();
        protected abstract void DrawHSBSaturation();
        protected abstract void DrawHSBBrightness();

        protected abstract void DrawHSLHue();
        protected abstract void DrawHSLSaturation();
        protected abstract void DrawHSLLightness();

        protected abstract void DrawRed();
        protected abstract void DrawGreen();
        protected abstract void DrawBlue();



        protected void GetBoxColor()
        {
            switch (DrawStyle)
            {
                // HSB Color Space
                case ColorSpaceDrawStyle.HSBHue:
                    selectedColor.HSB.Saturation = (float)lastClicked.X / clientWidth;
                    selectedColor.HSB.Brightness = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSB();
                    break;
                case ColorSpaceDrawStyle.HSBSaturation:
                    selectedColor.HSB.Hue = (float)lastClicked.X / clientWidth;
                    selectedColor.HSB.Brightness = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSB();
                    break;
                case ColorSpaceDrawStyle.HSBBrightness:
                    selectedColor.HSB.Hue = (float)lastClicked.X / clientWidth;
                    selectedColor.HSB.Saturation = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSB();
                    break;

                // HSL Color Space
                case ColorSpaceDrawStyle.HSLHue:
                    selectedColor.HSL.Saturation = (float)lastClicked.X / clientWidth;
                    selectedColor.HSL.Lightness = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSL();
                    break;
                case ColorSpaceDrawStyle.HSLSaturation:
                    selectedColor.HSL.Hue = (float)lastClicked.X / clientWidth;
                    selectedColor.HSL.Lightness = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSL();
                    break;
                case ColorSpaceDrawStyle.HSLLightness:
                    selectedColor.HSL.Hue = (float)lastClicked.X / clientWidth;
                    selectedColor.HSL.Saturation = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSL();
                    break;

                // RGB Color Space
                case ColorSpaceDrawStyle.Red:
                    selectedColor.ARGB.B = (byte)Math.Round(255 * (double)lastClicked.X / (clientWidth));
                    selectedColor.ARGB.G = (byte)Math.Round(255 * (1.0 - ((double)lastClicked.Y / (clientHeight))));
                    selectedColor.UpdateARGB();
                    break;
                case ColorSpaceDrawStyle.Green:
                    selectedColor.ARGB.B = (byte)Math.Round(255 * (double)lastClicked.X / (clientWidth));
                    selectedColor.ARGB.R = (byte)Math.Round(255 * (1.0 - ((double)lastClicked.Y / (clientHeight))));
                    selectedColor.UpdateARGB();
                    break;
                case ColorSpaceDrawStyle.Blue:
                    selectedColor.ARGB.R = (byte)Math.Round(255 * (double)lastClicked.X / (clientWidth));
                    selectedColor.ARGB.G = (byte)Math.Round(255 * (1.0 - ((double)lastClicked.Y / (clientHeight))));
                    selectedColor.UpdateARGB();
                    break;
            }
        }

        protected void GetSliderColor()
        {
            switch (DrawStyle)
            {
                // HSB Color Space
                case ColorSpaceDrawStyle.HSBHue:
                    selectedColor.HSB.Hue = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSB();
                    break;
                case ColorSpaceDrawStyle.HSBSaturation:
                    selectedColor.HSB.Saturation = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSB();
                    break;
                case ColorSpaceDrawStyle.HSBBrightness:
                    selectedColor.HSB.Brightness = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSB();
                    break;

                // HSL Color Space
                case ColorSpaceDrawStyle.HSLHue:
                    selectedColor.HSL.Hue = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSL();
                    break;
                case ColorSpaceDrawStyle.HSLSaturation:
                    selectedColor.HSL.Saturation = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSL();
                    break;
                case ColorSpaceDrawStyle.HSLLightness:
                    selectedColor.HSL.Lightness = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSL();
                    break;

                // RGB Color Space
                case ColorSpaceDrawStyle.Red:
                    selectedColor.ARGB.R = (byte)(255 - Math.Round(255 * (double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateARGB();
                    break;
                case ColorSpaceDrawStyle.Green:
                    selectedColor.ARGB.G = (byte)(255 - Math.Round(255 * (double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateARGB();
                    break;
                case ColorSpaceDrawStyle.Blue:
                    selectedColor.ARGB.B = (byte)(255 - Math.Round(255 * (double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateARGB();
                    break;
            }
        }

        protected void SetBoxMarker()
        {
            switch (drawStyle)
            {
                // HSB Color Space
                case ColorSpaceDrawStyle.HSBHue:
                    lastClicked.X = (int)Math.Round(clientWidth * selectedColor.HSB.Saturation);
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - selectedColor.HSB.Brightness));
                    break;
                case ColorSpaceDrawStyle.HSBSaturation:
                    lastClicked.X = (int)Math.Round(clientWidth * selectedColor.HSB.Hue);
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - selectedColor.HSB.Brightness));
                    break;
                case ColorSpaceDrawStyle.HSBBrightness:
                    lastClicked.X = (int)Math.Round(clientWidth * selectedColor.HSB.Hue);
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - selectedColor.HSB.Saturation));
                    break;

                // HSL Color Space
                case ColorSpaceDrawStyle.HSLHue:
                    lastClicked.X = (int)Math.Round(clientWidth * selectedColor.HSL.Saturation);
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - selectedColor.HSL.Lightness));
                    break;
                case ColorSpaceDrawStyle.HSLSaturation:
                    lastClicked.X = (int)Math.Round(clientWidth * selectedColor.HSL.Hue);
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - selectedColor.HSL.Lightness));
                    break;
                case ColorSpaceDrawStyle.HSLLightness:
                    lastClicked.X = (int)Math.Round(clientWidth * selectedColor.HSL.Hue);
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - selectedColor.HSL.Saturation));
                    break;

                // RGB Color Space
                case ColorSpaceDrawStyle.Red:
                    lastClicked.X = (int)Math.Round(clientWidth * (double)selectedColor.ARGB.B / 255);
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - ((double)selectedColor.ARGB.G / 255)));
                    break;
                case ColorSpaceDrawStyle.Green:
                    lastClicked.X = (int)Math.Round(clientWidth * (double)selectedColor.ARGB.B / 255);
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - ((double)selectedColor.ARGB.R / 255)));
                    break;
                case ColorSpaceDrawStyle.Blue:
                    lastClicked.X = (int)Math.Round(clientWidth * (double)selectedColor.ARGB.R / 255);
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - ((double)selectedColor.ARGB.G / 255)));
                    break;
            }

            lastClicked = GetPoint(lastClicked);
        }

        protected void SetSliderMarker()
        {
            switch (DrawStyle)
            {
                // HSB Color Space
                case ColorSpaceDrawStyle.HSBHue:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * SelectedColor.HSB.Hue));
                    break;
                case ColorSpaceDrawStyle.HSBSaturation:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * SelectedColor.HSB.Saturation));
                    break;
                case ColorSpaceDrawStyle.HSBBrightness:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * SelectedColor.HSB.Brightness));
                    break;

                // HSL Color Space
                case ColorSpaceDrawStyle.HSLHue:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * SelectedColor.HSL.Hue));
                    break;
                case ColorSpaceDrawStyle.HSLSaturation:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * SelectedColor.HSL.Saturation));
                    break;
                case ColorSpaceDrawStyle.HSLLightness:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * SelectedColor.HSL.Lightness));
                    break;

                // RGB Color Space
                case ColorSpaceDrawStyle.Red:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * (double)SelectedColor.ARGB.R / 255));
                    break;
                case ColorSpaceDrawStyle.Green:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * (double)SelectedColor.ARGB.G / 255));
                    break;
                case ColorSpaceDrawStyle.Blue:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * (double)SelectedColor.ARGB.B / 255));
                    break;
            }

            lastClicked = GetPoint(lastClicked);
        }

        protected Point GetPoint(Point point)
        {
            return new Point(point.X.Clamp(0, clientWidth), point.Y.Clamp(0, clientHeight));
        }

        private void OnColorChanged()
        {
            //Console.WriteLine(this.absoluteColor.argb);
            if (ColorChanged != null)
                ColorChanged(this, new ColorEventArgs(this.selectedColor, this.drawStyle));
        }
    }
}
