namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    public class DocumentDeserializationCollection : DocumentSerializationCollection
    {
        private DevExpress.XtraPrinting.Document document;
        private Predicate<int> predicate;

        public DocumentDeserializationCollection(DevExpress.XtraPrinting.Document document, Predicate<int> predicate);
        public override void Add(DocumentSerializationOptions options);
        protected virtual Page AddPageToDocument(int index, int pageCount);
        protected virtual Page CreatePage(int index, int pageCount);
        protected virtual void CreatePages(int pageCount);
        protected virtual void OnPageCountChanged(object sender, EventArgs e);

        protected DevExpress.XtraPrinting.Document Document { get; }
    }
}

