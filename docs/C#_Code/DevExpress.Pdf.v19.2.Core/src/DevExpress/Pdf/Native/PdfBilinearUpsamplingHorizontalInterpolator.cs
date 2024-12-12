namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfBilinearUpsamplingHorizontalInterpolator : PdfBilinearUpsamplingInterpolator
    {
        private readonly int width;
        private readonly byte[] sourceScanline;

        public PdfBilinearUpsamplingHorizontalInterpolator(IPdfImageScanlineSource dataSource, int width, int sourceWidth) : base(dataSource, sourceWidth, width, sourceWidth)
        {
            this.width = width;
            this.sourceScanline = new byte[sourceWidth * this.ComponentsCount];
        }

        public override unsafe void FillNextScanline(byte[] scanlineData)
        {
            base.Source.FillNextScanline(this.sourceScanline);
            int sourceWidth = base.SourceWidth;
            int componentsCount = this.ComponentsCount;
            PdfConvolutionWindowInfo[] convolutionWindowInfo = base.ConvolutionWindowInfo;
            int index = 0;
            int num4 = 0;
            while (index < this.width)
            {
                PdfFixedPointNumber[] numberArray = new PdfFixedPointNumber[componentsCount];
                PdfConvolutionWindowInfo info = convolutionWindowInfo[index];
                PdfFixedPointNumber[] weights = info.Weights;
                int startPosition = info.StartPosition;
                int num6 = 0;
                while (true)
                {
                    if (num6 >= weights.Length)
                    {
                        int num9 = 0;
                        while (true)
                        {
                            if (num9 >= componentsCount)
                            {
                                index++;
                                break;
                            }
                            scanlineData[num4++] = numberArray[num9].RoundToByte();
                            num9++;
                        }
                        break;
                    }
                    PdfFixedPointNumber number = weights[num6];
                    int num7 = 0;
                    int num8 = startPosition * componentsCount;
                    while (true)
                    {
                        if (num7 >= componentsCount)
                        {
                            num6++;
                            startPosition++;
                            break;
                        }
                        PdfFixedPointNumber* numberPtr1 = &(numberArray[num7]);
                        numberPtr1[0] += this.sourceScanline[num8++] * number;
                        num7++;
                    }
                }
            }
        }
    }
}

