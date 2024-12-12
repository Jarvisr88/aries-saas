namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Printing.ExportHelpers;
    using System;

    internal class TreeListDataAwareBandedExportContext<TCol, TRow> : DataAwareBandedExportContext<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public TreeListDataAwareBandedExportContext(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
        }

        public override void PrintGroupRowHeader(TRow groupRow)
        {
        }
    }
}

