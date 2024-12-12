namespace DevExpress.XtraPrinting.Caching
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class MemoryDocumentStorage : DocumentStorage, IDisposable
    {
        private Dictionary<DocumentStorageLocation, Dictionary<int, Stream>> streams;
        private DateTime lastAccessTime;

        public MemoryDocumentStorage();
        public override void Clear();
        protected override int Count(DocumentStorageLocation table);
        protected override void Dispose(bool disposing);
        protected override Stream Restore(DocumentStorageLocation table, int id);
        internal void SaveAllToFiles(string folder);
        protected override void Store(DocumentStorageLocation table, int id, Stream stream);
        protected internal override void UpdateLastAccessTime();

        protected internal override DateTime LastAccessTimeUtc { get; }
    }
}

