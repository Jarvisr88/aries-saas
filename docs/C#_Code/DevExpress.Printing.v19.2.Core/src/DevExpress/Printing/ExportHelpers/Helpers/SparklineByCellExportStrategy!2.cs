namespace DevExpress.Printing.ExportHelpers.Helpers
{
    using DevExpress.Printing.ExportHelpers;
    using System;
    using System.Collections.Generic;

    internal class SparklineByCellExportStrategy<TCol, TRow> : SparklineExportStrategy<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public SparklineByCellExportStrategy(IEnumerable<TCol> sparklineColumns, ExportInfo<TCol, TRow> exportInfo) : base(sparklineColumns, exportInfo)
        {
        }

        protected override void AssignSparklinesToSheet()
        {
            base.AssignSparklinesToSheet();
            if (base.sparklineData.Count > 0)
            {
                foreach (TCol col in base.sparklineColumns)
                {
                    base.exportInfo.Helper.ForAllRows(base.exportInfo.View, row => ((SparklineByCellExportStrategy<TCol, TRow>) this).CreateSparklineGroup(col, ((SparklineByCellExportStrategy<TCol, TRow>) this).exportInfo.View.GetRowCellSparklineInfo(row, col)));
                }
            }
        }
    }
}

