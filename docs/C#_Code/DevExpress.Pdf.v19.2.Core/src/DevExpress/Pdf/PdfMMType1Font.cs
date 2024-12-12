namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfMMType1Font : PdfType1Font
    {
        internal const string Name = "MMType1";

        internal PdfMMType1Font(PdfReaderDictionary dictionary, string baseFont, PdfReaderStream toUnicode, PdfReaderDictionary fontDescriptor, PdfSimpleFontEncoding encoding, int firstChar, int lastChar, double[] widths) : base(dictionary, baseFont, toUnicode, fontDescriptor, encoding, new int?(firstChar), new int?(lastChar), widths)
        {
        }

        protected internal override string Subtype =>
            "MMType1";
    }
}

