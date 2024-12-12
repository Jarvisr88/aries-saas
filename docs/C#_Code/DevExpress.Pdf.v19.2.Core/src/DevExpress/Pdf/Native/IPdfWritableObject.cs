namespace DevExpress.Pdf.Native
{
    using System;

    public interface IPdfWritableObject
    {
        void Write(PdfDocumentStream stream, int number);
    }
}

