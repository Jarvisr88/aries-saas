namespace DevExpress.Office.Services.Implementation
{
    using DevExpress.Office.Services;
    using DevExpress.Office.Utils;
    using System;
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    public class DataStringUriProvider : IUriProvider
    {
        public string CreateCssUri(string rootUri, string styleText, string relativeUri) => 
            string.Empty;

        public string CreateImageUri(string rootUri, OfficeImage image, string relativeUri)
        {
            if (image == null)
            {
                return string.Empty;
            }
            try
            {
                ImageBytes imageBytes = this.GetImageBytes(image);
                string contentType = OfficeImage.GetContentType(imageBytes.Format);
                return $"data:{contentType};base64,{Convert.ToBase64String(imageBytes.Bytes)}";
            }
            catch
            {
                return string.Empty;
            }
        }

        private ImageBytes GetImageBytes(OfficeImage image)
        {
            try
            {
                return this.GetImageBytesCore(image, image.RawFormat);
            }
            catch
            {
                return this.GetImageBytesCore(image, OfficeImageFormat.Png);
            }
        }

        private ImageBytes GetImageBytesCore(OfficeImage image, OfficeImageFormat imageFormat)
        {
            byte[] imageBytesSafe = image.GetImageBytesSafe(imageFormat);
            return ((imageFormat != OfficeImageFormat.MemoryBmp) ? new ImageBytes(imageBytesSafe, imageFormat) : new ImageBytes(imageBytesSafe, OfficeImageFormat.Png));
        }

        private class ImageBytes
        {
            private readonly byte[] bytes;
            private readonly OfficeImageFormat format;

            public ImageBytes(byte[] bytes, OfficeImageFormat format)
            {
                this.bytes = bytes;
                this.format = format;
            }

            public byte[] Bytes =>
                this.bytes;

            public OfficeImageFormat Format =>
                this.format;
        }
    }
}

