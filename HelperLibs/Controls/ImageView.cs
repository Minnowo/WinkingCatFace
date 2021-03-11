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
    public partial class ImageView : UserControl
    {
        public Double ZoomFactor
        {
            get
            {
                return drawingBoard1.ZoomFactor;
            }
            set
            {
                drawingBoard1.ZoomFactor = value;
            }
        }

        public Point Origin
        {
            get
            {
                return drawingBoard1.Origin;
            }
            set
            {
                drawingBoard1.Origin = value;
            }
        }

        public Size ApparentImageSize
        {
            get
            {
                return drawingBoard1.ApparentImageSize;
            }
        }


        private bool scrollVisible = true;

        public ImageView()
        {
            InitializeComponent();
        }

        #region public properties

        public void FitToScreen()
        {
            drawingBoard1.FitToScreen();
        }

        public void ZoomIn()
        {
            drawingBoard1.ZoomIn();
        }

        public void ZoomOut()
        {
            drawingBoard1.ZoomOut();
        }

        #endregion
    }
}
