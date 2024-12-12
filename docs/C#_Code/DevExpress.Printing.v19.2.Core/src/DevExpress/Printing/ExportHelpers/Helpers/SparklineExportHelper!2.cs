namespace DevExpress.Printing.ExportHelpers.Helpers
{
    using DevExpress.Printing.ExportHelpers;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class SparklineExportHelper<TCol, TRow> : ExportHelperBase<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public SparklineExportHelper(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
        }

        public override void Execute()
        {
            ISparklineExportStrategy strategy;
            Func<TCol, bool> predicate = <>c<TCol, TRow>.<>9__1_0;
            if (<>c<TCol, TRow>.<>9__1_0 == null)
            {
                Func<TCol, bool> local1 = <>c<TCol, TRow>.<>9__1_0;
                predicate = <>c<TCol, TRow>.<>9__1_0 = col => (col.ColEditType == ColumnEditTypes.Sparkline) && (col.SparklineInfo != null);
            }
            IEnumerable<TCol> source = base.ExportInfo.GridColumns.Where<TCol>(predicate);
            if (source.Any<TCol>())
            {
                strategy = new SparklineByColumnExportStrategy<TCol, TRow>(source, base.ExportInfo);
            }
            else
            {
                Func<TCol, bool> func2 = <>c<TCol, TRow>.<>9__1_1;
                if (<>c<TCol, TRow>.<>9__1_1 == null)
                {
                    Func<TCol, bool> local2 = <>c<TCol, TRow>.<>9__1_1;
                    func2 = <>c<TCol, TRow>.<>9__1_1 = col => col.ColEditType == ColumnEditTypes.Sparkline;
                }
                strategy = new SparklineByCellExportStrategy<TCol, TRow>(base.ExportInfo.GridColumns.Where<TCol>(func2), base.ExportInfo);
            }
            strategy.Export();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SparklineExportHelper<TCol, TRow>.<>c <>9;
            public static Func<TCol, bool> <>9__1_0;
            public static Func<TCol, bool> <>9__1_1;

            static <>c()
            {
                SparklineExportHelper<TCol, TRow>.<>c.<>9 = new SparklineExportHelper<TCol, TRow>.<>c();
            }

            internal bool <Execute>b__1_0(TCol col) => 
                (col.ColEditType == ColumnEditTypes.Sparkline) && (col.SparklineInfo != null);

            internal bool <Execute>b__1_1(TCol col) => 
                col.ColEditType == ColumnEditTypes.Sparkline;
        }
    }
}

