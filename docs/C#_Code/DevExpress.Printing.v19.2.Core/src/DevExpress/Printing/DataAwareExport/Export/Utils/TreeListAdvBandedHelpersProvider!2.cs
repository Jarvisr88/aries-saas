namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Printing.ExportHelpers;
    using System;

    internal class TreeListAdvBandedHelpersProvider<TCol, TRow> : AdvBandedExportHelpersProvider<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public TreeListAdvBandedHelpersProvider(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
        }

        protected override SummaryExportHelper<TCol, TRow> CreateSummaryExportHelper() => 
            new TreeListBandedSummaryExportHelper<TCol, TRow>(base.exportInfo);
    }
}

