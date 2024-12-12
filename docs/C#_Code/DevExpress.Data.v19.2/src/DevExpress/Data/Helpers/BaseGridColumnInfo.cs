namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Utils;
    using DevExpress.XtraGrid;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class BaseGridColumnInfo : IDisposable
    {
        private FilterDataOutlookDateHelper outlookHelper;
        private BaseFilterData data;
        private bool required;
        private object column;
        private bool isLastSortColumn;
        private bool isGrouped;
        public ColumnSortMode SortMode;
        public ColumnGroupInterval GroupInterval;
        private static FormatInfo[] defaultFormats;

        static BaseGridColumnInfo();
        protected BaseGridColumnInfo(BaseFilterData data, object column);
        public int? CompareGroupValues(int listSourceRow1, int listSourceRow2, object value1, object value2);
        private static int CompareNullableComparables<T>(T? v1, T? v2) where T: struct, IComparable<T>;
        public int? CompareSortValues(int listSourceRow1, int listSourceRow2, object value1, object value2, ColumnSortOrder sortOrder);
        private static FormatInfo CreateFormat(FormatType type, string format);
        public virtual void Dispose();
        public string GetAlpha(object val);
        public virtual FormatInfo GetColumnGroupFormat();
        public ExpressiveSortInfo.Cell GetCompareGroupValuesInfo(Type basicExtractorType);
        public ExpressiveSortInfo.Cell GetCompareSortValuesInfo(Type basicExtractorType, ColumnSortOrder sortOrder);
        public DateTime? GetDate(DateTime? ndt);
        private int? GetDateInt(DateTime? ndt);
        public DateTime? GetDateMonth(DateTime? ndt);
        private int? GetDateMonthInt(DateTime? ndt);
        public DateTime? GetDateTime(object val);
        public DateTime? GetDateYear(DateTime? ndt);
        private int? GetDateYearInt(DateTime? ndt);
        private string GetDayName(int day);
        public FormatInfo GetDefaultFormat();
        public abstract string GetDisplayText(int listSourceIndex, object val);
        public string GetGroupDisplayText(object val, string text);
        public string GetOutlookDisplayText(object val);
        public OutlookInterval? GetOutlookInterval(DateTime? ndt);
        private ExpressiveSortInfo.Cell MakeDateRangeExtractorUndComparerInfo(Func<DateTime?, int?> transformator, Type basicExtractorType);
        protected abstract int? RaiseCustomGroup(int listSourceRow1, int listSourceRow2, object value1, object value2, ColumnSortOrder columnSortOrder);
        protected abstract int? RaiseCustomSort(int listSourceRow1, int listSourceRow2, object value1, object value2, ColumnSortOrder sortOrder);
        public object UpdateGroupDisplayValue(object val);

        public object Column { get; }

        public virtual bool Required { get; set; }

        public BaseFilterData Data { get; }

        protected bool IsGrouped { get; }

        protected bool IsLastSortColumn { get; }

        public bool AllowImageGroup { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseGridColumnInfo.<>c <>9;
            public static Func<DateTime, DateTime, int> <>9__37_3;
            public static Func<DateTime?, DateTime?, int> <>9__37_4;
            public static Func<int?, int?, int> <>9__43_0;

            static <>c();
            internal int <GetCompareSortValuesInfo>b__37_3(DateTime v1, DateTime v2);
            internal int <GetCompareSortValuesInfo>b__37_4(DateTime? v1, DateTime? v2);
            internal int <MakeDateRangeExtractorUndComparerInfo>b__43_0(int? g1, int? g2);
        }
    }
}

