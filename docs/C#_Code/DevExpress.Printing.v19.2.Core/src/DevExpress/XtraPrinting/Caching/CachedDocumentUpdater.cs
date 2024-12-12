namespace DevExpress.XtraPrinting.Caching
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;

    public class CachedDocumentUpdater
    {
        private int updatePagesCount;
        private PartiallyDeserializedDocument document;
        private DocumentStorage storage;
        private PrnxExportProvider provider;
        private Dictionary<object, long> pageIDOffsets;
        private long nextPageIDOffset;

        internal CachedDocumentUpdater(PartiallyDeserializedDocument document, DocumentStorage storage);
        public void AddRange(IEnumerable<Page> pages);
        public void BeginUpdatePages();
        public void EndUpdatePages();
        private long GetPageIDOffset(Document document);
        public void InsertPage(int index, Page page);
        private void InsertPageCore(int index, Page page);
        public void ModifyDocument(Action callback);
        public void RemovePageAt(int index);
        private void UpdatePageIDOffset(Page page);
        private void UpdatePageIDOffset(IEnumerable<BrickBase> bricks, long offset);
    }
}

