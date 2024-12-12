namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfRange
    {
        private readonly double min;
        private readonly double max;

        public PdfRange(double min, double max)
        {
            this.min = min;
            this.max = max;
        }

        internal bool Contains(int value) => 
            (this.min <= value) && (value <= this.max);

        internal bool IsSame(PdfRange range) => 
            (this.min == range.min) && (this.max == range.max);

        internal static PdfWritableDoubleArray ToArray(IEnumerable<PdfRange> ranges)
        {
            List<double> list = new List<double>();
            foreach (PdfRange range in ranges)
            {
                list.Add(range.Min);
                list.Add(range.Max);
            }
            return new PdfWritableDoubleArray(list);
        }

        public double Min =>
            this.min;

        public double Max =>
            this.max;
    }
}

