namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Printing.StreamingPagination;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Caching;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class PrnxExportProvider : IStreamingPageExportProvider, IDisposable
    {
        private readonly DocumentStorage storage;
        private readonly IStreamingDocument document;
        private readonly PrintingSystemBase ps;
        private int streamIndex;
        private PrnxContinuousInfoExporter ciExporter;
        private StreamingSerializationContext context = new StreamingSerializationContext();

        public PrnxExportProvider(DocumentStorage storage, IStreamingDocument document, PrintingSystemBase ps, bool buildContinuousInfo = true)
        {
            this.storage = storage;
            this.document = document;
            this.ps = ps;
            this.BuildContinuousInfo = buildContinuousInfo;
            this.AllowCleanupBands = !buildContinuousInfo;
        }

        protected virtual void ClearDocument()
        {
            ((Document) this.document).ClearContent();
        }

        private XmlXtraSerializer CreateSerializer() => 
            new StreamingXmlSerializer(this.context);

        public void Dispose()
        {
        }

        public void Export(Page page)
        {
            if (this.BuildContinuousInfo && (this.ciExporter == null))
            {
                this.ciExporter = new PrnxContinuousInfoExporter(this.document, this.storage, this.context);
                this.ciExporter.StartExport();
            }
            if (this.ciExporter != null)
            {
                this.ciExporter.ExportCollectedBricks();
            }
            XmlXtraSerializer serializer = this.CreateSerializer();
            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    StoredIDSerializationManager.BeginSerialize();
                    DocumentSerializationCollection objects = new DocumentSerializationCollection();
                    objects.Add(new XtraObjectInfo("Page", page));
                    objects.Add(new XtraObjectInfo("PageData", page.PageData));
                    objects.Add(new XtraObjectInfo("Watermark", page.Watermark));
                    this.document.PrepareSerialization(page);
                    serializer.SerializeObjects(this.document, objects, stream, string.Empty, null);
                    int streamIndex = this.streamIndex;
                    this.streamIndex = streamIndex + 1;
                    this.storage.StorePage(streamIndex, stream, page);
                }
                finally
                {
                    StoredIDSerializationManager.EndSerialize();
                }
            }
        }

        public void FinalizeBuildBookmarks(IList<int> pageIndexes)
        {
        }

        public void FinalizeBuildDocument()
        {
            this.SerializeDocument();
            this.document.FinishBuild();
        }

        public void Flush()
        {
            if (this.BuildContinuousInfo && (this.ciExporter != null))
            {
                this.ciExporter.EndExport();
            }
            this.UpdateBookmark(this.document.RootBookmark);
            this.SerializeDocument();
            this.ClearDocument();
        }

        private void SerializeDocument()
        {
            XmlXtraSerializer serializer = this.CreateSerializer();
            ICachedStreamingDocument document = this.document as ICachedStreamingDocument;
            IStoredIDProvider storedIdProvider = document?.StoredIdProvider;
            using (MemoryStream stream = new MemoryStream())
            {
                this.document.PrepareDocumentSerialization();
                DocumentSerializationCollection objects = new DocumentSerializationCollection();
                objects.Add(new XtraObjectInfo("UpdatedObjects", this.document.UpdatedObjects));
                objects.Add(new XtraObjectInfo("SerializedPageDataList", this.storage.PageDataList));
                StreamingDocumentSerializationOptions options = new StreamingDocumentSerializationOptions(this.document, this.document.BuiltPageCount);
                options.MaxBrickRight = this.document.MaxBrickRight;
                options.BuildContinuousInfo = this.document.BuildContinuousInfo;
                options.LastStoredID = (storedIdProvider != null) ? storedIdProvider.LastID : -1;
                objects.Add(options);
                serializer.SerializeObjects(this.document, objects, stream, string.Empty, null);
                this.storage.StoreDocument(stream);
            }
            if (document != null)
            {
                document.OnDocumentSerialized();
            }
        }

        private void UpdateBookmark(BookmarkNode node)
        {
            BrickPagePair pair = node.Pair;
            node.Pair = BrickPagePair.Create(pair.BrickIndices, this.storage.PageDataList.GetPageIndex(pair.PageID), pair.PageID, pair.BrickBounds);
            foreach (BookmarkNode node2 in node.Nodes)
            {
                this.UpdateBookmark(node2);
            }
        }

        internal void WaitForContinuousInfoExportFinished()
        {
            if (this.ciExporter != null)
            {
                this.ciExporter.WaitForExportFinished();
            }
        }

        public int LastStreamIndex
        {
            get => 
                this.streamIndex;
            set => 
                this.streamIndex = value;
        }

        public bool BuildContinuousInfo { get; private set; }

        public bool AllowCleanupBands { get; private set; }
    }
}

