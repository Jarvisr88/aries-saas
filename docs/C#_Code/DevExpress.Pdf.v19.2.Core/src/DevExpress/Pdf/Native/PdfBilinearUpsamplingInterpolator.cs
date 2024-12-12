namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public abstract class PdfBilinearUpsamplingInterpolator : PdfImageScanlineSourceDecorator
    {
        protected const int ConvolutionWindowSize = 2;
        private readonly PdfConvolutionWindowInfo[] convolutionWindowInfo;

        protected PdfBilinearUpsamplingInterpolator(IPdfImageScanlineSource dataSource, int sourceWidth, int targetDimension, int sourceDimension) : base(dataSource, sourceWidth)
        {
            this.convolutionWindowInfo = new PdfConvolutionWindowInfo[targetDimension];
            float num = ((float) sourceDimension) / ((float) targetDimension);
            int index = 0;
            while (index < targetDimension)
            {
                float num3 = index * num;
                List<float> list = new List<float>(2);
                int startPosition = (int) Math.Floor((double) num3);
                float num5 = 0f;
                int num7 = 0;
                while (true)
                {
                    if (num7 >= 2)
                    {
                        int count = list.Count;
                        PdfFixedPointNumber[] weight = new PdfFixedPointNumber[count];
                        int num10 = 0;
                        while (true)
                        {
                            if (num10 >= count)
                            {
                                this.convolutionWindowInfo[index] = new PdfConvolutionWindowInfo(weight, startPosition);
                                index++;
                                break;
                            }
                            weight[num10] = new PdfFixedPointNumber(list[num10] / num5);
                            num10++;
                        }
                        break;
                    }
                    int num8 = startPosition + num7;
                    if (num8 < sourceDimension)
                    {
                        float item = CalculateLinearInterpolationWeight(num8 - num3);
                        list.Add(item);
                        num5 += item;
                    }
                    num7++;
                }
            }
        }

        private static float CalculateLinearInterpolationWeight(float value)
        {
            value = Math.Abs(value);
            return ((value < 1f) ? (1f - value) : 0f);
        }

        protected PdfConvolutionWindowInfo[] ConvolutionWindowInfo =>
            this.convolutionWindowInfo;
    }
}

