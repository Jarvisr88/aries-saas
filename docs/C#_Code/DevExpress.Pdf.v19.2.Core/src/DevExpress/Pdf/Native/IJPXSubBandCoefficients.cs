namespace DevExpress.Pdf.Native
{
    using System;

    public interface IJPXSubBandCoefficients
    {
        void FillCoefficients(float[] coefficients, int resultWidth);

        int Width { get; }

        int Height { get; }
    }
}

