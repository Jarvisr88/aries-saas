namespace DevExpress.XtraPrinting.Caching
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.IO;

    public class FileDocumentStorage : DocumentStorage
    {
        private const string fileSuffix = ".prnx";
        private string documentFolder;

        public FileDocumentStorage();
        public FileDocumentStorage(string documentFolder);
        internal FileDocumentStorage(string documentFolder, bool useFlate);
        public override void Clear();
        protected override int Count(DocumentStorageLocation table);
        protected virtual void CreateDocumentDirectory();
        private string GetDirectoryPath(DocumentStorageLocation table);
        internal string GetFilePath(DocumentStorageLocation table, int id);
        private void RemoveDirectory(string path, int triesRemain);
        protected override Stream Restore(DocumentStorageLocation table, int id);
        protected override void Store(DocumentStorageLocation table, int id, Stream stream);
        protected internal override void UpdateLastAccessTime();
        private void UpdateLastAccessTimeInternal();
        private static bool ValidatePath(string path);

        [Editor("System.Windows.Forms.Design.FolderNameEditor, System.Design", typeof(UITypeEditor)), DefaultValue("")]
        public string DocumentFolder { get; set; }

        protected internal override DateTime LastAccessTimeUtc { get; }
    }
}

