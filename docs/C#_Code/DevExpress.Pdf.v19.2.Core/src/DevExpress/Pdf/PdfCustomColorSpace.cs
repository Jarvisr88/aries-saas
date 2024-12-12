namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfCustomColorSpace : PdfColorSpace
    {
        protected PdfCustomColorSpace()
        {
        }

        protected internal override object Write(PdfObjectCollection collection) => 
            collection.AddObject((PdfObject) this);
    }
}

