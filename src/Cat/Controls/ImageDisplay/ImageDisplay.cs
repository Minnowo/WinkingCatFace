using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinkingCat.HelperLibs;
using WinkingCat.Settings;

namespace WinkingCat.Controls
{
    // regards to https://github.com/cyotek/Cyotek.Windows.Forms.ImageBox
    // some of this code has been taken from the above repository and has been modified

    public partial class ImageDisplay : UserControl
    {
        /// <summary>
        /// The default Cell color 1 for all <see cref="ImageDisplay"/> controls.
        /// </summary>
        public static Color DefaultCellColor1 { get { return Color.FromArgb(32, 32, 32); } }

        /// <summary>
        /// The default Cell color 2 for all <see cref="ImageDisplay"/> controls.
        /// </summary>
        public static Color DefaultCellColor2 { get { return Color.FromArgb(64, 64, 64); } }




        public delegate void ImageChangedEvent();
        public delegate void ZoomLevelChangedEvent(int zoomLevelPercent);
        public delegate void ImageLoadFailed_(string path);

        /// <summary>
        /// Called when the image is changed.
        /// </summary>
        public event ImageChangedEvent ImageChanged;

        /// <summary>
        /// Invoked when the zoom level is changed.
        /// </summary>
        public event ZoomLevelChangedEvent ZoomLevelChanged;

        /// <summary>
        /// Invoked when <see cref="TryLoadImage(string)"/> or <see cref="TryLoadImageAsync(string)"/> cannot load an image.
        /// </summary>
        public event ImageLoadFailed_ ImageLoadFailed;

        /// <summary>
        /// Text to be drawn in the viewport.
        /// </summary>
        public override string Text
        {
            get { return TextArgs.Text; }
            set { this._textArgs.Text = value; }
        }


        /// <summary>
        /// The ImageDrawMode to use when drawing the image.
        /// </summary>
        public ImageDrawMode DrawMode
        {
            get { return this._DrawMode; }
            set
            {
                if (this._DrawMode == value)
                    return;

                this._DrawMode = value;
                Invalidate();
            }
        }
        private ImageDrawMode _DrawMode = ImageDrawMode.FitImage;

        /// <summary>
        /// The InterpolationMode to use when drawing the image.
        /// </summary>
        public InterpolationMode InterpolationMode
        {
            get { return this._InterpolationMode; }
            set
            {
                if (this._InterpolationMode == value)
                    return;
                this._InterpolationMode = value;
                Invalidate();
            }
        }
        private InterpolationMode _InterpolationMode = InterpolationMode.NearestNeighbor;

        /// <summary>
        /// The maximum zoom percentage.
        /// </summary>
        public int MaxZoomPercent { get; set; } = 20000;

        /// <summary>
        /// The minimum zoom percentage.
        /// </summary>
        public int MinZoomPercent { get; set; } = 1;

        /// <summary>
        /// The zoom in percentage.
        /// </summary>
        public int ZoomPercent
        {
            get
            {
                return (int)(this._Zoom * 100);
            }
            set
            {
                if (value > this.MaxZoomPercent)
                    return;

                if (value < this.MinZoomPercent)
                    return;

                this._Zoom = value / 100d;

                OnZoomLevelChanged(value);
            }
        }

        /// <summary>
        /// The zoom level, 1 = 100%.
        /// </summary>
        public double Zoom
        {
            get { return _Zoom; }
            set
            {
                if (value * 100 > this.MaxZoomPercent)
                    return;

                if (value * 100 < this.MinZoomPercent)
                    return;

                _Zoom = value;
                OnZoomLevelChanged((int)(_Zoom * 100));
            }
        }
        private double _Zoom = 1;

        /// <summary>
        /// The cell size of the background tiles.
        /// </summary>
        public int CellSize
        {
            get { return this._CellSize; }
            set
            {
                if (this._CellSize == value)
                    return;

                this._CellSize = value;
                this.InitTileBrush((int)(this.CellSize * this.CellScale), this.CellColor1, this.CellColor2);
            }
        }
        private int _CellSize = 32;

        /// <summary>
        /// Multiplies the <see cref="CellSize"/>.
        /// </summary>
        public float CellScale
        {
            get { return this._CellScale; }
            set
            {
                if (this._CellScale == value)
                    return;

                this._CellScale = value;
                this.InitTileBrush((int)(this.CellSize * this.CellScale), this.CellColor1, this.CellColor2);
            }
        }
        private float _CellScale = 2;

        /// <summary>
        /// Color 1 of the background tiles.
        /// </summary>
        public Color CellColor1
        {
            get { return this._CellColor1; }
            set
            {
                if (this._CellColor1 == value)
                    return;

                this._CellColor1 = value;
                this.InitTileBrush((int)(this.CellSize * this.CellScale), this.CellColor1, this.CellColor2);
            }
        }
        private Color _CellColor1 = DefaultCellColor1;

        /// <summary>
        /// Color 2 of the background tiles.
        /// </summary>
        public Color CellColor2
        {
            get { return this._CellColor2; }
            set
            {
                if (this._CellColor2 == value)
                    return;

                this._CellColor2 = value;
                this.InitTileBrush((int)(this.CellSize * this.CellScale), this.CellColor1, this.CellColor2);
            }
        }
        private Color _CellColor2 = DefaultCellColor2;

        /// <summary>
        /// The image display in the control.
        /// The image is NOT disposed with the control.
        /// </summary>
        public IMAGE Image
        {
            get { return this._Image; }
            set
            {
                if (value != null)
                {
                    if (value.GetImageFormat() == ImgFormat.gif)
                    {
                        Gif g = value as Gif;
                        g.Animate(OnFrameChangedHandler);
                        this.IsAnimating = true;
                    }
                    else
                    {
                        this.IsAnimating = false;
                    }
                }

                if (this.DisposeImageOnReplace)
                {
                    this._Image?.Dispose();
                    if (InternalSettings.Garbage_Collect_After_Image_Display_Unload)
                    {
                        GC.Collect();
                    }
                }

                if (this.ClearImagePathOnReplace)
                {
                    this.ImagePath = null;
                }

                this._Image = value;

                if (this.CenterImage)
                {
                    this.CenterCurrentImage();
                }
                else
                {
                    this._drx = 0;
                    this._dry = 0;
                }

                Invalidate();

                OnImageChanged();
            }
        }
        private IMAGE _Image;

        /// <summary>
        /// The zoom levels of the control.
        /// </summary>
        public ZoomLevelCollection ZoomLevels = ZoomLevelCollection.Default;

        /// <summary>
        /// Text to be drawn in the viewport.
        /// </summary>
        public TextArgs TextArgs
        {
            get { return this._textArgs; }
        }
        private TextArgs _textArgs = new TextArgs();

        /// <summary>
        /// The <see cref="FileInfo"/> of the image displayed in the control (if available).
        /// </summary>
        public FileInfo ImagePath { get; set; }

        /// <summary>
        /// Controls how the zoom of the image is done.
        /// </summary>
        public ZoomMode ZoomMode = ZoomMode.IntoMouse;

        /// <summary>
        /// The button used to drag the image.
        /// </summary>
        public MouseButtons DragButton = MouseButtons.Left;

        /// <summary>
        /// The button used to reset the image x y offset.
        /// </summary>
        public MouseButtons ResetOffsetButton = MouseButtons.None;

        /// <summary>
        /// Should the control draw anything.
        /// </summary>
        public bool Display { get; set; } = true;

        /// <summary>
        /// Should <see cref=""/> be drawn.
        /// </summary>
        public bool DisplayText { get; set; } = true;

        /// <summary>
        /// Is the current image animating.
        /// </summary>
        public bool IsAnimating { get; set; } = false;

        /// <summary>
        /// Is the current animation paused.
        /// </summary>
        public bool AnimationPaused { get; set; } = false;

        /// <summary>
        /// Should the image be centered in the control.
        /// </summary>
        public bool CenterImage { get; set; } = true;

        /// <summary>
        /// Should the user be able to drag the image around. (does not affect <see cref="ImageDrawMode.ActualSize"/>)
        /// </summary>
        public bool AllowDrag { get; set; } = true;

        /// <summary>
        /// Should the image be disposed when the <see cref="Image"/> is changed.
        /// </summary>
        public bool DisposeImageOnReplace { get; set; } = true;

        /// <summary>
        /// Should the <see cref="ImagePath"/> be set null when the image is changed.
        /// </summary>
        public bool ClearImagePathOnReplace { get; set; } = true;


        /// <summary>
        /// Brush used to draw the background grid
        /// </summary>
        Brush BackgroundTileBrush;

        /// <summary>
        /// used when dragging the image
        /// </summary>
        Point _lastClickedPoint;

        /// <summary>
        /// Is the user left clicking
        /// </summary>
        bool _isDragButtonDown = false;

        /// <summary>
        /// x offset to draw the image when the image is not being centered 
        /// </summary>
        int _drx = 0;

        /// <summary>
        /// y offset to draw the image when the image is not being centered
        /// </summary>
        int _dry = 0;


        public ImageDisplay()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.ResizeRedraw, true);

            this.AutoScaleMode = AutoScaleMode.Font;

            this.Width = 250;
            this.Height = 250;

            this.InitTileBrush((int)(this.CellSize * this.CellScale), this.CellColor1, this.CellColor2);
        }











        /// <summary>
        /// Sets the tile brush used to drag the background grid.
        /// </summary>
        /// <param name="cellSize">The size of each square.</param>
        /// <param name="cellColor">The color of half the squares.</param>
        /// <param name="alternateCellColor">The other color of the squares.</param>
        public void InitTileBrush(int cellSize, Color cellColor, Color alternateCellColor)
        {
            Bitmap result;
            int width;
            int height;

            // draw the tile
            width = cellSize * 2;
            height = cellSize * 2;
            result = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(result))
            {
                using (Brush brush = new SolidBrush(cellColor))
                {
                    g.FillRectangle(brush, new Rectangle(cellSize, 0, cellSize, cellSize));
                    g.FillRectangle(brush, new Rectangle(0, cellSize, cellSize, cellSize));
                }

                using (Brush brush = new SolidBrush(alternateCellColor))
                {
                    g.FillRectangle(brush, new Rectangle(0, 0, cellSize, cellSize));
                    g.FillRectangle(brush, new Rectangle(cellSize, cellSize, cellSize, cellSize));
                }
            }

            this.BackgroundTileBrush?.Dispose();
            this.BackgroundTileBrush = new TextureBrush(result);
            this.Invalidate();
        }

        /// <summary>
        /// Centers the image based off the drawmode of the control
        /// </summary>
        public void CenterCurrentImage()
        {
            if (this.Image == null)
                return;

            int width = this._Image.Width;
            int height = this._Image.Height;

            switch (this.DrawMode)
            {
                case ImageDrawMode.FitImage:
                case ImageDrawMode.Resizeable:

                    this.FitImage();
                    width = (int)Math.Round(width * this._Zoom);
                    height = (int)Math.Round(height * this._Zoom);

                    this._drx = (this.ClientSize.Width >> 1) - (width >> 1);
                    this._dry = (this.ClientSize.Height >> 1) - (height >> 1);
                    break;

                case ImageDrawMode.ActualSize:

                    if (width < this.ClientSize.Width)
                    {
                        this._drx = (this.ClientSize.Width >> 1) - (width >> 1);
                    }

                    if (height < this.ClientSize.Height)
                    {
                        this._dry = (this.ClientSize.Height >> 1) - (height >> 1);
                    }
                    break;

                case ImageDrawMode.DownscaleImage:
                    if (width > this.ClientSize.Width || height > this.ClientSize.Height)
                    {
                        this.FitImage();

                        width = (int)Math.Round(this._Image.Width * this._Zoom);
                        height = (int)Math.Round(this._Image.Height * this._Zoom);
                    }

                    this._drx = (this.Width >> 1) - (width >> 1);
                    this._dry = (this.Height >> 1) - (height >> 1);
                    break;
            }

            Invalidate();
        }

        /// <summary>
        /// Resets the image x, y draw offset
        /// </summary>
        public void ResetOffsets()
        {
            _drx = 0;
            _dry = 0;
            Invalidate();
        }

        /// <summary>
        /// Copies the current image to the clipboard.
        /// </summary>
        public void CopyImage()
        {
            if (_Image == null)
                return;

            ClipboardHelper.CopyImage(_Image);
        }

        /// <summary>
        /// Inverts the color of the current image.
        /// </summary>
        public void InvertColor()
        {
            if (_Image == null)
                return;

            _Image.InvertColor();
            Invalidate();
        }

        /// <summary>
        /// Converts the current image to grayscale.
        /// </summary>
        public void ConvertGrayscale()
        {
            if (_Image == null)
                return;

            _Image.ConvertGrayscale();
            Invalidate();
        }

        /// <summary>
        /// Reloads the current image if the <see cref="ImagePath"/> exists.
        /// </summary>
        public void ReloadImage()
        {
            if (this.ImagePath == null || !File.Exists(this.ImagePath.FullName))
                return;

            IMAGE i = ImageHelper.LoadImage(this.ImagePath.FullName);

            if (i == null)
                return;

            bool _ = this.DisposeImageOnReplace;
            bool __ = this.ClearImagePathOnReplace;

            this.DisposeImageOnReplace = true;
            this.ClearImagePathOnReplace = false;

            this.Image = i;

            this.DisposeImageOnReplace = _;
            this.ClearImagePathOnReplace = __;
        }

        /// <summary>
        /// Tries to load an image from the given path and sets the <see cref="ImagePath"/>.
        /// </summary>
        /// <param name="path">The path of the image.</param>
        /// <returns>true if the image was loaded, else false.</returns>
        public bool TryLoadImage(string path)
        {
            if (!File.Exists(path))
                return false;

            IMAGE i = ImageHelper.LoadImage(path);

            if (i == null)
            {
                this.OnLoadFailed(path);
                return false;
            }

            bool _ = this.DisposeImageOnReplace;
            this.DisposeImageOnReplace = true;

            this.Image = i;
            this.ImagePath = new FileInfo(path);

            this.DisposeImageOnReplace = _;
            return true;
        }

        /// <summary>
        /// Tries to load an image from the given path and sets the <see cref="ImagePath"/>.
        /// </summary>
        /// <param name="path">The path of the image.</param>
        /// <returns>true if the image was loaded, else false.</returns>
        public async Task<bool> TryLoadImageAsync(string path)
        {
            if (!File.Exists(path))
                return false;

            IMAGE i = await ImageHelper.LoadImageAsync(path);

            if (i == null)
            {
                this.OnLoadFailed(path);
                return false;
            }

            bool _ = this.DisposeImageOnReplace;
            this.DisposeImageOnReplace = true;

            this.Image = i;
            this.ImagePath = new FileInfo(path);

            this.DisposeImageOnReplace = _;
            return true;
        }











        /// <summary>
        /// Gets the control size.
        /// </summary>
        /// <param name="includePadding">Should padding be subtracted from the client size.</param>
        /// <returns>The size of the control with or without padding.</returns>
        protected Rectangle GetInsideViewPort(bool includePadding)
        {
            int left = 0;
            int top = 0;
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;

            if (includePadding)
            {
                left += this.Padding.Left;
                top += this.Padding.Top;
                width -= this.Padding.Horizontal;
                height -= this.Padding.Vertical;
            }

            return new Rectangle(left, top, width, height);
        }

        /// <summary>
        /// sets the zoom level to fit the image within the control, while keeping aspect ratio
        /// </summary>
        protected void FitImage()
        {
            if (this._Image == null)
                return;

            Rectangle innerRectangle;
            double zoom;
            double aspectRatio;

            this.AutoScrollMinSize = Size.Empty;

            innerRectangle = this.GetInsideViewPort(true);

            if (this._Image.Width > this._Image.Height)
            {
                aspectRatio = (double)innerRectangle.Width / this._Image.Width;
                zoom = aspectRatio;

                if (innerRectangle.Height < this._Image.Height * zoom)
                {
                    aspectRatio = (double)innerRectangle.Height / this._Image.Height;
                    zoom = aspectRatio;
                }
            }
            else
            {
                aspectRatio = (double)innerRectangle.Height / this._Image.Height;
                zoom = aspectRatio;

                if (innerRectangle.Width < this._Image.Width * zoom)
                {
                    aspectRatio = (double)innerRectangle.Width / this._Image.Width;
                    zoom = aspectRatio;
                }
            }

            this._Zoom = zoom;
            OnZoomLevelChanged((int)(zoom * 100));
        }

        /// <summary>
        /// Gets the destination rectangle to draw the image
        /// </summary>
        /// <returns></returns>
        protected Rectangle GetImageViewPort()
        {
            if (this._Image == null)
                return Rectangle.Empty;

            int xPos = _drx;
            int yPos = _dry;

            int width = this._Image.Width;
            int height = this._Image.Height;


            switch (this.DrawMode)
            {
                case ImageDrawMode.Resizeable:

                    width = (int)Math.Round(width * this._Zoom);
                    height = (int)Math.Round(height * this._Zoom);

                    // prevent the image from getting lost off the left and top side of the control
                    if (_drx < -width)
                    {
                        xPos = -width;
                        _drx = xPos;
                    }

                    if (_dry < -height)
                    {
                        yPos = -height;
                        _dry = yPos;
                    }

                    return new Rectangle(xPos, yPos, width, height);

                case ImageDrawMode.ActualSize:

                    if (this.CenterImage)
                    {
                        // center the image, using bitshifting cause i can
                        // else
                        // prevent the image from getting lost off the left and top of the control
                        if (width < this.ClientSize.Width)
                        {
                            //     (this.ClientSize.Width / 2) - (width / 2);
                            xPos = (this.ClientSize.Width >> 1) - (width >> 1);
                        }
                        else if (xPos < -width)
                        {
                            xPos = -width;
                            _drx = xPos;
                        }

                        if (height < this.ClientSize.Height)
                        {
                            yPos = (this.ClientSize.Height >> 1) - (height >> 1);
                        }
                        else if (yPos < -height)
                        {
                            yPos = -height;
                            _dry = yPos;
                        }
                    }
                    else
                    {
                        if (width < this.ClientSize.Width)
                        {
                            xPos = 0;
                            _drx = 0;
                        }

                        if (height < this.ClientSize.Height)
                        {
                            yPos = 0;
                            _dry = 0;
                        }
                    }


                    return new Rectangle(xPos, yPos, width, height);

                case ImageDrawMode.FitImage:
                    FitImage();

                    width = (int)Math.Round(width * this._Zoom);
                    height = (int)Math.Round(height * this._Zoom);

                    if (this.CenterImage)
                    {
                        //     (this.Width / 2) - (width / 2)
                        xPos = (this.Width >> 1) - (width >> 1);
                        yPos = (this.Height >> 1) - (height >> 1);
                    }
                    else
                    {
                        xPos = 0;
                        yPos = 0;
                    }

                    return new Rectangle(xPos, yPos, width, height);

                case ImageDrawMode.DownscaleImage:

                    if (width > this.ClientSize.Width || height > this.ClientSize.Height)
                    {
                        FitImage();

                        width = (int)Math.Round(width * this._Zoom);
                        height = (int)Math.Round(height * this._Zoom);
                    }

                    if (this.CenterImage)
                    {
                        xPos = (this.ClientSize.Width >> 1) - (width >> 1);
                        yPos = (this.ClientSize.Height >> 1) - (height >> 1);
                    }
                    else
                    {
                        xPos = 0;
                        yPos = 0;
                    }

                    return new Rectangle(xPos, yPos, width, height);
            }

            return Rectangle.Empty;
        }

        /// <summary>
        /// Gets a <see cref="RectangleF"/> formatted with X, Y, Width, Height = 0, 0, Image.Width, Image.Height
        /// </summary>
        protected RectangleF GetSourceImageRegion()
        {
            if (this._Image == null)
                return RectangleF.Empty;

            return new RectangleF(0, 0, this._Image.Width, this._Image.Height);
        }

        /// <summary>
        /// Draws the image.
        /// </summary>
        /// <param name="g"></param>
        protected void DrawImage(Graphics g)
        {
            InterpolationMode currentInterpolationMode = g.InterpolationMode;
            PixelOffsetMode currentPixelOffsetMode = g.PixelOffsetMode;

            g.InterpolationMode = this.InterpolationMode;

            // disable pixel offsets. Thanks to Rotem for the info.
            // http://stackoverflow.com/questions/14070311/why-is-graphics-drawimage-cropping-part-of-my-image/14070372#14070372
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            try
            {
                if (this.IsAnimating && !this.AnimationPaused)
                {
                    ImageAnimator.UpdateFrames(this._Image);
                }

                g.DrawImage(this._Image, this.GetImageViewPort(), this.GetSourceImageRegion(), GraphicsUnit.Pixel);
            }
            catch
            {
            }
            finally
            {
                g.PixelOffsetMode = currentPixelOffsetMode;
                g.InterpolationMode = currentInterpolationMode;
            }
        }

        /// <summary>
        /// Draws the background texture.
        /// </summary>
        /// <param name="g"></param>
        protected void DrawBackground(Graphics g)
        {
            g.FillRectangle(this.BackgroundTileBrush, this.GetInsideViewPort(false));
        }

        /// <summary>
        /// Draws text.
        /// </summary>
        protected virtual void DrawText(Graphics g, TextArgs text)
        {
            if (text.AutoBounds)
                text.Bounds = this.GetInsideViewPort(true);

            DrawText(g, text.Text, text.Font, text.ForeColor, text.BackColor, text.Bounds, text.ScaleText);
        }

        /// <summary>
        /// Draws text.
        /// </summary>
        protected virtual void DrawText(Graphics g, string text, Font font, Color foreColor, Color backColor, Rectangle bounds, bool scaleText)
        {
            if (scaleText)
            {
                font = new Font(font.FontFamily, (float)(font.Size * this._Zoom), font.Style);
            }

            TextRenderer.DrawText(g, text, font, bounds, foreColor, backColor,
                TextFormatFlags.NoPrefix |
                TextFormatFlags.WordEllipsis |
                TextFormatFlags.WordBreak |
                TextFormatFlags.NoPadding);

            if (scaleText)
            {
                font.Dispose();
            }
        }

        /// <summary>
        /// Handles mouse adjusting of the zoomlevel for zooming in and out.
        /// </summary>
        /// <param name="isZoomIn">Is the zoom in or out.</param>
        protected void MouseZoom(bool isZoomIn)
        {
            int newZoom;
            int currentZoom = this.ZoomPercent;

            if (isZoomIn)
            {
                newZoom = this.ZoomLevels.NextZoom(currentZoom);
            }
            else
            {
                newZoom = this.ZoomLevels.PreviousZoom(currentZoom);
            }

            if (newZoom == currentZoom)
                return;

            this.ZoomPercent = newZoom;
        }

        /// <summary>
        /// Zooms the image into the point of the mouse position
        /// 
        /// credits to: https://stackoverflow.com/a/61964222
        /// </summary>
        protected void ZoomIntoMousePosition(double beforeZoom, double afterZoom, Point mousePosition)
        {
            /*
                        double oldWidth = _Image.Width * beforeZoom;
                        double newWidth = _Image.Width * afterZoom;

                        double scaleRatio = oldWidth / newWidth;

                        double mouseOffsetX = mousePosition.X - _drx;
                        double mouseOffsetY = mousePosition.Y - _dry;

                        double scaledOffsetX = mouseOffsetX / scaleRatio;
                        double scaledOffsetY = mouseOffsetY / scaleRatio;

                        double x = _drx - (scaledOffsetX - mouseOffsetX);
                        double y = _dry - (scaledOffsetY - mouseOffsetY);

                        _drx = (int)Math.Round(x);
                        _dry = (int)Math.Round(y);
            */

            double scaleRatio = (_Image.Width * beforeZoom) / (_Image.Width * afterZoom);

            double mouseOffsetX = mousePosition.X - _drx;
            double mouseOffsetY = mousePosition.Y - _dry;

            _drx = (int)Math.Round(_drx - ((mouseOffsetX / scaleRatio) - mouseOffsetX));
            _dry = (int)Math.Round(_dry - ((mouseOffsetY / scaleRatio) - mouseOffsetY));
        }

        /// <summary>
        /// Zooms the image at the bottom right corner.
        /// </summary>
        protected void ZoomBottomRightImage(double beforeZoom, double afterZoom)
        {
            double beforeZoomWidth = _Image.Width * beforeZoom;
            double beforeZoomHeight = _Image.Height * beforeZoom;

            double afterZoomWidth = _Image.Width * afterZoom;
            double afterZoomHeight = _Image.Height * afterZoom;

            this._drx -= (int)Math.Round(afterZoomWidth - beforeZoomWidth);
            this._dry -= (int)Math.Round(afterZoomHeight - beforeZoomHeight);
        }

        /// <summary>
        /// Zooms at the center image.
        /// </summary>
        protected void ZoomCenterImage(double beforeZoom, double afterZoom)
        {
            double beforeZoomWidth = _Image.Width * beforeZoom;
            double beforeZoomHeight = _Image.Height * beforeZoom;

            double afterZoomWidth = _Image.Width * afterZoom;
            double afterZoomHeight = _Image.Height * afterZoom;

            this._drx -= (int)Math.Round(afterZoomWidth - beforeZoomWidth) >> 1;
            this._dry -= (int)Math.Round(afterZoomHeight - beforeZoomHeight) >> 1;
        }

        /// <summary>
        /// ZoomCenterImage but at the mouse position.
        /// </summary>
        protected void ZoomCenterMouse(double afterZoom, Point mousePosition)
        {
            double afterZoomWidth = _Image.Width * afterZoom;
            double afterZoomHeight = _Image.Height * afterZoom;

            this._drx = (int)Math.Round(mousePosition.X - (afterZoomWidth / 2));
            this._dry = (int)Math.Round(mousePosition.Y - (afterZoomHeight / 2));
        }









        private void OnImageChanged()
        {
            if (ImageChanged != null)
                ImageChanged.Invoke();
        }

        private void OnZoomLevelChanged(int zoomLevelPercent)
        {
            if (ZoomLevelChanged != null)
                ZoomLevelChanged.Invoke(zoomLevelPercent);
        }

        private void OnFrameChangedHandler(object sender, EventArgs eventArgs)
        {
            if (this.AnimationPaused)
                return;

            this.Invalidate();
        }

        private void OnLoadFailed(string path)
        {
            if (this.ImageLoadFailed != null)
                this.ImageLoadFailed.Invoke(path);
        }














        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (this._Image == null)
                return;

            if (this.DrawMode != ImageDrawMode.Resizeable)
                return;


            // The MouseWheel event can contain multiple "spins" of the wheel so we need to adjust accordingly
            int spins = Math.Abs(e.Delta / SystemInformation.MouseWheelScrollDelta);

            // TODO: Really should update the source method to handle multiple increments rather than calling it multiple times
            for (int i = 0; i < spins; i++)
            {
                double beforeZoom = this._Zoom;

                this.MouseZoom(e.Delta > 0);

                switch (ZoomMode)
                {
                    case ZoomMode.TopLeftImage:
                        break;

                    case ZoomMode.BottomRightImage:
                        ZoomBottomRightImage(beforeZoom, this._Zoom);
                        break;

                    case ZoomMode.IntoMouse:
                        ZoomIntoMousePosition(beforeZoom, this._Zoom, e.Location);
                        break;

                    case ZoomMode.CenterImage:
                        ZoomCenterImage(beforeZoom, this._Zoom);
                        break;

                    case ZoomMode.CenterMouse:
                        ZoomCenterMouse(this._Zoom, e.Location);
                        break;
                }
            }

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == this.ResetOffsetButton)
            {
                this._drx = 0;
                this._dry = 0;
                Invalidate();
                return;
            }

            if (this.DrawMode != ImageDrawMode.ActualSize && !this.AllowDrag)
                return;

            if (e.Button == this.DragButton)
            {
                this._lastClickedPoint = e.Location;     // set last position
                this._isDragButtonDown = true;           // enable drag
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == this.DragButton)
            {
                this._isDragButtonDown = false;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!this._isDragButtonDown)
                return;

            if (this.DrawMode != ImageDrawMode.Resizeable &&
                this.DrawMode != ImageDrawMode.ActualSize)
                return;

            _drx -= _lastClickedPoint.X - e.X;
            _dry -= _lastClickedPoint.Y - e.Y;

            if (_drx > this.ClientSize.Width)
                _drx = this.ClientSize.Width;

            if (_dry > this.ClientSize.Height)
                _dry = this.ClientSize.Height;

            // set the new last click pos and redraw the image
            _lastClickedPoint = e.Location;

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Display)
            {
                this.DrawBackground(e.Graphics);
                this.DrawImage(e.Graphics);

                if (this.DisplayText)
                {
                    this.DrawText(e.Graphics, this.TextArgs);
                }
            }
            base.OnPaint(e);
        }

        protected override void Dispose(bool disposing)
        {

            BackgroundTileBrush?.Dispose();

            base.Dispose(disposing);
        }

    }
}
