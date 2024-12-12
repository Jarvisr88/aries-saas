namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Printing.ExportHelpers;
    using System;

    internal class TreeListDataAwareExportContext<TCol, TRow> : DataAwareExportContext<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public TreeListDataAwareExportContext(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
        }

        public override void PrintGroupRowHeader(TRow groupRow)
        {
        }
    }
}

