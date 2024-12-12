namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public abstract class PdfImageScanlineDecoder
    {
        private readonly IList<PdfRange> colorKey;
        private readonly bool isColorKeyPresent;
        private readonly int width;
        private readonly int componentsCount;

        protected PdfImageScanlineDecoder(PdfImage image, int componentsCount)
        {
            this.componentsCount = componentsCount;
            this.width = image.Width;
            this.colorKey = image.ColorKeyMask;
            this.isColorKeyPresent = this.colorKey != null;
        }

        public static PdfImageScanlineDecoder CreateImageScanlineDecoder(PdfImage image, int componentsCount)
        {
            int bitsPerComponent = image.BitsPerComponent;
            switch (bitsPerComponent)
            {
                case 1:
                    if ((componentsCount != 1) || (image.ColorKeyMask != null))
                    {
                        break;
                    }
                    return new Pdf1bppImageScanlineDecoder(image);

                case 2:
                case 4:
                    break;

                case 3:
                    goto TR_0000;

                default:
                    if (bitsPerComponent == 0x10)
                    {
                        return new PdfByteAlignedImageScanlineDecoder(image, componentsCount, 2);
                    }
                    goto TR_0000;
            }
            return new PdfNonByteAlignedImageScanlineDecoder(image, componentsCount);
        TR_0000:
            return new PdfByteAlignedImageScanlineDecoder(image, componentsCount, 1);
        }

        public abstract void FillNextScanline(byte[] scanline, byte[] sourceData, int sourceOffset);

        protected IList<PdfRange> ColorKey =>
            this.colorKey;

        protected int Width =>
            this.width;

        public bool IsColorKeyPresent =>
            this.isColorKeyPresent;

        public int ComponentsCount =>
            this.componentsCount;

        public abstract int Stride { get; }
    }
}

