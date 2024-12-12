namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;

    internal class ImageStreamBuilder
    {
        protected int pdfBpp = 3;
        protected byte[] line;
        private PixelConverter converter;

        public ImageStreamBuilder(PixelConverter converter)
        {
            this.pdfBpp = converter.TargetBytesPerPixel;
            this.converter = converter;
        }

        public void Build(Image image, PdfStream stream)
        {
            int sourceBytesPerPixel = this.converter.SourceBytesPerPixel;
            byte[] tempBuffer = BitmapUtils.ImageToByteArray(image);
            this.Initialize(image.Width * this.pdfBpp);
            int byteWidth = image.Width * sourceBytesPerPixel;
            int length = tempBuffer.Length;
            int num4 = BitmapUtils.CalculateCorrectedByteWidth(byteWidth);
            int num5 = 0;
            while (num5 < image.Height)
            {
                length -= num4;
                int j = 0;
                int i = length;
                while (true)
                {
                    if (i >= (length + byteWidth))
                    {
                        this.PutLineToStream(stream);
                        num5++;
                        break;
                    }
                    this.converter.ExtractPixel(this.line, ref j, tempBuffer, i);
                    i += sourceBytesPerPixel;
                }
            }
        }

        public static ImageStreamBuilder Create(bool compressed, PixelConverter converter) => 
            compressed ? new CompressedImageStreamBuilder(converter) : new ImageStreamBuilder(converter);

        protected virtual void Initialize(int pdfByteWidth)
        {
            this.line = new byte[pdfByteWidth];
        }

        protected virtual void PutLineToStream(PdfStream stream)
        {
            stream.SetBytes(this.line);
        }
    }
}

