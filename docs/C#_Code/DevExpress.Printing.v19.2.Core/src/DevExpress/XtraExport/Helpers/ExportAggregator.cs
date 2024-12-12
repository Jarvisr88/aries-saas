namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class ExportAggregator : XlExportManager
    {
        private List<IExcelExporter> exporters;

        public ExportAggregator(IDataAwareExportOptions options) : base(options)
        {
            this.exporters = new List<IExcelExporter>();
        }

        public void AddExporter(IExcelExporter exporter)
        {
            this.exporters.Add(exporter);
        }

        public void Export(Stream stream)
        {
            IXlDocument document = base.Exporter.BeginExport(stream);
            base.SetDocumentOptions(document);
            base.SetDocumentProperties(document);
            for (int i = 0; i < this.exporters.Count; i++)
            {
                this.exporters[i].ExportSheet(base.Exporter);
            }
            base.Exporter.EndExport();
        }
    }
}

