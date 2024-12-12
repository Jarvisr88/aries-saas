namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXUnitaryStepSizeQuantizationHelper : JPXQuantizationHelper
    {
        private readonly float exponent;

        public JPXUnitaryStepSizeQuantizationHelper(int subBandGainLog, int stepSizeExponent, int GuardBitCount)
        {
            this.exponent = (GuardBitCount + stepSizeExponent) - 1;
        }

        public override float Apply(JPXCoefficient coefficient, int zeroBitPlanes)
        {
            int num = coefficient.ToInteger();
            if (num == 0)
            {
                return 0f;
            }
            float num2 = (float) Math.Pow(2.0, (double) ((this.exponent - coefficient.BitsDecoded) - zeroBitPlanes));
            return ((num2 == 1f) ? ((float) num) : ((num + ((num < 0) ? -0.5f : 0.5f)) * num2));
        }
    }
}

