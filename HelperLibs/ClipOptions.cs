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
        public Size maxClipSize { get; set; } = new Size(5000, 5000);
        public Color borderColor { get; set; } = ApplicationStyles.currentStyle.clipStyle.clipBorderColor;//Color.FromArgb(249, 0, 187);
        public Point location { get; set; } = new Point(0, 0);
        public int borderThickness { get; set; } = 2;
        public bool isDraggable { get; set; } = true;
        public bool isResizable { get; set; } = true;

    }
}
