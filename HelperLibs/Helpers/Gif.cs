using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace WinkingCat.HelperLibs
{
    /// <summary>
    /// Uses default .net GIF encoding and adds animation headers.
    /// </summary>
    // code taken from https://stackoverflow.com/a/32809759
    public class Gif : IDisposable, IEnumerable<Image>
    {
        #region Header Constants

        const byte DefaultDelay = 250;
        const byte FileTrailer = 0x3b;
        const byte ApplicationBlockSize = 0x0b;
        const byte GraphicControlExtensionBlockSize = 0x04;

        const int ApplicationExtensionBlockIdentifier = 0xff21;
        const int GraphicControlExtensionBlockIdentifier = 0xf921;

        const long SourceGlobalColorInfoPosition = 10;
        const long SourceGraphicControlExtensionPosition = 781;
        const long SourceGraphicControlExtensionLength = 8;
        const long SourceImageBlockPosition = 789;
        const long SourceImageBlockHeaderLength = 11;
        const long SourceColorBlockPosition = 13;
        const long SourceColorBlockLength = 768;

        const string ApplicationIdentification = "NETSCAPE2.0";
        const string FileType = "GIF";
        const string FileVersion = "89a";

        #endregion

        /// <summary>
        /// The number of frames.
        /// </summary>
        public int Count
        {
            get
            {
                return Frames.Count;
            }
        }

        /// <summary>
        /// Default width in pixels.
        /// </summary>
        public int DefaultWidth { get; set; }

        /// <summary>
        /// Default height in pixels.
        /// </summary>
        public int DefaultHeight { get; set; }

        /// <summary>
        /// Default frame delay in Milliseconds.
        /// </summary>
        public int DefaultFrameDelay { get; set; }

        /// <summary>
        /// Repeat count for images.
        /// </summary>
        public int Repeat { get; private set; }

        private List<GifFrame> Frames = new List<GifFrame>();
        private MemoryStream memStream = null;

        /// <summary>
        /// Create an empty gif.
        /// </summary>
        public Gif()
        {
            DefaultFrameDelay = DefaultDelay;
        }

        // <summary>
        /// Load a gif from a file.
        /// </summary>
        /// <param name="file"> The path to the file. </param>
        public Gif(Bitmap file)
        {
            if (!ImageAnimator.CanAnimate(file))
                throw new Exception("Cannot animate this bitmap, try setting a frame to this image instead");
            
            try
            {
                memStream = new MemoryStream(); // keep track of our stream to dispose later
                file.Save(memStream, ImageFormat.Gif); // save the bitmap data to the stream
                this.InitFromStream(memStream); 
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Load a gif from a file.
        /// </summary>
        /// <param name="file"> The path to the file. </param>
        public Gif(string file)
        {
            if (!File.Exists(file))
                throw new Exception("The given file does not exists");

            try
            {
                memStream = new MemoryStream(File.ReadAllBytes(file)); // keep track of our stream to dispose later
                this.InitFromStream(memStream); // don't lock the file
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Create a gif from a stream.
        /// </summary>
        /// <param name="InStream"> The stream of the file. </param>
        /// <param name="Delay"> The frame delay in milliseconds, -1 to try getting delay from the gif if this fails 250 default. </param>
        /// <param name="Repeat"> The repeat count for images. </param>
        public Gif(Stream InStream, int Delay = -1, int Repeat = 0)
        {
            this.InitFromStream(InStream, Delay, Repeat);
        }

        #region Sub classes

        private class GifFrame
        {
            public Image Image;
            public double Delay;
            public int XOffset, YOffset;

            public GifFrame(Image image, double delay, int xOffset, int yOffset)
            {
                Image = image;
                Delay = delay;
                XOffset = xOffset;
                YOffset = yOffset;
            }
        }

        #endregion

        /// <summary>
        /// Adds a frame to this animation.
        /// </summary>
        /// <param name="Image"> The image to add. </param>
        /// <param name="XOffset"> The positioning x offset this image should be displayed at. </param>
        /// <param name="YOffset"> The positioning y offset this image should be displayed at. </param>
        public void AddFrame(Image Image, double? frameDelay = null, int XOffset = 0, int YOffset = 0)
        {
            Frames.Add(new GifFrame(Image, frameDelay ?? DefaultFrameDelay, XOffset, YOffset));
        }

        /// <summary>
        /// Adds a frame to this animation.
        /// </summary>
        /// <param name="FilePath"> The path of the image to add. </param>
        /// <param name="frameDelay"> The frame delay. </param>
        /// <param name="XOffset"> The positioning x offset this image should be displayed at. </param>
        /// <param name="YOffset"> The positioning y offset this image should be displayed at. </param>
        public void AddFrame(string FilePath, double? frameDelay = null, int XOffset = 0, int YOffset = 0)
        {
            AddFrame(new Bitmap(FilePath), frameDelay, XOffset, YOffset);
        }

        /// <summary>
        /// Removes a frame from at the specific index.
        /// </summary>
        /// <param name="Index"></param>
        public void RemoveAt(int Index)
        {
            if (Index < 0 || Index > Frames.Count - 1)
                return;

            Frames[Index].Image.Dispose();
            Frames.RemoveAt(Index);
        }

        /// <summary>
        /// Saves this animation to a file.
        /// </summary>
        /// <param name="path"> The path to save the file. (will overwrite if the file already exists) </param>
        public void Save(string path)
        {
            using (Stream s = new FileStream(path, FileMode.Create))
            {
                Save(s);
            }
        }

        /// <summary>
        /// Save this animation to a file.
        /// </summary>
        /// <param name="OutStream"> The stream to write with. </param>
        public void Save(Stream OutStream)
        {
            using (BinaryWriter Writer = new BinaryWriter(OutStream))
            {
                for (int i = 0; i < Count; ++i)
                {
                    GifFrame Frame = Frames[i];

                    using (MemoryStream gifStream = new MemoryStream())
                    {
                        Frame.Image.Save(gifStream, ImageFormat.Gif);

                        if (i == 0)
                            InitHeader(gifStream, Writer, Frame.Image.Width, Frame.Image.Height);

                        WriteGraphicControlBlock(gifStream, Writer, Frame.Delay);
                        WriteImageBlock(gifStream, Writer, i != 0, Frame.XOffset, Frame.YOffset, Frame.Image.Width, Frame.Image.Height);
                    }
                }

                Writer.Write(FileTrailer);
            }
        }

        /// <summary>
        /// Convert the gif into a Bitmap object.
        /// </summary>
        /// <returns></returns>
        public Bitmap ToBitmap()
        {
            // this is kinda messy but whatever it does the job
            Stream s = new MemoryStream();
            BinaryWriter Writer = new BinaryWriter(s);

            for (int i = 0; i < Count; ++i)
            {
                GifFrame Frame = Frames[i];

                using (MemoryStream gifStream = new MemoryStream())
                {
                    Frame.Image.Save(gifStream, ImageFormat.Gif);

                    if (i == 0)
                        InitHeader(gifStream, Writer, Frame.Image.Width, Frame.Image.Height);

                    WriteGraphicControlBlock(gifStream, Writer, Frame.Delay);
                    WriteImageBlock(gifStream, Writer, i != 0, Frame.XOffset, Frame.YOffset, Frame.Image.Width, Frame.Image.Height);
                }
            }

            return (Bitmap)Bitmap.FromStream(s);
        }

        /// <summary>
        /// Clears the animation.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < Frames.Count; i++)
            {
                Frames[i].Image.Dispose();
            }

            Frames.Clear();
        }

        /// <summary>
        /// Dispose of the animation.
        /// </summary>
        public void Dispose()
        {
            Clear();
            Frames = null;

            if (memStream != null)
                memStream.Dispose();
        }

        public Image this[int Index]
        {
            get
            {
                return Frames[Index].Image;
            }
        }

        public IEnumerator<Image> GetEnumerator()
        {
            foreach (GifFrame Frame in Frames)
                yield return Frame.Image;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region Priavet bois

        private int GetDelayFromPropertyItems(Image im)
        {
            try
            {
                // https://stackoverflow.com/a/3785231
                PropertyItem item = im.GetPropertyItem(0x5100);
                return (item.Value[0] + item.Value[1] * 256) * 10;
            }
            catch
            {
                return DefaultDelay;
            }
        }

        private void InitFromStream(Stream InStream, int Delay = -1, int Repeat = 0)
        {
            using (Image Animation = Bitmap.FromStream(InStream))
            {
                int Length = Animation.GetFrameCount(FrameDimension.Time);
                this.Repeat = Repeat;

                if (Delay == -1)
                {
                    this.DefaultFrameDelay = GetDelayFromPropertyItems(Animation);
                }
                else
                {
                    this.DefaultFrameDelay = Delay;
                }

                for (int i = 0; i < Length; ++i)
                {
                    Animation.SelectActiveFrame(FrameDimension.Time, i);

                    Bitmap Frame = new Bitmap(Animation.Size.Width, Animation.Size.Height);

                    using (Graphics g = Graphics.FromImage(Frame))
                        g.DrawImage(Animation, new Point(0, 0));

                    Frames.Add(new GifFrame(Frame, Delay, 0, 0));
                }
            }
        }

        private void InitHeader(Stream sourceGif, BinaryWriter Writer, int w, int h)
        {
            // File Header
            Writer.Write(FileType.ToCharArray());
            Writer.Write(FileVersion.ToCharArray());

            Writer.Write((short)(DefaultWidth == 0 ? w : DefaultWidth)); // Initial Logical Width
            Writer.Write((short)(DefaultHeight == 0 ? h : DefaultHeight)); // Initial Logical Height

            sourceGif.Position = SourceGlobalColorInfoPosition;
            Writer.Write((byte)sourceGif.ReadByte()); // Global Color Table Info
            Writer.Write((byte)0); // Background Color Index
            Writer.Write((byte)0); // Pixel aspect ratio
            WriteColorTable(sourceGif, Writer);

            // App Extension Header
            unchecked { Writer.Write((short)ApplicationExtensionBlockIdentifier); };
            Writer.Write((byte)ApplicationBlockSize);
            Writer.Write(ApplicationIdentification.ToCharArray());
            Writer.Write((byte)3); // Application block length
            Writer.Write((byte)1);
            Writer.Write((short)Repeat); // Repeat count for images.
            Writer.Write((byte)0); // terminator
        }

        private void WriteColorTable(Stream sourceGif, BinaryWriter Writer)
        {
            sourceGif.Position = SourceColorBlockPosition; // Locating the image color table
            byte[] colorTable = new byte[SourceColorBlockLength];
            sourceGif.Read(colorTable, 0, colorTable.Length);
            Writer.Write(colorTable, 0, colorTable.Length);
        }

        private void WriteGraphicControlBlock(Stream sourceGif, BinaryWriter Writer, double frameDelay)
        {
            sourceGif.Position = SourceGraphicControlExtensionPosition; // Locating the source GCE
            byte[] blockhead = new byte[SourceGraphicControlExtensionLength];
            sourceGif.Read(blockhead, 0, blockhead.Length); // Reading source GCE

            unchecked { Writer.Write((short)GraphicControlExtensionBlockIdentifier); }; // Identifier
            Writer.Write((byte)GraphicControlExtensionBlockSize); // Block Size
            Writer.Write((byte)(blockhead[3] & 0xf7 | 0x08)); // Setting disposal flag
            Writer.Write((short)(frameDelay / 10)); // Setting frame delay
            Writer.Write((byte)blockhead[6]); // Transparent color index
            Writer.Write((byte)0); // Terminator
        }

        private void WriteImageBlock(Stream sourceGif, BinaryWriter Writer, bool includeColorTable, int x, int y, int w, int h)
        {
            sourceGif.Position = SourceImageBlockPosition; // Locating the image block
            byte[] header = new byte[SourceImageBlockHeaderLength];
            sourceGif.Read(header, 0, header.Length);
            Writer.Write((byte)header[0]); // Separator
            Writer.Write((short)x); // Position X
            Writer.Write((short)y); // Position Y
            Writer.Write((short)w); // Width
            Writer.Write((short)h); // Height

            if (includeColorTable) // If first frame, use global color table - else use local
            {
                sourceGif.Position = SourceGlobalColorInfoPosition;
                Writer.Write((byte)(sourceGif.ReadByte() & 0x3f | 0x80)); // Enabling local color table
                WriteColorTable(sourceGif, Writer);
            }
            else Writer.Write((byte)(header[9] & 0x07 | 0x07)); // Disabling local color table

            Writer.Write((byte)header[10]); // LZW Min Code Size

            // Read/Write image data
            sourceGif.Position = SourceImageBlockPosition + SourceImageBlockHeaderLength;

            int dataLength = sourceGif.ReadByte();
            while (dataLength > 0)
            {
                byte[] imgData = new byte[dataLength];
                sourceGif.Read(imgData, 0, dataLength);

                Writer.Write((byte)dataLength);
                Writer.Write(imgData, 0, dataLength);
                dataLength = sourceGif.ReadByte();
            }

            Writer.Write((byte)0); // Terminator
        }

        #endregion
    }
}