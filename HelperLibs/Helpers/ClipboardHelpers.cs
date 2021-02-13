using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{
    public static class ClipboardHelpers
    {
        private const int RETRYTIMES = 20;
        private const int RETRYDELAY = 100;
        private const string FORMAT_PNG = "PNG";
        private const string FORMAT_17 = "Format17";

        private static readonly object ClipboardLock = new object();

        public static bool CopyData(IDataObject data, bool copy = true)
        {
            if (data != null)
            {
                lock (ClipboardLock)
                {
                    Clipboard.SetDataObject(data, copy, RETRYTIMES, RETRYDELAY);
                }

                return true;
            }

            return false;
        }

        public static bool Clear()
        {
            try
            {
                IDataObject data = new DataObject();
                CopyData(data, false);
            }
            catch (Exception e)
            {
                Logger.WriteException(e, "Clipboard clear failed.");
            }

            return false;
        }

        public static bool CopyImageDefault(Image img)
        {
            IDataObject dataObject = new DataObject();
            dataObject.SetData(DataFormats.Bitmap, true, img);

            return CopyData(dataObject);
        }
    }
}
