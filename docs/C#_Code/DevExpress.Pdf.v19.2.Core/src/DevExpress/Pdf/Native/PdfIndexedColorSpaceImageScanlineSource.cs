namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfIndexedColorSpaceImageScanlineSource : PdfImageScanlineSourceDecorator
    {
        private readonly int baseColorSpaceComponentsCount;
        private readonly byte[] buffer;
        private readonly byte[] lookupTable;
        private readonly int shift;

        public PdfIndexedColorSpaceImageScanlineSource(IPdfImageScanlineSource source, int width, int bitsPerComponent, byte[] lookupTable, int baseColorSpaceComponentsCount) : base(source, width)
        {
            this.lookupTable = lookupTable;
            this.buffer = new byte[width * source.ComponentsCount];
            this.baseColorSpaceComponentsCount = baseColorSpaceComponentsCount;
            this.shift = (bitsPerComponent > 8) ? 0 : (8 - bitsPerComponent);
        }

        public override void FillNextScanline(byte[] scanlineData)
        {
            base.Source.FillNextScanline(this.buffer);
            int sourceWidth = base.SourceWidth;
            bool hasAlpha = base.HasAlpha;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            while (num2 < sourceWidth)
            {
                int num5 = 0;
                int num6 = (this.buffer[num4++] >> (this.shift & 0x1f)) * this.baseColorSpaceComponentsCount;
                while (true)
                {
                    if (num5 >= this.baseColorSpaceComponentsCount)
                    {
                        if (hasAlpha)
                        {
                            scanlineData[num3++] = this.buffer[num4++];
                        }
                        num2++;
                        break;
                    }
                    scanlineData[num3++] = this.lookupTable[num6 + num5];
                    num5++;
                }
            }
        }

        public override int ComponentsCount =>
            (base.ComponentsCount + this.baseColorSpaceComponentsCount) - 1;
    }
}

