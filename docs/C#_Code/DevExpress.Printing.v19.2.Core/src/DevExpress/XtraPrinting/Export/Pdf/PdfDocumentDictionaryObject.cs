namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public abstract class PdfDocumentDictionaryObject : PdfDocumentObject
    {
        protected PdfDocumentDictionaryObject(bool compressed) : base(new PdfDictionary(PdfObjectType.Indirect), compressed)
        {
        }

        public PdfDictionary Dictionary =>
            (PdfDictionary) base.InnerObject;
    }
}

