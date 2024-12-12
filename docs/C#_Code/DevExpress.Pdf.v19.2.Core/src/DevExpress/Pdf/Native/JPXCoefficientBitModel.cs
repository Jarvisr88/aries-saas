namespace DevExpress.Pdf.Native
{
    using System;
    using System.IO;

    public class JPXCoefficientBitModel
    {
        private const int runLengthContexIndex = 0x11;
        private const int uniformContextIndex = 0x12;
        private static readonly byte[,] signContextLabels;
        private readonly int codeBlockWidth;
        private readonly int codeBlockHeight;
        private readonly PdfArithmeticContext arithmeticContext;
        private readonly byte[] contextLabelLookupTable;
        private readonly JPXCoefficient[] coefficients;
        private byte passCount;

        static JPXCoefficientBitModel()
        {
            byte[] buffer1 = new byte[,] { { 13, 12, 11 }, { 10, 9, 10 }, { 11, 12, 13 } };
            signContextLabels = buffer1;
        }

        private JPXCoefficientBitModel(JPXCodeBlock codeBlock, Stream stream, JPXSubBand subBand)
        {
            byte[] initialContext = new byte[] { 8 };
            initialContext[0x11] = 6;
            initialContext[0x12] = 0x5c;
            this.arithmeticContext = new PdfArithmeticContext(new PdfArithmeticState(new PdfBigEndianStreamReader(stream)), initialContext);
            this.codeBlockWidth = codeBlock.Width;
            this.codeBlockHeight = codeBlock.Height;
            int num = this.codeBlockWidth * this.codeBlockHeight;
            this.passCount = codeBlock.CodingPassCount;
            this.coefficients = new JPXCoefficient[num];
            this.contextLabelLookupTable = subBand.LookupTable;
        }

        private unsafe void CleanUpPass()
        {
            int num = this.passCount + 2;
            int num2 = 0;
            while (num2 < this.codeBlockHeight)
            {
                int num3 = Math.Min(num2 + 4, this.codeBlockHeight);
                int x = 0;
                int num5 = 0;
                while (true)
                {
                    if (x >= this.codeBlockWidth)
                    {
                        num2 += 4;
                        break;
                    }
                    bool flag = (num2 + 4) <= this.codeBlockHeight;
                    int num6 = num3 + num5;
                    int index = num5 + num2;
                    while (true)
                    {
                        if (index < num6)
                        {
                            JPXCoefficient coefficient = this.coefficients[index];
                            if ((coefficient.Significance == 0) && ((coefficient.NotZeroContextLabelPassNumber != num) && (this.contextLabelLookupTable[coefficient.RawNeighborSignificance] == 0)))
                            {
                                index++;
                                continue;
                            }
                            flag = false;
                        }
                        int y = num2;
                        if (flag)
                        {
                            byte bitsDecoded;
                            if (this.arithmeticContext.DecodeBit(0x11) == 0)
                            {
                                int num14 = num5 + y;
                                while (true)
                                {
                                    if (num14 >= num6)
                                    {
                                        y = num3;
                                        break;
                                    }
                                    JPXCoefficient* coefficientPtr2 = &(this.coefficients[num14]);
                                    bitsDecoded = coefficientPtr2.BitsDecoded;
                                    coefficientPtr2.BitsDecoded = (byte) (bitsDecoded + 1);
                                    num14++;
                                }
                            }
                            else
                            {
                                int num9 = (this.arithmeticContext.DecodeBit(0x12) << 1) | this.arithmeticContext.DecodeBit(0x12);
                                int num10 = num5 + num2;
                                int num11 = num10 + num9;
                                int num12 = num10;
                                while (true)
                                {
                                    if (num12 > num11)
                                    {
                                        this.ReadSignBit(x, num2 + num9);
                                        y += num9 + 1;
                                        break;
                                    }
                                    JPXCoefficient* coefficientPtr1 = &(this.coefficients[num12]);
                                    bitsDecoded = coefficientPtr1.BitsDecoded;
                                    coefficientPtr1.BitsDecoded = (byte) (bitsDecoded + 1);
                                    num12++;
                                }
                            }
                        }
                        int num15 = num5 + y;
                        while (true)
                        {
                            if (num15 >= num6)
                            {
                                x++;
                                num5 += this.codeBlockHeight;
                                break;
                            }
                            JPXCoefficient coefficient2 = this.coefficients[num15];
                            if ((coefficient2.Significance == 0) && (coefficient2.NotZeroContextLabelPassNumber != num))
                            {
                                if (this.arithmeticContext.DecodeBit(this.contextLabelLookupTable[coefficient2.RawNeighborSignificance]) != 0)
                                {
                                    this.ReadSignBit(x, y);
                                }
                                JPXCoefficient* coefficientPtr3 = &(this.coefficients[num15]);
                                coefficientPtr3.BitsDecoded = (byte) (coefficientPtr3.BitsDecoded + 1);
                            }
                            num15++;
                            y++;
                        }
                        break;
                    }
                }
            }
        }

        private JPXCoefficient[] Decode(JPXCodeBlockCodingStyle codeBlockCodingStyle)
        {
            int num = 2;
            bool flag = codeBlockCodingStyle.HasFlag(JPXCodeBlockCodingStyle.UseSegmentationSymbols);
            while (this.passCount > 0)
            {
                switch (num)
                {
                    case 0:
                        this.SignificancePropagationPass();
                        break;

                    case 1:
                        this.MagnitudeRefinementPass();
                        break;

                    case 2:
                        this.CleanUpPass();
                        break;

                    default:
                        break;
                }
                num = ++num % 3;
                if ((num == 0) & flag)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        this.arithmeticContext.DecodeBit(0x12);
                    }
                }
                this.passCount = (byte) (this.passCount - 1);
            }
            return this.coefficients;
        }

        public static JPXCoefficient[] Decode(JPXCodeBlock codeBlock, JPXSubBand subBand, JPXCodeBlockCodingStyle codeBlockCodingStyle)
        {
            using (Stream stream = new MemoryStream(codeBlock.EncodedData))
            {
                return new JPXCoefficientBitModel(codeBlock, stream, subBand).Decode(codeBlockCodingStyle);
            }
        }

        private int GetSignContribution(int index)
        {
            JPXCoefficient coefficient = this.coefficients[index];
            return (coefficient.Significance * (1 - coefficient.Sign));
        }

        private void IterateCodeBlock(Action<int, int, int, JPXCoefficient> action)
        {
            int num = 0;
            while (num < this.codeBlockHeight)
            {
                int num2 = Math.Min(num + 4, this.codeBlockHeight);
                int num3 = 0;
                while (true)
                {
                    if (num3 >= this.codeBlockWidth)
                    {
                        num += 4;
                        break;
                    }
                    int num4 = num;
                    int num5 = (num3 * this.codeBlockHeight) + num;
                    while (true)
                    {
                        if (num4 >= num2)
                        {
                            num3++;
                            break;
                        }
                        action(num5, num3, num4, this.coefficients[num5]);
                        num4++;
                        num5++;
                    }
                }
            }
        }

        private unsafe void MagnitudeRefinementPass()
        {
            this.IterateCodeBlock(delegate (int index, int x, int y, JPXCoefficient coefficient) {
                if ((coefficient.Significance != 0) && (coefficient.CalculatedSignificancesStepNumber != (this.passCount + 1)))
                {
                    JPXCoefficient* coefficientPtr1 = &coefficient;
                    coefficientPtr1.Magnitude = (short) (coefficientPtr1.Magnitude << 1);
                    if (coefficient.IsNotFirstRefinement)
                    {
                        JPXCoefficient* coefficientPtr2 = &coefficient;
                        coefficientPtr2.Magnitude = (short) (coefficientPtr2.Magnitude | ((short) this.arithmeticContext.DecodeBit(0x10)));
                    }
                    else
                    {
                        coefficient.IsNotFirstRefinement = true;
                        JPXCoefficient* coefficientPtr3 = &coefficient;
                        coefficientPtr3.Magnitude = (short) (coefficientPtr3.Magnitude | ((short) this.arithmeticContext.DecodeBit((coefficient.RawNeighborSignificance == 0) ? 14 : 15)));
                    }
                    JPXCoefficient* coefficientPtr4 = &coefficient;
                    coefficientPtr4.BitsDecoded = (byte) (coefficientPtr4.BitsDecoded + 1);
                    this.coefficients[index] = coefficient;
                }
            });
        }

        private static int NormalizeContribution(int contribution) => 
            Math.Min(1, Math.Max(-1, contribution));

        private void ReadSignBit(int x, int y)
        {
            int index = (x * this.codeBlockHeight) + y;
            this.coefficients[index] = this.ReadSignBit(x, y, index, this.coefficients[index]);
        }

        private JPXCoefficient ReadSignBit(int x, int y, int index, JPXCoefficient coefficient)
        {
            coefficient.Significance = 1;
            bool flag = x > 0;
            bool flag2 = x < (this.codeBlockWidth - 1);
            int num = index - this.codeBlockHeight;
            int num2 = index + this.codeBlockHeight;
            int contribution = 0;
            int num4 = 0;
            if (y > 0)
            {
                int num7 = index - 1;
                this.coefficients[num7].IncrementVerticalNeighborSignificance();
                contribution += this.GetSignContribution(num7);
                if (flag)
                {
                    this.coefficients[num - 1].IncrementDiagonalNeighborSignificance();
                }
                if (flag2)
                {
                    this.coefficients[num2 - 1].IncrementDiagonalNeighborSignificance();
                }
            }
            if (y < (this.codeBlockHeight - 1))
            {
                int num8 = index + 1;
                this.coefficients[num8].IncrementVerticalNeighborSignificance();
                contribution += this.GetSignContribution(num8);
                if (flag)
                {
                    this.coefficients[num + 1].IncrementDiagonalNeighborSignificance();
                }
                if (flag2)
                {
                    this.coefficients[num2 + 1].IncrementDiagonalNeighborSignificance();
                }
            }
            if (flag)
            {
                this.coefficients[num].IncrementHorizontalNeighborSignificance();
                num4 += this.GetSignContribution(num);
            }
            if (flag2)
            {
                this.coefficients[num2].IncrementHorizontalNeighborSignificance();
                num4 += this.GetSignContribution(num2);
            }
            coefficient.Magnitude = 1;
            contribution = NormalizeContribution(contribution);
            num4 = NormalizeContribution(num4);
            int num6 = this.arithmeticContext.DecodeBit(signContextLabels[num4 + 1, contribution + 1]);
            coefficient.Sign = (byte) (num6 ^ (((num4 < 0) || ((num4 == 0) && (contribution < 0))) ? 1 : 0));
            return coefficient;
        }

        private unsafe void SignificancePropagationPass()
        {
            this.IterateCodeBlock(delegate (int index, int x, int y, JPXCoefficient coefficient) {
                if (coefficient.Significance == 0)
                {
                    int cx = this.contextLabelLookupTable[coefficient.RawNeighborSignificance];
                    if (cx != 0)
                    {
                        coefficient.NotZeroContextLabelPassNumber = this.passCount;
                        if (this.arithmeticContext.DecodeBit(cx) != 0)
                        {
                            coefficient.CalculatedSignificancesStepNumber = this.passCount;
                            coefficient = this.ReadSignBit(x, y, index, coefficient);
                        }
                        JPXCoefficient* coefficientPtr1 = &coefficient;
                        coefficientPtr1.BitsDecoded = (byte) (coefficientPtr1.BitsDecoded + 1);
                        this.coefficients[index] = coefficient;
                    }
                }
            });
        }
    }
}

