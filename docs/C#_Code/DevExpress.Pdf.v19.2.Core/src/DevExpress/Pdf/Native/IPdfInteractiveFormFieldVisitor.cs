namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public interface IPdfInteractiveFormFieldVisitor
    {
        void Visit(PdfButtonFormField formField);
        void Visit(PdfChoiceFormField formField);
        void Visit(PdfInteractiveFormField formField);
        void Visit(PdfTextFormField formField);
    }
}

