namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfHalftone : PdfObject
    {
        protected const string HalftoneTypeDictionaryKey = "HalftoneType";
        protected const string HalftoneNameDictionaryKey = "HalftoneName";
        private const string halftoneType = "Halftone";
        private readonly string name;

        protected PdfHalftone(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.name = dictionary.GetString("HalftoneName");
        }

        protected PdfHalftone(string name) : base(-1)
        {
            this.name = name;
        }

        protected virtual PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("Type", new PdfName("Halftone"));
            dictionary.Add("HalftoneName", PdfDocumentStream.ConvertToArray(this.name), null);
            return dictionary;
        }

        protected internal virtual object CreateWriteableObject(PdfObjectCollection objects) => 
            objects.AddObject((PdfObject) this);

        protected internal virtual bool IsSame(PdfHalftone halftone) => 
            string.Equals(this.name, halftone.name);

        internal static PdfHalftone Parse(object value)
        {
            PdfName name = value as PdfName;
            if (name != null)
            {
                if (name.Name != "Default")
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return PdfDefaultHalftone.Instance;
            }
            PdfReaderDictionary dictionary = value as PdfReaderDictionary;
            if (dictionary == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int? integer = dictionary.GetInteger("HalftoneType");
            if (integer == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int num = integer.Value;
            if (num == 1)
            {
                return new PdfStandardHalftone(dictionary);
            }
            if (num == 5)
            {
                return new PdfSeparateHalftone(dictionary);
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            this.CreateDictionary(objects);

        public string Name =>
            this.name;
    }
}

