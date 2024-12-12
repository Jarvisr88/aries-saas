namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfCMYKToRGBImageScanlineSource : PdfImageScanlineSourceDecorator
    {
        private readonly int width;
        private readonly byte[] sourceScanline;

        public PdfCMYKToRGBImageScanlineSource(IPdfImageScanlineSource source, int width) : base(source, width)
        {
            this.width = width;
            this.sourceScanline = new byte[width * source.ComponentsCount];
        }

        private static void ConvertCMYKToRGB(byte cyan, byte magenta, byte yellow, byte black, byte[] destination, int destinationOffset)
        {
            if (black == 0xff)
            {
                destination[destinationOffset++] = 0;
                destination[destinationOffset++] = 0;
                destination[destinationOffset++] = 0;
            }
            else
            {
                double num = 0xff - cyan;
                double num2 = 0xff - magenta;
                double num3 = 0xff - yellow;
                double num4 = 0xff - black;
                double num5 = ((double) black) / num4;
                double num6 = ((num * num2) * num3) * num4;
                num6 *= num5;
                num6 = ((num * num2) * yellow) * num4;
                num6 *= num5;
                num6 = ((num * magenta) * num3) * num4;
                num6 = ((num * magenta) * yellow) * num4;
                num6 = ((cyan * num2) * num3) * num4;
                num6 *= num5;
                num6 = ((cyan * num2) * yellow) * num4;
                num6 = ((cyan * magenta) * num3) * num4;
                double num7 = (((((((num6 + (0.1373 * num6)) + num6) + (0.1098 * num6)) + (0.9255 * num6)) + (0.1412 * (num6 * num5))) + (0.9294 * num6)) + (0.1333 * (num6 * num5))) + (0.1804 * num6);
                double num8 = ((((((((num6 + (0.1216 * num6)) + (0.949 * num6)) + (0.102 * num6)) + (0.1098 * num6)) + (0.6784 * num6)) + (0.0588 * num6)) + (0.651 * num6)) + (0.0745 * (((cyan * num2) * yellow) * black))) + (0.1922 * num6);
                double num9 = (((((((num6 + (0.1255 * num6)) + (0.549 * num6)) + (0.1412 * num6)) + (0.9373 * num6)) + (0.1412 * num6)) + (0.3137 * num6)) + (0.5725 * num6)) + (0.0078 * (num6 * num5));
                num6 = ((cyan * magenta) * yellow) * num4;
                destination[destinationOffset++] = (byte) ((num7 + (0.2118 * num6)) / 16581375.0);
                destination[destinationOffset++] = (byte) ((num8 + (0.2119 * num6)) / 16581375.0);
                destination[destinationOffset++] = (byte) ((num9 + (0.2235 * num6)) / 16581375.0);
            }
        }

        public override void FillNextScanline(byte[] scanline)
        {
            base.Source.FillNextScanline(this.sourceScanline);
            int num = 0;
            int num2 = 0;
            for (int i = 0; num < this.width; i += this.ComponentsCount)
            {
                ConvertCMYKToRGB(this.sourceScanline[num2++], this.sourceScanline[num2++], this.sourceScanline[num2++], this.sourceScanline[num2++], scanline, i);
                if (base.HasAlpha)
                {
                    scanline[i + 3] = this.sourceScanline[num2++];
                }
                num++;
            }
        }

        public override int ComponentsCount =>
            base.ComponentsCount - 1;
    }
}

