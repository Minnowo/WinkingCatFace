using System;
using System.Drawing;
using System.Windows.Forms;
using WinkingCat.HelperLibs;
namespace WinkingCat
{
    public class RegionReturn : EventArgs, IDisposable
    {
        /// <summary>
        /// The image captured.
        /// </summary>
        public Image Image { get; private set; }

        /// <summary>
        /// The monitor that was captured.
        /// </summary>
        public Screen CapturedMonitor { get; private set; }

        /// <summary>
        /// The color picked.
        /// </summary>
        public Color Color { get; private set; }

        /// <summary>
        /// The starting left click location.
        /// </summary>
        public Point StartLeftClick { get; private set; }

        /// <summary>
        /// The ending left click location.
        /// </summary>
        public Point StopLeftClick { get; private set; }

        /// <summary>
        /// The region that was captured.
        /// </summary>
        public Rectangle Region { get; private set; }

        /// <summary>
        /// The result type.
        /// </summary>
        public RegionResult Result { get; private set; }

        /// <summary>
        /// Was the entire screen captured.
        /// </summary>
        public bool CapturedScreenBounds { get; private set; }

        /// <summary>
        /// Was the captured monitor the primary monitor.
        /// </summary>
        public bool CapturedPrimaryMonitor { get; private set; }

        public RegionReturn(RegionResult result)
        {
            Result = result;
        }

        public RegionReturn(RegionResult result, Point startLeftClick, Point stopLeftClick, Rectangle region, Image image)
        {
            Result = result;
            StartLeftClick = startLeftClick;
            StopLeftClick = stopLeftClick;
            Region = region;
            Image = image;
        }

        public RegionReturn(RegionResult result, bool capturedFullscreen, Image image)
        {
            Result = result;
            CapturedScreenBounds = capturedFullscreen;
            Region = ScreenHelper.GetScreenBounds();
            Image = image;
        }

        public RegionReturn(Point stopLeftClick, Color color)
        {
            Result = RegionResult.Color;
            Color = color;
            StopLeftClick = stopLeftClick;
        }

        public RegionReturn(Screen capturedMonitor, Image image)
        {
            Result = RegionResult.ActiveMonitor;
            Image = image;
            CapturedMonitor = capturedMonitor;
            CapturedPrimaryMonitor = capturedMonitor.Primary;
            Region = capturedMonitor.Bounds;
        }

        public void Dispose()
        {
            Image?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
