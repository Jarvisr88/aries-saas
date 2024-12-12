namespace DevExpress.Pdf
{
    using System;

    public class PdfDecodeRange : PdfRange
    {
        private readonly int bitsCount;

        public PdfDecodeRange(double min, double max, int bitsCount) : base(min, max)
        {
            this.bitsCount = bitsCount;
        }

        public double DecodeValue(double value)
        {
            double min = base.Min;
            return (min + ((value * (base.Max - min)) / (Math.Pow(2.0, (double) this.bitsCount) - 1.0)));
        }
    }
}

