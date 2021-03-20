using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using WinkingCat.HelperLibs;

namespace WinkingCat.ScreenCaptureLib
{
    public class ImageSavedEvent : EventArgs
    {
        public FileInfo info { get; private set; }
        public Size dimensions { get; private set; }
        public long size { get; private set; }
        public ImageSavedEvent(FileInfo info)
        {
            this.info = info;
            size = info.Length;
            dimensions = ImageHelper.GetImageDimensionsFile(info.FullName);
        }
        public ImageSavedEvent(string path) : this(new FileInfo(path))
        {

        }
    }
}
