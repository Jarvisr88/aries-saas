namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfLogicalStructureContentItem : PdfLogicalStructureItem
    {
        internal const string Type = "OBJR";
        private const string contentPageDictionaryKey = "Pg";
        private const string contentObjectDictionaryKey = "Obj";
        private readonly PdfPage page;
        private readonly PdfObject content;

        internal PdfLogicalStructureContentItem(PdfPage elementPage, PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            PdfObjectCollection objects = dictionary.Objects;
            PdfObjectReference objectReference = dictionary.GetObjectReference("Pg");
            if (objectReference != null)
            {
                this.page = objects.DocumentCatalog.FindPage(objectReference);
                if (this.page != null)
                {
                    this.page.EnsureAnnotations();
                }
            }
            PdfObjectReference reference2 = dictionary.GetObjectReference("Obj");
            if (reference2 != null)
            {
                this.content = objects.GetResolvedObject<PdfObject>(reference2.Number);
                if (this.content == null)
                {
                    object obj2 = objects.TryResolve(reference2, null);
                    if (obj2 != null)
                    {
                        PdfReaderDictionary dictionary2 = obj2 as PdfReaderDictionary;
                        if ((dictionary2 != null) && (dictionary2.GetName("Type") == "Annot"))
                        {
                            PdfPage page = this.page;
                            if (this.page == null)
                            {
                                PdfPage local1 = this.page;
                                page = elementPage;
                            }
                            this.content = objects.GetAnnotation(page, reference2);
                        }
                    }
                }
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.AddName("Type", "OBJR");
            dictionary.Add("Pg", this.page);
            dictionary.Add("Obj", this.content);
            return dictionary;
        }

        public PdfPage Page =>
            this.page;

        public object Content =>
            this.content;

        protected internal override PdfPage ContainingPage =>
            this.page;
    }
}

