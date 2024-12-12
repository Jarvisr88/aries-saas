namespace DevExpress.XtraPrinting.Caching
{
    using DevExpress.Printing.Core.Native.Serialization;
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;
    using System.IO;

    [TypeConverter("DevExpress.XtraReports.Design.Caching.DocumentStorageConverter, DevExpress.XtraReports.v19.2.Design")]
    public abstract class DocumentStorage : IDisposable
    {
        private SerializedPageDataList pageDataList;
        private bool useFlate;
        private bool clearOnDispose;

        protected DocumentStorage();
        public virtual void Clear();
        protected abstract int Count(DocumentStorageLocation table);
        private Stream DeflateIfNeed(Stream data);
        public void Dispose();
        protected virtual void Dispose(bool disposing);
        private Stream FlateIfNeed(Stream stream);
        protected abstract Stream Restore(DocumentStorageLocation table, int id);
        internal Stream RestoreAndDeflateIfNeed(DocumentStorageLocation table, int id);
        internal Stream RestoreDocument();
        internal Stream RestoreMeta(int id);
        internal Stream RestorePage(int pageIndex);
        protected virtual void Store(DocumentStorageLocation table, int id, Stream stream);
        internal void StoreAndFlateIfNeed(DocumentStorageLocation table, int id, Stream stream);
        internal void StoreDocument(Stream stream);
        internal void StoreMeta(Stream stream, int id);
        internal void StorePage(int streamIndex, Stream stream, Page page);
        protected internal abstract void UpdateLastAccessTime();

        [DefaultValue(true), Browsable(false)]
        public bool ClearOnDispose { get; set; }

        internal bool UseFlate { get; set; }

        internal SerializedPageDataList PageDataList { get; }

        internal Stream ExportInfoReadStream { get; }

        internal Stream ExportInfoWriteStream { get; }

        internal bool HasDocument { get; }

        internal bool HasContinuousInfo { get; }

        internal int ContinuousInfoCount { get; }

        internal int PageCount { get; }

        protected internal abstract DateTime LastAccessTimeUtc { get; }
    }
}

