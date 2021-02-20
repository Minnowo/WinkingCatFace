using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using WinkingCat.HelperLibs;
using System.Diagnostics;
using System.IO;

namespace WinkingCat.ScreenCaptureLib
{
    public partial class ClippingWindowForm : Form
    {
        public Rectangle clientArea { get; private set; }

        //private Timer timer;


        public bool isLeftClicking { get; private set; } = false;

        public Bitmap image { get; private set; }
        public Point leftClickStart { get; private set; } = new Point();
        public Point leftClickStop { get; private set; } = new Point();

        public Rectangle activeMonitor;
        public Point mousePos;
        public RegionResult result;
        public RegionCaptureMode mode;

        private TextureBrush backgroundBrush;
        private Pen borderDotPen, borderPen;
        private Brush textBackgroundBrush, textFontBrush, backgroundHighlightBrush;
        private Font infoFont;

        public ClippingWindowForm(Rectangle region)
        {
            clientArea = region;
            image = ScreenShotManager.CaptureRectangle(region);
            mode = RegionCaptureOptions.mode;

            borderPen = new Pen(Color.Black);            
            borderDotPen = new Pen(Color.White) { DashPattern = new float[] { 5, 5 } };
            infoFont = new Font("Verdana", 12); // 10

            textBackgroundBrush = new SolidBrush(System.Drawing.Color.FromArgb(39, 43, 50));
            textFontBrush = new SolidBrush(System.Drawing.Color.FromArgb(255, 255, 255));

            SuspendLayout();

            BackColor = Color.Black;

            //TopMost = true;
            var buffer = Properties.Resources.ResourceManager.GetObject(ResourceManager.regionCaptureCursor) as byte[];
            using (MemoryStream m = new MemoryStream(buffer))
            {
                Cursor = new Cursor(m);
            }

            ResumeLayout();
            InitializeComponent();

            MinimumSize = new Size(clientArea.Width, clientArea.Height);
            MaximumSize = new Size(clientArea.Width, clientArea.Height);
            Bounds = region;

            Bitmap DimmedCanvas = (Bitmap)image.Clone();

            using (Graphics g = Graphics.FromImage(DimmedCanvas))
            using (Brush brush = new SolidBrush(Color.FromArgb(50, Color.Black)))
            {
                g.FillRectangle(brush, 0, 0, DimmedCanvas.Width, DimmedCanvas.Height);

                backgroundBrush = new TextureBrush(DimmedCanvas) { WrapMode = WrapMode.Clamp };
            }
            DimmedCanvas.Dispose();
            // used to make the selection box fill with the same color as background
            backgroundHighlightBrush = new TextureBrush(image) { WrapMode = WrapMode.Clamp }; 
        }


        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void MouseWheel_Event(object sender, MouseEventArgs e)
        {
            if(e.Delta > 0)
            {
                if (RegionCaptureOptions.magnifierZoomLevel + 1 < 10)
                    RegionCaptureOptions.magnifierZoomLevel += 1;
                if (RegionCaptureOptions.magnifierZoomLevel > 0)
                    RegionCaptureOptions.drawMagnifier = true;
            }
            else
            {
                if (RegionCaptureOptions.magnifierZoomLevel - 1 >=  0)
                    RegionCaptureOptions.magnifierZoomLevel -= 1;
                if (RegionCaptureOptions.magnifierZoomLevel == 0)
                    RegionCaptureOptions.drawMagnifier = false;
            }
            Invalidate();
        }

        private void MouseMove_Event(object sender, MouseEventArgs e)
        {
            Invalidate();
        }

        private void ClickRelease_Event(object sender, EventArgs e)
        {
            switch (((MouseEventArgs)e).Button)
            {
                case MouseButtons.Left:
                    leftClickStop = ((MouseEventArgs)e).Location;
                    if(mode != RegionCaptureMode.ColorPicker)
                    {
                        if (isLeftClicking && leftClickStart != leftClickStop && ScreenHelper.IsValidCropArea(leftClickStart, leftClickStop))
                        {                          
                            result = RegionResult.Region;
                            Close();
                        }
                    }
                    else
                    {
                        result = RegionResult.Color;
                        Close();
                    }

                    isLeftClicking = false;
                    break;

                case MouseButtons.Right:
                    if (isLeftClicking)
                        isLeftClicking = false;
                    else
                    {
                        result = RegionResult.Close;
                        Close();
                    }
                    break;

                case MouseButtons.Middle:
                    result = RegionResult.LastRegion;
                    Close();
                    break;

                case MouseButtons.XButton1:
                    result = RegionResult.ActiveMonitor;
                    Close();
                    break;

                case MouseButtons.XButton2:
                    result = RegionResult.Fullscreen;
                    Close();
                    break;
            }
        }

        private void Click_Event(object sender, EventArgs e)
        {
            switch (((MouseEventArgs)e).Button)
            {
                case MouseButtons.Left:
                    isLeftClicking = true;
                    leftClickStart = ((MouseEventArgs)e).Location;
                    break;

                case MouseButtons.Right:
                    break;

                case MouseButtons.Middle:
                    break;

                case MouseButtons.XButton1:
                    break;

                case MouseButtons.XButton2:
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            mousePos = ScreenHelper.ScreenToClient(ScreenHelper.GetCursorPosition());
            activeMonitor = ScreenHelper.GetActiveScreenBounds();

            Graphics g = e.Graphics;

            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality; // for some reason highspeed crashes the window
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.CompositingMode = CompositingMode.SourceOver;

            if (RegionCaptureOptions.marchingAnts)
            {
                borderDotPen.DashOffset += 0.33f;
                if (borderDotPen.DashOffset > 10) borderDotPen.DashOffset = 0;
            }

            g.CompositingMode = CompositingMode.SourceCopy;
            g.FillRectangle(backgroundBrush, clientArea);
            g.CompositingMode = CompositingMode.SourceOver;

            DrawMouseGraphics(g);
            //Invalidate()
        }


        private void DrawMouseGraphics(Graphics g)
        {
            if (isLeftClicking)
            {
                DrawSelectionBox(g, mousePos);
            }

            DrawMouseInfo(g);

            if (RegionCaptureOptions.drawCrossHair)
            {
                DrawCrosshair(g, mousePos);               
            }            
        }

        private void DrawMouseInfo(Graphics g)
        {
            Bitmap magnifier = null;
            int magX = mousePos.X;
            int magY = mousePos.Y;
            int totalWidth = RegionCaptureOptions.cursorInfoOffset;
            int totalHeight = RegionCaptureOptions.cursorInfoOffset;

            if (RegionCaptureOptions.tryCenterMagnifier && RegionCaptureOptions.drawMagnifier)
            {
                magnifier = DrawMagnifier(mousePos);
                totalWidth = magnifier.Width;
                totalHeight = magnifier.Height;


                g.DrawImage(magnifier, new Point(magX - totalWidth/2, magY - totalHeight/2));
                magnifier.Dispose();
            }
            else
            {
                if (RegionCaptureOptions.drawMagnifier)
                {
                    magnifier = DrawMagnifier(mousePos);

                    totalWidth += magnifier.Width;
                    totalHeight += magnifier.Height;
                }

                if (RegionCaptureOptions.drawInfoText)
                {
                    totalHeight += infoFont.Height + 2;
                    totalHeight += RegionCaptureOptions.cursorInfoOffset;
                }

                if (magX + totalWidth > activeMonitor.Width + activeMonitor.X)
                {
                    totalWidth *= -1;
                    magX += totalWidth;
                }
                else
                {
                    magX += RegionCaptureOptions.cursorInfoOffset;
                }

                if (magY + totalHeight > activeMonitor.Height + activeMonitor.Y)
                {
                    totalHeight *= -1;
                    magY += totalHeight;
                }
                else
                {
                    magY += RegionCaptureOptions.cursorInfoOffset;
                }

                if (RegionCaptureOptions.drawInfoText)
                {
                    DrawInfoText(g, $"X: {mousePos.X} Y: {mousePos.Y}", infoFont, textFontBrush, textBackgroundBrush, borderPen, new Point(magX, magY + Math.Abs(totalHeight)));
                }

                if (RegionCaptureOptions.drawMagnifier)
                {
                    g.DrawImage(magnifier, new Point(magX, magY));
                    magnifier.Dispose();
                }
            }
        }

        private Bitmap DrawMagnifier(Point mousePos)
        {
            int pixelCount = MathHelper.MakeOdd(((int)(RegionCaptureOptions.magnifierPixelCount * RegionCaptureOptions.magnifierZoomLevel)).Clamp(1, 500));
            int pixelSize = MathHelper.MakeOdd(RegionCaptureOptions.magnifierPixelSize.Clamp(1, 50));

            int width = pixelCount * pixelSize;
            int height = pixelCount * pixelSize;

            Bitmap bmp = new Bitmap(width, height);
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.InterpolationMode = InterpolationMode.NearestNeighbor;
                gr.PixelOffsetMode = PixelOffsetMode.Half;

                gr.DrawImage(image, new Rectangle(0, 0, width, height),
                    new Rectangle(
                        mousePos.X - pixelCount / 2, mousePos.Y - pixelCount / 2,
                        pixelCount, pixelCount),
                    GraphicsUnit.Pixel);

                gr.PixelOffsetMode = PixelOffsetMode.None;

                if (RegionCaptureOptions.drawMagnifierGrid)
                {
                    using (Pen pen = new Pen(Color.FromArgb(25, Color.White)))
                    {
                        for (int x = 1; x < pixelCount; x++)
                        {
                            gr.DrawLine(pen, new Point((x * pixelSize) - 1, 0), new Point((x * pixelSize) - 1, height - 1));
                        }

                        for (int y = 1; y < pixelCount; y++)
                        {
                            gr.DrawLine(pen, new Point(0, (y * pixelSize) - 1), new Point(width - 1, (y * pixelSize) - 1));
                        }
                    }
                }

                if (RegionCaptureOptions.drawMagnifierCrosshair)
                {
                    using (SolidBrush crosshairBrush = new SolidBrush(Color.FromArgb(125, Color.LightBlue)))
                    {
                        gr.FillRectangle(crosshairBrush, new Rectangle(0, (height - pixelSize) / 2, (width - pixelSize) / 2, pixelSize)); // Left
                        gr.FillRectangle(crosshairBrush, new Rectangle((width + pixelSize) / 2, (height - pixelSize) / 2, (width - pixelSize) / 2, pixelSize)); // Right
                        gr.FillRectangle(crosshairBrush, new Rectangle((width - pixelSize) / 2, 0, pixelSize, (height - pixelSize) / 2)); // Top
                        gr.FillRectangle(crosshairBrush, new Rectangle((width - pixelSize) / 2, (height + pixelSize) / 2, pixelSize, (height - pixelSize) / 2)); // Bottom
                    }
                }
            }
            return bmp;
        }
        private void DrawSelectionBox(Graphics g, Point mousePos)
        {
            g.FillRectangle(backgroundHighlightBrush, ScreenHelper.CreateValidCropArea(leftClickStart, mousePos));
            
            g.DrawLine(borderDotPen, leftClickStart, new Point(mousePos.X, leftClickStart.Y));
            g.DrawLine(borderDotPen, leftClickStart, new Point(leftClickStart.X, mousePos.Y));
        }

        private void DrawInfoText(Graphics g, string text, Font font, Brush textFontBrush ,Brush backgroundBrush, Pen outerBorderPen, Point pos)
        {
            int width = font.Height / 2 * text.Length;
            int height = font.Height + 2;
            int mX = pos.X;
            int mY = pos.Y - height - RegionCaptureOptions.cursorInfoOffset;

            Rectangle rect = new Rectangle(
                new Point(mX, mY), 
                new Size(width, height));

            g.DrawRectangle(outerBorderPen, rect);         
            g.FillRectangle(backgroundBrush, rect);

            g.DrawString(text, font, textFontBrush, mX + 1, mY + 1);            
        }

        private void DrawCrosshair(Graphics g, Point mousePos)
        {
            Point horizontalP1 = new Point(0, mousePos.Y);
            Point horizontalP2 = new Point(clientArea.Width, mousePos.Y);

            Point verticalP1 = new Point(mousePos.X, 0);
            Point verticalP2 = new Point(mousePos.X, clientArea.Height);

            g.DrawLine(borderDotPen, horizontalP1, horizontalP2);
            g.DrawLine(borderDotPen, verticalP1, verticalP2);
        }

        public LastRegionCaptureInfo GetResultImage()
        {
            if (leftClickStart.X < leftClickStop.X)
                leftClickStop = new Point(leftClickStop.X + 1, leftClickStop.Y);
            else
                leftClickStop = new Point(leftClickStop.X - 1, leftClickStop.Y);
            if (leftClickStart.Y < leftClickStop.Y)
                leftClickStop = new Point(leftClickStop.X, leftClickStop.Y + 1);
            else
                leftClickStop = new Point(leftClickStop.X, leftClickStop.Y - 1);
            switch (result)
            {
                case RegionResult.Close:
                    return new LastRegionCaptureInfo(RegionResult.Close);

                case RegionResult.Region:
                    return new LastRegionCaptureInfo(RegionResult.Region, leftClickStart, leftClickStop, 
                        ScreenHelper.CreateValidCropArea(leftClickStart, leftClickStop), 
                        ScreenShotManager.CropImage(leftClickStart, leftClickStop, image));                    //return ScreenShotManager.CropImage(leftClickStart, leftClickStop, clipWinPictureBox.Image);

                case RegionResult.LastRegion: 
                    return new LastRegionCaptureInfo(RegionResult.LastRegion, ImageHandler.LastInfo.StartLeftClick, 
                        ImageHandler.LastInfo.StopLeftClick, ImageHandler.LastInfo.Region,
                        ScreenShotManager.CropImage(ImageHandler.LastInfo.Region, image));

                case RegionResult.Fullscreen:
                    return new LastRegionCaptureInfo(RegionResult.Fullscreen, true, image);

                case RegionResult.ActiveMonitor:
                    return new LastRegionCaptureInfo(RegionResult.ActiveMonitor, Screen.FromPoint(ScreenHelper.GetCursorPosition()),
                        ScreenShotManager.CropImage(ScreenHelper.GetActiveScreenBounds0Based(), image));

                case RegionResult.Color:
                    return new LastRegionCaptureInfo(RegionResult.Color, leftClickStart, leftClickStop, (image).GetPixel(leftClickStop.X, leftClickStop.Y));
            }
            return null;
        }

        public void Destroy()
        {
            borderDotPen?.Dispose();
            borderPen?.Dispose();
            infoFont?.Dispose();


            image?.Dispose();
            backgroundBrush?.Dispose();
            backgroundHighlightBrush?.Dispose();

            base.Dispose(true);
        }
    }
}
