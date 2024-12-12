namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfGrayToRGBImageScanlineSource : PdfImageScanlineSourceDecorator
    {
        private readonly byte[] sourceScanline;

        public PdfGrayToRGBImageScanlineSource(IPdfImageScanlineSource source, int width) : base(source, width)
        {
            this.sourceScanline = new byte[width * source.ComponentsCount];
        }

        public override void FillNextScanline(byte[] scanlineData)
        {
            base.Source.FillNextScanline(this.sourceScanline);
            int sourceWidth = base.SourceWidth;
            int num2 = base.Source.ComponentsCount - 1;
            bool hasAlpha = base.HasAlpha;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            while (num3 < sourceWidth)
            {
                byte num6 = this.sourceScanline[num5++];
                int num7 = 0;
                while (true)
                {
                    if (num7 >= 3)
                    {
                        if (hasAlpha)
                        {
                            scanlineData[num4++] = this.sourceScanline[num5++];
                        }
                        num3++;
                        break;
                    }
                    scanlineData[num4++] = num6;
                    num7++;
                }
            }
        }

        public override int ComponentsCount =>
            base.ComponentsCount + 2;
    }
}

