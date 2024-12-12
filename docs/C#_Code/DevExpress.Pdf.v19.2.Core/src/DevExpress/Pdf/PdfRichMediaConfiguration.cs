namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfRichMediaConfiguration : PdfObject
    {
        private const string nameDictionaryKey = "Name";
        private const string instancesDictionaryKey = "Instances";
        private readonly int number;
        private readonly string name;
        private readonly List<PdfRichMediaInstance> instances;
        private readonly PdfRichMediaContentType contentType;

        internal PdfRichMediaConfiguration(IEnumerable<KeyValuePair<string, PdfFileSpecification>> assets, PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.instances = new List<PdfRichMediaInstance>();
            this.number = dictionary.Number;
            this.name = dictionary.GetString("Name");
            IList<object> array = dictionary.GetArray("Instances");
            if (array != null)
            {
                PdfObjectCollection objects = dictionary.Objects;
                foreach (object obj2 in array)
                {
                    PdfReaderDictionary dictionary2 = objects.TryResolve(obj2, null) as PdfReaderDictionary;
                    if (dictionary2 == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.instances.Add(new PdfRichMediaInstance(assets, dictionary2));
                }
            }
            PdfRichMediaContentType? richMediaContentType = dictionary.GetRichMediaContentType();
            if (richMediaContentType != null)
            {
                this.contentType = richMediaContentType.Value;
            }
            else if (this.instances.Count > 0)
            {
                this.contentType = this.instances[0].ContentType;
            }
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddEnumName<PdfRichMediaContentType>("Subtype", this.contentType);
            dictionary.Add("Name", this.name, null);
            dictionary.AddList<PdfRichMediaInstance>("Instances", this.instances);
            return dictionary;
        }

        internal int Number =>
            this.number;

        public string Name =>
            this.name;

        public IList<PdfRichMediaInstance> Instances =>
            this.instances;

        public PdfRichMediaContentType ContentType =>
            this.contentType;
    }
}

