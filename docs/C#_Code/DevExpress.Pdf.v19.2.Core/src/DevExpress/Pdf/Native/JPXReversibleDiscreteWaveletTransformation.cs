namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXReversibleDiscreteWaveletTransformation : JPXDiscreteWaveletTransformation
    {
        public JPXReversibleDiscreteWaveletTransformation(IJPXSubBandCoefficients llSubBandCoefficients) : base(llSubBandCoefficients)
        {
        }

        protected override unsafe void Filter(float[] y, int i0, int i1)
        {
            for (int i = 2 * (i0 / 2); i < (2 * ((i1 / 2) + 1)); i += 2)
            {
                float* singlePtr1 = &(y[i]);
                singlePtr1[0] -= ((int) ((((y[i - 1] + y[i + 1]) + 2f) / 4f) + 255f)) - 0xff;
            }
            for (int j = (2 * (i0 / 2)) + 1; j < ((2 * (i1 / 2)) + 1); j += 2)
            {
                float* singlePtr2 = &(y[j]);
                singlePtr2[0] += ((int) (((y[j - 1] + y[j + 1]) / 2f) + 255f)) - 0xff;
            }
        }
    }
}

