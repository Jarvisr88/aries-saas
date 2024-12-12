namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export.Xl;
    using System;

    internal class UnboundColumnTableExportProvider<TCol, TRow> : UnboundColumnExportProvider<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public UnboundColumnTableExportProvider(IXlTable table, TCol target, ColumnExportInfo<TCol, TRow> columnInfo, int cIndex) : base(target, columnInfo, cIndex)
        {
            if ((table != null) && (columnInfo.ExpressionConverter != null))
            {
                columnInfo.ExpressionConverter.Context.CurrentTable = table;
                columnInfo.ExpressionConverter.Context.RowOffset = 0;
                columnInfo.ExpressionConverter.Context.ReferenceMode = XlCellReferenceMode.Reference;
            }
        }
    }
}

