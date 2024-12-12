namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfFontMetricsMetadata
    {
        private readonly double height;
        private readonly double ascent;
        private readonly double descent;
        private readonly double emSize;

        public PdfFontMetricsMetadata(double ascent, double descent, double emSize)
        {
            this.ascent = ascent;
            this.descent = descent;
            this.height = Math.Max((double) 1.0, (double) (ascent - descent));
            this.emSize = emSize;
        }

        public double Height =>
            this.height;

        public double Ascent =>
            this.ascent;

        public double Descent =>
            this.descent;

        public double EmSize =>
            this.emSize;
    }
}

