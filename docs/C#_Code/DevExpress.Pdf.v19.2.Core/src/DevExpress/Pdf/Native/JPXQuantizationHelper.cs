namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class JPXQuantizationHelper
    {
        protected const float R = 0.5f;

        protected JPXQuantizationHelper()
        {
        }

        public abstract float Apply(JPXCoefficient coefficient, int zeroBitPlanes);
    }
}

