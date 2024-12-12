namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Printing.StreamingPagination;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Caching;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class CachedStreamingDocument : PSStreamingDocument, ICachedStreamingDocument
    {
        private readonly IStoredIDProvider storedIdProvider;
        private DocumentStorage storage;

        public event EventHandler DocumentSerialized;

        public CachedStreamingDocument(PrintingSystemBase ps, DocumentStorage storage, bool allowMultiThreading, bool buildContinuousInfo = true) : base(ps, allowMultiThreading, buildContinuousInfo)
        {
            this.storedIdProvider = new StoredIDProvider();
            this.storage = storage;
        }

        internal override void AddBrickObjectsToCache(Brick brick)
        {
            this.storedIdProvider.SetNextID(brick);
            base.AdjustNavigationPair(brick);
            base.AddRealBrickObjectsToCache(brick);
            base.AddBrickObjectsToCacheRecursive(brick);
            base.AddStyleToCache(brick);
        }

        protected override void AfterSerialize()
        {
            base.ClearPageData();
            this.NullCaches();
        }

        public override void CopyScaleParameters(float scaleFactor, int autoFitToPagesWidth)
        {
            base.CopyScaleParameters(scaleFactor, 0);
        }

        protected override PageBuildEngine CreatePageBuildEngine(bool buildPagesInBackground, bool rollPaper)
        {
            if (rollPaper | buildPagesInBackground)
            {
                return base.CreatePageBuildEngine(buildPagesInBackground, rollPaper);
            }
            base.PageBuildEngine.AfterBuild += new Action0(this.HandleAfterBuild);
            return (PageBuildEngine) base.PageBuildEngine;
        }

        void ICachedStreamingDocument.OnDocumentSerialized()
        {
            EventHandler documentSerialized = this.DocumentSerialized;
            if (documentSerialized != null)
            {
                documentSerialized(this, EventArgs.Empty);
            }
        }

        protected internal override void End(bool buildPagesInBackground)
        {
            base.End(buildPagesInBackground);
            PrnxDocumentBuilder.CreateDocument(this.storage, this, base.BuildContinuousInfo);
            base.ps.OnDocumentChanged(EventArgs.Empty);
        }

        protected override SerializationInfo GetIndexByObjectCore(string propertyName, object obj) => 
            ((propertyName == "SharedStyles") || (propertyName == "Style")) ? base.styles.GetIndexByObject(obj) : null;

        IStoredIDProvider ICachedStreamingDocument.StoredIdProvider =>
            this.storedIdProvider;
    }
}

