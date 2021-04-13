using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using WinkingCat.HelperLibs;

namespace WinkingCat.HelperLibs
{
    public class ClipOptions
    {
        public static int ZoomRefreshRate
        {
            get
            {
                return zoomRefreshRate;
            }
            set
            {
                zoomRefreshRate = value.Clamp(1, 5000);
            }
        }
        private static int zoomRefreshRate = 10;

        public static int extendBorderGrabRange { get; set; } = 1;

        public Size maxClipSize { get; set; } = new Size(5000, 5000);
        public Color borderColor { get; set; } = ApplicationStyles.currentStyle.clipStyle.borderColor;
        public Point location { get; set; } = new Point(0, 0);
        public int borderThickness { get; set; } = ApplicationStyles.currentStyle.clipStyle.borderThickness;
        public bool isDraggable { get; set; } = true;
        public bool isResizable { get; set; } = true;
        public string filePath { get; set; }
        public string uuid { get; set; }
        public DateTime date { get; set; }

    }
}
