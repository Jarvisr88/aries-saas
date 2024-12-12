namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfFileSpecificationData : PdfObject
    {
        private const string sizeDictionaryKey = "Size";
        private const string creationDateDictionaryKey = "CreationDate";
        private const string modificationDateDictionaryKey = "ModDate";
        private const string parametersDictionaryKey = "Params";
        private string mimeType;
        private DateTimeOffset? creationDate;
        private DateTimeOffset? modificationDate;
        private int? size;
        private PdfCompressedData data;

        public PdfFileSpecificationData()
        {
        }

        public PdfFileSpecificationData(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.mimeType = dictionary.GetName("Subtype");
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("Params");
            if (dictionary2 != null)
            {
                this.size = dictionary2.GetInteger("Size");
                this.creationDate = dictionary2.GetDate("CreationDate");
                this.modificationDate = dictionary2.GetDate("ModDate");
            }
            this.data = new PdfStreamCompressedData(dictionary);
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Subtype", this.mimeType);
            PdfWriterDictionary dictionary2 = new PdfWriterDictionary(objects);
            dictionary2.AddIfPresent("CreationDate", this.creationDate);
            dictionary2.AddIfPresent("ModDate", this.modificationDate);
            dictionary2.AddIfPresent("Size", this.size);
            if (dictionary2.Count > 0)
            {
                dictionary.Add("Params", dictionary2);
            }
            return this.data.CreateWritableObject(dictionary);
        }

        public bool HasData =>
            this.data != null;

        public byte[] Data
        {
            get => 
                this.data?.UncompressedData;
            set
            {
                this.data = new PdfArrayCompressedData(new PdfFilter[0], value);
                this.size = new int?(value.Length);
            }
        }

        public int? Size =>
            this.size;

        public string MimeType
        {
            get => 
                this.mimeType;
            set => 
                this.mimeType = value;
        }

        public DateTimeOffset? CreationDate
        {
            get => 
                this.creationDate;
            set => 
                this.creationDate = value;
        }

        public DateTimeOffset? ModificationDate
        {
            get => 
                this.modificationDate;
            set => 
                this.modificationDate = value;
        }
    }
}

