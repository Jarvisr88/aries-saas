namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfDocumentActions : PdfObject
    {
        private const string documentClosingDictionaryKey = "WC";
        private const string documentSavingDictionaryKey = "WS";
        private const string documentSavedDictionaryKey = "DS";
        private const string documentPrintingDictionaryKey = "WP";
        private const string documentPrintedDictionaryKey = "DP";
        private PdfJavaScriptAction documentClosing;
        private PdfJavaScriptAction documentSaving;
        private PdfJavaScriptAction documentSaved;
        private PdfJavaScriptAction documentPrinting;
        private PdfJavaScriptAction documentPrinted;
        private readonly PdfDocumentCatalog documentCatalog;

        internal PdfDocumentActions(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.documentClosing = dictionary.GetJavaScriptAction("WC");
            this.documentSaving = dictionary.GetJavaScriptAction("WS");
            this.documentSaved = dictionary.GetJavaScriptAction("DS");
            this.documentPrinting = dictionary.GetJavaScriptAction("WP");
            this.documentPrinted = dictionary.GetJavaScriptAction("DP");
        }

        public PdfDocumentActions(PdfDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            this.documentCatalog = document.DocumentCatalog;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("WC", this.documentClosing);
            dictionary.Add("WS", this.documentSaving);
            dictionary.Add("DS", this.documentSaved);
            dictionary.Add("WP", this.documentPrinting);
            dictionary.Add("DP", this.documentPrinted);
            return dictionary;
        }

        public PdfJavaScriptAction DocumentClosing
        {
            get => 
                this.documentClosing;
            set
            {
                PdfDocumentCatalog.ValidateCatalog(value.DocumentCatalog, this.documentCatalog, "value");
                this.documentClosing = value;
            }
        }

        public PdfJavaScriptAction DocumentSaving
        {
            get => 
                this.documentSaving;
            set
            {
                PdfDocumentCatalog.ValidateCatalog(value.DocumentCatalog, this.documentCatalog, "value");
                this.documentSaving = value;
            }
        }

        public PdfJavaScriptAction DocumentSaved
        {
            get => 
                this.documentSaved;
            set
            {
                PdfDocumentCatalog.ValidateCatalog(value.DocumentCatalog, this.documentCatalog, "value");
                this.documentSaved = value;
            }
        }

        public PdfJavaScriptAction DocumentPrinting
        {
            get => 
                this.documentPrinting;
            set
            {
                PdfDocumentCatalog.ValidateCatalog(value.DocumentCatalog, this.documentCatalog, "value");
                this.documentPrinting = value;
            }
        }

        public PdfJavaScriptAction DocumentPrinted
        {
            get => 
                this.documentPrinted;
            set
            {
                PdfDocumentCatalog.ValidateCatalog(value.DocumentCatalog, this.documentCatalog, "value");
                this.documentPrinted = value;
            }
        }

        internal PdfDocumentCatalog DocumentCatalog =>
            this.documentCatalog;
    }
}

