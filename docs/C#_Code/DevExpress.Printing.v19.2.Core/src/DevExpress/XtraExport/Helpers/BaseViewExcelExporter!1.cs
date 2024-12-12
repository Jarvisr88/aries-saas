namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.Utils;
    using System;
    using System.IO;

    public abstract class BaseViewExcelExporter<T> : XlExportManager, IExcelExporter where T: class
    {
        private T viewToExportCore;
        private IXlSheet sheet;

        protected BaseViewExcelExporter(T viewToExport, IDataAwareExportOptions options) : base(options)
        {
            this.viewToExportCore = viewToExport;
        }

        void IExcelExporter.ExportSheet(IXlExport exporter)
        {
            base.Exporter = exporter;
            this.sheet = base.Exporter.BeginSheet();
            if (!string.IsNullOrEmpty(base.Options.SheetName))
            {
                this.sheet.Name = base.Options.SheetName;
            }
            this.sheet.ViewOptions.RightToLeft = base.Options.RightToLeftDocument == DefaultBoolean.True;
            this.sheet.IgnoreErrors = (XlIgnoreErrors) base.Options.IgnoreErrors;
            this.sheet.OutlineProperties.SummaryRight = false;
            this.sheet.PageSetup = new XlPageSetup();
            this.sheet.PageSetup.FitToWidth = base.Options.FitToPrintedPageWidth ? 1 : 0;
            this.sheet.PageSetup.FitToHeight = base.Options.FitToPrintedPageHeight ? 1 : 0;
            if (this.ExportOverride())
            {
                base.Exporter.EndSheet();
            }
        }

        public virtual void Export(Stream stream)
        {
            IXlDocument document = base.Exporter.BeginExport(stream, base.EncryptionOptions);
            base.SetDocumentOptions(document);
            base.SetDocumentProperties(document);
            ((IExcelExporter) this).ExportSheet(base.Exporter);
            base.Exporter.EndExport();
        }

        public void Export(string outputpath)
        {
            using (FileStream stream = new FileStream(outputpath, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                this.Export(stream);
            }
        }

        protected abstract bool ExportOverride();

        internal T View =>
            this.viewToExportCore;

        internal IXlSheet Sheet =>
            this.sheet;
    }
}

