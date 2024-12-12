namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfImportDataAction : PdfAction
    {
        internal const string Name = "ImportData";
        private const string fileDictionaryKey = "F";
        private readonly PdfFileSpecification fileSpecification;

        internal PdfImportDataAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
            this.fileSpecification = PdfFileSpecification.Parse(dictionary, "F", true);
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.Add("F", this.fileSpecification);
            return dictionary;
        }

        protected override string ActionType =>
            "ImportData";

        public PdfFileSpecification FileSpecification =>
            this.fileSpecification;
    }
}

