namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;

    public class PdfTransparentColor : PdfColor
    {
        private readonly double alpha;

        public PdfTransparentColor(byte alpha, params double[] components) : this(alpha, null, components)
        {
        }

        public PdfTransparentColor(byte alpha, PdfPattern pattern, params double[] components) : base(pattern, components)
        {
            this.alpha = ((double) alpha) / 255.0;
        }

        public double Alpha =>
            this.alpha;
    }
}

