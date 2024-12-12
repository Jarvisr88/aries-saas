namespace DevExpress.Printing.ExportHelpers.Helpers
{
    using DevExpress.Printing.ExportHelpers;
    using System;
    using System.Collections.Generic;

    internal class SparklineByColumnExportStrategy<TCol, TRow> : SparklineExportStrategy<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public SparklineByColumnExportStrategy(IEnumerable<TCol> sparklineColumns, ExportInfo<TCol, TRow> exportInfo) : base(sparklineColumns, exportInfo)
        {
        }

        protected override void AssignSparklinesToSheet()
        {
            base.AssignSparklinesToSheet();
            if (base.sparklineData.Count > 0)
            {
                foreach (TCol local in base.sparklineColumns)
                {
                    this.CreateSparklineGroup(local, local.SparklineInfo);
                }
            }
        }
    }
}

