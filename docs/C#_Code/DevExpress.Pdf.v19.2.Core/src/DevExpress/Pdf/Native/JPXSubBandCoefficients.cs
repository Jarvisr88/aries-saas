namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXSubBandCoefficients
    {
        private readonly int width;
        private readonly int height;

        public JPXSubBandCoefficients(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public JPXSubBandCoefficients Interleave(float[] coefficients, IJPXSubBandCoefficients llSubBandCoefficients, IJPXSubBandCoefficients hlSubBandCoefficients, IJPXSubBandCoefficients lhSubBandCoefficients, IJPXSubBandCoefficients hhSubBandCoefficients)
        {
            int resultWidth = this.width + hlSubBandCoefficients.Width;
            int height = this.height + lhSubBandCoefficients.Height;
            if (llSubBandCoefficients == null)
            {
                this.SparceCoefficients(coefficients, this.width, this.height, 0, 0, resultWidth);
            }
            IJPXSubBandCoefficients[] sharedData = new IJPXSubBandCoefficients[] { lhSubBandCoefficients, hlSubBandCoefficients, hhSubBandCoefficients, llSubBandCoefficients };
            PdfTaskHelper.RunParallel<IJPXSubBandCoefficients[]>((llSubBandCoefficients == null) ? 3 : 4, sharedData, delegate (int first, int count, IJPXSubBandCoefficients[] subbands) {
                for (int i = first; i < (first + count); i++)
                {
                    subbands[i].FillCoefficients(coefficients, resultWidth);
                }
            });
            return new JPXSubBandCoefficients(resultWidth, height);
        }

        private void SparceCoefficients(float[] coefficients, int subbandWidth, int subbandHeight, int horizontalOffset, int verticalOffset, int resultWidth)
        {
            int num = resultWidth * 2;
            int num2 = ((((((subbandHeight - 1) * 2) * resultWidth) + (subbandWidth * 2)) - 2) + horizontalOffset) + (verticalOffset * resultWidth);
            int num3 = subbandHeight;
            while (num3 > 0)
            {
                int num4 = 0;
                int index = num2;
                int num6 = (num3 * subbandWidth) - 1;
                while (true)
                {
                    if (num4 >= subbandWidth)
                    {
                        num3--;
                        num2 -= num;
                        break;
                    }
                    coefficients[index] = coefficients[num6--];
                    num4++;
                    index -= 2;
                }
            }
        }

        public int Width =>
            this.width;

        public int Height =>
            this.height;
    }
}

