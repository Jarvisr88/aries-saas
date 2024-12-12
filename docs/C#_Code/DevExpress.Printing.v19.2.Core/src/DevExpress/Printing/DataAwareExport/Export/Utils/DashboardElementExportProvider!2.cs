namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export.Xl;
    using System;

    internal class DashboardElementExportProvider<TCol, TRow> : ColumnExportProvider<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public DashboardElementExportProvider(TCol target, ColumnExportInfo<TCol, TRow> columnInfo, int cIndex) : base(target, columnInfo, cIndex)
        {
        }

        protected override void CreateDataCell(IXlCell cell, TRow gridRow, int exportRowIndex)
        {
            if (gridRow != null)
            {
                this.CombineFormatSettings(gridRow, base.targetLocalColumn, cell);
            }
            this.ExportDataValue(cell, gridRow, exportRowIndex);
        }
    }
}

