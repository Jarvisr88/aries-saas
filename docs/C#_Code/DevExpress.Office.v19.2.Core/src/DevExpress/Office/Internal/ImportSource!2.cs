namespace DevExpress.Office.Internal
{
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class ImportSource<TFormat, TResult>
    {
        private readonly string fileName;
        private readonly IImporter<TFormat, TResult> importer;

        public ImportSource(string fileName, IImporter<TFormat, TResult> importer) : this(fileName, fileName, importer)
        {
        }

        public ImportSource(string storage, string fileName, IImporter<TFormat, TResult> importer)
        {
            Guard.ArgumentNotNull(importer, "importer");
            this.importer = importer;
            this.fileName = fileName;
        }

        public virtual Stream GetStream() => 
            new FileStream(this.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);

        public string FileName =>
            this.fileName;

        public IImporter<TFormat, TResult> Importer =>
            this.importer;

        public string Storage =>
            this.FileName;
    }
}

