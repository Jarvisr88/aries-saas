namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public interface IPdfAnnotationBuilder
    {
        PdfRectangle Rect { get; }

        PdfAnnotationFlags Flags { get; }

        string Name { get; }

        string Contents { get; }

        DateTimeOffset? ModificationDate { get; }

        PdfRGBColor Color { get; }

        PdfAnnotationBorder Border { get; }
    }
}

