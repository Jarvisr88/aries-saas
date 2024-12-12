namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Grid.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class DatePeriodFilterValue : ImmutableObject
    {
        internal DatePeriodFilterValue(FilterDateType filterDateType, string displayText)
        {
            this.<Value>k__BackingField = filterDateType;
            this.<DisplayText>k__BackingField = displayText;
        }

        internal FilterDateType Value { get; }

        public string DisplayText { get; }
    }
}

