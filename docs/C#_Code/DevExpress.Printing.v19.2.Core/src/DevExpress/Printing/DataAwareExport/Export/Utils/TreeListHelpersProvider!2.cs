namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Printing.ExportHelpers;
    using System;

    internal class TreeListHelpersProvider<TCol, TRow> : ExportHelpersProvider<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public TreeListHelpersProvider(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
        }

        protected override SummaryExportHelper<TCol, TRow> CreateSummaryExportHelper() => 
            new TreeListSummaryExportHelper<TCol, TRow>(base.exportInfo);
    }
}

