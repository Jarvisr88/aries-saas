namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfWritablePointsArray : PdfWritableArray<PdfPoint>
    {
        public PdfWritablePointsArray(IEnumerable<PdfPoint> value) : base(value)
        {
        }

        protected override void WriteItem(PdfDocumentStream documentStream, PdfPoint value, int number)
        {
            documentStream.WriteDouble(value.X);
            documentStream.WriteSpace();
            documentStream.WriteDouble(value.Y);
        }
    }
}

