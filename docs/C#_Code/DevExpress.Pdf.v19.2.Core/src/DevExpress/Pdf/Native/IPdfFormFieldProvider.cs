namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public interface IPdfFormFieldProvider
    {
        void ResetValue();

        PdfInteractiveFormField FormField { get; }
    }
}

