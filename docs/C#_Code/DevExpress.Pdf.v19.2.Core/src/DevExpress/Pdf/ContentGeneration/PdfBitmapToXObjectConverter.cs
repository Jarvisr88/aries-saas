namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public abstract class PdfBitmapToXObjectConverter : PdfDeviceImageToXObjectConverter
    {
        protected const byte PngUpPrediction = 2;
        private readonly PdfFilter flateFilter;
        private byte[] imageData;

        protected PdfBitmapToXObjectConverter(Bitmap bitmap) : base(bitmap)
        {
            this.flateFilter = new PdfFlateDecodeFilter(PdfFlateLZWFilterPredictor.PngUpPrediction, 3, 8, bitmap.Width);
            this.ProcessBitmap(bitmap);
        }

        protected override PdfImage GetSoftMask()
        {
            byte[] sMask = this.SMask;
            return ((sMask == null) ? null : CreateSoftMaskImage(base.Width, base.Height, sMask));
        }

        private void ProcessBitmap(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            byte[] buffer = new byte[width * this.ComponentsCount];
            using (PdfImageConverterImageDataReader reader = PdfImageConverterImageDataReader.Create(bitmap, this.ComponentsCount))
            {
                using (PdfImageStreamFlateEncoder encoder = new PdfImageStreamFlateEncoder(width * 3))
                {
                    int num4 = 0;
                    while (true)
                    {
                        if (num4 >= height)
                        {
                            this.imageData = encoder.GetEncodedData();
                            break;
                        }
                        reader.ReadNextRow(buffer, buffer.Length);
                        this.ReadNextImageRow(buffer, encoder);
                        encoder.EndRow();
                        num4++;
                    }
                }
            }
        }

        protected abstract void ReadNextImageRow(byte[] rowBuffer, PdfImageStreamFlateEncoder dataEncoder);

        protected override PdfFilter Filter =>
            this.flateFilter;

        protected override byte[] ImageData =>
            this.imageData;

        protected abstract byte[] SMask { get; }

        public override int ImageDataLength =>
            this.imageData.Length;

        protected abstract int ComponentsCount { get; }
    }
}

