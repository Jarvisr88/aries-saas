namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfUnknownFont : PdfType1Font
    {
        internal PdfUnknownFont(PdfReaderDictionary dictionary, string baseFont, PdfReaderStream toUnicode, PdfReaderDictionary fontDescriptor, PdfSimpleFontEncoding encoding, int? firstChar, int? lastChar, double[] widths) : base(dictionary, baseFont, toUnicode, fontDescriptor, encoding, firstChar, lastChar, widths)
        {
        }

        protected internal override string Subtype =>
            string.Empty;
    }
}

