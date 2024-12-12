namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Printing.ExportHelpers;
    using System;

    internal class TableExportHelpersProvider<TCol, TRow> : ExportHelpersProvider<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public TableExportHelpersProvider(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
        }

        protected override SummaryExportHelper<TCol, TRow> CreateSummaryExportHelper() => 
            new TableSummaryExportHelper<TCol, TRow>(base.exportInfo);
    }
}

