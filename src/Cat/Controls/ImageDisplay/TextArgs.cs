using System.Drawing;

namespace WinkingCat.Controls
{
    public class TextArgs
    {
        /// <summary>
        /// Text to be rendered.
        /// </summary>
        public string Text;

        /// <summary>
        /// Font to render.
        /// </summary>
        public Font Font;

        /// <summary>
        /// The text color.
        /// </summary>
        public Color ForeColor;

        /// <summary>
        /// The background color.
        /// </summary>
        public Color BackColor;

        /// <summary>
        /// The bounds of the text.
        /// </summary>
        public Rectangle Bounds;

        /// <summary>
        /// Should the text scale with the zoom level.
        /// </summary>
        public bool ScaleText = false;

        /// <summary>
        /// Let the user of the object set the bounds automatically.
        /// </summary>
        public bool AutoBounds = false;

        public TextArgs()
        {
            this.Text = "";
            this.Font = new Font(FontFamily.GenericSansSerif, 16, FontStyle.Regular);
            this.ForeColor = Color.FromArgb(0, 0, 0);
            this.BackColor = Color.FromArgb(255, 255, 255);
            this.Bounds = Rectangle.Empty;
            this.ScaleText = false;
        }


    }
}
