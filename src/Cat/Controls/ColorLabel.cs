using System;
using System.Drawing;
using System.Windows.Forms;
namespace WinkingCat.Controls
{
    public partial class ColorLabel : UserControl
    {
        public Color StaticBackColor
        {
            get
            {
                return staticBackColor;
            }
            set
            {
                staticBackColor = value;
                this.BackColor = value;
            }
        }
        private Color staticBackColor;

        public ColorLabel()
        {
            InitializeComponent();
            staticBackColor = this.BackColor;
            this.BackColorChanged += ColorLabel_BackColorChanged;
        }

        private void ColorLabel_BackColorChanged(object sender, EventArgs e)
        {
            this.BackColor = staticBackColor;
        }
    }
}
