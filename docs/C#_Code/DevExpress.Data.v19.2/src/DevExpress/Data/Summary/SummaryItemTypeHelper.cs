namespace DevExpress.Data.Summary
{
    using DevExpress.Data;
    using System;

    public static class SummaryItemTypeHelper
    {
        public static SummaryItemType[] generalPermanentTypes;
        public static SummaryItemType[] generalSumTypes;
        public static SummaryItemType[] generalComparableTypes;
        private static readonly Type[] numericalTypes;

        static SummaryItemTypeHelper();
        public static bool CanApplySummary(SummaryItemType summaryType, Type objectType);
        public static bool CanApplySummary<T>(T summaryType, Type objectType, T[] permanentTypes, T[] sumTypes, T[] comparableTypes) where T: struct;
        public static bool IsBool(Type type);
        public static bool IsDateTime(Type type);
        public static bool IsNumericalType(Type type);
        public static bool IsTimeSpan(Type type);
    }
}

