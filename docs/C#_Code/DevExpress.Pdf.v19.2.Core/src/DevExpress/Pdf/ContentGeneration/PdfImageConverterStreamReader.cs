namespace DevExpress.Pdf.ContentGeneration
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public class PdfImageConverterStreamReader : PdfImageConverterImageDataReader
    {
        private const int bitmapDataOffset = 0x36;
        private MemoryStream stream = new MemoryStream();
        private int y;
        private int stride;

        internal PdfImageConverterStreamReader(Bitmap bitmap, int componentsCount)
        {
            bitmap.Save(this.stream, ImageFormat.Bmp);
            this.stride = 4 * (((bitmap.Width * componentsCount) + 3) / 4);
            this.y = 0x36 + ((bitmap.Height - 1) * this.stride);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.stream.Dispose();
            }
        }

        public override int ReadNextRow(byte[] buffer, int count)
        {
            this.stream.Position = this.y;
            this.stream.Read(buffer, 0, count);
            this.y -= this.stride;
            return this.stride;
        }
    }
}

