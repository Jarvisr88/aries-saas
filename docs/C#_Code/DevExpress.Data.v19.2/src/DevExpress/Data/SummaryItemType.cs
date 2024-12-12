namespace DevExpress.Data
{
    using DevExpress.Data.Design;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(SummaryItemTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum SummaryItemType
    {
        public const SummaryItemType Sum = SummaryItemType.Sum;,
        public const SummaryItemType Min = SummaryItemType.Min;,
        public const SummaryItemType Max = SummaryItemType.Max;,
        public const SummaryItemType Count = SummaryItemType.Count;,
        public const SummaryItemType Average = SummaryItemType.Average;,
        public const SummaryItemType Custom = SummaryItemType.Custom;,
        public const SummaryItemType None = SummaryItemType.None;
    }
}

