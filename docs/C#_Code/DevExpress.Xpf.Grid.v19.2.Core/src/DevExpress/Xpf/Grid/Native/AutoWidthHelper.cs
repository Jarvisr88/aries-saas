namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class AutoWidthHelper
    {
        public static void CalcColumnLayout(IList columns, double width, GridViewInfo viewInfo, LayoutAssigner layoutAssigner, bool needRoundingLastColumn, bool allowFixedWidth = true)
        {
            double rest = 0.0;
            Action<BaseColumn, int> setColumnWidth = (column, columnIndex) => layoutAssigner.SetWidth(column, GetRoundedActualColumnWidth(column.HeaderWidth, ref rest, IsLastColumn(needRoundingLastColumn, columns.Count, columnIndex)));
            double desiredColumnsWidth = viewInfo.GetDesiredColumnsWidth(columns);
            double fixedWidth = CalcColumnsFixedWidth(columns, viewInfo);
            if (desiredColumnsWidth <= fixedWidth)
            {
                CalcColumnsLayoutForLargeFixedWidth(columns, width, desiredColumnsWidth, viewInfo, allowFixedWidth, setColumnWidth);
            }
            else
            {
                CalcColumnsLayoutForRegularCase(columns, width, desiredColumnsWidth, fixedWidth, viewInfo, setColumnWidth);
            }
        }

        public static double CalcColumnsFixedWidth(IEnumerable columns, GridViewInfo viewInfo) => 
            columns.Cast<BaseColumn>().Sum<BaseColumn>(x => GetColumnFixedWidth(x, viewInfo, x.FixedWidth));

        private static void CalcColumnsLayoutForLargeFixedWidth(IList columns, double width, double totalWidth, GridViewInfo viewInfo, bool allowFixedWidth, Action<BaseColumn, int> setColumnWidth)
        {
            double num = width - totalWidth;
            int unfixedColumnsCount = GetUnfixedColumnsCount(columns);
            num = (unfixedColumnsCount != 0) ? (num / ((double) unfixedColumnsCount)) : (allowFixedWidth ? 0.0 : (num / ((double) columns.Count)));
            if (double.IsInfinity(Math.Max(0.0, num)))
            {
                num = 0.0;
            }
            for (int i = 0; i < columns.Count; i++)
            {
                BaseColumn column = (BaseColumn) columns[i];
                column.HeaderWidth = viewInfo.GetColumnHeaderWidth(column) + ((!(column.FixedWidth & allowFixedWidth) || (unfixedColumnsCount == 0)) ? num : 0.0);
                setColumnWidth(column, i);
            }
        }

        private static void CalcColumnsLayoutForRegularCase(IList columns, double width, double totalWidth, double fixedWidth, GridViewInfo viewInfo, Action<BaseColumn, int> updateColumnWidth)
        {
            double num;
            if (double.IsInfinity(Math.Max((double) 0.0, (double) ((width - fixedWidth) / (totalWidth - fixedWidth)))))
            {
                num = 1.0;
            }
            for (int i = 0; i < columns.Count; i++)
            {
                BaseColumn column = (BaseColumn) columns[i];
                double num3 = GetColumnFixedWidth(column, viewInfo, column.FixedWidth);
                column.HeaderWidth = (Math.Max((double) 0.0, (double) (viewInfo.GetColumnHeaderWidth(column) - num3)) * num) + num3;
                updateColumnWidth(column, i);
            }
        }

        public static double GetColumnFixedWidth(BaseColumn column, GridViewInfo viewInfo, bool allowFixedWidth = true) => 
            !(column.FixedWidth & allowFixedWidth) ? viewInfo.GetColumnFixedWidthCore(column) : viewInfo.GetDesiredColumnWidth(column);

        public static double GetRoundedActualColumnWidth(double columnWidth, ref double rest, bool isLastColumn = false)
        {
            double num = DXArranger.Round(columnWidth + rest, 4);
            if (isLastColumn)
            {
                return DXArranger.Floor(num, 4);
            }
            num = DXArranger.Round(num, 0);
            rest += columnWidth - num;
            return num;
        }

        private static int GetUnfixedColumnsCount(IList columns)
        {
            Func<BaseColumn, bool> predicate = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<BaseColumn, bool> local1 = <>c.<>9__4_0;
                predicate = <>c.<>9__4_0 = x => !x.FixedWidth;
            }
            return columns.Cast<BaseColumn>().Count<BaseColumn>(predicate);
        }

        private static bool IsLastColumn(bool needRoundingLastColumn, int count, int index) => 
            !needRoundingLastColumn && (index == (count - 1));

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AutoWidthHelper.<>c <>9 = new AutoWidthHelper.<>c();
            public static Func<BaseColumn, bool> <>9__4_0;

            internal bool <GetUnfixedColumnsCount>b__4_0(BaseColumn x) => 
                !x.FixedWidth;
        }
    }
}

