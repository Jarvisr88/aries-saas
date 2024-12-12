namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfWritableObjectArray : PdfWritableConvertibleArray<PdfObject>
    {
        public PdfWritableObjectArray(IEnumerable<PdfObject> value, PdfObjectCollection objects) : base(value, o => objects.AddObject(o))
        {
        }
    }
}

