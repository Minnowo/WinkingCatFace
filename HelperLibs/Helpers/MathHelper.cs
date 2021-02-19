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
        public static bool IsNumericType(this object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
        public static Size PictureBoxZoomSize(PictureBox pictureBox, Size imageSize)
        {
            double wfactor = (double)imageSize.Width / pictureBox.ClientSize.Width;
            double hfactor = (double)imageSize.Height / pictureBox.ClientSize.Height;

            double resizeFactor = Math.Max(wfactor, hfactor);
            Size newImageSize = new Size((int)(imageSize.Width / resizeFactor), (int)(imageSize.Height / resizeFactor));
            return newImageSize;
        }

        public static int MakeEven(int input, bool roundUp = true)
        {
            if (IsEven(input))
            {
                return input;
            }
            else
            {
                if (roundUp)
                    return input + 1;
                else
                    return input - 1;
            }
        }

        public static int MakeOdd(int input, bool roundUp = true)
        {
            if (IsEven(input))
            {
                if (roundUp)
                    return input + 1;
                else
                    return input - 1;
            }
            else
            {
                return input;
            }
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
