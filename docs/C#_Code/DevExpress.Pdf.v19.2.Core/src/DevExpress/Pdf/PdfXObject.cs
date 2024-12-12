namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfXObject : PdfObject
    {
        internal const string DictionaryType = "XObject";
        private const string structParentDictionaryKey = "StructParent";
        private const string openPrepressInterfaceDictionaryKey = "OPI";
        private readonly PdfMetadata metadata;
        private readonly int? structParent;
        private readonly PdfOpenPrepressInterface openPrepressInterface;
        private readonly PdfOptionalContent optionalContent;

        protected PdfXObject() : base(-1)
        {
        }

        protected PdfXObject(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.metadata = dictionary.GetMetadata();
            this.structParent = dictionary.GetInteger("StructParent");
            this.openPrepressInterface = PdfOpenPrepressInterface.Create(dictionary.GetDictionary("OPI"));
            this.optionalContent = dictionary.GetOptionalContent();
        }

        protected virtual PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Type", "XObject");
            dictionary.Add("Metadata", this.metadata);
            dictionary.AddNullable<int>("StructParent", this.structParent);
            if (this.openPrepressInterface != null)
            {
                dictionary.Add("OPI", this.openPrepressInterface.ToWritableObject(objects));
            }
            dictionary.Add("OC", this.optionalContent);
            return dictionary;
        }

        protected abstract PdfStream CreateStream(PdfObjectCollection objects);
        internal static PdfXObject Parse(PdfReaderStream stream, PdfResources parentResources, string defaultSubtype)
        {
            if (stream == null)
            {
                return null;
            }
            string str = stream.Dictionary.GetName("Subtype") ?? defaultSubtype;
            try
            {
                PdfXObject obj2;
                if (str == "Image")
                {
                    obj2 = new PdfImage(stream);
                }
                else if (str == "Form")
                {
                    obj2 = PdfForm.Create(stream, parentResources);
                }
                else
                {
                    goto TR_0001;
                }
                return obj2;
            }
            catch
            {
            }
        TR_0001:
            return null;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            this.CreateStream(objects);

        public PdfMetadata Metadata =>
            this.metadata;

        public int? StructParent =>
            this.structParent;

        public PdfOpenPrepressInterface OpenPrepressInterface =>
            this.openPrepressInterface;

        public PdfOptionalContent OptionalContent =>
            this.optionalContent;
    }
}

