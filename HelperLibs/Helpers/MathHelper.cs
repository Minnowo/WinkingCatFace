using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{
    public static class MathHelper
    {

        public static Size PictureBoxZoomSize(PictureBox pictureBox, Size imageSize)
        {
            double wfactor = (double)imageSize.Width / pictureBox.ClientSize.Width;
            double hfactor = (double)imageSize.Height / pictureBox.ClientSize.Height;

            double resizeFactor = Math.Max(wfactor, hfactor);
            Size newImageSize = new Size((int)(imageSize.Width / resizeFactor), (int)(imageSize.Height / resizeFactor));
            return newImageSize;
        }

        public static bool IsEven(int number)
        {
            if (number % 2 == 0)
                return true; //even number
            else
                return false; //odd number
        }
    }
}
