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
using WinkingCat.ScreenCaptureLib;
using System.Diagnostics;

namespace WinkingCat
{
    public partial class ClippingWindowForm : Form
    {
        public Rectangle clientArea { get; private set; }

        //private Timer timer;


        public bool isLeftClicking { get; private set; } = false;

        public Bitmap image { get; private set; }
        public Point leftClickStart { get; private set; } = new Point();
        public Point leftClickStop { get; private set; } = new Point();


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
            mode = RegionCaptureOptions.mode;

            borderPen = new Pen(Color.Black);            
            borderDotPen = new Pen(Color.White) { DashPattern = new float[] { 5, 5 } };
            infoFont = new Font("Verdana", 12); // 10
            //infoFont = new Font("Consolas", 25);

            textBackgroundBrush = new SolidBrush(System.Drawing.Color.FromArgb(39, 43, 50));
            textFontBrush = new SolidBrush(System.Drawing.Color.FromArgb(255, 255, 255));

            SuspendLayout();

            //clipWinPictureBox.Image = ScreenShotManager.CaptureRectangle(region);
            image = ScreenShotManager.CaptureRectangle(region);
            BackColor = Color.Black;
            //Show();

            MaximizeBox = false;
            TopMost = true;

            Cursor = new Cursor(DirectoryManager.currentDirectory + ResourceManager.regionCaptureCursor);


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

        private void MouseMove_Event(object sender, MouseEventArgs e)
        {
            Invalidate();
            //Refresh();
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

            if (RegionCaptureOptions.drawMagnifier)
            {
                
            }

            if (RegionCaptureOptions.drawCrossHair)
            {
                DrawCrosshair(g, mousePos);               
            }

            if (RegionCaptureOptions.drawInfoText)
            {
                DrawInfoText(g, $"X: {mousePos.X} Y: {mousePos.Y}", infoFont, textFontBrush, textBackgroundBrush, borderPen, mousePos);
            }

            DrawCursorGraphics();

        }
        private void DrawCursorGraphics()
        {

        }
        private void DrawSelectionBox(Graphics g, Point mousePos)
        {
            g.FillRectangle(backgroundHighlightBrush, ScreenHelper.CreateValidCropArea(leftClickStart, mousePos));
            
            g.DrawLine(borderDotPen, leftClickStart, new Point(mousePos.X, leftClickStart.Y));
            g.DrawLine(borderDotPen, leftClickStart, new Point(leftClickStart.X, mousePos.Y));
        }

        private void DrawInfoText(Graphics g, string text, Font font, Brush textFontBrush ,Brush backgroundBrush, Pen outerBorderPen,  Point cursor)
        {
            Rectangle rect = new Rectangle(new Point(cursor.X + RegionCaptureOptions.cursorInfoOffset, cursor.Y + RegionCaptureOptions.cursorInfoOffset), new Size(font.Height/2 * text.Length, font.Height + 2));
            g.DrawRectangle(outerBorderPen, rect);         
            g.FillRectangle(backgroundBrush, rect);

            g.DrawString(text, font, textFontBrush, cursor.X + RegionCaptureOptions.cursorInfoOffset + 1, cursor.Y + RegionCaptureOptions.cursorInfoOffset + 1);            
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
            //timer?.Stop();
            //timer?.Dispose();

            borderDotPen?.Dispose();
            borderPen?.Dispose();
            infoFont?.Dispose();
            backgroundHighlightBrush?.Dispose();

            image?.Dispose();
            backgroundBrush?.Dispose();
            //clipWinPictureBox?.Dispose();

            base.Dispose(true);
        }
    }
}
