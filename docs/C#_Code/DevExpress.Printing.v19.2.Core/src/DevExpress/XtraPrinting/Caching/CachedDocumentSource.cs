namespace DevExpress.XtraPrinting.Caching
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;

    [ToolboxItem(false)]
    public class CachedDocumentSource : IDocumentSource, ILink
    {
        private DocumentStorage storage;
        private PrintingSystemBase deserializedPS;
        private PartiallyDeserializedDocument deserializedDocument;
        private CachedDocumentUpdater updater;

        public CachedDocumentSource(DocumentStorage storage);
        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This method has become obsolete"), DXHelpExclude(true)]
        public void BeginUpdatePages();
        internal void ClearPageListBuffer();
        void ILink.CreateDocument();
        void ILink.CreateDocument(bool buildForInstantPreview);
        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This method has become obsolete"), DXHelpExclude(true)]
        public void EndUpdatePages();
        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the ModifyDocument method instead"), DXHelpExclude(true)]
        public void InsertPage(int index, Page page);
        private void InstantiatePrintingSystem();
        public void ModifyDocument(Action<IDocumentModifier> callback);
        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the ModifyDocument method instead"), DXHelpExclude(true)]
        public void RemoveAtPage(int index);
        public void UpdatePages();

        [Browsable(false)]
        public PrintingSystemBase PrintingSystem { get; }

        [Browsable(false)]
        public PageList Pages { get; }

        IPrintingSystem ILink.PrintingSystem { get; }

        PrintingSystemBase IDocumentSource.PrintingSystemBase { get; }
    }
}

