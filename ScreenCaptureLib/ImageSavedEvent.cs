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
        /// <summary>
        /// The file name.
        /// </summary>
        public string Name
        {
            get { return FileInfo.Name; }
        }

        /// <summary>
        /// The full path of the file.
        /// </summary>
        public string FullName
        {
            get { return FileInfo.FullName; }
        }

        /// <summary>
        /// The FileInfo of the saved image.
        /// </summary>
        public FileInfo FileInfo { get; private set; }

        /// <summary>
        /// The width and height of the image.
        /// </summary>
        public Size Dimensions { get; private set; }

        /// <summary>
        /// The size in bytes.
        /// </summary>
        public long SizeInBytes { get; private set; }

        public ImageSavedEvent(FileInfo info)
        {
            this.FileInfo = info;
            SizeInBytes = info.Length;
            Dimensions = ImageHelper.GetImageDimensionsFromFile(info.FullName);
        }

        public ImageSavedEvent(string path) : this(new FileInfo(path))
        {

        }
    }
}
