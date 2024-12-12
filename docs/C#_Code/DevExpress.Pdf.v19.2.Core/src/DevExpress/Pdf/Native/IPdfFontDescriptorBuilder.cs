namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public interface IPdfFontDescriptorBuilder
    {
        string FontFamily { get; }

        bool Bold { get; }

        double Ascent { get; }

        double Descent { get; }

        PdfRectangle BBox { get; }

        PdfFontFlags Flags { get; }

        double ItalicAngle { get; }

        int NumGlyphs { get; }

        double StemH { get; }

        double StemV { get; }

        double XHeight { get; }

        double CapHeight { get; }
    }
}

