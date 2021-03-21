using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
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
