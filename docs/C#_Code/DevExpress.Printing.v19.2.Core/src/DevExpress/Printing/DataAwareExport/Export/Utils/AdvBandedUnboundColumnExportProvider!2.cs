namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using System;

    internal class AdvBandedUnboundColumnExportProvider<TCol, TRow> : UnboundColumnExportProvider<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private AdvBandedColumnExportInfo<TCol, TRow> advBandedColumnExportInfo;

        public AdvBandedUnboundColumnExportProvider(TCol target, ColumnExportInfo<TCol, TRow> columnInfo, int cIndex) : base(target, columnInfo, cIndex)
        {
            this.advBandedColumnExportInfo = columnInfo as AdvBandedColumnExportInfo<TCol, TRow>;
        }

        internal void SetExpressionConverterOffset(int indexInBandPanel)
        {
            int num = Math.Abs((int) (indexInBandPanel - this.advBandedColumnExportInfo.HeaderPanelRowsCount));
            base.columnInfo.ExpressionConverter.Context.RowOffset = -num;
        }
    }
}

