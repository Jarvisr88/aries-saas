namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXIrreversibleDiscreteWaveletTransformation : JPXDiscreteWaveletTransformation
    {
        private const float alpha = -1.586134f;
        private const float beta = -0.05298012f;
        private const float gamma = 0.8829111f;
        private const float delta = 0.4435069f;
        private const float k = 1.230174f;
        private const float k1 = 0.8128931f;

        public JPXIrreversibleDiscreteWaveletTransformation(IJPXSubBandCoefficients llSubBandCoefficients) : base(llSubBandCoefficients)
        {
        }

        protected override unsafe void Filter(float[] y, int i0, int i1)
        {
            float num3;
            int index = y.Length - 1;
            int num4 = 1;
            while (num4 < index)
            {
                float* singlePtr1 = &(y[num4++]);
                singlePtr1[0] *= 0.8128931f;
                float* singlePtr2 = &(y[num4++]);
                singlePtr2[0] *= 1.230174f;
            }
            if ((index % 2) != 0)
            {
                float* singlePtr3 = &(y[index]);
                singlePtr3[0] *= 0.8128931f;
            }
            float num2 = y[1];
            for (int i = 2; i < index; i += 2)
            {
                num3 = y[i + 1];
                float* singlePtr4 = &(y[i]);
                singlePtr4[0] -= 0.4435069f * (num2 + num3);
                num2 = num3;
            }
            num2 = y[0];
            for (int j = 1; j < index; j += 2)
            {
                num3 = y[j + 1];
                float* singlePtr5 = &(y[j]);
                singlePtr5[0] -= 0.8829111f * (num2 + num3);
                num2 = num3;
            }
            num2 = y[1];
            for (int k = 2; k < index; k += 2)
            {
                num3 = y[k + 1];
                float* singlePtr6 = &(y[k]);
                singlePtr6[0] -= -0.05298012f * (num2 + num3);
                num2 = num3;
            }
            num2 = y[0];
            for (int m = 1; m < index; m += 2)
            {
                num3 = y[m + 1];
                float* singlePtr7 = &(y[m]);
                singlePtr7[0] -= -1.586134f * (num2 + num3);
                num2 = num3;
            }
        }
    }
}

