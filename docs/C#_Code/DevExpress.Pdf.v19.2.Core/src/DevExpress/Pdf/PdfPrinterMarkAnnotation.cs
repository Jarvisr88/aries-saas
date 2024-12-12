namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfPrinterMarkAnnotation : PdfAnnotation
    {
        internal const string Type = "PrinterMark";

        internal PdfPrinterMarkAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
        }

        protected override string AnnotationType =>
            "PrinterMark";
    }
}

