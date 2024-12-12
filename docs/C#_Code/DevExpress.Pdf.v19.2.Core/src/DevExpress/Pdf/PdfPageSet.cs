namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfPageSet : PdfSpiderSet
    {
        private readonly string title;

        internal PdfPageSet(PdfReaderDictionary dictionary) : base(dictionary)
        {
            this.title = dictionary.GetString("T");
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            if (!string.IsNullOrEmpty(this.title))
            {
                dictionary.Add("T", this.title);
            }
            return dictionary;
        }

        public string Title =>
            this.title;

        protected override string SubType =>
            "SPS";
    }
}

