namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public abstract class PdfCompressedData
    {
        public const string FilterDictionaryKey = "Filter";
        public const string DecodeParametersDictionaryKey = "DecodeParms";
        private readonly IList<PdfFilter> filters;

        protected PdfCompressedData(PdfReaderDictionary dictionary) : this(list2)
        {
            IList<PdfFilter> filters = dictionary.GetFilters("Filter", "DecodeParms");
            IList<PdfFilter> list2 = filters;
            if (filters == null)
            {
                IList<PdfFilter> local1 = filters;
                list2 = new PdfFilter[0];
            }
        }

        protected PdfCompressedData(IList<PdfFilter> filters)
        {
            this.filters = filters;
        }

        public void AddFilters(PdfWriterDictionary dictionary)
        {
            PdfObjectCollection objects = dictionary.Objects;
            int count = this.filters.Count;
            if (count != 0)
            {
                if (count == 1)
                {
                    PdfFilter filter = this.filters[0];
                    dictionary.Add("Filter", new PdfName(filter.FilterName));
                    dictionary.AddIfPresent("DecodeParms", filter.Write(objects));
                }
                else
                {
                    object[] enumerable = new object[count];
                    object[] objArray2 = new object[count];
                    int index = 0;
                    while (true)
                    {
                        if (index >= count)
                        {
                            dictionary.Add("Filter", new PdfWritableArray(enumerable));
                            dictionary.Add("DecodeParms", new PdfWritableArray(objArray2));
                            break;
                        }
                        PdfFilter filter2 = this.filters[index];
                        enumerable[index] = new PdfName(filter2.FilterName);
                        objArray2[index] = filter2.Write(objects);
                        index++;
                    }
                }
            }
        }

        public abstract IPdfWritableObject CreateWritableObject(PdfWriterDictionary dictionary);

        public IList<PdfFilter> Filters =>
            this.filters;

        public byte[] UncompressedData
        {
            get
            {
                byte[] data = this.Data;
                if (data.Length != 0)
                {
                    foreach (PdfFilter filter in this.filters)
                    {
                        data = filter.Decode(data);
                    }
                }
                return data;
            }
        }

        public abstract byte[] Data { get; }
    }
}

