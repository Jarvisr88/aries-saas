namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfWritableConvertibleArray<T> : PdfWritableArray<object>
    {
        public PdfWritableConvertibleArray(IEnumerable<T> value, Func<T, object> convert) : base(PdfWritableConvertibleArray<T>.DoConvert(value, convert))
        {
        }

        private static IEnumerable<object> DoConvert(IEnumerable<T> value, Func<T, object> convert)
        {
            List<object> list = new List<object>();
            foreach (T local in value)
            {
                list.Add(convert(local));
            }
            return list;
        }

        protected override void WriteItem(PdfDocumentStream documentStream, object value, int number)
        {
            documentStream.WriteObject(value, number);
        }
    }
}

