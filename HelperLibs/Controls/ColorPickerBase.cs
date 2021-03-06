using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System;

namespace WinkingCat.HelperLibs
{
    public abstract partial class ColorPickerBase : UserControl
    {
        public event ColorEventHandler ColorChanged;

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

        public _Color SelectedColor
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

        public _Color AbsoluteColor
        {
            get
            {
                return absoluteColor;
            }

            private set
            {
                absoluteColor = value;

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

        public DrawStyles DrawStyle
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

        protected DrawStyles drawStyle;
        protected _Color selectedColor;
        protected _Color absoluteColor = Color.White;
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
            this.drawStyle = DrawStyles.HSBHue;

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
            Point mousePosition = PointToClient(MousePosition);

            if (isLeftClicking && lastClicked != mousePosition)
            {
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
            switch (e.Button)
            {
                case MouseButtons.Left:
                    crosshairVisible = true;
                    isLeftClicking = true;
                    mouseMoveTimer.Start();

                    break;
                case MouseButtons.Right:
                    break;
                case MouseButtons.Middle:
                    break;
            }
        }

        private void MouseUp_Event(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    isLeftClicking = false;
                    mouseMoveTimer.Stop();
                    break;
                case MouseButtons.Right:
                    break;
                case MouseButtons.Middle:
                    break;
            }
        }
        private void MouseEnter_Event(object sender, EventArgs e)
        {

        }
        private void Paint_Event(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (!isLeftClicking)
            {
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
                case DrawStyles.HSBHue:
                    DrawHSBHue();
                    break;
                case DrawStyles.HSBSaturation:
                    DrawHSBSaturation();
                    break;
                case DrawStyles.HSBBrightness:
                    DrawHSBBrightness();
                    break;

                // HSL Color Space
                case DrawStyles.HSLHue:
                    DrawHSLHue();
                    break;
                case DrawStyles.HSLSaturation:
                    DrawHSLSaturation();
                    break;
                case DrawStyles.HSLLightness:
                    DrawHSLLightness();
                    break;

                // RGB Color Space
                case DrawStyles.Red:
                    DrawRed();
                    break;
                case DrawStyles.Green:
                    DrawGreen();
                    break;
                case DrawStyles.Blue:
                    DrawBlue();
                    break;

                case DrawStyles.xyz:
                    DrawXYZ();
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

        protected abstract void DrawXYZ();



        protected void GetBoxColor()
        {
            switch (DrawStyle)
            {
                // HSB Color Space
                case DrawStyles.HSBHue:
                    selectedColor.hsb.Saturation = (float)lastClicked.X / clientWidth;
                    selectedColor.hsb.Brightness = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSB();
                    break;
                case DrawStyles.HSBSaturation:
                    selectedColor.hsb.Hue = (float)lastClicked.X / clientWidth;
                    selectedColor.hsb.Brightness = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSB();
                    break;
                case DrawStyles.HSBBrightness:
                    selectedColor.hsb.Hue = (float)lastClicked.X / clientWidth;
                    selectedColor.hsb.Saturation = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSB();
                    break;

                // HSL Color Space
                case DrawStyles.HSLHue:
                    selectedColor.hsl.Saturation = (float)lastClicked.X / clientWidth;
                    selectedColor.hsl.Lightness = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSL();
                    break;
                case DrawStyles.HSLSaturation:
                    selectedColor.hsl.Hue = (float)lastClicked.X / clientWidth;
                    selectedColor.hsl.Lightness = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSL();
                    break;
                case DrawStyles.HSLLightness:
                    selectedColor.hsl.Hue = (float)lastClicked.X / clientWidth;
                    selectedColor.hsl.Saturation = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSL();
                    break;

                // RGB Color Space
                case DrawStyles.Red:
                    selectedColor.argb.B = (int)Math.Round(255 * (double)lastClicked.X / (clientWidth));
                    selectedColor.argb.G = (int)Math.Round(255 * (1.0 - ((double)lastClicked.Y / (clientHeight))));
                    selectedColor.UpdateARGB();
                    break;
                case DrawStyles.Green:
                    selectedColor.argb.B = (int)Math.Round(255 * (double)lastClicked.X / (clientWidth));
                    selectedColor.argb.R = (int)Math.Round(255 * (1.0 - ((double)lastClicked.Y / (clientHeight))));
                    selectedColor.UpdateARGB();
                    break;
                case DrawStyles.Blue:
                    selectedColor.argb.R = (int)Math.Round(255 * (double)lastClicked.X / (clientWidth));
                    selectedColor.argb.G = (int)Math.Round(255 * (1.0 - ((double)lastClicked.Y / (clientHeight))));
                    selectedColor.UpdateARGB();
                    break;

                case DrawStyles.xyz:
                    // get the pixel from the bitmap to display to the user, but keep the selected color as the calculation so the cursor moves correctly
                    absoluteColor = bmp.GetPixel(lastClicked.X.Clamp(0, clientWidth - 1), lastClicked.Y.Clamp(0, clientHeight - 1));
                    absoluteColor.UpdateXYZ();

                    // this isn't very accurate to the color under the cursor 
                    selectedColor.xyz.Y = (float)(100.0 * ((double)lastClicked.X / clientWidth));
                    selectedColor.xyz.Z = (float)(150.0 * (1.0 - ((double)lastClicked.Y / clientHeight)));
                    selectedColor.UpdateXYZ();
                    break;
            }
        }

        protected void GetSliderColor()
        {
            switch (DrawStyle)
            {
                // HSB Color Space
                case DrawStyles.HSBHue:
                    selectedColor.hsb.Hue = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSB();
                    break;
                case DrawStyles.HSBSaturation:
                    selectedColor.hsb.Saturation = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSB();
                    break;
                case DrawStyles.HSBBrightness:
                    selectedColor.hsb.Brightness = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSB();
                    break;

                // HSL Color Space
                case DrawStyles.HSLHue:
                    selectedColor.hsl.Hue = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSL();
                    break;
                case DrawStyles.HSLSaturation:
                    selectedColor.hsl.Saturation = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSL();
                    break;
                case DrawStyles.HSLLightness:
                    selectedColor.hsl.Lightness = (float)(1.0 - ((double)lastClicked.Y / clientHeight));
                    selectedColor.UpdateHSL();
                    break;

                // RGB Color Space
                case DrawStyles.Red:
                    selectedColor.argb.R = 255 - (int)Math.Round(255 * (double)lastClicked.Y / clientHeight);
                    selectedColor.UpdateARGB();
                    break;
                case DrawStyles.Green:
                    selectedColor.argb.G = 255 - (int)Math.Round(255 * (double)lastClicked.Y / clientHeight);
                    selectedColor.UpdateARGB();
                    break;
                case DrawStyles.Blue:
                    selectedColor.argb.B = 255 - (int)Math.Round(255 * (double)lastClicked.Y / clientHeight);
                    selectedColor.UpdateARGB();
                    break;

                case DrawStyles.xyz:
                    // just don't even touch the absolute color, let the user move the box slider again to get the value off of white

                    selectedColor.xyz.X = (float)(150 - (150.0 * ((double)lastClicked.Y / clientHeight)));
                    selectedColor.UpdateXYZ();
                    break;
            }
        }

        protected void SetBoxMarker()
        {
            switch (drawStyle)
            {
                // HSB Color Space
                case DrawStyles.HSBHue:
                    lastClicked.X = (int)Math.Round(clientWidth * selectedColor.hsb.Saturation);
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - selectedColor.hsb.Brightness));
                    break;
                case DrawStyles.HSBSaturation:
                    lastClicked.X = (int)Math.Round(clientWidth * selectedColor.hsb.Hue);
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - selectedColor.hsb.Brightness));
                    break;
                case DrawStyles.HSBBrightness:
                    lastClicked.X = (int)Math.Round(clientWidth * selectedColor.hsb.Hue);
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - selectedColor.hsb.Saturation));
                    break;

                // HSL Color Space
                case DrawStyles.HSLHue:
                    lastClicked.X = (int)Math.Round(clientWidth * selectedColor.hsl.Saturation);
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - selectedColor.hsl.Lightness));
                    break;
                case DrawStyles.HSLSaturation:
                    lastClicked.X = (int)Math.Round(clientWidth * selectedColor.hsl.Hue);
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - selectedColor.hsl.Lightness));
                    break;
                case DrawStyles.HSLLightness:
                    lastClicked.X = (int)Math.Round(clientWidth * selectedColor.hsl.Hue);
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - selectedColor.hsl.Saturation));
                    break;

                // RGB Color Space
                case DrawStyles.Red:
                    lastClicked.X = (int)Math.Round(clientWidth * (double)selectedColor.argb.B / 255);
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - ((double)selectedColor.argb.G / 255)));
                    break;
                case DrawStyles.Green:
                    lastClicked.X = (int)Math.Round(clientWidth * (double)selectedColor.argb.B / 255);
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - ((double)selectedColor.argb.R / 255)));
                    break;
                case DrawStyles.Blue:
                    lastClicked.X = (int)Math.Round(clientWidth * (double)selectedColor.argb.R / 255);
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - ((double)selectedColor.argb.G / 255)));
                    break;

                case DrawStyles.xyz:
                    lastClicked.X = (int)Math.Round(clientWidth * ((double)selectedColor.xyz.Y / 100.0));
                    lastClicked.Y = (int)Math.Round(clientHeight * (1.0 - (double)selectedColor.xyz.Z / 150.0));
                    break;
            }

            lastClicked = GetPoint(lastClicked);
        }

        protected void SetSliderMarker()
        {
            switch (DrawStyle)
            {
                // HSB Color Space
                case DrawStyles.HSBHue:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * SelectedColor.hsb.Hue));
                    break;
                case DrawStyles.HSBSaturation:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * SelectedColor.hsb.Saturation));
                    break;
                case DrawStyles.HSBBrightness:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * SelectedColor.hsb.Brightness));
                    break;

                // HSL Color Space
                case DrawStyles.HSLHue:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * SelectedColor.hsl.Hue));
                    break;
                case DrawStyles.HSLSaturation:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * SelectedColor.hsl.Saturation));
                    break;
                case DrawStyles.HSLLightness:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * SelectedColor.hsl.Lightness));
                    break;

                // RGB Color Space
                case DrawStyles.Red:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * (double)SelectedColor.argb.R / 255));
                    break;
                case DrawStyles.Green:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * (double)SelectedColor.argb.G / 255));
                    break;
                case DrawStyles.Blue:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * (double)SelectedColor.argb.B / 255));
                    break;

                case DrawStyles.xyz:
                    lastClicked.Y = (clientHeight) - (int)Math.Round(((clientHeight) * (double)SelectedColor.xyz.X / 150.0));
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
                ColorChanged(this, new ColorEventArgs(this.selectedColor, this.absoluteColor, this.drawStyle));
        }
    }
}
