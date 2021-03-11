using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{
    public partial class Form1 : Form
    {
        public Form1(Bitmap img)
        {
            InitializeComponent();
            this.drawingBoard1.Image = img;
        }
    }
}
