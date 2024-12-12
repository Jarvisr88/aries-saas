namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSubmitFormAction : PdfAction
    {
        internal const string Name = "SubmitForm";
        private const string fileSpecificationDictionaryKey = "F";
        private const string flagsDictionaryKey = "Flags";
        private const int defaultFlag = 0;
        private readonly PdfFileSpecification fileSpecification;
        private readonly int flags;

        internal PdfSubmitFormAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
            this.fileSpecification = PdfFileSpecification.Parse(dictionary, "F", false);
            int? integer = dictionary.GetInteger("Flags");
            this.flags = (integer != null) ? integer.GetValueOrDefault() : 0;
            if (this.flags < 0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.Add("F", this.fileSpecification);
            dictionary.Add("Flags", this.flags, 0);
            return dictionary;
        }

        public PdfFileSpecification FileSpecification =>
            this.fileSpecification;

        public int Flags =>
            this.flags;

        protected override string ActionType =>
            "SubmitForm";
    }
}

