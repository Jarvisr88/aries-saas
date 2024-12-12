namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfCustomProperties : PdfProperties
    {
        private readonly Dictionary<string, object> dictionary;

        internal PdfCustomProperties(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.dictionary = new Dictionary<string, object>();
            PdfObjectCollection collection = dictionary.Objects;
            foreach (KeyValuePair<string, object> pair in dictionary)
            {
                this.dictionary.Add(pair.Key, PdfPrivateData.TryResolve(null, collection, pair.Value));
            }
        }

        protected internal override object Write(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            foreach (KeyValuePair<string, object> pair in this.dictionary)
            {
                object obj2 = pair.Value;
                PdfPrivateData data = obj2 as PdfPrivateData;
                dictionary.Add(pair.Key, (data == null) ? obj2 : collection.AddObject((PdfObject) data));
            }
            return dictionary;
        }

        public IDictionary<string, object> Dictionary =>
            this.dictionary;
    }
}

