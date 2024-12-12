namespace DevExpress.Office.Utils
{
    using DevExpress.Data.Utils;
    using DevExpress.Office.Model;
    using System;
    using System.Drawing;
    using System.IO;

    public static class ImageLoaderHelper
    {
        public static MemoryStream GetMemoryStream(Stream stream, int length) => 
            GetMemoryStream(stream, length, false);

        internal static MemoryStream GetMemoryStream(Stream stream, int length, bool seekToBegin)
        {
            int count = (length < 0) ? ((int) stream.Length) : length;
            byte[] buffer = new byte[count];
            long num2 = stream.CanSeek ? stream.Position : -1L;
            if (seekToBegin && stream.CanSeek)
            {
                stream.Seek(0L, SeekOrigin.Begin);
            }
            stream.Read(buffer, 0, count);
            if (stream.CanSeek)
            {
                stream.Position = num2;
            }
            return new MemoryStream(buffer);
        }

        private static Stream GetStream(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                MemoryStream destination = new MemoryStream();
                stream.CopyTo(destination);
                destination.Position = 0L;
                return destination;
            }
        }

        public static MemoryStreamBasedImage ImageFromFile(string filename) => 
            ImageFromStream(GetStream(filename));

        public static MemoryStreamBasedImage ImageFromStream(Stream stream) => 
            ImageFromStream(stream, null);

        public static MemoryStreamBasedImage ImageFromStream(Stream stream, IUniqueImageId imageId)
        {
            MemoryStream memoryStream = GetMemoryStream(stream, -1);
            Image image = ImageTool.ImageFromStream(memoryStream, true);
            return ((imageId != null) ? new MemoryStreamBasedImage(image, memoryStream, imageId) : new MemoryStreamBasedImage(image, memoryStream));
        }
    }
}

