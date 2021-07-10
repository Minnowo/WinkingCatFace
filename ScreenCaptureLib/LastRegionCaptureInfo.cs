using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using WinkingCat.HelperLibs;

namespace WinkingCat.ScreenCaptureLib
{
    public class LastRegionCaptureInfo : EventArgs, IDisposable
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

        public LastRegionCaptureInfo(RegionResult result)
        {
            Result = result;
        }

        public LastRegionCaptureInfo(RegionResult result, Point startLeftClick, Point stopLeftClick, Rectangle region, Image image)
        {
            Result = result;
            StartLeftClick = startLeftClick;
            StopLeftClick = stopLeftClick;
            Region = region;
            Image = image;
        }

        public LastRegionCaptureInfo(RegionResult result, bool capturedFullscreen, Image image)
        {
            Result = result;
            CapturedScreenBounds = capturedFullscreen;
            Region = ScreenHelper.GetScreenBounds();
            Image = image;
        }

        public LastRegionCaptureInfo(Point stopLeftClick, Color color)
        {
            Result = RegionResult.Color;
            Color = color;
            StopLeftClick = stopLeftClick;
        }

        public LastRegionCaptureInfo(Screen capturedMonitor, Image image)
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
        }
    }
}
