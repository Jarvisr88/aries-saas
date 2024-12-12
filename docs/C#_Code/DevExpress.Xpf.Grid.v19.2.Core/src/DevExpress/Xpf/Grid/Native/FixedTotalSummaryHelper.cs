namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Data;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class FixedTotalSummaryHelper
    {
        private static void GenerateCountTotalSummary(DevExpress.Xpf.Grid.SummaryItemBase item, Func<DevExpress.Xpf.Grid.SummaryItemBase, object> getSummaryValue, IList<GridTotalSummaryData> destination)
        {
            object obj2 = getSummaryValue(item);
            destination.Add(new GridTotalSummaryData(item, obj2, null));
        }

        public static void GenerateTotalSummaries(IList<DevExpress.Xpf.Grid.SummaryItemBase> source, IColumnCollection ColumnsCore, Func<DevExpress.Xpf.Grid.SummaryItemBase, object> getSummaryValue, IList<GridTotalSummaryData> destination)
        {
            destination.Clear();
            foreach (DevExpress.Xpf.Grid.SummaryItemBase base2 in source)
            {
                if (string.IsNullOrEmpty(base2.FieldName) && (base2.SummaryType == SummaryItemType.Count))
                {
                    GenerateCountTotalSummary(base2, getSummaryValue, destination);
                    continue;
                }
                destination.Add(new GridTotalSummaryData(base2, getSummaryValue(base2), ColumnsCore[base2.FieldName]));
            }
        }

        public static string GetFixedSummariesString(IList<GridTotalSummaryData> totalSummaryDataList)
        {
            InlineCollectionInfo fixedSummariesTextValues = GetFixedSummariesTextValues(totalSummaryDataList, null);
            return fixedSummariesTextValues?.TextSource;
        }

        public static InlineCollectionInfo GetFixedSummariesTextValues(IList<GridTotalSummaryData> totalSummaryDataList, Style fixedTotalSummaryElementStyle = null)
        {
            SummaryInlineInfoCreator creator = new SummaryInlineInfoCreator {
                SeparatorText = GridControlLocalizer.GetString(GridControlStringId.SummaryItemsSeparator),
                DefaultStyle = fixedTotalSummaryElementStyle
            };
            Func<GridSummaryData, string> func1 = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<GridSummaryData, string> local1 = <>c.<>9__1_0;
                func1 = <>c.<>9__1_0 = d => ColumnBase.GetSummaryText(d, true);
            }
            creator.GetItemDisplayText = func1;
            Func<DevExpress.Xpf.Grid.SummaryItemBase, Style> func2 = <>c.<>9__1_1;
            if (<>c.<>9__1_1 == null)
            {
                Func<DevExpress.Xpf.Grid.SummaryItemBase, Style> local2 = <>c.<>9__1_1;
                func2 = <>c.<>9__1_1 = s => s.FixedTotalSummaryElementStyle;
            }
            creator.GetSummaryStyle = func2;
            return creator.Create((IEnumerable<GridSummaryData>) totalSummaryDataList);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FixedTotalSummaryHelper.<>c <>9 = new FixedTotalSummaryHelper.<>c();
            public static Func<GridSummaryData, string> <>9__1_0;
            public static Func<DevExpress.Xpf.Grid.SummaryItemBase, Style> <>9__1_1;

            internal string <GetFixedSummariesTextValues>b__1_0(GridSummaryData d) => 
                ColumnBase.GetSummaryText(d, true);

            internal Style <GetFixedSummariesTextValues>b__1_1(DevExpress.Xpf.Grid.SummaryItemBase s) => 
                s.FixedTotalSummaryElementStyle;
        }
    }
}

