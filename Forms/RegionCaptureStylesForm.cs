using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinkingCat.HelperLibs;

namespace WinkingCat
{
    public partial class RegionCaptureStylesForm : Form
    {
        public RegionCaptureStylesForm()
        {
            InitializeComponent();

            propertyGrid1.SelectedObject = new AppstyleTEST();
        }
    }
}
