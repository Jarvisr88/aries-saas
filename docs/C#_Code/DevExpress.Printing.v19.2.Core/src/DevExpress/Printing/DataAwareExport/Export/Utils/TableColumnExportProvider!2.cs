namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using System;

    internal class TableColumnExportProvider<TCol, TRow> : ColumnExportProvider<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public TableColumnExportProvider(TCol target, ColumnExportInfo<TCol, TRow> columnInfo, int cIndex) : base(target, columnInfo, cIndex)
        {
        }

        protected override void ExportHeaderValue(IXlCell cell, TRow gridRow, int exportRowIndex)
        {
            string str = string.IsNullOrEmpty(base.targetLocalColumn.Header) ? base.targetLocalColumn.FieldName : base.targetLocalColumn.Header;
            base.SetValue(cell, gridRow, str, exportRowIndex, SheetAreaType.Header);
            this.SetRichText(cell, cell.Value.TextValue, cell.Formatting);
        }
    }
}

