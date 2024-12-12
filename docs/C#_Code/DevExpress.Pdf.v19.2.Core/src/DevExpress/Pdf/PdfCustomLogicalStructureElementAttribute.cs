namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfCustomLogicalStructureElementAttribute : PdfLogicalStructureElementAttribute
    {
        private readonly string owner;
        private PdfPrivateData attributes;

        internal PdfCustomLogicalStructureElementAttribute(string owner, PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.owner = owner;
            this.attributes = new PdfPrivateData(null, dictionary);
            this.attributes.Remove("O");
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.AddName("O", this.owner);
            PdfDictionary dictionary2 = this.attributes.CreateWriterDictionary(collection);
            if (dictionary2 != null)
            {
                foreach (KeyValuePair<string, object> pair in dictionary2)
                {
                    dictionary.Add(pair.Key, pair.Value);
                }
            }
            return dictionary;
        }

        public string Owner =>
            this.owner;

        public PdfPrivateData Attributes =>
            this.attributes;
    }
}

