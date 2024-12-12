namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Printing.ExportHelpers;
    using System;

    internal class PivotDataAwareExportContext<TCol, TRow> : DataAwareExportContext<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public PivotDataAwareExportContext(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
        }
    }
}

