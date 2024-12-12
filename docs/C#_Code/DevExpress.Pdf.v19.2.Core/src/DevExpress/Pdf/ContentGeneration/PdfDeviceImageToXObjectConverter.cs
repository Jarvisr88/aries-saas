namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public abstract class PdfDeviceImageToXObjectConverter : PdfImageToXObjectConverter
    {
        private static readonly PdfRange[] maskDecode = new PdfRange[] { new PdfRange(0.0, 1.0) };
        private readonly PdfDeviceColorSpaceKind colorSpaceKind;
        private readonly IList<PdfRange> decode;

        protected PdfDeviceImageToXObjectConverter(Image image) : base(image)
        {
            this.colorSpaceKind = PdfDeviceColorSpaceKind.RGB;
            this.decode = RgbDecode;
        }

        protected PdfDeviceImageToXObjectConverter(int width, int height, PdfDeviceColorSpaceKind colorSpaceKind, IList<PdfRange> decode) : base(width, height)
        {
            this.colorSpaceKind = colorSpaceKind;
            this.decode = decode;
        }

        protected static PdfImage CreateSoftMaskImage(int width, int height, byte[] softMaskData)
        {
            PdfFilter[] filters = new PdfFilter[] { new PdfFlateDecodeFilter(PdfFlateLZWFilterPredictor.PngUpPrediction, 1, 8, width) };
            return new PdfImage(width, height, new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.Gray), 8, maskDecode, new PdfArrayCompressedData(filters, softMaskData), null);
        }

        protected abstract PdfImage GetSoftMask();
        public override PdfImage GetXObject()
        {
            PdfFilter[] filters = new PdfFilter[] { this.Filter };
            return new PdfImage(base.Width, base.Height, new PdfDeviceColorSpace(this.colorSpaceKind), 8, this.decode, new PdfArrayCompressedData(filters, this.ImageData), this.GetSoftMask());
        }

        protected static PdfRange[] RgbDecode =>
            new PdfRange[] { new PdfRange(0.0, 1.0), new PdfRange(0.0, 1.0), new PdfRange(0.0, 1.0) };

        protected abstract byte[] ImageData { get; }

        protected abstract PdfFilter Filter { get; }
    }
}

