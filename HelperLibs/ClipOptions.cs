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
        //https://social.msdn.microsoft.com/Forums/windows/en-US/a1843fa0-64f3-41f7-a117-a28b8d83dd12/windows-form-larger-than-screen-size?forum=winformsdesigner
        // you can't have a window greater than the screen size + 12
        public static Size MaxWinSize
        {
            get
            {
                Rectangle r = ScreenHelper.GetScreenBounds();
                return new Size(r.Width + 12, r.Height + 12);
            }
        }

        public static int ZoomRefreshRate
        {
            get { return zoomRefreshRate; }
            set { zoomRefreshRate = value.Clamp(1, 5000); }
        }
        private static int zoomRefreshRate = 10;

        public static int ExtendBorderGrabRangePixels = 1;
        public static bool ForceAspectRatio = true;
        
        public int BorderThickness;
        public string FilePath;
        public string Name;
        public DateTime DateCreated;
        public Size MaxSize;
        public Color Color;
        public Point Location = Point.Empty;

        public ClipOptions()
        {
            DateCreated = DateTime.Now;
            Name = Guid.NewGuid().ToString();
            Color = ApplicationStyles.currentStyle.clipStyle.borderColor;
            BorderThickness = ApplicationStyles.currentStyle.clipStyle.borderThickness;

            if (ForceAspectRatio)
                MaxSize = MaxWinSize;
            else
                MaxSize = new Size(5000, 5000);
        }

        public ClipOptions(Point locataion) : this()
        {
            Location = locataion;
        }
    }
}
