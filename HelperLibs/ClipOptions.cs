using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WinkingCat.HelperLibs
{
    public class ClipOptions
    {
        public Size maxClipSize { get; set; } = new Size(9999, 9999);
        public Color borderColor { get; set; } = Color.FromArgb(249, 0, 187);
        public Point location { get; set; } = new Point(0, 0);
        public int borderThickness { get; set; } = 2;
        public bool isDraggable { get; set; } = true;
        public bool isResizable { get; set; } = true;

    }
}
