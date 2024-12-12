namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Printing.ExportHelpers;
    using System;

    internal class TreeListDataAwareAdvBandedExportContext<TCol, TRow> : DataAwareAdvBandedExportContext<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public TreeListDataAwareAdvBandedExportContext(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
        }

        public override void PrintGroupRowHeader(TRow groupRow)
        {
        }
    }
}

