namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class JPXSubBand : JPXArea, IJPXSubBandCoefficients
    {
        private readonly JPXTileComponent component;
        private readonly int resolutionLevelNumber;
        private readonly int codeBlocksWide;
        private readonly int codeBlocksHigh;
        private readonly JPXCodeBlock[] codeBlocks;

        protected JPXSubBand(JPXTileComponent component, int resolutionLevelNumber, int codeBlockWidth, int codeBlockHeight)
        {
            this.component = component;
            this.resolutionLevelNumber = resolutionLevelNumber;
            long num = 1L << (resolutionLevelNumber & 0x3f);
            long num2 = num / 2L;
            long num3 = num2 * this.HorizontalQuantity;
            long num4 = num2 * this.VerticalQuantity;
            int num5 = (int) Math.Ceiling((double) (((float) (component.X0 - num3)) / ((float) num)));
            int num6 = (int) Math.Ceiling((double) (((float) (component.X1 - num3)) / ((float) num)));
            int num7 = (int) Math.Ceiling((double) (((float) (component.Y0 - num4)) / ((float) num)));
            int num8 = (int) Math.Ceiling((double) (((float) (component.Y1 - num4)) / ((float) num)));
            base.X0 = num5;
            base.X1 = num6;
            base.Y0 = num7;
            base.Y1 = num8;
            JPXCodingStyleComponent codingStyle = component.CodingStyle;
            if ((base.Width == 0) || (base.Height == 0))
            {
                this.codeBlocks = new JPXCodeBlock[0];
            }
            else
            {
                this.codeBlocksWide = ((int) Math.Ceiling((double) (((float) num6) / ((float) codeBlockWidth)))) - (num5 / codeBlockWidth);
                this.codeBlocksHigh = ((int) Math.Ceiling((double) (((float) num8) / ((float) codeBlockHeight)))) - (num7 / codeBlockHeight);
                this.codeBlocks = JPXCodeBlocksBuilder.Build(this, codeBlockWidth, codeBlockHeight);
            }
        }

        protected static byte[] BuldLookupTable(Func<byte, byte, byte, int> calculateContextLabel)
        {
            byte[] buffer = new byte[0xff];
            byte num = 0;
            while (num <= 2)
            {
                byte num2 = 0;
                while (true)
                {
                    if (num2 > 2)
                    {
                        num = (byte) (num + 1);
                        break;
                    }
                    byte num3 = 0;
                    while (true)
                    {
                        if (num3 > 4)
                        {
                            num2 = (byte) (num2 + 1);
                            break;
                        }
                        buffer[((num3 << 4) | (num2 << 2)) | num] = (byte) calculateContextLabel(num, num2, num3);
                        num3 = (byte) (num3 + 1);
                    }
                }
            }
            return buffer;
        }

        public void FillCoefficients(float[] coefficients, int resultWidth)
        {
            if (this.codeBlocks.Length != 0)
            {
                PdfTaskHelper.RunParallel<float[]>(this.codeBlocks.Length, coefficients, delegate (int firstCodeBlockIndex, int codeBlockCount, float[] coefficients2) {
                    JPXCodingStyleComponent codingStyle = this.component.CodingStyle;
                    JPXCodeBlockCodingStyle codeBlockCodingStyle = codingStyle.CodeBlockCodingStyle;
                    int subBandIndex = (((codingStyle.DecompositionLevelCount - this.resolutionLevelNumber) * 3) + this.HorizontalQuantity) + (2 * this.VerticalQuantity);
                    int num2 = this.X0;
                    int num3 = this.Y0;
                    int width = this.Width;
                    JPXQuantizationHelper helper = this.component.QuantizationParameters.CreateHelper(this.GainLog, this.component.BitsPerComponent, subBandIndex);
                    for (int i = firstCodeBlockIndex; i < (firstCodeBlockIndex + codeBlockCount); i++)
                    {
                        JPXCodeBlock codeBlock = this.codeBlocks[i];
                        int height = codeBlock.Height;
                        int num7 = codeBlock.Width * height;
                        int zeroBitPlanes = codeBlock.ZeroBitPlanes;
                        JPXCoefficient[] coefficientArray = JPXCoefficientBitModel.Decode(codeBlock, this, codeBlockCodingStyle);
                        if (resultWidth > 0)
                        {
                            int num9 = (((((codeBlock.Y0 - num3) * 2) + this.VerticalQuantity) * resultWidth) + ((codeBlock.X0 - num2) * 2)) + this.HorizontalQuantity;
                            int num10 = 0;
                            while (num10 < height)
                            {
                                int index = num10;
                                int num12 = num9;
                                while (true)
                                {
                                    if (index >= num7)
                                    {
                                        num10++;
                                        num9 += resultWidth * 2;
                                        break;
                                    }
                                    coefficients[num12] = helper.Apply(coefficientArray[index], zeroBitPlanes);
                                    index += height;
                                    num12 += 2;
                                }
                            }
                        }
                        else
                        {
                            int num13 = 0;
                            int num14 = (((codeBlock.Y0 - num3) * width) + codeBlock.X0) - num2;
                            while (num13 < height)
                            {
                                int index = num13;
                                int num16 = num14;
                                while (true)
                                {
                                    if (index >= num7)
                                    {
                                        num13++;
                                        num14 += width;
                                        break;
                                    }
                                    coefficients[num16] = helper.Apply(coefficientArray[index], zeroBitPlanes);
                                    index += height;
                                    num16++;
                                }
                            }
                        }
                    }
                });
            }
        }

        public int CodeBlocksWide =>
            this.codeBlocksWide;

        public int CodeBlocksHigh =>
            this.codeBlocksHigh;

        public JPXCodeBlock[] CodeBlocks =>
            this.codeBlocks;

        public abstract byte[] LookupTable { get; }

        protected abstract int HorizontalQuantity { get; }

        protected abstract int VerticalQuantity { get; }

        protected abstract int GainLog { get; }
    }
}

