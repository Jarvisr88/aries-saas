namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export.Xl;
    using DevExpress.XtraExport.Helpers;
    using System;

    internal class PivotColumnExportProvider<TCol, TRow> : ColumnExportProvider<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public PivotColumnExportProvider(TCol target, ColumnExportInfo<TCol, TRow> columnInfo, int cIndex) : base(target, columnInfo, cIndex)
        {
        }

        protected override object GetGroupHeaderValue(IGroupRow<TRow> groupRow)
        {
            TRow row = groupRow as TRow;
            return ((row == null) ? null : ((base.targetLocalColumn.LogicalPosition == 0) ? groupRow.GetGroupRowHeader() : base.columnInfo.View.GetRowCellValue(row, base.targetLocalColumn)));
        }

        protected override void SetGroupHeaderCellFormatting(TRow row, IXlCell cell)
        {
            if (!base.columnInfo.RawDataMode && (cell != null))
            {
                cell.Formatting ??= new XlCellFormatting();
                cell.Formatting.GetActual(base.columnInfo.View.GetRowCellFormatting(row, base.targetLocalColumn));
                FormattingUtils.SetBoldFont(cell.Formatting);
                FormattingUtils.SetBorder(cell.Formatting, base.columnInfo.AllowHorzLines, base.columnInfo.AllowVertLines);
            }
        }
    }
}

