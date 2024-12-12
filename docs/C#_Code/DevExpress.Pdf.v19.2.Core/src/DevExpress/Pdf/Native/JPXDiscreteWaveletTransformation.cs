namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class JPXDiscreteWaveletTransformation
    {
        private const int extendSampleCount = 4;
        private JPXSubBandCoefficients subBandCoefficients;

        protected JPXDiscreteWaveletTransformation(IJPXSubBandCoefficients llSubBandCoefficients)
        {
            this.subBandCoefficients = new JPXSubBandCoefficients(llSubBandCoefficients.Width, llSubBandCoefficients.Height);
        }

        public void Append(float[] coefficients, IJPXSubBandCoefficients llCoefficients, IJPXSubBandCoefficients hlCoefficients, IJPXSubBandCoefficients lhCoefficients, IJPXSubBandCoefficients hhCoefficients)
        {
            this.subBandCoefficients = this.subBandCoefficients.Interleave(coefficients, llCoefficients, hlCoefficients, lhCoefficients, hhCoefficients);
            this.HorizontalReconstruction(coefficients);
            this.VerticalReconstruction(coefficients);
        }

        private static void Extend(float[] buffer, int size)
        {
            int index = 3;
            int num2 = 5;
            int num3 = (4 + size) - 2;
            int num4 = 4 + size;
            buffer[index--] = buffer[num2++];
            buffer[num4++] = buffer[num3--];
            buffer[index--] = buffer[num2++];
            buffer[num4++] = buffer[num3--];
            buffer[index--] = buffer[num2++];
            buffer[num4++] = buffer[num3--];
            buffer[index] = buffer[num2];
            buffer[num4] = buffer[num3];
        }

        protected abstract void Filter(float[] y, int i0, int i1);
        private void HorizontalReconstruction(float[] coefficients)
        {
            int width = this.subBandCoefficients.Width;
            if (width > 1)
            {
                PdfTaskHelper.RunParallel<object>(this.subBandCoefficients.Height, null, delegate (int firstRowIndex, int rowCount, object obj) {
                    int num = width + 4;
                    int count = width * 4;
                    int dstOffset = 0x10;
                    float[] dst = new float[width + 8];
                    int num4 = 0;
                    for (int i = firstRowIndex * count; num4 < rowCount; i += count)
                    {
                        Buffer.BlockCopy(coefficients, i, dst, dstOffset, count);
                        Extend(dst, width);
                        this.Filter(dst, 4, num);
                        Buffer.BlockCopy(dst, dstOffset, coefficients, i, count);
                        num4++;
                    }
                });
            }
        }

        private void VerticalReconstruction(float[] coefficients)
        {
            int height = this.subBandCoefficients.Height;
            if (height > 1)
            {
                PdfTaskHelper.RunParallel<object>(this.subBandCoefficients.Width, null, delegate (int firstColumnIndex, int columnCount, object obj) {
                    int num = height + 4;
                    int width = this.subBandCoefficients.Width;
                    int index = 0;
                    int num4 = 0x10;
                    float[][] numArray = new float[num4][];
                    int num5 = 4 + height;
                    for (int i = 0; i < num4; i++)
                    {
                        numArray[i] = new float[height + 8];
                    }
                    int num6 = firstColumnIndex + columnCount;
                    for (int j = firstColumnIndex; j < num6; j++)
                    {
                        if (index == 0)
                        {
                            num4 = Math.Min(num6 - j, num4);
                            int num9 = j;
                            int num10 = 4;
                            while (true)
                            {
                                if (num10 >= num5)
                                {
                                    index = num4;
                                    break;
                                }
                                int num11 = 0;
                                while (true)
                                {
                                    if (num11 >= num4)
                                    {
                                        num9 += width;
                                        num10++;
                                        break;
                                    }
                                    numArray[num11][num10] = coefficients[num9 + num11];
                                    num11++;
                                }
                            }
                        }
                        index--;
                        float[] buffer = numArray[index];
                        Extend(buffer, height);
                        this.Filter(buffer, 4, num);
                        if (index == 0)
                        {
                            int num12 = (j - num4) + 1;
                            int num13 = 4;
                            while (num13 < num5)
                            {
                                int num14 = 0;
                                while (true)
                                {
                                    if (num14 >= num4)
                                    {
                                        num12 += width;
                                        num13++;
                                        break;
                                    }
                                    coefficients[num12 + num14] = numArray[num14][num13];
                                    num14++;
                                }
                            }
                        }
                    }
                });
            }
        }

        public JPXSubBandCoefficients SubBandCoefficients =>
            this.subBandCoefficients;
    }
}

