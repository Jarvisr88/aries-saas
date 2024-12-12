namespace DevExpress.Pdf.Native
{
    using System;

    public interface IPdfAnnotationStateVisitor
    {
        void Visit(PdfButtonFormFieldWidgetAnnotationState state);
        void Visit(PdfChoiceFormFieldWidgetAnnotationState state);
        void Visit(PdfCommonAnnotationState state);
        void Visit(PdfLinkAnnotationState state);
        void Visit(PdfMarkupAnnotationState state);
        void Visit(PdfTextFormFieldWidgetAnnotationState state);
        void Visit(PdfTextMarkupAnnotationState state);
    }
}

