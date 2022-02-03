using System;
using System.Drawing;
using System.IO;

namespace WinkingCat.HelperLibs
{
    public class ImageSavedEvent : EventArgs
    {
        /// <summary>
        /// The file path.
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

        public ImageSavedEvent(FileInfo info, Size size)
        {
            this.FileInfo = info;
            this.Dimensions = size;
            this.SizeInBytes = info.Length;
        }

        public ImageSavedEvent(FileInfo info) : this (info, ImageHelper.GetImageDimensionsFromFile(info.FullName))
        {
        }        

        public ImageSavedEvent(string path) : this(new FileInfo(path))
        {
        }

        public ImageSavedEvent(string path, Size size) : this(new FileInfo(path), size)
        {
        }
    }
}
