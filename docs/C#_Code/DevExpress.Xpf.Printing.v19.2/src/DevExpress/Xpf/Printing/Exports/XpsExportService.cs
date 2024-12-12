namespace DevExpress.Xpf.Printing.Exports
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.IO;
    using System.Windows.Documents;
    using System.Windows.Threading;

    internal class XpsExportService : XpsExportServiceBase
    {
        private readonly DocumentPaginator paginator;
        private ProgressReflector progressReflector;

        public XpsExportService(DocumentPaginator paginator)
        {
            Guard.ArgumentNotNull(paginator, "paginator");
            this.paginator = paginator;
        }

        public override void Export(Stream stream, XpsExportOptions options, ProgressReflector progressReflector)
        {
            try
            {
                this.progressReflector = progressReflector;
                progressReflector.InitializeRange(this.paginator.PageCount + 2);
                XpsExporter exporter = new XpsExporter();
                exporter.ProgressChanged += new EventHandler(this.Exporter_ProgressChanged);
                exporter.CreateDocument(this.paginator, stream, options);
                exporter.ProgressChanged -= new EventHandler(this.Exporter_ProgressChanged);
            }
            finally
            {
                progressReflector.MaximizeRange();
            }
        }

        private void Exporter_ProgressChanged(object sender, EventArgs e)
        {
            Action callback = () => this.progressReflector.RangeValue++;
            Dispatcher.CurrentDispatcher.Invoke(callback, DispatcherPriority.Render);
        }

        public DocumentPaginator Paginator =>
            this.paginator;
    }
}

