namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Printing.ExportHelpers;
    using System;

    internal class BandedExportHelpersProvider<TCol, TRow> : ExportHelpersProvider<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public BandedExportHelpersProvider(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
        }

        protected override SummaryExportHelper<TCol, TRow> CreateSummaryExportHelper() => 
            new BandedSummaryExportHelper<TCol, TRow>(base.exportInfo);
    }
}

