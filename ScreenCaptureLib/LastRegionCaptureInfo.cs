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
    public class LastRegionCaptureInfo : EventArgs
    {
        public Image Image { get; private set; }
        public Color color { get; private set; }
        public Point StartLeftClick { get; private set; }
        public Point StopLeftClick { get; private set; }
        public Rectangle Region { get; private set; }
        public bool CapturedFullscreeen { get; private set; }
        public bool CapturedPrimaryMonitor { get; private set; }
        public Screen CapturedMonitor { get; private set; }
        public RegionResult Result { get; private set; }

        public LastRegionCaptureInfo(RegionResult result)
        {
            Result = result;
        }

        public LastRegionCaptureInfo(RegionResult result, Point startLeftClick = default, Point stopLeftClick = default, Rectangle region = default, Image img = null) : this(result)
        {
            StartLeftClick = startLeftClick;
            StopLeftClick = stopLeftClick;
            Region = region;
            Image = img;
        }

        public LastRegionCaptureInfo(RegionResult result, bool capturedFullscreen = false, Image img = null) : this(result, default, default, default, img)
        {
            CapturedFullscreeen = capturedFullscreen;
        }

        public LastRegionCaptureInfo(RegionResult result, Point startLeftClick = default, Point stopLeftClick = default, Color color = default) : this(result)
        {
            this.color = color;
            StartLeftClick = startLeftClick;
            StopLeftClick = stopLeftClick;
        }

        public LastRegionCaptureInfo(RegionResult result, Screen capturedMonitor = null, Image img = null) : this(result, false, img)
        {
            CapturedMonitor = capturedMonitor;
            if (capturedMonitor.Primary)
                CapturedPrimaryMonitor = true;
            else
                CapturedPrimaryMonitor = false;
        }

        public void Destroy()
        {
            Image?.Dispose();
        }
    }
}
