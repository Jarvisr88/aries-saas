namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXIrreversibleTransformationQuantizationHelper : JPXQuantizationHelper
    {
        private readonly int pow;
        private readonly float stepSize;

        public JPXIrreversibleTransformationQuantizationHelper(int subBandGainLog, int ri, JPXQuantizationStepSize quantizationStepSize, int GuardBitCount)
        {
            this.stepSize = 1f + (((float) quantizationStepSize.Mu) / 2048f);
            int epsilon = quantizationStepSize.Epsilon;
            this.pow = (subBandGainLog + ri) - epsilon;
            this.stepSize = (this.pow >= 0) ? (this.stepSize * (1 << (this.pow & 0x1f))) : (this.stepSize / ((float) (1 << (-this.pow & 0x1f))));
            this.pow = (GuardBitCount + epsilon) - 1;
        }

        public override float Apply(JPXCoefficient coefficient, int zeroBitPlanes)
        {
            int num = coefficient.ToInteger();
            if (num == 0)
            {
                return 0f;
            }
            int num2 = (this.pow - coefficient.BitsDecoded) - zeroBitPlanes;
            return (((num + ((num < 0) ? -0.5f : 0.5f)) * this.stepSize) * (1 << (num2 & 0x1f)));
        }
    }
}

