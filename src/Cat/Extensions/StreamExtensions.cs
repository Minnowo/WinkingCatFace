using System.IO;

namespace WinkingCat.HelperLibs
{
    public static class StreamExtensions
    {
        private const int DefaultBufferSize = 4096;

        public static void CopyStreamTo(this Stream fromStream, Stream toStream, int bufferSize = DefaultBufferSize)
        {
            if (fromStream.CanSeek)
            {
                fromStream.Position = 0;
            }

            byte[] buffer = new byte[bufferSize];
            int bytesRead;

            while ((bytesRead = fromStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                toStream.Write(buffer, 0, bytesRead);
            }
        }
    }
}
