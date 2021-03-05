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
using System.Drawing.Imaging;

namespace WinkingCat.ScreenCaptureLib
{
    public partial class ClippingWindowForm : Form
    {
        public Rectangle clientArea { get; private set; }
        public RegionCaptureMode mode { get; set; }
        public Bitmap image { get; private set; }


        private Point leftClickStart = new Point();
        private Point leftClickStop = new Point();
        private Point mousePos;
        private Rectangle activeMonitor;
        private RegionResult result;
        private bool isLeftClicking = false;

        private TextureBrush backgroundBrush;
        private Pen borderDotPen, borderPen, magnifierBorderPen;
        private Brush textBackgroundBrush, textFontBrush, backgroundHighlightBrush;
        private Font infoFont;

        public ClippingWindowForm(Rectangle region)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            // force the client area to be at 0, 0 so that when you draw using graphics it draws in the correct location
            clientArea = new Rectangle(new Point(0, 0), new Size(region.Width, region.Height));
            mode = RegionCaptureOptions.mode;

            // borderDotPen draws the dashed lines for the screenwide corsshair and selection box
            // magnifierBorderPen draws the border around the magnifier
            // borderPen draws the border on the info text
            borderDotPen = new Pen(ApplicationStyles.currentStyle.regionCaptureStyle.ScreenWideCrosshairColor) { DashPattern = new float[] { 5, 5 } };
            magnifierBorderPen = new Pen(ApplicationStyles.currentStyle.regionCaptureStyle.MagnifierBorderColor);
            borderPen = new Pen(ApplicationStyles.currentStyle.regionCaptureStyle.infoTextBorderColor);                
            
            // textBackgroundBrush is used to fill the color behind the info text
            // textFontBrush is used to specify the color of the info text
            textBackgroundBrush = new SolidBrush(ApplicationStyles.currentStyle.regionCaptureStyle.infoTextBackgroundColor);
            textFontBrush = new SolidBrush(ApplicationStyles.currentStyle.regionCaptureStyle.infoTextTextColor);

            // infoFont is used to specify the font / size of the info text
            infoFont = new Font("Verdana", 12); // 10


            InitializeComponent();

            // this has to be done after InitializeComponent 
            // otherwise the taskbar will be in front of the window 
            SuspendLayout();

            BackColor = Color.Black;
            MinimumSize = new Size(clientArea.Width, clientArea.Height);
            MaximumSize = new Size(clientArea.Width, clientArea.Height);
            Bounds = region;
#if !DEBUG
            TopMost = true;
#endif

            ResumeLayout();
            

            // set the cursor 
            var buffer = Properties.Resources.ResourceManager.GetObject(ResourceManager.regionCaptureCursor) as byte[];
            using (MemoryStream m = new MemoryStream(buffer))
            {
                Cursor = new Cursor(m);
            }

            // set the image wanted to play around with PixelFormat of the image
            image = new Bitmap(region.Width, region.Height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(image))
            using (Bitmap tmp = ScreenShotManager.CaptureRectangle(region))
            {
                g.DrawImage(tmp, new Point(0, 0));
            }

            // create the texture brush that will draw the background image to the canvas
            // if the user wants an overlay it will be added to the texture brush
            using(Bitmap DimmedCanvas = (Bitmap)image.Clone())
            using (Graphics g = Graphics.FromImage(DimmedCanvas))
            using (Brush brush = new SolidBrush(Color.FromArgb(ApplicationStyles.currentStyle.regionCaptureStyle.BackgroundOverlayOpacity, ApplicationStyles.currentStyle.regionCaptureStyle.BackgroundOverlayColor)))
            {
                if(RegionCaptureOptions.dimBackground)
                    g.FillRectangle(brush, 0, 0, DimmedCanvas.Width, DimmedCanvas.Height);

                backgroundBrush = new TextureBrush(DimmedCanvas) { WrapMode = WrapMode.Clamp };
            }

            // used to make the selection box fill with the same color as background
            backgroundHighlightBrush = new TextureBrush(image) { WrapMode = WrapMode.Clamp }; 
        }

        // pretty sure this makes it look like everything is drawn at once?
        // i forget but i think it is smoother with this 
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void KeyDown_Event(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            switch (e.KeyData)
            {
                case Keys.Z:
                    InRegionTaskHandler(RegionCaptureOptions.onZPress);
                    break;

                case Keys.Escape:
                    InRegionTaskHandler(RegionCaptureOptions.onEscapePress);
                    break;
            }
        }

        private void MouseWheel_Event(object sender, MouseEventArgs e)
        {
            if(e.Delta > 0)
            {
                if (RegionCaptureOptions.magnifierZoomLevel + RegionCaptureOptions.magnifierZoomScale < 6)
                    RegionCaptureOptions.magnifierZoomLevel += RegionCaptureOptions.magnifierZoomScale;
                if (RegionCaptureOptions.magnifierZoomLevel > 0)
                    RegionCaptureOptions.drawMagnifier = true;
            }
            else
            {
                if (RegionCaptureOptions.magnifierZoomLevel - RegionCaptureOptions.magnifierZoomScale >  0)
                    RegionCaptureOptions.magnifierZoomLevel -= 0.25f;
                else
                {
                    RegionCaptureOptions.magnifierZoomLevel = 0;
                    RegionCaptureOptions.drawMagnifier = false;
                }                
            }
            Invalidate();
        }

        private void MouseMove_Event(object sender, MouseEventArgs e)
        {
            if(RegionCaptureOptions.updateOnMouseMove)
                Invalidate();
        }

        private void InRegionTaskHandler(InRegionTasks task)
        {
            switch (task)
            {
                case InRegionTasks.DoNothing:
                    break;

                case InRegionTasks.Cancel:
                    result = RegionResult.Close;
                    Close();
                    break;

                case InRegionTasks.RemoveSelectionOrCancel:
                    if (isLeftClicking)
                        isLeftClicking = false;
                    else
                    {
                        result = RegionResult.Close;
                        Close();
                    }
                    break;

                case InRegionTasks.CaptureFullScreen:
                    result = RegionResult.Fullscreen;
                    Close();
                    break;

                case InRegionTasks.CaptureActiveMonitor:
                    result = RegionResult.ActiveMonitor;
                    Close();
                    break;

                case InRegionTasks.CaptureLastRegion:
                    result = RegionResult.LastRegion;
                    Close();
                    break;

                case InRegionTasks.RemoveSelection:
                    if (isLeftClicking)
                        isLeftClicking = false;
                    break;

                case InRegionTasks.SwapToolType:
                    if (mode == RegionCaptureMode.ColorPicker)
                    {
                        mode = RegionCaptureMode.Default;
                    }
                    else if(mode == RegionCaptureMode.Default)
                    {
                        mode = RegionCaptureMode.ColorPicker;
                    }
                    break;

                case InRegionTasks.SwapCenterMagnifier:
                    RegionCaptureOptions.tryCenterMagnifier = !RegionCaptureOptions.tryCenterMagnifier;
                    Invalidate();
                    break;
            }
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
                    InRegionTaskHandler(RegionCaptureOptions.onMouseRightClick);
                    break;

                case MouseButtons.Middle:
                    InRegionTaskHandler(RegionCaptureOptions.onMouseMiddleClick);
                    break;

                case MouseButtons.XButton1:
                    InRegionTaskHandler(RegionCaptureOptions.onXButton1Click);
                    break;

                case MouseButtons.XButton2:
                    InRegionTaskHandler(RegionCaptureOptions.onXButton2Click);
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
            activeMonitor = ScreenHelper.GetActiveScreenBounds0Based();

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

            // if you want to update as often as possible for the smoothest graphics 
            // otherwise if you want to use less cpu disable this 
            if (!RegionCaptureOptions.updateOnMouseMove)
                Invalidate();
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
            Point pointToScreen = PointToScreen(mousePos);
            int magX = mousePos.X;
            int magY = mousePos.Y;
            int totalWidth = RegionCaptureOptions.cursorInfoOffset;
            int totalHeight = RegionCaptureOptions.cursorInfoOffset;
            string infoText = $"X: {pointToScreen.X} Y: {pointToScreen.Y}";
            Size infoTextSize = g.MeasureString(infoText, infoFont).ToSize();

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
                    totalHeight += infoTextSize.Height;
                    totalHeight += RegionCaptureOptions.cursorInfoOffset;

                    // if the width of the info text is greater than the magnifier width use that for totalWidth
                    if (RegionCaptureOptions.drawMagnifier)
                    {
                        if (magnifier.Width < infoTextSize.Width)
                        {
                            totalWidth -= magnifier.Width;
                            totalWidth += infoTextSize.Width;
                        }
                    }
                    else
                    {
                        totalWidth += infoTextSize.Width;

                        // to cancel the increase to totalHeight below otherwise the info text will apear 
                        // farther down than its supposed to
                        totalHeight -= RegionCaptureOptions.cursorInfoOffset;
                    }
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
                    DrawInfoText(g, infoText, 
                        new Rectangle(new Point(magX, magY + Math.Abs(totalHeight)), infoTextSize), 
                        infoFont, textFontBrush, textBackgroundBrush, borderPen);
                }

                if (RegionCaptureOptions.drawMagnifier)
                {
                    if (RegionCaptureOptions.drawMagnifierBorder)
                        g.DrawRectangle(magnifierBorderPen, magX - 1, magY - 1, magnifier.Width + 1, magnifier.Height + 1);

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
                    using (Pen pen = new Pen(ApplicationStyles.currentStyle.regionCaptureStyle.MagnifierGridColor))
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
                    using (SolidBrush crosshairBrush = new SolidBrush(ApplicationStyles.currentStyle.regionCaptureStyle.MagnifierCrosshairColor))
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

            if (!RegionCaptureOptions.drawCrossHair)
            {
                g.DrawLine(borderDotPen, new Point(mousePos.X, leftClickStart.Y), mousePos);
                g.DrawLine(borderDotPen, new Point(leftClickStart.X, mousePos.Y), mousePos);
            }
        }

        private void DrawInfoText(Graphics g, string text, Rectangle rec, Font font, Brush textFontBrush ,Brush backgroundBrush, Pen outerBorderPen)
        {
            int mX = rec.X;
            int mY = rec.Y - RegionCaptureOptions.cursorInfoOffset - rec.Height;

            Rectangle rect = new Rectangle(
                new Point(mX, mY), 
                new Size(rec.Width, rec.Height));

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
