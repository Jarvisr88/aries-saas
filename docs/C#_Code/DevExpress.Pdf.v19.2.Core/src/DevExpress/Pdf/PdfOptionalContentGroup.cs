namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfOptionalContentGroup : PdfOptionalContent
    {
        internal const string Type = "OCG";
        private const string nameDictionaryKey = "Name";
        private const string intentDictionaryKey = "Intent";
        private const string usageDictionaryKey = "Usage";
        private readonly string name;
        private readonly PdfOptionalContentIntent intent;
        private readonly PdfOptionalContentUsage usage;

        internal PdfOptionalContentGroup(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.name = dictionary.GetTextString("Name");
            this.intent = dictionary.GetOptionalContentIntent("Intent");
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("Usage");
            if (dictionary2 != null)
            {
                this.usage = new PdfOptionalContentUsage(dictionary2);
            }
        }

        protected internal override object Write(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Type", "OCG");
            dictionary.AddIfPresent("Name", this.name);
            dictionary.AddIntent("Intent", this.intent);
            dictionary.Add("Usage", this.usage);
            return dictionary;
        }

        public string Name =>
            this.name;

        public PdfOptionalContentIntent Intent =>
            this.intent;

        public PdfOptionalContentUsage Usage =>
            this.usage;
    }
}

