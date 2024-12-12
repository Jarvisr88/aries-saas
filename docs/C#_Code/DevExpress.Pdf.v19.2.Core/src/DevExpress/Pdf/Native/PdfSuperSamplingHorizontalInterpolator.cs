namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfSuperSamplingHorizontalInterpolator : PdfSuperSamplingInterpolator
    {
        private readonly byte[] sourceScanline;
        private readonly int width;

        public PdfSuperSamplingHorizontalInterpolator(IPdfImageScanlineSource dataSource, int width, int sourceWidth) : base(dataSource, width, sourceWidth)
        {
            this.width = width;
            this.sourceScanline = new byte[sourceWidth * base.ComponentsCount];
        }

        public override unsafe void FillNextScanline(byte[] scanline)
        {
            base.DataSource.FillNextScanline(this.sourceScanline);
            PdfSourceImageScanlineInfo[] pixelInfo = base.PixelInfo;
            int windowSize = base.WindowSize;
            int index = 0;
            int num3 = 0;
            while (index < this.width)
            {
                int componentsCount = base.ComponentsCount;
                PdfFixedPointNumber[] numberArray = new PdfFixedPointNumber[componentsCount];
                PdfSourceImagePixelInfo[] infoArray2 = pixelInfo[index].PixelInfo;
                int length = infoArray2.Length;
                int num6 = 0;
                while (true)
                {
                    if (num6 >= length)
                    {
                        int num10 = 0;
                        while (true)
                        {
                            if (num10 >= componentsCount)
                            {
                                index++;
                                break;
                            }
                            scanline[num3++] = numberArray[num10].RoundToByte();
                            num10++;
                        }
                        break;
                    }
                    PdfSourceImagePixelInfo info = infoArray2[num6];
                    PdfFixedPointNumber factor = info.Factor;
                    int num7 = info.Index;
                    int num8 = 0;
                    int num9 = num7 * componentsCount;
                    while (true)
                    {
                        if (num8 >= componentsCount)
                        {
                            num6++;
                            break;
                        }
                        PdfFixedPointNumber* numberPtr1 = &(numberArray[num8]);
                        numberPtr1[0] += this.sourceScanline[num9++] * factor;
                        num8++;
                    }
                }
            }
        }
    }
}

