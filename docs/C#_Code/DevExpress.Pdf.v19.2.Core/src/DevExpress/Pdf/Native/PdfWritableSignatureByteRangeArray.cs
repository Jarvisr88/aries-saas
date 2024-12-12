namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfWritableSignatureByteRangeArray : PdfWritableArray<PdfSignatureByteRange>
    {
        public PdfWritableSignatureByteRangeArray(IEnumerable<PdfSignatureByteRange> value) : base(value)
        {
        }

        protected override void WriteItem(PdfDocumentStream documentStream, PdfSignatureByteRange value, int number)
        {
            documentStream.WriteInt(value.Start);
            documentStream.WriteSpace();
            documentStream.WriteInt(value.Length);
        }
    }
}

