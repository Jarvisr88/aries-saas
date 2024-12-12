namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfImageData
    {
        private readonly PdfImageDataSource data;
        private readonly int width;
        private readonly int height;
        private readonly int stride;
        private readonly PdfPixelFormat pixelFormat;
        private readonly PdfImageColor[] palette;

        public PdfImageData(PdfImageDataSource data, int width, int height, int stride, PdfPixelFormat pixelFormat, PdfImageColor[] palette)
        {
            this.data = data;
            this.width = width;
            this.height = height;
            this.stride = stride;
            this.pixelFormat = pixelFormat;
            this.palette = palette;
        }

        public PdfImageDataSource Data =>
            this.data;

        public int Width =>
            this.width;

        public int Height =>
            this.height;

        public int Stride =>
            this.stride;

        public PdfPixelFormat PixelFormat =>
            this.pixelFormat;

        public PdfImageColor[] Palette =>
            this.palette;
    }
}

