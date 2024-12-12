namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfCIEBasedImageScanlineSource : PdfImageScanlineSourceDecorator
    {
        private readonly PdfColorSpace colorSpace;
        private readonly byte[] sourceScanline;
        private readonly int sourceComponentsCount;

        public PdfCIEBasedImageScanlineSource(IPdfImageScanlineSource source, PdfColorSpace colorSpace, int width, int sourceComponentsCount) : base(source, width)
        {
            this.colorSpace = colorSpace;
            this.sourceComponentsCount = sourceComponentsCount;
            this.sourceScanline = new byte[width * source.ComponentsCount];
        }

        protected virtual void Decode(double[] pixelBuffer, byte[] data, int offset)
        {
            for (int i = 0; i < this.sourceComponentsCount; i++)
            {
                pixelBuffer[i] = ((double) data[offset++]) / 255.0;
            }
        }

        public override void FillNextScanline(byte[] scanline)
        {
            base.Source.FillNextScanline(this.sourceScanline);
            int sourceWidth = base.SourceWidth;
            double[] pixelBuffer = new double[this.sourceComponentsCount];
            bool hasAlpha = base.HasAlpha;
            int num2 = 0;
            int offset = 0;
            int num4 = 0;
            while (num2 < sourceWidth)
            {
                this.Decode(pixelBuffer, this.sourceScanline, offset);
                offset += this.sourceComponentsCount;
                double[] components = this.colorSpace.Transform(new PdfColor(pixelBuffer)).Components;
                int index = 0;
                while (true)
                {
                    if (index >= 3)
                    {
                        if (hasAlpha)
                        {
                            scanline[num4++] = this.sourceScanline[offset++];
                        }
                        num2++;
                        break;
                    }
                    scanline[num4++] = PdfMathUtils.ToByte(components[index] * 255.0);
                    index++;
                }
            }
        }

        public override int ComponentsCount =>
            base.HasAlpha ? 4 : 3;
    }
}

