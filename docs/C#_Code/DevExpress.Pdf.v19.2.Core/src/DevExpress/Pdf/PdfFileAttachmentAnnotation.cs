namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfFileAttachmentAnnotation : PdfMarkupAnnotation
    {
        internal const string Type = "FileAttachment";
        private const string fileSpecificationDictionaryKey = "FS";
        private const string iconNameDictionaryKey = "Name";
        private const string defaultIconName = "PushPin";
        private readonly PdfFileSpecification fileSpecification;
        private readonly string iconName;

        internal PdfFileAttachmentAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            object obj2;
            this.fileSpecification = PdfFileSpecification.Parse(dictionary, "FS", false);
            if (!dictionary.TryGetValue("Name", out obj2))
            {
                this.iconName = "PushPin";
            }
            else
            {
                PdfName name = obj2 as PdfName;
                if (name != null)
                {
                    this.iconName = name.Name;
                }
                else
                {
                    byte[] buffer = obj2 as byte[];
                    if (buffer == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.iconName = PdfDocumentReader.ConvertToString(buffer);
                }
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            dictionary.Add("FS", this.fileSpecification);
            dictionary.AddName("Name", this.iconName, "PushPin");
            return dictionary;
        }

        public PdfFileSpecification FileSpecification =>
            this.fileSpecification;

        public string IconName =>
            this.iconName;

        protected override string AnnotationType =>
            "FileAttachment";
    }
}

