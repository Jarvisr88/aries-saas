namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfWritableDoubleArray : PdfWritableArray<double>
    {
        public PdfWritableDoubleArray(IEnumerable<double> value) : base(value)
        {
        }

        public PdfWritableDoubleArray(params double[] value) : this((IEnumerable<double>) value)
        {
        }

        protected override void WriteItem(PdfDocumentStream documentStream, double value, int number)
        {
            documentStream.WriteDouble(value);
        }
    }
}

