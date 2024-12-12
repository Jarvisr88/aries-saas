namespace DevExpress.Office.Internal
{
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class ExportTarget<TFormat, TResult>
    {
        private readonly string fileName;
        private readonly IExporter<TFormat, TResult> exporter;

        public ExportTarget(string fileName, IExporter<TFormat, TResult> exporter)
        {
            Guard.ArgumentNotNull(exporter, "exporter");
            this.exporter = exporter;
            this.fileName = fileName;
        }

        public virtual Stream GetStream() => 
            new FileStream(this.FileName, FileMode.Create, FileAccess.Write, FileShare.Read);

        public string FileName =>
            this.fileName;

        public IExporter<TFormat, TResult> Exporter =>
            this.exporter;

        public string Storage =>
            this.FileName;
    }
}

