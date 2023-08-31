using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using WinkingCat.HelperLibs;
using WinkingCat.Settings;

namespace WinkingCat
{
    public partial class RegionCaptureForm : Form
    {
        public static RegionReturn LastRegionReturn;


        public Rectangle clientArea { get; private set; }
        public RegionCaptureMode mode { get; set; }
        public Bitmap image { get; private set; }


        private Point leftClickStart = Point.Empty;
        private Point leftClickStop = Point.Empty;
        private Point mousePos;
        private Rectangle activeMonitor;
        private RegionResult result;
        private bool isLeftClicking = false;

        private TextureBrush backgroundBrush;
        private SolidBrush magnifierCrosshairBrush;
        private Brush textBackgroundBrush, textFontBrush, backgroundHighlightBrush;
        private Pen borderDotPen, borderPen, magnifierBorderPen, magnifierGridPen;
        private Font infoFont;

        public RegionCaptureForm() : this(ScreenHelper.GetScreenBounds(), SettingsManager.RegionCaptureSettings.Mode)
        {

        }
        public RegionCaptureForm(Rectangle region, RegionCaptureMode mode_)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            // force the client area to be at 0, 0 so that when you draw using graphics it draws in the correct location
            clientArea = new Rectangle(new Point(0, 0), new Size(region.Width, region.Height));
            mode = mode_;

            // borderDotPen draws the dashed lines for the screenwide corsshair and selection box
            // magnifierBorderPen draws the border around the magnifier
            // magnifierGridPen draws the grid on the magnifier
            // borderPen draws the border on the info text
            // magnifierCrosshairBrush draws the magnifier crosshair
            borderDotPen = new Pen(SettingsManager.RegionCaptureSettings.Screen_Wide_Crosshair_Color);
            magnifierBorderPen = new Pen(SettingsManager.RegionCaptureSettings.Magnifier_Border_Color);
            magnifierGridPen = new Pen(SettingsManager.RegionCaptureSettings.Magnifier_Grid_Color);
            borderPen = new Pen(SettingsManager.RegionCaptureSettings.Info_Text_Border_Color);
            magnifierCrosshairBrush = new SolidBrush(SettingsManager.RegionCaptureSettings.Magnifier_Crosshair_Color);

            // textBackgroundBrush is used to fill the color behind the info text
            // textFontBrush is used to specify the color of the info text
            textBackgroundBrush = new SolidBrush(SettingsManager.RegionCaptureSettings.Info_Text_Back_Color);
            textFontBrush = new SolidBrush(SettingsManager.RegionCaptureSettings.Info_Text_Color);

            // infoFont is used to specify the font / size of the info text
            infoFont = new Font("Verdana", 12); // 10


            if (SettingsManager.RegionCaptureSettings.Draw_Marching_Ants)
            {
                borderDotPen.DashPattern = new float[] { 5, 5 };
            }
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

            var buffer = Properties.Resources.Crosshair;
            using (MemoryStream m = new MemoryStream(buffer))
            {
                Cursor = new Cursor(m);
            }

            image = ScreenshotHelper.CaptureRectangle(region);

            using (Bitmap DimmedCanvas = image.CloneSafe())
            using (Graphics g = Graphics.FromImage(DimmedCanvas))
            using (Brush brush = new SolidBrush(SettingsManager.RegionCaptureSettings.Background_Overlay_Color))
            {
                if (SettingsManager.RegionCaptureSettings.Draw_Background_Overlay)
                    g.FillRectangle(brush, 0, 0, DimmedCanvas.Width, DimmedCanvas.Height);

                backgroundBrush = new TextureBrush(DimmedCanvas) { WrapMode = WrapMode.Clamp };
            }

            // used to make the selection box fill with the same color as background
            backgroundHighlightBrush = new TextureBrush(image) { WrapMode = WrapMode.Clamp };
        }


        /// <summary>
        /// Gets the RegionCaptureInfo for the given region capture.
        /// </summary>
        /// <returns> The image/color captured and other info.</returns>
        public RegionReturn GetRsult()
        {
            if (result == RegionResult.Region)
            {
                if (leftClickStart.X < leftClickStop.X)
                    leftClickStop = new Point(leftClickStop.X + 1, leftClickStop.Y);
                else
                    leftClickStop = new Point(leftClickStop.X - 1, leftClickStop.Y);

                if (leftClickStart.Y < leftClickStop.Y)
                    leftClickStop = new Point(leftClickStop.X, leftClickStop.Y + 1);
                else
                    leftClickStop = new Point(leftClickStop.X, leftClickStop.Y - 1);
            }

            switch (result)
            {
                case RegionResult.Close:
                    return new RegionReturn(RegionResult.Close);

                case RegionResult.Region:
                    return new RegionReturn(
                        RegionResult.Region,
                        PointToScreen(leftClickStart),
                        PointToScreen(leftClickStop),
                        Helper.CreateRect(leftClickStart, leftClickStop),
                        ImageProcessor.GetCroppedBitmap(leftClickStart, leftClickStop, image, PixelFormat.Format24bppRgb));

                case RegionResult.LastRegion:
                    return new RegionReturn(
                        RegionResult.LastRegion,
                        LastRegionReturn.StartLeftClick,
                        LastRegionReturn.StopLeftClick,
                        LastRegionReturn.Region,
                        ImageProcessor.GetCroppedBitmap(LastRegionReturn.Region, image, PixelFormat.Format24bppRgb));

                case RegionResult.Fullscreen:
                    return new RegionReturn(RegionResult.Fullscreen, true, image);

                case RegionResult.ActiveMonitor:
                    return new RegionReturn(
                        Screen.FromPoint(ScreenHelper.GetCursorPosition()),
                        ImageProcessor.GetCroppedBitmap(ScreenHelper.GetActiveScreenBounds0Based(), image, PixelFormat.Format24bppRgb));

                case RegionResult.Color:
                    return new RegionReturn(
                        PointToScreen(leftClickStop),
                        image.GetPixel(leftClickStop.X, leftClickStop.Y));
            }
            return null;
        }


        private void InRegionTaskHandler(InRegionTasks task)
        {
            switch (task)
            {
                case InRegionTasks.DoNothing:
                    return;

                case InRegionTasks.Cancel:
                    result = RegionResult.Close;
                    Close();
                    return;

                case InRegionTasks.RemoveSelectionOrCancel:
                    if (isLeftClicking)
                    {
                        isLeftClicking = false;
                    }
                    else
                    {
                        result = RegionResult.Close;
                        Close();
                    }
                    return;

                case InRegionTasks.CaptureFullScreen:
                    result = RegionResult.Fullscreen;
                    Close();
                    return;

                case InRegionTasks.CaptureActiveMonitor:
                    result = RegionResult.ActiveMonitor;
                    Close();
                    return;

                case InRegionTasks.CaptureLastRegion:
                    result = RegionResult.LastRegion;
                    Close();
                    return;

                case InRegionTasks.RemoveSelection:
                    if (isLeftClicking)
                    {
                        isLeftClicking = false;
                    }
                    return;

                case InRegionTasks.SwapCenterMagnifier:
                    SettingsManager.RegionCaptureSettings.Center_Magnifier_On_Mouse = !SettingsManager.RegionCaptureSettings.Center_Magnifier_On_Mouse;
                    Invalidate();
                    return;
            }
        }

        private void DrawMouseGraphics(Graphics g)
        {
            if (mode == RegionCaptureMode.ColorPicker)
            {
                DrawMouseInfo(g);
            }
            else
            {
                if (isLeftClicking)
                {
                    DrawSelectionBox(g, mousePos);
                }

                DrawMouseInfo(g);

                if (SettingsManager.RegionCaptureSettings.Draw_Screen_Wide_Crosshair)
                {
                    DrawCrosshair(g, mousePos);
                }
            }
        }

        private void DrawMouseInfo(Graphics g)
        {
            Bitmap magnifier = null;
            Point pointToScreen = PointToScreen(mousePos);
            int magX = mousePos.X;
            int magY = mousePos.Y;
            int totalWidth = SettingsManager.RegionCaptureSettings.Cursor_Info_Offset;
            int totalHeight = SettingsManager.RegionCaptureSettings.Cursor_Info_Offset;
            string infoText = $"X: {pointToScreen.X} Y: {pointToScreen.Y}";
            Size infoTextSize = g.MeasureString(infoText, infoFont).ToSize();

            if (SettingsManager.RegionCaptureSettings.Center_Magnifier_On_Mouse && SettingsManager.RegionCaptureSettings.Draw_Magnifier)
            {
                magnifier = DrawMagnifier(mousePos);
                totalWidth = magnifier.Width;
                totalHeight = magnifier.Height;


                g.DrawImage(magnifier, new Point(magX - totalWidth / 2, magY - totalHeight / 2));
                magnifier.Dispose();
            }
            else
            {
                if (SettingsManager.RegionCaptureSettings.Draw_Magnifier)
                {
                    magnifier = DrawMagnifier(mousePos);

                    totalWidth += magnifier.Width;
                    totalHeight += magnifier.Height;
                }


                if (SettingsManager.RegionCaptureSettings.Draw_Info_Text)
                {
                    totalHeight += infoTextSize.Height;
                    totalHeight += SettingsManager.RegionCaptureSettings.Cursor_Info_Offset;

                    // if the width of the info text is greater than the magnifier width use that for totalWidth
                    if (SettingsManager.RegionCaptureSettings.Draw_Magnifier)
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
                        totalHeight -= SettingsManager.RegionCaptureSettings.Cursor_Info_Offset;
                    }
                }

                if (magX + totalWidth > activeMonitor.Width + activeMonitor.X)
                {
                    totalWidth *= -1;
                    magX += totalWidth;
                }
                else
                {
                    magX += SettingsManager.RegionCaptureSettings.Cursor_Info_Offset;
                }

                if (magY + totalHeight > activeMonitor.Height + activeMonitor.Y)
                {
                    totalHeight *= -1;
                    magY += totalHeight;
                }
                else
                {
                    magY += SettingsManager.RegionCaptureSettings.Cursor_Info_Offset;
                }

                if (SettingsManager.RegionCaptureSettings.Draw_Info_Text)
                {
                    DrawInfoText(g, infoText,
                        new Rectangle(new Point(magX, magY + Math.Abs(totalHeight)), infoTextSize),
                        infoFont, textFontBrush, textBackgroundBrush, borderPen);
                }

                if (SettingsManager.RegionCaptureSettings.Draw_Magnifier)
                {
                    if (SettingsManager.RegionCaptureSettings.Draw_Border_On_Magnifier)
                        g.DrawRectangle(magnifierBorderPen, magX - 1, magY - 1, magnifier.Width + 1, magnifier.Height + 1);

                    g.DrawImage(magnifier, new Point(magX, magY));
                    magnifier.Dispose();
                }
            }
        }

        private Bitmap DrawMagnifier(Point mousePos)
        {
            int pixelCount = MathHelper.MakeOdd(((int)(SettingsManager.RegionCaptureSettings.Magnifier_Pixel_Count * SettingsManager.RegionCaptureSettings.Magnifier_Zoom_Level)).Clamp(1, 500));
            int pixelSize = MathHelper.MakeOdd(SettingsManager.RegionCaptureSettings.Magnifier_Pixel_Size.Clamp(1, 50));

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

                if (SettingsManager.RegionCaptureSettings.Draw_Pixel_Grid_In_Magnifier)
                {
                    for (int x = 1; x < pixelCount; x++)
                    {
                        gr.DrawLine(magnifierGridPen, new Point((x * pixelSize) - 1, 0), new Point((x * pixelSize) - 1, height - 1));
                    }

                    for (int y = 1; y < pixelCount; y++)
                    {
                        gr.DrawLine(magnifierGridPen, new Point(0, (y * pixelSize) - 1), new Point(width - 1, (y * pixelSize) - 1));
                    }
                }

                if (SettingsManager.RegionCaptureSettings.Draw_Crosshair_In_Magnifier)
                {
                    gr.FillRectangle(magnifierCrosshairBrush, new Rectangle(0, (height - pixelSize) / 2, (width - pixelSize) / 2, pixelSize)); // Left
                    gr.FillRectangle(magnifierCrosshairBrush, new Rectangle((width + pixelSize) / 2, (height - pixelSize) / 2, (width - pixelSize) / 2, pixelSize)); // Right
                    gr.FillRectangle(magnifierCrosshairBrush, new Rectangle((width - pixelSize) / 2, 0, pixelSize, (height - pixelSize) / 2)); // Top
                    gr.FillRectangle(magnifierCrosshairBrush, new Rectangle((width - pixelSize) / 2, (height + pixelSize) / 2, pixelSize, (height - pixelSize) / 2)); // Bottom
                }
            }
            return bmp;
        }

        private void DrawSelectionBox(Graphics g, Point mousePos)
        {
            g.FillRectangle(backgroundHighlightBrush, Helper.CreateRect(leftClickStart, mousePos));

            g.DrawLine(borderDotPen, leftClickStart, new Point(mousePos.X, leftClickStart.Y));
            g.DrawLine(borderDotPen, leftClickStart, new Point(leftClickStart.X, mousePos.Y));

            if (!SettingsManager.RegionCaptureSettings.Draw_Screen_Wide_Crosshair)
            {
                g.DrawLine(borderDotPen, new Point(mousePos.X, leftClickStart.Y), mousePos);
                g.DrawLine(borderDotPen, new Point(leftClickStart.X, mousePos.Y), mousePos);
            }
        }

        private void DrawInfoText(Graphics g, string text, Rectangle rec, Font font, Brush textFontBrush, Brush backgroundBrush, Pen outerBorderPen)
        {
            int mX = rec.X;
            int mY = rec.Y - SettingsManager.RegionCaptureSettings.Cursor_Info_Offset - rec.Height;

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

        #region events

        private void KeyDown_Event(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            switch (e.KeyData)
            {
                case Keys.Z:
                    InRegionTaskHandler(SettingsManager.RegionCaptureSettings.On_Z_Press);
                    break;

                case Keys.Escape:
                    InRegionTaskHandler(SettingsManager.RegionCaptureSettings.On_Escape_Press);
                    break;
            }
        }

        private void MouseWheel_Event(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (SettingsManager.RegionCaptureSettings.Magnifier_Zoom_Level + SettingsManager.RegionCaptureSettings.Magnifier_Zoom_Scale < 12)
                {
                    SettingsManager.RegionCaptureSettings.Magnifier_Zoom_Level += SettingsManager.RegionCaptureSettings.Magnifier_Zoom_Scale;
                }
                if (SettingsManager.RegionCaptureSettings.Magnifier_Zoom_Level > 0)
                {
                    SettingsManager.RegionCaptureSettings.Draw_Magnifier = true;
                }
            }
            else
            if (e.Delta < 0)
            {
                if (SettingsManager.RegionCaptureSettings.Magnifier_Zoom_Level - SettingsManager.RegionCaptureSettings.Magnifier_Zoom_Scale > 0)
                {
                    SettingsManager.RegionCaptureSettings.Magnifier_Zoom_Level -= 0.25f;
                }
                else
                {
                    SettingsManager.RegionCaptureSettings.Magnifier_Zoom_Level = 0;
                    SettingsManager.RegionCaptureSettings.Draw_Magnifier = false;
                }
            }
            Invalidate();
        }

        private void MouseMove_Event(object sender, MouseEventArgs e)
        {
            if (!SettingsManager.RegionCaptureSettings.Draw_Marching_Ants)
                Invalidate();
        }

        private void ClickRelease_Event(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    leftClickStop = e.Location;
                    if (mode != RegionCaptureMode.ColorPicker)
                    {
                        if (isLeftClicking && leftClickStart != leftClickStop && Helper.IsValidCropArea(leftClickStart, leftClickStop))
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
                    InRegionTaskHandler(SettingsManager.RegionCaptureSettings.On_Mouse_Right_Click);
                    break;

                case MouseButtons.Middle:
                    InRegionTaskHandler(SettingsManager.RegionCaptureSettings.On_Mouse_Middle_Click);
                    break;

                case MouseButtons.XButton1:
                    InRegionTaskHandler(SettingsManager.RegionCaptureSettings.On_XButton1_Click);
                    break;

                case MouseButtons.XButton2:
                    InRegionTaskHandler(SettingsManager.RegionCaptureSettings.On_XButton2_Click);
                    break;
            }
        }

        private void Click_Event(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isLeftClicking = true;
                leftClickStart = e.Location;
            }
        }

        #endregion

        #region overrides

        protected override void OnPaint(PaintEventArgs e)
        {

            mousePos = ScreenHelper.ScreenToClient(ScreenHelper.GetCursorPosition());
            activeMonitor = ScreenHelper.GetActiveScreenBounds0Based();

            Graphics g = e.Graphics;

            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.SmoothingMode = SmoothingMode.HighQuality; // for some reason highspeed crashes the window
            g.CompositingQuality = CompositingQuality.HighSpeed;

            g.CompositingMode = CompositingMode.SourceCopy;
            g.FillRectangle(backgroundBrush, clientArea);
            g.CompositingMode = CompositingMode.SourceOver;

            DrawMouseGraphics(g);

            if (SettingsManager.RegionCaptureSettings.Draw_Marching_Ants)
            {
                borderDotPen.DashOffset += 0.25f;
                if (borderDotPen.DashOffset > 10) borderDotPen.DashOffset = 0;
                Invalidate();
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            borderDotPen?.Dispose();
            borderPen?.Dispose();
            infoFont?.Dispose();
            magnifierBorderPen?.Dispose();
            magnifierBorderPen?.Dispose();
            magnifierCrosshairBrush?.Dispose();
            textBackgroundBrush?.Dispose();
            textFontBrush?.Dispose();

            image?.Dispose();
            backgroundBrush?.Dispose();
            backgroundHighlightBrush?.Dispose();

            base.Dispose(disposing);
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

        #endregion
    }
}
