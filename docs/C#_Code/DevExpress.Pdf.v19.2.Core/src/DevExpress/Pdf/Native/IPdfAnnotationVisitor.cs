namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public interface IPdfAnnotationVisitor
    {
        void Visit(PdfAnnotation annotation);
        void Visit(PdfInkAnnotation inkAnnotation);
        void Visit(PdfLinkAnnotation annotation);
        void Visit(PdfMarkupAnnotation annotation);
        void Visit(PdfPopupAnnotation annotation);
        void Visit(PdfTextMarkupAnnotation annotation);
        void Visit(PdfWidgetAnnotation annotation);
    }
}

