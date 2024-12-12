namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public interface IPdfLineCapPainter
    {
        void DrawLineCap(PdfCommandConstructor constructor, PdfPoint startPoint, PdfPoint endPoint);
        PdfPoint TranslatePoint(PdfPoint point, PdfPoint vector);
    }
}

