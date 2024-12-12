namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfRichMediaDeactivation : PdfObject
    {
        internal PdfRichMediaDeactivation(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection) => 
            new PdfWriterDictionary(collection);
    }
}

