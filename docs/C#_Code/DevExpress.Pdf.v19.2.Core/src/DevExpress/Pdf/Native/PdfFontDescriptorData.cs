namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.CompilerServices;

    public class PdfFontDescriptorData : IPdfFontDescriptorBuilder
    {
        public PdfFontDescriptorData()
        {
            this.CapHeight = 500.0;
        }

        public PdfFontDescriptorData(PdfFontMetrics fontMetrics, PdfFontFlags fontFlags, double italicAngle, bool bold, int numGlyphs)
        {
            this.Flags = fontFlags;
            this.ItalicAngle = italicAngle;
            this.Bold = bold;
            this.Ascent = fontMetrics.EmAscent;
            this.Descent = -fontMetrics.EmDescent;
            this.BBox = fontMetrics.EmBBox;
            this.NumGlyphs = numGlyphs;
            this.CapHeight = 500.0;
        }

        public string FontFamily { get; set; }

        public PdfFontFlags Flags { get; set; }

        public double ItalicAngle { get; set; }

        public bool Bold { get; set; }

        public double Ascent { get; set; }

        public double Descent { get; set; }

        public PdfRectangle BBox { get; set; }

        public int NumGlyphs { get; set; }

        public double CapHeight { get; set; }

        public double XHeight { get; set; }

        public double StemV { get; set; }

        public double StemH { get; set; }
    }
}

