namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfLogicalStructureMarkedContentReference : PdfLogicalStructureItem
    {
        internal const string Type = "MCR";
        private const string pageDictionaryKey = "Pg";
        private const string streamDictionaryKey = "Stm";
        private const string streamOwnDictionaryKey = "StmOwn";
        private const string mcidDictionaryKey = "MCID";
        private readonly PdfPage page;
        private readonly PdfForm form;
        private readonly int mcid;

        internal PdfLogicalStructureMarkedContentReference(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            PdfObjectCollection objects = dictionary.Objects;
            PdfObjectReference objectReference = dictionary.GetObjectReference("Pg");
            if (objectReference != null)
            {
                this.page = objects.DocumentCatalog.FindPage(objectReference);
                if (this.page == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            PdfObjectReference reference2 = dictionary.GetObjectReference("Stm");
            if (reference2 != null)
            {
                this.form = objects.GetXObject(reference2, null, "Form") as PdfForm;
                if (this.form == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            int? integer = dictionary.GetInteger("MCID");
            if (integer == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.mcid = integer.Value;
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.AddName("Type", "MCR");
            dictionary.Add("Pg", this.page);
            dictionary.Add("Stm", this.form);
            dictionary.Add("MCID", this.mcid);
            return dictionary;
        }

        public PdfPage Page =>
            this.page;

        public PdfForm Form =>
            this.form;

        public int Mcid =>
            this.mcid;

        protected internal override PdfPage ContainingPage =>
            this.page;
    }
}

