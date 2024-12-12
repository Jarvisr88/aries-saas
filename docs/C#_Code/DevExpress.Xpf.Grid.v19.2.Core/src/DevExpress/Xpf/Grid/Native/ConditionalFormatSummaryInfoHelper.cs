namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Data;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Data;
    using System;
    using System.Runtime.CompilerServices;

    public static class ConditionalFormatSummaryInfoHelper
    {
        public static bool AreSameItems(ServiceSummaryItem item, ConditionalFormatSummaryType summaryType, string fieldName, DataProviderBase dataProvider)
        {
            Type type = GetType(fieldName, dataProvider);
            if ((item.FieldName != fieldName) || (item.SummaryType != ToSummaryItemType(summaryType, type)))
            {
                return false;
            }
            CustomServiceSummaryItemType? customServiceSummaryItemType = item.CustomServiceSummaryItemType;
            CustomServiceSummaryItemType? nullable2 = ToCustomServiceSummaryItemType(summaryType, type);
            return ((customServiceSummaryItemType.GetValueOrDefault() == nullable2.GetValueOrDefault()) ? ((customServiceSummaryItemType != null) == (nullable2 != null)) : false);
        }

        private static Type GetType(string fieldName, DataProviderBase dataProvider)
        {
            Func<DataColumnInfo, Type> evaluator = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<DataColumnInfo, Type> local1 = <>c.<>9__4_0;
                evaluator = <>c.<>9__4_0 = x => x.Type;
            }
            return dataProvider.GetActualColumnInfo(fieldName).With<DataColumnInfo, Type>(evaluator);
        }

        private static CustomServiceSummaryItemType? ToCustomServiceSummaryItemType(ConditionalFormatSummaryType summaryType, Type type)
        {
            switch (summaryType)
            {
                case ConditionalFormatSummaryType.Average:
                    if (type == typeof(DateTime))
                    {
                        return 0;
                    }
                    return null;

                case ConditionalFormatSummaryType.SortedList:
                    return 3;

                case ConditionalFormatSummaryType.Unique:
                    return 4;

                case ConditionalFormatSummaryType.Duplicate:
                    return 5;
            }
            return null;
        }

        public static ServiceSummaryItem ToSummaryItem(ConditionalFormatSummaryType summaryType, string fieldName, DataProviderBase dataProvider)
        {
            Type type = GetType(fieldName, dataProvider);
            ServiceSummaryItem item1 = new ServiceSummaryItem();
            item1.SummaryType = ToSummaryItemType(summaryType, type);
            item1.FieldName = fieldName;
            item1.CustomServiceSummaryItemType = ToCustomServiceSummaryItemType(summaryType, type);
            return item1;
        }

        public static ServiceSummaryItemKey ToSummaryItemKey(ServiceSummaryItem serviceSummaryItem) => 
            new ServiceSummaryItemKey(serviceSummaryItem.FieldName, serviceSummaryItem.CustomServiceSummaryItemType, serviceSummaryItem.SummaryType);

        public static ServiceSummaryItemKey ToSummaryItemKey(ConditionalFormatSummaryType summaryType, string fieldName, DataProviderBase dataProvider)
        {
            Type type = GetType(fieldName, dataProvider);
            return new ServiceSummaryItemKey(fieldName, ToCustomServiceSummaryItemType(summaryType, type), ToSummaryItemType(summaryType, type));
        }

        private static SummaryItemType ToSummaryItemType(ConditionalFormatSummaryType summaryType, Type type)
        {
            switch (summaryType)
            {
                case ConditionalFormatSummaryType.Min:
                    return SummaryItemType.Min;

                case ConditionalFormatSummaryType.Max:
                    return SummaryItemType.Max;

                case ConditionalFormatSummaryType.Average:
                    return ((type != typeof(DateTime)) ? SummaryItemType.Average : SummaryItemType.Custom);

                case ConditionalFormatSummaryType.SortedList:
                case ConditionalFormatSummaryType.Unique:
                case ConditionalFormatSummaryType.Duplicate:
                    return SummaryItemType.Custom;
            }
            throw new InvalidOperationException();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ConditionalFormatSummaryInfoHelper.<>c <>9 = new ConditionalFormatSummaryInfoHelper.<>c();
            public static Func<DataColumnInfo, Type> <>9__4_0;

            internal Type <GetType>b__4_0(DataColumnInfo x) => 
                x.Type;
        }
    }
}

